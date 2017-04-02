namespace DeploymentHelper.Interface
{
    public interface IApplicationModel
    {
        string ApplicationName { get; set; }
        string ApplicationType { get; set; }
        string ApplicationPath { get; set; }
    }
}