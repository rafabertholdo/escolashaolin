define(['app/main'],function (app) {
    var controller = function($scope){
        $scope.greetings = "hello";
    };

    app.controller('mainController',['$scope',controller]);
});