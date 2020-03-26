myapp.controller('AdminController', function ($scope, $window, AdminService) {
    LoadListAgency();

    function LoadListAgency() {
        var lstAg = AdminService.ListUser(2)
        lstAg.then(function (d) {
            $scope.Agencys = d.data;
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
    }
    $scope.Resetsave = function () {
        Resetsave();
    }

    $scope.save = function () {
        var user = {
            Username: $scope.Username,
            Password: $scope.Password,
            Fullname: $scope.Fullname,
            Phone: $scope.Phone,
            Address: $scope.Address
        };
        var saverecord = AdminService.save(user);
        saverecord.then(function (d) {
            if (d.data.success===true) {
                LoadListAgency();
                alert("Thêm thành công");
                Resetsave();
            }
        })
    }

})