using ASADemoTelemetryDriver.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace ASADemoTelemetryDriver
{
    public class TemperatureDataReader : ITemperatureDataReader
    {
        public TemperatureDataReader() { }

        public string GetTemperatureData()
        {
            string data = string.Empty;

            StreamReader sReader = new StreamReader(ConfigurationManager.AppSettings["SourceTemperatureData"]);

            data = sReader.ReadToEnd();

            return data;
        }
    }
}
