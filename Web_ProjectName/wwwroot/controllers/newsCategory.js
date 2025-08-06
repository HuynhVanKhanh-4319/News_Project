$(document).ready(function () {
    const status = 1;
    $.ajax({
        url: '/NewsCategory/GetList?status=' + status,
        type: 'GET',
        dataType: 'json',
        success: function (res) {
    

            if (!res || res.isSuccess === false) {
                const errorMessage = res && res.message ? res.message : "Không thể lấy dữ liệu từ hệ thống.";
                return showError(errorMessage);
            }

            const data = res.data;
            let html = '';

            if (!Array.isArray(data) || data.length === 0) {
                html = '<tr><td colspan="4">Không có dữ liệu.</td></tr>';
            } else {
                data.forEach(item => {
                    html += `
                        <tr>
                            <td>${item.name || ''}</td>
                            <td>${item.nameSlug || ''}</td>
                            <td>${item.status === 1 ? 'Hiện' : 'Ẩn'}</td>
                            <td>${item.sort ?? ''}</td>
                            <td>${item.remark ?? ''}</td>
                        </tr>
                    `;
                });
            }

            $('#newsCategoryTable tbody').html(html);
        },
        error: function (xhr, status, error) {
            console.error("Lỗi AJAX:", error);
            showError("Không thể kết nối đến máy chủ.");
        }
    });

    function showError(message) {
        $('#newsCategoryTable tbody').html(`
            <tr>
                <td colspan="4" style="color:red;">${message}</td>
            </tr>
        `);
    }
});

//Create NewsCategory

$('#btnCreate').click(function () {
    var model = {
        name: $('#Name').val(),
        remark: $('#Remark').val(),
        nameSlug: $('#NameSlug').val(),
        status: $('#Status').val()
    };

    $.ajax({
        url: '/NewsCategory/Create',
        type: 'POST',
        data: model,
        success: function (res) {
            if (res.result === 1) {
                alert( res.error?.message || "Tạo thành công!");
                location.href = '/NewsCategory'; 
            } else {
                alert("Tạo thất bại: " + (res.error?.message || "Không rõ lỗi"));
            }
        },
        error: function (err) {
            alert("Lỗi khi gọi API!");
            console.log(err);
        }
    });
});


