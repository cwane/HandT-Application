using Dapper;
using HandT_Api_Layer.DomainEntities;
using HandT_Api_Layer.DomainEntities.Front;
using HandT_Api_Layer.DomainInterface;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DbContext;
using HandT_Test_PG.DomainEntities;
using Microsoft.Extensions.Logging;
using System.Data;

namespace HandT_Api_Layer.DomainRepository
{
    public class FrontRepo : IFrontRepo
    {
        private readonly DapperContext _context;
        public FrontRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeaturedEvent>> getFeaturedEvents()
        {
            using (var connection = _context.CreateConnection())
            {
                var data = await connection.QueryAsync<FeaturedEvent>("HandT_Mysql.SP_GET_FEATURED_EVENTS", commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }

        public async Task<FrontEventDetail> GetFrontEventDetails(int event_id)
        {
            FrontEventDetail frontEventDetail = new FrontEventDetail();
            List<EventPhoto> photos = new List<EventPhoto>();
            List<EventDay> days = new List<EventDay>();

            using (var connection = _context.CreateConnection())
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@P_EVENT_ID", event_id);
                var event_detail = await connection.QueryFirstOrDefaultAsync<FrontEventDetail>("HandT_Mysql.SP_GET_FRONT_EVENT_DETAIL", param, commandType: CommandType.StoredProcedure);
                frontEventDetail = event_detail;

                var event_photos = await connection.QueryAsync<EventPhoto>("HandT_Mysql.SP_GET_EVENT_PHOTOS", param, commandType: CommandType.StoredProcedure);
                photos = event_photos.ToList();
                frontEventDetail.photos = photos;

                var event_days = await connection.QueryAsync<EventDay>("HandT_Mysql.SP_GET_EVENT_DAYS", param, commandType: CommandType.StoredProcedure);
                days = event_days.ToList();
                frontEventDetail.eventDays = days;
            }
            return frontEventDetail;
        }
    }
}
