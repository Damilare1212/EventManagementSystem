using EventApp.DTOs;
using EventApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EventApp.DTOs.TicketDto;

namespace EventApp.Interfaces.Services
{
    public interface ITicketService
    {
        Task<BaseResponse<TicketDto>> CreateTicket(CreateTicketRequestModel model);
        Task<BaseResponse<TicketDto>> UpdateTicket( int id, UpdateTicketRequestModel model);
        Task<BaseResponse<TicketDto>> GetTicketById(int id);
        Task<BaseResponse<TicketDto>> GetTicketByEventId(int EventId);
        Task<BaseResponse<TicketDto>> GetTicketByTicketType(EventType ticketType);
        Task<BaseResponse<TicketDto>> GetTicketByTicketNumber(string ticketNumber);
        Task<BaseResponse<IList<TicketDto>>> GetSelectedTicket(IList<int> ids);
        Task<BaseResponse<TicketDto>> DeleteTicket(int id);
        Task<BaseResponse<IList<TicketDto>>> GetAll();
    }
}
