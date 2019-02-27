const createCharts = (xCategories, sum, titleText, totalspent) => {
    Highcharts.chart('container', {
        borderWidth: 3,
        borderRadius: 5,
        chart: {
            type: 'column'
        },
        title: {
            text: titleText + ' ' + totalspent
        },
        xAxis: {
            type: 'category',
            categories: xCategories,
            labels: {
                rotation: -45,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'هزینه کرد'
            }
        },
        legend: {
            enabled: false
        },
        tooltip: {
            //pointFormat: '{series.name}: <b>{point.y}</b><br/>',
            headerFormat: '<span>{point.key}</span>',
            pointFormat: '{series.name}: <b>{point.y:.1f} </b>',
            footerFormat: '',
            shared: true,
            useHTML: true
        },

        series: [{
            type: 'column',
            data: sum
        }]
    });
};
const showLastMonth = () => {
    const titleMessage = "گزارش مالی ماه اخیر";
    $.ajax({
        type: "GET",
        url: "/Expense/GetWeeklyExpense",
        contentType: "application/json",
        dataType: "json",
        success: (result) => {
            const keys = Object.keys(result);
            const weeklydata = new Array();
            let totalspent = 0.0;
            keys.forEach(key => {
                const arrL = [];
                arrL.push(key);
                arrL.push(result[key]);
                totalspent += result[key];
                weeklydata.push(arrL);
            });
            createCharts(keys, weeklydata, titleMessage, totalspent.toFixed(2));
        }
    })
};
const showLastSixMonth = () => {
    const titleMessage = "گزارش مالی 6 ماه اخیر";
    $.ajax({
        type: "GET",
        url: "/Expense/GetMonthlyExpense",
        contentType: "application/json",
        dataType: "json",
        success: (result) => {
            const keys = Object.keys(result);
            const monthlydata = new Array();
            let totalspent = 0.0;
            keys.forEach(key => {
                const arrL = [];
                arrL.push(key);
                arrL.push(result[key]);
                totalspent += result[key];
                monthlydata.push(arrL);
            });
            createCharts(keys, monthlydata, titleMessage, totalspent.toFixed(2));
        }
    })
};
const showLastWeek = () => {
    const titleMessage = "گزارش مالی هفته اخیر";
    $.ajax({
        type: "GET",
        url: "/Expense/GetLastWeekExpense",
        contentType: "application/json",
        dataType: "json",
        success: (result) => {
            const keys = Object.keys(result);
            const monthlydata = new Array();
            let totalspent = 0.0;
            keys.forEach(key => {
                const arrL = [];
                arrL.push(key);
                arrL.push(result[keys]);
                totalspent += result[key];
                monthlydata.push(arrL);
            });
            createCharts(keys, monthlydata, titleMessage, totalspent.toFixed(2));
        }
    })
};