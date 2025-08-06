const MAX_FILE_SIZE_IMAGE = 5;
const ACCEPT_EXTENSION_IMAGE_FILE = ".png,.jpg,.jpeg";
const MAX_IMAGE_FILE = 5;
const MAX_VALUE_INT32 = 2147483647;


//Declare icon html
var _loadAnimationSmallHtml =
    `<div class="preloader-wrapper xs btn_search active">
        <div class="spinner-layer spinner-green-only">
            <div class="circle-clipper left">
                <div class="circle"></div>
            </div>
            <div class="gap-patch">
                <div class="circle"></div>
            </div>
            <div class="circle-clipper right">
                <div class="circle"></div>
            </div>
        </div>
    </div>`,
    _iconEditHtml = '<i class="material-icons">edit</i>',
    _iconAddHtml = '<i class="material-icons">add</i>',
    _iconDeleteHtml = '<i class="material-icons">delete</i>',
    _iconSearchHtml = '<i class="material-icons">search</i>',
    _iconPublishHtml = '<i class="material-icons">publish</i>',
    _iconViewHtml = '<i class="material-icons">visibility</i>',
    _iconGPSHtml = '<i class="icons8-google-maps"></i>',
    _iconLoadingHtml = '<i class="material-icons">autoreview</i>',
    _iconPermissionHtml = '<i class="material-icons">account_circle</i>';

//Declare loading overlay
var _overlaySuccessHtml = `<div class="overlay_update_success" style=" background-color: seagreen; display: flex; justify-content: center; align-items: center; opacity: 0.3; width: 100%; z-index: 10; height: 100%; position: absolute; ">
                            <i class="mdi mdi-check mdi-36px text-white"></i>
                        </div>`;
var _loadingOverlay = '<div class="overlay-loading" style="display:none;"><div class="lds-ring"><div></div><div></div></div></div>';
var _loadingOverlay3Dot = '<div class="overlay-loading" style="display:none;"><div class="lds-ellipsis"><div></div><div></div><div></div></div>';
var _loadingButtonOverlay = `<div class="overlay-loading-button text-center" style="display:none;"><div class="lds-dual-ring"><div></div><div></div><div></div><div></div></div>`;


//Declare resources language in js
var _dataTableResource = {
    Search: "Tìm kiếm:",
    Show: "Xem",
    All: "Tất cả",
    Entries: "dòng",
    Showing: "Đang xem",
    To: "đến",
    Of: "của",
    Next: "<i class='mdi mdi-chevron-right'>",
    Previous: "<i class='mdi mdi-chevron-left'>",
    First: "Trang đầu",
    Last: "Trang cuối",
    EmptyTable: "Không có dữ liệu",
    LoadingRecords: "Đang tải...",
    ZeroRecords: "Không tìm thấy dữ liệu"
};
var _dataTablePaginationStyle = function () {
    $(".dataTables_paginate > .pagination").addClass("pagination-rounded");
};
var _buttonResource = {
    Add: "Thêm",
    AddNew: '<i class="fa fa-plus-circle"></i> Thêm mới',
    Edit: "Sửa",
    Update: "Cập nhật",
    Save: "Lưu",
    Pushlish: "Đăng",
    uPushlish: "Gỡ",
    Delete: "Xóa",
    Cancel: "Hủy",
    Lock: "Khóa",
    UnLock: "Mở khóa",
    ChangePassword: "Đổi mật khẩu",
    ResetPassword: "Làm mới mật khẩu",
    ConfirmDelete: "Xác nhận xóa?",
    ConfirmSave: "Xác nhận lưu?",
    ConfirmLock: "Xác nhận khóa?",
    ConfirmUnLock: "Xác nhận mở khóa?",
    ConfirmRecover: "Xác nhận khôi phục?",
    View: "Xem",
    Accept: "Chấp nhận",
    Schedule: "Xếp lịch",
    Permission: "Quyền"
};
var _resultActionResource = {
    AddSuccess: "Thêm thành công",
    UpdateSuccess: "Cập nhật thành công",
    DeleteSuccess: "Đã xóa",
    LockSuccess: "Đã khóa",
    UnLockSuccess: "Đã mở khóa",
    RecoverSuccess: "Đã khôi phục",
    AddFail: "Thêm thất bại",
    UpdateFail: "Cập nhật thất bại",
    DeleteFail: "Xóa thất bại",
    ErrorAction: "Kết nối đến máy chủ không ổn định",
    PleaseWrite: "Bạn chưa nhập đủ hoặc thông tin không hợp lệ",
    NoData: "Không có dữ liệu",
};
var _textOhterResource = {
    contact: "Liên hệ",
    active: "Mở",
    lock: "Khóa",
};

var _lengthMenuResource = [[10, 25, 50, 100, -1], ['10', '25', '50', '100', 'Tất cả']];

var _languageDataTalbeObj = {
    url: '/assets/admin/vendors/datatables.net/js/i18n/vi.json',
};

var _imageErrorUrl = {
    avatar: `onerror="this.onerror=null;this.src='/images/error-avatar.png';"`,
    product: `onerror="this.onerror=null;this.src='/images/error-avatar.png';"`,
    news: `onerror="this.onerror=null;this.src='/images/error_3x2.png';"`,
    square: `onerror="this.onerror=null;this.src='/images/error-image.png';"`,
    notFound: '<div class="text-center"><img src="/images/notFound.png"></div>',
};


//Declare current page
var _currentPageUrl = location.pathname.split("/")[1] + "/" + location.pathname.split("/")[2];;

const _loadAnimationSpinnerHtml = function (isSpinner, text) {
    var spinner = `<button class="btn btn-sm" style="padding:0px 0px;" type="button" disabled=""><span class="spinner-border spinner-border-sm mr-1" role="status" aria-hidden="true"></span>${text}</button>`;
    if (isSpinner == 1)
        return spinner;
    return text;
}
var isSubmiting = false;

$(document).ready(function () {

    //Prevent default double click submit form
    $('form').submit(function (e) {
        let el = $(this).find('button[type="submit"]');
        el.attr('disabled', true);
        setTimeout(function () {
            isSubmiting = false;
            el.attr('disabled', false);
        }, 800);
        if (isSubmiting) {
            e.preventDefault();
            e.stopPropagation();
            e.stopImmediatePropagation();
            return false;
        } else {
            isSubmiting = true;
        }
    });

    //Focus confirm btn when show modal delete
    $('#modal_delete').on('shown.bs.modal', function () {
        $('#btn_delete_item_main_table').focus();
    });

});

//Focus end text in input text, use: $('#input_target').focusTextToEnd()
$.fn.focusTextToEnd = function () {
    this.focus();
    var $thisVal = this.val();
    this.val('').val($thisVal);
    return this;
}

//Check status code to redirect
function CheckResponseIsSuccessToRedirect(data) {
    if (data.error.code !== 200) {
        location.href = `/error/${data.error.code}`;
        return false;
    }
    return true;
}

//Check response is success
function CheckResponseIsSuccess(data) {
    var type = "warning";
    let isSuccess = false;
    switch (parseInt(data.result)) {
        case -1: type = "error"; ListErrorCodeAndMessage(data, type); break;
        case 0: type = "warning"; ShowToastNoti(type, '', data.error.message); break;
        case 1: type = "success"; isSuccess = true; break;
        default: break;
    }
    return isSuccess;
}

