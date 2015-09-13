(function() {
    var app = angular.module("dictionary", []);

    app.controller("ListController", [
        '$scope', '$http', function($scope, $http) {
            $scope.phrases = [];
            $http({
                url: window.location + '/api/Dictionary/GetRandom'
            });
        }
    ]);
})