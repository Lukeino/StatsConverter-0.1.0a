# StatsConverter

## Image to Data Converter: Your Smart Statistics Extractor

Tired of manually transcribing statistics from screenshots or images? **StatsConverter** is a powerful and versatile tool designed to automate text extraction from visual elements on your computer, transforming them into organized and analyzable data. This project leverages **OCR (Optical Character Recognition) technology** via the `pytesseract` Python library, making it ideal for analyzing game statistics, software data, or any textual information present in your images.

### How it Works:

StatsConverter is primarily a **WPF (C#) desktop application** that seamlessly integrates with a Python backend. The C# application handles the user interface and overall workflow, while a dedicated Python script (`ImageToText.py`) performs the heavy lifting of OCR using `pytesseract` and the Tesseract OCR engine.

<img src="https://github.com/Luca00IT/icons/blob/main/Tesseract_OCR_logo_(Google).png" width="500" />

### Key Features:

* **Text Extraction from Images:** Converts `.jpg` and `.png` image files into plain text.
* **Versatile Output:** Generates `.txt` files containing the extracted data.

### Technologies Used:

* **WPF (C#):** For the user interface and application logic.
* **Python:** For the OCR processing backend.
* **Pytesseract:** Python wrapper for the Tesseract OCR engine.
    * [Pytesseract on PyPI](https://pypi.org/project/pytesseract/)
* **Tesseract OCR Engine:** The core Optical Character Recognition engine.

### RELEASE 0.1.0a: The Starting Point

This is the initial public release of the project, encompassing core functionalities for data extraction and organization. The project is under continuous development, with the aim of adding new features and enhancing the user experience.

### Getting Started (Setup and Usage)

To get StatsConverter up and running on your system, please follow these steps:

1.  **Prerequisites:**
    * Ensure you have **Python 3.x** installed.
    * Ensure you have **.NET Framework** installed (required for WPF applications on Windows).
2.  **Install Python Dependencies:**
    * Open your terminal (Command Prompt/PowerShell) and install the necessary Python libraries:
        ```bash
        pip install pytesseract Pillow
        ```
3.  **Install Tesseract OCR Engine:**
    * **Tesseract OCR is the fundamental engine for text recognition.** You MUST download and install it on your system.
    * For **Windows**, download the installer from the official Tesseract-OCR GitHub page: [Tesseract-OCR for Windows Downloads](https://github.com/UB-Mannheim/tesseract/wiki/Downloads)
    * **During installation, it is crucial to select the option to "Add Tesseract to your PATH"** (or manually add the installation path, e.g., `C:\Program Files\Tesseract-OCR`, to your system's environment variables). This allows the Python script to find the Tesseract executable.
4.  **Download/Clone the Project:**
    * Download the compiled WPF `.exe` application (if provided in a release) or clone this GitHub repository to get the source code:
        ```bash
        git clone [https://github.com/YourGitHubUsername/StatsConverter.git](https://github.com/YourGitHubUsername/StatsConverter.git)
        ```
    * Navigate into the project directory:
        ```bash
        cd StatsConverter
        ```
5.  **Running the Application:**
    * If you downloaded a compiled `.exe`, simply run it.
    * If you cloned the source code, open the C# solution in Visual Studio and build/run the WPF project. The WPF application will then call the Python script (`ImageToText.py`) as needed.

### Detailed Usage Modes:

#### Manual Mode:

This mode guides you through the steps for a controlled extraction process:

1.  **Image Upload:** Upload a `.jpg` or `.png` file from your computer.
2.  **XLS Structure Definition:** Select or define the desired structure for your final `.xls` file.
3.  **String Database Management:** Create or import a database of strings to assist with data recognition and categorization.
4.  **Output Download:** Download the `.txt` or `.xls` file containing the extracted and organized text.

#### Automatic Mode:

For fast and continuous statistics extraction:

1.  **Database Category Selection:** Choose a predefined database category (e.g., "Video Games," "Basic Software").
2.  **Compiled Database Selection:** Select an already configured database for your specific needs.
3.  **Dedicated Screenshot Key:** Choose a hotkey to automatically capture screenshots.
4.  **Additive/Full Mode:** Decide whether to add data from each screenshot to a single Excel file or create a full Excel file for each screenshot.
    * *Goal:* Create entire statistical sets with a single key press, ideal for gaming sessions or continuous monitoring.

### Future Developments:

The project is continuously evolving! Some ideas for future versions include:

* Implementation of a graphical user interface (GUI) within the WPF app for improved usability.
* Enhancements in OCR accuracy through custom recognition models.
* Direct integration support with online spreadsheets.
* More advanced data analysis and visualization features.
