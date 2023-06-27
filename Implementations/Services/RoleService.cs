using EventApp.DTOs;
using EventApp.Entities;
using EventApp.Interfaces.Repositories;
using EventApp.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;  
        }

        public async Task<BaseResponse<RoleDto>> CreateRole(CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.GetByName(model.Name);
            if(roleExist != null)
            {
                return new BaseResponse<RoleDto>
                {
                    Status = false,
                    Message = $"The Role {model.Name}  already exist.",
                };
            }
            var role = new Role
            {
                Name = model.Name,
                Description = model.Description
            };
            await _roleRepository.Create(role);
            return new BaseResponse<RoleDto>
            {
                Status = true,
                Message = $"The new Role {model.Name} is successfully registered.",
                Data = new RoleDto
                {
                    Id = role.Id,
                    Description = role.Description,
                    Name = role.Name
                }
            };

        }

       

        public  async Task<BaseResponse<RoleDto>> DeleteRoleById(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Status = false,
                    Message = $"The Role searching for is not found.",
                };
            }
            role.IsDeleted = true;
            _roleRepository.SaveChanges();
            return new BaseResponse<RoleDto>
            {
                Status = true,
                Message = $"The Role is successfully deleted.",
                Data =  new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> DeleteRoleByName(string name)
        {
            var role = await _roleRepository.GetByName(name);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Status = false,
                    Message = $" The role is not found.",
                };
            }
            role.IsDeleted = true;
            _roleRepository.SaveChanges();
            return new BaseResponse<RoleDto>
            {
                Status = true,
                Message = $"The role is retrieved successfully.",
                Data = new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description,
                    Id  = role.Id
                }
            };

        }

        public async Task<BaseResponse<ICollection<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleRepository.GetAll();
            if(roles == null)
            {
                return new BaseResponse<ICollection<RoleDto>>
                {
                    Status = false,
                    Message = "The Admins are not found."
                };
            }
            return new BaseResponse<ICollection<RoleDto>>
            {
                Status = true,
                Message = "The Admins are successfully recalled.",
                Data = roles.Select(g => new RoleDto
                {
                    Description = g.Description,
                    Id = g.Id,
                    Name = g.Name,
   
                }).ToList(),
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Status = false,
                    Message = $" The Role searching for is not found.",
                };
            }
            return new BaseResponse<RoleDto>
            {
                Status = true,
                Message = $"The Role is retrieved successfully.",
                Data = new RoleDto
                {
                    Id = role.Id,
                    Description = role.Description,
                    Name = role.Name
                }
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRoleByName(string name)
        {
            var role = await _roleRepository.GetByName(name);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Status = false,
                    Message = $" The Role searching for is not found.",
                };
            }
            return new BaseResponse<RoleDto>
            {
                Status = true,
                Message = $"The Role is retrieved successfully.",
                Data = new RoleDto
                {
                    Id = role.Id,
                    Description = role.Description,
                    Name = role.Name
                }
            };
        }

        public async Task<BaseResponse<IList<RoleDto>>> GetSelectedRoles(IList<int> ids)
        {
            var roles = await _roleRepository.GetSelected(ids);
            if(roles == null || roles.Count == 0)
            {
                return new BaseResponse<IList<RoleDto>>
                {
                    Status = false,
                    Message = $"The role is not found.",
                };
            }
            return new BaseResponse<IList<RoleDto>>
            {
                Status = true,
                Message = $"The roles are retrieved successfully.",
                Data =  roles.Select(f => new RoleDto
                {
                    Name = f.Name,
                    Description = f.Description,
                    Id = f.Id
                }).ToList()

            };
        }

        public async Task<BaseResponse<RoleDto>> UpdateRole(int id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.Get(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Status = false,
                    Message = $"The Role is not found.",
                };
            }
            else
            {
                role.Name = model.Name;
                role.Description = model.Description;

                await _roleRepository.Update(role);
                return new BaseResponse<RoleDto>
                {
                    Status = true,
                    Message = $"{role.Name} is updated successfully.",
                    Data = new RoleDto
                    {
                        Id = role.Id,
                        Description = role.Description,
                        Name = role.Name
                    }
                };

            }
        }
    }
}