//List error code
function ListErrorCodeAndMessage(data, type) {
    switch (parseInt(data.error.code)) {
        case -2: ShowToastNoti(type, '', "Có gì đó không đúng!"); break;
        case -1: ShowToastNoti(type, '', "Có lỗi trong quá trình truy cập dữ liệu!"); break;
        case 0:
            if (!window.navigator.onLine)
                ShowToastNoti(type, '', "No Internet! Hãy kiểm tra kết nối mạng!");
            else
                ShowToastNoti(type, '', data.error.message ?? "");
            break;
        case 400: ShowToastNoti(type, "Lỗi 400!", "Yêu cầu không hợp lệ!"); break;
        case 401:
            //ShowToastNoti(type, "Lỗi 401!", "Chưa xác thực hoặc hết phiên. Vui lòng đăng nhập lại!"); SignInAgain();
            setTimeout(function () { location.href = '/account/signout?ReturnUrl=' + location.pathname; }, 100); break;
        case 403: ShowToastNoti(type, "Lỗi 403!", "Không có quyền truy cập!"); break;
        case 404: ShowToastNoti(type, "Lỗi 404!", "Không tìm thấy tài nguyên được yêu cầu!"); break;
        case 405: ShowToastNoti(type, "Lỗi 405!", "Phương thức yêu cầu vô hiệu hóa!"); break;
        case 406: ShowToastNoti(type, "Lỗi 406!", "Không tìm thấy bất kỳ nội dung nào với yêu cầu đưa ra!"); break;
        case 407: ShowToastNoti(type, "Lỗi 407!", "Yêu cầu xác thực proxy!"); break;
        case 408:
            //ShowToastNoti(type, "Lỗi 408!", "Phiên đăng nhập hết hạn! Đang thử tạo lại phiên mới...", 3000);
            SignOutJs(); break;
        case 409: ShowToastNoti(type, "Lỗi 409!", "Yêu cầu xung đột với máy chủ!"); break;
        case 410: ShowToastNoti(type, "Lỗi 410!", "Nội dung yêu cầu đã bị xóa!"); break;
        case 411: ShowToastNoti(type, "Lỗi 411!", "Máy chủ từ chối yêu cầu!"); break;
        case 411: ShowToastNoti(type, "Lỗi 411!", "Máy chủ từ chối yêu cầu!"); break;
        case 412: ShowToastNoti(type, "Lỗi 412!", "Điều kiện tiên quyết không được đáp ứng!"); break;
        case 413: ShowToastNoti(type, "Lỗi 413!", "Dữ liệu yêu cầu vượt giới hạn!"); break;
        case 414: ShowToastNoti(type, "Lỗi 414!", "URI yêu cầu dài hơn mức quy định!"); break;
        case 415: ShowToastNoti(type, "Lỗi 415!", "Định dạng của dữ liệu được yêu cầu không được máy chủ hỗ trợ!"); break;
        case 416: ShowToastNoti(type, "Lỗi 416!", "Không thể thực hiện phạm vi yêu cầu!"); break;
        case 417: ShowToastNoti(type, "Lỗi 417!", "Máy chủ không thể đáp ứng được yêu cầu!"); break;
        case 421: ShowToastNoti(type, "Lỗi 421!", "Yêu cầu được hướng đến một máy chủ không thể tạo phản hồi!"); break;
        case 423: ShowToastNoti(type, "Lỗi 423!", "Tài nguyên đang được truy cập bị khóa!"); break;
        case 424: ShowToastNoti(type, "Lỗi 424!", "Yêu cầu không thành công do không thực hiện được yêu cầu trước đó!"); break;
        case 428: ShowToastNoti(type, "Lỗi 428!", "Máy chủ gốc yêu cầu yêu cầu phải có điều kiện!"); break;
        case 429: ShowToastNoti(type, "Lỗi 429!", "Bạn đang yêu cầu quá nhiều trong một khoảng thời gian nhất định!"); break;
        case 431: ShowToastNoti(type, "Lỗi 431!", "Máy chủ không thể xử lý yêu cầu vì tiêu đề quá lớn!"); break;
        case 451: ShowToastNoti(type, "Lỗi 451!", "Tài nguyên yêu cầu không thể được cung cấp một cách hợp pháp!"); break;
        case 500: ShowToastNoti(type, "Lỗi 500!", `Máy chủ không thể xử lý yêu cầu! ${!IsNullOrEmty(data.error.message) ? "<br/>" + data.error.message : ""}`); break;
        case 501: ShowToastNoti(type, "Lỗi 501!", "Phương thức yêu cầu không được máy chủ hỗ trợ và không thể xử lý được!"); break;
        case 502: ShowToastNoti(type, "Lỗi 502!", "Máy chủ nhận được một phản hồi không hợp lệ!"); break;
        case 503: ShowToastNoti(type, "Lỗi 503!", "Máy chủ xảy ra lỗi trong quá trình xử lý yêu cầu!"); break;
        case 504: ShowToastNoti(type, "Lỗi 504!", "Máy chủ không thể nhận được phản hồi kịp thời!"); break;
        case 505: ShowToastNoti(type, "Lỗi 505!", "Phiên bản HTTP được sử dụng trong yêu cầu không được máy chủ hỗ trợ!"); break;
        case 506: ShowToastNoti(type, "Lỗi 506!", "Máy chủ có lỗi cấu hình nội bộ!"); break;
        case 507: ShowToastNoti(type, "Lỗi 507!", "Máy chủ không đủ tài nguyên!"); break;
        case 508: ShowToastNoti(type, "Lỗi 508!", "Máy chủ đã phát hiện một vòng lặp vô hạn trong khi xử lý yêu cầu!"); break;
        case 510: ShowToastNoti(type, "Lỗi 510!", "Cần có các phần mở rộng khác cho yêu cầu để máy chủ đáp ứng yêu cầu đó!"); break;
        case 511: ShowToastNoti(type, "Lỗi 511!", "Máy khách cần xác thực để có quyền truy cập mạng!"); break;
        default: ShowToastNoti(type, '', data.error.message ?? ""); break;
    }
}

//Sign out js
function SignOutJs() {
    ShowToastNoti("warning", "", "Đang kết nối lại...");
    $.ajax({
        type: 'POST',
        url: '/Account/SignOutJs',
        dataType: 'json',
        success: function (response) {
            switch (response.result) {
                case -1: ShowToastNoti("error", "", "Kết nối thất bại!"); break;
                case 0:
                    ShowToastNoti("error", "Lỗi 408!", "Hết thời gian yêu cầu hoặc phiên đăng nhập hết hạn!");
                    setTimeout(function () { SignInAgain();/*location.href = '/account/signout?ReturnUrl=' + location.pathname;*/ }, 2000); break;
                case 1: ShowToastNoti("success", "", "Đã khôi phục kết nối, hãy thử lại."); break;
                default: break;
            }
        }, error: function (err) {
            CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
        }
    })
}

//Clear validate form css
function RemoveClassValidate(elm) {
    try {
        $(elm).removeClass("was-validated");
        $(elm).find('input').removeClass("parsley-success");
        $(elm).find('input').removeClass("parsley-error");
        $('.parsley-errors-list').remove();
    } catch (e) {

    }
    //Remove other
    //$("#detailEditor").summernote("code", ""); //clear detail
    //myDropzone.removeAllFiles(); //clear file dropzone
    //$(".dropify-clear").click(); //clear dropify
}

//Show message error
function ShowToastNoti(type, title, message, timeout = 2000, position = 'topRight', icon = '') {
    let color = '';
    let audio = document.getElementById('audio_info');
    switch (type) {
        case 'success': icon = '<i class="iziToast-icon ico-success revealIn"></i>'; color = 'green';
            audio = document.getElementById('audio_success'); audio.volume = 0.2; break;
            //var audio = new Audio('/sounds/sound1.ogg'); audio.volume = 0.2; audio.play(); break;
        case 'warning': icon = '<i class="iziToast-icon ico-warning revealIn"></i>'; color = 'orange';
            audio = document.getElementById('audio_warning'); audio.volume = 1; break;
        //var audio = new Audio('/sounds/sound6.ogg'); audio.volume = 0.8; audio.play(); break;
        case 'error': icon = '<i class="iziToast-icon ico-error revealIn"></i>'; color = 'red';
            audio = document.getElementById('audio_error'); audio.volume = 0.4; break;
            //var audio = new Audio('/sounds/sound5.ogg'); audio.volume = 0.4; audio.play(); break;
        case 'info': icon = '<i class="iziToast-icon ico-info revealIn"></i>'; color = 'blue';
            audio = document.getElementById('audio_info'); audio.volume = 0.4; break;
        //var audio = new Audio('/sounds/sound7.ogg'); audio.volume = 0.4; audio.play(); break;
        case 'question': icon = '<i class="iziToast-icon ico-question revealIn"></i>'; color = 'yellow';
            audio = document.getElementById('audio_question'); audio.volume = 0.4; break;
        //var audio = new Audio('/sounds/sound4.ogg'); audio.volume = 0.4; audio.play(); break;
        default: break;
    }
    audio.currentTime = 0;
    audio.play();
    iziToast.show({
        icon: icon,
        color: color,
        displayMode: 0,
        title: title,
        message: message,
        position: position,
        progressBar: true,
        timeout: timeout,
        transitionIn: 'flipInX',
        transitionOut: 'flipOutX',
        progressBarColor: 'rgb(0, 255, 184)',
        imageWidth: 70,
        layout: 2,
        iconColor: 'rgb(0, 255, 184)'
    });
}

