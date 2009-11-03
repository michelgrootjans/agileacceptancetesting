using System.Web.Mvc;

namespace Snacks_R_Us.UnitTests.Utilities
{
    public static class AtionResultExtensions
    {
            public static ActionResult ShouldRedirectToController(this ActionResult result, string controller)
            {
                var routeResult = result.ShouldBeOfType<RedirectToRouteResult>();
                routeResult.RouteValues["controller"].ShouldBeEqualTo(controller);
                return result;
            }

            public static ActionResult ShouldRedirectToAction(this ActionResult result, string action)
            {
                var routeResult = result.ShouldBeOfType<RedirectToRouteResult>();
                routeResult.RouteValues["action"].ShouldBeEqualTo(action);

                return result;
            }

            public static ActionResult ShouldHaveIdInRoute(this ActionResult result, string id)
            {
                var routeResult = result.ShouldBeOfType<RedirectToRouteResult>();
                routeResult.RouteValues["id"].ShouldBeEqualTo(id);
                return result;
            }

            public static ActionResult ShouldShowView(this ActionResult result, string viewName)
            {
                var viewResult = result.ShouldBeOfType<ViewResult>();
                viewResult.ViewName.ShouldBeEqualTo(viewName);
                return result;
            }

            public static ActionResult ShouldShowView(this ActionResult result)
            {
                return result.ShouldShowView("");
            }
        }
    }
