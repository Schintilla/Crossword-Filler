using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Crossword_Filler
{
	public class IpuzFileSaver
	{
		// Add the 'async' keyword here
		public async Task Save(CrosswordPuzzle puzzle, string filePath, JsonSerializerOptions options)
		{
			var jsonContent = JsonSerializer.Serialize(puzzle, options);

			// The await keyword is now valid inside this method.
			await Task.Run(() => File.WriteAllText(filePath, jsonContent));
		}
	}
}

