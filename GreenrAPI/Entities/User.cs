﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public int Score { get; }

        public IEnumerable<UserTrip> Trips { get; }
    }
}
