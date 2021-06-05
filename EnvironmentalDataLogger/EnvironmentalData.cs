using System;

namespace EnvironmentalDataLogger
{
    public class EnvironmentalData
    {
        /// <summary>
        /// The time the measurement was taken.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>Temperature in degrees Celsius.</summary>
        public double Temperature { get; set; }

        /// <summary>Relative humidity in percent.</summary>
        public double Humidity { get; set; }

        /// <summary>Pressure in Pascal.</summary>
        public double Pressure { get; set; }

        /// <summary>Gas resistance in Ohm.</summary>
        public double GasResistance { get; set; }
    }
}
