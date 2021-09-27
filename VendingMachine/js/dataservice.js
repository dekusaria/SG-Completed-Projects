var DataService = function () {
    var data = this;
    
    data.getInventory = function (response, errorMessage) {
        $.ajax({
            method: 'GET',
            url: 'http://vending.us-east-1.elasticbeanstalk.com/items',
            success: response,
            error: errorMessage
        });
    }

    data.vendItem = function (purchase, change, errorMessage) {
        $.ajax({
            method: 'POST',
            url: 'http://vending.us-east-1.elasticbeanstalk.com/money/' + purchase.balance + '/item/' + purchase.itemId,
            contentType: 'application/json',
            success: change,
            error: errorMessage
        });
    }
}