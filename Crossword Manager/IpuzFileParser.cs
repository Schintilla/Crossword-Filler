// File: IpuzFileParser.cs
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crossword_Filler
{
	public class IpuzFileParser : IPuzzleParser
	{
		public async Task<CrosswordPuzzle> Parse(string filePath)
		{
			string jsonContent = await Task.Run(() => File.ReadAllText(filePath));

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				Converters = { new CellOrIntListConverter(), new ClueConverter(), new CharArray2DConverter() }
			};

			return JsonSerializer.Deserialize<CrosswordPuzzle>(jsonContent, options);
		}
	}
}

