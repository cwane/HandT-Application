using HandT_Api_Layer.DomainEntities;
using HandT_Api_Layer.DomainEntities.Front;
using HandT_Test_PG.DomainEntities;

namespace HandT_Api_Layer.DomainInterface
{
    public interface IFrontRepo
    {
        public Task<IEnumerable<FeaturedEvent>> getFeaturedEvents();
        public Task<FrontEventDetail> GetFrontEventDetails(int event_id);        
    }
}
