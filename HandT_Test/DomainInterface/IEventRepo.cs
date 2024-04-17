using HandT_Api_Layer.DomainEntities;
using HandT_Test_Mysql.DomainEntities;
using HandT_Test_PG.DomainEntities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HandT_Test_Mysql.DomainInterface
{
    public interface IEventRepo
    {
        public Task<IEnumerable<Events>> getEvents(int eventid);
        public Task<ApiResponse> AddEvents(Events events);
        public Task<JsonResponse> GetEventOut(Events events);
        public Task<JsonResponse> GetEventNoOut(Events events);
        public Task<ApiResponse> BookEvent(EventParticipant eventParticipant);
        public Task<ApiResponse> AddEventPhotos(int event_id,List<EventPhoto> eventPhotos);
        public Task<ApiResponse> AddEventDays(int event_id,List<EventDay> eventDays);
        public Task<ApiResponse> AddBookmarks(string userid, int eventid);
        public Task<IEnumerable<FeaturedEvent>> BookmarkedEvents(string userid);


    }
}
