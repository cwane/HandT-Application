using HandT_Test.Authentication;
using HandT_Test_Mysql.DomainEntities;

namespace HandT_Test_Mysql.DomainInterface
{
    public interface ICommonRepo
    {
        Task<ApiResponse> GetTokenDetail(string AccessToken);
    }
}
