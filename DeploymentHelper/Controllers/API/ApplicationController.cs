using System.Web.Http;
using System.Web.Mvc;
using DeploymentHelper.Services;

namespace DeploymentHelper.Controllers.API
{
    public class ApplicationController : ApiController
    {
        public ActionResult GetApplicationList()
        {
            return new JsonResult { Data = ApplicationService.GetApplicationList() };
        }
    }
}