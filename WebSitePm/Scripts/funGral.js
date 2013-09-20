
$(document).ready(
    function () {
        configCalendar();
    });

function configCalendar() {
    $('.inputDate').datepicker(
        {
            changeMonth: true,
            changeYear: true,
        });
}