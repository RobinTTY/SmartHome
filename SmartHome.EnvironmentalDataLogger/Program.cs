using System;
using System.Device.I2c;
using System.Threading.Tasks;
using Bme680Driver;
using MongoDB.Driver;
using Serilog;
using SmartHome.Models;

namespace SmartHome.EnvironmentalDataLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Database
            const string connector = "mongodb://admin:local@192.168.178.23:27017/?authSource=admin&readPreference=primary&appname=EnvironmentalDataLogger&ssl=false";
            var mongoClient = new MongoClient(connector);
            var smartHomeDb = mongoClient.GetDatabase("SmartHome");
            var environmentalDataCollection = smartHomeDb.GetCollection<EnvironmentalData>("EnvironmentalData");

            // logging
            using var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/EnvironmentalDataLogger.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var settings = new I2cConnectionSettings(1, Bme680.SecondaryI2cAddress);
            var i2CDevice = I2cDevice.Create(settings);
            using var bme680 = new Bme680(i2CDevice) {GasConversionIsEnabled = false, HeaterIsEnabled = false};

            while (true)
            {
                try
                {
                    var measurement = await bme680.PerformMeasurementAsync();
                    var dbData = new EnvironmentalData
                    {
                        Timestamp = DateTime.UtcNow,
                        Temperature = measurement.Temperature,
                        Pressure = measurement.Pressure,
                        Humidity = measurement.Humidity,
                        GasResistance = measurement.GasResistance
                    };
                    await environmentalDataCollection.InsertOneAsync(dbData);
                    await Task.Delay(10000);
                }
                catch (Exception exception)
                {
                    Log.Error(exception, "Exception occurred during runtime.");
                }
            }
        }
    }
}
