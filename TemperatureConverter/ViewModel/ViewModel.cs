using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cells;
using Model;

namespace ViewModel
{
    public class ConverterViewModel
    {
        public ConverterViewModel()
        {
            this.TemperatureInKelvin = new Cell<double>();

            this.Kelvin = new TemperatureScaleViewModel(this, new KelvinTemperatureScale());
            this.Celsius = new TemperatureScaleViewModel(this, new CelsiusTemperatureScale());
            this.Fahrenheit = new TemperatureScaleViewModel(this, new FahrenheitTemperatureScale());
        }

        public Cell<double> TemperatureInKelvin { get; }

        public TemperatureScaleViewModel Kelvin { get; }

        public TemperatureScaleViewModel Celsius { get; }

        public TemperatureScaleViewModel Fahrenheit { get; }

        public IEnumerable<TemperatureScaleViewModel> Scales
        {
            get
            {
                yield return Celsius;
                yield return Fahrenheit;
                yield return Kelvin;
            }
        }
    }

    public class TemperatureScaleViewModel
    {
        private readonly ConverterViewModel parent;

        private readonly ITemperatureScale temperatureScale;

        public TemperatureScaleViewModel(ConverterViewModel parent, ITemperatureScale temperatureScale)
        {
            this.parent = parent;
            this.temperatureScale = temperatureScale;
            this.Temperature = this.parent.TemperatureInKelvin.Derive(kelvin => temperatureScale.ConvertFromKelvin(kelvin), t => temperatureScale.ConvertToKelvin(t));

            var minimum = temperatureScale.ConvertFromKelvin(0);
            var maximum = temperatureScale.ConvertFromKelvin(1000);
            this.Increment = new AddCommand(this.Temperature, 1, minimum, maximum);
            this.Decrement= new AddCommand(this.Temperature, -1, minimum, maximum);
        }

        public string Name => temperatureScale.Name;

        public Cell<double> Temperature { get; }

        public ICommand Increment { get; }

        public ICommand Decrement { get; }
    }
    
    public class AddCommand : ICommand
    {
        private readonly Cell<double> cell;

        private readonly int delta;

        private readonly double minimum;

        private readonly double maximum;

        public AddCommand(Cell<double> cell, int delta, double minimum, double maximum)
        {
            this.cell = cell;
            this.delta = delta;
            this.minimum = minimum;
            this.maximum = maximum;

            this.cell.PropertyChanged += (sender, args) => CanExecuteChanged(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var newValue = cell.Value + delta;

            return minimum <= newValue && newValue <= maximum;
        }

        public void Execute(object parameter)
        {
            cell.Value = Math.Round(cell.Value + delta);
        }
    }
}
