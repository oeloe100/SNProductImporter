var inputFields = $("input[name='cat-value']");
var shopMainCategory = $(".select-shop-category");

$("#create-link").click(function () {
    var proceed = false;

    for (var i = 0; i < inputFields.length; i++) {
        if (inputFields[i].value != undefined &&
            inputFields[i].value != "") {

            ValidateLinks();
            ClearLinkBoxInput(inputFields[i]);
        }
    }

    if (proceed) {
        PostSelectedMapping();
    }

    ResetProgressBar();
});

function ValidateLinks() {
    if (inputFields[i].value == $(selectedCategories[selectedCategories.length - 2]).text() ||
        inputFields[i].value == $(selectedCategories[selectedCategories.length - 1]).text()) {

        proceed = true;
    }
    else {
        alert("Selected Categories Do NOT Match!");
        proceed = false;
    }
}

function PostSelectedMapping() {
    console.log(selectedCategories[selectedCategories.length - 2]);
    console.log(selectedCategories[selectedCategories.length - 1]);

    selectedCategories = [];
    proceed = false;
}

function ClearLinkBoxInput(inputField) {
    inputField.value = "";
}

function ResetProgressBar() {
    $(".progress-bar").css("width", "0px");
    $(".progress-bar").text("");
}