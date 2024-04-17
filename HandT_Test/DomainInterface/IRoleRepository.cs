using HandT_Test_PG.DomainEntities;

namespace HandT_Test_PG.DomainInterface
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<AspnetRole>> GetAllRoles();
    }
}
