
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
        $http.post("api/Dictionary/GetRandomRange")
            .then(function (response) {
                if (response.data.length === 0) {
                    $scope.descriptions.push({
                        Word: "Nothing Here!",
                        PartOfSpeech: "There is nothing on the server",
                        Definition: "Try adding a character!"
                    });
                }
                for (var i = 0; i < response.data.length; i++) {
                    var definition = "";
                    for (var x = 0; x < response.data[i].Definition.length; i++) {
                        definition.append(response.data[i].Definition[x] + ", ");
                    }
                    definition = definition.substring(0, definition.length - 2);
                    $scope.descriptions.append({
                        Word: response.data[i].Word,
                        PartOfSpeech: response.data[i].PartOfSpeech,
                        Definition: definition
                    });
                }
            }, function(response) {
                $scope.descriptions.push({
                    Word: "An error occurred",
                    PartOfSpeech: response.status,
                    Definition: response.statusText
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