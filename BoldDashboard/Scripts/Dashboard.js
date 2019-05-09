﻿(function () { })

function getDashboardData() {
    ResetDisplay()
    var APIKey = $("#apiKey").val()
    var patt1 = /[0-9]+:[0-9]+:[a-zA-Z0-9]+/;
    var result = APIKey.match(patt1);
    if (!result) {
        DisplayError("Incorrect input");
    }
    else {
        $.ajax({
            url: 'Home/GetDashboardData',
            async: 'false',
            type: 'GET',
            data: { "apiKey": APIKey },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    UpdateDashboard(data);
                }
                else {
                    DisplayError(data.error)
                }
            },
            error: function (xhr, status, error) {
                DisplayError(error);
            }
        });
    }
    
}

function UpdateDashboard(data) {
    $("#chatCount").text(data.chatCount);
    $("#desktopVisits").text(data.desktopVisit);
    $("#mobileVisits").text(data.mobileVisits);
}

function DisplayError(msg) {
    ResetDisplay()
    var div = $("#userMessage")
    div.append("<div><h2>" + msg + "</h2></div>")
}

function ResetDisplay() {
    $("#userMessage").empty()
    $("#chatCount").text("-");
    $("#desktopVisits").text("-");
    $("#mobileVisits").text("-");
}