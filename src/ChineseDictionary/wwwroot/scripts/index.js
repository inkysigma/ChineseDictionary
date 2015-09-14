
"use strict";

var SearchType = {
    Definition: 0,
    Word: 1
}

var app = angular.module("DictionaryModule", ["ngRoute", "characterControllers"]);

app.controller("ListController",
    function($scope, $http) {
        $scope.descriptions = [];
        $scope.search = "";
        $scope.searchBy =
            $http.post("api/Dictionary/GetLatestRange")
            .then(function(response) {
                if (response.data.length === 0) {

                    $scope.descriptions.push({
                        Word: "Nothing Here!",
                        Definitions: {
                            "Try adding a character!":"There is nothing on the server"
                        }
                    });
                }
                for (var i = 0; i < response.data.length; i++) {
                    $scope.descriptions.push({
                        Word: response.data[i].Word,
                        Definitions: response.data[i].Definitions
                    });
                }
            }, function(response) {
                var definitions = new Array();
                definitions[response.statusText] = response.status;
                $scope.descriptions.push({
                    Word: "An error occurred",
                    Definitions: definitions
                });
            });
    });



app.config([
    "$routeProvider", "$locationProvider", function(provider, location) {
        provider.when("/", {
            templateUrl: "List.html",
            controller: "ListController"
        });
        provider.when("/AddCharacter",
        {
            templateUrl: "Characters/AddCharacter.html",
            controller: "AddCharacterController"
        });

        location.html5Mode(true);
    }
]);