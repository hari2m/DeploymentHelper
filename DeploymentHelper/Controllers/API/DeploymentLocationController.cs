using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DeploymentHelper.Services;

namespace DeploymentHelper.Controllers.API
{
    public class DeploymentLocationController : ApiController
    {
        public ActionResult GetDeploymentLocation()
        {
            return new JsonResult { Data = ApplicationService.GetDeploymentLocation() };
        }

        [System.Web.Http.HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);

            return new JsonResult();
        }
    }
}
