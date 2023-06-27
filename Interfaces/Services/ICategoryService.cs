using EventApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryDto>> CreateCategory(CreateCategoryRequestModel model);
        Task<BaseResponse<CategoryDto>> UpdateCategory(int id, UpdateCategoryRequestModel model);
        Task<BaseResponse<CategoryDto>> GetCategoryById(int id);
        Task<BaseResponse<CategoryDto>> GetCategoryByEventId(int eventId);
        Task<BaseResponse<CategoryDto>> GetCategoryByEventName(string eventName);
        Task<BaseResponse<CategoryDto>> GetCategoryByName(string name);
        Task<BaseResponse<IList<CategoryDto>>> GetSelectedCategory(IList<int> ids);
        Task<BaseResponse<IList<CategoryDto>>> GetAll();
        Task<BaseResponse<CategoryDto>> DeleteCategory(int id);
    }
}
