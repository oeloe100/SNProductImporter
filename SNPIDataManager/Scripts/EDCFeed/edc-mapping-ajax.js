var inputFields = $("input[name='cat-value']");
var shopMainCategory = $(".select-shop-category");

var inputArray = new Array();
var pathname = window.location.pathname;

$("#create-link").click(function () {
    for (var i = 0; i < inputFields.length; i++) {
        if (inputFields[i].value != undefined &&
            inputFields[i].value != "") {
            if (inputArray[i] != inputFields[i].value)
                inputArray.push(inputFields[i]);
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
        var obj = { "id": SelectInputID(inputArray[index], vendor), "title": inputArray[index].value, "Vendor": vendor };
        data.push(obj);
    });

    $.ajax({
        url: '/MappingMiddelware/InsertMapping',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        //crossDomain: true,
        //xhrFields: {
        //    withCredentials: true
        //},
        data: JSON.stringify(data),
        success: function (result) {
            console.log(result);
            if (result == "Logout")
                Logout();
        },
        failure: function (jqXHR, textStatus, errorThrown) {
            alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText);
        }
    });
}

function Logout() {
    console.log("Logout2");
    $.ajax({
        url: '/Home/Logout',
        type: 'POST',
        contentType: 'html',
        success: function (result) {
            console.log(result);
            window.location.replace("https://localhost:44365/");
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
            //console.log($(categoryItem[i]));
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

/* Manage Existing Mappings */
if (pathname == "/EDCFeed/EDCFeedMapping/DisplayMappings") {
    $(".delete-mapping").click(function () {
        var currentRow = $(this).closest("tr");
        $.ajax({
            url: "/EDCFeed/EDCFeedMapping/DeleteMapping/" + $(this).attr("id") + "",
            type: "POST",
            success: function (result) {
                $(currentRow).remove();
            },
            function(jqXHR, textStatus, errorThrown) {
                alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText);
            }
        });
    });

    $(".delete-mappings").click(function () {
        var tbody = $(".table").children();
        var row = $(tbody).children();
        var deleteMapping = $(row).find(".delete-mapping");
        var data = new Array();

        $(deleteMapping).each(function () {
            var id = $(this).attr("id");
            data.push(id);
        });

        $.ajax({
            url: "/EDCFeed/EDCFeedMapping/DeleteMappings",
            type: "POST",
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            success: function (result) {
                $(row).not(":first").remove();
            },
            function(jqXHR, textStatus, errorThrown) {
                alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText);
            }
        });
    });

    //*** Run/Start Sync (Mapping) ***\\
    $(function () {
        $(".start-mappings").click(function () {
            console.log("start");
            $.ajax({
                url: "/MappingMiddelware/StartMapping",
                type: "POST",
                success: function (result) {

                },
                function(jqXHR, textStatus, errorThrown) {
                    alert("Status: " + jqXHR.status + "; Error: " + jqXHR.responseText);
                }
            });
        });
    });
}