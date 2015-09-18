"use strict";

var SearchType = {
    Definition: 0,
    Word: 1
}

/// <reference path="" />
var app = angular.module("DictionaryModule", ["ngRoute", "CharacterControllers"]);

app.controller("ListController",
    function($scope, $http) {
        $scope.descriptions = [];
        $scope.search = "";
        $http.post("api/Dictionary/GetLatestRange")
            .then(function(response) {
                if (response.data.length === 0) {

                    $scope.descriptions.push({
                        Word: "Nothing Here!",
                        Definitions: [
                            {
                                Definition: "Try adding a character!",
                                PartOfSpeech: "There is nothing on the server"
                            }
                        ]
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
        provider.when("/Characters/AddCharacter",
        {
            templateUrl: "Characters/AddCharacter.html",
            controller: "AddCharacterController"
        });
        provider.when("/Characters/ReviewCharacter/:char?",
        {
            templateUrl: "Characters/ReviewCharacter.html",
            controller: "ReviewCharacterController"
        });
        provider.when("/Phrases/AddPhrase", {
            templateUrl: "Phrases/AddPhrase.html",
            controller: "AddPhraseController"
        });


        location.html5Mode(true);
    }
]);