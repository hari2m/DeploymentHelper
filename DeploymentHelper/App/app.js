(function () {
    var myApp = angular.module("myApp", ["serviceProvider"]);

    myApp.controller("myappController", ["$scope", "myAppService",
        function ($scope, myAppService) {

            $scope.kanna = "kanna";
            $scope.selectedApplication = "";
            $scope.files = [];
            $scope.environmentList = [];
            $scope.selectedEnvironment = {};
            $scope.applicationNames = [];
            $scope.applicationList = [];
            $scope.selectList = [];
            $scope.deploymentListObject = {};
            $scope.deploymentList = [];
            $scope.deploymentListObjectNumber = 0;
            $scope.deploymentSteps = [];

            $scope.getApplicationList = function () {
                myAppService.getApplications().then(function (result) {
                    $scope.applicationNames = result.data.Data;
                    $scope.updateApplicationSelectBox(result.data.Data);
                    $scope.getdeploymentFolder();
                });
            };

            $scope.newEnvironmentSelected = function (selectedEnvironment) {
                $scope.selectedEnvironment = selectedEnvironment;
            };

            $scope.getdeploymentFolder = function () {
                myAppService.getdeploymentFolder().then(function (result) {
                    $scope.environmentList = result.data.Data;
                });
            };

            $scope.updateApplicationSelectBox = function (data) {
                $scope.selectList = [];
                angular.forEach(data, function (value) {
                    $scope.applicationList.push(value);
                    $scope.selectList.push(value.ApplicationName);
                });

            };

            $scope.updatePreviousStep = function () {
                $scope.deploymentListObject = $scope.deploymentList[$scope.deploymentListObjectNumber];
                $scope.deploymentListObjectNumber--;

            };

            $scope.newAppSelected = function (selectedApplication) {
                $scope.deploymentListObject.ApplicationName = selectedApplication;
            };

            $scope.getDeploymentSteps = function (deploymentListObject) {
                deploymentListObject.deploymentFolder = deploymentListObject.deploymentFolder === "" ? deploymentListObject.deploymentFolder : $scope.selectedEnvironment.DeploymentLocation;
                $scope.deploymentList[$scope.deploymentListObjectNumber] = deploymentListObject;
                var target = $scope.getTargetLocation(deploymentListObject);
                $scope.deploymentSteps[$scope.deploymentListObjectNumber] = { Instructions: "Copy files from " + deploymentListObject.deploymentFolder + " to " + target };
                angular.forEach(deploymentListObject.files, function (value) {
                    $scope.uploadFiles(value,target);
                });

                $scope.deploymentListObjectNumber++;
                $scope.deploymentListObject = {};
            };

            $scope.uploadFiles = function (files, target)
            {
                myAppService.uploadFiles(files, target).then(function (result) {
                    console.log(result);
                });
            };

            $scope.getTargetLocation = function (deploymentListObject) {
                var targetLocation = "";
                angular.forEach($scope.applicationList, function (value) {
                    if (deploymentListObject.ApplicationName === value.ApplicationName) targetLocation = value.ApplicationPath;
                });
                return targetLocation;
            };

            $scope.getApplicationList();

        }]);

    myApp.service("myAppService", ["$http", "httpService",
        function ($http) {
            var service = {};
            service.getApplications = function () {
                return $http.get("http://localhost:50610/api/Application/GetApplicationList");
            };

            service.getdeploymentFolder = function () {
                return $http.get("http://localhost:50610/api/DeploymentLocation/GetDeploymentLocation");
            };

            service.uploadFiles = function (files) {
                var payload = new FormData();
                payload.append("file", files);
                //payload.append("target", target);
                return $http.post("http://localhost:50610/api/DeploymentLocation/UploadFile", payload);
            };

            return service;
        }]);

    myApp.directive('ngFileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.ngFileModel);
                var isMultiple = attrs.multiple;
                var modelSetter = model.assign;
                element.bind('change', function () {
                    var values = [];
                    angular.forEach(element[0].files, function (item) {
                        var value = {
                            // File Name 
                            name: item.name,
                            //File Size 
                            size: item.size,
                            //File URL to view 
                            url: URL.createObjectURL(item),
                            // File Input Value 
                            _file: item
                        };
                        values.push(value);
                    });
                    scope.$apply(function () {
                        if (isMultiple) {
                            modelSetter(scope, values);
                        } else {
                            modelSetter(scope, values[0]);
                        }
                    });
                });
            }
        };
    }]);

    angular.module("serviceProvider", []);
})();