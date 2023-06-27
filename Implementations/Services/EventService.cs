using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static EventApp.DTOs.TicketDto;

namespace EventApp.Implementations.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventAttendeeRepository _eventAttendeeRepository;
       
        public EventService(IEventRepository eventRepository, ICategoryRepository categoryRepository,
            IOrganizerRepository organizerRepository, IUserRepository userRepository, 
            IAttendeeRepository attendeeRepository, ITicketRepository ticketRepository, 
            IEventAttendeeRepository eventAttendeeRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _organizerRepository = organizerRepository;
            _userRepository = userRepository;
            _attendeeRepository = attendeeRepository;
            _ticketRepository = ticketRepository;
            _eventAttendeeRepository = eventAttendeeRepository;
        }
        public async Task<BaseResponse<EventDto>> CreateEvent(CreateEventRequestModel model)
        {
            var eventExist = await _eventRepository.Get(events => events.Title == model.Title && events.EventDate == model.EventDate);
            if(eventExist != null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event was created before now.."
                };
            }
            else
            {
                var events = new Events
                {
                   Created = DateTime.UtcNow,
                   EventDate = model.EventDate,
                   EventTime = model.EventTime,
                   IsDeleted = false,
                   Theme = model.Theme,
                   Title = model.Title,
                   EventType = model.EventType,
                   Venue = model.Venue,
                   Instructions = model.Instructions,
                };
                var categories = await _categoryRepository.GetSelected(model.CategoryIds);
                foreach(var category in categories)
                {
                    var eventCategory = new EventCategory
                    {
                        CategoryId = category.Id,
                        EventId = events.Id,
                        Category = category,
                        Event = events,
                        IsDeleted = false
                    };
                    events.EventCategories.Add(eventCategory);
                }
                
                var organizers = await _organizerRepository.GetSelected(model.OrganizerIds);
                foreach (var organizer  in  organizers)
                {
                    var eventOrganizer = new EventOrganizer
                    {
                        Event = events,
                        EventId = events.Id,
                        OrganizerId = organizer.Id,
                        IsDeleted = organizer.IsDeleted,
                        Organizer = organizer,
                    };
                    events.EventOrganizers.Add(eventOrganizer);               
                }

                await _eventRepository.Create(events);
                return new BaseResponse<EventDto>
                {
                    Status = true,
                    Message = $"{events.Title} is successfully created.",
                    Data = new EventDto
                    {
                        Id = events.Id,
                        Title = events.Title,
                        Theme = events.Theme,
                        EventDate = events.EventDate,
                        Venue = events.Venue,
                        EventTime = events.EventTime,
                        EventType = model.EventType,
                        Created = DateTime.UtcNow,
                        Instructions = events.Instructions,
                        IsDeleted = events.IsDeleted
                    }
                };
            }   
        }

        public async Task<BaseResponse<EventDto>> DeleteEvent(int id)
        {
            var events = await _eventRepository.Get(id);
            if(events == null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event searching for cannot be find.",
                };
            }
            events.IsDeleted = true;
            _eventRepository.SaveChanges();
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "The Event is successfully deleted.",
            };
        }

        public async Task<BaseResponse<IList<EventDto>>> GetAll()
        {
            var events = await _eventRepository.GetAll();
            if(events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "The Events are Not retrieved."
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = "The Evenets are successfully retrieved.",
                Data = events.Select(h => new EventDto
                {
                   Id = h.Id,
                  Created = h.Created,
                  EventDate = h.EventDate,
                  Theme = h.Theme,
                  IsDeleted = h.IsDeleted,
                  EventType =h.EventType,
                  Title = h.Title,
                  Venue = h.Venue,
                  EventTime = h.EventTime,
                  Instructions = h.Instructions
                }).ToList()
            };
        }

        public Task<BaseResponse<IList<EventDto>>> GetAll(Expression<Func<Events, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IList<EventDto>>> GetAllCurrentEvents()
        {
            var events = await _eventRepository.GetAll(f => f.EventDate > DateTime.Today && f.EventType != Enums.EventType.PrivateEvent );
            if(events == null || events.Count == 0)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = " Sorry, no event is currently available.",
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = "The events are retrieved successfully.",
                Data = events.Select( b => new EventDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Theme = b.Theme,
                    Venue = b.Venue,
                    EventDate = b.EventDate,
                    EventTime = b.EventTime,
                    Instructions = b.Instructions,
                    IsDeleted = b.IsDeleted
                }).ToList(),
            };
        }

       
        public async Task<BaseResponse<EventDto>> GetEventByEventDate(DateTime eventDate)
        {
            var events = await _eventRepository.Get(p => p.EventDate == eventDate);
            if(events == null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event is not found."
                };
            }
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "The Event searching for is successfully recalled.",
                Data = new EventDto
                {
                    Id = events.Id,
                    IsDeleted = events.IsDeleted,
                    Theme = events.Theme,
                    Title = events.Title,
                    Venue = events.Venue,
                    EventTime = events.EventTime,
                    Instructions  =events.Instructions,
                    EventCategories = events.EventCategories.Select(p => new CategoryDto
                    {
                        Id = p.Id,
                        Name = p.Category.Name,
                        Description = p.Category.Description,
                    }).ToList(),
                    EventDate = events.EventDate,
                    EventOrganizers = events.EventOrganizers.Select(b => new OrganizerDto
                    {
                        Id = b.Id,
                        FirstName = b.Organizer.FirstName,
                        LastName = b.Organizer.LastName,
                        Address = b.Organizer.Address,
                        PhoneNumber = b.Organizer.PhoneNumber,
                        Organization = b.Organizer.Organization,
                        Position = b.Organizer.Position,
                    }).ToList(),  
                }
            };
        }

        public async Task<BaseResponse<EventDto>> GetEventById(int id)
        {
            var events = await _eventRepository.Get(id);
            if(events == null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event cannot be found.", 
                };
            }
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "The Event searching for is successfully retrieved.",
                Data = new EventDto
                {
                    Id = events.Id,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    Theme = events.Theme,
                    IsDeleted = events.IsDeleted,
                    EventType = events.EventType,
                    Title = events.Title,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                    EventOrganizers = events.EventOrganizers.Select(g => new OrganizerDto
                    {
                        FirstName = g.Organizer.FirstName,
                        LastName = g.Organizer.LastName,
                        PhoneNumber = g.Organizer.PhoneNumber,
                        Address = g.Organizer.Address,
                        Id = g.Id,
                        Organization = g.Organizer.Organization,
                        Position = g.Organizer.Position,
                    }).ToList(),

                    EventCategories = events.EventCategories.Select(b => new CategoryDto
                    {
                        Id = b.Id,
                        Description = b.Category.Description,
                        Name = b.Category.Name,
                    }).ToList(),
                }
            };
        }

       
        

        public async Task<BaseResponse<EventDto>> GetEventByTitle(string title)
        {
            var events = await _eventRepository.Get(b => b.Title.ToLower() == title.ToLower().Trim());
            if(events == null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event searching for is not found.",
                };
            }
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "The Event is successfully recalled.",
                Data = new EventDto
                {
                    Id = events.Id,
                    Theme = events.Theme,
                    Title = events.Title,
                    Venue = events.Venue,
                    EventType = events.EventType,
                    EventTime = events.EventTime,
                    Instructions = events.Instructions,
                    IsDeleted = events.IsDeleted,
                    EventCategories = events.EventCategories.Select( f => new CategoryDto
                    {
                        Id = f.Id,
                        Description = f.Category.Description,
                        Name = f.Category.Name,
                        IsDeleted = f.IsDeleted
                    }).ToList(),
                    EventDate = events.EventDate,
                    EventOrganizers = events.EventOrganizers.Select(s => new OrganizerDto
                    {
                        Id = s.Id,
                        FirstName = s.Organizer.FirstName,
                        LastName = s.Organizer.LastName,
                        PhoneNumber = s.Organizer.PhoneNumber,
                        Address = s.Organizer.Address,
                        Organization = s.Organizer.Organization,
                        Position = s.Organizer.Position,
                    }).ToList(),  
                }
            };
        }

        public async Task<BaseResponse<EventDto>> GetEventByVenue(string venue)
        {
            var events = await _eventRepository.Get(j => j.Venue.ToLower() == venue.ToLower().Trim());
            if(events == null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event looking for is not found.",
                };
            }
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "The Event is successfully retrieved.",
                Data = new EventDto
                {
                    Id = events.Id,
                    EventType = events.EventType,
                    Theme = events.Theme,
                    Title = events.Title,
                    Venue = events.Venue,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    IsDeleted = events.IsDeleted,
                    Instructions = events.Instructions,
                    EventCategories = events.EventCategories.Select(f => new CategoryDto 
                    {
                        Description = f.Category.Description,
                        Name = f.Category.Name,
                        Id = f.Id,
                        IsDeleted = f.IsDeleted
                    }).ToList(),
                    EventOrganizers = events.EventOrganizers.Select(h => new OrganizerDto 
                    {
                        FirstName = h.Organizer.FirstName,
                        LastName = h.Organizer.LastName,
                        PhoneNumber = h.Organizer.PhoneNumber,
                        Address = h.Organizer.Address,
                        Organization = h.Organizer.Organization,
                        Position = h.Organizer.Position,
                    }).ToList(),  
                }
            };
        }

        public async Task<BaseResponse<IList<EventDto>>> GetSelectedEvents(IList<int> ids)
        {
            var events = await _eventRepository.GetSelected(ids);

            if (events == null || events.Count == 0)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "The selected Events are not found."
                };
            }
            return new BaseResponse<IList<EventDto>>
            
            {
                Status = true,
                Message = "The selected Events are successfully retrieved.",
                Data = events.Select(d => new EventDto
                {
                    EventCategories = d.EventCategories.Select(k => new CategoryDto
                    {
                        Description = k.Category.Description,
                        Name = k.Category.Name
                    }).ToList(),
                    EventDate = d.EventDate,
                    EventOrganizers = d.EventOrganizers.Select(f => new OrganizerDto
                    {
                        FirstName = f.Organizer.FirstName,
                        LastName = f.Organizer.LastName,
                        Address = f.Organizer.Address,
                        Organization = f.Organizer.Organization,
                        PhoneNumber = f.Organizer.PhoneNumber,
                        Position = f.Organizer.Position,
                    }).ToList(),
                    EventTime = d.EventTime,
                    Theme = d.Theme,
                    Title = d.Title,
                    Venue = d.Venue,
                    Created = d.Created,
                    Instructions = d.Instructions,
                    IsDeleted = d.IsDeleted,
                    Id = d.Id,
                    EventType = d.EventType
                }).ToList()
            };
        }

        public async Task<BaseResponse<EventDto>> BookForEvent(int attendeeId, int eventId)
        {
            var attendeeEvent = await _eventRepository.GetAttendeeEventById(attendeeId, eventId);
            var attendee = await _attendeeRepository.Get(attendeeId);
            var events = await _eventRepository.Get(eventId);
                if (attendeeEvent != null || attendeeEvent.Event.EventType == Enums.EventType.PrivateEvent || attendeeEvent.Event.EventDate < DateTime.UtcNow)
                {
                    return new BaseResponse<EventDto>
                    {
                        Status = false,
                        Message = "The Event searching for is either not found, past not an open event or has been registered for bsfore now.",
                    };
                }
            
            var eventAttendee = new EventAttendee
            {
               
                Event = events,
                EventId = events.Id,
                Attendee = attendee,
                AttendeeId = attendee.Id,
                IsDeleted = false,
                BookingStatus = Enums.BookingStatus.booked
            };
          
            var ticket = new Ticket
            {
                AttendeeId = attendee.Id,
                EventId = events.Id,
                TicketNumber = Guid.NewGuid().ToString().Substring(0, 11).Replace(" - ", " ").ToUpper(),
                EventType = events.EventType
            };
            await _eventAttendeeRepository.Create(eventAttendee);
            await _ticketRepository.Create(ticket);          
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "Your registration for the Event is successful.",
                Data = new EventDto
                {
                    Id = events.Id,
                    Title = events.Title,
                    Theme = events.Theme,
                    Venue = events.Venue,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    EventType = events.EventType
                }
            };
        }

        public async Task<BaseResponse<IList<EventDto>>> SearchEvent(string searchText)
        {
            var events = await _eventRepository.Search(searchText);
            if(events == null || events.Count == 0)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "The Events searching for are not found.",
                };
            }

            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = "The Events are successfully retrieved.",
                Data = events.Select(n => new EventDto
                {
                    Id = n.Id,
                    EventDate = n.EventDate,
                    EventOrganizers = n.EventOrganizers,
                    EventTime = n.EventTime,
                    Theme = n.Theme,
                    Title = n.Title,
                    Venue = n.Venue,
                    Instructions = n.Instructions,
                    EventCategories = n.EventCategories
                }).ToList(),
            };
        }

        public async Task<BaseResponse<EventDto>> UpdateEvent(int id, UpdateEventRequestModel model)
        {
            var events = await _eventRepository.Get(id);
            if(events == null)
            {
                return new BaseResponse<EventDto>
                {
                    Status = false,
                    Message = "The Event searching for is not found.",
                };
            }
            await _eventRepository.Update(events);
            _eventRepository.SaveChanges();
            return new BaseResponse<EventDto>
            {
                Status = true,
                Message = "The Event is successfully updated.",
                Data = new EventDto
                {
                    Id = events.Id,
                    Theme = events.Theme,
                    Title = events.Title,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                    IsDeleted = events.IsDeleted,
                    EventType = events.EventType,
                    EventCategories = events.EventCategories.Select(h => new CategoryDto
                    {
                        Description = h.Category.Description,
                        Name = h.Category.Name,
                    }).ToList(),
                    EventDate = events.EventDate,
                    EventOrganizers = events.EventOrganizers.Select(b => new OrganizerDto 
                    {
                        FirstName = b.Organizer.FirstName,
                        LastName = b.Organizer.LastName,
                        Address = b.Organizer.Address,
                        Organization = b.Organizer.Organization,
                        PhoneNumber = b.Organizer.PhoneNumber,
                        Position = b.Organizer.Position,
                    }).ToList(),
                    EventTime = events.EventTime,               
                }      
            };
        }

        public async Task<BaseResponse<IList<EventDto>>> GetAttendeeAreaOfInterestEvents(int attendeeId)
        {
            List<Events> filtered = new List<Events>();
            
            var attendeeAreaOfInterestIds = await _attendeeRepository.GetAttendeeCategories(attendeeId);
            var events = await _eventRepository.GetAllNotPrivateEvents();
            foreach (var e in events)
            {
                var eventCategories = await _categoryRepository.GetCategoriesByEvent(e.Id);
                var istaken = eventCategories.Where(a => attendeeAreaOfInterestIds.Contains(a.Id)).ToList();
               if(istaken.Count != 0)
                {
                    filtered.Add(e);
                }
            }

            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = "The Events are successfully retrieved.",
                Data = filtered.Select(n => new EventDto
                {
                    Id = n.Id,
                    EventDate = n.EventDate,
                    EventTime = n.EventTime,
                    Theme = n.Theme,
                    Title = n.Title,
                    Venue = n.Venue,
                    Instructions = n.Instructions
                }).ToList(),
            };

        }

        public async Task<BaseResponse<IList<EventDto>>> GetAllAttendeePastEvents(int attendeeId)
        {
            var events = await _eventRepository.GetAllAttendeePastEvents(attendeeId);
            if( events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "No records of  history of the Attendee 's past events."
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = "The history  of past events record of the Attendee are successfully retrieved.",
                Data = events.Select(events => new EventDto
                {
                    Title  = events.Title,
                    Theme = events.Theme,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    IsDeleted = events.IsDeleted,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                    EventType = events.EventType,
                    Id = events.Id
                }).ToList()
            };
        }

        public async Task<BaseResponse<IList<EventDto>>> GetAllAttendeeUpComingEvents(int attendeeId)
        {
            var events = await _eventRepository.GetAllAttendeeUpcomingEvents(attendeeId);
            if(events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = " No history records of events of the Attendee can be found.",
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = " The Attendee's upcomimg events are successfully retrieved. ",
                Data = events.Select(events => new EventDto
                {
                    Id = events.Id,
                    Title = events.Title,
                    Theme = events.Theme,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    EventType = events.EventType,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                }).ToList(),
            };
        }

        public async Task<BaseResponse<IList<EventDto>>> GetAllOrganizerPastEvents(int organizerId)
        {
            var events = await _eventRepository.GetAllOrganizerPastEvents(organizerId);
            if (events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "The Organizer has no past record of events.",
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = " The  history of events of the Organizer are successfully retrieved.",
                Data = events.Select(events => new EventDto
                {
                    Id = events.Id,
                    Title = events.Title,
                    Theme = events.Theme,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    EventType = events.EventType,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                }).ToList(),
            };
            
        }

        public async Task<BaseResponse<IList<EventDto>>> GetAllOrganizerUpComingEvents(int organizerId)
        {
            var events = await _eventRepository.GetAllAttendeeUpcomingEvents(organizerId);
            if (events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "The Organizer has no upcoming events.",
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = " The Organizer's upcoming  events are successfully retrieved.",
                Data = events.Select(events => new EventDto
                {
                    Id = events.Id,
                    Title = events.Title,
                    Theme = events.Theme,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    EventType = events.EventType,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                }).ToList(),
            };

        }

        public async Task<BaseResponse<IList<EventDto>>> GetAllBookedEvents(int attendeeId)
        {
            var events = await _eventRepository.GetAllBookedEvents(attendeeId);
            if(events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "No booked events.",
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = false,
                Message = "The booked events are successfully retrieved.",
                Data = events.Select(events => new EventDto
                {
                    Id = events.Id,
                    Title = events.Title,
                    Theme = events.Theme,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    EventType = events.EventType,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                }).ToList()
            };
        }

        public async Task<BaseResponse<EventAttendee>> CancelEvent(int attendeeId, int eventsId) 
        {
            var eventAttendee = await _eventRepository.GetAttendeeEventById(attendeeId, eventsId);
            if(eventAttendee == null)
            {
                return new BaseResponse<EventAttendee>
                {
                    Status = false,
                    Message = "The event is not found.",
                };
            }
            eventAttendee.BookingStatus = Enums.BookingStatus.cancelled;
            await _eventAttendeeRepository.Update(eventAttendee);
            return new BaseResponse<EventAttendee>
            {
                Status = false,
                Message = "The event is successfully cancelled.",  
            };
        }


        public async Task<BaseResponse<IList<EventDto>>> GetOrganizerAllEvents(int organizerId)
        {
            var events = await _eventRepository.GetOrganizerAllEvents(organizerId);
            if (events == null)
            {
                return new BaseResponse<IList<EventDto>>
                {
                    Status = false,
                    Message = "The Organizer's events cannot be found.",
                };
            }
            return new BaseResponse<IList<EventDto>>
            {
                Status = true,
                Message = "The Organizer's events are succesfully retrieved.",
                Data = events.Select(events => new EventDto
                {
                    Id = events.Id,
                    Title = events.Title,
                    Theme = events.Theme,
                    EventDate = events.EventDate,
                    EventTime = events.EventTime,
                    EventType = events.EventType,
                    Venue = events.Venue,
                    Instructions = events.Instructions,
                }).ToList()
            };
        }
    }
}
