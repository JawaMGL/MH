namespace MH_Api.Models
{
    public class ScoutRequest
    {
        public int ScoutKey { get; set; }
        public string ScoutFirstName { get; set; }
        public string ScoutLastName { get; set; }
        public string ScoutPhoneNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
