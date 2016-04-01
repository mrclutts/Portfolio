$(document).ready(function () {
    $("#showfib").on("click", function () {
        var userInput = parseInt($("#fib1").val());
        if (isNaN(userInput)){
            $("#printfib").text("Please enter a number!").show();
            $("#printfib").addClass("error");
        }
        else if (userInput <= 0) {
            $("#printfib").text("Please enter a positive number!").show();
            $("#printfib").addClass("error");
        }
        else{
            var a = 1;
            var b = 0;
            var c = a + b;
            var arr = [];
            for (i = 1; c < userInput;) {
                c = a + b;
                if (c >= userInput) {
                    break;
                }
                arr.push(c);
                $("#printfib").removeClass("error");
                $("#printfib").text(arr.join(" ")).show();
                $()
                a = b;
                b = c;
            }
        }
    });
    $("#clearfib").on("click", function () {
        $("#printfib").hide();
        $("#fib1").val(" ");
    });
    $(".close").on("click", function () {
        $("#printfib").hide();
        $("#fib1").val(" ");
    });
});


