﻿using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class AspNetUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
