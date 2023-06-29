using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

       

        public async Task<BaseResponse<UserDto>> DeleteUser(int id)
        {
            var user = await _userRepository.Get(id);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = $"The User is not found."
                };
            }
            user.IsDeleted = true;
            _userRepository.SaveChanges();
            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = $"The User is deleted successfully."
            };
        }

        public async Task<BaseResponse<IList<UserDto>>> GetSelectedUser(IList<int> ids)
        {
            var users = await _userRepository.GetSelected(ids);
            if(users == null || users.Count == 0)
            {
                return new BaseResponse<IList<UserDto>>
                {
                    Status = false,
                    Message = $"The Users are not found.",
                };
            }
            users.Select(f => new UserDto
            {
                Password = f.Password,
                Email = f.Email,
                Id = f.Id
            }).ToList();
            return new BaseResponse<IList<UserDto>>
            {
                Status = false,
                Message = $"The Users are retreived successfully.",
            };
        }   

        public async Task<BaseResponse<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userRepository.Get(d => d.Email == email);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = $"The User is not found."
                };
            }
            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = $"The User with {user.Email} address is retrieved successfully.",
                Data = new UserDto
                {
                    Email = user.Email,
                    Password = user.Password,
                    Id = user.Id
                }
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.Get(id);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = $"The user is not found."
                };
            }
            return new BaseResponse<UserDto>
            {
                Status = true,
                Message = $"The User is retrieved successfully.",
                Data = new UserDto
                {
                    Email = user.Email,
                    Password = user.Password,
                    Id = user.Id
                }
            };
        }

       
        public async Task<BaseResponse<UserDto>> Login(LoginUserDto model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user is not null && (BCrypt.Net.BCrypt.Verify(model.Password, user.Password)))
            {
                return new BaseResponse<UserDto>
                {
                    Status = true,
                    Message = $"The User login in successfully.",
                    Data = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = user.Password,
                        Roles = user.UserRoles.Select(user => new RoleDto
                        {
                            Name = user.Role.Name
                        }).ToList()
                    }
                };
            }
            else
            {
                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = $"The User's Password or Email is/are invalid."
                };
            }
            
        }

        public async Task<BaseResponse<UserDto>> UpdateUser(int id, UpdateUserRequestModel model)
        {
            var user = await _userRepository.Get(id);
            if(user == null)
            {

                return new BaseResponse<UserDto>
                {
                    Status = false,
                    Message = $"The User is not found."
                };
            }
            else
            {
                user.Email = model.Email;
                user.Password = model.Password;

                await _userRepository.Update(user);
                return new BaseResponse<UserDto>
                {
                    Status = true,
                    Message = $"The User is Updated successfully.",
                    Data = new UserDto
                    {
                        Email = user.Email,
                        Password = user.Password,
                        Id = user.Id
                    }
                };
            }
        }
    }
}
