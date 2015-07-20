
    //--------------- Global variables -----------------------//
    var menuItemName = null;
    var baverageItemName = null;
    var inventoryItemName = null;
    var menuItemMeasure = null;
    var profitValue = null;
    var menuPrice = null;
    $('#create_menu_item').on("click", function() {
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
    });


    //--------------- Push ups modals windows functions -----------------------------//
    var addBeverages = function() {
        bootbox.dialog({
            message: "Would you like to add a beverage?",
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
                        setTimeout(addInventoryQ, 1000);
                    }
                }
            }
        });
    };
    var addBeverage = function () {
        bootbox.dialog({
            title: "Add baverage",
            message: '<div class="row">  ' +
                '<div class="col-md-12"> ' +
                '<form class="form-horizontal"> ' +
                '<div class="form-group"> ' +
                '<label class="col-md-4 control-label" for="name">Select a baverage</label> ' +
                '<div class="col-md-4"> ' +
                '<select id="name" name="name" class="form-control input-md">' +
                '<option>Cider</option><option>Coca-Cola</option><option>Beer</option></select>' +
                '</div> ' +
                '</div> ' +
                '<div class="form-group"> ' +
                '<span class="help-block" style="text-align:center;">This baverege is measured in: oz</span>' +
                '<label class="col-md-4 control-label" for="awesomeness">How much is in this menu item?</label> ' +
                '<div class="col-md-5">' +
                '<label for="awesomeness-1"><input type="text" name="awesomeness" id="awesomeness-1" > oz </label> ' +
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
                        baverageItemName = $('#name').val();
                        setTimeout(addAnotherBeverage, 1000);
                    }
                }
            }
        });
    };
    var addAnotherBeverage = function () {
        bootbox.dialog({
            message: "Would you like to add a beverage?",
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
        bootbox.dialog({
            title: "Add Inventory",
            message: '<div class="row">  ' +
                '<div class="col-md-12"> ' +
                '<form class="form-horizontal"> ' +
                '<div class="form-group"> ' +
                '<label class="col-md-4 control-label" for="name">Select an Item</label> ' +
                '<div class="col-md-4"> ' +
                '<select id="name" name="name" class="form-control input-md">' +
                '<option>Apple</option><option>Pear</option><option>Cherry</option>' +
                '</select> ' +
                '</div> ' +
                '</div> ' +
                '<div class="form-group"> ' +
                '<span class="help-block" style="text-align:center;">This baverege is measured in: Can</span>' +
                '<label class="col-md-4 control-label" for="awesomeness">How much is in this menu item?</label> ' +
                '<div class="col-md-5">' +
                '<label for="awesomeness-1"><input type="text" name="awesomeness" id="awesomeness-1" > Can </label> ' +
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
                        inventoryItemName = $('#name').val();
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
                '<select id="name" name="name" class="form-control input-md">' +
                '<option>20%</option><option>40%</option><option>60%</option><option>80%</option>' +
                '</select> ' +
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
                        profitValue = $('#name').val();
                        setTimeout(enterMenuPrice, 1000);
                    }
                }
            }
        });
    };
    var enterMenuPrice = function () {
        bootbox.dialog({
            title: "Enter Menu Price",
            message: '<div class="row">  ' +
                '<div class="col-md-12"> ' +
                '<form class="form-horizontal"> ' +
                '<div class="form-group"> ' +
                '<label class="col-md-4 control-label" for="name">At (' + profitValue + ') profit we suggest a price of $8.00 or more, Enter the menu price you would like</label> ' +
                '<div class="col-md-4"> ' +
                '<select id="name" name="name" class="form-control input-md"><option>1</option><option>2</option></select> ' +
                '</div> ' +
                '</div> ' +
                '</form> </div>  </div>',
            buttons: {
                success: {
                    label: "Save new Menu Item",
                    className: "btn-success",
                    callback: function () {
                        //var name = $('#name').val();
                        //var answer = $("input[name='awesomeness']:checked").val()
                        //alert("Hello " + name + ". You've chosen <b>" + answer + "</b>");
                        //setTimeout(addAnotherBeverage, 1000);
                        menuPrice = $('#name').val();

                    }
                }
            }
        });
    };