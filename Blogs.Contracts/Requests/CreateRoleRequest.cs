﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Contracts.Requests
{
    public class CreateRoleRequest
    {
        public required string Name {  get; set; }
    }
}
