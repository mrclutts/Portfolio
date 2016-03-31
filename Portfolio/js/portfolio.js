$(document).ready(function () {
    
    $('.portfolioHolder').children('div:not(.jQuery)').hide();
    $('#filterOptions li a').click(function () {
        var activeClass = $(this).attr('class');
        $('#filterOptions li').removeClass('active');
        $(this).parent().addClass('active');

        if (activeClass == 'allWork') {
            $(".portfolioHolder").children(".portfolio-item").hide();
            $('.portfolioHolder').children(".portfolio-item").each(function(index) {
                $(this).delay(100*index).fadeIn(2000);
            });
            }
        else {
            $(".portfolioHolder").children(".portfolio-item").hide();
            $('.portfolioHolder').children('div:not(.' + activeClass + ')').hide();
            $('.portfolioHolder').children('div.' + activeClass).each(function(index) {
                $(this).delay(100*index).fadeIn(1500);
            });
        }
        return false;
    });
});