using HandT_Test_Mysql.DomainEntities;

namespace HandT_Api_Layer.DomainEntities.Front
{
    public class ProfileParamCnt
    {
        public int Cnt_Event_Created { get; set; }
        public int Cnt_Event_Participated { get; set; }
        public int Cnt_Followers { get; set; }
        public int Cnt_Following { get; set; }
        public decimal Overall_rating { get; set; }
    }

    public class UserEvent
    {
        public string? event_title { get; set; }
        public string? event_desc { get; set; }
        public string? destination_location { get; set; }
        public string? days { get; set; }
        public string? paid_free { get; set; }
        public string? cost_per_person { get; set; }
        public string? starting_date { get; set; }
    }

    public class UserProfileDetail
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? Bio { get; set; }
        public string? Interest { get; set; }
        public string? Picture { get;set; }

        private List<UserRating> _userReviews = new List<UserRating>();
        public List<UserRating> userReviews
        {
            get { return _userReviews; }
            set { _userReviews = value; }
        }
    }

    public class UserDashboard
    {
        private UserProfileDetail _userProfileDetail = new UserProfileDetail();
        public UserProfileDetail UserProfileDetail
        {
            get { return _userProfileDetail; }
            set { _userProfileDetail = value; }
        }

        private ProfileParamCnt _profileParamCnt = new ProfileParamCnt();
        public ProfileParamCnt ProfileParamCnt
        {
            get { return _profileParamCnt; }
            set { _profileParamCnt = value; }
        }

        //my events
        private List<UserEvent> _myEvents = new List<UserEvent>();
        public List<UserEvent> myEvents
        {
            get { return _myEvents; }
            set { _myEvents = value; }
        }

        //participated events
        private List<UserEvent> _participatedEvents = new List<UserEvent>();
        public List<UserEvent> participatedEvents
        {
            get { return _participatedEvents; }
            set { _participatedEvents = value; }
        }
    }

}
