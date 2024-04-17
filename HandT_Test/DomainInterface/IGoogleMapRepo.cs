using HandT_Api_Layer.DomainEntities;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HandT_Api_Layer.DomainInterface
{
    public interface IGoogleMapRepo
    {
        public Task<ApiResponse> DisplayMap();

    }
}