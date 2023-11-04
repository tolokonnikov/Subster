using Microsoft.Win32;
using Subtitles;
using System.IO;
using System.Windows;

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
        private readonly IGetSubtitlesFromMKV _subtitlesFromMKV;

        public MainWindow(IGetSubtitlesFromMKV subtitlesFromMKV)
        {
            InitializeComponent();
            _subtitlesFromMKV = subtitlesFromMKV;
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mkv files (*.mkv)|*.mkv|all files (*.*)|*.*";

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
                _subtitlesFromMKV.GetSubtitlesListFromMKV(_selectedFileName, _selectedDirectory);
            }
        }
    }
}