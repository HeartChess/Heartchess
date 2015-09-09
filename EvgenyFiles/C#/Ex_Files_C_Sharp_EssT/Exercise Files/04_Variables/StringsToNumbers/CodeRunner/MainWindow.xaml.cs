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

        private void RunCode(object sender, RoutedEventArgs e)
        {
            //Place code here
            string s = "255";
            int stringToInt = Int32.Parse(s);
            Output("Value " + stringToInt);

            int doubled = stringToInt * 2;
            Output("Value " + doubled);

            string s2 = "23.4";
            double v;
            if (Double.TryParse(s2, out v))
            {
                Output("Value" + v);
            }
            else
            {
                Output("Could not parse");
            }

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
