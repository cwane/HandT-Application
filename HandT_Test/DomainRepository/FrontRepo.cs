using Dapper;
using HandT_Api_Layer.DomainEntities;
using HandT_Api_Layer.DomainEntities.Front;
using HandT_Api_Layer.DomainInterface;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DbContext;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Mvc;
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

        //public async Task<List<Events>> UpcomingEvents(int category_id)
        //{
        //    var response = new JsonResponse();
        //    using (var connection = _context.CreateConnection())
        //    {
        //        connection.Open();
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@CategoryID", category_id);
        //        //var query = "SELECT * FROM events WHERE (@CategoryID IS NULL OR category_id = @CategoryID) AND start_date >= CURDATE() ORDER BY start_date ASC";
        //        //var query = "SELECT * FROM events WHERE category_id = @CategoryID AND start_date >= CURDATE() ORDER BY start_date ASC";
        //        string query;

        //        if (category_id == 0)
        //        {
        //            query = "SELECT * FROM events WHERE  start_date >= CURDATE() ORDER BY start_date ASC";
        //        }
        //        else
        //        {
        //            query = "SELECT * FROM events WHERE category_id = @CategoryID AND  start_date >= CURDATE() ORDER BY start_date ASC";
        //        }

        //        var events = await connection.QueryAsync<Events>(query, parameters);
        //        return events.ToList();
        //    }
        //}


        public async Task<IEnumerable<FeaturedEvent>> UpcomingEvents(int category_id)
         {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@input_category_id", category_id, DbType.Int32);
                var data = await connection.QueryAsync<FeaturedEvent>("HandT_Mysql.SP_GET_UPCOMING_EVENTS", parameters, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }


        public async Task<List<NearbyEvent>> NearbyEvents(decimal latitude, decimal longitude)
        {
            var response = new JsonResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@latitude", latitude);
                parameters.Add("@longitude", longitude);
                //var query = "SELECT * FROM nearbyevent WHERE latitude=@latitude";
                //var query = "SELECT id, (3959*acos(cos(radians(37))*cos(radians(@latitude))*cos(radians(@longitude)-radians(-122))+sin(radians(37))*sin(radians(@latitude)))) AS distance FROM nearbyevent HAVING distance <25 ORDER BY distance LIMIT 0,20";
                //var query = "SELECT *,(((acos(sin((7.085941*pi()/180))*sin((@latitude*pi()/180))+cos((7.085941*pi()/180))*cos((@latitude*pi()/180))*cos(((286.9522533-@longitude)*pi()/180))))*180/pi())*60*1.1515*1.609344) as distance FROM nearbyevent WHERE (((acos(sin((7.085941*pi()/180))*sin((@latitude*pi()/180))+cos((7.085941*pi()/180))*cos((@latitude*pi()/180))*cos(((286.9522533-@longitude)*pi()/180))))*180/pi())*60*1.1515*1.609344) <= 140.85 LIMIT 15";
                //var query = "SELECT *,(((acos(sin((@latitude*pi()/180))*sin((latitude*pi()/180))+cos((@latitude*pi()/180))*cos((latitude*pi()/180))*cos(((@longitude-longitude)*pi()/180))))*180/pi())*60*1.1515*1.609344) as distance FROM nearbyevent ORDER BY distance LIMIT 6";

               var query = "SELECT *,(6371 * acos(cos(radians(@latitude)) * cos(radians(latitude)) * cos(radians(longitude) - radians(@longitude)) + sin(radians(@latitude)) * sin(radians(latitude)))) AS distance FROM nearbyevent ORDER BY distance LIMIT 10";


                var events = await connection.QueryAsync<NearbyEvent>(query, parameters);
                return events.ToList();
            }
        }

        //public async Task<IEnumerable<FeaturedEvent>> filterEvents(int category_id, string location, decimal price1,decimal price2, string tags,string text,DateTime date1, DateTime date2)
        public async Task<IEnumerable<FeaturedEvent>> filterEvents(int? category_id = null, string? location = null,decimal? price1 = null,decimal? price2 = null,string? tags = null,string? text = null,DateTime? date1 = null,DateTime? date2 = null)

        {
            var response = new JsonResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@CategoryID", category_id );
                parameters.Add("@Location", location);
                parameters.Add("@Price1", price1);
                parameters.Add("@Price2", price2);
                parameters.Add("@Tags", tags);
                parameters.Add("@Text", text);
                parameters.Add("@Date1", date1);
                parameters.Add("@Date2", date2);

                string whereClause = "WHERE 1=1";

                if (category_id.HasValue && category_id.Value > 0)
                {
                    parameters.Add("@CategoryID", category_id);
                    whereClause += " AND category_id = @CategoryID";
                }

                if (!string.IsNullOrEmpty(location))
                {
                    parameters.Add("@Location", location);
                    whereClause += " AND destination_location = @Location";
                }

                if (price1.HasValue)
                {
                    parameters.Add("@Price1", price1);
                    whereClause += " AND cost_per_person >= @Price1";
                }

                if (price2.HasValue)
                {
                    parameters.Add("@Price2", price2);
                    whereClause += " AND cost_per_person <= @Price2";
                }

                if (!string.IsNullOrEmpty(tags))
                {
                    parameters.Add("@Tags", tags);
                    whereClause += " AND event_tag = @Tags";
                }

                if (!string.IsNullOrEmpty(text))
                {
                    parameters.Add("@Text", text);
                    whereClause += " AND event_title = @Text";
                }

                if (date1.HasValue)
                {
                    parameters.Add("@Date1", date1);
                    whereClause += " AND start_date >= @Date1";
                }

                if (date2.HasValue)
                {
                    parameters.Add("@Date2", date2);
                    whereClause += " AND start_date <= @Date2";
                }

                //string query = $"SELECT * FROM events {whereClause} ORDER BY start_date ASC";
                string query = $"SELECT event_title, event_desc, destination_location, event_tag, " +
               "DATEDIFF(end_date, start_date) AS days, " +
               "DATE_FORMAT(start_date, '%M %d %Y') AS starting_date, " +
               "paid_free, cost_per_person " +
               $"FROM events {whereClause} ORDER BY start_date ASC";

                var events = await connection.QueryAsync<FeaturedEvent>(query, parameters);
                return events.ToList();
            }
        }



    }
}
