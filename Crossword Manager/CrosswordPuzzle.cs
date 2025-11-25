using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Crossword_Filler
{
	public class CrosswordPuzzle
	{
		// IPUZ properties
		[JsonPropertyName("kind")]
		public List<string> Kind { get; set; }

		[JsonPropertyName("version")]
		public string Version { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("author")]
		public string Author { get; set; }

		[JsonPropertyName("copyright")]
		public string Copyright { get; set; }

		[JsonPropertyName("dimensions")]
		public Dimensions Dimensions { get; set; }

		[JsonPropertyName("puzzle")]
		[JsonConverter(typeof(CellOrIntListConverter))] // Apply the custom converter
		public List<List<object>> Puzzle { get; set; } // Change the type to a List of Lists of objects

		[JsonPropertyName("solution")]
		public List<List<string>> Solution { get; set; }

		[JsonPropertyName("clues")]
		public Clues Clues { get; set; }

		[JsonPropertyName("usergrid")]
		public List<List<string>> UserAmswers { get; set; }

		// Additional properties for PUZ files, if needed
		public string Notes { get; set; }
		public char[,] PuzSolutionGrid { get; set; }
		public char[,] PuzUserGrid { get; set; }



	}

	public class Dimensions
	{
		[JsonPropertyName("width")]
		public int Width { get; set; }

		[JsonPropertyName("height")]
		public int Height { get; set; }
	}

	public class Cell
	{
		[JsonPropertyName("label")]
		public string Label { get; set; }

		[JsonPropertyName("cell")]
		public string Value { get; set; }
	}

	public class Clues
	{
		[JsonPropertyName("Across")]
		public List<Clue> Across { get; set; }

		[JsonPropertyName("Down")]
		public List<Clue> Down { get; set; }
	}

	public class Clue
	{
		[JsonPropertyName("clue")]
		public string Text { get; set; }

		[JsonPropertyName("label")]
		public string Number { get; set; }
	}
}

