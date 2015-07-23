
//--------------- Global variables -----------------------//
var menuItemName = null;
var baverageItemName = null;
var inventoryItemName = null;
var currentMenuItemMeasure = null;
var profitValue = null;
var menuPrice = null;
var price = null;

//list of selected items
var selectedMenuItemsList = [];

//data about inventory & bavarages from db
var beverageInventoryListArray = null;

//bavarege or inventory (true/false)
var isBeverage = null;

//
var isActive = true;


$('#create_menu_item').on("click", function () {

    //is active menu item?
    if ($(this).data('type') === "unactive") {
        isActive = false;
    }

    console.log(isActive);

    $.ajax({
        cache: false,
        type: "GET",
        url: '/api/pushups/getdata',
        //data: { 'id': id },
        contentType: 'application/json; charset=utf-8',
        complete: function () {


        },
        success: function (data) {
            beverageInventoryListArray = data;
            createMenuItem();

        },
        error: function (error) {
        }
    });



});

var createMenuItem = function () {
    bootbox.prompt("What is the name of your menu item?", function (result) {
        if (result === null) {
            //alert("Prompt dismissed");
        } else {
            menuItemName = result.trim();
            if (menuItemName === "") {
                return;
            }
            setTimeout(addBeverages, 1000);

        }
    });
};