//Change url without reload
function ChangeURLWithOut(title, url) {
    window.history.pushState('', title, url);
}

//Show overlay loading button
function ShowOverlayLoadingButton(elm) {
    var $loading = $($(elm).children()[0]);
    if (!$loading.hasClass('overlay-loading-button')) {
        if ($(elm).hasClass('fa-refresh'))
            $(elm).removeAttr('class');
        $(elm).prepend(_loadingButtonOverlay);
        $($(elm).children()[0]).fadeIn(200);
    }
}

//Hide overlay
function HideOverlayLoadingButton(elm) {
    var $loading = $($(elm).children()[0]);
    if ($loading.hasClass('overlay-loading-button')) {
        $loading.fadeOut(200);
        setTimeout(function () {
            $loading.remove();
        }, 250);
    }
}

//Show overlay
function ShowOverlay3Dot(elm) {
    var $loading = $($(elm).children()[0]);
    if (!$loading.hasClass('overlay-loading')) {
        $(elm).prepend(_loadingOverlay3Dot);
        $($(elm).children()[0]).fadeIn(100);
    }
}

//Hide overlay
function HideOverlay3Dot(elm) {
    var $loading = $($(elm).children()[0]);
    if ($loading.hasClass('overlay-loading')) {
        $loading.fadeOut(100);
        setTimeout(function () {
            $loading.remove();
        }, 150);
    }
}

//Show hide panel
function ShowHidePanel(panelShow, panelHide) {
    $(panelHide).fadeOut(100);
    setTimeout(function () {
        $(panelShow).fadeIn(100);
    }, 200);
}

//Show panel
function ShowPanel(panel) {
    $(panel).slideDown(200);
}

//Hide panel
function HidePanel(panel) {
    $(panel).fadeOut(200);
}

//Check exists key-value in array object
function IsExistsKeyValueInArrayObj(arr, key, value) {
    if (arr.length > 0) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i][key] === value) return true;
        }
    }
    return false;
}

//Find item by keyword in array string
function FindItemByKeywordInArrayString(arr, keyword) {
    let result = "";
    arr.forEach(function (item, index) {
        if (item.indexOf(keyword) > -1) {
            result = item;
        }
    });
    return result;
}

//Find item by key-value in array object
function FindItemByKeyValueInArrayObj(arr, key, value) {
    if (arr.length > 0) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i][key] === value) return arr[i];
        }
    }
    return null;
}

//Back to talbe
function BackToTable(elmShow = '#div_main_table', elmHide = '#div_view_panel') {
    ShowHidePanel(elmShow, elmHide);
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    $(elmHide).find('.navbar_title').removeClass('active');
}

//Show overlay
function ShowOverlay(elm) {
    var $loading = $($(elm).children()[0]);
    if (!$loading.hasClass('overlay-loading')) {
        $(elm).prepend(_loadingOverlay);
        $(elm).find('.overlay-loading').fadeIn(100);
    }
}

//Hide overlay
function HideOverlay(elm) {
    var $loading = $(elm).find('.overlay-loading');
    if ($loading.hasClass('overlay-loading')) {
        $loading.fadeOut(100);
        setTimeout(function () {
            $loading.remove();
        }, 150);
    }
}

//Show overlay update success
function ShowOverlayUpdateSuccess(elm) {
    elm.prepend(_overlaySuccessHtml);
    $('.overlay_update_success').fadeOut(1000);
    setTimeout(function () {
        $('.overlay_update_success').remove();
    }, 1100);
}

//Show sweet alert
function ShowSweetAlert(type, title, message, url, id, messageSuccess) {
    swal.fire({
        title: title,
        text: message,
        icon: type,
        showCancelButton: !0,
        confirmButtonText: "OK",
        cancelButtonText: "Cancel",
        confirmButtonClass: "btn btn-success mt-2",
        cancelButtonClass: "btn btn-danger ml-2 mt-2",
        buttonsStyling: !1,
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve, reject) {
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: {
                        id: id
                    },
                    dataType: 'json',
                    success: function (response) {
                        //Check error code
                        if (response.data == null && response.code != null) {
                            CheckResponseIsSuccess(response);
                            resolve();
                            return;
                        }

                        switch (response.isSuccess) {
                            case true: swal.fire(messageSuccess, '', 'success'); break;
                            case false: ShowToastNoti('warning', '', response.messageErr); resolve(); break;
                            default: ShowToastNoti('warning', '', _resultActionResource.ErrorAction); resolve(); break;
                        }
                    },
                    error: function () {
                        ShowToastNoti('error', '', _resultActionResource.ErrorAction);
                        resolve();
                    }
                });
            });
        }
    });
}

//Add first row DataTalbe
function AddFirstRowDataTable(table, data) {
    //Add row data
    table.row.add(data).draw(false).node();

    //Set current page
    var currentPage = table.page();

    //move added row to desired index
    var rowCount = table.data().length - 1,
        insertedRow = table.row(rowCount).data(),
        tempRow;
    for (var i = rowCount; i >= 1; i--) {
        tempRow = table.row(i - 1).data();
        table.row(i).data(tempRow);
        table.row(i - 1).data(insertedRow);
    }

    //refresh the current page
    table.page(currentPage).draw(false);

    //Add animation new row
    var rowNode = table.row(0).node();
    RunAnimationAddRowNode(rowNode);
}

//Run animation add
function RunAnimationAddRowNode(rowNode) {
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    setTimeout(function () {
        //4a81d4
        $(rowNode).css({ "background-color": "#339933", "color": "white", "fontSize": ".1rem", "opacity": "0" })
            .animate({
                fontSize: '.75rem',
                opacity: '1'
            }, 300);
        setTimeout(function () {
            if ($(rowNode).hasClass('odd')) {
                $(rowNode).css({ "background-color": "#f3f7f9", "transition": "3s", "color": "#6c757d" });
            } else {
                $(rowNode).css({ "background-color": "#fff", "transition": "3s", "color": "#6c757d" });
            }
            setTimeout(() => {
                $(rowNode).removeAttr("style");
            }, 3000);
        }, 300);
    }, 800);
}

//Run animation delete
function RunAnimationDeleteRowNode(rowNode) {
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    $(rowNode).css({ "fontSize": ".875rem", opacity: "1" })
        .animate({
            fontSize: '.1rem',
            opacity: '0'
        }, 500);
}

//Run animation edit
function RunAnimationEditRowNode(rowNode) {
    $('.dataTables_scrollHeadInner').css('width', '100%');
    $('.dataTables_scrollFootInner').css('width', '100%');
    $('.dataTables_scrollHeadInner table').css('width', '100%');
    $('.dataTables_scrollFootInner table').css('width', '100%');
    setTimeout(function () {
        //FFCC33 | bb000c
        $(rowNode).css({ "background-color": "#18c355", "color": "#FF0000", "font-weight": "bold" }).animate({
            opacity: '0.7'
        }, 300);
        $(rowNode).animate({
            opacity: '1'
        }, 300);
        $(rowNode).css({ "background-color": "#fff", "transition": "1s", "color": "#6c757d" });
        setTimeout(function () {
            $(rowNode).css({ "background-color": "#18c355", "color": "#FF0000", "font-weight": "normal" }).animate({
                opacity: '0.5'
            }, 300);
        }, 500);
        setTimeout(() => {
            $(rowNode).removeAttr("style");
        }, 2000);
    }, 200);
}

