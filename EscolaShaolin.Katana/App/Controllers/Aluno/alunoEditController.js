(function () {    
    define(['app/main','app/services/alunoApiService','jquery-mask'], function (app) {        
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

            $scope.delete = function () {
                $scope.entity.$delete(function () {                    
                    $location.path('/alunos');
                });
            };

            $scope.entity = alunoApiService.get({ id: $routeParams.id }, function () {
                debugger;
            });
            
            $(document).ready(function () {
                $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
            });            

        };
        app.register.controller('alunoEditController', ['$scope', '$resource', 'alunoApiService', '$routeParams', '$location', controller]);
    });
}());

