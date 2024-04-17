namespace HandT_Test_Mysql.DomainEntities
{
    public class UserRating
    {
        public string? user_id { get; set; }
        public string? rating_by { get; set; }
        public int rating_value { get; set; }
        public string? rating_comment { get; set;}
    }
}
