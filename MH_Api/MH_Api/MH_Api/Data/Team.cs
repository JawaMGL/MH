using System.ComponentModel.DataAnnotations;

namespace MH_Api.Data
{
    public class Team
    {
        [Key]
        public int TeamKey { get; set; }
        public int? LeagueKey { get; set; }
        public int? LeagueKey_Domestic { get; set; }
        public int? ArenaKey { get; set; }
        public string TeamName { get; set; }
        public string? TeamNickname { get; set; }
        public string? Conference { get; set; }
        public string? SubConference { get; set; }
        public string? TeamCountry { get; set; }
        public string? CoachName { get; set; }
        public string? URLPhoto { get; set; }
        public bool? CurrentNBATeamFlg { get; set; }

    }
}
