using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.DTOs
{
    public class LoginResponseDto : BaseResponse<UserDto>
    {
        public string  Token { get; set; }
    }
}