//--------------- Push ups modals windows functions -----------------------------//
var addBeverages = function () {
    bootbox.dialog({
        message: "Would you like to add a beverage?",
        title: "Add beverages",
        buttons: {
            success: {
                label: "Yes",
                className: "btn-success",
                callback: function () {
                    isBeverage = true;
                    setTimeout(addBeverage, 1000);
                }
            },
            danger: {
                label: "No",
                className: "btn-danger",
                callback: function () {
                    setTimeout(addInventoryQ, 1000);
                }
            }
        }
    });
};
var addBeverage = function () {
    var options = "";
    for (var i = 0; i < beverageInventoryListArray.BeverageList.length; i++) {
        options += "<option value='" + beverageInventoryListArray.BeverageList[i].Id + "' data-price='" + beverageInventoryListArray.BeverageList[i].Price + "'>" + beverageInventoryListArray.BeverageList[i].ProductName + "</option>";

    }
    bootbox.dialog({
        title: "Add baverage",
        message: '<div class="row">  ' +
            '<div class="col-md-12"> ' +
            '<form class="form-horizontal"> ' +
            '<div class="form-group"> ' +
            '<label class="col-md-4 control-label" for="name">Select a baverage</label> ' +
            '<div class="col-md-4"> ' +
            '<select id="name" name="name" class="form-control input-md">' +
            options + '</select>' +
            '</div> ' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<span class="help-block" style="text-align:center;">This baverege is measured in: oz</span>' +
            '<label class="col-md-4 control-label" for="awesomeness">How much is in this menu item?</label> ' +
            '<div class="col-md-5">' +
            '<label for="awesomeness-1"><input type="text" name="measure" id="measure_val" > oz </label> ' +
            '</div> </div>' +
            '</form> </div>  </div>',
        buttons: {
            success: {
                label: "Add",
                className: "btn-success",
                callback: function () {
                    //var name = $('#name').val();
                    //var answer = $("input[name='awesomeness']:checked").val()
                    //alert("Hello " + name + ". You've chosen <b>" + answer + "</b>");
                    selectedMenuItemsList.push({ Id: $('#name').val(), Price: $('#name option:selected').data('price'), Qty: $('#measure_val').val() });
                    setTimeout(addAnotherBeverage(), 1000);
                }
            }
        }
    });
};
var addAnotherBeverage = function () {
    bootbox.dialog({
        message: "Would you like to add another a beverage?",
        title: "Add beverages",
        buttons: {
            success: {
                label: "Yes",
                className: "btn-success",
                callback: function () {
                    setTimeout(addBeverage, 1000);
                }
            },
            danger: {
                label: "No",
                className: "btn-danger",
                callback: function () {
                    setTimeout(profitCalculator, 1000);
                    //alert("uh oh, look out!");
                }
            }
        }
    });
};
var addInventoryQ = function () {
    bootbox.dialog({
        message: "Would you like to add an Inventory Item?",
        title: "Add Inventory",
        buttons: {
            success: {
                label: "Yes",
                className: "btn-success",
                callback: function () {
                    isBeverage = false;
                    setTimeout(addInventory, 1000);
                }
            },
            danger: {
                label: "No",
                className: "btn-danger",
                callback: function () {
                    //alert("uh oh, look out!");
                }
            }
        }
    });
};
var addInventory = function () {
    var options = "";
    for (var i = 0; i < beverageInventoryListArray.InventoryList.length; i++) {
        options += "<option value='" + beverageInventoryListArray.InventoryList[i].Id + "' data-price='" + beverageInventoryListArray.InventoryList[i].Price + "'>" + beverageInventoryListArray.InventoryList[i].ProductName + "</option>";

    }
    bootbox.dialog({
        title: "Add Inventory",
        message: '<div class="row">  ' +
            '<div class="col-md-12"> ' +
            '<form class="form-horizontal"> ' +
            '<div class="form-group"> ' +
            '<label class="col-md-4 control-label" for="name">Select an Item</label> ' +
            '<div class="col-md-4"> ' +
            '<select id="name" name="name" class="form-control input-md">' +
            options +
            '</select> ' +
            '</div> ' +
            '</div> ' +
            '<div class="form-group"> ' +
            '<span class="help-block" style="text-align:center;">This baverege is measured in: Can</span>' +
            '<label class="col-md-4 control-label" for="awesomeness">How much is in this menu item?</label> ' +
            '<div class="col-md-5">' +
            '<label for="awesomeness-1"><input type="text" name="awesomeness" id="measure_val" > Can </label> ' +
            '</div> </div>' +
            '</form> </div>  </div>',
        buttons: {
            success: {
                label: "Add",
                className: "btn-success",
                callback: function () {
                    //var name = $('#name').val();
                    //var answer = $("input[name='awesomeness']:checked").val()
                    //alert("Hello " + name + ". You've chosen <b>" + answer + "</b>");

                    selectedMenuItemsList.push({ Id: $('#name').val(), Price: $('#name option:selected').data('price'), Qty: $('#measure_val').val() });
                    setTimeout(addAnotherInventory, 1000);
                }
            }
        }
    });
};
var addAnotherInventory = function () {
    bootbox.dialog({
        message: "Would you like to add another item?",
        title: "Add Inventory",
        buttons: {
            success: {
                label: "Yes",
                className: "btn-success",
                callback: function () {
                    setTimeout(addInventory, 1000);
                }
            },
            danger: {
                label: "No",
                className: "btn-danger",
                callback: function () {
                    //alert("uh oh, look out!");
                    setTimeout(profitCalculator, 1000);
                }
            }
        }
    });
};
var profitCalculator = function () {
    bootbox.dialog({
        title: "Profit Calculator",
        message: '<div class="row">  ' +
            '<div class="col-md-12"> ' +
            '<form class="form-horizontal"> ' +
            '<div class="form-group"> ' +
            '<label class="col-md-4 control-label" for="name">How much profit would you like to make?</label> ' +
            '<div class="col-md-4"> ' +
            '<input name="profit" id="profit" type="number" min="0" max="100" /> %' +
            '</div> ' +
            '</div> ' +
            '</form> </div>  </div>',
        buttons: {
            success: {
                label: "Next",
                className: "btn-success",
                callback: function () {
                    //var name = $('#name').val();
                    //var answer = $("input[name='awesomeness']:checked").val()
                    //alert("Hello " + name + ". You've chosen <b>" + answer + "</b>");
                    profitValue = $('#profit').val();
                    setTimeout(enterMenuPrice, 1000);
                }
            }
        }
    });
};
var enterMenuPrice = function () {
    /*--------- price calculating ----------*/
    var totalPrice = 0;

    for (var i = 0; i < selectedMenuItemsList.length; i++) {
        totalPrice += (selectedMenuItemsList[i].Price * selectedMenuItemsList[i].Qty);
    }


    var resultPrice = totalPrice * profitValue / 100;

    //round to one digit after comma
    resultPrice = Math.round(resultPrice * 100) / 100;

    price = resultPrice;


    bootbox.dialog({
        title: "Enter Menu Price",
        message: '<div class="row">  ' +
            '<div class="col-md-12"> ' +
            '<form class="form-horizontal"> ' +
            '<div class="form-group"> ' +
            '<label class="col-md-4 control-label" for="name">At (' + profitValue + ') profit we suggest a price of $' + resultPrice + ' or more, Enter the menu price you would like</label> ' +
            '<div class="col-md-4"> ' +
            '<input value="' + resultPrice + '" name="resultPrice" id="resultPrice" /> ' +
            '</div> ' +
            '</div> ' +
            '</form> </div>  </div>',
        buttons: {
            success: {
                label: "Save new Menu Item",
                className: "btn-success",
                callback: function () {
                    menuPrice = $('#resultPrice').val();
                    addMenuItem();
                }
            }
        }
    });
};

