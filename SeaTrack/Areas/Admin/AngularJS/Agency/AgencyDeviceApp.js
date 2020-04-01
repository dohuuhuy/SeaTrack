var DeviceApp = angular.module("AgencyDeviceApp", []);

DeviceApp.controller('DeviceController', function ($scope, $http, DeviceService) {

    $scope.namesData = null;
    DeviceService.GetAllRecords().then(function (d) {
        $scope.namesData = d.data;
    }, function () {
        alert('Unable to Get Data !!!');
    });

    $scope.loadMessage = updateInfo();

    function updateInfo() {
        var today = new Date();
        return "Last updated " + today.toLocaleString() + ".";
    };

    $scope.total = function () {


        var total = 0;
        angular.forEach($scope.namesData, function (item) {
            total++;
        });
        return total;
    };

    $scope.Device = {
        DeviceID: '',
        DeviceNo: '',
        DeviceName: '',
        DeviceImei: '',
        DeviceVersion: '',
        DeviceGroup: '',
        DateExpired: '',
        DeviceNote: ''

    };

    $scope.View = function (data) {
        console.log('i am inside view() + ');
        $scope.Device = {
            DeviceID: data.DeviceID,
            DeviceNo: data.DeviceNo,
            DeviceName: data.DeviceName,
            DeviceImei: data.DeviceImei,
            DeviceVersion: data.DeviceVersion,
            DeviceGroup: data.DeviceGroup,
            DateExpired: data.DateExpired,
            DeviceNote: data.DeviceNote

        };
    };

    $scope.Save = function () {
        if ($scope.Device.DeviceName != "") {
            console.log('i am inside save func' + JSON.stringify($scope.Device));
            $http({
                method: 'POST',
                url: '/Admin/Device/AgencyCreateDevice',
                data: JSON.stringify($scope.Device)
            }).then(function successCallback(response) {
                //$scope.namesData.push(response.data);
                LoadDevice();
                $scope.Clear();
                alert(" Added Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };
    $scope.update = function () {
        //nếu không trường nào bị null
        if (
            $scope.Customer.Name != "") {
            console.log('i am inside update funcr ' +
                JSON.stringify($scope.Customer));
            $http({
                method: 'PUT',
                url: '/Admin/Device/EditDevice/',
                data: JSON.stringify($scope.Device)
            }).then(function successCallback(response) {
                $scope.namesData = null;
                DeviceService.GetAllRecords().then(function (d) {
                    $scope.namesData = d.data;
                }, function () {
                    alert('Unable to Get Data !!!');
                });
                $scope.Clear();
                alert(" Updated Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };
    $scope.Edit = function (data) {
        console.log('i am inside edit() ' + JSON.stringify($scope.Device));
        $scope.Device = {
            DeviceID: data.DeviceID,
            DeviceNo: data.DeviceNo,
            DeviceName: data.DeviceName,
            DeviceImei: data.DeviceImei,
            DeviceVersion: data.DeviceVersion,
            DeviceGroup: data.DeviceGroup,
            DateExpired: data.DateExpired,
            DeviceNote: data.DeviceNote

        };
    };
    $scope.Clear = function () {
        $scope.Device.DeviceID = '',
        $scope.Device.DeviceNo = '',
        $scope.Device.DeviceName = '',
        $scope.Device.DeviceImei = '',
        $scope.Device.DeviceVersion = '',
        $scope.Device.DeviceGroup = '',
        $scope.Device.DateExpired = '',
        $scope.Device.DeviceNote = ''

    };
    $scope.Cancel = function () {
        $scope.clear();

        console.log('i am inside cancel func' + JSON.stringify($scope.Device));
    };

    $scope.Delete = function (index) {

        console.log('i am inside delete funcr' + JSON.stringify($scope.Device));
        $http({
            method: 'GET',
            url: '/Admin/Device/DeleteDevice/' + $scope.namesData[index].DeviceID
        }).then(function successCallback(response) {
            //$scope.namesData.splice(index, 1);
            LoadDevice();
            alert("Device Deleted Successfully !!!");
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };

    function LoadDevice(){
    DeviceService.GetAllRecords().then(function (d) {
        $scope.namesData = d.data;
    }, function () {
        alert('Unable to Get Data !!!');
    });
}
});



DeviceApp.factory('DeviceService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('/Admin/Device/GetListDeviceByUserID');
    };

    console.log('i am inside Service ');

    return fac;
});
