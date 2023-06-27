using EventApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface IOrganizerService
    {
        Task<BaseResponse<OrganizerDto>> AddOrganizer(CreateOrganizerRequestModel model);
        Task<BaseResponse<OrganizerDto>> UpdateOrganizer(int id, UpdateOrganizerRequestModel model);
        Task<BaseResponse<OrganizerDto>> DeleteOrganizer(int id);
        Task<BaseResponse<OrganizerDto>> GetOrganizerById(int id);
        Task<BaseResponse<OrganizerDto>> GetOrganizerByName(string firstName);
        Task<BaseResponse<OrganizerDto>> GetOrganizerByEmail(string email);
        Task<BaseResponse<ICollection<OrganizerDto>>> GetSelectedOrganizers( IList<int> ids);
        Task<BaseResponse<IList<OrganizerDto>>> GetAllOrganizers();

    }
}