var addMenuItem = function() {
    var type = isBeverage ? "Beverage Inventory" : "Food";

    var beverageInventoryIds = "";
    for (var i = 0; i < selectedMenuItemsList.length; i++) {
        //make uniqe
        if (beverageInventoryIds.indexOf(selectedMenuItemsList[i].Id.toString()) < 0) {
            beverageInventoryIds +=
                selectedMenuItemsList[i].Id + ',';
        }
    }
    console.log(beverageInventoryIds);

    $.ajax({
        cache: false,
        type: "POST",
        url: '/api/pushups/addMenuItem?name=' + menuItemName + '&price=' + price + '&type=' + type + '&menuPrice=' + menuPrice + '&profit=' + profitValue + '&isActive=' + isActive + '&beverageIds=' + beverageInventoryIds,
        //data: { 'id': id },
        contentType: 'application/json; charset=utf-8',
        complete: function() {


        },
        success: function(data) {
            window.location.reload();

        },
        error: function(error) {
        }
    });
};



/*------------------------ Edit Menu Item --------------------------*/
$("#edit_menu_item").on("click", function () {
    var menuItemId = $('.itemCheckbox:checked:first').val();
    editMenuItem(menuItemId);
});

var editMenuItem = function (id) {
    $.ajax({
        cache: false,
        type: "GET",
        url: '/api/menuitem/get?id=' + id,
        contentType: 'application/json; charset=utf-8',
        complete: function () {


        },
        success: function (data) {
            console.log(data.BeverageIds);
            data.BeverageIds = (data.BeverageIds[data.BeverageIds.length - 1] === ',') ? data.BeverageIds.slice(0, -1) : data.BeverageIds;
            var beverageIdsStr = data.BeverageIds.split(',').join('&beverageIds=');
            console.log(beverageIdsStr);

            $.ajax({
                cache: false,
                type: "GET",
                url: '/api/pushups/getEditData?beverageIds=' + beverageIdsStr,
                contentType: 'application/json; charset=utf-8',
                complete: function () {


                },
                success: function (data2) {
                    data.InventoryList = data2.InventoryList;
                    data.BeverageList = data2.BeverageList;
                    console.log(data);
                    editMenuItemPopup(data);
                },
                error: function (error) {
                }
            });
        },
        error: function (error) {
        }
    });
};

