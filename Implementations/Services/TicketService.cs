using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Enums;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EventApp.DTOs.TicketDto;

namespace EventApp.Implementations.Services
{
    public class TicketService : ITicketService
    { 
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        public TicketService(ITicketRepository ticketRepository, IEventRepository eventRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
        }
        public async Task<BaseResponse<TicketDto>>  CreateTicket(CreateTicketRequestModel model)
        {
              var ticket = new Ticket
            {
                AttendeeId = model.AttendeeId,
                EventId = model.EventId,
                TicketNumber = Guid.NewGuid().ToString().Substring(0, 11).Replace(" - ", " ").ToUpper(),
                EventType = model.EventType
            };
            await _ticketRepository.Create(ticket);
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message =$"The new ticket is created successfully.",
                Data = new TicketDto
                {
                    AttendeeId = ticket.AttendeeId,
                    EventId = ticket.EventId,
                    TicketNumber = ticket.TicketNumber,
                    EventType = ticket.EventType,
                    Id = ticket.Id,
                    IsDeleted = ticket.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<TicketDto>> DeleteTicket(int id)
        {
            var ticket = await _ticketRepository.Get(id);
            if(ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The ticket is not found.",
                };
            }
            ticket.IsDeleted = true;
            _eventRepository.SaveChanges();
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message = $"The ticket is successfully deleted.",
            };
        }

        public async Task<BaseResponse<IList<TicketDto>>> GetSelectedTicket(IList<int> ids)
        {
            var tickets = await _ticketRepository.GetSelected(ids);
            if (tickets == null || tickets.Count == 0)
            {
                return new BaseResponse<IList<TicketDto>>
                {
                    Status = false,
                    Message = $"The tickets are not found.",
                };
            }
            
            return new BaseResponse<IList<TicketDto>>
            {
                Status = true,
                Message = $"The selected tickets are retrieved successfully.",
                Data = tickets.Select(h => new TicketDto
                {
                    AttendeeId = h.AttendeeId,
                    EventId = h.EventId,
                    TicketNumber = h.TicketNumber,
                    EventType = h.EventType,
                    Id = h.Id,
                    IsDeleted = h.IsDeleted
                }).ToList()
        };
        }

        
        public async Task<BaseResponse<TicketDto>> GetTicketByTicketNumber(string ticketNumber)
        {
            var ticket = await _ticketRepository.Get(h => h.TicketNumber == ticketNumber);
            if (ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The ticket is not found.",
                };
            }
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message = $"The ticket is retrieved successfully.",
                Data = new TicketDto
                {
                    AttendeeId = ticket.AttendeeId,
                    EventId= ticket.EventId,
                    TicketNumber = ticket.TicketNumber,
                    EventType = ticket.EventType,
                    Id = ticket.Id,
                    IsDeleted = ticket.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<TicketDto>> GetTicketByType(EventType eventType)
        {
            var ticket = await _ticketRepository.Get(b => b.EventType == eventType);
            if (ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The ticket is not found.",
                };
            }
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message = $"The ticket is retrieved successfully.",
                Data = new TicketDto
                {
                    AttendeeId = ticket.AttendeeId,
                    EventId = ticket.EventId,
                    TicketNumber = ticket.TicketNumber,
                    EventType = ticket.EventType,
                    Id = ticket.Id,
                    IsDeleted = ticket.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<TicketDto>> UpdateTicket( int id, UpdateTicketRequestModel model)
        {
            var ticket = await _ticketRepository.Get(id);
            if(ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The ticket is not found.",
                };
            }
            else
            {
                ticket.EventId = model.EventId;
                ticket.AttendeeId = model.AttendeeId;
               

                await _ticketRepository.Update(ticket);
                return new BaseResponse<TicketDto>
                {
                    Status = true,
                    Message = $"The ticket email {ticket.TicketNumber} is updated successfully.",
                    Data = new TicketDto
                    {
                       AttendeeId = ticket.AttendeeId,
                       EventId = ticket.EventId,
                       TicketNumber = ticket.TicketNumber,
                       EventType = ticket.EventType,
                       IsDeleted = ticket.IsDeleted,
                       Id = ticket.Id
                    }
                };
            }
        }

        public async Task<BaseResponse<TicketDto>> GetTicketByEventId(int eventId)
        {
            var ticket = await _ticketRepository.Get(f => f.EventId == eventId);
            if(ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The Ticket searching for is noit found.",
                };
            }
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message = $"The Ticket is retrieved successfully.",
                Data = new TicketDto
                {
                    AttendeeId = ticket.AttendeeId,
                    EventId = ticket.EventId,
                    TicketNumber = ticket.TicketNumber,
                    EventType = ticket.EventType,
                    Id = ticket.Id,
                    IsDeleted = ticket.IsDeleted,
                }
            };
        }

        public async Task<BaseResponse<TicketDto>> GetTicketByTicketType(EventType eventType)
        {
            var ticket = await _ticketRepository.Get(f => f.EventType == eventType);
            if(ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The ticket is not found.",
                };
            }
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message = $"The Ticket searching for is successfully retrieved.",
                Data = new TicketDto
                {
                    AttendeeId = ticket.AttendeeId,
                    EventId = ticket.EventId,
                    TicketNumber = ticket.TicketNumber,
                    EventType = ticket.EventType,
                    IsDeleted = ticket.IsDeleted,
                    Id = ticket.Id
                }
            };
        }

        public async Task<BaseResponse<TicketDto>> GetTicketById(int id)
        {
            var ticket = await _ticketRepository.Get(id);
            if (ticket == null)
            {
                return new BaseResponse<TicketDto>
                {
                    Status = false,
                    Message = $"The ticket is not found.",
                };
            }
            return new BaseResponse<TicketDto>
            {
                Status = true,
                Message = $"The Ticket searching for is successfully retrieved.",
                Data = new TicketDto
                {
                    AttendeeId = ticket.AttendeeId,
                    EventId = ticket.EventId,
                    TicketNumber = ticket.TicketNumber,
                    EventType = ticket.EventType,
                    IsDeleted = ticket.IsDeleted,
                    Id = ticket.Id
                }
            };
        }

        public async Task<BaseResponse<IList<TicketDto>>> GetAll()
        {
            var tickets = await _ticketRepository.GetAll();
            if(tickets == null || tickets.Count == 0)
            {
                return new BaseResponse<IList<TicketDto>>
                {
                    Status = false,
                    Message = "The Tickets are not found."
                };
            }
            return new BaseResponse<IList<TicketDto>>
            {
                Status = true,
                Message = "The Tickets are retrieved successfully.",
                Data = tickets.Select(p => new TicketDto
                {
                    Id = p.Id,
                    IsDeleted = p.IsDeleted,
                    TicketNumber = p.TicketNumber
                }).ToList(),
            };
        }
    }
}
