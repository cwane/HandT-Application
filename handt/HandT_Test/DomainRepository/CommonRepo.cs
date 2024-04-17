using Azure.Core;
using HandT_Test.Authentication;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;
using HandT_Test_PG.Authentication;
using HandT_Test_PG.DbContext;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace HandT_Test_Mysql.DomainRepository
{
    public class CommonRepo : ICommonRepo
    {
        private readonly DapperContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonRepo(DapperContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<ApiResponse> GetTokenDetail(string AccessToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            ClaimsInfo claimsInfo = new ClaimsInfo();
            var tokeninfo = AccessToken.ToString().Split(' ');
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(tokeninfo[1]);

            string userdetails = "";
            List<string> claims = new List<string>();
            foreach (var item in jwt.Claims)
            {
                userdetails += item.Value.ToString() + ", ";
                claims.Add(item.Value.ToString());
            }

            var un = claims[0];//get username
            var user_details = await _userManager.FindByNameAsync(un);
            apiResponse.UserName = un;
            return apiResponse;
        }
    }
}
