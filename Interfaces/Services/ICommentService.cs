using EventApp.DTOs;
using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Interfaces.Services
{
    public interface ICommentService
    {
        Task<BaseResponse<CommentDto>> AddComment(CreateCommentRequestModel model);
        Task<BaseResponse<CommentDto>> UpdateComment(int id, UpdateCommentRequestModel model);
        Task<BaseResponse<CommentDto>> GetCommentById(int id);
        Task<BaseResponse<CommentDto>> GetCommentBySubject(string subject);
        Task<BaseResponse<CommentDto>> GetCommentByContent(string content);
        Task<BaseResponse<CommentDto>> GetCommentByUserId(int userId);
        Task<BaseResponse<IList<CommentDto>>> GetAllComments();
        Task<BaseResponse<IList<CommentDto>>> GetSelectedComments(IList<int> ids);
        Task<BaseResponse<CommentDto>> DeleteComment(int id);

    }
}
