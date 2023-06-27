using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using EventApp.Mail_Model;
using EventApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Implementations.Services
{
    public class OrganizerService : IOrganizerService
    {
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMailServices _mailServices;
        public OrganizerService(IOrganizerRepository organizerRepository, IUserRepository userRepository, 
            IRoleRepository roleRepository, IMailServices mailServices)
        {
            _organizerRepository = organizerRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mailServices = mailServices;
        }
        public async Task<BaseResponse<OrganizerDto>> AddOrganizer(CreateOrganizerRequestModel model)
        {
            var organizerExist =  _organizerRepository.Exist(k => k.Email == model.Email); 
            if(organizerExist)
            {
                return new BaseResponse<OrganizerDto>
                {
                    Status = false,
                    Message = $" The Organizer was registered before now."
                };
            }
            else
            {
                var user = new User
                {
                    IsDeleted = false,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                };
                var roles = await _roleRepository.GetSelected(model.RoleIds);
                foreach(var role in roles)
                {
                    var userRole = new UserRole
                    {
                        User = user,
                        UserId = user.Id,
                        Role = role,
                        RoleId = role.Id
                    };
                    user.UserRoles.Add(userRole);
                }
                var organizer = new Organizer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserId = user.Id,
                    User = user,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Organization = model.Organization,
                    Position  = model.Position,                  
                };
                var welcome = new WelcomeRequest
                {
                    ToEmail = organizer.Email,
                    UserName = organizer.FirstName + organizer.LastName,
                };
                await _userRepository.Create(user);
                await _organizerRepository.Create(organizer);
               // await _mailServices.SendWelcomeEmailAsync(welcome);
                return new BaseResponse<OrganizerDto>
                {
                    Status = true,
                    Message = $"The Organizer is successfully registered.",
                    Data = new OrganizerDto
                    {
                        FirstName = organizer.FirstName,
                        LastName = organizer.LastName,
                        PhoneNumber = organizer.PhoneNumber,
                        Position = organizer.Position,
                        Organization = organizer.Organization,
                        Address = organizer.Address,
                        Id = organizer.Id,
                        IsDeleted = organizer.IsDeleted,
                        UserId = organizer.UserId
                    }
                };
            }
        }

        public async Task<BaseResponse<OrganizerDto>> DeleteOrganizer(int id)
        {
            var organizer = await _organizerRepository.Get(id);
            if(organizer == null)
            {
                return new BaseResponse<OrganizerDto>
                {
                    Status = false,
                    Message = $"The Organizer is not found."
                };
            }
            organizer.IsDeleted = true;
            _organizerRepository.SaveChanges();
            return new BaseResponse<OrganizerDto>
            {
                Status = false,
                Message = $"The Organizer is deleted successfully.",
            };
        }

        public async Task<BaseResponse<IList<OrganizerDto>>> GetAllOrganizers()
        {
            var organizers = await _organizerRepository.GetAll();
            if(organizers == null)
            {
                return new BaseResponse<IList<OrganizerDto>>
                {
                    Status = false,
                    Message = "The Organizers are not found."
                };
            }
            return new BaseResponse<IList<OrganizerDto>>
            {
                Status = true,
                Message = "The Organizers are successfully recalled.",
                Data = organizers.Select(h => new OrganizerDto
                {
                    Id = h.Id,
                    Address = h.Address,
                    Email = h.Email,
                    EventOrganizers = h.EventOrganizers,
                    FirstName = h.FirstName,
                    IsDeleted = h.IsDeleted,
                    LastName = h.LastName,
                    Organization = h.Organization,
                    PhoneNumber = h.PhoneNumber,
                    Position = h.Position,
                    UserId = h.UserId
                }).ToList(),
            };
        }

        public async Task<BaseResponse<OrganizerDto>> GetOrganizerByEmail(string email)
        {
            var organizer = await _organizerRepository.Get(f => f.Email == email);
            if(organizer == null)
            {
                return new BaseResponse<OrganizerDto>
                {
                    Status = false,
                    Message = "The Organizer is not found.",
                };
            }
            return new BaseResponse<OrganizerDto>
            {
                Status = true,
                Message = "The Attendee is successfully retrieved.",
                Data = new OrganizerDto
                {
                    FirstName = organizer.FirstName,
                    LastName = organizer.LastName,
                    PhoneNumber = organizer.PhoneNumber,
                    Address = organizer.Address,
                    Organization = organizer.Organization,
                    Position = organizer.Position,
                    Id = organizer.Id,
                    IsDeleted = organizer.IsDeleted,
                    UserId = organizer.UserId,
                    Email = organizer.Email
                }
            };
        }

        public async Task<BaseResponse<OrganizerDto>> GetOrganizerById(int id)
        {
            var organizer = await _organizerRepository.Get(h => h.Id == id);
            if(organizer == null)
            {
                return new BaseResponse<OrganizerDto>
                {
                    Status = false,
                    Message = $"The Organizer is not found."
                };
            }
            return new BaseResponse<OrganizerDto>
            {
                Status = true,
                Message = $"The Organizer is retrieved successfully.",
                Data = new OrganizerDto
                {
                    FirstName = organizer.FirstName,
                    LastName = organizer.LastName,
                    PhoneNumber = organizer.PhoneNumber,
                    Address = organizer.Address,
                    Organization = organizer.Organization,
                    Position = organizer.Position,
                    Id = organizer.Id,
                    IsDeleted = organizer.IsDeleted,
                    UserId = organizer.UserId,
                    Email = organizer.Email
                }
            };
        }

        public async Task<BaseResponse<OrganizerDto>> GetOrganizerByName(string firstName)
        {
            var organizer = await _organizerRepository.Get(f => f.FirstName.ToLower() == firstName.ToLower().Trim());
            if(organizer == null)
            {
                return new BaseResponse<OrganizerDto>
                {
                    Status = false,
                    Message = $"The Organizer is not found."
                };
            }
            return new BaseResponse<OrganizerDto>
            {
                Status = true,
                Message = $"The Organizer is retrieved Succesfully.",
                Data = new OrganizerDto
                {
                    FirstName = organizer.FirstName,
                    LastName = organizer.LastName,
                    PhoneNumber = organizer.PhoneNumber,
                    Address = organizer.Address,
                    Organization = organizer.Organization,
                    Position = organizer.Position,
                    Id = organizer.Id,
                    IsDeleted = organizer.IsDeleted,
                    UserId = organizer.UserId,
                    Email = organizer.Email
                }
            };
        }

        public async Task<BaseResponse<ICollection<OrganizerDto>>> GetSelectedOrganizers(IList<int> ids)
        {
            var organizers = await _organizerRepository.GetSelected(ids);
            if(organizers == null || organizers.Count == 0)
            {
                return new BaseResponse<ICollection<OrganizerDto>>
                {
                    Status = false,
                    Message = $" The selected Organizers are not found."
                };
            }
                return new BaseResponse<ICollection<OrganizerDto>>
            {
                Status = true,
                Message = $"The selected Organizers are retrieved successfully.",
                Data = organizers.Select(m => new OrganizerDto
                {
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    Position = m.Position,
                    Organization = m.Organization,
                    Id = m.Id,
                    IsDeleted = m.IsDeleted,
                    UserId = m.UserId,
                    Email = m.Email,
                }).ToList()

            };
        }

        public async Task<BaseResponse<OrganizerDto>> UpdateOrganizer(int id, UpdateOrganizerRequestModel model)
        {
            var organizer = await _organizerRepository.Get(id);
            if(organizer == null)
            {
                return new BaseResponse<OrganizerDto>
                {
                    Status = false,
                    Message = $"The organizer is not found."
                };
            }
            else
            {
                organizer.FirstName = model.FirstName;
                organizer.LastName = model.LastName;
                organizer.PhoneNumber = model.PhoneNumber;
                organizer.Organization = model.Organization;
                organizer.Position = model.Position;
                organizer.Email = model.Email;
                organizer.Address = model.Address;
                await _organizerRepository.Update(organizer);

                return new BaseResponse<OrganizerDto>
                {
                    Status = true,
                    Message = $"The organizer is updated successfully.",
                    Data = new OrganizerDto
                    {
                        FirstName = organizer.FirstName,
                        Address = organizer.Address,
                        Id = organizer.Id,
                        Email = organizer.Email,
                        LastName = organizer.LastName,
                        Organization = organizer.Organization,
                        PhoneNumber = organizer.PhoneNumber,
                        Position = organizer.Position,
                        IsDeleted = organizer.IsDeleted,
                        UserId = organizer.UserId
                    }
                };

            }
        }
    }
}
