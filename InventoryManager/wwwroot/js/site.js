$(document).ready(function () {
    AddLoadingIndicator();

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
            RemoveLoadingIndicator();
            $(".main").append(result);
        });
    }
}

function AddLoadingIndicator() {
    $(".main").append('<i class="fa fa-spinner fa-spin fa-3x">');
}

function RemoveLoadingIndicator() {
    $(".fa-spinner").remove();
}
