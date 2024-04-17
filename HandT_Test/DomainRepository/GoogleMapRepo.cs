using Dapper;
using HandT_Api_Layer.DomainEntities;
using HandT_Api_Layer.DomainInterface;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DbContext;
using System.Data;

namespace HandT_Api_Layer.DomainRepository
{
    public class GoogleMapRepo : IGoogleMapRepo
    {
        private readonly DapperContext _context;
        public GoogleMapRepo(DapperContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> DisplayMap()
        {
            using (var connection = _context.CreateConnection())
            {
                return null;
            }
        }
    }
}