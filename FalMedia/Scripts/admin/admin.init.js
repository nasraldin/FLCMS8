(function() {
    $("div.material-table table tbody tr")
        .hover(function() {
                $(".linkspost > span").addClass("showHov");
            },
            function() {
                $(".linkspost > span").removeClass("showHov");
            });
})(jQuery);