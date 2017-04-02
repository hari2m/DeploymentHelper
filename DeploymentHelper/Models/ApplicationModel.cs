using DeploymentHelper.Interface;

namespace DeploymentHelper.Models
{
    public class ApplicationModel : IApplicationModel
    {
        public string ApplicationName { get; set; }
        public string ApplicationType { get; set; }
        public string ApplicationPath { get; set; }
    }
}