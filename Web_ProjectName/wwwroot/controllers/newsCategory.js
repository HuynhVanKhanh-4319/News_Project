
$(document).ready(function () {
    const table = initNewsCategoryTable();

    bindFormToggleEvents();
    bindCreateFormSubmit(table);
    bindDeleteNewsCategoryEvent(table);
    bindEditNewsCategoryEvent(table);
    bindEditNewsCategoryEvent(table);
});

function initNewsCategoryTable() {
    const status = 1;
    $.fn.dataTable.ext.errMode = 'none';

    return $('#newsCategoryTable').DataTable({
        ajax: {
            url: '/NewsCategory/GetList?status=' + status,
            dataSrc: function (json) {
                if (!json || json.isSuccess === false || !Array.isArray(json.data)) {
                    console.warn("API trả lỗi hoặc dữ liệu sai:", json?.message || json);
                    return [];
                }
                return json.data;
            },
            error: function (xhr, status, error) {
                ShowToastNoti('warning', '', 'Lỗi không kết nối tới máy chủ!');
            }
        },
        columns: [
            { data: 'name' },
            { data: 'nameSlug' },
            { data: 'remark' },
            {
                data: null,
                title: 'Hành động',
                orderable: false,
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-sm btn-warning btn-edit" data-id="${row.id}" title="Chỉnh sửa">
                            <i class="fas fa-edit"></i>
                        </button>
                        <button class="btn btn-sm btn-danger btn-delete" data-id="${row.id}" title="Xoá">
                            <i class="fas fa-trash-alt"></i>
                        </button>
                    `;
                }
            },
        ],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/vi.json'
        },
        processing: true,
        serverSide: false,
        responsive: true
    });
}

function bindFormToggleEvents() {
    $('#btnToggleForm').click(function () {
        $('#newsCategoryTable_wrapper').hide();
        $('#createFormContainer').slideDown();
    });

    $(document).on('click', '#btnCancelCreate', function () {
        $('#createFormContainer').slideUp();
        $('#newsCategoryTable_wrapper').show();
    });
}

function bindCreateFormSubmit(table) {
    $('#formCreateNewsCategory').submit(function (e) {
        e.preventDefault();

        const model = {
            name: $('#Name').val(),
            remark: $('#Remark').val(),
            status: $('#Status').val()
        };

        $.ajax({
            url: '/NewsCategory/Create',
            type: 'POST',
            data: model,
            success: function (res) {
                if (res.result === 1) {
                    ShowToastNoti('success', '', 'Tạo thành công!');
                    $('#formCreateNewsCategory')[0].reset();
                    table.ajax.reload(null, false);
                    $('#createFormContainer').slideUp();
                    $('#newsCategoryTable_wrapper').show();
                } else {
                    ShowToastNoti('warning', '', 'Tạo thất bại!');
                }
            },
            error: function () {
                ShowToastNoti('warning', '', 'Lỗi khi gọi API!');
            }
        });
    });
}

function bindDeleteNewsCategoryEvent(table) {
    $('#newsCategoryTable').on('click', '.btn-delete', function () {
        const id = $(this).data('id');

        if (confirm("Bạn có chắc chắn muốn xoá danh mục này không?")) {
            $.ajax({
                url: '/NewsCategory/UpdateStatus',
                type: 'POST',
                data: { id: id },
                success: function (res) {
                    if (res.success) {
                        ShowToastNoti('success', '', 'Xoá thành công!');
                        table.ajax.reload(null, false);
                    } else {
                        ShowToastNoti('warning', '', 'Xoá thất bại!');
                    }
                },
                error: function () {
                    ShowToastNoti('warning', '', 'Đã xảy ra lỗi khi xoá.');
                }
            });
        }
    });
}
function bindEditNewsCategoryEvent(table) {
    $('#newsCategoryTable').on('click', '.btn-edit', function () {
        const id = $(this).data('id');

        $.ajax({
            url: '/NewsCategory/GetById',
            type: 'GET',
            data: { id: id },
            success: function (res) {
                if (res && res.data) {
                    const item = res.data;

   
                    $('#Id').val(item.id);
                    $('#Name').val(item.name);
                    $('#Remark').val(item.remark);
                    $('#Status').val(item.status);
                    $('#btnSubmitCreate').hide();
                    $('#btnSubmitUpdate').show();
                    $('#createFormContainer').slideDown();
                    $('#newsCategoryTable_wrapper').hide();
                } else {
                    ShowToastNoti('warning', '', 'Không lấy được dữ liệu cần sửa');
                }
            },
            error: function () {
                ShowToastNoti('warning', '', 'Lỗi khi lấy thông tin danh mục');
            }
        });
    });

    // Submit update
    $('#btnSubmitUpdate').click(function (e) {
        e.preventDefault();

        const model = {
            id: $('#Id').val(),
            name: $('#Name').val(),
            remark: $('#Remark').val(),
            status: $('#Status').val(),
        };

        $.ajax({
            url: '/NewsCategory/Update',
            type: 'POST',
            data: model,
            success: function (res) {
                if (res.result === 1 || res.status === true) {
                    ShowToastNoti('success', '', 'Cập nhật thành công!');
                    $('#formCreateNewsCategory')[0].reset();
                    $('#btnSubmitCreate').show();
                    $('#btnSubmitUpdate').hide();
                    $('#createFormContainer').slideUp();
                    $('#newsCategoryTable_wrapper').show();
                    table.ajax.reload(null, false);
                } else {
                    ShowToastNoti('warning', '', 'Cập nhật thất bại!');
                }
            },
            error: function () {
                ShowToastNoti('warning', '', 'Lỗi khi cập nhật!');
            }
        });
    });
}


