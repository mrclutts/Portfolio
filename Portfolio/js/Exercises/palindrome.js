$(document).ready(function () {
    $("#checkpali").on("click", function(){
        var userInput = $('#pali1').val();
        if (userInput == "") {
            $("#printpali").addClass("error");
            $("#printpali").text("Please enter a word, phrase, or number.").show();
        }
        else if (userInput == " ") {
            $("#printpali").addClass("error");
            $("#printpali").text("Please enter a word, phrase, or number.").show();
        }
        else {
            var str = userInput.toUpperCase().replace(/\s+/g, '');
            var strRev = str.split('').reverse().join('');
                if (str == strRev) {
                    $("#printpali").removeClass("error");
                    $("#printpali").addClass("jsdisplay");
                    $("#printpali").text("Yes, ").show();
                    $("#printpali").append("<span class='userinputdisplay'>" + userInput + "</span>");
                    $("#printpali").append(" is a Palindrome!").show();
                }
                else {
                    $("#printpali").removeClass("error");
                    $("#printpali").addClass("jsdisplay");
                    $("#printpali").text("No, ").show();
                    $("#printpali").append("<span class='error'>" + userInput + "</span>");
                    $("#printpali").append(" is not a Palindrome!").show();
                }
        }
    });
    $("#clearpali").on("click", function () {
        $("#printpali").hide();
        $("#pali1").val(" ");
    });
    $(".close").on("click", function () {
        $("#printpali").hide();
        $("#pali1").val(" ");
    });
});