//Change UI Add
function ChangeUIAdd(table, data) {
    AddFirstRowDataTable(table, data);
}

//Change UI Edit
function ChangeUIEdit(table, id, data) {
    var rowIdx;
    var rowData = table.row(
        function (idx, data, node) {
            if (data.id == id) {
                rowIdx = idx;
                return idx.toString();
            }
        }).data();
    Object.keys(data).forEach((item, index) => rowData[item] = data[item]);
    table.row(rowIdx).data(rowData).draw(false);
    var rowNode = table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).node();
    RunAnimationEditRowNode(rowNode);
}

//Change UI Delete
function ChangeUIDelete(table, id) {
    var rowNode = table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).node();
    RunAnimationDeleteRowNode(rowNode);
    setTimeout(() => table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).remove().draw(false), 500);
}

//Change UI Status
function ChangeUIStatus(table, id, data) {
    var rowIdx;
    var rowData = table.row(
        function (idx, data, node) {
            if (data.id == id) {
                rowIdx = idx;
                return idx.toString();
            }
        }).data();
    Object.keys(data).forEach((item, index) => rowData[item] = data[item]);
    table.row(rowIdx).data(rowData).draw(false);
    var rowNode = table.row(function (idx, data, node) { if (data.id == id) return idx.toString(); }).node();
    RunAnimationEditRowNode(rowNode);
}

//Disabled key alphabet
function IsNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        ShowToastNoti('info', '', 'Input must be numeric!', 2000, 'bottomLeft');
        return false;
    }
    return true;
}

//Init editor content summernote
function InitEditorContent(element, name, placeHolder) {
    $(element).summernote({
        codeviewFilter: true,
        codeviewIframeFilter: true,
        placeholder: placeHolder,
        height: 230,
        lang: "vi-VN",
        callbacks: {
            onInit: function (e) {
                $(e.editor).find(".custom-control-description").addClass("custom-control-label").parent().removeAttr("for")
            },
            onChange: function (contents, $editable) {
                $(name).val(contents);
            },
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    $.upload(files[i]);
                }
            }
        }
    });
    if ($(name).val() != '') {
        $(element).summernote('code', $(name).val());
    }
    $.upload = function (file) {
        let out = new FormData();
        out.append('imgFile', file, file.name);
        $.ajax({
            method: 'POST',
            url: '/Common/ImageUpload',
            contentType: false,
            processData: false,
            cache: false,
            data: out,
            success: function (response) {
                if (response.result == 1 && response.data != null)
                    $(element).summernote('insertImage', response.data.smallUrl);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ShowToastNoti('error', '', _resultActionResource.ErrorAction);
            }
        });
    };
    $.ajax({
        url: "https://api.github.com/emojis",
        async: !1
    }).then(function (e) {
        window.emojis = Object.keys(e), window.emojiUrls = e
    });

    //Fix error: Cannot read properties of undefined (reading 'replace')
    $('form').each(function () {
        if ($(this).data('validator'))
            $(this).data('validator').settings.ignore = ".note-editor *";
    });
}

//Init editor content summernote with template
function InitEditorContentWithTemplate(element, name, placeHolder, templateButtons) {
    $(element).summernote({
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'video']],
            ['view', ['fullscreen', 'codeview', 'help']],
            ['template', Object.getOwnPropertyNames(templateButtons)]
        ],
        buttons: templateButtons,
        codeviewFilter: true,
        codeviewIframeFilter: true,
        placeholder: placeHolder,
        height: 230,
        lang: "vi-VN",
        callbacks: {
            onInit: function (e) {
                $(e.editor).find(".custom-control-description").addClass("custom-control-label").parent().removeAttr("for")
            },
            onChange: function (contents, $editable) {
                $(name).val(contents);
            },
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    $.upload(files[i]);
                }
            }
        }
    });
    if ($(name).val() != '') {
        $(element).summernote('code', $(name).val());
    }
    $.upload = function (file) {
        let out = new FormData();
        out.append('upload', file, file.name);
        $.ajax({
            method: 'POST',
            url: '/Common/ImageUpload',
            contentType: false,
            processData: false,
            cache: false,
            data: out,
            success: function (response) {
                if (response.result == 1 && response.data != null)
                    $(element).summernote('insertImage', response.data.smallUrl);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ShowToastNoti('error', '', _resultActionResource.ErrorAction);
            }
        });
    };
    $.ajax({
        url: "https://api.github.com/emojis",
        async: !1
    }).then(function (e) {
        window.emojis = Object.keys(e), window.emojiUrls = e
    });

    //Fix error: Cannot read properties of undefined (reading 'replace')
    $('form').each(function () {
        if ($(this).data('validator'))
            $(this).data('validator').settings.ignore = ".note-editor *";
    });
}

//Range value input number
function RangeValueInputNumber(element) {
    $(element).on('change keyup paste', function () {
        if (parseInt($(this).val()) > parseInt($(this).attr('max'))) {
            $(this).val($(this).attr('max'));
        }
        if (parseInt($(this).val()) < parseInt($(this).attr('min'))) {
            $(this).val($(this).attr('min'));
        }
    });
}

//Range numberic
function OnChangeRangeNumberic(elm) {
    let min = parseInt($(elm).attr('min'));
    let max = parseInt($(elm).attr('max'));
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//Range numberic max
function OnChangeRangeNumbericWithMaxElm(elm, elmMax) {
    let min = parseInt($(elm).attr('min'));
    let max = parseInt($(elmMax).val());
    if (isNaN(max)) {
        $(elm).val(0);
    }
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//Range numberic min
function OnChangeRangeNumbericWithMinElm(elm, elmMin) {
    let min = parseInt($(elmMin).val());
    let max = parseInt($(elm).attr('max'));
    if (isNaN(min)) {
        $(elm).val(0);
    }
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//Range min number min max
function OnChangeRangeNumberWithMinMax(elm, min, max) {
    if ($(elm).val() > max) {
        $(elm).val(max);
    } else if ($(elm).val() < min) {
        $(elm).val(min);
    }
}

//clear content modal
function clearDiv(id) {
    $(id).on('hidden.bs.modal', function () {
        setTimeout(function () {
            $(id).empty();
        }, 100)
    })
}

//Check validateion in lib Unobtrusive js
function CheckValidationUnobtrusive(formElm) {
    $.validator.unobtrusive.parse(formElm);
    formElm.validate();
    
    return formElm.valid();
}

//Scroll to top location page by div element
function ScrollToTop(el, margin, ms) {
    var speed = (ms) ? ms : 600;
    $('html,body').animate({
        scrollTop: $(el).offset().top - margin
    }, speed);
}

//Clean url
function CleanUrl(url) {
    return url.replace(/[^A-Za-z0-9_-]/g, '');
}

//Clean url
function ToSlugString(str) {
    str = str.replace(/^\s+|\s+$/g, ''); // trim
    str = str.toLowerCase();

    // remove accents, swap ñ for n, etc                                                                     
    var from = "ÁÄÂÀẢÃẠẦẤẨẪẬÃÅẶẲẴẰẮČÇĆĎĐÉĚËÈẺẼẸÊỀẾỂỄỆẼĔȆĞÍÌÎÏỊİỈŇÑÓÖÒỎÕỌÔỘÕƠỚỜỞỢỠỔỖỐỒØŘŔŠŞŤÚỦŨỤŮÜÙÛƯỪỨỬỮỰÝŸŽáàảãạäâầấẩẫậãåặắẳẵằčçćďđéèẻẽẹěëêềếểễệẽĕȇğíìỉịîïıňñóòỏõọộöơớờởợỡôốồõổốồøðřŕšşťúủũụůüùûưừứửữựýÿžþÞĐđßÆa·/_,:;";
    var to = "AAAAAAAAAAAAAAAAAAACCCDDEEEEEEEEEEEEEEEEGIIIIIIINNOOOOOOOOOOOOOOOOOOOORRSSTUUUUUUUUUUUUUUYYZaaaaaaaaaaaaaaaaaaacccddeeeeeeeeeeeeeeeegiiiiiiinnoooooooooooooooooooooorrsstuuuuuuuuuuuuuuyyzbBDdBAa------";
    for (var i = 0, l = from.length; i < l; i++) {
        str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
    }

    str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
        .replace(/\s+/g, '-') // collapse whitespace and replace by -
        .replace(/-+/g, '-'); // collapse dashes

    return str;
}

function SortByKeyDesc(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x > y) ? -1 : ((x < y) ? 1 : 0));
    });
}

