let reportTable;
let chart; 

$(document).ready(function () {
    BindEvents();
});
function BindEvents() {
    $("#yearBefore, #yearAfter, #dot, #province").change(function () {
        const namTruoc = $("#yearBefore").val();
        const namSau = $("#yearAfter").val();
        const dot = $("#dot").val(); 
        const tinh = $("#province").val();

        LoadReportTable(namTruoc, namSau, dot, tinh);
    });
}

function LoadReportTable(namTruoc, namSau, dot, tinh) {
    if (namSau) {
        $("#colSau").text(`Sản lượng ${namSau} (tấn)`);
    } else {
        $("#colSau").text("Sản lượng vụ sau (tấn)");
    }

    if (namTruoc) {
        $("#colTruoc").text(`Sản lượng ${namTruoc} (tấn)`);
    } else {
        $("#colTruoc").text("Sản lượng vụ trước (tấn)");
    }
    $.ajax({
        url: "/controllers/fake_data.json",
        method: "GET",
        data: { namTruoc, namSau, dot, tinh },
        dataType: "json",
        success: function (res) {
            let data = [];

            if (namTruoc && namSau && dot && tinh && res) {

                data = compareData(res, namTruoc, namSau, dot, tinh);

                let tong = { hoKhaoSat: 0, sanLuongSau: 0, sanLuongTruoc: 0, tangGiam: 0 };
                data.forEach(item => {
                    tong.hoKhaoSat += item.hoKhaoSat;
                    tong.sanLuongSau += item.sanLuongSau;
                    tong.sanLuongTruoc += item.sanLuongTruoc;
                    tong.tangGiam += item.tangGiam;
                });

                const tileTong = tong.sanLuongTruoc
                    ? ((tong.tangGiam / tong.sanLuongTruoc) * 100).toFixed(2)
                    : 0;
                data.push({
                    vung: "<b>TỔNG CỘNG</b>",
                    hoKhaoSat: `<b>${tong.hoKhaoSat}</b>`,
                    sanLuongSau: `<b>${tong.sanLuongSau.toFixed(2)}</b>`,
                    sanLuongTruoc: `<b>${tong.sanLuongTruoc.toFixed(2)}</b>`,
                    tangGiam: `<b>${tong.tangGiam.toFixed(2)}</b>`,
                    tile: `<b>${tileTong}%</b>`,
                    namTrongTB: ""
                });

                let danhGia = `Đánh giá tổng thể: Qua kết quả khảo sát tại các nông hộ ở nhiều vùng cho thấy, niên vụ ${namSau} sản lượng cà phê ` +
                    (tileTong > 0 ? `dự kiến sẽ tăng khoảng ${tileTong}%` : `dự kiến sẽ giảm khoảng ${Math.abs(tileTong)}%`) +
                    `. Giá cà phê duy trì ở mức cao đã thúc đẩy người dân mạnh dạn đầu tư hơn vào cây cà phê. Tuy nhiên, một số vườn vẫn bị rệp sáp gây hại và chưa được xử lý kịp thời, một số vườn có hiện tượng ra hoa gặp mưa nên bị ảnh hưởng đến sản lượng.`;
                $("#danhGia").text(danhGia);

                renderChart(data, namTruoc, namSau);
            }

            renderReportTable(data);
        },
        error: function (xhr, status, error) {
            ShowToastNoti('warning', '', 'Lỗi khi gọi API!');
            console.error("Lỗi khi load dữ liệu:", error);

            renderReportTable([]);
            $("#danhGia").text("");
        }
    });
}

function renderReportTable(data) {
    if (reportTable) {
        reportTable.clear().rows.add(data).draw();
        return;
    }

    reportTable = $('#reportTable').DataTable({
        dom: 't',
        paging: false,
        searching: false,
        info: false,
        processing: true,
        serverSide: false,
        scrollX: true,
        responsive: false,
        autoWidth: true,
        order: [],
        data: data,
        columns: columnReport(),
        language: {
            emptyTable: "Không có dữ liệu",
            zeroRecords: "Không tìm thấy",
            url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/vi.json'
        },
        initComplete: function () {
            this.api().columns.adjust();
            $(window).trigger('resize');
        }
    });

    $(window).on('resize', function () {
        if (reportTable) {
            setTimeout(function () {
                reportTable.columns.adjust().draw(false);
            }, 100);
        }
    });
}

const columnReport = function () {
    return [
        { data: null, className: "text-center", render: (d, t, r, m) => m.row + 1 },
        { data: "vung", className: "text-left" },
        { data: "hoKhaoSat", className: "text-right" },
        { data: "sanLuongSau", className: "text-right" },
        { data: "sanLuongTruoc", className: "text-right" },
        { data: "tangGiam", className: "text-right" },
        { data: "tile", className: "text-right" },
        { data: "namTrongTB", className: "text-right" }
    ];
};

function compareData(apiData, namTruoc, namSau, dot, tinh) {
    const truoc = apiData[namTruoc][dot][tinh];
    const sau = apiData[namSau][dot][tinh];

    return sau.map((item, idx) => {
        const truocData = truoc[idx];
        const tangGiam = +(item.sanLuong - truocData.sanLuong).toFixed(2);
        const tile = +((tangGiam / truocData.sanLuong) * 100).toFixed(2);

        return {
            vung: item.vung,
            hoKhaoSat: item.hoKhaoSat,
            sanLuongSau: item.sanLuong,
            sanLuongTruoc: truocData.sanLuong,
            tangGiam,
            tile,
            namTrongTB: item.namTrongTB
        };
    });
}

function renderChart(data, namTruoc, namSau) {
    let chartData = data.filter(d => !d.vung.includes("TỔNG CỘNG"));

    let categories = chartData.map(d => d.vung);
    let seriesSau = chartData.map(d => d.sanLuongSau);
    let seriesTruoc = chartData.map(d => d.sanLuongTruoc);
    let seriesTangGiam = chartData.map(d => d.tangGiam);

    let options = {
        series: chartData.length > 0 ? [
            { name: `Sản lượng ${namSau}`, data: seriesSau },
            { name: `Sản lượng ${namTruoc}`, data: seriesTruoc },
            { name: 'Sản lượng tăng/giảm (tấn)', data: seriesTangGiam }
        ] : [],
        chart: { type: 'bar', height: 400 },
        plotOptions: {
            bar: {
                columnWidth: '80%',
                dataLabels: { position: 'top' }
            }
        },
        dataLabels: {
            enabled: chartData.length > 0, 
            formatter: function (val) { return val; },
            offsetY: -25,
            style: { fontSize: '10px', colors: ["#304758"] }
        },
        yaxis: { title: { text: 'Sản lượng (tấn)' } },
        xaxis: {
            type: 'category',
            categories: categories,
            labels: { rotate: -90 }
        },
        noData: {
            text: "Chưa có dữ liệu để hiển thị",
            align: 'center',
            verticalAlign: 'middle',
            style: { fontSize: '14px' }
        }
    };

    if (chart) {
        chart.updateOptions(options);
    } else {
        chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }
}

