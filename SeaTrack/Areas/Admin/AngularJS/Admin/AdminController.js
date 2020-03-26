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

    $scope.KeyFilter = function () {

    }

})