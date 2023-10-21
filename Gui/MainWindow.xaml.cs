using Microsoft.Win32;
using Subtitles;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Отримайте вибраний файл і його шлях
                string selectedFileName = openFileDialog.FileName;
                string selectedFilePath = openFileDialog.InitialDirectory;

                // Обробка вибраного файлу
                // Наприклад, відкриття файлу та читання його вмісту
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            new MPlayer().GetSubtitlesListFromMKV();
        }
    }
}