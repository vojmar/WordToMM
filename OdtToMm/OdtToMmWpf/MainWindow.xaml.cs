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
using Microsoft.Win32;

using OdtToMm;


namespace OdtToMmWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog ODTofd;
        private SaveFileDialog MMsfd;
        private MMParser mmparser;
        private string ODTFilePath;
        private string MMFilePath;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void selOdt(object sender, RoutedEventArgs e) 
        {

        }

        private void selOdt(object sender, MouseButtonEventArgs e)
        {

        }

        private void selSave(object sender, RoutedEventArgs e)
        {

        }

        private void selSave(object sender, MouseButtonEventArgs e)
        {

        }

        private void doStuff(object sender, RoutedEventArgs e)
        {

        }
    }
}
