$(document).ready(function () {
    $('.submit-js').click(function (e) {
        e.preventDefault();
        const currentTarget = $(e.currentTarget);

        const datasFrom = $('.date-from-js');
        const datasTo = $('.date-to-js');
        const factoryId = currentTarget.data('factory-id');
        let dateFromToSend = "";
        let dateToToSend = "";
        datasFrom.each(function (e) {
            const currentDate = $(this);
            const targetFactoryId = currentDate.data('factory-id');
            if (targetFactoryId === factoryId) {
                dateFromToSend = currentDate.val();
            }
        });
        datasTo.each(function (e) {
            const currentDate = $(this);
            const targetFactoryId = currentDate.data('factory-id');
            if (targetFactoryId === factoryId) {
                dateToToSend = currentDate.val();
            }
        });
        const data = {
            from: dateFromToSend,
            to: dateToToSend,
            id_factory: factoryId
        }
        $.ajax({
            url: '../Schedule/FactorySchedule',
            type: 'POST',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            error:function (errorData) {
                console.log(errorData);
                alert(errorData);
            }
        });

    });
});
