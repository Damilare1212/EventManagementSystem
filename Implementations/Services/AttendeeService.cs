using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using EventApp.Mail_Model;
using EventApp.Services;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Implementations.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMailServices _mailServices;
        
        public AttendeeService(IAttendeeRepository attendeeRepository, IUserRepository userRepository, 
            IRoleRepository roleRepository, IEventRepository eventRepository, 
            ICategoryRepository categoryRepository, IMailServices mailServices)
        {
            _attendeeRepository =  attendeeRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _mailServices = mailServices;
        }
        public async Task<BaseResponse<AttendeeDto>> AddAttendee(CreateAttendeeRequestModel model)
        {
            var attendeeExist = await _attendeeRepository.Get(h => h.User.Email == model.Email);
            if(attendeeExist != null)
            {
                return new BaseResponse<AttendeeDto>
                {
                    Status = false,
                    Message = $"The Attendee was registered before now."
                };
            }
            else
            {
                var user = new User
                {
                    IsDeleted = false ,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };
                var role = await _roleRepository.GetByName("Attendee");
               
                
                    var userRole = new UserRole
                    {
                        User = user,
                        UserId= user.Id,
                        Role = role,
                        RoleId = role.Id
                    };
                    user.UserRoles.Add(userRole);
                
                
                await _userRepository.Create(user);
                var attendees = new Attendee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    User = user,
                    Email = model.Email,
                    UserId = user.Id,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender
                };

                var categories = await _categoryRepository.GetSelected(model.CategoryIds);
                foreach (var category in categories)
                {
                    var attendeecCategory = new AttendeeCategory
                    {
                        Category = category,
                        Attendee = attendees,
                        AttendeeId = attendees.Id,
                        CategoryId = category.Id
                    };
                    attendees.AttendeeCategories.Add(attendeecCategory);
                }
                var welcome = new WelcomeRequest
                {
                    ToEmail  = attendees.Email,
                    UserName = $"{attendees.FirstName} {attendees.LastName}",
                };
               var attendee =  await _attendeeRepository.Create(attendees);
               
               // await _mailServices.SendWelcomeEmailAsync(welcome);
                return new BaseResponse<AttendeeDto>
                {
                    Status = true,
                    Message = $" The Attendee is successfully registered.",
                    Data = new AttendeeDto
                    { 
                        FirstName = attendee.FirstName,
                        LastName = attendee.LastName,
                        PhoneNumber = attendee.PhoneNumber,
                        Address = attendee.Address,
                        Id = attendee.Id,
                        Gender = attendee.Gender,
                        UserId = attendee.UserId,
                        Email = attendee.Email
                    }
                };
            }
        }

        public async Task<BaseResponse<AttendeeDto>> DeleteAttendee(int id)
        {
            var attendee = await _attendeeRepository.Get(id);
            if(attendee == null)
            {
                return new BaseResponse<AttendeeDto>
                {
                    Status = false,
                    Message = $"The Attendee is not found."
                };
            }
            attendee.IsDeleted = true;
            _attendeeRepository.SaveChanges();
            return new BaseResponse<AttendeeDto>
            {
                Status = true,
                Message = $"The Attendee is successfully deleted.",
            };
        }

        public Task<BaseResponse<IList<CategoryDto>>> Get(int ateendeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IList<AttendeeDto>>> GetAll()
        {
            var attendees = await _attendeeRepository.GetAll();
            if(attendees == null || attendees.Count == 0)
            {
                return new BaseResponse<IList<AttendeeDto>>
                {
                    Status = false,
                    Message = "The Attendees are not found."
                };
            }
            return new BaseResponse<IList<AttendeeDto>>
            {
                Status = true,
                Message = "The Attendees are successfully recalled.",
                Data = attendees.Select( f => new AttendeeDto
                {
                    Id = f.Id,
                    Address = f.Address,
                    Email = f.Email,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    Gender = f.Gender,
                    IsDeleted = f.IsDeleted,
                    PhoneNumber = f.PhoneNumber,
                    UserId = f.UserId
                }).ToList(),
            };
        }

        public Task<BaseResponse<IList<AttendeeDto>>> GetAll(Expression<Func<Attendee, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<AttendeeDto>> GetAttendee(int id)
        {
            var attendee = await _attendeeRepository.Get(id);
            if(attendee == null)
            {
                return new BaseResponse<AttendeeDto>
                {
                    Status = false,
                    Message = $"The Attendee is not found."
                };
            }
            return new BaseResponse<AttendeeDto>
            {
                Status = true,
                Message = $"The Attendee with Id {attendee.Id} is retrieved successfully.",
                Data = new AttendeeDto
                {  
                    Id = attendee.Id,
                    FirstName = attendee.FirstName,
                    LastName = attendee.LastName,
                    Address = attendee.Address,
                    PhoneNumber = attendee.PhoneNumber,
                    Gender = attendee.Gender,
                    IsDeleted = attendee.IsDeleted,
                    UserId = attendee.UserId,
                    Email = attendee.Email
                }
            };
        }

        public async Task<BaseResponse<AttendeeDto>> GetAttendeeByEmail(string email)
        {
            var attendee = await _attendeeRepository.Get(h => h.Email == email);
            if(attendee == null)
            {
                return new BaseResponse<AttendeeDto>
                {
                    Status = false,
                    Message = "The Attendee searching for is not found.",
                };
            }
            return new BaseResponse<AttendeeDto>
            {
                Status = true,
                Message = " The Attendee is successfully retrieved.",
                Data = new AttendeeDto
                {
                    Id = attendee.Id,
                    FirstName = attendee.FirstName,
                    LastName = attendee.LastName,
                    PhoneNumber = attendee.PhoneNumber,
                    Address = attendee.Address,
                    Gender = attendee.Gender,
                    IsDeleted = attendee.IsDeleted,
                    UserId = attendee.UserId,
                    Email = attendee.Email
                }
            };
        }

        public async Task<BaseResponse<AttendeeDto>> GetAttendeeByFirstName(string firstName)
        {
            var attendee = await _attendeeRepository.Get(f => f.FirstName.ToLower() == firstName.ToLower().Trim());
            if(attendee == null)
            {
                return new BaseResponse<AttendeeDto>
                {
                    Status = false,
                    Message = $"No Attendee with the First Name is found."
                };
            }
            return new BaseResponse<AttendeeDto>
            {
                Status = true,
                Message = $"{attendee.FirstName} is retrieved successfully.",
                Data = new AttendeeDto
                {
                    Id = attendee.Id,
                    FirstName = attendee.FirstName,
                    LastName = attendee.LastName,
                    PhoneNumber = attendee.PhoneNumber,
                    Address = attendee .Address,
                    Gender = attendee.Gender,
                    IsDeleted = attendee.IsDeleted,
                    UserId = attendee.UserId,
                    Email = attendee.Email
                }
            };
        }

        public async Task<BaseResponse<IList<AttendeeDto>>> GetAttendeesByEvent(int eventId)
        {
            var attendees = await _attendeeRepository.GetAttendeesByEvent(eventId);
            if(attendees == null)
            {
                return new BaseResponse<IList<AttendeeDto>>
                {
                    Status = false,
                    Message = "The Ateendees are not found.",
                };
            }
            return new BaseResponse<IList<AttendeeDto>>
            {
                Status = true,
                Message = "The Attendees are successfully retrieved.",
                Data = attendees.Select(attendee => new AttendeeDto
                {
                    Id = attendee.Id,
                    FirstName = attendee.FirstName,
                    LastName = attendee.LastName,
                    PhoneNumber = attendee.PhoneNumber,
                    Address = attendee.Address,
                    Gender = attendee.Gender,
                    IsDeleted = attendee.IsDeleted,
                    UserId = attendee.UserId,
                    Email = attendee.Email

                }).ToList(),
            };
        }

        public async Task<BaseResponse<IList<AttendeeDto>>> GetSelected(IList<int> ids)
        {
            var attendees = await _attendeeRepository.GetSelected(ids);
            if(attendees == null || attendees.Count == 0)
            {
                return new BaseResponse<IList<AttendeeDto>>
                {
                    Status = false,
                    Message = $"The selected Attendees are not found.",
                };
            }
            
            return new BaseResponse<IList<AttendeeDto>>
            {
                Status = true,
                Message = $"The selected Attendees are retrieved successfully.",
                Data = attendees.Select(f => new AttendeeDto
                {
                    Id = f.Id,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    Address = f.Address,
                    PhoneNumber = f.PhoneNumber,
                    Gender = f.Gender,
                    IsDeleted = f.IsDeleted,
                    UserId = f.UserId,
                    Email = f.Email
                }).ToList()
        };
        }

     
        
        public async Task<BaseResponse<AttendeeDto>> UpdateAttendee( int id,UpdateAttendeeRequestModel model)
        {
            var attendee = await _attendeeRepository.Get(id);
            if(attendee == null)
            {
                return new BaseResponse<AttendeeDto>
                {
                    Status = false,
                    Message = $"The Attendee is not found.",
                };
            }
            else
            {
              
                attendee.FirstName = model.FirstName;
                attendee.LastName = model.LastName;
                attendee.PhoneNumber = model.PhoneNumber;
                attendee.Address = model.Address;
                attendee.Email = model.Email;
                

                await _attendeeRepository.Update(attendee);
                return new BaseResponse<AttendeeDto>
                {
                    Status = true,
                    Message = $" {attendee.FirstName}'s details is updated successfully.",
                    Data = new AttendeeDto
                    {
                        Id = attendee.Id,
                        FirstName = attendee.FirstName,
                        LastName = attendee.LastName,
                        Address = attendee.Address,
                        PhoneNumber = attendee.PhoneNumber,
                        Gender = attendee.Gender,
                        UserId = attendee.UserId,
                        IsDeleted = attendee.IsDeleted,
                        Email = attendee.Email
                    }
                };
            }
        }

        
    }
}
