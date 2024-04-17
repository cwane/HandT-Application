using HandT_Test_Mysql.DomainEntities;

namespace HandT_Test_Mysql.DomainInterface
{
    public interface ICategoryRepo
    {
        public Task<ApiResponse> AddCategory(Category category);
    }
}
