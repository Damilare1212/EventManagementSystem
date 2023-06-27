using EventApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CreateCommentRequestModel
    { 
        public string Subject { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Content { get; set; }
        public int UserId { get; set; }
        public int EventsId { get; set; }
    }

    public class UpdateCommentRequestModel
    {
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
