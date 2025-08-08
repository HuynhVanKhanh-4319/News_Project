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

function loadNewsCategories() {
    $.get('/NewsCategory/GetList?status=1', function (res) {
        const $select = $('#newsCategoryId');
        $select.empty();

        if (res?.result === 1 && Array.isArray(res.data)) {
            $select.append('<option value="">-- Chọn danh mục --</option>');
            res.data.forEach(item => {
                $select.append(`<option value="${item.id}">${item.name}</option>`);
            });
        } else {
            $select.append('<option value="">-- Không có danh mục --</option>');
        }

       
        $select.trigger('change.select2'); 
    }).fail(function () {
        $('#newsCategoryId').html('<option value="">-- Lỗi khi tải danh mục --</option>');
    });
}


function openCreateModal() {
    $('#modalCreate').modal('show');
    loadNewsCategories();
}

function handleCreateNews() {
    $('#formCreateNews').on('submit', function (e) {
        e.preventDefault();
        let formData = new FormData(this);

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
                if (res.isSuccess) {
                    $('#modalCreate').modal('hide');
                    $('#newsTable').DataTable().ajax.reload();
                    toastr.success('Thêm thành công');
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

function bindDeleteNewsEvent() {
    $('#newsTable').on('click', '.btn-delete', function () {
        const id = $(this).data('id');
        if (!id) return;

        if (confirm("Bạn có chắc chắn muốn xoá tin tức này không?")) {
            $.ajax({
                url: '/News/UpdateStatus',
                type: 'POST',
                data: { id: id },
                success: function (res) {
                    if (res.success) {
                        ShowToastNoti('success', '', 'Xoá thành công!');
                        $('#newsTable').DataTable().ajax.reload(null, false);
                    } else {
                        ShowToastNoti('warning', '', res.message || 'Xoá thất bại!');
                    }
                },
                error: function () {
                    ShowToastNoti('warning', '', 'Lỗi khi gọi API xoá tin tức!');
                }
            });
        }
    });
}

$(document).ready(function () {
    initNewsTable();
    handleCreateNews();
    bindDeleteNewsEvent();

    $('#newsCategoryId').select2({
        placeholder: "Chọn danh mục",
        allowClear: true,
        dropdownParent: $('#modalCreate')
    });
});
