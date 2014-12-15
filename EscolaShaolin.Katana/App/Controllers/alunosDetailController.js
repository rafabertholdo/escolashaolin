(function () {    
    define(['app/main','app/services/alunoApiService','jquery-mask'], function (app) {        
        var controller = function ($scope, $resource, alunoApiService, $routeParams, $location) {
                        
            $scope.state = 'edit';
            
            $scope.switchView = function (state) {
                $scope.state = state;
            };            

            // callback for ng-click 'updateUser':
            $scope.save = function () {
                if ($routeParams.id) {
                    $scope.entity.$update(function () {
                        $location.path('/alunos');
                    });
                }
                else {
                    $scope.entity.$save(function () {
                        $location.path('/alunos');
                    });
                }
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

            $scope.entity = alunoApiService.get({ id: $routeParams.id ? $routeParams.id : '{00000000-0000-0000-0000-000000000000}' }, function () {
                if (!$routeParams.id)
                    delete $scope.entity.id;
            });

            //$(document).ready(function () {
            //    $('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
            //});            

        };
        app.register.controller('alunosDetailController', ['$scope', '$resource', 'alunoApiService', '$routeParams', '$location', controller]);
    });
}());

