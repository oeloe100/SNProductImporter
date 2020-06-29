var inputFields = $("input[name='cat-value']");
var shopMainCategory = $(".select-shop-category");

$("#create-link").click(function () {
    for (var i = 0; i < inputFields.length; i++) {
        if (inputFields[i].value != undefined &&
            inputFields[i].value != "") {

            console.log(selectedHTML[i]);

            ClearLinkBoxInput(inputFields[i]);
        }
    }
    //After implementing Ajax. Run Method after success
    ResetProgressBar();
});

function ClearLinkBoxInput(inputField) {
    inputField.value = "";
}

function ResetProgressBar() {
    $(".progress-bar").css("width", "0px");
    $(".progress-bar").text("");
}