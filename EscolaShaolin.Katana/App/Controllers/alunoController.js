(function () {
    define(['app/main'], function (app) {        
        var controller = function ($scope, $http) {
            $http.get('/api/DynamicData/Academia/Aluno').success(function (data) {
                $scope.alunos = data.$values;
            });
        };        
        app.register.controller('alunoController', ['$scope', '$http', controller]);
    });
}());
