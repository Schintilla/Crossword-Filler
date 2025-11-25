using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Crossword_Filler
{

	public static class SettingsManager
	{
		//private static readonly string SettingsFilePath = Path.Combine(Application.StartupPath, "usersettings.json");
		//appPath = Application.StartupPath;

		private static string SettingsFilePath
		{
			get
			{
				// Combines the executable's directory path with the filename
				return Path.Combine(Application.StartupPath, "usersettings.json");
			}
		}

		public static UserSettings LoadSettings()
		{
			if (File.Exists(SettingsFilePath))
			{
				try
				{
					var json = File.ReadAllText(SettingsFilePath);
					return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
				}
				catch (Exception ex)
				{
					// Handle or log error during loading
					Console.WriteLine($"Error loading user settings: {ex.Message}");
				}
			}
			return new UserSettings();
		}

		public static void SaveSettings(UserSettings settings)
		{
			try
			{
				var directory = Path.GetDirectoryName(SettingsFilePath);
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(SettingsFilePath, json);
			}
			catch (UnauthorizedAccessException uacEx)
			{
				MessageBox.Show($"Permission denied saving settings to {SettingsFilePath}. Inner error: {uacEx.Message}");
				// Re-throw or handle the error properly
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving user settings: {ex.Message}");
				MessageBox.Show($"Error saving settings: {ex.Message}");
			}
		}
	}
}
