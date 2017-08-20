using System;

namespace ASADemoTelemetryDriver
{
    public class TempReading
    {
        public string station { get; set; }
        public string stationName { get; set; }
        public Double latitude { get; set; }
        public Double longitude { get; set; }
        public DateTime readingDateTime { get; set; }
        public Double tempF { get; set; }
        public Double tempC { get; set; }
        public Double dewPointTempF { get; set; }
        public Double dewPointTempC { get; set; }
        public Double relativeHumidity { get; set; }
    }
}
