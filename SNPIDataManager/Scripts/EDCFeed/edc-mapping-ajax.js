var inputFields = $("input[name='cat-value']");
var shopMainCategory = $(".select-shop-category");

var inputArray = new Array();

$(function () {
    
});

$("#create-link").click(function () {
    for (var i = 0; i < inputFields.length; i++) {
        if (inputFields[i].value != undefined &&
            inputFields[i].value != "") {
            inputArray.push(inputFields[i].value);
        }
    }

    PostData();
    ClearLinkBoxInput();
    ResetProgressBar();
});

function PostData() {
    var data = new Array();

    $(inputArray).each(function (index) {
        var obj = { "id": SelectInputID(inputArray[index], index), "title": inputArray[index] };
        data.push(obj);
    });

    $.ajax({
        url: '/Mapping/CreateMapping',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function (result) {
            console.log(result);
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText);
        }
    });
}

function SelectInputID(data, index) {
    var categoryItem = SelectBoxNestedElement(index);

    for (var i = 0; i < categoryItem.length; i++) {
        if ($(categoryItem[i]).text() == data)
            return $(categoryItem[i]).attr("id");
    }
}

function SelectBoxNestedElement(index) {
    switch (index) {
        case 0:
            return $(".shop-box").find("p");
        case 1:
            return $(".supplier-box").find("p")
    }
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