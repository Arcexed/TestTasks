using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestTasks.WeatherFromAPI.Models
{
    class City
    {
        public City(string name)
        {
            this._name = name;
        }
        private string _name { get; set; }
        private Coordinates _coordinate { get; set; } = new Coordinates();
        public List<Day> Days { get; set; } = new List<Day>();
        public async Task GetCoordinates()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var request = await client
                        .GetAsync($"https://api.openweathermap.org/geo/1.0/direct?q={_name}&appid={WeatherManager.apiKey}");
                    if (request.IsSuccessStatusCode)
                    {
                        string response = request.Content.ReadAsStringAsync().Result;
                        dynamic json = JArray.Parse(response).Last;
                        if (json.ToString() != null && json.ToString().Contains("lat") && json.ToString().Contains("lon"))
                        {

                            this._coordinate.Latitude = json.lat;
                            this._coordinate.Longitude = json.lon;
                            return;

                        }
                        else
                        {
                            throw new ArgumentException("City doesn't exists");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task GetDays(int count)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var request = await client
                        .GetAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={_coordinate.Latitude}&lon={_coordinate.Longitude}&appid={WeatherManager.apiKey}");
                    if (request.IsSuccessStatusCode)
                    {
                        string response = request.Content.ReadAsStringAsync().Result;
                        if (response != "")
                        {
                            dynamic json = JObject.Parse(response);
                            dynamic elements = json.daily;
                            int i = 0;
                            foreach (dynamic element in elements)
                            {
                                if (i < count)
                                {
                                    int dt = element.dt;
                                    double avgTemp = element.temp.day;
                                    double? rain = element.rain;
                                    Days.Add(
                                        new Day()
                                        {
                                            date = StaticFunctions.UnixTimeStampToDateTime(dt),
                                            AvgTemperature = avgTemp,
                                            Rain = rain,
                                        }
                                    );
                                    i++;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            throw new ArgumentException("City doesn't exists");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    class Day
    {
        public DateTime date { get; set; }
        public double AvgTemperature { get; set; }
        public double? Rain { get; set; }
    }
    class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