function SortByKeyAsc(array, key) {
    return array.sort(function (a, b) {
        var x = a[key]; var y = b[key];
        return ((x < y) ? -1 : ((x > y) ? 1 : 0));
    });
}

function SetCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires;
}

function GetCookie(cname) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == cname) {
            return unescape(y);
        }
    }
}

function DeleteCookie(cname) {
    document.cookie = cname + "=" + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
}

function CopyValue(elm) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(elm).val()).select();
    document.execCommand("copy");
    $temp.remove();
    ShowToastNoti('success', '', 'Đã sao chép!', 2000, 'bottomRight');
}

function CopyText(text) {
    var textField = document.createElement('textarea');
    textField.innerText = text;
    document.body.appendChild(textField);
    textField.select();
    textField.focus(); //SET FOCUS on the TEXTFIELD
    document.execCommand('copy');
    textField.remove();
    ShowToastNoti('success', '', 'Đã sao chép!', 2000, 'bottomRight');
}

function IsNullOrEmty(string) {
    return string == null || string === "";
}

function SignOut(elm) {
    $(elm).attr('disabled', true);
    $('#sign_out_modal').modal('show');
    setTimeout(function () {
        $(elm).attr('disabled', false);
    }, 500);
}

function SignInAgain() {
    $('#modal_signin_again').modal('show');
}

function RemoveElementInArray(array, elem) {
    var index = array.indexOf(elem);
    if (index > -1) {
        array.splice(index, 1);
    }
    return array;
}

function HtmlEncodeString(str) {
    return String(str).replace(/[^\w. ]/gi, function (c) {
        return '&#' + c.charCodeAt(0) + ';';
    });
}

function HtmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function PrintElem(elem) {
    //var mywindow = window.open('', 'PRINT', 'height=400,width=600');
    let mywindow = window.open("");

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById(elem).innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}

function PrintHtml(html) {
    //var mywindow = window.open('', 'PRINT', 'height=400,width=600');
    let mywindow = window.open("");

    mywindow.document.write(html);

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    setTimeout(function () {
        mywindow.print();
        mywindow.close();
    }, 800);

    return true;
}

function PrintBase64PDF(base64, title) {
    let pdfWindow = window.open("");
    pdfWindow.document.write('<html><head><title>' + title + '</title>');
    pdfWindow.document.write('</head><body >');
    pdfWindow.document.write("<iframe width='100%' height='100%' src='data:application/pdf;base64, " + encodeURI(base64) + "'></iframe>");
    pdfWindow.document.write('</body></html>');
}

function encodeImageFileAsURL(element, viewDataElement) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        $(viewDataElement).val(reader.result);
    }
    reader.readAsDataURL(file);
}

function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

function FormatToVND(value) {
    if (IsNullOrEmty(value))
        return "NaN";
    //return value.toLocaleString('vi', { style: 'currency', currency: 'VND' }).replace(/\s₫/g, ' đ');
    return value.toLocaleString('vi', { style: 'currency', currency: 'VND' }).replace(/\s₫/g, '<sup>đ</sup>');
}

function FormatViewCurrency(elm) { // <input type="text" name="money" onkeypress="return IsNumberKey(event)" onpaste="event.preventDefault()" onkeyup="FormatViewCurrency(this)" />
    let value = $(elm).val();
    let breakS = ',';
    if (IsNullOrEmty(value)) return false;
    let subStringVal = parseInt(value.toString().replace(/,/g, "")).toString();
    let array = subStringVal.split('');
    let result = '';
    let dem = -1;
    for (var i = array.length - 1; i >= 0; i--) {
        dem++;
        if (dem % 3 === 0 && result.length > 2)
            result = array[i] + breakS + result;
        else
            result = array[i] + result;
    }
    dem = -1;
    $(elm).val(result);
}
function ClearFormatViewCurrencyToNumber(value) {
    let val = value.toString().replace(/,/g, "");
    return !IsNullOrEmty(val) ? parseInt(val) : 0;
}

function AddDaysInput(id, currentDate, addDays, format) {
    var date = new Date(currentDate);
    var day = date.addDays(addDays).getDate();// (date.getDate()+addDays);// + addDays;
    var month = date.addDays(addDays).getMonth() + 1;// date.getMonth() + 1;
    var year = date.addDays(addDays).getFullYear();
    var hour = date.addDays(addDays).getHours();
    var minutes = date.addDays(addDays).getMinutes();
    var seconds = date.addDays(addDays).getSeconds();

    if (month < 10)
        month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = '';
    if (format == 0)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 1)
        today = year + "-" + month + "-" + day;//yyyy-MM-dd
    if (format == 2)
        return finalDate = day + " " + month + "-" + year + "-" + hour + ":" + minutes;// dd MM-yyyy-HH:mm
    if (format == 3)
        today = day + "-" + month + "-" + year;//dd-MM-yyyy
    if (format == 4)
        today = year + "/" + month + "/" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 5)
        today = year + "/" + month + "/" + day;//yyyy/MM/dd 
    if (format == 6)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;//dd/MM/yyyy HH:mm
    if (format == 7)
        today = day + "/" + month + "/" + year;
    if (format == 8)
        today = month + "/" + day + "/" + year + " " + hour + ":" + minutes;//mm/dd/yyyy HH:mm
    if (format == 9)
        today = month + "/" + day + "/" + year;
    if (format == 10)
        today = year + "" + month + "" + day + "_" + hour + "" + minutes;//yyyyMMdd_HHmm
    if (format == 11)
        today = day + "/" + month + "/" + year;// + " " + hour + ":" + minutes;
    if (format == 12)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;
    if (format == 13)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes + ":" + seconds;//dd/mm/yyyy HH:mm:ss
    if (format == 14)
        today = day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds;//dd-mm-yyy HH:mm:ss
    if (format == 15)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds; //yyyy-mm- dd HH:mm:ss
    //var today = day + "/" + month + "/" + year;
    $(id).val(today);
}

function AddDaysDiv(id, currentDate, addDays, format) {
    var date = new Date(currentDate);
    var day = date.addDays(addDays).getDate();// (date.getDate()+addDays);// + addDays;
    var month = date.addDays(addDays).getMonth() + 1;// date.getMonth() + 1;
    var year = date.addDays(addDays).getFullYear();
    var hour = date.addDays(addDays).getHours();
    var minutes = date.addDays(addDays).getMinutes();
    var seconds = date.addDays(addDays).getSeconds();

    if (month < 10)
        month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = '';
    if (format == 0)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 1)
        today = year + "-" + month + "-" + day;//yyyy-MM-dd
    if (format == 2)
        return finalDate = day + " " + month + "-" + year + "-" + hour + ":" + minutes;// dd MM-yyyy-HH:mm
    if (format == 3)
        today = day + "-" + month + "-" + year;//dd-MM-yyyy
    if (format == 4)
        today = year + "/" + month + "/" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 5)
        today = year + "/" + month + "/" + day;//yyyy/MM/dd 
    if (format == 6)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;//dd/MM/yyyy HH:mm
    if (format == 7)
        today = day + "/" + month + "/" + year;
    if (format == 8)
        today = month + "/" + day + "/" + year + " " + hour + ":" + minutes;//mm/dd/yyyy HH:mm
    if (format == 9)
        today = month + "/" + day + "/" + year;
    if (format == 10)
        today = year + "" + month + "" + day + "_" + hour + "" + minutes;//yyyyMMdd_HHmm
    if (format == 11)
        today = day + "/" + month + "/" + year;// + " " + hour + ":" + minutes;
    if (format == 12)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;
    if (format == 13)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes + ":" + seconds;//dd/mm/yyyy HH:mm:ss
    if (format == 14)
        today = day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds;//dd-mm-yyy HH:mm:ss
    if (format == 15)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds; //yyyy-mm- dd HH:mm:ss
    //var today = day + "/" + month + "/" + year;
    $(id).text(today);
}

