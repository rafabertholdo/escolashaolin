(function () {    
    define(['app/main','app/services/alunoApiService'], function (app) {        
        var controller = function ($scope, $resource, alunoApiService, $routeParams, $location) {
            // callback for ng-click 'updateUser':
            $scope.save = function () {                
                $scope.entity.$update(function () {
                    $location.path('/alunos');
                });
            };

            // callback for ng-click 'cancel':
            $scope.cancel = function () {                
                $location.path('/alunos');
            };

            $scope.entity = alunoApiService.get({ id: $routeParams.id });
        };
        app.register.controller('alunoDetailsController', ['$scope', '$resource', 'alunoApiService', '$routeParams', '$location', controller]);
    });
}());

