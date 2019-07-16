using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfiguration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            HasMany(a => a.Followers)
                .WithRequired(a => a.Followee)
                .WillCascadeOnDelete(false);

            HasMany(a => a.Followees)
                 .WithRequired(a => a.Follower)
                 .WillCascadeOnDelete(false);




        }
    }
}