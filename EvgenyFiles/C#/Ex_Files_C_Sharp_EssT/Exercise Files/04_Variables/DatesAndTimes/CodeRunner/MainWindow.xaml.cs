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
            DateTime dt = new DateTime(2014, 1, 1);
            Output("The date is " + dt.ToString("MM, d, yy"));
            DateTime now = DateTime.Now;
            //Output("The date is " + now.ToString("MM, d, yy"));

            DateTime another = dt.AddDays(-1);
            Output("The date is " + another.ToString("MM, d, yy"));
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
