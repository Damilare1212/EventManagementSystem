using EventApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
     public interface IAdminService
    {
        Task<BaseResponse<AdminDto>> AddAdmin(CreateAdminRequestModel model);
        Task<BaseResponse<AdminDto>> UpdateAdmin(int id, UpdateAdminRequestModel model);
        Task<BaseResponse<AdminDto>> GetAdminByUserId(int userId);
        Task<BaseResponse<AdminDto>> GetAdmin(int id);
        Task<BaseResponse<AdminDto>> GetAdminByEmail(string email);
        Task<BaseResponse<AdminDto>> GetAdminByFirstName(string firstName);
        Task<BaseResponse<ICollection<AdminDto>>> GetAllAdmin();
        Task<BaseResponse<AdminDto>> DeleteAdmin(int id);
        

    }
}
