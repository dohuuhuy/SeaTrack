myapp.service('AdminService', function ($http, $filter) {
    this.ListUser = function (roleID) {
        return $http.get('/Admin/HomeAdmin/ListUser/' + roleID)
    }

    
})