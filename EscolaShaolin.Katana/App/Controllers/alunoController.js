(function () {
    define(['app/main'], function (app) {
        var controller = function ($scope, $resource) {
            var res = $resource('/api/DynamicData/Academia/Aluno/:id',
                { id: '@id' },
                {
                    'update': { method: 'PUT' }
                });

            res.query(function (data) {
                $scope.alunos = data;
            });
        };
        app.register.controller('alunoController', ['$scope', '$resource', controller]);
    });
}());
