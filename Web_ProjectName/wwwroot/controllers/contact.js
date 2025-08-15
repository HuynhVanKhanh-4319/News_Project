
$(document).ready(function () {
    InitMaxLength();
    handleContactFormSubmit('#contactForm', '#contactForm button[type="submit"]');
});

function InitMaxLength(scope) {
    $(scope).find('input[maxlength], textarea[maxlength]').maxlength({
        alwaysShow: true,
        warningClass: "badge bg-info",
        limitReachedClass: "badge bg-danger",
        appendToParent: true,
        separator: ' / ',
        preText: 'Đã nhập ',
        postText: ' ký tự.',
        validate: true,
        showOnReady: false
    });
    $('#phone').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
}
function handleContactFormSubmit(formSelector, submitButtonSelector) {
    const $formElm = $(formSelector);
    const laddaSubmitForm = Ladda.create(document.querySelector(submitButtonSelector));

    $formElm.submit(function (e) {
        e.preventDefault();
        if (!this.checkValidity()) {
            $formElm.addClass('was-validated');
            return;
        }

        laddaSubmitForm.start();
        const formData = $(this).serialize();

        $.ajax({
            url: '/Contact/CreateContact',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                laddaSubmitForm.stop();
                InitMaxLength('#contactForm');
                if (!CheckResponseIsSuccess(response)) {
                    Swal.fire('Lỗi', response?.message || 'Có lỗi xảy ra', 'error');
                    return false;
                }
                Swal.fire('Đã gửi yêu cầu!', 'Cảm ơn bạn đã quan tâm!', 'success');
                $formElm[0].reset();
                $formElm.removeClass('was-validated');
            },
            error: function (err) {
                console.log(err);
                laddaSubmitForm.stop();
                Swal.fire('Lỗi', 'Có lỗi xảy ra, vui lòng thử lại', 'error');
            }
        });
    });
}

