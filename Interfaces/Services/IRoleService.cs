using EventApp.DTOs;
using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDto>> CreateRole(CreateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> GetRoleByName(string name);
        Task<BaseResponse<RoleDto>> GetRoleById(int id);
        Task<BaseResponse<RoleDto>> DeleteRoleByName(string name);
        Task<BaseResponse<RoleDto>> DeleteRoleById(int id);
        Task<BaseResponse<RoleDto>> UpdateRole(int id, UpdateRoleRequestModel model);
        Task<BaseResponse<IList<RoleDto>>> GetSelectedRoles(IList<int> ids);
        Task<BaseResponse<ICollection<RoleDto>>> GetAllRoles();
    }
}
