﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Exceptions
{
    public class InvalidCredentialsException:Exception
    {
        public InvalidCredentialsException()
       : base("Invalid email or password.") { }
    }
}
