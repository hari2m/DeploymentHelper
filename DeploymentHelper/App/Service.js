(function () {
    var serviceProvider = angular.module("serviceProvider", []);

    serviceProvider.service("httpService", ["$http", "$q",
        function ($http, $q) {

            var myService = {
                getPromise: function (url) {
                    var promise = $http.get(url),
                        deferObject = deferObject || $q.defer();

                    promise.then(
                        function (answer) {
                            deferObject.resolve(answer);
                        },
                        function (reason) {
                            deferObject.reject(reason);
                        });
                    return deferObject.promise;
                },
                postPromise: function (url, data) {
                    var promise = $http.post(url, data),
                        deferObject = deferObject || $q.defer();
                    promise.then(
                        function (answer) {
                            deferObject.resolve(answer);
                        },
                        function (reason) {
                            deferObject.reject(reason);
                        });
                }
            };
            return myService;
        }]);
})();