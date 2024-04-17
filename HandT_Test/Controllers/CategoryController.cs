using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;
using HandT_Test_Mysql.DomainRepository;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HandT_Test_Mysql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepo _categoryRepo;


        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpPost]
        [Route("Add-Category")]
        public async Task<IActionResult> AddCategory(Category category)
        {

            try
            {
                var resp = await _categoryRepo.AddCategory(category);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
