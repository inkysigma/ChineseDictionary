
    'use strict';

    var app = angular.module('characterControllers', []);
    app.controller("AddCharacterController", function($scope, $http) {
        $scope.character = "";
        $scope.partOfSpeech = "";
        $scope.pronunciation = "";
        $scope.definitions = [""];
        $scope.usages = [""];
        $scope.result = "";
        $scope.submit = function() {
            $http.post("/api/Character/AddCharacter",
            {
                Logograph: $scope.character,
                Definitions: $scope.definitions,
                PartOfSpeech: $scope.partOfSpeech,
                Pronunciation: $scope.pronunciation,
                Usages: $scope.usages
            }).then(function(response) {
                if (response.data === "true") {
                    $scope.result = "Success!";
                    $scope.character = "";
                    $scope.partOfSpeech = "";
                    $scope.pronunciation = "";
                    $scope.definitions = [""];
                    $scope.usages = [""];
                } else {
                    $scope.result = "There is an error in the information provided. Is it incomplete";
                }
            }, function(response) {
                $scope.result = response.status + " - " + response.statusText;
            });
        }
        $scope.addDefinition = function() {
            $scope.definitions.push("");
        }
        $scope.addUsage = function() {
            $scope.usages.push("");
        }
    });