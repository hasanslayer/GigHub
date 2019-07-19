using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Moq;
using ControllerContext = System.Web.Mvc.ControllerContext;

namespace GigHub.IntegrationTests.Extensions
{
    public static class ControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string userId, string username)
        {
            var identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));

            var principal = new GenericPrincipal(identity, null);




            //var mockHttpContext = new Mock<HttpContextBase>();

            //mockHttpContext.SetupGet(c => c.User).Returns(principal);

            //var mockControllerContext = new Mock<ControllerContext>();
            //mockControllerContext.SetupGet(c => c.HttpContext).Returns(mockHttpContext.Object);

            //controller.User = principal; // this is used in apiController not mvc controller so we do 4 lines above

            //controller.ControllerContext = mockControllerContext.Object;



            /* alternative approach */

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext == Mock.Of<HttpContextBase>(http => http.User == principal));

            /* end alternative approach */
        }
    }
}
