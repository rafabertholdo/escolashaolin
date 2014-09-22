(function () {
    var dependencies = [        
        'app/services/routeResolver'
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
                 .when('/main', route.resolve('menu', '', 'menu'))
                 .when('/alunos', route.resolve('list', 'aluno/', 'alunoList'))
                 .when('/alunos/new', route.resolve('new', 'aluno/', 'alunoNew'))
                 .when('/alunos/:id', route.resolve('details', 'aluno/', 'alunoDetails'))
                 //.when('/customerorders/:customerId', route.resolve('CustomerOrders', 'customers/'))
                 //.when('/customeredit/:customerId', route.resolve('CustomerEdit', 'customers/', true))
                 //.when('/orders', route.resolve('Orders', 'orders/'))
                 //.when('/about', route.resolve('About'))
                 //.when('/login/:redirect*?', route.resolve('Login'))
                 .otherwise({ redirectTo: '/main' });

         }]);
        return app;
    });
}());
