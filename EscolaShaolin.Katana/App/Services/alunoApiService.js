(function () {
    define(['app/main'], function (app) {        
        app.register.factory('alunoApiService', ['$resource', function ($resource) {
            return $resource('/api/DynamicData/Academia/Aluno/:id',
                { id: '@id' },
                {
                    'update': { method: 'PUT' }
                });
        }]);
    });
}());

