namespace TestTasks.WeatherFromAPI.Models
{
    class WeatherComparisonResult
    {
        public City CityA { get; set; }

        public City CityB { get; set; }

        public int WarmerDaysCount { get; set; } = 0;

        public int RainierDaysCount { get; set; } = 0;

    }
}
