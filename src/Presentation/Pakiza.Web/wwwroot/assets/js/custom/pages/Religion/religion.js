$('#p_modal_form').submit(function (event) {
    event.preventDefault();

    $('.error-message').hide();

    var postDataModel = {};

    var inputs = $(this).find('input,select');
    inputs.each(function () {
        var fieldName = $(this).attr('name');
        var fieldValue = $(this).val();
        if (fieldName == "id" || fieldName == "statusId" || fieldName == "parentId") {
            postDataModel[fieldName] = parseInt(fieldValue == "" || fieldValue == null ? "0" : fieldValue);
        } else {
            postDataModel[fieldName] = fieldValue;
        }
    });

    var buttonText = $("#addOrUpdateBtn").text();
    var url = "", message = "";

    if (buttonText == "Save") {
        url = "/ADM/Religion/CreateReligion";
        message = 'Submitted successfully!';
    }
    if (buttonText == "Update") {
        url = "/ADM/Religion/EditRegion";
        message = 'Updated successfully!';
    }
    console.log(JSON.stringify(postDataModel));

    $.ajax({
        url: url,
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        data: JSON.stringify(postDataModel),
        success: function (response) {
            GetAllData();

            $("#CloseBtn").click();
            $("#addOrUpdateBtn").text("Save");

            $("#resetBtn").click();

            UIkit.notify(message, { pos: 'top-right', status: 'success' });
        },
        error: function (xhr, status, error) {
            if (xhr.status === 400) {
                var data = xhr.responseJSON;

                if (data.errors != null || data.errors != undefined) {
                    console.log(data.errors);
                    $.each(data.errors, function (fieldName, errorMessage) {
                        $('#' + fieldName + '-error').text(errorMessage).show();
                    });
                    $('.error-message').each(function () {
                        var fieldName = $(this).attr('id').replace('-error', '');
                        if (!(fieldName in data.errors)) {
                            $("#" + fieldName + "-error").hide();
                        }
                    });
                    console.log('>>>>> start :: errors');
                    console.log(data.errors);
                    console.log('end :: errors <<<<<');
                }

                if (data.message != null || data.message != undefined) {
                    UIkit.notify(data.message, { pos: 'top-right', status: 'warning' });
                }
            } else {

               UIkit.notify(xhr.responseJSON.message, { pos: 'top-right', status: 'warning' });
            }
        }
    });
});

function GetSingleById(id, typeOfPurpose) {
    if (typeOfPurpose == "Delete") {
        DeleteSingleById("religionId", id, "/ADM/Religion/DeleteRegion");
    } else {
        GetSingleByIdForEditOrDetails(id, typeOfPurpose, "/ADM/Religion/GetReligionById");
    }
};

function DeleteSingleById(parameterName, parameterValue, url) {
    if (!parameterName || !parameterValue || !url) {
        console.error('Required parameters are missing.');
        return;
    }
    const requestData = parameterName != '' && parameterValue != '' ? { [parameterName]: parameterValue } : {};
    // Perform AJAX request to fetch data
    $.ajax({
        url: url,
        method: 'Post',
        data: requestData,
        success: function (data) {
            GetAllData();
            UIkit.notify("Delete successfully!", { pos: 'top-right', status: 'success' });
        },
        error: function (xhr, status, error) {
            console.error('Error fetching data:', error);
        }
    });
}

function GetSingleByIdForEditOrDetails(id, typeOfPurpose, url) {
    if (!id || !typeOfPurpose || !url) {
        console.error('Required parameters are missing.');
        return;
    }

    // Perform AJAX request to fetch data
    $.ajax({
        url: url,
        method: 'GET',
        data: { "menuId": id },
        success: function (data) {
            console.log(JSON.stringify(data));

            if (data != null) {
                if (typeOfPurpose == "Edit") {
                    $('#addOrUpdateBtn').text("Update");

                    for (var key in data) {
                        var inputElement = $('#p_modal_form').find('#' + key);
                        if (inputElement.length) {

                            if (key == "status" || key == "parentId") {
                                if (key == "parentId") {
                                    $('#parentIdHidden').val(data[key]);
                                } else {
                                    inputElement.val(data[key]).change();
                                }
                            } else {
                                inputElement.val(data[key]);
                            }
                        }
                    }
                    UIkit.modal("#myModal", { bgclose: false, keyboard: false }).show();
                }
                if (typeOfPurpose == "Details") {
                    $('#addOrUpdateBtn').text("Save");

                    for (var key in data) {
                        var inputElement = $('#p_modal_form').find('#' + key + 'View');
                        if (inputElement.length) {
                            inputElement.val(data[key]);
                        }
                    }
                    UIkit.modal("#myModal", { bgclose: false, keyboard: false }).show();
                }
            }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching data:', error);
        }
    });
}

