# Crossword Manager — User Guide

Welcome to **Crossword Manager**! This guide walks you through importing puzzles, scanning grids and clues with OCR, solving crosswords, and using the built-in Cryptic Toolkit.

---

## Table of Contents
1. [Adding a Crossword](#1-adding-a-crossword)
2. [Using Image Pre-Processing](#2-using-image-pre-processing)
3. [Extracting Clues with OCR](#3-extracting-clues-with-ocr)
4. [Solving & Managing Your Library](#4-solving--managing-your-library)
5. [Using the Cryptic & Solving Toolkit](#5-using-the-cryptic--solving-toolkit)
6. [Importing & Exporting (.puz / .ipuz)](#6-importing--exporting-puz--ipuz)

---

## 1. Adding a Crossword

You can bring a crossword into Crossword Manager in two ways:

### Option A: From an Image or Hardcopy Scan
1. Open the app and select **New Crossword from Image** *(adjust to match your exact menu/button name)*.
2. Load your crossword grid image (`.png`, `.jpg`, `.bmp`, etc.) from a file, paste it from your clipboard, or capture it directly from your screen.
3. Load the **Across** and **Down** clue images using the same methods.
   * *Note:* Clues remain stored as `.png` images alongside your puzzle unless you convert them to editable text using OCR.

### Option B: From a Digital Puzzle File
* Use **File > Import** to load an existing `.puz` or `.ipuz` file. This loads the grid, clues, and any embedded solutions or hints immediately without scanning.

---

## 2. Using Image Pre-Processing

If you are working from a photo, newspaper scan, or low-resolution screenshot, cleaning up the image first will improve grid detection and OCR accuracy.

1. Open the **Pre-Processing** panel after loading your grid or clue image.
2. Adjust the on-the-fly filters as needed:
   * **Scale:** Enlarge small or low-DPI text so characters are easier to read.
   * **Sharpen:** Crisp up blurry edges from camera photos.
   * **Threshold:** Convert greyscale or coloured backgrounds into high-contrast black-and-white text.
   * **Denoise:** Remove speckles, paper grain, or compression artifacts.
3. Preview the changes in real time and apply them before scanning the grid or running OCR.

---

## 3. Extracting Clues with OCR

Converting clue images to text makes them searchable and lets you send words directly to the **Clues Explorer** and **AI Lookup**.

1. Choose your preferred OCR source:
   * **From Loaded Image:** Run OCR on the clue image you already attached.
   * **From Clipboard:** Copy a screenshot of clues (`Win + Shift + S`) and paste directly into the OCR tool.
   * **From Screen:** Use the built-in screen capture tool to drag a selection box over clues on your monitor.
2. Select your **OCR Engine**:
   * **Tesseract OCR (Local):** Fast, offline recognition using local language data. Works on both Windows 10 and Windows 11.
   * **Windows OCR (Windows 11):** Uses Windows 11's native OCR engine. Ideal for clean digital screenshots and varied fonts.
3. Review the extracted text in the editor and fix any minor character misreads before saving.

---

## 4. Solving & Managing Your Library

### Crossword Library
* All imported and scanned puzzles are saved in your **Crossword Library**.
* Open the library view at any time to browse unstarted, in-progress, or completed puzzles and pick up right where you left off.

### Filling the Grid
* Click any cell in the grid to select it, and click again (or press `Space` / `Tab` depending on your shortcuts) to toggle between **Across** and **Down**.
* Type letters directly into the grid. Your progress is saved automatically or via the **Save** button.

---

## 5. Using the Cryptic & Solving Toolkit

Stuck on a stubborn or cryptic clue? Open the solving tools to work through wordplay step by step:

* **Clues Explorer:**
  * **Anagram Solver:** Enter fodder letters to generate potential anagrams.
  * **Pattern Matching:** Enter known letters and blanks (e.g., `C _ O _ S _ O _ D`) to find matching words.
  * **Clue Dissecting & Elimination:** Break a clue down into definition, indicator words, and fodder, and rule out candidates systematically.
* **Solver's Scratchpad:** Use the free-text notepad to jot down letter combinations, synonyms, and thoughts while working through a clue.
* **Personal Hints Database:** Save common cryptic indicators (e.g., anagram flags, reversal words, hidden-word cues) and solving tricks to build your own searchable reference guide.
* **Online Dictionary:** Look up word definitions, synonyms, and phrases directly from the app.
* **AI Lookup:** Highlight or send a tricky clue to the AI Lookup tool for hints, wordplay explanations, or answer suggestions. *(Uses a free allocation tier; if rate limits are reached, wait briefly before trying again.)*

---

## 6. Importing & Exporting (.puz / .ipuz)

Share your digitised crosswords with friends or back them up in standard formats:

* **Exporting:** Select **File > Export** and choose `.puz` or `.ipuz`. This packages your grid, clues, and (optionally) your completed solution and hints into a single file.
* **Sharing Tip:** Exporting a hardcopy crossword to `.ipuz` lets others open and solve it digitally without needing to re-scan or OCR the original images.

---

## Need Help or Found a Bug?

Try testing the workflow using the files in the [`/Sample`](./Sample) folder first. If you encounter an issue, please open an issue in this GitHub repository with details about your Windows version and steps to reproduce the problem.
