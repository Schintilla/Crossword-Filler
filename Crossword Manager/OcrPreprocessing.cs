using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
namespace Crossword_Filler
{
	public static class OcrPreprocessing
	{
		public static Bitmap PreprocessForOcr(
			Bitmap inputBmp,
			double scale = 2.0,
			bool useDenoise = true,
			bool useMedian = false,
			bool useClahe = true,
			bool useAdaptiveThreshold = true,
			bool sharpen = true,
			double sharpenAmount = 0.8,
			bool useKMeans = false,
			int kMeansK = 4,
			int adaptiveBlockSize = 15,
			double adaptiveC = 9.0)
		{
			if (inputBmp == null)
				throw new ArgumentNullException(nameof(inputBmp));

			using (var src = BitmapConverter.ToMat(inputBmp))
			{
				Mat m = src.Clone();

				if (m.Channels() == 4)
				{
					Mat tmp = new Mat();
					Cv2.CvtColor(m, tmp, ColorConversionCodes.BGRA2BGR);
					m.Dispose();
					m = tmp;
					// Debug.WriteLine($"Converted BGRA->BGR: type={m.Type()}, ch={m.Channels()}");
				}
				else if (m.Channels() == 1)
				{
					// if somehow grayscale came in, promote to BGR for color ops
					Mat tmp = new Mat();
					Cv2.CvtColor(m, tmp, ColorConversionCodes.GRAY2BGR);
					m.Dispose();
					m = tmp;
					//Debug.WriteLine($"Converted GRAY->BGR: type={m.Type()}, ch={m.Channels()}");
				}
				//Debug.WriteLine($"Start: type={m.Type()}, ch={m.Channels()}");

				// 1️⃣ Resize
				if (Math.Abs(scale - 1.0) > 1e-9)
				{
					Mat tmp = new Mat();
					Cv2.Resize(m, tmp, new OpenCvSharp.Size(), scale, scale, InterpolationFlags.Cubic);
					m.Dispose();
					m = tmp;
					//Debug.WriteLine($"After resize: type={m.Type()}, ch={m.Channels()}");
				}

				// 2️⃣ Denoise (on BGR)
				if (useDenoise)
				{
					Mat tmp = new Mat();
					Cv2.FastNlMeansDenoisingColored(m, tmp, 8, 8, 7, 21);
					m.Dispose();
					m = tmp;
					//Debug.WriteLine($"After denoise: type={m.Type()}, ch={m.Channels()}");
				}

				// 3️⃣ Median blur
				if (useMedian)
				{
					Mat tmp = new Mat();
					Cv2.MedianBlur(m, tmp, 3);
					m.Dispose();
					m = tmp;
					//Debug.WriteLine($"After median: type={m.Type()}, ch={m.Channels()}");
				}

				// 4️⃣ K-means color reduction (must be BGR)
				if (useKMeans)
				{
					//Debug.WriteLine($"Before KMeans: type={m.Type()}, ch={m.Channels()}");
					Mat tmp = ColorReductionKMeans(m, kMeansK);
					m.Dispose();
					m = tmp;
					//Debug.WriteLine($"After KMeans: type={m.Type()}, ch={m.Channels()}");
				}

				// 5️⃣ Sharpen
				if (sharpen && sharpenAmount > 0.0)
				{
					Mat blurred = new Mat();
					Cv2.GaussianBlur(m, blurred, new OpenCvSharp.Size(3, 3), 0);
					Mat tmp = new Mat();
					Cv2.AddWeighted(m, 1.0 + sharpenAmount, blurred, -sharpenAmount, 0, tmp);
					blurred.Dispose();
					m.Dispose();
					m = tmp;
					//Debug.WriteLine($"After sharpen: type={m.Type()}, ch={m.Channels()}");
				}

				// 6️⃣ CLAHE (convert to gray first)
				Mat gray = SafeClahe(m, clipLimit: 2.0, tileGridSize: 8);
				//Debug.WriteLine($"After CLAHE: type={gray.Type()}, ch={gray.Channels()}");

				// 7️⃣ Adaptive threshold
				Mat bin = gray;
				if (useAdaptiveThreshold)
				{
					Mat thr = SafeAdaptiveThreshold(gray, adaptiveBlockSize, adaptiveC);
					if (!ReferenceEquals(thr, gray))
					{
						gray.Dispose();
						gray = thr;
					}
					bin = gray;
					//Debug.WriteLine($"After threshold: type={bin.Type()}, ch={bin.Channels()}");
				}

				// 8️⃣ Convert back to BGR
				Mat finalMat = new Mat();
				Cv2.CvtColor(bin, finalMat, ColorConversionCodes.GRAY2BGR);
				//Debug.WriteLine($"Final: type={finalMat.Type()}, ch={finalMat.Channels()}");

				// Cleanup
				if (!ReferenceEquals(bin, gray)) bin.Dispose();
				gray.Dispose();
				m.Dispose();

				Bitmap outBmp = BitmapConverter.ToBitmap(finalMat);
				finalMat.Dispose();
				return outBmp;
			}
		}

