using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Crossword_Filler
{
	internal static class Program
	{

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		public static IConfiguration Configuration;

		[STAThread]
		static void Main(string[] args)
		{
			Configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			//ApplicationConfiguration.Initialize();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
			Application.SetDefaultFont(new Font("Segoe UI", 9f));
			Application.Run(new Form1(args));
		}
	}
}
