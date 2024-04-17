using Dapper;
using HandT_Test_PG.DbContext;
using HandT_Test_PG.DomainEntities;
using HandT_Test_PG.DomainInterface;

namespace HandT_Test_PG.DomainRepository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly DapperContext _context;

        public RoleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AspnetRole>> GetAllRoles()
        {
            var query = "SELECT * FROM public.\"AspNetRoles\"";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<AspnetRole>(query);
                return result.ToList();
            }
        }
    }
}
