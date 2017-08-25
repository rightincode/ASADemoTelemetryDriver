using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ASADemoTelemetryDriver.Interfaces;

namespace ASADemoTelemetryDriver
{
    public class TempJsonProcessor
    {
        private ITemperatureDataReader _tempReader;

        public TempJsonProcessor(ITemperatureDataReader tempReader)
        {
            _tempReader = tempReader;
        }

        public IEnumerable<TempReading> LoadTempReadings()
        {
            List<TempReading> tempReadings = new List<TempReading>();

            string tempDataText = _tempReader.GetTemperatureData();

            List<JObject> fullReadings = JsonConvert.DeserializeObject<List<JObject>>(tempDataText);
            
            foreach(JObject fullReading in fullReadings)
            {
                TempReading currentReading = new TempReading
                {
                    station = fullReading["STATION"].ToString(),
                    stationName = fullReading["STATION_NAME"].ToString()
                };

                Double tempLat;
                Double.TryParse(fullReading["LATITUDE"].ToString(), out tempLat);
                currentReading.latitude = tempLat;
                
                Double tempLong;
                Double.TryParse(fullReading["LONGITUDE"].ToString(), out tempLong);
                currentReading.longitude = tempLong;

                DateTime tempReadingDateTime;
                DateTime.TryParse(fullReading["DATE"].ToString(), out tempReadingDateTime);
                currentReading.readingDateTime = tempReadingDateTime;

                Double tempF;
                Double.TryParse(fullReading["HOURLYDRYBULBTEMPF"].ToString(), out tempF);
                currentReading.tempF = tempF;

                Double tempC;
                Double.TryParse(fullReading["HOURLYDRYBULBTEMPC"].ToString(), out tempC);
                currentReading.tempC = tempC;

                Double dewPointTempF;
                Double.TryParse(fullReading["HOURLYDewPointTempF"].ToString(), out dewPointTempF);
                currentReading.dewPointTempF = dewPointTempF;

                Double dewPointTempC;
                Double.TryParse(fullReading["HOURLYDewPointTempC"].ToString(), out dewPointTempC);
                currentReading.dewPointTempC = dewPointTempC;

                Double relativeHumidity;
                Double.TryParse(fullReading["HOURLYRelativeHumidity"].ToString(), out relativeHumidity);
                currentReading.relativeHumidity = relativeHumidity;

                tempReadings.Add(currentReading);
            }

            return tempReadings.Where(reading => reading.readingDateTime <= new DateTime(2017, 8, 12))
                        .OrderBy(reading => reading.readingDateTime);
        }
    }
}
