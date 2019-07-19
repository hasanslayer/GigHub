using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private GigHub.Controllers.API.GigsController _apiGigsController;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
            _apiGigsController = new GigHub.Controllers.API.GigsController(new UnitOfWork(_context));
        }

        // in TearDown we should dispose _context because we create it in setup method

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated] // Isolated : because we changed the status of the database and we want make sure that the database should rolled back

        public void Mine_WhenCall_ShouldReturnUpcomingGigs()
        {
            // Arrange

            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();

            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "anything" };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            //Assert

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]

        public void Update_WhenCall_ShouldUpdateTheGivenGig()
        {
            // Arrange

            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);

            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "Venue" };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Update(new GigFormViewModel()
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2 // so id of genre is changed
            });

            //Assert

            // need to refresh a gig after the update
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);
        }

        [Test, Isolated]

        public void Cancel_WhenCall_ShouldCancelTheGivenGig()
        {
            // Arrange

            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);
            

            var genre = _context.Genres.Single(g => g.Id == 1);

            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "Venue" };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Update(new GigFormViewModel()
            {
                Id = gig.Id,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2 // so id of genre is changed
            });

            //Assert

            // need to refresh a gig after the update
            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);
        }

    }
}
