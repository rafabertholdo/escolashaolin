'use strict';

define(function () {

    var routeResolver = function () {        
        this.$get = function () {            
            return this;
        };

        this.routeConfig = function () {
            var viewsDirectory = '/app/views/',
                controllersDirectory = '/app/controllers/',
                viewSuffix = '',
                controllerSuffix = 'Controller',

            setBaseDirectories = function (viewsDir, controllersDir, viewSuff, controllerSuff) {
                viewsDirectory = viewsDir;
                controllersDirectory = controllersDir;
                viewSuffix = viewSuff,
                controllerSuff = controllerSuff
            },

            getViewsDirectory = function () {
                return viewsDirectory;
            },

            getControllersDirectory = function () {
                return controllersDirectory;
            },

            getViewSuffix = function () {
                return viewSuffix;
            },

            getControllerSuffix = function () {
                return controllerSuffix;
            };

            return {
                setBaseDirectories: setBaseDirectories,
                getControllersDirectory: getControllersDirectory,
                getViewsDirectory: getViewsDirectory,
                getViewSuffix: getViewSuffix,
                getControllerSuffix: getControllerSuffix
            };
        }();

        this.route = function (routeConfig) {

            var resolve = function (baseName, path, controllerName, secure) {
                if (!path) path = '';                
                var routeDef = {};
                routeDef.templateUrl = function (params) {                    
                    var suffix = routeConfig.getViewSuffix();
                    if (params.id)
                        suffix = "Detail" + suffix;

                    return routeConfig.getViewsDirectory() + params.controller + suffix +'.html';
                };

                routeDef.controller = ['$route', '$scope', '$controller', function ($route, $scope, $controller) {
                    var suffix = routeConfig.getControllerSuffix();
                    if ($route.current.params.id)
                        suffix = "Detail" + suffix;

                    $controller($route.current.params.controller + suffix, { $scope: $scope }); 
                }];
                routeDef.secure = (secure) ? secure : false;
                
                routeDef.resolve = {
                    load: ['$q', '$rootScope', '$route', function ($q, $rootScope, $route) {                        
                        var params = $route.current.params;

                        var suffix = routeConfig.getControllerSuffix();
                        if (params.id)
                            suffix = "Detail" + suffix;

                        var dependencies = [routeConfig.getControllersDirectory() + params.controller + suffix + '.js'];
                        return resolveDependencies($q, $rootScope, dependencies);
                    }]
                };

                return routeDef;
            },

            resolveDependencies = function ($q, $rootScope, dependencies) {
                var defer = $q.defer();                
                require(dependencies, function () {
                    defer.resolve();                    
                    $rootScope.$apply();
                });

                return defer.promise;
            };

            return {
                resolve: resolve                
            }
        }(this.routeConfig);

    };

    var servicesApp = angular.module('routeResolverServices', []);

    //Must be a provider since it will be injected into module.config()    
    servicesApp.provider('routeResolver', routeResolver);
});
