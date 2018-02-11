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

namespace View
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

        private void ConvertFahrenheit(object sender, RoutedEventArgs e)
        {
            var fahrenheitString = fahrenheitTextBox.Text;
            var fahrenheit = double.Parse(fahrenheitString);
            var celsius = (fahrenheit - 32) / 1.8;
            var kelvin = celsius + 273.15;
            var celsiusString = celsius.ToString();
            var kelvinString = kelvin.ToString();
            celsiusTextBox.Text = celsiusString;
            kelvinTextBox.Text = kelvinString;
        }

        private void ConvertCelsius(object sender, RoutedEventArgs e)
        {
            var celsiusString = celsiusTextBox.Text;
            var celsius = double.Parse(celsiusString);
            var fahrenheit = celsius * 1.8 + 32;
            var kelvin = celsius + 273.15;
            var fahrenheitString = fahrenheit.ToString();
            var kelvinString = kelvin.ToString();
            fahrenheitTextBox.Text = fahrenheitString;
            kelvinTextBox.Text = kelvinString;
        }

        private void ConvertKelvin(object sender, RoutedEventArgs e)
        {
            var kelvinString = kelvinTextBox.Text;
            var kelvin = double.Parse(kelvinString);
            var celsius = kelvin - 273.15;
            var fahrenheit = celsius * 1.8 + 32;
            var fahrenheitString = fahrenheit.ToString();
            var celsiusString = celsius.ToString();
            fahrenheitTextBox.Text = fahrenheitString;
            celsiusTextBox.Text = celsiusString;
        }
    }
}
