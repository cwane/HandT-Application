namespace HandT_Api_Layer.DomainEntities
{
    public class UserPasswordChangeRequest
    {
        public string oldpassword {  get; set; } = string.Empty;
        public string newpassword { get; set; } = string.Empty;
    }
}
