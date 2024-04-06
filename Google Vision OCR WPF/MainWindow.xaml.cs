using Google.Cloud.Vision.V1;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Google_Vision_OCR_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                var fileNames = openFileDialog.FileNames;
                foreach (string file in fileNames)
                {
                    txtFilePath.Text = file;
                    imgPageImage.Source = new BitmapImage(new Uri( txtFilePath.Text));
                }

            }
        }

        private void butOcrStart_Click(object sender, RoutedEventArgs e)
        {
            doOcr(txtFilePath.Text);
        }

        private void doOcr(string fileName)
        {
            var jsonPath = @"E:\temp\ocr-img\urdu-text-ocr-80e9e1c09809.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonPath);

            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(fileName);

            try
            {
                TextAnnotation response = client.DetectDocumentText(image);
                rtbUrduOcrText.Document.Blocks.Clear();
                rtbUrduOcrText.AppendText(response.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}