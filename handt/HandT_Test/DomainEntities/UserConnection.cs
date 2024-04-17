namespace HandT_Test_Mysql.DomainEntities
{
    public class UserConnection
    {
        public string? followed_by { get; set; }
        public string? followed_to { get; set; }
        public DateTime connect_date { get; set; }
    }
}
