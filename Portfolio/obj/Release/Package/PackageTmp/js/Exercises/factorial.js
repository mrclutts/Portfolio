$(document).ready(function(){
    $("#facbtn").on("click", function(){
        var userInput = parseInt($("#fac1").val());
        if (isNaN(userInput)) {
            $("#showfac").addClass("error");
            $("#showfac").text("Please enter a number!").show();
            
        }
        else if (userInput <= 0) {
            $("#showfac").addClass("error");
            $("#showfac").text("Please enter a positive number!").show();
        }
        else {
            for (var i = 1,fac = 1; i <= userInput; i++) {
                fac = fac * i;
                $("#showfac").removeClass("error");
                $("#showfac").addClass("jsdisplay");
                $("#showfac").text("The factorial of ");
                $("#showfac").append("<span class='fizzbuzzdisplay'>" + userInput + "</span>").show();
                $("#showfac").append(" is: ")
                $("#showfac").append("<span class='userinputdisplay'>" + fac+ "</span>").show(); 
                
            }
	    }
    });
    $("#clearfac").on("click", function() {
        $("#showfac").hide();
        $("#fac1").val(" ");

    });
    $(".close").on("click", function () {
        $("#showfac").hide();
        $("#fac1").val(" ");

    });
});

