﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetArtistFollowedBy(string userId);
    }
}
