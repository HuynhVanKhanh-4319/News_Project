
$(document).ready(function () {
    $("#form_import").on("submit", function (e) {
        e.preventDefault();
        importExcel(this);
    });
});

function SyncIsEudrValue() {
    $("#isEudrHidden").val($("#isEudrSelect").val());
}

function importExcel(form) {
    SyncIsEudrValue();
    let formData = new FormData(form);

    $.ajax({
        url: '/TransportMaterial_UploadFile/ReadExcelTransportMaterial',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {
            if (res.result === 1) {
                ShowToastNoti('success', '', 'Tạo thành công!' + res.data.length + 'dòng.');
            } else {
                  ShowToastNoti('warning', '', 'Lỗi khi import!' + message);
            }
        },
        error: function () {
            ShowToastNoti('warning', '', 'Lỗi  hệ thống ');
        }
    });
}
