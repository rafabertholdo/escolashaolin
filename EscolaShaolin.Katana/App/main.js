(function () {
    var dependencies = [
        'app/services/routeResolver',
        'moment',        
        'jquery-mask'
    ];

    define(dependencies, function () {
        var app = angular.module('myApp', ['ngRoute', 'ngResource', 'routeResolverServices']);

        app.config(['$routeProvider', 'routeResolverProvider', '$controllerProvider',
                 '$compileProvider', '$filterProvider', '$provide', '$httpProvider', '$resourceProvider',

         function ($routeProvider, routeResolverProvider, $controllerProvider,
                   $compileProvider, $filterProvider, $provide, $httpProvider, $resourceProvider) {

             //Change default views and controllers directory using the following:
             //routeResolverProvider.routeConfig.setBaseDirectories('/app/views', '/app/controllers');        
             app.register =
             {
                 controller: $controllerProvider.register,
                 directive: $compileProvider.directive,
                 filter: $filterProvider.register,
                 factory: $provide.factory,
                 service: $provide.service
             };

             //Define routes - controllers will be loaded dynamically
             var route = routeResolverProvider.route;             
             $routeProvider
                 //route.resolve() now accepts the convention to use (name of controller & view) as well as the 
                 //path where the controller or view lives in the controllers or views folder if it's in a sub folder. 
                 //For example, the controllers for customers live in controllers/customers and the views are in views/customers.
                 //The controllers for orders live in controllers/orders and the views are in views/orders
                 //The second parameter allows for putting related controllers/views into subfolders to better organize large projects
                 //Thanks to Ton Yeung for the idea and contribution
                 .when('/:controller/:id?', route.resolve())
                 ////.when('/main', route.resolve('menu', '', 'menu'))
                 ////.when('/alunos', route.resolve('list', 'aluno/', 'alunoList'))
                 ////.when('/alunos/new', route.resolve('detail', 'aluno/', 'alunoEdit'))
                 ////.when('/alunos/:id', route.resolve('detail', 'aluno/', 'alunoEdit'))
                 //.when('/customerorders/:customerId', route.resolve('CustomerOrders', 'customers/'))
                 //.when('/customeredit/:customerId', route.resolve('CustomerEdit', 'customers/', true))
                 //.when('/orders', route.resolve('Orders', 'orders/'))
                 //.when('/about', route.resolve('About'))
                 //.when('/login/:redirect*?', route.resolve('Login'))
                 .otherwise({ redirectTo: '/main' });

         }]);

        app.directive('date', function () {
            return {
                require: 'ngModel',
                link: function (scope, element, attrs, ngModelController) {                    
                    $(element[0]).mask("00/00/0000", { placeholder: "__/__/____" });
                    ngModelController.$parsers.push(function (data) {
                        //convert data from view format to model format                        
                        var pattern = 'DD/MM/YYYY HH:mm:SS';
                        var output = "-";
                        if (data !== null && data !== undefined && data.length > 0) {
                            return moment(data, 'DD/MM/YYYY').format(pattern);
                        }
                    });

                    ngModelController.$formatters.push(function (data) {
                        //convert data from model format to view format                        
                        if (data != null && data.indexOf('01/01/0001') >= 0)
                            return null;
                        var pattern = 'DD/MM/YYYY';
                        var output = "-";
                        if (data !== null && data !== undefined && data.length > 0) {
                            return moment(data, 'DD/MM/YYYY HH:mm:SS').format(pattern);
                        }

                        return data; //converted
                    });
                }
            }
        });

        return app;
    });
}());
