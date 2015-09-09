using System;
using System.Windows;

namespace CodeRunner
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

        public const int MeanOfLive = 42;

        enum Flavores { qwertyzalupa=1,chlenix=2,dick=3}

        private void RunCode(object sender, RoutedEventArgs e)
        {
            // Place code here
            Flavores text;
            text = Flavores.qwertyzalupa;
            Output("zalupa" + text);

        }

        private void Output(string value)
        {
            txtOutput.Text += value + Environment.NewLine;
        }

        private void ClearOutput(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
        }
    }

}
