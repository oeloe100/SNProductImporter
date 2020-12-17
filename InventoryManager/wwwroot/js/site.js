$(document).ready(function () {
    var currentPathName = window.location.pathname;
    AutoLoad(currentPathName);
});

function AutoLoad(path) {
    if (path != null && path === "/") {
        $.ajax({
            type: "GET",
            url: "/NopAuthorization/Index",
            traditional: true
        }).done(function (result) {
            $(".main").html(result);
        });
    }
}
