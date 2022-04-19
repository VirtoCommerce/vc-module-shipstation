angular.module('ShipStation')
    .controller('ShipStation.helloWorldController', ['$scope', 'ShipStation.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'ShipStation';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'ShipStation.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
