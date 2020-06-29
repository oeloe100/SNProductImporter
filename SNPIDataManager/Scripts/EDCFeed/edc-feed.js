var progress = 0;
var progressToString = "";
var selectedHTML = new Array();

$(document).ready(function () {
    SelectedCategory("shop");
    SelectedCategory("supplier");
});

function SelectedCategory(margent) {
    var selectedClass = document.getElementsByClassName("select-" + margent + "-category");

    for (var i = 0; i < selectedClass.length; i++) {
        selectedClass[i].addEventListener("click", function () {
            var value = this.innerText;
            var selectedBoxChild = $("." + margent + "-box-selected").children();

            $("." + margent + "-box-selected").children()[0].value = value;
            selectedHTML.push($(this).children()[0]);

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