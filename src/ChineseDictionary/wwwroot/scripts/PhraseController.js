var app = app.module("PhraseModule", []);
app.controller("AddPhraseController", function($scope) {
    $scope.Phrases = "";
    $scope.character = "";
    $scope.pronunciation = "";
    $scope.partsOfSpeech = [""];
    $scope.definitions = [""];
    $scope.usages = [""];
    $scope.result = "";
});