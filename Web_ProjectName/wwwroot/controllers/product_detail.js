$(document).ready(function () {

    //Init bootstrap max length
    $('[maxlength]').maxlength({
        alwaysShow: !0,
        warningClass: "badge bg-success",
        limitReachedClass: "badge bg-danger"
    });

    $('[data-toggle="tooltip"]').tooltip();

    //Submit form contact
    $('#form_data_contact').on('submit', function (e) {
        let $formElm = $(this);
        grecaptcha.ready(function () {
            grecaptcha.execute(reCATPCHA_Site_Key, { action: 'submit' }).then(function (token) {
                // Add your logic to submit to your backend server here.
                $formElm.find(".tokenReCAPTCHA").val(token);

                let isvalidate = $formElm[0].checkValidity();
                if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }
                e.preventDefault();
                e.stopImmediatePropagation();
                let formData = new FormData($formElm[0]);
                let laddaSubmitForm = Ladda.create($formElm.find('button[type="submit"]')[0]);
                laddaSubmitForm.start();
                $.ajax({
                    url: '/Product/Send',
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        laddaSubmitForm.stop();
                        if (!CheckResponseIsSuccess(response)) return false;
                        Swal.fire('Đã gửi yêu cầu! Cảm ơn bạn đã quan tâm!', '', 'success');
                        $formElm[0].reset();
                    }, error: function (err) {
                        laddaSubmitForm.stop();
                        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
                    }
                });
            });
        });
    });

});
