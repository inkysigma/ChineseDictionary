
'use strict';

var app = angular.module('characterControllers', []);

app.controller("AddCharacterController", function ($scope) {
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
        for (var i = 0; i < $scope.definitions.length; i++) {
            build.push({
                Definition: $scope.definitions[i],
                PartOfSpeech: $scope.partsOfSpeech[i]
            });
        }
        var usageBuilt = [];
        for (var i = 0; i < $scope.usages.length; i++) {
            usageBuilt.push({
                Sentence: $scope.usages[i]
            });
        }
        $.ajax({
            url: "/api/Character/AddCharacter",
            method: "post",
            timeout: 60000,
            data: {
                Logograph: $scope.character,
                Pronunciation: $scope.pronunciation,
                Definitions: build,
                Usages: $scope.usages
            },
            success: function (data) {
                $scope.result = data;
                if (data === "Success") {
                    $scope.character = "";
                    $scope.pronunciation = "";
                    $scope.partsOfSpeech = [""];
                    $scope.definitions = [""];
                    $scope.usages = [""];
                }
                $scope.$digest();
            },
            error: function (jqXhr, status) {
                $scope.result = status;
            }
        });
    }
    $scope.addDefinition = function () {
        $scope.definitions.push("");
        $scope.partsOfSpeech.push("");
    }
    $scope.addUsage = function () {
        $scope.usages.push("");
    }
    $scope.removeDefinition = function () {
        if ($scope.definitions.length !== 1) {
            $scope.definitions.pop();
            $scope.partsOfSpeech.pop();
        }
    }
    $scope.removeUsage = function () {
        if ($scope.usages.length !== 1) {
            $scope.definitions.pop();
            $scope.partsOfSpeech.pop();
        }
    }
});

app.controller("ReviewCharacterController", function($scope, $http) {
    $scope.character = {
        Logograph: "",
        Pronunciation: "",
        Definitions: [],
        Usages: [],
        Phrases: [],
        Idioms: []
    }
    $http.post("api/Character/GetRandom").then(function(response) {
        $scope.character = response.data;
    }, function(response) {
        $scope.Logograph = "Error";
        $scope.Pronunciation = response.status + " - " + response.statusText;
    });
});