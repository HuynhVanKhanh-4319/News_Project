
$(document).ready(function () {
    LoadNewsCategories($('#filterCategory'));
    InitNewsTable();
    HandleCreateNews();
    BindCreateModalEvents();
    BindDeleteNewsEvent();
    BindViewDetailEvent();
    BindEditNewsEvent();
    $('<style>.cke_notification_warning { display: none !important; }</style>').appendTo('head');
    $('#modal_update_news').on('hidden.bs.modal', function () {
        const $btnOpen = $('#btnOpenUpdateNewsModal');
        if ($btnOpen.length) {

            $btnOpen.focus();
        } else {
            $('body').focus();
        }
    });
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
}
function EscapeHtml(text) {
    if (!text) return '';
    return text
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function TruncateWithTooltip(text, maxLength = 20) {
    if (!text) return '';
    const truncated = text.length > maxLength ? text.substring(0, maxLength) + '...' : text;
    return `<span data-bs-toggle="tooltip" data-bs-placement="top" title="${EscapeHtml(text)}">${EscapeHtml(truncated)}</span>`;
}

const dataParamsNews = function () {
    $.fn.dataTable.ext.errMode = 'none';
    return {
        type: 'GET',
        url: '/News/GetList',
        traditional: true, 
        data: function (d) {
            d.status = [0, 1];
            d.categoryId = $('#filterCategory').val() || null;
        },
        dataType: 'json',
        dataSrc: function (json) {
            if (!json || !Array.isArray(json.data)) return [];
            return json.data;
        },
        error: function (xhr, status, error) {
            console.error("Error loading data:", error);
            ShowToastNoti('warning', '', 'Lỗi không kết nối tới máy chủ!');
            return [];
        }
    };
};

const columnNews = function () {
    return [
        {
            data: 'name',
            title: 'Tiêu đề',
            render: function (data) {
                return TruncateWithTooltip(data);
            }
        },
        {
            data: 'description',
            title: 'Mô tả',
            render: function (data) {
                return TruncateWithTooltip(data);
            }
        },
        {
            data: 'newsCategoryObj.name',
            title: 'Danh mục',
            render: function (data) {
                return EscapeHtml(data || '(Không có)');
            }
        },
        {
            data: 'status',
            title: 'Trạng thái',
            render: function (data, type, row) {
                const status = parseInt(data, 10);
                const btnClass = status === 1 ? 'btn-success' : 'btn-danger';
                const btnText = status === 1 ? 'Hoạt động' : 'Không hoạt động';
                return `<button class="btn btn-sm ${btnClass} btn-toggle-status" data-id="${row.id}" data-status="${status}">${btnText}</button>`;
            }
        },
        {
            data: null,
            orderable: false,
            render: function (row) {
                return `
                    <button class="btn btn-info btn-sm btn-view" data-id="${row.id}" title="Xem chi tiết"><i class="fas fa-eye"></i></button>
                    <button class="btn btn-warning btn-sm btn-edit" data-id="${row.id}" title="Chỉnh sửa"><i class="fas fa-edit"></i></button>
                    <button class="btn btn-sm btn-danger btn-delete" data-id="${row.id}" title="Xoá"><i class="fas fa-trash-alt"></i></button>
                `;
            }
        }
    ];
};

function InitNewsTable() {
    if (typeof newsTable !== "undefined" && newsTable) {
        newsTable.destroy();
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
        ajax: dataParamsNews(),
        columns: columnNews(),
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/vi.json'
        },
        fixedColumns: {
            left: 1
        },
        initComplete: function () {
            this.api().columns.adjust();
            $(window).trigger('resize');
        }
    };

    newsTable = $('#news_table').DataTable(options);

    $('#filterCategory').on('change', function () {
        newsTable.ajax.reload();
    });

    $('#news_table').on('click', '.btn-toggle-status', function () {
        const id = $(this).data('id');
        const currentStatus = $(this).data('status');
        const newStatus = currentStatus === 1 ? 0 : 1;
        const statusText = newStatus === 1 ? 'Hoạt động' : 'Không hoạt động';

        Swal.fire({
            title: 'Xác nhận thay đổi?',
            text: `Bạn có chắc muốn đổi trạng thái sang "${statusText}"?`,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Huỷ'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/News/UpdateStatus',
                    type: 'POST',
                    data: { id: id, status: newStatus },
                    success: function (res) {
                        if (res.success) {

                            Swal.fire('Thành công!', 'Trạng thái đã được cập nhật.', 'success');
                            newsTable.ajax.reload(null, false);
                        } else {
                            Swal.fire('Thất bại!', res.message || 'Không thể cập nhật trạng thái.', 'error');
                        }
                    },
                    error: function () {
                        Swal.fire('Lỗi!', 'Lỗi khi gọi API cập nhật trạng thái.', 'error');
                    }
                });
            }
        });
    });

    $('#news_table').on('draw.dt', function () {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    });

    $(window).on('resize', function () {
        if (newsTable) {
            setTimeout(function () {
                newsTable.columns.adjust().draw(false);
            }, 100);
        }
    });

    return newsTable;
}


