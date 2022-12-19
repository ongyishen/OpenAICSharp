var app = angular.module("myApp", [
    "ngSanitize",
    "ngStorage",
    "ngTouch",
    "ngAria"
]);

app.factory("$serviceBase", function ($q, $http, $filter) {
    var service = {};
    return service;
});