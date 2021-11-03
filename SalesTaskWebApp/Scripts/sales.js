$(document).ready(function () {
    $("#ProductCode").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Product/GetAllProductByCode",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Code, value: item.Code };
                    }))

                }
            })
        }
    });

    $("#ProductName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Product/GetAllProductByName",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }))

                }
            })
        }
    });
});
let productId = null;
$('#ProductCode').change(function () {
    let ProductCode = $('#ProductCode').val();
    if (ProductCode) {
        $.ajax({
            type: 'GET',
            url: '/Product/GetProductByCode',
            data: { code: ProductCode },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Result !== 'null') {
                    productId = data.Result.Id;
                    $('#ProductName').val(data.Result.Name);
                    $('#Price').val(data.Result.Price);
                    $('#Quantity').val(1);
                    $('#TotalPrice').val(data.Result.Price);
                    $('#AddOrderToList').prop('disabled', false);
                } else {
                    ClearInputs();
                }
            },
            error: function () {
                console.log("Something Error");
            }
        })
    }
});

$('#ProductName').change(function () {
    let ProductName = $('#ProductName').val().trim();
    if (ProductName) {
        $.ajax({
            type: 'GET',
            url: '/Product/GetProductByName',
            data: { Name: ProductName },
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Result !== 'null') {
                    productId = data.Result.Id;
                    $('#ProductCode').val(data.Result.Code);
                    $('#Price').val(data.Result.Price);
                    $('#Quantity').val(1);
                    $('#TotalPrice').val(data.Result.Price);
                    $('#AddOrderToList').prop('disabled', false);
                } else {
                    ClearInputs();
                }
            },
            error: function () {
                console.log("Something Error");
            }
        })
    }
});

function GetTotalPrice() {
    let discount = parseFloat($('#Discount').val());
    let price = parseFloat($('#Price').val());
    let quantity = parseInt($('#Quantity').val());
    if (price >= discount) {
        if (quantity == 0) {
            $('#TotalPrice').val(price - discount);
        } else {
            $('#TotalPrice').val((price - discount) * quantity);
        }
    } else {
        $('#Discount').val(0);
        if (quantity == 0) {
            $('#TotalPrice').val(price);
        } else {
            $('#TotalPrice').val(price * quantity);
        }
    }
}

$('#Discount').change(function () {
    GetTotalPrice();
});

$('#Quantity').change(function () {
    GetTotalPrice();
});

var indexTable = 1;
$('#AddOrderToList').click(function (e) {
    let _productId = productId;
    let productCode = $('#ProductCode').val();
    let productName = $('#ProductName').val().trim();
    let price = parseFloat($('#Price').val());
    let discount = parseFloat($('#Discount').val());
    let quantity = parseInt($('#Quantity').val());
    let totalPrice = parseInt($('#TotalPrice').val());
    var validateCount = 0;
    if (!productCode) {
        $('#CodeValid').text('Product code is required.')
        validateCount++;
    } else {
        $('#CodeValid').remove();
    }

    if (!productName) {
        $('#NameValid').text('Product name is required.')
        validateCount++;
    } else {
        $('#NameValid').remove();
    }
    if (validateCount > 0)
        return;

    var orderArray = new Array();
    $("#OrderDetailsTable tbody tr").each(function () {
        var row = $(this);
        var orderDetails = {};
        orderDetails.productCode = row.find("td").eq(1).html();
        orderDetails.productName = row.find("td").eq(2).html();
        orderDetails.price = row.find("td").eq(3).html();
        orderDetails.discount = row.find("td").eq(4).html();
        orderDetails.quantity = row.find("td").eq(5).html();
        orderDetails.totalPrice = row.find("td").eq(6).html();
        orderDetails.productId = row.find("td").eq(7).html();
        orderArray.push(orderDetails);
    });

    if (orderArray.length > 0) {
        for (var i = 0; i < orderArray.length; i++) {
            var productValueCode = orderArray[i].productCode;
            if (productValueCode == productCode) {
                Swal.fire(
                    'Error!',
                    'Product code is exist in table.',
                    'error'
                );
                ClearInputs();
                return false;
            }
        }
    }
    ClearInputs();

    var html = '';
    html += '<tr style="cursor: default;">';
    html += '<td>' + (indexTable++) + '</td>';
    html += '<td>' + productCode + '</td>';
    html += '<td>' + productName + '</td>';
    html += '<td>' + price + '</td>';
    html += '<td>' + discount + '</td>';
    html += '<td>' + quantity + '</td>';
    html += '<td>' + totalPrice + '</td>';
    html += '<td hidden>' + _productId + '</td>';
    html += '<td>' + '<button type="button" class="btn btn-danger" id="btnRemoveRow"><i class="fa fa-trash fa-x2"></i></button>'
        + '<button type="button" class="btn btn-defualt" id="btnEditRow" style="margin-left: 5px;"><i class="fa fa-edit fa-x2"></i></button>'
        + '</td>';
    html += '</tr>';
    $("#OrderDetailsTable tbody").append(html);
    return false;
});

