var app = angular.module("DeviceApp", []);

app.controller("DeviceController", function ($scope, $http) {
    GetListDevice();
    function GetListDevice() {
        $http({
            method: "GET",
            url: '/Admin/Device/GetListDevice/-1'
        }).then(function (response) {
            console.log(response, 'res');
            $scope.namesData = response.data;
        }, function (error) {
            console.log(error, 'can not get data.');
        });
    };
});
  
app.controller("DeviceController", function ($scope, $http) {
    //GetListDeviceByID(id);
    //function GetListDeviceByID(id) {
    //    $http({
    //        method: 'get',
    //        url: '/Admin/Device/GetListDeviceByID/' + id
    //    }).then(function (response) {
    //        console.log(response, 'res');
    //        $scope.namesData = response.data;
    //    }, function (error) {
    //        console.log(error, 'can not get data.');
    //    });
    //}
    $scope.GetID = function (id) {
        $scope.id = id;
        console.log($scope.id);
    };
});