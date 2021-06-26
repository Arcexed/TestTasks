using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestTasks.WeatherFromAPI.Models;

namespace TestTasks.WeatherFromAPI
{
    internal class WeatherManager
    {
        public static string apiKey { get; } = "5edbb425f07dd27adf69370b35414d30";
        
        public async Task<WeatherComparisonResult> CompareWeather(string CityA, string CityB, int dayCount)
        {
            WeatherComparisonResult result = new WeatherComparisonResult();
            if (dayCount<=5)
            {
                result.CityA = new City(CityA);
                result.CityB = new City(CityB);
                await result.CityA.GetCoordinates();
                await result.CityB.GetCoordinates();
                await result.CityA.GetDays(dayCount);
                await result.CityB.GetDays(dayCount);

                for (int i = 0; i < dayCount; i++)
                {
                    var CityA_day = result.CityA.Days[i];
                    var CityB_day = result.CityB.Days[i];

                    #region NullableAction
                    if (!CityA_day.Rain.HasValue)
                    {
                        CityA_day.Rain = 0;
                    }

                    if (!CityB_day.Rain.HasValue)
                    {
                        CityB_day.Rain = 0;
                    }
                    #endregion

                    if (CityA_day.Rain.Value > CityB_day.Rain.Value)
                    {
                        result.RainierDaysCount++;
                    }
                    
                    if (CityA_day.AvgTemperature > CityB_day.AvgTemperature)
                    {
                        result.WarmerDaysCount++;
                    }
                }
            }
            else
            {
                throw new ArgumentException("CountDays must be inclusively less 5 days");
            }

            return result;
        }
    }

    
}
