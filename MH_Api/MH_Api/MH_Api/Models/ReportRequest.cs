using System.ComponentModel.DataAnnotations;

namespace MH_Api.Models
{
    public class ReportRequest
    {

        public int ReportId { get; set; }
        public int PlayerKey { get; set; }
        public int TeamKey { get; set; }
        public int ScoutKey { get; set; }
        public string? Comments { get; set; }
        public string? HighlightLink { get; set; }

        public int? DefenseRating { get; set; }
        public int? ReboundRating { get; set; }
        public int? ShootingRating { get; set; }
        public int? AssistRating { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
