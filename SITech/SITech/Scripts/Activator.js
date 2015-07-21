
//----------------------------------Active Record------------------------
//$("body").on("click", ".activebtn", function () {
//    var itemid = $(this).attr('itemid');
//    var rolename = $(this).attr('rolename');
//        var tr = $(this).parents('tr:first');


//        $.ajax({
//            url: '/Customer/Activate/',
//            dataType: "json",
//            type: "PUT",
//            contentType: 'application/json; charset=utf-8',
//            data: JSON.stringify({ UserId: itemid, RoleName : rolename }),
//            async: true,
//            processData: false,
//            cache: false,
//            complete: function () {
//                tr.empty();
//                //var table = $('#datatable-column-filter').DataTable({
//                //    ajax: "data.json"
//                //});

//                //setInterval(function () {
//                //    table.ajax.reload();
//                //}, 2000);
//            },
//            success: function (data) {
//                tr.empty();
//                //var table = $('#datatable-column-filter').DataTable({
//                //    ajax: "data.json"
//                //});

//                //setInterval(function () {
//                //    table.ajax.reload();
//                //}, 2000);
//            },
//            error: function (error) {
//            }
//        });
   
//});


//----------------------------------Delete Record------------------------
$("body").on("click", ".activebtn", function () {
    var itemid = $(this).attr('itemid');
    var rolename = $(this).attr('rolename');
        var tr = $(this).parents('tr:first');

        $.ajax({
            url: '/Customer/Activate/',
            data: { UserId: itemid, RoleName: rolename },
            type: 'GET',
            dataType: "text",
            //contentType: 'application/json; charset=utf-8',
            complete: function () {
                tr.empty();
            },
            success: function (data) {
                tr.empty();
            },
            error: function (error) {
            }
        });
  
});