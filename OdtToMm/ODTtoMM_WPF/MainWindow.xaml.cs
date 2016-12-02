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
        public MainWindow()
        {
            InitializeComponent();
            ODTofd = new OpenFileDialog();
            ODTofd.Filter = "Odt files |*.odt|" +
                "Word files |*.doc;*.docx|" +
                "All files (*.*)|*.*";
            ODTofd.Multiselect = false;
            MMsfd = new SaveFileDialog();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void outputBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MMsfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inputTB.Text = MMsfd.FileName;
                MMFilePath = MMsfd.FileName;
            }
        }

        private void inputBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ODTofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputTB.Text = ODTofd.FileName;
                ODTFilePath = ODTofd.FileName;
            }
        }

        private void convertBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
