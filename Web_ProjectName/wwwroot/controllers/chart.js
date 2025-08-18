let chart;

$(document).ready(function () {
    BindEvents();
    RenderChart(); 
});
function RenderChart(data) {
    let seriesData, labelsData, colors;

    if (!data || data.length === 0) {
        seriesData = [100];
        labelsData = ['Chưa có dữ liệu'];
        colors = ['#008FFB'];
    } else {
        seriesData = data.map(item => item.value);
        labelsData = data.map(item => item.name);
        colors = GenerateColors(seriesData.length);
    }
    const options = {
        series: seriesData,
        chart: { width: 500, type: 'pie' },
        labels: labelsData,
        colors: colors,
        dataLabels: {
            formatter: function (val) {
                return val.toFixed(2) + "%";
            }
        },
        legend: { position: 'bottom' },
        responsive: [{
            breakpoint: 480,
            options: {
                chart: { width: 300 },
                legend: { position: 'bottom' }
            }
        }]
    };

    if (chart) {
        chart.updateOptions(options);
    } else {
        chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();
    }
}

function GenerateColors(count) {
    let colors = [];
    for (let i = 0; i < count; i++) {
        let hue = Math.floor((360 / count) * i);
        colors.push(`hsl(${hue}, 70%, 50%)`);
    }
    return colors;
}

function loadTeamData(teamId) {
    if (!teamId) {
        RenderChart(); 
        return;
    }
    $.ajax({
        url: "/Chart/GetDataByTeam",
        method: "GET",
        data: { teamId: teamId },
        dataType: "json",
        success: function (data) {
            RenderChart(data);
        },
        error: function (xhr, status, error) {
            console.error("Lỗi khi lấy dữ liệu:", error);
            RenderChart();
        }
    });
}
function BindEvents() {
    $("#teamSelect").on("change", function () {
        loadTeamData($(this).val());
    });
}
