$(document).ready(function () {
    $("#getfb").on("click", function () {
    $("#showfb").empty();
    var user1 = $('#fb1').val();
    var user2 = $('#fb2').val();
    if (isNaN(user1) || isNaN(user2)) {
        $("#showfb").addClass("error");
        $("#showfb").text("Please enter two numbers!").show();
    }
    else if (user1 >= 100 || user1 <= 0 || user2 >= 100 || user2 <= 0) {
        $("#showfb").addClass("error");
        $("#showfb").text("Please enter two numbers between 1-100.").show();
    }
    else {       
        var numArr = [user1, user2];
        var fizz = numArr[0];
        var buzz = numArr[1];
        for (var i = 1; i <= 100; i++) {
            numArr[i] = parseInt(numArr[i]);
            var fizzRemain = i % fizz;
            var buzzRemain = i % buzz;
            if (fizzRemain == 0 && buzzRemain == 0) {

                $("#showfb").append("<span class='fizzbuzzdisplay'>" + 'FizzBuzz ' + "</span>").show();
            }
            else if (fizzRemain == 0) {
                $("#showfb").append("<span class='fizzdisplay'>" + 'Fizz ' + "</span>").show();
            }
            else if (buzzRemain == 0) {
                $("#showfb").append("<span class='buzzdisplay'>" + 'Buzz ' + "</span>").show();
            }

            else {
                $("#showfb").removeClass("error");
                $("#showfb").append("<span class='jsdisplay'>" + i + " " + "</span>").show();
            }

        }
    }
    });
$("#clearfb").on("click", function () {
    $("#showfb").empty();
    $("#fb1").val(" ");
    $("#fb2").val(" ");
});
$(".close").on("click", function () {
    $("#showfb").empty();
    $("#fb1").val(" ");
    $("#fb2").val(" ");
});
});
