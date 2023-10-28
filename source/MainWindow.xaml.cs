using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace StatsLover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool debugMode = false; // when true, activates all DEBUG MODE functions
        static string filePath;
        static string exePath;
        static string extractedText;
        private static object fileLock = new object();

        public MainWindow()
        {
            // initializes the UI components
            InitializeComponent();
        }

        /// <summary>
        /// This function gets called when the button is pressed to let the user picking the picture to convert into text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pickImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            // opens a dialog box in the user's operating system to select the path to a file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // filters the user selectable file type
            openFileDialog.Filter = "Image to convert|*.png; *.jpg|All files|*,*";

            // checks whether the user has pressed "OK" after selecting a file in the dialog box
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName; // saves the path (as a text) of the file selected by the user in the dialog box

                exePath = System.Reflection.Assembly.GetExecutingAssembly().Location; // gets the full path to the executable of this same code
                string scriptPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(exePath), "ImageToText.py"); // combines the path to the .exe file with the code in Python to execute

                // start an external Python process from this code in C#
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python", // starts the python interpreter
                    RedirectStandardInput = true, // provides input to the Python process from this code
                    RedirectStandardOutput = true, // authorizes the catch of program output in python by this code
                    RedirectStandardError = true, // authorizes the catch of errors output in python by this code
                    UseShellExecute = false, // does not use OS shell
                    CreateNoWindow = true, // does not create a dedicated window for the python script
                    Arguments = $"\"{scriptPath}\" \"{filePath}\"" // defines the location of the script in python and the file it will work on
                };

                // ensures that the resources of the external script are properly managed
                using (Process process = Process.Start(psi))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        // assigns the text read from the image to the result string variable thanks to the script in python
                        extractedText = reader.ReadToEnd();

                        // show an error window if the result of the conversion from image to text is null or a blank string full of spaces
                        if (string.IsNullOrEmpty(extractedText) || string.IsNullOrWhiteSpace(extractedText))
                        {
                            MessageBox.Show("No text was found in the selected image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (extractedText != null) // check else if the result is not null and has some text in it
                        {
                            imageToConvertText.Text = System.IO.Path.GetFileName(filePath); // show in the TextBlock the name of the file chosen
                            txtReadyManual.Visibility = Visibility.Visible;
                            downloadTxtManual.Visibility = Visibility.Visible;
                        }

                        // ----------- DEBUG MODE ------------
                        if (debugMode)
                        {
                            CheckTextContent(extractedText, filePath); // call the function to check the result content
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks the result content
        /// </summary>
        /// <param name="text">Contains the text extracted from the picture chosen by the user</param>
        private void CheckTextContent(string text, string filePath)
        {
            if (text != null)
            {
                MessageBox.Show(filePath);
                MessageBox.Show(text, "DEBUG: Extracted text", MessageBoxButton.OK, MessageBoxImage.Information);
                Console.WriteLine("Testo estratto nel codice C#: " + text);
            }
            else
            {
                MessageBox.Show(text, "DEBUG: No text found!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// This function creates a text file containing text extracted from an image chosen by the user
        /// </summary>
        /// <param name="exePath"> The path to the exe file </param>
        /// <param name="filePath"> The picture to be converted path </param>
        private void CreateTxtFile(string exePath, string filePath, string text)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + ".txt"; // creates a variable combining the name of the chosen image to be converted without extension and the extension .txt
            string fileLocation = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(exePath), fileName); // creates a path to the new .txt file in the same folder as the program's .exe file

            // check if the file already exists
            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation); // in case, delete it
            }
            else
            {
                // otherwise, create it and write the extracted text on it
                using (StreamWriter writer = new StreamWriter(fileLocation))
                {
                    // writes the extracted text in the file
                    writer.WriteLine(text);
                }
                MessageBox.Show("The text file has been generated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Creates the .txt file inside the exe path by clicking the button on the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadTxtManual_OnClick(object sender, RoutedEventArgs e)
        {
            CreateTxtFile(exePath, filePath, extractedText); // calls the function that creates the file
        }

        void customizeStatsButton_OnClick(object sender, RoutedEventArgs e)
        {
            customizeStatsPage.Visibility = Visibility.Visible;
            customStatsTitle.Visibility = Visibility.Visible;
            importXlsText.Visibility = Visibility.Visible;
            structureText.Visibility = Visibility.Visible;
            pickStructureButton.Visibility = Visibility.Visible;
            cancelCustomButton.Visibility = Visibility.Visible;
            saveCustomButton.Visibility = Visibility.Visible;
            structureRectangle.Visibility = Visibility.Visible;
        }

        void saveCustomButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        void cancelCustomButton_OnClick(object sender, RoutedEventArgs e)
        {
            customizeStatsPage.Visibility = Visibility.Hidden;
            customStatsTitle.Visibility = Visibility.Hidden;
            importXlsText.Visibility = Visibility.Hidden;
            structureText.Visibility = Visibility.Hidden;
            pickStructureButton.Visibility = Visibility.Hidden;
            cancelCustomButton.Visibility = Visibility.Hidden;
            saveCustomButton.Visibility = Visibility.Hidden;
            structureRectangle.Visibility = Visibility.Hidden;
        }

        void pickStructureButton_OnClick(object sender, RoutedEventArgs e)
        {
            // opens a dialog box in the user's operating system to select the path to a file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // filters the user selectable file type
            openFileDialog.Filter = "Text structure file|*.txt|All files|*,*";

            if (openFileDialog.ShowDialog() == true)
            {
                string txtPath = openFileDialog.FileName; // saves the path (as a text) of the file selected by the user in the dialog box

                structureText.Text = System.IO.Path.GetFileName(txtPath); // show in the TextBlock the name of the file chosen;
            }
        }

        private void settingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            creditsUI.Visibility = Visibility.Visible;
        }

        private void closeSettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            creditsUI.Visibility = Visibility.Hidden;
        }
    }
}
