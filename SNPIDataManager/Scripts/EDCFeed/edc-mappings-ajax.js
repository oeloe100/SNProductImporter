var inputFields = $("input[name='cat-value']");
var shopMainCategory = $(".select-shop-category");

var inputArray = new Array();

$("#create-link").click(function () {
    for (var i = 0; i < inputFields.length; i++) {
        if (inputFields[i].value != undefined &&
            inputFields[i].value != "") {
            inputArray.push(inputFields[i].value);
        }
    }

    PostData(inputArray);
    ClearLinkBoxInput();
    ResetProgressBar();
});

function PostData(dataToPost) {
    var postData = { id: "testId", name: "name" };

    $.ajax({
        url: '/Mapping/CreateMapping',
        data: JSON.stringify(postData),
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        success: function (result) {
            console.log(result);
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText);
        }
    });
}

function ClearLinkBoxInput() {
    $(inputFields).each(function (index) {
        inputFields[index].value = "";
    });
}

function ResetProgressBar() {
    $(".progress-bar").css("width", "0px");
    $(".progress-bar").text("");
}