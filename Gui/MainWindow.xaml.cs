using Microsoft.Win32;
using Subtitles;
using System.Windows;
using System.IO;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _selectedFileName = string.Empty;
        private string _selectedFilePath = string.Empty;
        private string? _selectedDirectory = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MKV files (*.mkv)|*.mkv|Усі файли (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                _selectedFilePath = openFileDialog.FileName;

                if (!string.IsNullOrEmpty(_selectedFilePath))
                {
                    _selectedDirectory = Path.GetDirectoryName(_selectedFilePath);
                    _selectedFileName = Path.GetFileName(_selectedFilePath);

                    if (!string.IsNullOrEmpty(_selectedDirectory) && !string.IsNullOrEmpty(_selectedFileName))
                    {
                        lblSelectedFile.Content = Path.ChangeExtension(Path.Combine(_selectedDirectory, _selectedFileName), "srt");
                    }
                }
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedDirectory))
            {
                new MPlayer().GetSubtitlesListFromMKV(_selectedFileName, _selectedDirectory);
            }
        }
    }
}