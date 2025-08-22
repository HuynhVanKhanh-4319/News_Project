
let chart;
$(document).ready(function () {
    InitDatepicker();
    InitDefaultDate();
    BindEvents();
    LoadData();
});

function InitDatepicker() {
    $(".datepicker").datepicker({
        format: "yyyy-mm-dd", 
        autoclose: true,
        todayHighlight: true
    });
}
function InitDefaultDate() {
    $("#fromDate").datepicker("setDate", "2025-07-20");
    $("#toDate").datepicker("setDate", "2025-08-19");
}

function BindEvents() {
    $("#fromDate, #toDate").on("change", function () {
        LoadData();
    });
}
function LoadData() {
    const from = $("#fromDate").val();
    const to = $("#toDate").val();

    $.ajax({
        url: "/controllers/data.json",
        method: "GET",
        dataType: "json",
        success: function (data) {
            const filtered = FilterDataByDate(data, from, to);

            const tongXuatKho = CalcTotal(filtered, "xuatKho");
            const tongXuatHQ = CalcTotal(filtered, "xuatHQ");
            const tongQuyKho = CalcTotal(filtered, "quyKho");
            const tongQuyHQ = CalcTotal(filtered, "quyHQ");

            RenderChart(tongXuatKho, tongXuatHQ, tongQuyKho, tongQuyHQ, from, to);
        },
        error: function (xhr, status, error) {
            console.error("Lỗi khi lấy dữ liệu:", error);
        }
    });
}

function FilterDataByDate(data, from, to) {
    const fromDate = new Date(from);
    const toDate = new Date(to);

    return data.filter(d => {
        const dDate = new Date(d.date);
        return dDate >= fromDate && dDate <= toDate;
    });
}

function CalcTotal(list, field) {
    const sum = list.reduce((total, d) => total + d[field], 0);
    return Number(sum.toFixed(2));
}

function RenderChart(tongXuatKho, tongXuatHQ, tongQuyKho, tongQuyHQ, from, to) {
    if (chart) chart.destroy();

    const options = {
        series: [
            { name: 'Tổng KL xuất tại kho', data: [tongXuatKho] },
            { name: 'Tổng KL xuất tại hải quan', data: [tongXuatHQ] },
            { name: 'Tổng KL quy khô tại kho', data: [tongQuyKho] },
            { name: 'Tổng KL quy khô tại hải quan', data: [tongQuyHQ] }
        ],
        chart: { type: 'bar', height: 350 },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '55%',
                borderRadius: 5,
                borderRadiusApplication: 'end',
                dataLabels: {
                    position: 'top'
                }
            },
        },
        dataLabels: {
            enabled: true,
            formatter: val => val.toFixed(2),
            offsetY: -15,
            style: { fontSize: '12px', colors: ["#304758"] }
        },
        stroke: { show: true, width: 2, colors: ['transparent'] },
        xaxis: {
            categories: [`${from} đến ${to}`],
        },
        yaxis: { title: { text: 'khối lượng (kg)' } },
        fill: { opacity: 1 },
        tooltip: {
            y: { formatter: val => val.toFixed(2) + " kg" }
        }
    };

    chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();
}