		// Safe helpers


		public static Mat UnsharpMask(Mat src, double amount = 1.0, int gaussianSize = 3)
		{
			if (src == null) throw new ArgumentNullException(nameof(src));
			if (gaussianSize <= 0) gaussianSize = 3;
			if ((gaussianSize % 2) == 0) gaussianSize++; // ensure odd kernel size

			// Work on a clone so we don't mutate caller's Mat
			Mat srcClone = src.Clone();

			Mat blurred = new Mat();
			Cv2.GaussianBlur(srcClone, blurred, new OpenCvSharp.Size(gaussianSize, gaussianSize), 0);

			Mat dst = new Mat();
			// dst = srcClone * (1 + amount) - blurred * amount
			Cv2.AddWeighted(srcClone, 1.0 + amount, blurred, -amount, 0, dst);

			// cleanup
			blurred.Dispose();
			srcClone.Dispose();

			return dst; // caller owns and must Dispose()
		}


		private static Mat EnsureGray8U(Mat src)
		{
			if (src.Type() == MatType.CV_8UC1)
			{
				// Already 8-bit grayscale
				return src.Clone();
			}

			Mat gray = new Mat();
			if (src.Channels() == 3)
			{
				Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
			}
			else if (src.Channels() == 4)
			{
				Cv2.CvtColor(src, gray, ColorConversionCodes.BGRA2GRAY);
			}
			else
			{
				throw new ArgumentException("Unsupported image format for EnsureGray8U");
			}

			return gray;
		}


		public static Mat SafeClahe(Mat src, double clipLimit = 2.0, int tileGridSize = 8)
		{
			using (var gray = EnsureGray8U(src))
			{
				var clahe = Cv2.CreateCLAHE(clipLimit, new OpenCvSharp.Size(tileGridSize, tileGridSize));
				Mat dst = new Mat();
				clahe.Apply(gray, dst);
				return dst;
			}
		}

		public static Mat SafeAdaptiveThreshold(Mat src, int blockSize = 15, double c = 9.0)
		{
			using (var gray = EnsureGray8U(src))
			{
				Mat dst = new Mat();
				Cv2.AdaptiveThreshold(gray, dst, 255,
					AdaptiveThresholdTypes.MeanC,
					ThresholdTypes.Binary,
					blockSize % 2 == 0 ? blockSize + 1 : blockSize, c);
				return dst;
			}
		}

		private static Mat ColorReductionKMeans(Mat src, int K)
		{
			if (src.Channels() != 3)
				throw new ArgumentException("ColorReductionKMeans expects a 3-channel BGR image (CV_8UC3).");

			// Debug info
			Console.WriteLine($"[DEBUG] Input type before KMeans: {src.Type()} (channels: {src.Channels()})");

			Mat samples = src.Reshape(1, src.Rows * src.Cols);
			samples.ConvertTo(samples, MatType.CV_32F);

			Mat labels = new Mat();
			Mat centers = new Mat();

			// ✅ Fix: explicitly qualify the enum flags for TermCriteria.Type
			var criteria = new TermCriteria(OpenCvSharp.CriteriaTypes.Eps | OpenCvSharp.CriteriaTypes.MaxIter, 10, 1.0);

			Cv2.Kmeans(
				data: samples,
				k: K,
				bestLabels: labels,
				criteria: criteria,
				attempts: 3,
				flags: KMeansFlags.PpCenters,
				centers: centers
			);

			centers.ConvertTo(centers, MatType.CV_8U);

			var reduced = new Mat(samples.Size(), MatType.CV_8UC1);
			for (int i = 0; i < samples.Rows; i++)
			{
				int clusterIdx = labels.Get<int>(i);
				reduced.Set(i, centers.At<Vec3b>(clusterIdx));
			}

			reduced = reduced.Reshape(3, src.Rows);

			//Console.WriteLine($"[DEBUG] Output type after KMeans: {reduced.Type()} (channels: {reduced.Channels()})");

			return reduced;
		}

	}
}

