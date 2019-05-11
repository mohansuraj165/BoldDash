(function () { })
let status = { 1: "Away", 2:"Available"}

/**
 * Fetches all data required for dashboard 
 */
function getDashboardData() {
    
    var APIKey = $("#apiKey").val()
    var patt1 = /[0-9]+:[0-9]+:[a-zA-Z0-9]+/;
    var result = APIKey.match(patt1);
    if (!result) {
        DisplayError("Incorrect input");
    }
    else {
        ResetDisplay()
        $('#loading').removeClass('hidden')
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
                    PopulateActiveOperator(data.activeOperators)
                    $('#loading').addClass('hidden')
                }
                else {
                    DisplayError(data.error)
                    $('#loading').addClass('hidden')
                }
            },
            error: function (xhr, status, error) {
                DisplayError(error);
                $('#loading').addClass('hidden')
            }
        });
    }
    
}

/**
 *Updates the dashboard with data retrieved
 */
function UpdateDashboard(data) {
    $("#chatCount").text(data.chatCount);
    $("#desktopVisits").text(data.desktopVisit);
    $("#mobileVisits").text(data.mobileVisits);
}

/**
 * Displays any error message to the user
 */
function DisplayError(msg) {
    
    $("#userMessage").empty()
    var div = $("#userMessage")
    div.append("<div><h2>" + msg + "</h2></div>")
}

/**
 * Displays warning or success message to the user
 */
function DisplayMsg(msg) {
    var div = $("#userMessage")
    div.append("<div><h2>" + msg + "</h2></div>")
    setTimeout(function () { $("#userMessage").empty() }, 5000)
}

/**
 * Clears the dashboard
 * */
function ResetDisplay() {
    $("#userMessage").empty()
    $("#chatCount").text("-");
    $("#desktopVisits").text("-");
    $("#mobileVisits").text("-");
}

/**
 * Displays all active operators on the table
 */
function PopulateActiveOperator(data) {
    $("#operatorStatusTable").empty()
    var div = $("#operatorStatusTable")
    var table = $("<table></table>").addClass("table table-bordered table-striped")
    var thead = $("<thead></thead>")
    thead = $("<td>Login ID</td><td>Name</td><td>Status</td>")
    var tbody = $("<tbody></tbody>")

    data.forEach(function (d) {
        var tr = $("<tr></tr>")
        tr.append("<td>" + d.Name + "</td>")
        var s = status[d.StatusType]
        tr.append("<td>" + s + "</td>")
        
        tr.append("<td>" + GetButton(d) + "</td>")
        tbody.append(tr)
    });

    table.append(tbody);
    div.append(table)
}

/**
 * Makes the HTML for Change butten in the table
 */
function GetButton(d) {
    var str = JSON.stringify(d)
    var currentStatus = d.StatusType;
    var nextStatus = null
    if (currentStatus == 1)
        nextStatus = 2
    else if (currentStatus == 2)
        nextStatus = 1
    return "<button class='btn btn-primary' onclick=ChangeOperatorStatus(" + str + ")> Change</button > "
}

/**
 * Changing the operator status to available/away
 */
function ChangeOperatorStatus(data) {
    var obj = JSON.stringify(data)
    $.ajax({
        url: 'Home/SetOperatorStatus',
        async: 'false',
        type: 'GET',
        data: {"operatorData": obj},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.success) {
                $('#loading').addClass('hidden')
                PopulateActiveOperator(data.activeOperators);
                
                DisplayMsg("Status successfully changed")
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