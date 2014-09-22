﻿(function () {    
    define(['app/main','app/services/alunoApiService'], function (app) {        
        var controller = function ($scope, $resource, alunoApiService, $location) {
            $scope.entity = new alunoApiService();

            $scope.save = function () {
                $scope.entity.$save();
            };

            // callback for ng-click 'cancel':
            $scope.cancel = function () {
                $location.path('/alunos');
            };
        };
        app.register.controller('alunoNewController', ['$scope', '$resource', 'alunoApiService', '$location', controller]);
    });
}());

