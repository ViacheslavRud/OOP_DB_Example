namespace Database.Database
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
    
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CapitalCityId { get; set; }
    }
    
}