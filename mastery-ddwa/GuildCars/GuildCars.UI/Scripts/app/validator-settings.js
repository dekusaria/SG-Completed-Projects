$.validator.setDefaults({
    errorClass: 'has-error',
    validClass: 'has-success',
    onsubmit: true,
    highlight: function (element, errorClass, validClass) {
        $(element).siblings('label').removeClass('has-success').addClass('has-error');
        $(element).siblings('span').removeClass('has-success').addClass('has-error');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).siblings('label').removeClass('has-error').addClass('has-success');
        $(element).siblings('span').removeClass('has-error').addClass('has-success');
    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.appendTo($(element).siblings('span'));
    }
});