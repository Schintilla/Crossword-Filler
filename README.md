# Crossword Manager

A Windows desktop application built with C# and WinForms that lets you digitize, solve, and organize crosswords—especially those only available as hardcopies, printouts, or static images.

![App Screenshot](path/to/screenshot.png)

## Key Features

### Digitize & Capture Crosswords
* **Image & Clue Loading:** Import a crossword grid image along with its clues. Clues remain stored as `.png` images unless converted to text via OCR.
* **Flexible OCR Input:** Extract clues directly from your screen, paste from the clipboard, or scan from a loaded image file.
* **On-the-Fly Image Pre-Processing:** Improve scan and OCR accuracy on low-quality images with built-in tools to scale, sharpen, threshold, and denoise before processing.

### Organize & Share
* **Crossword Library:** Store all your in-progress and unstarted crosswords in one place so you can browse and resume solving at any time.
* **Import & Export Support:** Share puzzles or load digital files using standard `.puz` and `.ipuz` formats—preserving grids, solutions, and hints without needing OCR.

### Cryptic & Solving Toolkit
* **Clues Explorer:** Break down stubborn or cryptic clues with tools for anagramming, clue dissecting, letter pattern matching, and process of elimination.
* **Solver's Scratchpad:** A dedicated space for notes, wordplay ruminations, and working through clues out loud.
* **Personal Hints Database:** Build and maintain your own reference database of common cryptic indicators, techniques, and reminders.
* **Online Dictionary & AI Lookup:** Search words and phrases via integrated online dictionaries or use built-in AI lookup for extra help on obscure clues.

## Sample Files

Check the [`/Sample`](./Sample) folder in this repository for example crossword images, clue files, and `.puz`/`.ipuz` puzzles you can use to test the application right away.

## Getting Started

### Prerequisites
* **OS:** Windows 10 or Windows 11
* **Runtime:** [.NET Desktop Runtime](https://microsoft.com) *(update with your specific .NET version, e.g., .NET 8.0)*
* **IDE (for building from source):** Visual Studio 2022 with the **.NET desktop development** workload

### Installation & Running

#### Option 1: Run a Release Build
1. Go to the [Releases](../../releases) page and download the latest `.zip` archive.
2. Extract the files to a folder on your PC.
3. Run `CrosswordManager.exe`.

#### Option 2: Build from Source
1. Clone the repository:
   ```bash
   git clone https://github.com
   ```
2. Open the `.sln` solution file in Visual Studio 2022.
3. Restore NuGet packages and press **F5** (or click **Start**) to build and run the app.

## Contributing

Contributions, bug reports, and feature suggestions are welcome. Feel free to open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
