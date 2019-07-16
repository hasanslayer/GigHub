using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetialsViewModel
    {
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }

    }
}