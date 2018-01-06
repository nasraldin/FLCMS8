using System.Web.Mvc;

namespace FalMedia.Core.Culture
{
    public class SetCulture : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var culture = CultureHelper.GetImplementedCulture(CultureHelper.GetCurrentNeutralCulture());
            filterContext.RouteData.Values["culture"] = culture;
        }
    }
}