function AddMinutes(oldDate, diff) {
    return new Date(oldDate.getTime() + diff * 60000);
}

function ConvertDateTimeToUnixTimestamp(date) {
    return Math.floor(new Date(date).getTime() / 1000);
}

function ConvertUnixTimestampToDateTime(unix) {
    return new Date(unix * 1000);
}

function DiffDaysWithoutHour(fDate, tDate) {
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    return Math.round(Math.abs((new Date(fDate).setHours(0, 0, 0) - new Date(tDate).setHours(0, 0, 0)) / oneDay));
}

function GetParameterByNameInUrl(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function InitMagnificPopup(elm) {
    $(elm).magnificPopup({
        type: "image",
        closeOnContentClick: !1,
        closeBtnInside: !1,
        mainClass: "mfp-with-zoom mfp-img-mobile",
        image: {
            verticalFit: !0,
            titleSrc: function (e) {
                return e.el.attr("title")
            }
        },
        gallery: {
            enabled: !0
        },
        zoom: {
            enabled: !0,
            duration: 300,
            opener: function (e) {
                return e.find("img")
            }
        }
    });
}

function RemoveImageQuestion(elmImageId, elmImageView) {
    swal.fire({
        title: "Xóa ảnh?",
        text: "",
        icon: "question",
        showCancelButton: !0,
        confirmButtonText: "Xóa",
        cancelButtonText: "Không",
        confirmButtonClass: "btn btn-danger mt-2",
        cancelButtonClass: "btn btn-light mr-2 mt-2",
        reverseButtons: true,
        buttonsStyling: !1,
        showLoaderOnConfirm: true,
        preConfirm: function () {
            $(elmImageId).val(0);
            $(elmImageView).attr('src', '/images/error-avatar.png');
            $(elmImageView).parent('a').attr('href', '/images/error-avatar.png');
        }
    });
}

//get min/max value of arrays
function GetArrayMax(array) {
    return Math.max.apply(null, array);
}
function GetArrayMin(array) {
    return Math.min.apply(null, array);
}

function GetQueryParam(url) {
    var qs = url.substring(url.indexOf('?') + 1).split('&');
    for (var i = 0, result = {}; i < qs.length; i++) {
        qs[i] = qs[i].split('=');
        result[qs[i][0]] = decodeURIComponent(qs[i][1]);
    }
    return result;
}

function CheckUrlIsImage(url, timeoutT) {
    return new Promise(resolve => {
        if (url.match(/\.(jpeg|jpg|gif|png)$/)) {
            resolve(true);
        } else {
            var timeout = timeoutT || 5000;
            var timer, img = new Image();
            img.onerror = img.onabort = function () {
                clearTimeout(timer);
                resolve(false);
            };
            img.onload = function () {
                clearTimeout(timer);
                resolve(true);
            };
            timer = setTimeout(function () {
                // reset .src to invalid URL so it stops previous
                // loading, but doesn't trigger new load
                img.src = "//!!!!/test.jpg";
                reject("timeout");
            }, timeout);
            img.src = url;
        }
    });
}

function CheckUrlVideoIsAvailable(url) {
    return new Promise(resolve => {
        var video = document.createElement('video');
        video.src = url;
        video.onloadeddata = function () {
            resolve(true);
        }
        //video.onload = function () {
        //    console.log('is video');
        //    resolve(true);
        //}
        video.onerror = function () {
            resolve(false);
        }
    });
}

function GetFileNameInUrl(url) {
    return url.split('/').pop();
}

function GetFileExtension(filename) {
    return (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename) : undefined;
    //return filename.split('.').pop();
}

function ReplaceNonAccentVietnamese(str) {
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "")
    ////     We can also use this instead of from line 11 to line 17
    ////     str = str.replace(/\u00E0|\u00E1|\u1EA1|\u1EA3|\u00E3|\u00E2|\u1EA7|\u1EA5|\u1EAD|\u1EA9|\u1EAB|\u0103|\u1EB1|\u1EAF|\u1EB7|\u1EB3|\u1EB5/g, "a");
    ////     str = str.replace(/\u00E8|\u00E9|\u1EB9|\u1EBB|\u1EBD|\u00EA|\u1EC1|\u1EBF|\u1EC7|\u1EC3|\u1EC5/g, "e");
    ////     str = str.replace(/\u00EC|\u00ED|\u1ECB|\u1EC9|\u0129/g, "i");
    ////     str = str.replace(/\u00F2|\u00F3|\u1ECD|\u1ECF|\u00F5|\u00F4|\u1ED3|\u1ED1|\u1ED9|\u1ED5|\u1ED7|\u01A1|\u1EDD|\u1EDB|\u1EE3|\u1EDF|\u1EE1/g, "o");
    ////     str = str.replace(/\u00F9|\u00FA|\u1EE5|\u1EE7|\u0169|\u01B0|\u1EEB|\u1EE9|\u1EF1|\u1EED|\u1EEF/g, "u");
    ////     str = str.replace(/\u1EF3|\u00FD|\u1EF5|\u1EF7|\u1EF9/g, "y");
    ////     str = str.replace(/\u0111/g, "d");
    //str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    //str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    //str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    //str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    //str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    //str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    //str = str.replace(/đ/g, "d");
    //// Some system encode vietnamese combining accent as individual utf-8 characters
    //str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // Huyền sắc hỏi ngã nặng 
    //str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // Â, Ê, Ă, Ơ, Ư
    //return str;
}

function DateToYYYYMMDD(date, char = '-') {
    if (IsNullOrEmty(date)) return '';
    let split = date.split(char);
    return split[2] + '-' + split[1] + '-' + split[0];
}

function RemoveItemInArray(arr, item) {
    return arr.splice(arr.indexOf(item), 1);;
}

function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
    var R = 6371; // Radius of the earth in km
    var dLat = deg2rad(lat2 - lat1);  // deg2rad below
    var dLon = deg2rad(lon2 - lon1);
    var a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
        ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c; // Distance in km
    return d;
}
function deg2rad(deg) {
    return deg * (Math.PI / 180)
}

//Format date to format view social min
function FormatDateToViewSocialMin(date) {
    let res = '';
    let cur = new Date(date);
    let dateNow = new Date();
    let minus = Number((dateNow - cur) / 1000);
    let minusMinutes = Math.floor(minus / 60);
    let minusHours = Math.floor(minus / 3600);
    let minusDays = Math.floor(minus / (3600 * 24));
    let minusWeeks = Math.floor(minus / (3600 * 24 * 7));
    let minusMonths = Math.floor(minus / (3600 * 24 * 30));
    let minusYears = Math.floor(minus / (3600 * 24 * 365));
    if (dateNow < cur) //Exception
        return moment(date).format('HH:mm DD-MM-YYYY');
    if (minusMinutes === 0)
        res = "Vừa xong";
    else if (minusMinutes >= 1 && minusMinutes <= 60)
        res = minusMinutes + " phút";
    else if (minusHours >= 1 && minusHours <= 24)
        res = minusHours + " giờ";
    else if (minusDays === 1)
        res = "Hôm qua";
    else if (minusDays >= 2 && minusDays <= 7)
        res = minusDays + " ngày";
    else if (minusWeeks >= 1 && minusWeeks <= 4)
        res = minusWeeks + " tuần";
    else if (minusMonths >= 1 && minusMonths <= 12)
        res = minusMonths + " tháng";
    else
        res = minusYears + " năm";
    return res;
}