function ClearInputs() {
    $('#ProductCode').val('');
    $('#ProductName').val('');
    $('#Price').val(0);
    $('#Discount').val(0);
    $('#Quantity').val(0);
    $('#TotalPrice').val(0);
    productId = null;
    $("#ProductCode").focus();
    $('#AddOrderToList').prop('disabled', true);
}

$("#OrderDetailsTable").on('click', '#btnRemoveRow', function () {
    var row = $(this).closest("tr");
    Swal.fire({
        title: 'Are you sure you want delete this code?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            row.remove();
            Swal.fire(
                'Deleted!',
                'Your file has been deleted.',
                'success'
            )
        }
    })
});

$("#OrderDetailsTable").on('click', '#btnEditRow', function () {
    var row = $(this).closest("tr");
    $('#ProductCode').val(row.find("td").eq(1).html());
    $('#ProductName').val(row.find("td").eq(2).html());
    $('#Price').val(row.find("td").eq(3).html());
    $('#Discount').val(row.find("td").eq(4).html());
    $('#Quantity').val(row.find("td").eq(5).html());
    $('#TotalPrice').val(row.find("td").eq(6).html());
    $("#ProductCode").focus();
    row.remove();
});

function Create(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var formData = new FormData(form);
        var orderDetailsArray = new Array();
        $("#OrderDetailsTable tbody tr").each(function () {
            var row = $(this);
            var orderDetails = {};
            orderDetails.productCode = row.find("td").eq(1).html();
            orderDetails.productName = row.find("td").eq(2).html();
            orderDetails.price = row.find("td").eq(3).html();
            orderDetails.discount = row.find("td").eq(4).html();
            orderDetails.quantity = row.find("td").eq(5).html();
            orderDetails.totalPrice = row.find("td").eq(6).html();
            orderDetails.productId = row.find("td").eq(7).html();
            orderDetailsArray.push(orderDetails);
        });

        if (orderDetailsArray.length > 0) {
            for (var i = 0; i < orderDetailsArray.length; i++) {
                formData.append("OrderDetails[" + i + "].ProductId", orderDetailsArray[i].productId);
                formData.append("OrderDetails[" + i + "].Price", orderDetailsArray[i].price);
                formData.append("OrderDetails[" + i + "].Discount", orderDetailsArray[i].discount);
                formData.append("OrderDetails[" + i + "].Quantity", orderDetailsArray[i].quantity);
                formData.append("OrderDetails[" + i + "].TotalPrice", orderDetailsArray[i].totalPrice);
            }
        } else {
            Swal.fire(
                'Error!',
                'Please add product items for this order.',
                'error'
            );
            return false;
        }

        //return false;
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (response.success === true) {
                    window.location.href = "Index";
                }
                return false;
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
};