var editMenuItemPopup = function (data) {
    var options = "";
    for (var i = 0; i < data.BeverageList.length; i++) {
        options += '<tr>' +
            '<td><input type="checkbox" class="edtItemCheckbox" data-code="@item.MenuItemId" name="selectedItems" value="@item.MenuItemId" /></td>' +
            '<td>' + data.BeverageList[i].ProductName + '</td>' +
            '<td>' + data.BeverageList[i].Price + '</td>' +
            '<td>' + data.BeverageList[i].Price + '</td>' +
            '<td>' + data.BeverageList[i].UnitOfMeasurment + '</td>' +
            '</tr>';

    }
    console.log(options);


    var table = 
    '<div class="row">'+
    '<div class="col-md-12" style="margin-top: 20px;">'+
        '<div class="widget widget-table">'+
            '<div class="widget-content">'+
                '<table id="datatable-column-filter" class="table table-sorting table-striped table-hover datatable">'+
                    '<thead>'+
                        '<tr>'+
                            '<th style="width: 15px;"></th>'+
                            '<th>Item Name</th>'+
                            '<th>Cost Price</th>'+
                            '<th>Amount</th>'+
                            '<th>Measurement</th>'+
                        '</tr>'+
                    '</thead>'+
                    '<tbody>'+
                            options +
                    '</tbody>'+
                '</table>'+
            '</div>'+
        '</div>'+
    '</div>'+
'</div>';

    var btnRow =
        '<div class="row">' +
            '<input type="button" class="btn btn-default" value="Delete Item" id="delete_item">' +
            '<input type="button" class="btn btn-default" style="margin-left:20px;" value="Add New Item to ' + data.ItemName + '" id="add_new_item">' +
        '</div>';

    bootbox.dialog({
        title: "Edit " + data.ItemName,
        message: table + btnRow +
            '<h4>Total Cost of ' + data.ItemName + ': ' + data.ItemPrice + '</h3>' +
            '<div class="row">  ' +
            '<div class="col-md-12"> ' +
            '<form class="form-horizontal"> ' +
            '<div class="form-group"> ' +
                '<label class="col-md-4 control-label" for="menu_price">Menu Price of ' + data.ItemName + ': </label> ' +
                '<div class="col-md-4"> ' +
                    '<input id="menu_price" name="menu_price" type="number" value="' + data.MenuPrice + '" />' +
                '</div>' +
            '</div>' +
            '<div class="clearfix"></div>' +
            '<div class="form-group"> ' +
                '<label class="col-md-4 control-label" for="profit">% profit you are making: </label> ' +
                '<div class="col-md-4"> ' +
                    '<input id="profit" name="profit" type="number"  min="0" max="100"  value="' + data.Profit + '" />' +
                '</div>' +
            '</div> ' +
            '</form> </div>  </div>',
        buttons: {
            success: {
                label: "Save",
                className: "btn-success",
                callback: function () {
                    data.MenuPrice = $('#menu_price').val();
                    data.Profit = $('#profit').val();
                    updateMenuItem(data);
                }
            }
        }
    });
};

var updateMenuItem = function (item) {
    $.ajax({
        cache: false,
        type: "PUT",
        url: '/api/menuitem/put',
        data: JSON.stringify(item),
        contentType: 'application/json; charset=utf-8',
        complete: function () {

        },
        success: function (data) {
            location.reload();
        },
        error: function (error) {
        }
    });
};


