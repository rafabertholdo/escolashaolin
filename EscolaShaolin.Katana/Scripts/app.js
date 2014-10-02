(function () {
    // For any third party dependencies, like jQuery, place them in the lib folder.

    // Configure loading modules from the lib directory,
    // except for 'app' ones, which are in a sibling
    // directory.
    requirejs.config({
        baseUrl: '../scripts',
        paths: {
            app: '../app',
            'jquery': 'jquery-2.1.1.min'
        },
        shim: {
            'jquery-mask': {
                deps: ['jquery'],
            }
        }

    });

    // Start loading the main app file. Put all of
    // your application logic in there.
    requirejs([
        'app/main',
        'app/services/routeResolver'],
        function () {
            angular.bootstrap(document, ['myApp']);
    });
}());