//Format date to format view social
function FormatDateToViewSocial(date) {
    let res = '';
    let cur = new Date(date);
    let dateNow = new Date();
    let dateMonth = dateNow.getMonth();
    let dateDate = dateNow.getDate();
    let minus = Number((dateNow - cur) / 1000);
    let minusHours = Math.floor(minus / 3600);
    let minusMinutes = Math.floor(minus % 3600 / 60);
    let minusSeconds = Math.floor(minus % 3600 % 60);
    if (dateNow < cur) //Exception
        return moment(date).format('DD-MM-YYYY HH:mm');

    if (cur.getFullYear() === dateNow.getFullYear()) { //Year now
        if (cur.getMonth() === dateMonth) { //Month now
            if (cur.getDate() === dateDate) { //Date now
                if (minusHours === 0) { //Hours now
                    if (minusMinutes === 0) { //Minutes now
                        res = "Vừa xong";
                    } else { //Minutes ago
                        res = minusMinutes + " phút trước";
                    }
                } else { //Hours ago
                    res = minusHours + " giờ trước";
                    //res = "Hôm nay " + moment(date).format('HH:mm');
                }
            } else if (cur.getDate() === (dateDate - 1)) { //Yesterday
                res = "Hôm qua " + moment(date).format('HH:mm');
            } else { //Current month
                res = moment(date).format('DD') + " tháng " + moment(date).format('MM') + " lúc " + moment(date).format('HH:mm');
            }
        } else { //Month ago
            res = moment(date).format('DD') + " tháng " + moment(date).format('MM') + " lúc " + moment(date).format('HH:mm');
        }
    } else { //Year ago
        res = moment(date).format('DD-MM-YYYY HH:mm');
    }
    return res;
}

//Focus last character in element  | $(el).get(0) -> allowed div contenteditable
function PlaceCaretAtEnd(el) {
    el.focus();
    if (typeof window.getSelection != "undefined"
        && typeof document.createRange != "undefined") {
        var range = document.createRange();
        range.selectNodeContents(el);
        range.collapse(false);
        var sel = window.getSelection();
        sel.removeAllRanges();
        sel.addRange(range);
    } else if (typeof document.body.createTextRange != "undefined") {
        var textRange = document.body.createTextRange();
        textRange.moveToElementText(el);
        textRange.collapse(false);
        textRange.select();
    }
}

//Focus last character in element  | $(el).get(0) -> allowed input
function PlaceCaretAtEnd2(elem) {
    var elemLen = elem.value.length;
    // For IE Only
    if (document.selection) {
        // Set focus
        elem.focus();
        // Use IE Ranges
        var oSel = document.selection.createRange();
        // Reset position to 0 & then set at end
        oSel.moveStart('character', -elemLen);
        oSel.moveStart('character', elemLen);
        oSel.moveEnd('character', 0);
        oSel.select();
    }
    else if (elem.selectionStart || elem.selectionStart == '0') {
        // Firefox/Chrome
        elem.selectionStart = elemLen;
        elem.selectionEnd = elemLen;
        elem.focus();
    } // if
}

function GetParamsInCurrentUrlQuery(key) {
    let params = (new URL(document.location)).searchParams;
    return params.get(key);
}

//Format money
function NumberWithCommas(n, f = ',') {
    var parts = n.toString().split(".");
    if (f == ',') {
        return parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (parts[1] ? "." + parts[1] : "");
    }
    if (f == '.') {
        return parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ".") + (parts[1] ? "," + parts[1] : "");
    }
}

function FormatDisplayMoney(num, digit = 2) {
    let ratio = 1000;
    let res = '';
    if (num / (1000 * ratio) >= 1) {
        res = NumberWithCommas(GetFlooredFixed(num / (1000 * ratio), digit), ',') + ' Tỷ';
    } else if (num / (1 * ratio) >= 1) {
        res = NumberWithCommas(GetFlooredFixed(num / (1 * ratio), digit), ',') + ' Triệu';
    } else {
        res = NumberWithCommas(GetFlooredFixed(num * ratio, 0), ',') + 'đ';
    }
    return res;
}
function CountLengthNumberDecimalPart(number) {
    return (number + '').split('.')[1]?.length ?? 0;
}
function GetFlooredFixed(v, d) {
    var curDigit = CountLengthNumberDecimalPart(v);
    return +(Math.floor(v * Math.pow(10, d)) / Math.pow(10, d)).toFixed(curDigit < d ? curDigit : d);
    //return +(+(Math.round(+(v + 'e' + d)) + 'e' + -d)).toFixed(curDigit < d ? curDigit : d);
}

function ConvertNumberToCurrency(elm, char = '.') {
    let text = '';
    let parts = $(elm).val().toString().split(char);
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, char);
    text += parts.join(char);
    $(elm).parents('.input-group').find('.view_currency').html(text);
    //HTML
    //<div class="input-group">
    //<input asp-for="salaryAllowance" type="number" class="form-control" onkeypress="return IsNumberKey(event)" placeholder="Nhập phụ cấp" onkeyup="ConvertNumberToCurrency(this)" required pattern="\d*">
    //<div class="input-group-append">
    //<button class="btn btn-light waves-effect waves-light px-1" type="button"><span class="view_currency"></span> VNĐ</button>
    //</div>
    //</div>
}

Date.prototype.addDays = function (num) {
    var value = this.valueOf();
    value += 86400000 * num;
    return new Date(value);
};

Date.prototype.addSeconds = function (num) {
    var value = this.valueOf();
    value += 1000 * num;
    return new Date(value);
};

Date.prototype.addMinutes = function (num) {
    var value = this.valueOf();
    value += 60000 * num;
    return new Date(value);
};

Date.prototype.addHours = function (num) {
    var value = this.valueOf();
    value += 3600000 * num;
    return new Date(value);
};

Date.prototype.addMonths = function (num) {
    var value = new Date(this.valueOf());

    var mo = this.getMonth();
    var yr = this.getYear();

    mo = (mo + num) % 12;
    if (0 > mo) {
        yr += (this.getMonth() + num - mo - 12) / 12;
        mo += 12;
    }
    else
        yr += (this.getMonth() + num - mo) / 12;

    value.setMonth(mo);
    value.setYear(yr);
    return value;
}

function SetDefault_date(id, addDays, format) {
    let date = new Date();
    let day = date.addDays(addDays).getDate();// (date.getDate()+addDays);// + addDays;
    let month = date.addDays(addDays).getMonth() + 1;// date.getMonth() + 1;
    let year = date.addDays(addDays).getFullYear();
    let hour = date.addDays(addDays).getHours();
    let minutes = date.addDays(addDays).getMinutes();
    let seconds = date.addDays(addDays).getSeconds();

    if (month < 10)
        month = "0" + month;
    if (day < 10) day = "0" + day;

    let today = '';
    if (format == 0)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 1)
        today = year + "-" + month + "-" + day;//yyyy-MM-dd
    if (format == 2)
        return finalDate = day + " " + month + "-" + year + "-" + hour + ":" + minutes;// dd MM-yyyy-HH:mm
    if (format == 3)
        today = day + "-" + month + "-" + year;//dd-MM-yyyy
    if (format == 4)
        today = year + "/" + month + "/" + day + " " + hour + ":" + minutes;//yyyy/mm/dd HH:mm
    if (format == 5)
        today = year + "/" + month + "/" + day;//yyyy/MM/dd 
    if (format == 6)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;//dd/MM/yyyy HH:mm
    if (format == 7)
        today = day + "/" + month + "/" + year;//dd/MM/yyyy
    if (format == 8)
        today = month + "/" + day + "/" + year + " " + hour + ":" + minutes;//mm/dd/yyyy HH:mm
    if (format == 9)
        today = month + "/" + day + "/" + year;//MM/dd/yyyy
    if (format == 10)
        today = year + "" + month + "" + day + "_" + hour + "" + minutes;//yyyyMMdd_HHmm
    if (format == 11)
        today = day + "/" + month + "/" + year;// + " " + hour + ":" + minutes;
    if (format == 12)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes;
    if (format == 13)
        today = day + "/" + month + "/" + year + " " + hour + ":" + minutes + ":" + seconds;//dd/mm/yyyy HH:mm:ss
    if (format == 14)
        today = day + "-" + month + "-" + year + " " + hour + ":" + minutes + ":" + seconds;//dd-mm-yyy HH:mm:ss
    if (format == 15)
        today = year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + seconds; //yyyy-mm- dd HH:mm:ss
    if (format == 16)
        today = year + "-" + month; //yyyy-mm
    //var today = day + "/" + month + "/" + year;
    $(id).val(today);
}