/*---------------- Add BeverageInventories ----------*-----------*/
$('#add_beverageInventories').on('click', function() {
    addBeverageInventoriesW();
});
var addBeverageInventoriesW = function () {
    var html = 
        '<div class="row">'+
    '<div class="col-sm-12">'+
        '<div class="form-group form-group-default">'+
            '<label for="ProductName">ProductName</label>'+
            '<input class="form-control" id="ProductName" name="ProductName" placeholder="Name of your product" required="required" type="text" value="">'+
            '<span class="field-validation-valid" data-valmsg-for="ProductName" data-valmsg-replace="true"></span>'+
       ' </div>'+
    '</div>'+
'</div>'+
'<div class="row">'+
    '<div class="col-sm-12">'+
        '<div class="form-group form-group-default">'+
            '<label for="Vendor">Vendor</label>'+
            '<input class="form-control" id="Vendor" name="Vendor" placeholder="Vendor Name" required="required" type="text" value="">'+
            '<span class="field-validation-valid" data-valmsg-for="Vendor" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
'</div>'+
'<div class="row">'+
    '<div class="col-sm-4">'+
       ' <div class="form-group form-group-default">'+
            '<label for="Price">Price</label>'+
            '<input class="form-control" data-val="true" data-val-number="The field Price must be a number." data-val-required="The Price field is required." id="Price" name="Price" placeholder="Your price" required="required" type="text" value="0">'+
            '<span class="field-validation-valid" data-valmsg-for="Price" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
    '<div class="col-sm-4">'+
        '<div class="form-group form-group-default">'+
            '<label for="Age">Age</label>'+
            '<input class="form-control" data-val="true" data-val-number="The field Age must be a number." data-val-required="The Age field is required." id="Age" name="Age" placeholder="Age" required="required" type="text" value="0">'+
            '<span class="field-validation-valid" data-valmsg-for="Age" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
    '<div class="col-sm-4">'+
        '<div class="form-group form-group-default">'+
            '<label for="Volume">Unit Of Measurment</label>'+
            '<input class="form-control" id="Volume" name="Volume" placeholder="Volume in oz" required="required" type="text" value="">'+
            '<span class="field-validation-valid" data-valmsg-for="Volume" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
'</div>';
    bootbox.dialog({
        title: "Add",
        message: html,
        buttons: {
            success: {
                label: "Save",
                className: "btn-success",
                callback: function () {
                    var model = {
                        ProductName : $('#ProductName').val(),
                        Vendor: $('#Vendor').val(),
                        Price: $('#Price').val(),
                        UnitOfMeasurment: $('#Volume').val(),
                        Age: $('#Age').val()
                    };
                    addBeverageInventories(model);
                }
            }
        }
    });
};


var addBeverageInventories = function(model) {
    $.ajax({
        cache: false,
        type: "POST",
        url: '/api/beverageInventories/add',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        complete: function () {


        },
        success: function (data) {
            window.location.reload();

        },
        error: function (error) {
        }
    });
}


/*---------------- Add Inventories ---------------------*/
$('#add_inventories').on('click', function() {
    addInventoriesW();
});
var addInventoriesW = function () {
    var html = 
        '<div class="row">'+
    '<div class="col-sm-12">'+
        '<div class="form-group form-group-default">'+
            '<label for="ProductName">ProductName</label>'+
            '<input class="form-control" id="ProductName" name="ProductName" placeholder="Name of your product" required="required" type="text" value="">'+
            '<span class="field-validation-valid" data-valmsg-for="ProductName" data-valmsg-replace="true"></span>'+
       ' </div>'+
    '</div>'+
'</div>'+
'<div class="row">'+
    '<div class="col-sm-12">'+
        '<div class="form-group form-group-default">'+
            '<label for="Vendor">Vendor</label>'+
            '<input class="form-control" id="Vendor" name="Vendor" placeholder="Vendor Name" required="required" type="text" value="">'+
            '<span class="field-validation-valid" data-valmsg-for="Vendor" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
'</div>'+
'<div class="row">'+
    '<div class="col-sm-6">'+
       ' <div class="form-group form-group-default">'+
            '<label for="Price">Price</label>'+
            '<input class="form-control" data-val="true" data-val-number="The field Price must be a number." data-val-required="The Price field is required." id="Price" name="Price" placeholder="Your price" required="required" type="text" value="0">'+
            '<span class="field-validation-valid" data-valmsg-for="Price" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
    '<div class="col-sm-6">'+
        '<div class="form-group form-group-default">'+
            '<label for="Volume">Unit Of Measurment</label>'+
            '<input class="form-control" id="Volume" name="Volume" placeholder="Volume in oz" required="required" type="text" value="">'+
            '<span class="field-validation-valid" data-valmsg-for="Volume" data-valmsg-replace="true"></span>'+
        '</div>'+
    '</div>'+
'</div>';
    bootbox.dialog({
        title: "Add",
        message: html,
        buttons: {
            success: {
                label: "Save",
                className: "btn-success",
                callback: function () {
                    var model = {
                        ProductName : $('#ProductName').val(),
                        Vendor: $('#Vendor').val(),
                        Price: $('#Price').val(),
                        UnitOfMeasurment: $('#Volume').val(),
                    };
                    addInventories(model);
                }
            }
        }
    });
};