GetAllData();
function GetAllData() {
    $(".loading").show();
    var isParentValue = $("#isParent").val();
    var isParent = isParentValue == "Parent" ? true : false;
    var url = '/ADM/Religion/GetReligions';
    $.get(url, function (data) {
        console.log(data);
        $("#tb_Container").html(data.tableData);
        altair_datatables.dt_rkCustom("#rk_dtInfo2", ".rk_dtButton2", 10);
        $(".loading").hide();
    });
}


DropdownList("status", "/ADM/Menu/GetAllModules", '', '', "id", "name");

$('#statusId').on('change', function () {
    var selectedValue = $(this).val();
    if (selectedValue != null) {
        DropdownList("status", "/ADM/Menu/GetAllMenuByModuleId", "status", selectedValue, "id", "name");
    }
});
$("#isParent").on("change", function () {
    var isParent = $(this).val();
    GetAllData();
});
$('#controllerName, #actionName').on('keydown', function (event) {
    var inputId = ($(this).attr("id") == 'controllerName' ? 'C' : 'A') + $(this).attr("id").substr(1);
    const keyPressed = event.key;
    const regex = /\w/;

    if (!regex.test(keyPressed)) {
        event.preventDefault();
        $('#' + inputId + '-error').text('Input should not contain spaces or special characters.').show();
    } else {
        $('#' + inputId + '-error').text('').hide();
    }
});

function DropdownList(dropdownForId, url, parameterName, parameterValue, displayValue, displayText) {
    if (!dropdownForId || !url || !displayValue || !displayText) {
        console.error('Required parameters are missing.');
        return;
    }

    const requestData = parameterName != '' && parameterValue != '' ? { [parameterName]: parameterValue } : {};
    var data = [{ id: "Active", name: "Active" }, { id: "Inactive", name:"Inactive" }]
    $("#" + dropdownForId).empty();
    if (requestData != {}) {
        $("#" + dropdownForId).append($('<option>', { value: parameterValue, text: parameterValue, disabled: true, selected: true }));
    } else {
        $("#" + dropdownForId).append($('<option>', { value: '', text: '-- Select --', disabled: true, selected: true }));
    }
    $.each(data, function (index, item) {
        $("#" + dropdownForId).append($('<option>', {
            value: item[displayValue],
            text: item[displayText]
        }));
    });
    $('#parentId').val($('#parentIdHidden').val()).change();
}

// SR || MSG :: Reset
$("#resetBtn").on("click", function () {
    $("#addOrUpdateBtn").text("Save");
    $('form input, form select, form textarea').each(function () {
        if ($(this).is('input') || $(this).is('textarea')) {
            $(this).val('');
        } else if ($(this).is('select')) {
            $(this).val(null).trigger('change');
        }
    });
    $('.error-message').hide();
});
//sr



function OpenPopupModal(elem) {
    if (elem == undefined || elem == "") {
        $("#name").val('');
        $("#id").val('');
        $("#addOrUpdateBtn").text("Save");
        UIkit.modal("#myModal", { bgclose: false, keyboard: false }).show();

    }
    else {

        $.get('/ADM/Role/GetSingleRoleById?roleId=' + elem, function (data) {
            $("#name").val(data.name);
            $("#id").val(data.id);
            $("#addOrUpdateBtn").text("Update");
            UIkit.modal("#myModal", { bgclose: false, keyboard: false }).show();

        });
    }

}