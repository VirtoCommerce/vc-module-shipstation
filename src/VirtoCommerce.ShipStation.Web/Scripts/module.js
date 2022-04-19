// Call this to register your module to main application
var moduleName = 'ShipStation';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $stateProvider
                .state('workspace.ShipStationState', {
                    url: '/ShipStation',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        '$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'ShipStation.helloWorldController',
                                template: 'Modules/$(VirtoCommerce.ShipStation)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', 'platformWebApp.widgetService', '$state',
        function (mainMenuService, widgetService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/ShipStation',
                icon: 'fa fa-cube',
                title: 'ShipStation',
                priority: 100,
                action: function () { $state.go('workspace.ShipStationState'); },
                permission: 'ShipStation:access'
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
