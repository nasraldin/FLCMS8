(function($) {
    $(function() {
        // ReSharper disable once InconsistentNaming
        new WOW().init();

        $(".menu-sidenav-btn")
            .sideNav({
                    menuWidth: 300,
                    edge: "right",
                    closeOnClick: true,
                    draggable: true
                }
            );

        $(".button-collapse").sideNav();
        $(".modal")
            .modal({
                    dismissible: true,
                    opacity: .5,
                    in_duration: 300,
                    out_duration: 200,
                    starting_top: "4%",
                    ending_top: "10%"
                }
            );

        var options = [
            {
                selector: ".social",
                offset: 400,
                callback: function(el) {
                    Materialize.showStaggeredList($(el));
                }
            }
        ];
        Materialize.scrollFire(options);

        $(".primary-nav-inner .brand-logo").addClass("animated slideInDown");
        $("img").addClass("responsive-img");

        $(".primary-nav-inner ul li a")
            .hover(function() {
                $(".primary-nav-inner ul li a").addClass("hvr-underline-from-center");
            });
        $("#nav_multy_dropdown ul li a")
            .hover(function() {
                $("#nav_multy_dropdown ul li a").addClass("hvr-sweep-to-right");
            });

        //back to top page scroll Trigger
        if ($("#back-to-top").length) {
            var scrollTrigger = 100, // px
                backToTop = function() {
                    var scrollTop = $(window).scrollTop();
                    if (scrollTop > scrollTrigger) {
                        $("#back-to-top").addClass("active");
                    } else {
                        $("#back-to-top").removeClass("active");
                    }
                };
            backToTop();
            $(window)
                .on("scroll",
                    function() {
                        backToTop();
                    });
            $("#back-to-top")
                .on("click",
                    function(e) {
                        e.preventDefault();
                        $("html, body")
                            .animate({
                                    scrollTop: 0
                                },
                                700);
                    });
        }
    });
    // end of document ready
})(jQuery);
// end of jQuery name space