(function($) {
    $(function() {
        setTimeout(function() {
                $("body").addClass("loaded");
                $("#loader-wrapper img").remove();
            },
            1000);
        setTimeout(function() {
                $("#modal_popup").modal("open");
            },
            10000);
        $(".slider").slider({ full_width: true });
        $(".carousel").carousel();
        $(".scrollspy")
            .scrollSpy({
                scrollOffset: 60
            });
        $(".parallax").parallax();
        $("select").material_select();

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
        $(".dropdown_btn_lang")
            .dropdown({
                belowOrigin: true
            });
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
        var portfolioFilterList = {
            init: function() {
                $("#portfoliolist")
                    .mixItUp({
                        selectors: {
                            target: ".portfolio",
                            filter: ".filter"
                        },
                        load: {
                            filter: ".logo, .branding, .web, .media, .events, .digital, .execution, .gift"
                        }
                    });
            }
        };
        portfolioFilterList.init();

        var options = [
            {
                selector: "#projects",
                offset: 400,
                callback: function(el) {
                    Materialize.showStaggeredList($(el));
                }
            }, {
                selector: ".social",
                offset: 400,
                callback: function(el) {
                    Materialize.showStaggeredList($(el));
                }
            }, {
                selector: ".services",
                offset: 400,
                callback: function(el) {
                    Materialize.showStaggeredList($(el));
                }
            }, {
                selector: ".profile-img",
                offset: 500,
                callback: function(el) {
                    Materialize.fadeInImage($(el));
                }
            }
        ];
        Materialize.scrollFire(options);

        $(".primary-nav-inner .brand-logo").addClass("animated slideInDown");
        $("img").addClass("responsive-img");
        $(".portfolio").addClass("hvr-grow");
        $("#clints .item").addClass("hvr-wobble-vertical");
        $("#clints .item").addClass("hvr-wobble-vertical");

        $(".primary-nav-inner ul li a")
            .hover(function() {
                $(".primary-nav-inner ul li a").addClass("hvr-underline-from-center");
            });
        $("#nav_multy_dropdown ul li a")
            .hover(function() {
                $("#nav_multy_dropdown ul li a").addClass("hvr-sweep-to-right");
            });

        $(".social-sidebar .social ul li a")
            .hover(function() {
                $(this).addClass("");
            });

        $(".box-hover")
            .hover(function() {
                    $(this).addClass("flip");
                },
                function() {
                    $(this).removeClass("flip");
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

        $("#googlemap .map-overlay")
            .on("click",
                function() {
                    $(this).removeAttr("style");
                });

        $("#googlemap .map-overlay")
            .click(function() {
                $("#map").css("z-index", "1");
            });

        $("#googlemap")
            .mouseleave(function() {
                $("#map").css("z-index", "0");
            });
    });
    // end of document ready
})(jQuery);
// end of jQuery name space