namespace HandT_Api_Layer.DomainEntities
{
    public class Bookmarking
    {
        public string? user_id { get; set; }
        public int event_id { get; set; }
        public Boolean? is_active { get; set; } = true;
    }
}
