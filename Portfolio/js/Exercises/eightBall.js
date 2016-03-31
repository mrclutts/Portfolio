var myApp = angular.module("myModule", []);

myApp.controller("myController", function ($scope, $timeout) {
    $scope.magic = function () {
        var predict = Math.floor(Math.random() * 9);
        $scope.result = "";
        switch (predict) {
            case 0:
                result = "I'm sorry, I was out getting tacos.  Ask me again after I get done eating.";
                break;
            case 1:
                result = "My sources say.....YES!";
                break;
            case 2:
                result = "My sources say.....NO!";
                break;
            case 3:
                result = "Would you ask your mother that question?";
                break;
            case 4:
                result = "The answer to your question is a firm maybe.";
                break;
            case 5:
                result = "I've thought abour your query and I have concluded that the answer is YES.";
                break;
            case 6:
                result = "What's with all the quetsions?";
                break;
            case 7:
                result = "Does a bear crap in the woods?";
                break;
            case 8:
                result = "Can pigs fly?";
                break;
        }
        $scope.result = result;
    }
    $scope.setDelay = function () {
        $scope.delay = true;
        $timeout(function () {
            $scope.delay = false;
        }, 1500);
    }
});