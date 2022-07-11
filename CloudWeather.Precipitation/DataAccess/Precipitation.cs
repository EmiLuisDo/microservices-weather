

namespace CloudWeather.Precipitation.DataAccess {
    public class Precipitation{
        public Guid id {get; set;}
        public DateTime CreateOn {get; set;}
        public decimal AmountInches {get; set;}
        public string WeatherType {get; set;}
        public string ZipCode {get; set;}
    }
}