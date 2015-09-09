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
            int myVar = 100;
            int i = myVar;
            Console.Write(i);
            Output("zalupa is " + i);

            bool meaningOfLive = new Boolean();
            Output("zalupa is " + meaningOfLive);

            var impliciteValue = 56U;
            Output("zalupa is " + impliciteValue);
            Output("zalupa is " + impliciteValue.GetType().ToString());

            byte value =255;
            value += 2;
            Output("zalupa is " + value );
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
