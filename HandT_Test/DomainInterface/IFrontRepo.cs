using HandT_Api_Layer.DomainEntities;
using HandT_Api_Layer.DomainEntities.Front;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace HandT_Api_Layer.DomainInterface
{
    public interface IFrontRepo
    {
        public Task<IEnumerable<FeaturedEvent>> getFeaturedEvents();
        public Task<FrontEventDetail> GetFrontEventDetails(int event_id);
        //public Task<List<Events>> UpcomingEvents(int category_id);
        public Task<IEnumerable<FeaturedEvent>> UpcomingEvents(int category_id);
        public Task<List<NearbyEvent>> NearbyEvents(decimal latitude, decimal longitude);
        public Task<IEnumerable<FeaturedEvent>> filterEvents(int? category_id, string? location,decimal? price1,decimal? price2,string? tags,string? text,DateTime? date1, DateTime? date2);
        // public Task<ApiResponse> DisplayMap();
    }
}