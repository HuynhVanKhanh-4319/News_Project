let reportTable;
let chart;

$(document).ready(function () {
    BindEvents();
});
function BindEvents() {
    $("#yearBefore, #yearAfter, #dot, #province").change(function () {
        const filter = {
            namTruoc: $("#yearBefore").val(),
            namSau: $("#yearAfter").val(),
            dot: $("#dot").val(),
            tinh: $("#province").val()
        };

        LoadReportTable(filter);
    });
}

function LoadReportTable(filter) {
    const { namTruoc, namSau } = filter;
    $("#colSau").text(namSau ? `Sản lượng vụ ${namSau} (tấn)` : "Sản lượng vụ  (tấn)");
    $("#colTruoc").text(namTruoc ? `Sản lượng cùng kỳ vụ ${namTruoc} (tấn)` : "Sản lượng vụ cùng kỳ vụ (tấn)");

    $.ajax({
        url: "/controllers/fake_data.json",
        method: "GET",
        data: filter,
        dataType: "json",
        success: function (res) {
            let data = [];
            if (filter.namTruoc && filter.namSau && filter.dot && filter.tinh && res) {
                data = compareData(res, filter);
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

                let danhGia = `Đánh giá tổng thể: Qua kết quả khảo sát tại các nông hộ ở nhiều vùng cho thấy, niên vụ ${filter.namSau} sản lượng cà phê ` +
                    (tileTong > 0 ? `dự kiến sẽ tăng khoảng ${tileTong}%` : `dự kiến sẽ giảm khoảng ${Math.abs(tileTong)}%`) +
                    `. Giá cà phê duy trì ở mức cao đã thúc đẩy người dân mạnh dạn đầu tư hơn vào cây cà phê. Tuy nhiên, một số vườn vẫn bị rệp sáp gây hại và chưa được xử lý kịp thời, một số vườn có hiện tượng ra hoa gặp mưa nên bị ảnh hưởng đến sản lượng.`;

                $("#danhGia").text(danhGia);

                renderChart(data, filter);
            }

            renderReportTable(data);
        },
        error: function () {
            ShowToastNoti('warning', '', 'Lỗi khi gọi API!');
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
        },
        footerCallback: function (row, data) {
            let tongHoKhaoSat = 0, tongSau = 0, tongTruoc = 0, tongTangGiam = 0;

            data.forEach(item => {
                tongHoKhaoSat += +item.hoKhaoSat || 0;
                tongSau += +item.sanLuongSau || 0;
                tongTruoc += +item.sanLuongTruoc || 0;
                tongTangGiam += +item.tangGiam || 0;
            });

            let tile = tongTruoc ? ((tongTangGiam / tongTruoc) * 100).toFixed(2) : 0;
            $('#tongHoKhaoSat').html(`<b>${tongHoKhaoSat}</b>`);
            $('#tongSau').html(`<b>${tongSau.toFixed(2)}</b>`);
            $('#tongTruoc').html(`<b>${tongTruoc.toFixed(2)}</b>`);
            $('#tongTangGiam').html(`<b>${tongTangGiam.toFixed(2)}</b>`);
            $('#tongTile').html(`<b>${tile}%</b>`);
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

function compareData(apiData, filter) {
    const { namTruoc, namSau, dot, tinh } = filter;
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

function renderChart(data, filter) {
    const { namTruoc, namSau } = filter;
    let sanluongTruoc = data.map(d => d.sanLuongTruoc);
    let sanluongSau = data.map(d => d.sanLuongSau);
    let tanggiam = data.map(d => d.tile);
    let maxValue = Math.max(...sanluongTruoc, ...sanluongSau);
    let tanggiamScaled = tanggiam.map(v => v * (maxValue / 100));

    if (!data || data.length === 0) {
        if (chart) chart.updateOptions({ series: [] });
        return;
    }
    let options = {
        chart: { height: 400, type: "line" },
        series: [
            { name: `Sản lượng ${namTruoc}`, type: "column", data: sanluongTruoc },
            { name: `Sản lượng ${namSau}`, type: "column", data: sanluongSau },
            { name: "% Tỉ lệ tăng/giảm sản lượng giữa 2 mùa vụ cùng kỳ ", type: "line", data: tanggiamScaled }
        ],
        stroke: { width: [0, 0, 3] },
        dataLabels: {
            enabled: true,
            enabledOnSeries: [2],
            formatter: (val, opts) => {
                if (opts.seriesIndex === 2) {
                    return tanggiam[opts.dataPointIndex] + "%";
                }
                return val.toFixed(2);
            }
        },
        labels: data.map(d => d.vung),
        yaxis: [
            {
                title: { text: "Sản lượng (tấn)" },
                labels: {
                    formatter: val => val.toFixed(2)
                }
            }
        ],
        tooltip: {
            y: {
                formatter: (val, opts) => {
                    if (opts.seriesIndex === 2) {
                        return tanggiam[opts.dataPointIndex] + "%";
                    }
                    return val.toFixed(1);
                }
            }
        }
    };

    if (chart) {
        chart.updateOptions(options, true, true);
    } else {
        chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }
}





