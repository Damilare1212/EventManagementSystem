using EventApp.DTOs;
using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface IAttendeeService
    {
        Task<BaseResponse<AttendeeDto>> AddAttendee(CreateAttendeeRequestModel model);
        Task<BaseResponse<AttendeeDto>> UpdateAttendee( int id,UpdateAttendeeRequestModel model);
        Task<BaseResponse<IList<AttendeeDto>>> GetAll();
        Task<BaseResponse<IList<CategoryDto>>> Get(int ateendeeId);
        Task<BaseResponse<IList<AttendeeDto>>> GetAll(Expression<Func<Attendee, bool>> expression);
        Task<BaseResponse<AttendeeDto>> GetAttendee(int id);
        Task<BaseResponse<AttendeeDto>> GetAttendeeByFirstName(string firstName);
        Task<BaseResponse<AttendeeDto>> GetAttendeeByEmail(string email);
        Task<BaseResponse<AttendeeDto>> DeleteAttendee(int id);
        Task<BaseResponse<IList<AttendeeDto>>> GetSelected(IList<int> ids);
        Task<BaseResponse<IList<AttendeeDto>>> GetAttendeesByEvent(int eventId);

    }
}
