﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Model.DTO
{
    public class AccountCreateRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }
}