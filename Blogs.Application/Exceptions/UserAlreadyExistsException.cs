﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Exceptions
{
    public class UserAlreadyExistsException:Exception
    {
        public UserAlreadyExistsException(string email)
       : base($"User with email '{email}' already exists.") { }
    }
}
