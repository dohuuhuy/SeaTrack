myapp.filter('ctime', function(){

  return function(jsonDate){

    var date = new Date(parseInt(jsonDate.substr(6)));
    return date;
  };

});
