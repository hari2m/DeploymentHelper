using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using DeploymentHelper.Models;

namespace DeploymentHelper.Services
{
    public static class ApplicationService
    {
        private const string DefaultTargetLocationKey = "Target";
        private const string DefaultDeploymentLocationKey = "Deployment";
        private const string ListofEnvironments = "ListofEnvironments";

        private static string DeploymentFolderNameDate => DateTime.Now.ToString("dd");

        public static List<ApplicationModel> GetApplicationList()
        {
            var targetLocation = ConfigurationService.GetConfiguration(DefaultTargetLocationKey);

            var targetApplicationList = Directory.GetDirectories(targetLocation);
            var applicationList = new List<ApplicationModel>();
            GetApplicationDetails(targetApplicationList, applicationList);

            return applicationList;
        }

        private static void GetApplicationDetails(string[] targetApplicationList, List<ApplicationModel> applicationList)
        {
            applicationList.AddRange(from targetApplication in targetApplicationList
                                     let fullPath = Path.GetFullPath(targetApplication).TrimEnd(Path.DirectorySeparatorChar)
                                     let applicationName = Path.GetFileName(targetApplication)
                                     select new ApplicationModel
                                     {
                                         ApplicationName = applicationName,
                                         ApplicationPath = fullPath
                                     });
        }

        public static List<EnvironmentListModel> GetDeploymentLocation()
        {
            var folders = new List<EnvironmentListModel>();

            var deploymentLocation = ConfigurationService.GetConfiguration(DefaultDeploymentLocationKey);

            var listOfEnvironments = ConfigurationService.GetConfiguration(ListofEnvironments);

            var environments = listOfEnvironments.Split(',');
            foreach (var environment in environments)
            {
                var environmentList = new EnvironmentListModel();
                environmentList.Environment = environment;
                environmentList.DeploymentLocation = GetDeploymentFolder(deploymentLocation, environment);
                folders.Add(environmentList);
            }

            return folders;
        }

        internal static string GetDeploymentFolder(string deploymentLocation, string environment)
        {
            var iterator = 0;
            var deploymentFolderLocation = "";

            deploymentFolderLocation = AddDeploymentFolderLocation(deploymentLocation, environment, iterator, deploymentFolderLocation);

            Directory.CreateDirectory(deploymentFolderLocation);

            return deploymentFolderLocation;
        }

        internal static string AddDeploymentFolderLocation(string deploymentLocation, string environment,
            int iterator, string deploymentFolderLocation)
        {
            var exists = true;
            while (exists)
            {
                iterator++;
                var deploymentFolderName = GetDeploymentFolderName(environment, DeploymentFolderNameDate, iterator);
                deploymentFolderLocation = GetDeploymentFolderLocation(deploymentLocation, deploymentFolderName);
                exists = Directory.Exists(deploymentFolderLocation);
            }
            return deploymentFolderLocation;
        }


        internal static string GetDeploymentFolderLocation(string deploymentLocation, string deploymentFolderName)
        {
            return deploymentLocation + "\\" + deploymentFolderName;
        }

        internal static string GetDeploymentFolderName(string environment, string deploymentFolderNameDate, int iterator)
        {
            return deploymentFolderNameDate + "-" + iterator + "-" + environment;
        }
    }
}