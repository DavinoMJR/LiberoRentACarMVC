$().ready(function () {
    //desabilitando datepicker CHATA do chrome
    $('input[type="date"]').attr('type', 'text');

    jQuery.validator.methods.date = function (value, element) {
        if (value) {
            try {
                $.datepicker.parseDate('dd/mm/yy', value);
            }
            catch (ex)
            {
                return false;
            }
        }
        return true;
    };
   
});
