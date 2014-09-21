(function () {
    define(['app/main'], function (app) {
        var controller = function ($scope, $resource) {
            $scope.apps = [
            {
                caption: "alunos",
                image: "aluno.png",
                link: "#/alunos"
            }];
        };
        app.register.controller('menuController', ['$scope', '$resource', controller]);
    });
}());
