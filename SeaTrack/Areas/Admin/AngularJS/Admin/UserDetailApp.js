var app = angular.module("App", []);

app.controller("Controller", function ($scope, $http) {
    $scope.UserID = function (id) {
        $scope.id = id;
        fetchData(id);
        GetListDeviceNotUsedByUser(id);
    }
    $scope.AddDevice = function (UserID) {
        GetListDeviceNotUsedByUser(UserID);
    }
    $scope.RemoveDeviceFromUser = function (UserID, index) {
        DeviceToRemove = $scope.Devices[index];
        var RemoveModel = { UserID: UserID, DeviceID: DeviceToRemove.DeviceID };
        $http({
            method: "POST",
            url: '/Admin/Device/RemoveDeviceFromUser/',
            data: RemoveModel
        }).then(function (response) {
            console.log(response, 'res');

        });
        $scope.Devices.splice(index, 1);
        $scope.DevicesNotUsed.push(DeviceToRemove);
        //fetchData(UserID);


    }

    $scope.AddDeviceToUser = function (UserID, index) {
        DeviceToAdd = $scope.DevicesNotUsed[index];
        var Model = { UserID: UserID, DeviceID: DeviceToAdd.DeviceID };
        $http({
            method: "POST",
            url: '/Admin/Device/AddDeviceToUser/',
            data: Model
        }).then(function (response) {
            console.log(response, 'res');

        });
        $scope.DevicesNotUsed.splice(index, 1);
        $scope.Devices.push(DeviceToAdd);

        //GetListDeviceNotUsedByUser(UserID);
        //fetchData(UserID);

    }

    $scope.LoadDevice = function (UserID) {
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
