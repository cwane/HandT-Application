namespace HandT_Test_Mysql.DomainEntities
{
    public class ApiResponse
    {
        public int ResponseCode { get; set; }
        public string? Msg { get; set; }
        public object? ResponseData { get; set; }
        public string? ResponseMessage { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string? UserName { get; set; }
    }
}
