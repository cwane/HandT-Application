using Dapper;
using HandT_Api_Layer.DomainEntities.Front;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;
using HandT_Test_PG.DbContext;
using Microsoft.Extensions.Logging;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HandT_Test_Mysql.DomainRepository
{
    public class ProfileRepo : IProfileRepo
    {
        private readonly DapperContext _context;

        public ProfileRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDetail> GetUserProfile(string Username)
        {
            UserProfileDetail userProfileDetail = new UserProfileDetail();
            List<UserRating> ratingList = new List<UserRating>();

            using (var connection = _context.CreateConnection())
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_USERNAME", Username);

                var profile_detail = await connection.QueryFirstOrDefaultAsync<UserProfileDetail>("HandT_Mysql.SP_GET_USER_PROFILE", param, commandType: CommandType.StoredProcedure);
                userProfileDetail = profile_detail;

                var user_reviews = await connection.QueryAsync<UserRating>("HandT_Mysql.SP_GET_USER_REVIEWS", param, commandType: CommandType.StoredProcedure);
                ratingList = user_reviews.ToList();
                userProfileDetail.userReviews = ratingList;
            }
            return userProfileDetail;
        }

        public async Task<ApiResponse> PostRating(UserRating userRating)
        {
            ApiResponse response = new ApiResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_user_id", userRating.user_id);
                param.Add("P_rating_by", userRating.rating_by);
                param.Add("P_rating_value", userRating.rating_value);
                param.Add("P_rating_comment", (userRating.rating_comment == null || userRating.rating_comment == "" ? "" : userRating.rating_comment));
                response = await connection.QueryFirstOrDefaultAsync<ApiResponse>("HandT_Mysql.SP_POST_REVIEW", param, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<ApiResponse> UpdateProfile(Profile profile, IFormFile? pictureFile)
        {
            ApiResponse response = new ApiResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_isIndivCompany", (profile.isIndivCompany == null || profile.isIndivCompany == "" ? "" : profile.isIndivCompany));
                param.Add("P_FirstName", (profile.FirstName == null || profile.FirstName == "" ? "" : profile.FirstName));
                param.Add("P_MiddleName", (profile.MiddleName == null || profile.MiddleName == "" ? "" : profile.MiddleName));
                param.Add("P_LastName", (profile.LastName == null || profile.LastName == "" ? "" : profile.LastName));
                param.Add("P_DOB", (profile.DOB == null || profile.DOB == "" ? "" : profile.DOB));
                param.Add("P_Gender", (profile.Gender == null || profile.Gender == "" ? "" : profile.Gender));
                param.Add("P_Occupation", (profile.Occupation == null || profile.Occupation == "" ? "" : profile.Occupation));
                param.Add("P_Website", (profile.Website == null || profile.Website == "" ? "" : profile.Website));
                param.Add("P_Address", (profile.Address == null || profile.Address == "" ? "" : profile.Address));
                param.Add("P_Interest", (profile.Interest == null || profile.Interest == "" ? "" : profile.Interest));
                param.Add("P_Bio", (profile.Bio == null || profile.Bio == "" ? "" : profile.Bio));
                param.Add("P_Picture", (profile.Picture == null || profile.Picture == "" ? "" : profile.Picture));
                param.Add("P_Country", (profile.Country == null || profile.Country == "" ? "" : profile.Country));
                param.Add("P_Province", (profile.Province == null || profile.Province == "" ? "" : profile.Province));
                param.Add("P_City", (profile.City == null || profile.City == "" ? "" : profile.City));
                param.Add("P_Street", (profile.CitizenshipNo == null || profile.CitizenshipNo == "" ? "" : profile.CitizenshipNo));
                param.Add("P_UserName", profile.UserName);


                if (pictureFile != null && pictureFile.Length > 0)
                {
                    var fileName = profile.UserName + Path.GetExtension(pictureFile.FileName);
                    var filePath = Path.Combine("wwwroot", "Upload", fileName);

                    if (!string.IsNullOrEmpty(profile.Picture))
                    {
                        var existingFilePath = Path.Combine("wwwroot", "Upload", profile.Picture.TrimStart('/'));
                        if (File.Exists(existingFilePath))
                        {
                            File.Delete(existingFilePath);
                        }
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await pictureFile.CopyToAsync(stream);
                    }

                    param.Add("P_Picture",fileName);
                }



                response = await connection.QueryFirstOrDefaultAsync<ApiResponse>(
                                                                     "HandT_Mysql.SP_UPDATE_PROFILE", param, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<ApiResponse> UserConnection(UserConnection userConnection)
        {
            ApiResponse response = new ApiResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_FOLLOWED_BY", userConnection.followed_by);
                param.Add("P_FOLLOWED_TO", userConnection.followed_to);
                param.Add("P_CONNECT_DATE", userConnection.connect_date);
                response = await connection.QueryFirstOrDefaultAsync<ApiResponse>("HandT_Mysql.SP_ADD_CONNECTION", param, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<ProfileParamCnt> GetProfileParamCount(string Username)
        {
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_USERNAME", Username);
                var data = await connection.QueryFirstOrDefaultAsync<ProfileParamCnt>("HandT_Mysql.SP_GET_PROFILE_PARAM_COUNT", param, commandType: CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<UserDashboard> GetUserDashboard(string Username)
        {
            UserDashboard userDashboard = new UserDashboard();
            List<UserEvent> myEvents = new List<UserEvent>();
            List<UserEvent> participatedEvents = new List<UserEvent>();

            using (var connection = _context.CreateConnection())
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_USERNAME", Username);

                userDashboard.UserProfileDetail = await connection.QueryFirstOrDefaultAsync<UserProfileDetail>("HandT_Mysql.SP_GET_USER_PROFILE", param, commandType: CommandType.StoredProcedure);
                userDashboard.ProfileParamCnt = await connection.QueryFirstOrDefaultAsync<ProfileParamCnt>("HandT_Mysql.SP_GET_PROFILE_PARAM_COUNT", param, commandType: CommandType.StoredProcedure);

                var resultMyEvents = await connection.QueryAsync<UserEvent>("HandT_Mysql.SP_GET_MY_EVENTS", param, commandType: CommandType.StoredProcedure);
                myEvents = resultMyEvents.ToList();
                userDashboard.myEvents = myEvents;

                var resultParticipatedEvents = await connection.QueryAsync<UserEvent>("HandT_Mysql.SP_GET_PARTICIPATED_EVENTS", param, commandType: CommandType.StoredProcedure);
                participatedEvents = resultParticipatedEvents.ToList();
                userDashboard.participatedEvents = participatedEvents;
            }
            return userDashboard;
        }
    }
}
