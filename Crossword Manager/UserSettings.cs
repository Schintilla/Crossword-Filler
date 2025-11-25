using System.Collections.Generic;

namespace Crossword_Filler
{
	public class UserSettings
	{
		public List<string> RecentFiles { get; set; } = new List<string>();
		public string LastCrossword { get; set; } = "1";
		public string LastJSON { get; set; } = "";
	}
}
