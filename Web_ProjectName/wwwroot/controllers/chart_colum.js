let chartColum;

$(document).ready(function () {
    BindEventsColum();
    LoadTeamDataColum("");
});
function RenderChartColum(data) {
    let categories, chenData, dayData, tongData;

    if (!data || data.length === 0) {
        categories = ["Chưa có dữ liệu"];
        chenData = [0];
        dayData = [0];
        tongData = [0];
    } else {
        categories = data.map(item => item.name);
        chenData = data.map(item => item.chen);
        dayData = data.map(item => item.day);
        tongData = data.map(item => item.tong);
        
    }

    const options = {
        series: [
            { name: "Quy khô mủ chén", data: chenData },
            { name: "Quy khô mủ dây", data: dayData },
            { name: "Tổng quy khô", data: tongData }
        ],
        chart: {
            type: "bar",
            height: 350
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: "70%",
                borderRadius: 5,
                borderRadiusApplication: "end",
                dataLabels: {
                    position: "top"
                }
            }
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                return val;
            },
            offsetY: -20, 
            style: {
                fontSize: "10px",
                colors: ["#000"] 
            }
        },
        stroke: {
            show: true,
            width: 3,
            colors: ["transparent"]
        },
        xaxis: {
            categories: categories
        },
        yaxis: {
            title: { text: "Sản lượng" }
        },
        fill: { opacity: 1 },
        tooltip: {
            y: {
                formatter: function (val) {
                    return val;
                }
            }
        }
    };
    if (chartColum) {
        chartColum.updateOptions(options);
    } else {
        chartColum = new ApexCharts(document.querySelector("#chartcolum"), options);
        chartColum.render();
    }

}

function LoadTeamDataColum(teamId) {
    $.ajax({
        url: "/Chart/GetDataChartColum",
        method: "GET",
        data: { teamId: teamId },
        dataType: "json",
        success: function (res) {
            if (res.error) {
                ShowToastNoti('warning','', 'Lỗi không thể load');
                console.error("API error:", res.error);
                RenderChartColum([]);
                return;
            }
            RenderChartColum(res);
        },
        error: function (xhr, status, error) {
            console.error("Lỗi khi lấy dữ liệu:", error);
            RenderChartColum([]);
        }
    });
}
function BindEventsColum() {
    $("#team_Select").on("change", function () {
        LoadTeamDataColum($(this).val());
    });
}
