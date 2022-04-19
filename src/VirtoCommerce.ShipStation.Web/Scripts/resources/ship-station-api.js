angular.module('ShipStation')
    .factory('ShipStation.webApi', ['$resource', function ($resource) {
        return $resource('api/ShipStation');
}]);
