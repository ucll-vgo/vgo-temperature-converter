using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public interface ITemperatureScale
    {
        string Name { get; }

        double ConvertToKelvin(double temperature);

        double ConvertFromKelvin(double temperature);
    }
}
