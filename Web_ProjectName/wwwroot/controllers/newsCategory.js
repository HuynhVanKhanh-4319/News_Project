
let dataTable;


let $form_create_news_category;
let $btn_toggle_form;
let $btn_submit_create;
let $btn_submit_update;
let $newsCategoryTable_wrapper;
let $create_form_container;


const apiUrlGetList = '/NewsCategory/GetList';
const apiUrlCreate = '/NewsCategory/Create';
const apiUrlUpdate = '/NewsCategory/Update';
const apiUrlDelete = '/NewsCategory/UpdateStatus';
const apiUrlGetById = '/NewsCategory/GetById';

$(document).ready(function () {
 
    $form_create_news_category = $('#form_create_news_category');
    $btn_toggle_form = $('#btn_toggle_form');
    $btn_submit_create = $('#btn_submit_create');
    $btn_submit_update = $('#btn_submit_update');
    $newsCategoryTable_wrapper = $('#newsCategoryTable_wrapper');
    $create_form_container = $('#create_form_container');

   
    dataTable = LoadNewsCategoryTable();

  
    BindFormToggleEvents();
    BindCreateFormSubmit(dataTable);
    bindDeleteNewsCategoryEvent(dataTable);
    bindEditNewsCategoryEvent(dataTable);

   
    InitMaxlengthForm();
});


function InitMaxlengthForm() {
    $('#Name').maxlength({
        warningClass: "badge bg-success",
        limitReachedClass: "badge bg-danger",
        threshold: 10,
        placement: 'bottom-right-inside'
    });

    $('#Remark').maxlength({
        warningClass: "badge bg-success",
        limitReachedClass: "badge bg-danger",
        threshold: 20,
        placement: 'bottom-right-inside'
    });
}

const dataParamsNewsCategory = function () {
    $.fn.dataTable.ext.errMode = 'none';
    return {
        type: 'GET',
        url: apiUrlGetList,
        data: function (d) {
            d.status = 1;
        },
        dataType: 'json',
        beforeSend: function () { },
        dataSrc: function (response) {
            if (response && response.isSuccess !== false && Array.isArray(response.data)) {
                return response.data;
            }
            return [];
        },
        error: function (xhr, status, error) {
            console.error("Error loading data:", error);
            ShowToastNoti('warning', '', 'Lỗi không kết nối tới máy chủ!');
            return [];
        }
    };
}


const columnNewsCategory = function () {
    return [
        { data: "name", className: "text-left", responsivePriority: 1 },
        { data: "nameSlug", className: "text-left", responsivePriority: 2 },
        { data: "remark", className: "text-left", responsivePriority: 3 },
        {
            data: null,
            orderable: false,
            className: "text-center",
            responsivePriority: 4,
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
        }
    ];
}


function LoadNewsCategoryTable() {
    if (typeof dataTable !== "undefined" && dataTable) {
        dataTable.destroy();
    }

    let options = {
        dom: '<"top"lfB><"clear">tr<"bottom"ip>',
        paging: true,
        processing: true,
        serverSide: false,
        scrollX: true,
        responsive: false,
        autoWidth: true,
        order: [[0, 'asc']],
        ajax: dataParamsNewsCategory(),
        columns: columnNewsCategory(),
        language: { url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/vi.json' },
        fixedColumns: { left: 1 },
        initComplete: function () {
            this.api().columns.adjust();
            $(window).trigger('resize');
        }
    };

    dataTable = $('#news_category_table').DataTable(options);

    $(window).on('resize', function () {
        if (dataTable) {
            setTimeout(function () {
                dataTable.columns.adjust().draw(false);
            }, 100);
        }
    });

    return dataTable;
}


function BindFormToggleEvents() {
    $btn_toggle_form.click(function () {
        $form_create_news_category[0].reset();
        $btn_submit_create.show();
        $btn_submit_update.hide();
        $newsCategoryTable_wrapper.hide();
        $create_form_container.slideDown();
    });

    $(document).on('click', '#btn_cancel_create', function () {
        $create_form_container.slideUp();
        $newsCategoryTable_wrapper.show();
    });
}


function BindCreateFormSubmit(table) {
    $form_create_news_category.submit(function (e) {
        e.preventDefault();

        const model = {
            name: $('#Name').val(),
            remark: $('#Remark').val(),
            status: $('#Status').val()
        };

        $.ajax({
            url: apiUrlCreate,
            type: 'POST',
            data: model,
            success: function (res) {
                if (res.result === 1) {
                    ShowToastNoti('success', '', 'Tạo thành công!');
                    $form_create_news_category[0].reset();
                    InitMaxlengthForm();
                    table.ajax.reload(null, false);
                    $create_form_container.slideUp();
                    $newsCategoryTable_wrapper.show();
                    $btn_submit_create.show();
                    $btn_submit_update.hide();
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
    $('#news_category_table').on('click', '.btn-delete', function () {
        const id = $(this).data('id');
        if (!id) return;

        Swal.fire({
            title: 'Bạn có chắc chắn?',
            text: "Danh mục này sẽ bị xoá!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Vâng, xoá!',
            cancelButtonText: 'Huỷ'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: apiUrlDelete,
                    type: 'POST',
                    data: { id: id },
                    success: function (res) {
                        if (res.success) {
                            Swal.fire('Đã xoá!', 'Danh mục đã được xoá.', 'success');
                            table.ajax.reload(null, false);
                        } else {
                            Swal.fire('Thất bại!', res.message || 'Không thể xoá danh mục.', 'error');
                        }
                    },
                    error: function () {
                        Swal.fire('Lỗi!', 'Đã xảy ra lỗi khi xoá.', 'error');
                    }
                });
            }
        });
    });
}


function bindEditNewsCategoryEvent(table) {
    $('#news_category_table').on('click', '.btn-edit', function () {
        const id = $(this).data('id');

        $.ajax({
            url: apiUrlGetById,
            type: 'GET',
            data: { id: id },
            success: function (res) {
                if (res && res.data) {
                    const item = res.data;
                    $('#Id').val(item.id);
                    $('#Name').val(item.name);
                    $('#Remark').val(item.remark);
                    $('#Status').val(item.status);
                    InitMaxlengthForm();
                    $btn_submit_create.hide();
                    $btn_submit_update.show();
                    $create_form_container.slideDown();
                    $newsCategoryTable_wrapper.hide();
                } else {
                    ShowToastNoti('warning', '', 'Không lấy được dữ liệu cần sửa');
                }
            },
            error: function () {
                ShowToastNoti('warning', '', 'Lỗi khi lấy thông tin danh mục');
            }
        });
    });

    $btn_submit_update.off('click').on('click', function (e) {
        e.preventDefault();

        const model = {
            id: $('#Id').val(),
            name: $('#Name').val(),
            remark: $('#Remark').val(),
            status: $('#Status').val(),
        };

        $.ajax({
            url: apiUrlUpdate,
            type: 'POST',
            data: model,
            success: function (res) {
                if (res.result === 1 || res.status === true) {
                    ShowToastNoti('success', '', 'Cập nhật thành công!');
                    $form_create_news_category[0].reset();
                    InitMaxlengthForm();
                    $btn_submit_create.show();
                    $btn_submit_update.hide();
                    $create_form_container.slideUp();
                    $newsCategoryTable_wrapper.show();
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
