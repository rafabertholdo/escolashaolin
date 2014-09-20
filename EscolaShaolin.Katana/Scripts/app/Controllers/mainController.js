define(['app/main'],function (app) {
    var controller = function ($scope, $http) {        
        $http.get('/api/DynamicData/Academia/Aluno').success(function (data) {                                    
            $scope.alunos = data.$values;            
        });        
    };

    app.controller('mainController',['$scope','$http',controller]);
});