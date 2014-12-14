(function () {
    define(['app/main','jquery'], function (app,$) {
        app.register.factory('navigationController', function () {
            var self = this;

            var viewStack = [];
            var currentView = null;

            var result = {};

            result.pushView = function(options) {
                
                var settings = {                    
                    'viewName': null,
                    'dataFromServer': null,
                    'viewPushedCallback': null,
                    'animated': false,
                    'prepareForPushCallback': null
                };

                if (options) {
                    $.extend(settings, options);
                }

                var url = "";
                var module = null;
                //Se o projeto que eu estou logado for diferente do projeto que eu estou mandado carregar, quer dizer
                //que eu vou buscar do projeto principal, e não do módulo
                var urlBase = '../';
                url = urlBase + "Views/" + settings.viewName + ".js";
                module = urlBase + "Controlllers/" + settings.viewName + ".js";
                

                require([module], function (moduleClass) {
                    var modulueInstance = new moduleClass();

                    if (_.isFunction(settings.prepareForPushCallback)) {
                        settings.prepareForPushCallback(modulueInstance);
                    }

                    if (_.isFunction(modulueInstance.viewDidLoad)) {
                        modulueInstance.viewDidLoad(settings.dataFromServer, self);
                    }


                    $.ajax({
                        url: url,
                        dataType: 'html',
                        type: 'GET',
                        async: false,
                        success: function (data, textStatus, jqXHR) {

                            var objectToPush = {
                                script: modulueInstance,
                                html: data
                            };

                            self.stack.push(objectToPush);

                            self.presentView(objectToPush, settings.animated);

                            if (_.isFunction(settings.viewPushedCallback))
                                settings.viewPushedCallback(objectToPush);
                        }
                    });
                });

            };
            

            this.fireViewWillDesapear = function () {
                if (self.currentView && _.isFunction(self.currentView.script.viewWillDisappear)) {
                    self.currentView.script.viewWillDisappear(self.currentView.html);
                }
            }

            this.popView = function (animated) {
                animated = false;
                self.fireViewWillDesapear();
                var viewToHide = self.stack.pop();
                if (this.stack.length > 0) {
                    var poppedView = this.stack[this.stack.length - 1];
                    self.currentView = poppedView;
                } else {
                    self.currentView = null;
                }

                if (self.currentView && _.isFunction(self.currentView.script.viewWillAppear)) {
                    self.currentView.script.viewWillAppear(self.currentView.html);
                }

                var childrenNodes = $("#" + self.viewContainerId)[0].children;
                var elementToRemove = $(childrenNodes[childrenNodes.length - 1]);
                // "!= false" porque o default é que seja animado e somente não sera animado se passar false
                if (animated != false) {
                    elementToRemove.transition({
                        'margin-left': '100%',
                        'width': '0px'
                    }, 500, 'snap', function () {
                        ko.cleanNode(elementToRemove.get(0));
                        elementToRemove.remove();

                        if (self.currentView && _.isFunction(self.currentView.script.viewDidAppear)) {
                            self.currentView.script.viewDidAppear(self.currentView.html);
                        }
                    });
                } else {
                    ko.cleanNode(elementToRemove.get(0));
                    elementToRemove.remove();

                    if (self.currentView && _.isFunction(self.currentView.script.viewDidAppear)) {
                        self.currentView.script.viewDidAppear(self.currentView.html);
                    }
                }
            }


            this.getTotalHeight = function () {
                return $("#" + self.viewContainerId).height();
            }
            this.popToRootView = function (animated) {
                animated = false;
                self.fireViewWillDesapear();
                var view = self.stack[0];
                this.stack = new Array();
                this.stack[0] = view;
                self.currentView = view;

                if (self.currentView && _.isFunction(self.currentView.script.viewWillAppear)) {
                    self.currentView.script.viewWillAppear(self.currentView.html);
                }

                var childrenNodes = $("#" + self.viewContainerId)[0].children;
                // "!= false" porque o default é que seja animado e somente não sera animado se passar false
                if (animated != false) {
                    var elementToRemove = $(childrenNodes[childrenNodes.length - 1]);
                    var currentMarginTop = parseInt(elementToRemove.css("margin-top").replace("px", ""));
                    for (var i = childrenNodes.length - 2; i > 0; i--) {
                        var childrenNode = $(childrenNodes[i]);
                        ko.cleanNode(childrenNode.get(0));
                        currentMarginTop -= childrenNode.height();
                        childrenNode.remove();

                        elementToRemove.css({
                            'margin-top': '-' + currentMarginTop + 'px'
                        });
                    }

                    elementToRemove.transition({
                        'margin-left': '100%',
                        'width': '0px'
                    }, function () {
                        ko.cleanNode(elementToRemove.get(0));
                        elementToRemove.remove();

                        if (self.currentView && _.isFunction(self.currentView.script.viewDidAppear)) {
                            self.currentView.script.viewDidAppear(self.currentView.html);
                        }
                    });
                }
                else {
                    for (var i = childrenNodes.length - 1; i > 0; i--) {
                        var elementToRemove = $(childrenNodes[i]);
                        ko.cleanNode(elementToRemove.get(0));
                        elementToRemove.remove();

                        if (self.currentView && _.isFunction(self.currentView.script.viewDidAppear)) {
                            self.currentView.script.viewDidAppear(self.currentView.html);
                        }
                    }
                }

            }

            this.popAllViews = function (animated) {
                animated = false;
                self.fireViewWillDesapear();
                this.stack = new Array();
                self.currentView = null;

                var childrenNodes = $("#" + self.viewContainerId)[0].children;
                for (var i = childrenNodes.length - 1; i >= 0; i--) {
                    var elementToRemove = $(childrenNodes[i]);
                    ko.cleanNode(elementToRemove.get(0));
                    elementToRemove.remove();
                }
            }

            this.presentView = function (view, animated) {
                animated = false;
                //Chama evento que view corrente vai desaparecer
                var viewToHide = self.currentView;
                if (viewToHide) {
                    if (_.isFunction(viewToHide.script.viewWillDisappear))
                        viewToHide.script.viewWillDisappear();
                }

                //Chama evento que a proxima view vai aparecer
                if (view && _.isFunction(view.script.viewWillAppear)) {
                    view.script.viewWillAppear(view.html);
                }

                var viewContainer = $("#" + self.viewContainerId);

                if (view) {
                    var indexOfView = self.stack.indexOf(view);
                    var slidingView = $("<div class='" + ((indexOfView > 0) && (animated != false) ? "slidingView" : "rootView") + (self.fullWidth ? "Full" : "") + "' style='z-index: " + (indexOfView + 1) + "'></div>");
                    if ((viewToHide) && (viewToHide.container)) {
                        slidingView.css({
                            'margin-top': '-' + self.getTotalHeight() + 'px'
                        });
                    }

                    slidingView.html(view.html)
                    view.container = slidingView;
                    viewContainer.append(slidingView);
                    var originalSlidingViewHeight = slidingView.height();
                    if (((viewToHide) && (viewToHide.container)) && (slidingView.height() < viewToHide.container.height())) {
                        slidingView.css({
                            'height': viewToHide.container.height() + 'px'
                        });
                    }
                    ko.applyBindings(view.script.viewModel, slidingView.get(0));
                }
                else {
                    viewContainer.html("");
                }
                self.currentView = view;

                if (view && _.isFunction(view.script.viewDidAppear)) {
                    view.script.viewDidAppear(view.html);
                }
            }

            return result; // returning this is very important
        });
    });
}());

