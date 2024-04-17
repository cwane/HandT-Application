namespace HandT_Api_Layer.DomainEntities
{
    public class EventCreateRequest
    {
        public string Event_Title { get; set; } = string.Empty;
        public string Event_Category { get; set; } = string.Empty;
        public string Event_Description { get; set; } = string.Empty;
        public DateOnly Event_Start_Date { get; set; }
        public DateOnly Event_End_Date { get; set; }
        public TimeOnly Event_Start_Time { get; set; }
        public TimeOnly Event_End_Time { get; set; }
        public string Event_Type { get; set; } = string.Empty;
        public string Pickup { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public int Ticket_Quantity { get; set; } = 0;
        public DateOnly Sale_Start_Date { get; set; }
        public DateOnly Sale_End_Date { get; set; }
        public TimeOnly Sale_Start_Time { get; set; }
        public TimeOnly Sale_End_Time { get; set; }
        public float Price_Per_Pers { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = false;
        public DateOnly? Published_Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Organizer { get; set; } = string.Empty;
    }
}
