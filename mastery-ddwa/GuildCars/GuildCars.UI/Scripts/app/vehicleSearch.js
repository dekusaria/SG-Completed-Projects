$(document).ready(function () {
    $('#searchForm').submit(function (e) {
        e.preventDefault();
        search();
        return false;
    });
});

function search() {
    var vehicleTypeId = $('#vehicleTypeId').val();

    var params = '?searchTerm=' + $('#searchTerm').val() + '&minPrice=' + $('#minPrice').val() + '&maxPrice=' + $('#maxPrice').val() +
        '&minYear=' + $('#minYear').val() + '&maxYear=' + $('#maxYear').val();

    var moneyFormat = Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
    });

    $.ajax({
        type: 'GET',
        url: 'https://localhost:44380/api/inventory/search/' + vehicleTypeId + params,
        contentType: 'application/json',
        success: function (response) {
            $('#searchResults').empty();

            $.each(response, function (index, vehicleItem) {

                if (vehicleItem.Mileage <= 1000) {
                    var mileage = "New";
                }
                else {
                    var mileage = vehicleItem.Mileage.toLocaleString();
                }

                id = vehicleItem.VehicleId;

                var html = '<div class="col-12 vehicle-wide-card shadow">' +
                    '<p><strong>' + vehicleItem.Year + ' ' + vehicleItem.MakeName + ' ' + vehicleItem.ModelName + '</strong></p>' +
                    '<div class="row">' +
                    '<div class="col-sm-12 col-md-12 col-lg-3">' +
                    '<img class="img-thumbnail" src="' + imagePath + vehicleItem.ImageFileName + '"/>' +
                    '</div>' +
                    '<div class="col-sm-12 col-md-4 col-lg-3">' +
                    '<p><strong>Body Style: </strong>' + vehicleItem.BodyStyleType + '</p>' +
                    '<p><strong>Trans: </strong>' + vehicleItem.TransmissionType + '</p>' +
                    '<p><strong>Color: </strong>' + vehicleItem.ColorName + '</p>' +
                    '</div>' +
                    '<div class="col-sm-12 col-md-4 col-lg-3">' +
                    '<p><strong>Interior: </strong>' + vehicleItem.InteriorType + '</p>' +
                    '<p><strong>Mileage: </strong>' + mileage + '</p>' +
                    '<p><strong>VIN #: </strong>' + vehicleItem.Vin + '</p>' +
                    '</div>' +
                    '<div class="col-sm-12 col-md-4 col-lg-3">' +
                    '<p><strong>Sale Price: </strong>' + moneyFormat.format(vehicleItem.SalePrice) + '</p>' +
                    '<p><strong>MSRP: </strong>' + moneyFormat.format(vehicleItem.Msrp) + '</p>' +
                    '<p><a class="btn btn-primary" href="' + buttonPath + '/' + vehicleItem.VehicleId + '">' + buttonText + '</a></p></div></div></div>';

                $('#searchResults').append(html);
            });
        },
        error: function () {
            alert('Error performing search, try again later!');
        }
    });
}