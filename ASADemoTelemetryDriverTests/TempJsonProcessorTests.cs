using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ASADemoTelemetryDriver;
using ASADemoTelemetryDriver.Interfaces;


namespace ASADemoTelemetryDriverTests
{
    [TestClass]
    public class TempJsonProcessorTest
    {
        Mock<ITemperatureDataReader> _temperatureDataReaderMock;

        private readonly string sampleTemperatureReadings = "[{'STATION':'WBAN:93727','STATION_NAME':'NEW RIVER MCAF NC US','ELEVATION':'7.9','LATITUDE':'34.70842','LONGITUDE':'-77.43966','DATE':'2017-08-11 00:56','REPORTTPYE':'FM-15','HOURLYSKYCONDITIONS':'OVC:08 75','HOURLYVISIBILITY':'10.00','HOURLYPRSENTWEATHERTYPE':'','HOURLYDRYBULBTEMPF':'77','HOURLYDRYBULBTEMPC':'25.0','HOURLYWETBULBTEMPF':'77','HOURLYWETBULBTEMPC':'25.0','HOURLYDewPointTempF':'77','HOURLYDewPointTempC':'25.0','HOURLYRelativeHumidity':'100','HOURLYWindSpeed':'0','HOURLYWindDirection':'000','HOURLYWindGustSpeed':'','HOURLYStationPressure':'30.12','HOURLYPressureTendency':'6','HOURLYPressureChange':'','HOURLYSeaLevelPressure':'30.14','HOURLYPrecip':'','HOURLYAltimeterSetting':'30.14','DAILYMaximumDryBulbTemp':'','DAILYMinimumDryBulbTemp':'','DAILYAverageDryBulbTemp':'','DAILYDeptFromNormalAverageTemp':'','DAILYAverageRelativeHumidity':'','DAILYAverageDewPointTemp':'','DAILYAverageWetBulbTemp':'','DAILYHeatingDegreeDays':'','DAILYCoolingDegreeDays':'','DAILYSunrise':'0527','DAILYSunset':'1902','DAILYWeather':'','DAILYPrecip':'','DAILYSnowfall':'','DAILYSnowDepth':'','DAILYAverageStationPressure':'','DAILYAverageSeaLevelPressure':'','DAILYAverageWindSpeed':'','DAILYPeakWindSpeed':'','PeakWindDirection':'','DAILYSustainedWindSpeed':'','DAILYSustainedWindDirection':'','MonthlyMaximumTemp':'','MonthlyMinimumTemp':'','MonthlyMeanTemp':'','MonthlyAverageRH':'','MonthlyDewpointTemp':'','MonthlyWetBulbTemp':'','MonthlyAvgHeatingDegreeDays':'','MonthlyAvgCoolingDegreeDays':'','MonthlyStationPressure':'','MonthlySeaLevelPressure':'','MonthlyAverageWindSpeed':'','MonthlyTotalSnowfall':'','MonthlyDeptFromNormalMaximumTemp':'','MonthlyDeptFromNormalMinimumTemp':'','MonthlyDeptFromNormalAverageTemp':'','MonthlyDeptFromNormalPrecip':'','MonthlyTotalLiquidPrecip':'','MonthlyGreatestPrecip':'','MonthlyGreatestPrecipDate':'','MonthlyGreatestSnowfall':'','MonthlyGreatestSnowfallDate':'','MonthlyGreatestSnowDepth':'','MonthlyGreatestSnowDepthDate':'','MonthlyDaysWithGT90Temp':'','MonthlyDaysWithLT32Temp':'','MonthlyDaysWithGT32Temp':'','MonthlyDaysWithLT0Temp':'','MonthlyDaysWithGT001Precip':'','MonthlyDaysWithGT010Precip':'','MonthlyDaysWithGT1Snow':'','MonthlyMaxSeaLevelPressureValue':'','MonthlyMaxSeaLevelPressureDate':'-9999','MonthlyMaxSeaLevelPressureTime':'-9999','MonthlyMinSeaLevelPressureValue':'','MonthlyMinSeaLevelPressureDate':'-9999','MonthlyMinSeaLevelPressureTime':'-9999','MonthlyTotalHeatingDegreeDays':'','MonthlyTotalCoolingDegreeDays':'','MonthlyDeptFromNormalHeatingDD':'','MonthlyDeptFromNormalCoolingDD':'','MonthlyTotalSeasonToDateHeatingDD':'','MonthlyTotalSeasonToDateCoolingDD':''},{'STATION':'WBAN:93727','STATION_NAME':'NEW RIVER MCAF NC US','ELEVATION':'7.9','LATITUDE':'34.70842','LONGITUDE':'-77.43966','DATE':'2017-08-11 01:56','REPORTTPYE':'FM-15','HOURLYSKYCONDITIONS':'OVC:08 100','HOURLYVISIBILITY':'10.00','HOURLYPRSENTWEATHERTYPE':'','HOURLYDRYBULBTEMPF':'77','HOURLYDRYBULBTEMPC':'25.0','HOURLYWETBULBTEMPF':'77','HOURLYWETBULBTEMPC':'25.0','HOURLYDewPointTempF':'77','HOURLYDewPointTempC':'25.0','HOURLYRelativeHumidity':'100','HOURLYWindSpeed':'0','HOURLYWindDirection':'000','HOURLYWindGustSpeed':'','HOURLYStationPressure':'30.11','HOURLYPressureTendency':'','HOURLYPressureChange':'','HOURLYSeaLevelPressure':'30.14','HOURLYPrecip':'0.00','HOURLYAltimeterSetting':'30.13','DAILYMaximumDryBulbTemp':'','DAILYMinimumDryBulbTemp':'','DAILYAverageDryBulbTemp':'','DAILYDeptFromNormalAverageTemp':'','DAILYAverageRelativeHumidity':'','DAILYAverageDewPointTemp':'','DAILYAverageWetBulbTemp':'','DAILYHeatingDegreeDays':'','DAILYCoolingDegreeDays':'','DAILYSunrise':'0527','DAILYSunset':'1902','DAILYWeather':'','DAILYPrecip':'','DAILYSnowfall':'','DAILYSnowDepth':'','DAILYAverageStationPressure':'','DAILYAverageSeaLevelPressure':'','DAILYAverageWindSpeed':'','DAILYPeakWindSpeed':'','PeakWindDirection':'','DAILYSustainedWindSpeed':'','DAILYSustainedWindDirection':'','MonthlyMaximumTemp':'','MonthlyMinimumTemp':'','MonthlyMeanTemp':'','MonthlyAverageRH':'','MonthlyDewpointTemp':'','MonthlyWetBulbTemp':'','MonthlyAvgHeatingDegreeDays':'','MonthlyAvgCoolingDegreeDays':'','MonthlyStationPressure':'','MonthlySeaLevelPressure':'','MonthlyAverageWindSpeed':'','MonthlyTotalSnowfall':'','MonthlyDeptFromNormalMaximumTemp':'','MonthlyDeptFromNormalMinimumTemp':'','MonthlyDeptFromNormalAverageTemp':'','MonthlyDeptFromNormalPrecip':'','MonthlyTotalLiquidPrecip':'','MonthlyGreatestPrecip':'','MonthlyGreatestPrecipDate':'','MonthlyGreatestSnowfall':'','MonthlyGreatestSnowfallDate':'','MonthlyGreatestSnowDepth':'','MonthlyGreatestSnowDepthDate':'','MonthlyDaysWithGT90Temp':'','MonthlyDaysWithLT32Temp':'','MonthlyDaysWithGT32Temp':'','MonthlyDaysWithLT0Temp':'','MonthlyDaysWithGT001Precip':'','MonthlyDaysWithGT010Precip':'','MonthlyDaysWithGT1Snow':'','MonthlyMaxSeaLevelPressureValue':'','MonthlyMaxSeaLevelPressureDate':'-9999','MonthlyMaxSeaLevelPressureTime':'-9999','MonthlyMinSeaLevelPressureValue':'','MonthlyMinSeaLevelPressureDate':'-9999','MonthlyMinSeaLevelPressureTime':'-9999','MonthlyTotalHeatingDegreeDays':'','MonthlyTotalCoolingDegreeDays':'','MonthlyDeptFromNormalHeatingDD':'','MonthlyDeptFromNormalCoolingDD':'','MonthlyTotalSeasonToDateHeatingDD':'','MonthlyTotalSeasonToDateCoolingDD':''}]";
        public TempJsonProcessorTest()
        {
            _temperatureDataReaderMock = new Mock<ITemperatureDataReader>();
            _temperatureDataReaderMock.Setup(temperatureMock => temperatureMock.GetTemperatureData()).Returns(sampleTemperatureReadings);
        }

        [TestMethod]
        public void LoadTempReadingReturnsIEnumerable()
        {
            TempJsonProcessor testProcessor = new TempJsonProcessor(_temperatureDataReaderMock.Object);
            Assert.IsInstanceOfType(testProcessor.TempReadings, typeof(IEnumerable<TempReading>));
        }

        [TestMethod]
        public void LoadTempReadingReturnsSortedIEnumerable()
        {
            TempJsonProcessor testProcessor = new TempJsonProcessor(_temperatureDataReaderMock.Object);
            Assert.AreEqual(testProcessor.TempReadings.FirstOrDefault().readingDateTime, new DateTime(2017,8,11,0,56,0));
        }
    }
}
