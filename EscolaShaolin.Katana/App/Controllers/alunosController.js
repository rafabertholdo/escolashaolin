(function () {    
    define(['app/main', 'app/services/alunoApiService'], function (app) {
        var controller = function ($scope, $resource, alunoApiService) {
            
            alunoApiService.query(function (data) {
                $scope.entities = data;
            });                        

            $scope.delete = function (entity) {                
                entity.$delete(function () {                    
                    alunoApiService.query(function (data) {
                        $scope.entities = data;
                    });
                });
            };
        };
        app.register.controller('alunosController', ['$scope', '$resource', 'alunoApiService', '$routeParams', controller]);
    });
}());

