
    'use strict';

    var app = angular.module('characterControllers', []);

    app.controller("AddCharacterController", function($scope, $http) {
        $scope.character = "";
        $scope.pronunciation = "";
        $scope.partsOfSpeech = [""];
        $scope.definitions = [""];
        $scope.usages = [""];
        $scope.result = "";
        $scope.submit = function (isValid) {
            if (!isValid) {
                $scope.result = "The form has some incorrect fields.";
                return;
            }
            var build = [];
            for (var i = 0; i < definitions.length; i++) {
                build.push({
                    key: $scope.definitions[i],
                    value: $scope.partsOfSpeech[i]
                });
            }
            $http.post("/api/Character/AddCharacter",
            {
                Logograph: $scope.character,
                Definitions: build,
                Pronunciation: $scope.pronunciation,
                Usages: $scope.usages
            }).then(function(response) {
                if (response.data === "Success!") {
                    $scope.result = "Success!";
                    $scope.character = "";
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
            $scope.partsOfSpeech.push("");
        }
        $scope.addUsage = function() {
            $scope.usages.push("");
        }
        $scope.removeDefinition = function() {
            if ($scope.definitions.length !== 1) {
                $scope.definitions.pop();
                $scope.partsOfSpeech.pop();
            }
        }
        $scope.removeUsage = function() {
            if ($scope.usages.length !== 1) {
                $scope.definitions.pop();
                $scope.partsOfSpeech.pop();
            }
        }
    });