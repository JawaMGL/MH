using System.ComponentModel.DataAnnotations;

namespace MH_Api.Data
{
    public class Player
    {
        [Key]
        public int PlayerKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? PositionKey { get; set; }
        public int? AgentKey { get; set; }
        public int? FreeAgentYear { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public int? YearsOfService { get; set; }
        public decimal? Wing { get; set; }
        public decimal? BodyFat { get; set; }
        public decimal? StandingReach { get; set; }
        public decimal? CourtRunTime_3_4 { get; set; }
        public decimal? VerticalJumpNoStep { get; set; }
        public decimal? VerticalJumpMax { get; set; }
        public decimal? HandWidth { get; set; }
        public decimal? HandLength { get; set; }
        public string? URLPhoto { get; set; }
        public bool ActiveAnalysisFlg { get; set; }
        public int? LeagueCustomGroupKey { get; set; }
        public int? BboPlayerKey { get; set; }
        public DateTime dwh_insert_datetime { get; set; }
        public DateTime? dwh_update_datetime { get; set; }
        public string? AgentName { get; set; }
        public string? AgentPhone { get; set; }
        public string? CommittedTo { get; set; }
        public string? Handedness { get; set; }
        public int? GLPlayerKey { get; set; }
        public int? PlayerStatusKey { get; set; }
        public string? Height_Source { get; set; }
        public string? Weight_Source { get; set; }
        public string? Wing_Source { get; set; }
        public string? BodyFat_Source { get; set; }
        public string? StandingReach_Source { get; set; }
        public string? CourtRunTime_3_4_Source { get; set; }
        public string? VerticalJumpNoStep_Source { get; set; }
        public string? VerticalJumpMax_Source { get; set; }
        public string? Hand_W_H_Source { get; set; }
        public string? Hand { get; set; }
        public bool IsCustomData { get; set; }
        public string? Handedness_Source { get; set; }
    }
}
