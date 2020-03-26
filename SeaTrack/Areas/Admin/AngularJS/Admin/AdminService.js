myapp.service('AdminService', function ($http, $filter) {
    this.ListUser = function (roleID) {
        return $http.get('/Admin/HomeAdmin/ListUser/' + roleID)
    }

    this.save = function (User) {
        var us ={user: User, roleID:2}
        var request = $http({
            method: 'post',
            url: '/Admin/HomeAdmin/CreateUser',
            data: us
        });
        return request;
    }

})