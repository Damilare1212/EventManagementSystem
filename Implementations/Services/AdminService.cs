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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMailServices _mailServices;
        private readonly IAttendeeRepository _attendeeRepository;
        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, 
            IRoleRepository roleRepository, IMailServices mailServices, IAttendeeRepository attendeeRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _adminRepository = adminRepository;
            _mailServices = mailServices;
            _attendeeRepository = attendeeRepository;
        }
        public async Task<BaseResponse<AdminDto>> AddAdmin(CreateAdminRequestModel model)
        {
            var adminExist =  await _adminRepository.Get(h => h.Email == model.Email);
            if (adminExist != null)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = $" The Admin was registered before now.",
                    Status = false,
                };       
            }  
            else
            {
                var userInfo = new User
                {  
                    IsDeleted = false,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                };

                var roles = await _roleRepository.GetSelected(model.RoleIds);
                foreach(var role in roles)
                {
                    var userRole = new UserRole
                    {
                        UserId = userInfo.Id,
                        User = userInfo,
                        RoleId = role.Id,
                        Role = role,
                    };
                    userInfo.UserRoles.Add(userRole);
                }
                await _userRepository.Create(userInfo);
                var adminInfo = new Admin
                {
                   
                    FirstName = model.FirstName,
                    Email = model.Email,
                    User = userInfo,
                    UserId  = userInfo.Id,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    AdminPhoto = model.AdminPhoto,
                    IsDeleted = false,     
                };
                var welcome = new WelcomeRequest
                {
                    ToEmail = adminInfo.Email,
                    UserName = adminInfo.FirstName + adminInfo.LastName,
                };
                
               var admin = await _adminRepository.Create(adminInfo);
                await _mailServices.SendWelcomeEmailAsync(welcome);
                return new BaseResponse<AdminDto>
                {
                    Message = $"{model.FirstName} {model.LastName} is registered successfully.",
                    Status = true,
                    Data = new AdminDto
                    { 
                        Id = admin.Id,
                        FirstName = admin.FirstName,
                        LastName = admin.LastName,
                        AdminPhoto = admin.AdminPhoto,
                        PhoneNumber = admin.PhoneNumber,               
                        UserId = admin.UserId,
                        Email = admin.Email,
                        IsDeleted = admin.IsDeleted
                    }
                };
            }
        }

        public  async Task<BaseResponse<AdminDto>> DeleteAdmin(int id)
        {
            var admin = await _adminRepository.Get(id);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Message = $"The Admin is not found.",
                    Status = false,
                    
                };
            }
            admin.IsDeleted = true;
            _adminRepository.SaveChanges();
            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = $"{admin.FirstName} is deleted successfully.",
            };
        }

        public async Task<BaseResponse<AdminDto>> GetAdminByUserId(int userId)
        {
            var admin = await _userRepository.Get(userId);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = "The Admin searching for is not found."
                };
            }
            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = " The Admin is successfully retrieved.",
                Data = new AdminDto
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    IsDeleted = admin.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<AdminDto>> GetAdmin(int id)
        {
            var admin = await _adminRepository.Get(id);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = $" The Admin is not found."
                };
            }
            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = $"The Admin is retrieved successfully.",
                Data =  new AdminDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    PhoneNumber = admin.PhoneNumber,
                    AdminPhoto = admin.AdminPhoto,
                    UserId = admin.UserId,
                    Email = admin.Email,
                    IsDeleted = admin.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<AdminDto>> GetAdminByEmail(string email)
        {
            var admin = await _adminRepository.Get(f => f.Email == email);
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = "The Admin searching for is not found."

                };
            }
            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message = " The Admin is successfully retrieved.",
                Data =  new AdminDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    PhoneNumber = admin.PhoneNumber,
                    AdminPhoto = admin.AdminPhoto,
                    UserId = admin.UserId,
                    Email = admin.Email,
                    IsDeleted = admin.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<AdminDto>> GetAdminByFirstName(string firstName)
        {
            var admin = await _adminRepository.Get(g => g.FirstName.ToLower() == firstName.ToLower().Trim());
            if(admin == null)
            {
                return new BaseResponse<AdminDto>
                {
                    Status = false,
                    Message = $"The Admin is not found."
                };
            }
            return new BaseResponse<AdminDto>
            {
                Status = true,
                Message =$"The Admin is retrieved successfully.",
                Data = new AdminDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    PhoneNumber = admin.PhoneNumber,
                    AdminPhoto = admin.AdminPhoto,  
                    UserId = admin.UserId,
                    Email = admin.Email,
                    IsDeleted = admin.IsDeleted
                }
            };
        }

       

        public async Task<BaseResponse<ICollection<AdminDto>>> GetAllAdmin()
        {

            var adminList = await _adminRepository.GetAll();
            if(adminList == null)
            {
                return new BaseResponse<ICollection<AdminDto>>
                {
                    Status = false,
                    Message = $"The Admins are not found."
                };
            }
            return new BaseResponse<ICollection<AdminDto>>
            {
                Status = true,
                Message = $"The Admins are retrieved successfully.",
                Data = adminList.Select(f => new AdminDto
                 {
                     Id = f.Id,
                     FirstName = f.FirstName,
                     LastName = f.LastName,
                     PhoneNumber = f.PhoneNumber,
                     AdminPhoto = f.AdminPhoto,
                     UserId = f.UserId,
                     Email = f.Email,
                     IsDeleted = f.IsDeleted
                 }).ToList(),

             };
        }

       
        public async Task<BaseResponse<AdminDto>> UpdateAdmin(int id, UpdateAdminRequestModel model)
        {
            var admin = await _adminRepository.Get(id);
            if(admin == null)
            {
                throw new Exception($"The Admin is not found.");
            }
            else
            {         
                admin.FirstName = model.FirstName;
                admin.LastName = model.LastName;
                admin.PhoneNumber = model.PhoneNumber;
                admin.AdminPhoto = model.AdminPhoto;
                admin.Email = model.Email;
                await _adminRepository.Update(admin);
                return new BaseResponse<AdminDto>
                {
                    Status = true,
                    Message = $"The Admin details is updatated successfully.",
                    Data = new AdminDto
                    {
                        Id = admin.Id,
                        FirstName = admin.FirstName,
                        LastName = admin.LastName,
                        PhoneNumber = admin.PhoneNumber,
                        AdminPhoto = admin.AdminPhoto,
                        UserId = admin.UserId,
                        IsDeleted = admin.IsDeleted,
                        Email = admin.Email
                    }

                };

            }
            
        }

        
    }
}
