using HandT_Test_Mysql.DomainEntities;

namespace HandT_Api_Layer.DomainEntities.Front
{
    public class FrontEventDetail
    {
        public string? event_title { get; set; }
        public string? event_desc { get; set; }
        public string? destination_location { get; set; }
        public string? days { get; set; }
        public string? paid_free { get; set; }
        public string? cost_per_person { get; set; }
        public string? starting_date { get; set; }

        private List<EventPhoto> _photos = new List<EventPhoto>();
        public List<EventPhoto> photos
        {
            get { return _photos; }
            set { _photos = value; }
        }

        private List<EventDay> _eventDays = new List<EventDay>();
        public List<EventDay> eventDays
        {
            get { return _eventDays; }
            set { _eventDays = value; }
        }

    }
}
