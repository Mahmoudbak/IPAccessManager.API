namespace IPAccessManager.API.Dtos
{
    public class TemporalBlockDto
    {
        public required string CountryCode { get; set; }
        public int DurationMinutes { get; set; }
    }
}
