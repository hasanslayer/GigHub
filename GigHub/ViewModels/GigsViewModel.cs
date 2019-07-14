﻿using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpcomingsGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }

    }
}