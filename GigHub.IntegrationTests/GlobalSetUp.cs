using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    [SetUpFixture] // NUnit attribute
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            // need to reference configuration class for a code first migration

            var configuration = new GigHub.Migrations.Configuration();

            var migrator = new DbMigrator(configuration);

            migrator.Update();  // if we don't have a database it will created if not then update to latest version
        }
    }
}
