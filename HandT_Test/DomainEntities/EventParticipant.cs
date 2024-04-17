namespace HandT_Test_Mysql.DomainEntities
{
    public class EventParticipant
    {
        public string? user_id { get; set; }
        public int event_id { get; set; }
        public int no_of_person { get; set;}
        public string? event_type { get; set; }
        public string? event_paid_type { get; set; }
        public string? event_book_date { get; set; }

    }
}
