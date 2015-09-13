(function() {
    var app = angular.module("dictionary", []);

    app.controller("ListController", [
        '$scope', '$http', function($scope, $http) {
            $scope.descriptions = [];
            $http.post(window.domain + '/api/Dictionary/GetRandomRange')
                .then(function(response) {
                    for (var i = 0; i < response.data.length; i++) {
                        var definition = "";
                        for (var x = 0; x < response.data[i].Definition.length; i++) {
                            definition.append(response.data[i].Definition[x] + ', ');
                        }
                        definition = definition.substring(0, definition.length - 2);
                        $scope.descriptions.append({
                            Word: response.data[i].Word,
                            PartOfSpeech: response.data[i].PartOfSpeech,
                            Definition: definition
                    });
                    }
                }, function(response) {
                    $scope.descriptions.append({
                        Word: "An error occurred",
                        PartOfSpeech: response.status,
                        Definition: response.statusText
                    });
                });
        }
    ]);
})