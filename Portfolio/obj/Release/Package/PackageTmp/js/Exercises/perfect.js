$(document).ready(function(){
    $("#checkper").on("click", function(){
        var userInput = parseInt($("#per1").val());
        var arry = [];
        if (userInput < 0) {
            $("#showper").addClass("error");
            $("#showper").text("Pefect numbers cannot be negative.  Please enter a positive number.").show();
        }
        else if (userInput === 0) {
            $("#showper").addClass("error");
            $("#showper").text("No, 0 is not a Perfect number.").show();
        }
        else if (userInput === 1) {
            $("#showper").addClass("error");
            $("#showper").text("No, 1 is not a Perfect number.").show();
        }
        else if (isNaN(userInput)) {
            $("#showper").addClass("error");
            $("#showper").text("Please enter a number.").show();
        }
        else {
            for (var i = 1; i < userInput; i++) {
                if (userInput % i === 0) {
                    arry.push(i);
                }
			
                var arrSum = 0;
                for (var j=0, len=arry.length; j < len; j++) {
                    arrSum += arry[j];
                }
                if (arrSum === userInput) {
                    $("#showper").removeClass("error");
                    $("#showper").addClass("jsdisplay");
                    $("#showper").text("Yes, ").show();
                    $("#showper").append("<span class='userinputdisplay'>" + userInput + "</span>");
                    $("#showper").append(" is a Perfect number!").show();
                    
                }
                else {
                    $("#showper").removeClass("error");
                    $("#showper").addClass("jsdisplay");
                    $("#showper").text("No, ").show();
                    $("#showper").append("<span class='error'>" + userInput + "</span>");
                    $("#showper").append(" is a not a Perfect number!").show();
                }
					
            }	
			
        }
    });
    $("#clearper").on("click", function () {
        $("#showper").hide();
        $("#per1").val(" ");
    });
    $(".close").on("click", function () {
        $("#showper").hide();
        $("#per1").val(" ");
    });
   
$("#printper").on("click", function () {
   
    $(this).attr("disabled", "disabled");
        for (var i = 1; i < 10000; i++) {
            var arry = [];
            for (var j = 1; j < i; j++) {
                if (i % j === 0) {
                    arry.push(j);
                }
                var arrSum = 0;
                for (var k=0; k < arry.length; k++) {
                    arrSum += arry[k];		
                }		
            }
            if (arrSum === i) {
                $("#displayper").append(i + " ");
            }
        }
});

});