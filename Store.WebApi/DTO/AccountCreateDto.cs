﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WebApi.DTO
{
    public class AccountCreateDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }
}