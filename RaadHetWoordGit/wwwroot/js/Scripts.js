var uri = "api/product";
/**
 * To get a list of products, send an HTTP GET request to URI "/api/products".
 * The jQuery getJSON function sends an AJAX request.
 * For response contains array of JSON objects.
 * The done function specifies a callback that is called if the request succeeds.
 * In the callback, we update the DOM (Document Object Model) with the product information.
 */
$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            // Foreach loop: iterate through all products
            $.each(data, function (key, item) {
                // Add a list item for the product.
                $('<li>', { text: formatItem(item) }).appendTo($('#products'));
            });
        });
});