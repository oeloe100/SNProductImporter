var inputFields = $("input[name='cat-value']");
var shopMainCategory = $(".select-shop-category");

var inputArray = new Array();

$(function () {
    
});

$("#create-link").click(function () {
    for (var i = 0; i < inputFields.length; i++) {
        if (inputFields[i].value != undefined &&
            inputFields[i].value != "") {
            if (inputArray[i] != inputFields[i].value)
                inputArray.push(inputFields[i]);

            //console.log(inputFields[i]);
        }
    }

    PostData();
    ClearLinkBoxInput();
    ResetProgressBar();
});

function PostData() {
    var data = new Array();

    $(inputArray).each(function (index) {
        var vendor = $(inputArray[index]).attr("vendor");
        var obj = { "id": SelectInputID(inputArray[index], vendor), "title": inputArray[index].value };
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

function SelectInputID(data, vendor) {
    var categoryItem = SelectBoxNestedElement(vendor);

    for (var i = 0; i < categoryItem.length; i++) {
        if ($(categoryItem[i]).text() == data.value) {
            console.log($(categoryItem[i]));
            return $(categoryItem[i]).attr("id");
        }
    }
}

function SelectBoxNestedElement(vendor) {
    switch (vendor) {
        case "shop":
            return $(".shop-box").find("p");
        case "supplier":
            return $(".supplier-box").find("p");
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