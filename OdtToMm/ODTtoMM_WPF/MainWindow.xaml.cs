using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace ODTtoMM_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog ODTofd;
        private SaveFileDialog MMsfd;
        private string ODTFilePath;
        private string MMFilePath;
        private OdtParser odtParser;
        private MMParser mmParser;

        public MainWindow()
        {
            InitializeComponent();
            odtParser = new OdtParser();
            mmParser = new MMParser();
            odtParser.nodeParsed += OdtParser_nodeParsed;
            mmParser.OnNodeParseStep += MmParser_OnNodeParseStep;
            mmParser.OnMMParseEnded += MmParser_OnMMParseEnded;
            ODTofd = new OpenFileDialog();
            ODTofd.Filter = "Odt files |*.odt|" +
                "Word files |*.doc;*.docx|" +
                "All files (*.*)|*.*";
            ODTofd.Multiselect = false;
            MMsfd = new SaveFileDialog();
        }

        private void MmParser_OnMMParseEnded(object sender, MMParseEndedEventArgs e)
        {
            if (e.successful)
            {
                System.Windows.MessageBox.Show("Conversion completed!");
                Dispatcher.Invoke(() => conversionPB.Value = 100);
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to convert file!");
                Dispatcher.Invoke(() => conversionPB.Value = 0);
            }
        }

        private void MmParser_OnNodeParseStep(object sender, NodeParseStepEventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => conversionPB.Value = e.percentage / 2 + 50);
                return;
            }
            conversionPB.Value = e.percentage / 2 + 50;
        }

        private void OdtParser_nodeParsed(object sender, NodeParsedEventArgs e)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => conversionPB.Value = e.percentage / 2);
                return;
            }
            conversionPB.Value = e.percentage / 2;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void outputBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MMsfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputTB.Text = MMsfd.FileName;
                MMFilePath = MMsfd.FileName;
            }
        }

        private void inputBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ODTofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inputTB.Text = ODTofd.FileName;
                ODTFilePath = ODTofd.FileName;
            }
        }

        private async void convertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (
                (ODTFilePath != null) &&
                (ODTFilePath != "") &&
                (MMFilePath != null) &&
                (MMFilePath != "")
               )
            {
                try
                {
                    FreeMindNodeCollection fmnCollection = await odtParser.ParseOdt(ODTFilePath);
                    await mmParser.ParseAndSaveMM(MMFilePath, fmnCollection);
                }
                catch { System.Windows.MessageBox.Show("Conversion failed!"); }
            }
        }
    }
}