function SeparateFirstNameLastNameByName(s, isVN = 1) {
    let firstName = '';
    let lastName = '';
    if (!IsNullOrEmty(s)) {
        if (isVN === 1) {
            let names = s.trim().split(' ');
            if (names.length === 1) {
                lastName = '';
                firstName = names[0];
            } else {
                firstName = names[names.length - 1];
                names.splice(names.length - 1, 1);
                lastName = names.join(' ');
            }
        }
    }
    return { firstName: firstName, lastName: lastName };
}

function ConvertDateToDayMonthYear(date, currentFormat = 'DD/MM/YYYY', separate = '-') {
    if (IsNullOrEmty(date)) return '';
    let split = date.split(separate);
    let res = {};
    switch (currentFormat) {
        case 'DD/MM/YYYY':
            res.day = split[0];
            res.month = split[1];
            res.year = split[2];
            break;
        case 'YYYY/MM/DD':
            res.day = split[2];
            res.month = split[1];
            res.year = split[3];
            break;
        default:
            break;
    }
    return res;
}

function GetContentsCKEditor(elm) {
    // Get the editor instance that you want to interact with.
    var editor = CKEDITOR.instances[elm];

    // Get editor content.
    // https://ckeditor.com/docs/ckeditor4/latest/api/CKEDITOR_editor.html#method-getData
    return editor.getData();
}

function InitCKEditorFull(elm) {
    CKEDITOR.config.pasteFromWordPromptCleanup = true;
    CKEDITOR.config.pasteFromWordRemoveFontStyles = false;
    CKEDITOR.config.pasteFromWordRemoveStyles = false;
    CKEDITOR.config.htmlEncodeOutput = false;
    CKEDITOR.config.ProcessHTMLEntitles = false;
    CKEDITOR.config.entitles = false;
    CKEDITOR.config.entitles_latin = false;
    CKEDITOR.config.ForceSimpleAmpersand = true;
    //CKEDITOR.config.versionCheck = false;
    CKEDITOR.replace(elm, {
        extraPlugins: 'image2,embed,emoji',
        height: 400,
        image2_disableResizer: true,
        uploadUrl: '/Common/ImageUploadCkEditor',
        //filebrowserBrowseUrl: '/Common/ImageUploadCkEditor',
        //filebrowserImageBrowseUrl: '/Common/ImageUploadCkEditor',
        filebrowserUploadUrl: '/Common/ImageUploadCkEditor',
        filebrowserImageUploadUrl: '/Common/ImageUploadCkEditor',
        removeDialogTabs: 'image:advanced;link:advanced',
        toolbar: [
            { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Scayt'] },
            { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
            { name: 'insert', items: ['Image', 'Embed', 'Table', 'HorizontalRule', 'SpecialChar', 'EmojiPanel'] },
            { name: 'tools', items: ['Maximize'] },
            { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source'] },
            { name: 'others', items: ['-'] },
            '/',
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock',] },
            { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
            { name: 'colors', items: ['TextColor', 'BGColor'] },
        ],
        language: 'vi',
        removeButtons: 'Underline,Strike,Subscript,Superscript,Styles,Specialchar,PasteFromWord'
    });
}

function InitComponentForm() {
    try {
        $('.modal').modal();
        $('select.form-select').formSelect();
        $('[maxlength]').maxlength({
            alwaysShow: !0,
        });
        $('.tooltipped').tooltip();
        $('.select2').select2({ language: "vi" });
    } catch (e) {

    }
}

const popupCenter = ({ url, title, w, h }) => {
    // Fixes dual-screen position                             Most browsers      Firefox
    const dualScreenLeft = window.screenLeft !== undefined ? window.screenLeft : window.screenX;
    const dualScreenTop = window.screenTop !== undefined ? window.screenTop : window.screenY;

    const width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    const height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    const systemZoom = width / window.screen.availWidth;
    const left = (width - w) / 2 / systemZoom + dualScreenLeft
    const top = (height - h) / 2 / systemZoom + dualScreenTop
    const newWindow = window.open(url, title,
        `
      scrollbars=yes,
      width=${w / systemZoom}, 
      height=${h / systemZoom}, 
      top=${top}, 
      left=${left}
      `
    )

    if (window.focus) newWindow.focus();
}

function InitDatePicker(elm, format = "dd/mm/yyyy") {
    $(elm).datepicker({
        autoClose: !0,
        format: format,
        container: "body",
        i18n: {
            cancel: 'Đóng',
            clear: 'Làm mới',
            done: 'OK',
            previousMonth: '‹',
            nextMonth: '›',
            months: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
            monthsShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
            weekdays: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
            weekdaysShort: ["CN", "T.Hai", "T.Ba", "T.Tư", "T.Năm", "T.Sáu", "T.Bảy"],
            weekdaysAbbrev: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
        },
        onDraw: function () {
            $(".datepicker-container").find(".datepicker-select").addClass("browser-default"), $(".datepicker-container .select-dropdown.dropdown-trigger").remove()
        }
    });
}

function OpenPopupFilesInfoSingle(elmInput = '.image_url', elmImg = '.image_url') {
    popupCenter({ url: `/data/filebrowse-single/${elmInput}/${elmImg}`, title: 'Files Info', w: 800, h: 400 });
}

function UploadFile(elm, elmInput = '.image_url', elmImg = '.image_url', elmImageId = '', type = 'images') {
    let formData = new FormData();
    formData.append("upload", $(elm)[0].files[0]);
    ShowOverlay($(elm).parent());
    $.ajax({
        url: '/Common/ImageUpload',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            HideOverlay($(elm).parent());
            if (!CheckResponseIsSuccess(response)) return false;
            if (response.data != null) {
                $(elmInput).val(response.data.relativeUrl);
                $(elmImg).attr('src', response.data.relativeUrl);
                if (!IsNullOrEmty(elmImageId))
                    $(elmImageId).val(response.data.id);
            }
        }, error: function (err) {
            HideOverlay($(elm).parent());
            CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
        }
    });
}

function OnchangeInputImageUrl(elm) {
    $(elm).on('change', function () {
        let url = $(this).val();
        $(this).parent().parent().parent().find('img').attr('src', url);
    });
}

function CallbackChooseImageOnServer(url, elmInput = '.image_url', elmImg = '.image_url') {
    $(elmInput).val(url);
    $(elmImg).attr('src', url);
}


async function DownloadByLink(imageSrc, nameOfDownload = 'name.extension') {
    const response = await fetch(imageSrc);

    const blobImage = await response.blob();

    const href = URL.createObjectURL(blobImage);

    const anchorElement = document.createElement('a');
    anchorElement.href = href;
    anchorElement.download = nameOfDownload;

    document.body.appendChild(anchorElement);
    anchorElement.click();

    document.body.removeChild(anchorElement);
    window.URL.revokeObjectURL(href);
}