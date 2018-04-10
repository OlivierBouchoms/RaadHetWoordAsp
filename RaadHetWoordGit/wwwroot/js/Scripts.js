var uri = "api/productapi";
var name;
var sales;
var query;
var productData;
var $name;
var $sales;
var $id;
var $increase;

/**
 * HTTP GET request to URI "/api/productsai".
 * The jQuery getJSON function sends an AJAX request.
 * The response contains array of JSON objects.
 * The done function specifies a callback that is called if the request succeeds.
 * In the callback, we update the DOM (Document Object Model) with the product information.
 */

$(document).ready(getProducts() );

//Send a request to insert a product
function insertProduct() {
    $name = $("#productText").val();
    $sales = $("#productSale").val();
      
    $.ajax({
        type: 'POST',
        url: uri,
        data: { name: $name, sales: $sales },  
        dataType: "json",
        success: function () {
            getProducts();
        },
        error: function () { alert('Is foutgegaan'); }
        });
}

//Send a request to delete a product
function deleteProduct() {
    $id = $("#productID").val();
    $.ajax({
        type: 'DELETE',
        url: uri,
        data: { id: $id },
        dataType: "json",
        success: function() {
            getProducts();
        },
        error: function () { alert('Is foutgegaan'); }
    });
}

//Increase or decrease the price of a product by 1
function changeProductPrice() {
    $id = $("#productID").val();
    var $increase = false;
    if ($("#button1").hasClass("incorrect")) {
        $increase = true;
    }
    $.ajax({
        type: 'PATCH',
        url: uri,
        data: { id: $id, increase: $increase },
        dataType: "json",
        success: function () {
            getProducts();
            changeButtonClass();
        },
        error: function () { alert('Is foutgegaan'); }
    });
}

function changeButtonClass() {
    if ($("#button1").hasClass("incorrect")) {
        $increase = true;
        $('#button1').removeClass();
        $('#button1').addClass("iscorrect--");
    }
    else {
        $('#button1').removeClass();
        $('#button1').addClass("incorrect");
    }
}

function getProducts() {
    $('#products').empty();
    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $('<li>', { text: formatItem(item) }).appendTo($('#products'));
            });
        });
}

function formatItem(item) {
    return item.name + ': \u20AC ' + item.sales + ' (id = ' + item.id + ')';
}