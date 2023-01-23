namespace MH_Api.Models
{
    public class LeagueRequest
    {
        public int LeagueKey { get; set; }
        public string? LeagueName { get; set; }
        public string? Country { get; set; }
        public int? ActiveSource { get; set; }
        public int? LeagueGroupKey { get; set; }
        public int? LeagueCustomGroupKey { get; set; }
        public bool SearchDisplayFlag { get; set; }
    }
}
