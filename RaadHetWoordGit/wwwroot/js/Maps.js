function myMap() {
    var mapOptions = {
        center: new google.maps.LatLng(51.4520886, 5.481982600000038),
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.HYBRID
    }
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
}