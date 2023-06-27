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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async  Task<BaseResponse<CommentDto>> AddComment(CreateCommentRequestModel model)
        {
            var newComment = await _commentRepository.Get(h => h.UserId == model.UserId);
            if(newComment != null) 
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $"The Message had been registered.",
                };
            }
            var comment = new Comment
            {
                Content = model.Content,
                Subject = model.Subject,
                UserId = model.UserId,
                EventsId = model.EventsId,        
            };
            await _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return new BaseResponse<CommentDto>
            {
                Status = true,
                Message = $"The new Comment is created successfully.",
                Data = new CommentDto
                {
                    Content = comment.Content,
                    Subject = comment.Subject,
                    Id = comment.Id,
                    IsDeleted = comment.IsDeleted
                }
            };

        }

        public async Task<BaseResponse<CommentDto>> DeleteComment(int id)
        {
            var comment = await _commentRepository.Get(id);
            if(comment == null)
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $" The comment is not found.",
                };
            }
            comment.IsDeleted = true;
            _commentRepository.SaveChanges();
            return new BaseResponse<CommentDto>
            {
                Status = true,
                Message = $"The Comment is successfully deleted.",
            };
        }

        public async Task<BaseResponse<IList<CommentDto>>> GetAllComments()
        {
            var comments = await _commentRepository.GetAll();
            if(comments == null || comments.Count == 0)
            {
                return new BaseResponse<IList<CommentDto>>
                {
                    Status = false,
                    Message = "The comments are not found."
                };
            }
            return new BaseResponse<IList<CommentDto>>
            {
                Status = true,
                Message = "All comments are successfully retrieved.",
                Data = comments.Select(j => new CommentDto
                {
                    Id = j.Id,
                    Content = j.Content,
                    Subject = j.Subject,
                    IsDeleted = j.IsDeleted
                }).ToList(),
            };
        }

        public async Task<BaseResponse<CommentDto>> GetCommentByContent(string content)
        {
            var comment = await _commentRepository.Get(b => b.Content.ToLower() == content.ToLower().Trim());
            if(comment == null)
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $"The comment is not found.",
                };
            }
            return new BaseResponse<CommentDto>
            {
                Status = true,
                Message = $" The comment is successfully retrieved.",
                Data = new CommentDto
                {
                    Content = comment.Content,
                    Subject = comment.Subject,
                    IsDeleted = comment.IsDeleted,
                    Id = comment.Id
                }
            };
        }

        public async Task<BaseResponse<CommentDto>> GetCommentById(int id)
        {
            var comment = await _commentRepository.Get(id);
            if (comment == null)
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $"The comment is not found.",
                };
            }
            return new BaseResponse<CommentDto>
            {
                Status = true,
                Message = $" The comment is successfully retrieved.",
                Data = new CommentDto
                {
                    Content = comment.Content,
                    Subject = comment.Subject,
                    IsDeleted = comment.IsDeleted,
                    Id = comment.Id
                }
            };
        }

        
        public async Task<BaseResponse<CommentDto>> GetCommentBySubject(string subject)
        {
            var comment = await _commentRepository.Get(v => v.Subject.ToLower() == subject.ToLower().Trim());
            if (comment == null)
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $"The comment is not found.",
                };
            }
            return new BaseResponse<CommentDto>
            {
                Status = true,
                Message = $" The comment is successfully retrieved.",
                Data = new CommentDto
                {
                    Content = comment.Content,
                    Subject = comment.Subject,
                    IsDeleted = comment.IsDeleted,
                    Id = comment.Id
                }
            };
        }

        public async Task<BaseResponse<CommentDto>> GetCommentByUserId(int userId)
        {
            var comment = await _commentRepository.Get(m => m.UserId == userId);
            if (comment == null)
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $"The comment is not found.",
                };
            }
            return new BaseResponse<CommentDto>
            {
                Status = true,
                Message = $" The comment is successfully retrieved.",
                Data = new CommentDto
                {
                    Content = comment.Content,
                    Subject = comment.Subject,
                    IsDeleted = comment.IsDeleted,
                    Id = comment.Id
                }
            };
        }

        public async Task<BaseResponse<IList<CommentDto>>> GetSelectedComments(IList<int> ids)
        {
            var comments = await _commentRepository.GetSelected(ids);
            if(comments == null || comments.Count == 0)
            {
                return new BaseResponse<IList<CommentDto>>
                {
                    Status = false,
                    Message = $"The selected comments are not found.",
                };
            }
           
            return new BaseResponse<IList<CommentDto>>
            {
                Status = true,
                Message = $"The selected comments are retrieved successfully.",
                Data = comments.Select(g => new CommentDto
                {
                    Content = g.Content,
                    Subject = g.Subject,
                    IsDeleted = g.IsDeleted,
                    Id = g.Id

                }).ToList()
            };
        }

        public async Task<BaseResponse<CommentDto>> UpdateComment(int id, UpdateCommentRequestModel model)
        {
            var comment = await _commentRepository.Get(id);
            if(comment == null)
            {
                return new BaseResponse<CommentDto>
                {
                    Status = false,
                    Message = $"The comment is not found.",
                };
            }
            else
            {
                comment.Content = model.Content;
                comment.Subject = model.Subject;
                comment.IsDeleted = false;

                await _commentRepository.Update(comment);
                _commentRepository.SaveChanges();
                return new BaseResponse<CommentDto>
                {
                    Status = true,
                    Message = $"The comment is successfully updated.",
                    Data = new CommentDto
                    {
                        Content = comment.Content,
                        Subject = comment.Subject,
                        IsDeleted = comment.IsDeleted,
                        Id = comment.Id
                    }
                };

            }
        }
    }
}
