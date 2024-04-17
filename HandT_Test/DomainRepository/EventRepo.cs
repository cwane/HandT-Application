using Azure;
using Dapper;
using HandT_Api_Layer.DomainEntities;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_Mysql.DomainInterface;
using HandT_Test_PG.DbContext;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HandT_Test_Mysql.DomainRepository
{
    public class EventRepo : IEventRepo
    {
        private readonly DapperContext _context;

        public EventRepo(DapperContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> AddEventDays(int event_id, List<EventDay> eventDays)
        {
            var response = new ApiResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();

                foreach (EventDay eventDay in eventDays)
                {
                    param.Add("P_event_id", event_id);
                    param.Add("P_day", eventDay.day);
                    param.Add("P_title", eventDay.title);
                    param.Add("P_description", eventDay.description);
                    await connection.QueryFirstOrDefaultAsync<ApiResponse>("HandT_Mysql.SP_ADD_EVENT_DAYS", param, commandType: CommandType.StoredProcedure);
                }
                response.ResponseCode = 200;
                response.Msg = "Days added sucessfully";
                return response;
            }
        }

        public async Task<ApiResponse> AddEventPhotos(int event_id, List<EventPhoto> eventPhotos)
        {
            var response = new ApiResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();

                foreach (EventPhoto eventPhoto in eventPhotos)
                {
                    param.Add("P_event_id", event_id);
                    param.Add("P_photo_name", eventPhoto.photo_name);
                    param.Add("P_photo_desc", eventPhoto.photo_desc);
                    param.Add("P_is_active", eventPhoto.is_active);
                    await connection.QueryFirstOrDefaultAsync<ApiResponse>("HandT_Mysql.SP_ADD_EVENT_PHOTO", param, commandType: CommandType.StoredProcedure);
                }
                response.ResponseCode = 200;
                response.Msg = "Photo(s) added sucessfully";
                return response;
            }
        }

        public async Task<ApiResponse> AddEvents(Events events)
        {
            var response = new ApiResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_user_id", (events.user_id == null || events.user_id == "" ? "" : events.user_id));
                param.Add("P_event_title", (events.event_title == null || events.event_title == "" ? "" : events.event_title));
                param.Add("P_event_desc", (events.event_desc == null || events.event_desc == "" ? "" : events.event_desc));
                param.Add("P_category_id", (events.category_id == null || events.category_id == "" ? "" : events.category_id));
                param.Add("P_organisation_name", (events.organisation_name == null || events.organisation_name == "" ? "" : events.organisation_name));
                param.Add("P_destination_location", (events.destination_location == null || events.destination_location == "" ? "" : events.destination_location));
                param.Add("P_pickup_location", (events.pickup_location == null || events.pickup_location == "" ? "" : events.pickup_location));
                param.Add("P_cover_image", (events.cover_image == null || events.cover_image == "" ? "" : events.cover_image));
                param.Add("P_organisation_link", (events.organisation_link == null || events.organisation_link == "" ? "" : events.organisation_link));
                param.Add("P_is_single_recurring", (events.is_single_recurring == null || events.is_single_recurring == "" ? "" : events.is_single_recurring));
                param.Add("P_no_of_people", (events.no_of_people == null || events.no_of_people == "" ? "" : events.no_of_people));
                param.Add("P_start_date", events.start_date);
                param.Add("P_end_date", events.end_date);
                param.Add("P_start_time", (events.start_time == null || events.start_time == "" ? "" : events.start_time));
                param.Add("P_end_time", (events.end_time == null || events.end_time == "" ? "" : events.end_time));
                param.Add("P_email", (events.email == null || events.email == "" ? "" : events.email));
                param.Add("P_contact_no", (events.contact_no == null || events.contact_no == "" ? "" : events.contact_no));
                param.Add("P_location", (events.location == null || events.location == "" ? "" : events.location));
                param.Add("P_life_insurance", (events.life_insurance == null || events.life_insurance == "" ? "" : events.life_insurance));
                param.Add("P_payment_option", (events.payment_option == null || events.payment_option == "" ? "" : events.payment_option));
                param.Add("P_paid_free", (events.paid_free == null || events.paid_free == "" ? "" : events.paid_free));
                param.Add("P_publish_datetime", events.publish_datetime);
                param.Add("P_is_draft", (events.is_draft == null || events.is_draft == "" ? "" : events.is_draft));
                param.Add("P_cost_per_person", events.cost_per_person);
                param.Add("P_entered_by", (events.entered_by == null || events.entered_by == "" ? "" : events.entered_by));
                param.Add("P_updated_by", (events.updated_by == null || events.updated_by == "" ? "" : events.updated_by));
                param.Add("P_updated_date", events.updated_date);
                response = await connection.QueryFirstOrDefaultAsync<ApiResponse>(
                                                                     "HandT_Mysql.SP_ADD_EVENT", param, commandType: CommandType.StoredProcedure);
                return response;
            }
        }


        //public async Task<JsonResponse> AddEvents(Events events)
        //{
        //    var resp = new JsonResponse();
        //    using (var connection = _context.CreateConnection())
        //    {
        //        connection.Open();
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("UserId", events.UserId);
        //        param.Add("EventTitle",events.EventTitle);
        //        param.Add("EventDesc",events.EventDesc);
        //        param.Add("CategoryId",events.CategoryId);
        //        param.Add("DestinationLocation",events.DestinationLocation);
        //        resp = await connection.QueryFirstOrDefaultAsync<JsonResponse>(
        //                                                             "HandT_Mysql.addEvents", param, commandType: CommandType.StoredProcedure);
        //        return resp;
        //    }
        //}

        //book event
        public async Task<ApiResponse> BookEvent(EventParticipant eventParticipant)
        {
            var response = new ApiResponse();

            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_USER_ID", eventParticipant.user_id);
                param.Add("P_EVENT_ID", eventParticipant.event_id);
                param.Add("P_NO_OF_PERSON", eventParticipant.no_of_person);
                param.Add("P_EVENT_TYPE", eventParticipant.event_type);
                param.Add("P_EVENT_PAID_TYPE", eventParticipant.event_paid_type);
                param.Add("P_EVENT_BOOK_DATE", eventParticipant.event_book_date);
                response = await connection.QueryFirstOrDefaultAsync<ApiResponse>(
                                                                     "HandT_Mysql.SP_BOOK_EVENT", param, commandType: CommandType.StoredProcedure);
                return response;
            }

        }

        public async Task<JsonResponse> GetEventNoOut(Events events)
        {
            var response = new JsonResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_UserId", events.user_id);
                param.Add("P_EventTitle", events.event_title);
                response = await connection.QueryFirstOrDefaultAsync<JsonResponse>(
                                                                     "HandT_Mysql.addNoOutParam", param, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        public async Task<JsonResponse> GetEventOut(Events events)
        {
            var response = new JsonResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("P_UserId", events.user_id);
                param.Add("P_EventTitle", events.event_title);
                param.Add("O_ResponseCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("HandT_Mysql.addCondEvent", param, commandType: CommandType.StoredProcedure);
                var o_data = param.Get<int?>("O_ResponseCode");

                //response = await connection.QueryFirstOrDefaultAsync<JsonResponse>("HandT_Mysql.addCondEvent", param, commandType: CommandType.StoredProcedure);
                response.O_ResponseCode = (int)o_data;
                return response;
            }
        }

        public async Task<IEnumerable<Events>> getEvents(int eventid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@eventid", eventid);
            var query = "SELECT * FROM events where Id=@eventid";
            using (var connection = _context.CreateConnection())
            {

                //var recs = await connection.QueryAsync<Events>("HandT_Mysql.GetEvents", commandType: CommandType.StoredProcedure);

                //var recs = await connection.QueryAsync<Events>(query);

                var recs = await connection.QueryAsync<Events>(query, parameters);
                return recs.ToList();
            }
        }
        //public async Task<IEnumerable<Events>> UpcomingEvents()
        //{
        //    var query = "SELECT * FROM events WHERE category_id==1";
        //    using (var connection = _context.CreateConnection())
        //    {

        //        var recs = await connection.QueryAsync<Events>("HandT_Mysql.GetEvents", commandType: CommandType.StoredProcedure);

        //        //var recs = await connection.QueryAsync<Events>(query);
        //        return recs.ToList();
        //    }
        //}
        public async Task<ApiResponse> AddBookmarks(string userId, int eventId)

        {
            try
            {
                using (var connection = _context.CreateConnection())
                {

                    connection.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("@userid", userId);
                    parameters.Add("@eventid", eventId);

                    string checkEventExistenceQuery = "SELECT COUNT(*) FROM events WHERE id = @eventid";
                    var eventExists = await connection.ExecuteScalarAsync<int>(checkEventExistenceQuery, parameters);

                    if (eventExists == 0)
                    {
                        return new ApiResponse
                        {
                            ResponseCode = 400,
                            Msg = "Error",
                            ResponseMessage = "Event with the specified ID does not exist"
                        };
                    }

                    string checkExistingBookmarkQuery = "SELECT COUNT(*) FROM bookmark WHERE UserId = @userid AND EventId = @eventid";
                    var existingBookmarkCount = await connection.ExecuteScalarAsync<int>(checkExistingBookmarkQuery, parameters);

                    if (existingBookmarkCount == 0)
                    {
                        string addBookmarkQuery = "INSERT INTO bookmark (UserId, EventId, IsBookmarked) VALUES (@userid, @eventid, 1)";
                        await connection.ExecuteAsync(addBookmarkQuery, parameters);
                    }
                    else
                    {
                        string toggleBookmarkQuery = "UPDATE bookmark SET IsBookmarked = 1 - IsBookmarked WHERE UserId = @userid AND EventId = @eventid";
                        await connection.ExecuteAsync(toggleBookmarkQuery, parameters);
                    }

                    return new ApiResponse
                    {
                        Msg = "Success",
                        ResponseMessage = "Event bookmarked status updated Successfully"
                    };

                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Msg = "Error",
                    ResponseMessage = "Event can't be bookmarked"
                };

            }

        }

        public async Task<IEnumerable<FeaturedEvent>> BookmarkedEvents(string userId)

        {
            var response = new JsonResponse();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@userid", userId);

                string getBookmarkedEventIds = "SELECT EventId FROM bookmark WHERE UserId = @userid AND IsBookmarked = 1";
                var bookmarkedEventIds = await connection.QueryAsync<int>(getBookmarkedEventIds, parameters);

                if (bookmarkedEventIds.Any())
                {
                    string getBookmarkedEvents = "SELECT * FROM events WHERE id IN @eventIds";
                    parameters.Add("@eventIds", bookmarkedEventIds);

                    var bookmarkedEvents = await connection.QueryAsync<FeaturedEvent>(getBookmarkedEvents, parameters);
                    return bookmarkedEvents.ToList();
                }
                else
                {
                    return new List<FeaturedEvent>();
                }
            }
        }


    }
}
