using HandT_Api_Layer.DomainEntities.Front;
using HandT_Test_Mysql.DomainEntities;

namespace HandT_Test_Mysql.DomainInterface
{
    public interface IProfileRepo
    {
        public Task<ApiResponse> UpdateProfile(Profile profile);
        public Task<ApiResponse> UserConnection(UserConnection userConnection);
        public Task<ApiResponse> PostRating(UserRating userRating);
        public Task<UserProfileDetail> GetUserProfile(string Username);
        public Task<ProfileParamCnt> GetProfileParamCount(string Username);
        public Task<UserDashboard> GetUserDashboard(string Username);
    }
}
