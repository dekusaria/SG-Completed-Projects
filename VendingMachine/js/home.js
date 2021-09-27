var ds = new DataService();

function loadInventory(items){
    let display = $('#itemDisplay');
    display.empty();

    for (let i = 0; i < items.length; i++) {
        let item = items[i];
        $(display).append(formatItem(item, i));
    }
    
    itemInteraction(items);
}

function formatItem(item, index) {
    let price = formatMoney(item.price);

    return `<div class="col rounded shadow item" id="item${index}"><p>${index + 1}</p>
    <p><h4 class="text-center">${item.name}</h4></p>
    <p style="text-align:center;">${price}</p>
    <p style="text-align:center;">Quantity Left: ${item.quantity}</p></div>`;
}

function formatMoney(amount) {
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
    });

    amount = formatter.format(amount);
    return amount;
}

function itemInteraction(items) {
    /*$(".item").hover(
        function () {
            $(this).css("background-color", "aqua");
            $(this).css("border-color", "rgb(17, 207, 207)");    
        },
        function () {
            $(this).css("background-color", "");
            $(this).css("border-color", "");
            $(this).css("border-style", "");
        }
    );
    $(".item").on('mousedown', function (event) {
        $(this).css("border-style", "inset");
    });
    $(".item").on('mouseup', function (event) {
        $(this).css('border-style', '');
    });*/
    $.each(items, function(index, item) {
        $('#item'+ index).on('click', function () {
            $('#itemInput').val(item.id);
            $('#indexInput').val(index + 1);
        });
    });
}

function addMoneyButtons() {
    $('#addDolar').on('click', function(event) {
        addMoneyBalance(1);
    });
    $('#addQuarter').on('click', function(event) {
        addMoneyBalance(.25);
    });
    $('#addDime').on('click', function(event) {
        addMoneyBalance(.1);
    });
    $('#addNickel').on('click', function(event) {
        addMoneyBalance(.05);
    });
}

function addMoneyBalance(amount) {
    let balance = $('#currentBalance').val();
    let number = Number(balance.replace(/[^0-9\.-]+/g,""));
    let newBalance = number + amount;
    $('#currentBalance').val(formatMoney(newBalance));
}

function onMakePurchase(e) {
    e.preventDefault();
    let purchase = {
        itemId: $('#itemInput').val(),
        balance: Number($('#currentBalance').val().replace(/[^0-9\.-]+/g,""))
    }

    ds.vendItem(purchase, function(response){
        ds.getInventory(loadInventory);
        calculateChangeAmount(response);
        displayMessage('Thank You!!!');
    }, function(error){
        let msg = $.parseJSON(error.responseText);
        displayMessage(msg.message);
    });
}

function displayMessage(msg) {
    $('#messages').val(msg);
}

function displayCoinageCount(coins) {
    $('#quarters').val(coins.quarters);
    $('#dimes').val(coins.dimes);
    $('#nickels').val(coins.nickels);
    $('#pennies').val(coins.pennies);
}

function calculateCoinage(amount) {
    amount *= 100;
    let quarters = (amount - (amount % 25)) / 25;
    amount %= 25;
    let dimes = (amount - (amount % 10)) / 10;
    amount %= 10;
    let nickels = (amount - (amount % 5)) / 5;
    amount %= 5;
    let pennies = (amount - (amount % 1)) / 1;

    let coins = {
        quarters: quarters,
        dimes: dimes,
        nickels: nickels,
        pennies: pennies
    };

    return coins;
}

function calculateChangeAmount(response) {
    let change = response.quarters * .25;
        change += response.dimes * .10;
        change += response.nickels * .05;
        change += response.pennies * .01;

    $('#changeBalance').val(formatMoney(change));
    $('#currentBalance').val('$0.00');
}

function onChangeReturn() {
    let currentBalance = Number($('#currentBalance').val().replace(/[^0-9\.-]+/g,""));
    let changeBalance = Number($('#changeBalance').val().replace(/[^0-9\.-]+/g,""));

    let total = currentBalance + changeBalance;

    displayCoinageCount(calculateCoinage(total));
    resetMenuValues();
    setTimeout(function() {
        $('#quarters').val('0');
        $('#dimes').val('0');
        $('#nickels').val('0');
        $('#pennies').val('0');
    }, 4000);
}

function resetMenuValues() {
    $('#currentBalance').val('$0.00');
    $('#changeBalance').val('$0.00');
    $('#itemInput').val('');
    $('#messages').val('');
}

$(document).ready(function () {
    var ds = new DataService();
    ds.getInventory(loadInventory);
    addMoneyButtons();
    resetMenuValues();
    $(document).on('click', '#makePurchase', onMakePurchase);
    $(document).on('click', '#changeReturn', onChangeReturn);
});