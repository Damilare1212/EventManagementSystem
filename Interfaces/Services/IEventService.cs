using EventApp.DTOs;
using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface IEventService
    {
        Task<BaseResponse<EventDto>> CreateEvent(CreateEventRequestModel model);
        Task<BaseResponse<EventDto>> UpdateEvent(int id, UpdateEventRequestModel model);
        Task<BaseResponse<EventDto>> GetEventById(int id);
        Task<BaseResponse<IList<EventDto>>> SearchEvent(string searchText);
        Task<BaseResponse<EventDto>> GetEventByEventDate(DateTime eventDate);
        Task<BaseResponse<EventDto>> GetEventByVenue(string venue);
        Task<BaseResponse<IList<EventDto>>> GetSelectedEvents(IList<int> ids);
        Task<BaseResponse<IList<EventDto>>> GetAll();
        Task<BaseResponse<IList<EventDto>>> GetAll(Expression<Func<Events, bool>> expression);
        Task<BaseResponse<EventDto>> DeleteEvent(int id);
        Task<BaseResponse<EventDto>> GetEventByTitle(string title);
        Task<BaseResponse<EventDto>> BookForEvent(int attendeeId, int eventId);
        Task<BaseResponse<IList<EventDto>>> GetAllCurrentEvents();
        Task<BaseResponse<IList<EventDto>>> GetAttendeeAreaOfInterestEvents(int attendeeId);
        Task<BaseResponse<IList<EventDto>>> GetAllAttendeePastEvents(int attendeeId);
        Task<BaseResponse<IList<EventDto>>> GetAllAttendeeUpComingEvents(int attendeeId);
        Task<BaseResponse<IList<EventDto>>> GetAllOrganizerPastEvents(int organizerId);
        Task<BaseResponse<IList<EventDto>>> GetAllOrganizerUpComingEvents(int organizerId);
        Task<BaseResponse<IList<EventDto>>> GetAllBookedEvents(int attendeeId);
        Task<BaseResponse<EventAttendee>> CancelEvent(int attendeeId, int eventsId);
        Task<BaseResponse<IList<EventDto>>> GetOrganizerAllEvents(int organizerId);
    }
}
