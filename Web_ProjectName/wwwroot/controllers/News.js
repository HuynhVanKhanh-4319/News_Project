
$(document).ready(function () {
    initNewsTable();
    handleCreateNews();
    bindCreateModalEvents();
    bindDeleteNewsEvent();
    bindViewDetailEvent();
    bindEditNewsEvent();
    $('<style>.cke_notification_warning { display: none !important; }</style>').appendTo('head');

});

function escapeHtml(text) {
    if (!text) return '';
    return text
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function truncateWithTooltip(text, maxLength = 50) {
    if (!text) return '';
    const truncated = text.length > maxLength ? text.substring(0, maxLength) + '...' : text;
    return `<span data-bs-toggle="tooltip" data-bs-placement="top" title="${escapeHtml(text)}">${escapeHtml(truncated)}</span>`;
}

function initNewsTable() {
    $.fn.dataTable.ext.errMode = 'none';

    const table = $('#newsTable').DataTable({
        ajax: {
            url: '/News/GetList?status=1',
            dataSrc: function (json) {
                if (!json || !Array.isArray(json.data)) return [];
                return json.data;
            }
        },
        columns: [
            {
                data: 'name',
                title: 'Tiêu đề',
                render: function (data) {
                    return truncateWithTooltip(data);
                }
            },
            {
                data: 'description',
                title: 'Mô tả',
                render: function (data) {
                    return truncateWithTooltip(data);
                }
            },
            {
                data: 'newsCategoryObj.name',
                title: 'Danh mục',
                render: function (data) {
                    return escapeHtml(data || '(Không có)');
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
        ]
    });

    $('#newsTable').on('draw.dt', function () {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    });
}

function loadNewsCategories($select, selectedId = null, parentModal = null) {
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

function initCKEditor(id) {
    if (CKEDITOR.instances[id]) {
        CKEDITOR.instances[id].destroy(true);
    }

    CKEDITOR.replace(id, {
        height: 200,
        removeButtons: 'PasteFromWord'
    });
}

function handleCreateNews() {
    $('#formCreateNews').off('submit').on('submit', function (e) {
        e.preventDefault();

        for (var instance in CKEDITOR.instances) {
            CKEDITOR.instances[instance].updateElement();
        }

        const formData = new FormData(this);

        if (!formData.has('isHot')) {
            formData.append('isHot', false);
        }

        $.ajax({
            url: '/News/Create',
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                if (res?.result === 1) {
                    $('#modalCreate').modal('hide');
                    $('#newsTable').DataTable().ajax.reload(null, false);
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
function openCreateModal() {
    $('#modalCreate').modal('show');
    initCKEditor('description');
    loadNewsCategories($('#newsCategoryId'), null, $('#modalCreate'));

    setTimeout(function () {
        $('.datepicker').datepicker({
            format: 'yyyy-mm-dd',
            autoclose: true,
            todayHighlight: true
        });
    }, 200);
}
function bindCreateModalEvents() {
    $('#modalCreate').on('shown.bs.modal', function () {
        loadNewsCategories($('#newsCategoryId'), null, $('#modalCreate'));

        setTimeout(function () {
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });
            initCKEditor('description');
        }, 200);
    });

    $('#modalCreate').on('hidden.bs.modal', function () {
        $('#formCreateNews')[0].reset();

        if (CKEDITOR.instances['description']) {
            CKEDITOR.instances['description'].setData('');
        }

        $('#newsCategoryId').val(null).trigger('change');
    });
}

function bindEditNewsEvent() {
    $('#newsTable').on('click', '.btn-edit', function () {
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
                    $('#modalUpdate').modal('show');

                    initCKEditor('descriptionUpdate');
                    loadNewsCategories($('#newsCategoryIdUpdate'), data.newsCategoryId, $('#modalUpdate'));

                    $('#isHotUpdate').prop('checked', data.isHot === true);
                    $('.datepicker').datepicker({ format: 'yyyy-mm-dd' });

                 
                    setTimeout(function () {
                        CKEDITOR.instances['descriptionUpdate'].setData(data.description || '');
                    }, 300);

                    bindSubmitUpdateNews();
                },
                error: function () {
                    ShowToastNoti('danger', '', 'Lỗi khi tải form cập nhật.');
                }
            });
        });
    });
}

function bindSubmitUpdateNews() {
    $(document).off('submit', '#formUpdateNews').on('submit', '#formUpdateNews', function (e) {
        e.preventDefault();

        for (var instance in CKEDITOR.instances) {
            CKEDITOR.instances[instance].updateElement();
        }

        const formData = new FormData(this);

        if (!formData.has('isHot')) {
            formData.append('isHot', false);
        }

        $.ajax({
            url: '/News/Update',
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            success: function (res) {
                if (res?.result === 1 && res?.error?.code === 200) {
                    $('#modalUpdate').modal('hide');
                    $('#newsTable').DataTable().ajax.reload(null, false);
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

function bindDeleteNewsEvent() {
    $('#newsTable').on('click', '.btn-delete', function () {
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
                    data: { id: id },
                    success: function (res) {
                        if (res.success) {
                            Swal.fire('Đã xoá!', 'Tin tức đã được xoá.', 'success');
                            $('#newsTable').DataTable().ajax.reload(null, false);
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

function bindViewDetailEvent() {
    $('#newsTable').on('click', '.btn-view', function () {
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
                    $('#modalDetail').modal('show');
                },
                error: function () {
                    ShowToastNoti('danger', '', 'Lỗi khi tải view chi tiết.');
                }
            });
        });
    });
}


