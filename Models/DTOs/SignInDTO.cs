﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SignInDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
