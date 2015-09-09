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
            char[] charArray = { 'h', 'e', 'l', 'l', 'o' };
            Output("The type is " + charArray);
            string hello = new String(charArray);
            Output("The type is " + hello);
            string hello2 = hello.ToUpper();
            Output("The type is " + hello2);

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
