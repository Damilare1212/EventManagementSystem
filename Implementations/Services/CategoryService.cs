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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<BaseResponse<CategoryDto>> CreateCategory(CreateCategoryRequestModel model)
        {
            var newCategory = await _categoryRepository.Get( s => s.Name == model.Name);
            if(newCategory != null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category was registered before now.",
                };
            }
            var category = new Category
            {
                
                Name = model.Name,
                Description = model.Description,
            };
            await _categoryRepository.Create(category);
            _categoryRepository.SaveChanges();
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = $"The new Category is successfully created.",
                Data = new CategoryDto
                {
                    Id = category.Id,
                    Description = category.Description,
                    Name = category.Name,
                    IsDeleted = category.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<CategoryDto>> DeleteCategory(int id)
        {
            var category = await _categoryRepository.Get(id);
            if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category is not found.",
                };
            }
             category.IsDeleted = true;
            _categoryRepository.SaveChanges();
            return new BaseResponse<CategoryDto>
            {
                Status = false,
                Message = $"The category is deleted successfully.",
                Data = new CategoryDto
                {
                    Description = category.Description,
                    Name = category.Name,
                    Id = category.Id,
                    IsDeleted = category.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<IList<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            if (categories == null || categories.Count == 0)
            {
                return new BaseResponse<IList<CategoryDto>>
                {
                    Status = false,
                    Message = " The Categories are not found."
                };
            }
            return new BaseResponse<IList<CategoryDto>>
            {
                Status = true,
                Message = "The Categories are successfully retrieved.",
                Data = categories.Select(p => new CategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IsDeleted = p.IsDeleted
                }).ToList(),
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategoryByEventId(int id)
        {
            var category = await _categoryRepository.Get(h => h.Id == id);
            if (category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category is not found.",
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = $"The Category is successfully retrieved.",
                Data = new CategoryDto
                {
                    Description = category.Description,
                    Name = category.Name,
                    Id = category.Id,
                    IsDeleted = category.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategoryByEventName(string description)
        {
            var category = await _categoryRepository.Get(d => d.Description == description);
            if (category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category is not found.", 
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = $"The Category is successfully retrieved.",
                Data = new CategoryDto
                {
                    Description = category.Description,
                    Name = category.Name,
                    Id = category.Id,
                    IsDeleted = category.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.Get(id);
            if (category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category is not found.",
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = $"The Category is successfully retrieved.",
                Data = new CategoryDto
                {
                    Description = category.Description,
                    Name = category.Name,
                    Id = category.Id,
                    IsDeleted = category.IsDeleted
                }
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategoryByName(string name)
        {
            var category = await _categoryRepository.Get(v => v.Name.ToLower() == name.ToLower().Trim());
            if (category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category is not found.",
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = $"The Category is successfully retrieved.",
                Data = new CategoryDto
                {
                    Description = category.Description,
                    Id = category.Id,
                    Name = category.Name,
                }
            };
        }

        public async Task<BaseResponse<IList<CategoryDto>>> GetSelectedCategory(IList<int> ids)
        {
            var categories = await _categoryRepository.GetSelected(ids);
            if (categories == null || categories.Count == 0)
            {
                return new BaseResponse<IList<CategoryDto>>
                {
                    Status = false,
                    Message = $"The selected categories are not found.",
                };
            }

            return new BaseResponse<IList<CategoryDto>>
            {
                Status = true,
                Message = $"The selected categories are retrieved successfully.",
                Data = categories.Select(m => new CategoryDto
                {
                    Description = m.Description,
                    Name = m.Name,
                    Id = m.Id
                }).ToList()
            };
        }
        public async Task<BaseResponse<CategoryDto>> UpdateCategory(int id, UpdateCategoryRequestModel model)
        {
            var category = await _categoryRepository.Get(id);
            if(category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = $"The Category is not found.",
                };
            }
            else
            {
                category.Description = model.Description;
                category.Name = model.Name;
            }
            await _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = $"The Category is updated successfully.",
                Data = new CategoryDto
                {
                    Description = category.Description,
                    Name = category.Name,
                    Id = category.Id,
                    IsDeleted = category.IsDeleted
                }
            };
        }
    }
}
