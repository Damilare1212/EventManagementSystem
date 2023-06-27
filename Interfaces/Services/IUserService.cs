using EventApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> UpdateUser(int id, UpdateUserRequestModel model);
        Task<BaseResponse<UserDto>> DeleteUser(int id);
        Task<BaseResponse<UserDto>> GetUserById(int id);
        Task<BaseResponse<UserDto>> GetUserByEmail(string email);
        Task<BaseResponse<UserDto>> Login(LoginUserDto model);
    }
}
