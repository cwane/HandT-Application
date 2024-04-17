using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;

namespace HandT_Test_Mysql.DomainRepository
{
    public class CategoryRepo : ICategoryRepo
    {
        public Task<ApiResponse> AddCategory(Category category)
        {
            return null;
        }
    }
}
