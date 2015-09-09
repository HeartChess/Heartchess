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

            double value1 = 42;
            double value2 = 12;

            Output("Add: " + (42 + 12));
            Output("Subtract: " + (42 - 12));
            Output("Divide: " + (42 / 12));
            Output("Multiply: " + (42 * 12));
            Output("Remainder: " + (42 % 12));


            Output("inc: " + (++value1));
            Output("inc: " + (value1));


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
