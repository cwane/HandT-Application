using HandT_Test_PG.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandT_Test_PG.DomainEntities
{
    public class Events
    {
        public int Id { get; set; } 
        public string? user_id { get; set; }
        public string? event_title { get; set; }
        public string? event_desc { get; set; }
        public string? category_id { get; set; }
        public string? organisation_name { get; set; }
        public string? destination_location { get; set; }
        public string? pickup_location { get; set; }
        public string? cover_image { get; set; }
        public string? organisation_link { get; set; }
        public string? is_single_recurring { get; set; }
        public string? no_of_people { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string? start_time { get; set; }
        public string? end_time { get; set; }
        public string? email { get; set; }
        public string? contact_no { get; set; }
        public string? location { get; set; }
        public string? life_insurance { get; set; }
        public string? payment_option { get; set; }
        public string? paid_free { get; set; }
        public DateTime publish_datetime { get; set; }
        public string? is_draft { get; set; }
        public decimal cost_per_person { get; set; }
        public string? entered_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
}
