var app = angular.module("App", []);

app.controller("Controller", function ($scope, $http) {
    $scope.UserID = function (id, role, managey) {
        $scope.role = role;
        $scope.id = id;
        $scope.manageby = managey;
        fetchData(id);
        //GetListDeviceNotUsedByUser(id);
    }
    $scope.AddDevice = function (Username, id) {
        if($scope.role == 2){
        GetListDeviceNotUsedByUser(); //lấy danh sách thiết bị chưa được sử dụng
        }
        if($scope.role == 3){
        GetListDeviceNotUsedByUser(Username);//Lấy danh sách thiết bị của đại lý chưa được sử dụng
        }        
        if($scope.role == 4){
        GetListDeviceOfCustomer(Username, id);//Lây danh sách thiết bị của khách hàng
        }
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
    function GetListDeviceNotUsedByUser(Username) {
        $http({
            method: "GET",
            url: '/Admin/Device/GetListDeviceNotUsedByUser?Username='+Username
        }).then(function (response) {
            console.log(response, 'res');
            $scope.DevicesNotUsed = response.data;
        }, function (error) {
            console.log(error, 'can not get data.');
        });
    };
    function GetListDeviceOfCustomer(ManageBy, UserID) { //ManageBy: Username khách hàng, UserID: UserID người dùng
        var user = { UserID: UserID, ManageBy: ManageBy };
        $http({
            method: "POST",
            url: '/Admin/Device/GetListDeviceOfCustomer',
            data: user
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
