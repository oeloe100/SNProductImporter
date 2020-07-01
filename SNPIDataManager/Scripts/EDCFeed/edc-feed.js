var progress = 0;
var progressToString = "";

var selectedCategories = new Array();

$(document).ready(function () {
    MappingMechanism("shop");
    MappingMechanism("supplier");
});

function MappingMechanism(vendor) {
    var selectedClass = document.getElementsByClassName("select-" + vendor + "-category");

    for (var i = 0; i < selectedClass.length; i++) {
        selectedClass[i].addEventListener("click", function () {
            var value = this.innerText;
            $("." + vendor + "-box-selected").children()[0].value = value;
            $(this).children().attr("vendor", vendor);

            ManageProgress();
        });
    }
}

function ManageProgress() {
    if ($(".progress-bar").css("width") == "0px") {
        SetProgress("50%");
    }
    else if ($(".progress-bar").css("width") == "105px") {
        SetProgress("100%");
    }
}

function SetProgress(value) {
    $(".progress-bar").css("width", "" + value + "");
    $(".progress-bar").text("" + value + "");
}