function LoadNewsCategories($select, selectedId = null, parentModal = null) {
    $.get('/NewsCategory/GetList?status=1', function (res) {
        $select.empty();

        if (res?.result === 1 && Array.isArray(res.data)) {
            $select.append('<option value="">-- Chọn danh mục --</option>');
            res.data.forEach(item => {
                $select.append(`<option value="${item.id}">${item.name}</option>`);
            });

            if (selectedId) $select.val(selectedId);
        } else {
            $select.append('<option value="">-- Không có danh mục --</option>');
        }

        $select.select2({
            placeholder: "Chọn danh mục",
            allowClear: true,
            dropdownParent: parentModal || $(document.body)
        });
    });
}

function InitCKEditor(id) {
    if (CKEDITOR.instances[id]) {
        CKEDITOR.instances[id].destroy(true);
    }

    CKEDITOR.replace(id, {
        height: 200,
        removeButtons: 'PasteFromWord'
    });
}

function HandleCreateNews() {
    $('#form_create_news').off('submit').on('submit', function (e) {
        e.preventDefault();


        const formData = new FormData(this);
        formData.set('isHot', $('#isHot').is(':checked') ? 'true' : 'false');

        $.ajax({
            url: '/News/Create',
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                if (res?.result === 1) {
                    $('#modal_create_news').modal('hide');
                    $('#news_table').DataTable().ajax.reload(null, false);
                    ShowToastNoti('success', '', 'Tạo thành công!');
                } else {
                    ShowToastNoti('warning', '', res.error?.message || 'Tạo thất bại!');
                }
            },
            error: function (xhr) {
                console.error('Lỗi chi tiết:', xhr.responseText);
                ShowToastNoti('warning', '', 'Lỗi khi gọi API!');
            }
        });
    });
}

function OpenCreateModal() {
    $('#modal_create_news').modal('show');
    InitCKEditor('description');
    LoadNewsCategories($(''), null, $('#modal_create_news'));

    setTimeout(function () {
        $('.datepicker').datepicker({
            format: 'yyyy-mm-dd',
            autoclose: true,
            todayHighlight: true
        });
    }, 200);
}
function BindCreateModalEvents() {
    $('#modal_create_news').on('shown.bs.modal', function () {
        LoadNewsCategories($('#newsCategoryId'), null, $('#modal_create_news'));

        setTimeout(function () {
            $('.datepicker').datepicker({
                format: 'dd/MM/yyyy',
                autoclose: true,
                todayHighlight: true
            });

            InitCKEditor('description');
            InitMaxLength('#modal_create_news');
        }, 200);
    });

    $('#modal_create_news').on('hidden.bs.modal', function () {
        $('#form_create_news')[0].reset();

        if (CKEDITOR.instances['description']) {
            CKEDITOR.instances['description'].setData('');
        }

        $('#newsCategoryId').val(null).trigger('change');
    });
}



