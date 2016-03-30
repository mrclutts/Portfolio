$(document).ready(function () {
    $("#getprime").on("click", function () {
        var userInput = parseInt($("#prime1").val());
        console.log(userInput);

        if (isNaN(userInput)) {
            $("#printprime").addClass("error");
            $("#printprime").text("Please enter a number!").show();
        }
        else if (userInput <= 0) {
            $("#printprime").addClass("error");
            $("#printprime").text("Please enter a positive number great than 1.").show();
        }
        else {
            var arr = [];
            for (a = 2; a <= userInput; a++) {
                var prime = true;

                for (b = 2; b < a; b++) {
                    if (a % b === 0) {
                        prime = false;
                    }
                }
                if (prime) {
                    arr.push(a);
                }
            }
            $("#printprime").removeClass("error");
            $("#printprime").addClass("jsdisplay");
            $("#printprime").append(arr.join(" ")).show();
        }
    });
    $("#clearprime").on("click", function () {
        $("#printprime").empty();
        $("#prime1").val(" ");

    });
    $(".close").on("click", function () {
        $("#printprime").empty();
        $("#prime1").val(" ");

    });
});
