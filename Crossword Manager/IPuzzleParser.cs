using System.Threading.Tasks;

namespace Crossword_Filler
{
	public interface IPuzzleParser
	{
		Task<CrosswordPuzzle> Parse(string filePath);
	}
}
