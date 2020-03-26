var app = angular.module("DeviceApp", []);

app.controller("DeviceController", function ($scope, $http) 
{
    fetchData();
    function fetchData()
    {
        $http({
            method: "GET",
            url: '/Admin/Device/GetListDevice'
        }).then(function (response) {
            console.log(response, 'res');
            $scope.namesData = response.data;
        }, function (error) {
            console.log(error, 'can not get data.');
        });
    };
})