function BindEditNewsEvent() {
    $('#news_table').on('click', '.btn-edit', function () {
        const id = $(this).data('id');

        $.get(`/News/GetById?id=${id}`, function (res) {
            if (!res || !res.data) {
                ShowToastNoti('warning', '', 'Không tìm thấy tin tức.');
                return;
            }

            const data = res.data;

            $.ajax({
                url: '/News/RenderUpdateView',
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (html) {
                    $('#updateFormContainer').html(html);
                    $('#modal_update_news').modal('show');

                    InitCKEditor('descriptionUpdate');
                    LoadNewsCategories($('#newsCategoryIdUpdate'), data.newsCategoryId, $('#modal_update_news'));

                    $('#isHotUpdate').prop('checked', data.isHot === true);
                    $('.datepicker').datepicker({ format: 'yyyy-mm-dd' });
                    InitMaxLength('#modal_update_news');

                    setTimeout(function () {
                        CKEDITOR.instances['descriptionUpdate'].setData(data.description || '');
                    }, 300);

                    BindSubmitUpdateNews();
                },
                error: function () {
                    ShowToastNoti('danger', '', 'Lỗi khi tải form cập nhật.');
                }
            });
        });
    });
}

function BindSubmitUpdateNews() {
    $(document).off('submit', '#form_update_news').on('submit', '#form_update_news', function (e) {
        e.preventDefault();

        for (var instance in CKEDITOR.instances) {
            if (CKEDITOR.instances.hasOwnProperty(instance)) {
                CKEDITOR.instances[instance].updateElement();
            }
        }
        const formEl = this;
        const formData = new FormData(formEl);

        const $chk = $(formEl).find('#isHotUpdate');
        const isHotChecked = $chk.length ? !!$chk.prop('checked') : false;
        formData.delete('isHot');
        formData.append('isHot', isHotChecked ? 'true' : 'false');

        $.ajax({
            url: '/News/Update',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                if (res?.result === 1 && res?.error?.code === 200) {
                    $('#modal_update_news').modal('hide');
                    $('#news_table').DataTable().ajax.reload(null, false);
                    ShowToastNoti('success', '', 'Cập nhật thành công!');
                } else {
                    ShowToastNoti('warning', '', 'Cập nhật thất bại!');
                }
            },
            error: function (xhr, status, errorThrown) {
                console.error("Lỗi khi gọi API cập nhật!", xhr);
                ShowToastNoti('danger', '', 'Lỗi khi gọi API cập nhật!');
            }
        });
    });
}



function BindDeleteNewsEvent() {
    $('#news_table').on('click', '.btn-delete', function () {
        const id = $(this).data('id');

        if (!id) return;

        Swal.fire({
            title: 'Bạn có chắc chắn?',
            text: "Tin tức này sẽ bị xoá!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Vâng, xoá!',
            cancelButtonText: 'Huỷ'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/News/UpdateStatus',
                    type: 'POST',
                    data: { id: id, status: -1 },
                    success: function (res) {
                        if (res.success) {
                            Swal.fire('Đã xoá!', 'Tin tức đã được xoá.', 'success');
                            $('#news_table').DataTable().ajax.reload(null, false);
                        } else {
                            Swal.fire('Thất bại!', res.message || 'Không thể xoá tin tức.', 'error');
                        }
                    },
                    error: function () {
                        Swal.fire('Lỗi!', 'Lỗi khi gọi API xoá tin tức.', 'error');
                    }
                });
            }
        });
    });
}

function BindViewDetailEvent() {
    $('#news_table').on('click', '.btn-view', function () {
        const id = $(this).data('id');

        $.get(`/News/GetById?id=${id}`, function (res) {
            if (!res || !res.data) {
                ShowToastNoti('warning', '', 'Không tìm thấy tin tức.');
                return;
            }

            const data = res.data;

            $.ajax({
                url: '/News/RenderDetailView',
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (html) {
                    $('#detailFormContainer').html(html);
                    $('#modal_detail_news').modal('show');
                },
                error: function () {
                    ShowToastNoti('danger', '', 'Lỗi khi tải view chi tiết.');
                }
            });
        });
    });
} 


