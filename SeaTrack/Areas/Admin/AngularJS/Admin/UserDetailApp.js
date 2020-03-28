    var app = angular.module("App", []);

    app.controller("Controller", function ($scope, $http) {
        $scope.UserID = function (id) {
            $scope.id = id;
            fetchData(id);
        }
        $scope.AddDevice = function (UserID) {
        GetListDeviceNotUsedByUser(UserID);
}
        $scope.RemoveDeviceFromUser = function (UserID, DeviceID) {
        var RemoveModel = {UserID:UserID, DeviceID:DeviceID };
        $http({
            method: "POST",
            url: '/Admin/Device/RemoveDeviceFromUser/',
            data: RemoveModel
        }).then (function (response){
                        console.log(response, 'res');

});
fetchData(UserID);


}

        $scope.AddDeviceToUser = function (UserID, DeviceID) {
        var Model = {UserID:UserID, DeviceID:DeviceID };
        $http({
            method: "POST",
            url: '/Admin/Device/AddDeviceToUser/',
            data: Model
        }).then (function (response){
                        console.log(response, 'res');

});
GetListDeviceNotUsedByUser(UserID);
fetchData(UserID);

}

$scope.LoadDevice = function (UserID){
fetchData(UserID);
}
        function GetListDeviceNotUsedByUser(UserID) {
            $http({
                method: "GET",
                url: '/Admin/Device/GetListDeviceNotUsedByUser/' + UserID
            }).then(function (response) {
                console.log(response, 'res');
                $scope.DevicesNotUsed = response.data;
            }, function (error) {
                console.log(error, 'can not get data.');
            });
        };
        function fetchData(UserID) {
            $http({
                method: "GET",
                url: '/Admin/Device/GetListDeviceByUserID/' + UserID
            }).then(function (response) {
                console.log(response, 'res');
                $scope.Devices = response.data;
            }, function (error) {
                console.log(error, 'can not get data.');
            });
        };

    })
