(function () {
    define(['app/main'], function (app) {
        app.register.factory('alunoApiService', ['$resource', '$q', function ($resource, $q) {            
            return {
                query: function(f)
                {
                    f([{
                        id : '581a1397-7319-4a83-b9d4-1580136548a3',
                        nome : 'teste'
                    }]);
                }
            }

        }]);
    });
}());

