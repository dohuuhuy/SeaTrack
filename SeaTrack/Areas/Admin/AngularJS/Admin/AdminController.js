myapp.controller('AdminController', function ($scope, $window, AdminService) {
    $scope.Role = function (role) {
        $scope.role = role;
        if (role == 2){
            LoadListAgency();
        }
        if(role == 3){
            LoadListCustomer();
        }
        if(role == 4){
            LoadListUser();
        }

    }
    function LoadListAgency() {
        var lstAg = AdminService.ListUser(2)
        lstAg.then(function (d) {
            $scope.Agencys = d.data;
        },
            function () {
                alert("Không thể load danh sách đại lý")
            });
    }
    function LoadListCustomer() {
        var lstAg = AdminService.ListUser(3)
        lstAg.then(function (d) {
            $scope.Customers = d.data;
        },
            function () {
                alert("Không thể load danh sách đại lý")
            });
    }
    function LoadListUser() {
        var lstAg = AdminService.ListUser(4)
        lstAg.then(function (d) {
            $scope.Users = d.data;
        },
            function () {
                alert("Không thể load danh sách đại lý")
            });
    }

    function Resetsave() {
        $scope.Username = "";
        $scope.Password = "";
        $scope.Fullname = "";
        $scope.Phone = "";
        $scope.Address = "";
        $scope.ManageBy = "";
    }
    $scope.ClearSearch = function(){
        $scope.SearchKey = "";
        $scope.Status = null;
    }

    $scope.Resetsave = function () {
        Resetsave();
    }
    $scope.AddCustomer = function(){
        LoadListAgency();
    }
    $scope.AddUser = function(){
        LoadListCustomer();
    }
    $scope.save = function () {
        var user = {
            Username: $scope.Username,
            Password: $scope.Password,
            Fullname: $scope.Fullname,
            Phone: $scope.Phone,
            Address: $scope.Address,
            ManageBy: $scope.ManageBy
        };
        if($scope.role == 2){
            var saverecord = AdminService.save(user, 2);
        }
        if($scope.role == 3){
            var saverecord = AdminService.save(user, 3);
        }
        if($scope.role == 4){
            var saverecord = AdminService.save(user, 4);
        }

        saverecord.then(function (d) {
            if (d.data.success === true) {
                if($scope.role == 2){
                    LoadListAgency();
                }
                if($scope.role == 3){
                    LoadListCustomer();
                }
                if ($scope.role == 4) {
                    LoadListUser();
                } 
                alert("Thêm thành công");
                Resetsave();
            }
        })
    }
    $scope.LockUser = function(UserID){
        var res = AdminService.LockUser(UserID);
        res.then(function(d) {
            alert(d.data);
            if($scope.role == 2){
                LoadListAgency();
            }
            if($scope.role == 3){
                LoadListCustomer();
            }
            if($scope.role == 4) {
                LoadListUser();
            }
        })
    }

    $scope.UnLockUser = function(UserID){
        var res = AdminService.UnLockUser(UserID);
        res.then(function(d) {
            alert(d.data);
            if($scope.role == 2){
                LoadListAgency();
            }
            if($scope.role == 3){
                LoadListCustomer();
            }
            if ($scope.role == 4) {
                LoadListUser();
            }
        })
    }


})