var addInventories = function (model) {
    $.ajax({
        cache: false,
        type: "POST",
        url: '/api/inventories/add',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        complete: function () {


        },
        success: function (data) {
            window.location.reload();

        },
        error: function (error) {
        }
    });
}



/*------------------------ Edit Inventories--------------------------*/
$("#edit_inventories").on("click", function () {
    var id = $('.itemCheckbox:checked:first').val();
    editInventory(id);
});

var editInventory= function (id) {
    $.ajax({
        type: "GET",
        url: '/api/inventories/get?id=' + id,
        contentType: 'application/json; charset=utf-8',
        complete: function () {


        },
        success: function (data) {
            editInventoryPopup(data);
        },
        error: function (error) {
        }
    });
};


var editInventoryPopup = function (data) {
    var html =
        '<div class="row">' +
    '<div class="col-sm-12">' +
        '<div class="form-group form-group-default">' +
            '<label for="ProductName">ProductName</label>' +
            '<input class="form-control" id="ProductName" name="ProductName" placeholder="Name of your product" required="required" type="text" value="' + data.ProductName + '">' +
            '<span class="field-validation-valid" data-valmsg-for="ProductName" data-valmsg-replace="true"></span>' +
       ' </div>' +
    '</div>' +
'</div>' +
'<div class="row">' +
    '<div class="col-sm-12">' +
        '<div class="form-group form-group-default">' +
            '<label for="Vendor">Vendor</label>' +
            '<input class="form-control" id="Vendor" name="Vendor" placeholder="Vendor Name" required="required" type="text" value="' + data.Vendor + '">' +
            '<span class="field-validation-valid" data-valmsg-for="Vendor" data-valmsg-replace="true"></span>' +
        '</div>' +
    '</div>' +
'</div>' +
'<div class="row">' +
    '<div class="col-sm-6">' +
       ' <div class="form-group form-group-default">' +
            '<label for="Price">Price</label>' +
            '<input class="form-control" data-val="true" data-val-number="The field Price must be a number." data-val-required="The Price field is required." id="Price" name="Price" placeholder="Your price" required="required" type="text" value="' + data.Price + '">' +
            '<span class="field-validation-valid" data-valmsg-for="Price" data-valmsg-replace="true"></span>' +
        '</div>' +
    '</div>' +
    '<div class="col-sm-6">' +
        '<div class="form-group form-group-default">' +
            '<label for="Volume">Unit Of Measurment</label>' +
            '<input class="form-control" id="Volume" name="Volume" placeholder="Volume in oz" required="required" type="text" value="' + data.UnitOfMeasurment + '">' +
            '<span class="field-validation-valid" data-valmsg-for="Volume" data-valmsg-replace="true"></span>' +
        '</div>' +
    '</div>' +
'</div>';
    bootbox.dialog({
        title: "Edit",
        message: html,
        buttons: {
            success: {
                label: "Save",
                className: "btn-success",
                callback: function () {
                    data.ProductName = $('#ProductName').val();
                    data.Vendor = $('#Vendor').val();
                    data.Price = $('#Price').val();
                    data.UnitOfMeasurment = $('#Volume').val();

                    updateInventory(data);
                }
            }
        }
    });
};


var updateInventory = function (data) {
    $.ajax({
        type: "POST",
        url: '/api/inventories/put',
        data: JSON.stringify(data),
        contentType: 'application/json; charset=utf-8',
        complete: function () {


        },
        success: function (data) {
            editInventoryPopup(data);
        },
        error: function (error) {
        }
    });
};

