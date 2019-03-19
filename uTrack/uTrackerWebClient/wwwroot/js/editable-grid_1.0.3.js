var periodEnum = [{ id: 0, text: 'Regular' }, { id: 1, text: 'Over Time' }, { id: 2, text: 'Under Time' }, { id: 3, text: 'Excess Time' }];
var statusEnum = [{ id: 0, text: 'Draft' }, { id: 1, text: 'Pending Approval' }, { id: 2, text: 'Approved' }];
var periodDropDown = ['Regular', 'Over Time', 'Under Time', 'Excess Time'];
var statusDropDown = ['Draft', 'Pending Approval', 'Approved'];
var css = ['badge-secondary', 'badge-primary', 'badge-success'];

var columns = [
    {
        data: 'logId',
        type: 'text',
        width: 10,
        readOnly: true,
        className: 'htCenter'
    },
    {        
        data: 'date',
        type: 'date',
        dateFormat: 'DD/MM/YYYY',
        correctFormat: true,
        defaultDate: '01/01/1900',
        // datePicker additional options (see https://github.com/dbushell/Pikaday#configuration)
        datePickerConfig: {
            // First day of the week (0: Sunday, 1: Monday, etc)
            firstDay: 0,
            showWeekNumber: true,
            numberOfMonths: 1,
            disableDayFn: function (date) {
                // Disable Sunday and Saturday
                return date.getDay() === 0 || date.getDay() === 6;
            }
        },      
        width: 5,
        className: 'htCenter nomralize-text'
    },
    {
        data: 'project.clientName',
        type: 'text',
        width: 7,
        className: 'htCenter nomralize-text'
    },    
    {
        data: 'project.projectName',
        type: 'text',
        width: 7,
        className: 'htCenter nomralize-text'
    },
    {
        data: 'billable',
        type: 'numeric',
        width: 3,
        className: 'htCenter'
    },
    {
        data: 'nonBillable',
        type: 'numeric',       
        width: 4,
        className: 'htCenter'
    },
    {
        data: 'period',
        type: 'dropdown',
        source: periodDropDown,
        width: 5,
        renderer: periodDropdownRenderer
    },
    {
        data: 'status',
        type: 'dropdown',
        source: statusDropDown,
        width: 5,
        renderer: statusDropdownRenderer
    },   

];
var changedRows = [];
var hotElement = document.querySelector('#hot');
var hotElementContainer = hotElement.parentNode;

var hotSettings = {
    columns: columns,
    stretchH: 'all',
    width: ($('.body-content').width() - 10),
    autoWrapRow: true,
    height: 600,
    maxRows: 25,
    manualRowResize: true,
    manualColumnResize: true,
    rowHeaders: false,
    colHeaders: [
        'Log ID',
        'Date',
        'Client',       
        'Project',
        'Billable',
        'Non-Bilabble',
        'Period',
        'Status'       
    ],
    manualRowMove: true,
    manualColumnMove: true,
    contextMenu: true,
    filters: true,
    dropdownMenu: true,
    fillHandle: {
        direction: 'vertical',
        autoInsertRow: true
    }
};
var hot = new Handsontable(hotElement, hotSettings);

hot.addHook('afterChange', function (changes, source) {
    if (!changes || source === 'loadData') return;
    changes.forEach(([row, prop, oldValue, newValue]) => {      

        if (oldValue !== newValue) {
            changedRows.push(row);
        }
    });
});



function save() {
    $.blockUI({ message: 'saving data, please wait..' });   

    // distinct
    var rows = changedRows.filter((v, i, a) => a.indexOf(v) === i); 

    if (rows.length <= 0) {
        $.unblockUI();
        return;
    }

    rows.forEach(function (item, index) {
        const elm = hot.getDataAtRow(item);

        var data = {
       
        };

        var msg, title;

        //add
        if (elm[0] === null) {
            msg = data.jiraId + ' has been added. Priority : ' + data.storyPriority;
            title = currentUser + ' ADDED ' + data.jiraId;
            $.ajax({
                type: 'POST',
                accepts: 'application/json',
                url: '/api/ApiStories/',
                contentType: 'application/json',
                data: JSON.stringify(data),
                async: true,
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error(textStatus + ' ' + texterrorThrown);
                },
                success: function () {
                    connection.invoke("SendNotif", title, msg).catch(function (err) {
                        return console.error(err.toString());
                    });
                }
            });
        }
        // edit
        else {
            msg = data.jiraId + ' has been updated.';
            title = currentUser + ' UPDATED ' + data.jiraId;
            $.ajax({
                type: 'PUT',
                accepts: 'application/json',
                url: '/api/ApiStories/' + elm[0],
                contentType: 'application/json',
                data: JSON.stringify(data),
                async: true,
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error(textStatus + ' ' + texterrorThrown);
                },
                success: function () {

                    connection.invoke("SendNotif", title, msg).catch(function (err) {
                        return console.error(err.toString());
                    });
                }
            });
        }

    });   

    changedRows = [];
    refreshData = true;
}

function periodDropdownRenderer(instance, td, row, col, prop, value, cellProperties) {
    td.innerHTML = '';
    if (value || value === 0) {
        periodEnum.forEach(function (item) {
            if (item.id === value || item.text === value) {
                td.innerHTML = '<span class="badge badge-light">' + item.text + '</span>';               
            }
        });
       
    }
    td.className = 'htCenter';
    return td;
}

function statusDropdownRenderer(instance, td, row, col, prop, value, cellProperties) {
    td.innerHTML = '';
    if (value || value === 0) {
        statusEnum.forEach(function (item) {
            if (item.id === value || item.text === value) {
                td.innerHTML = '<span class="badge ' + css[item.id] +'">' + item.text + '</span>';
            }
        });

    }
    td.className = 'htCenter';
    return td;
}

function loadData(filter) {
    if (filter) {
        $.ajax({
            url: 'http://localhost:5000/api/ActivityLog',
            type: 'get',
            dataType: 'json',
            data: { sprint: filter },
            async: true,
            success: function (data) {
                hot.loadData(data);
            }
        });
    }
    else {
        $.ajax({
            url: 'http://localhost:5000/api/ActivityLog',
            type: 'get',
            dataType: 'json',          
            async: true,
            success: function (data) {
                hot.loadData(data);
            }
        });
    }
   
}

var refreshData = false;

// enable table responsiveness from view-port
$(window).on('resize', function () {
    hot.updateSettings({ width: ($('#body-content').width() - 10) });
});
// enable table responsiveness from sidebar adjustment
$(document).on('sidebarSizeChanged', function () {
    hot.updateSettings({ width: ($('#body-content').width() - 10) });
});

$(document).ready(function () {
  //  $.blockUI({ message: 'loading data, please wait..' });
    loadData();
});

//$(document).ajaxStop(function () {
//    $.unblockUI();

//    if (refreshData === true) {
//        hot.updateSettings({
//            data: []
//        });
//        loadData();
//        refreshData = false;
//    }
//});










