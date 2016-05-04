function GetDataFromService() {
    var result = $.get(URL);

    result.success(function(e){
        console.log(e);
    })
}

function GetData() {
    $.getJSON("http://sentientlabs.co/infographics/service/a=123", function (e) {
        console.log(e);
    });

    CreateDemoGraph();
}

var config = {
    type: 'pie',
    data: {
        datasets: [{
            data: [
                10,
                20,
                50,
                12,
                14,
            ],
            backgroundColor: [
                "#F7464A",
                "#46BFBD",
                "#FDB45C",
                "#949FB1",
                "#4D5360",
            ],
        }, {
            data: [
                10,
                20,
                50,
                12,
                14,
            ],
            backgroundColor: [
                "#F7464A",
                "#46BFBD",
                "#FDB45C",
                "#949FB1",
                "#4D5360",
            ],
        }, {
            data: [
                10,
                20,
                50,
                12,
                14,
            ],
            backgroundColor: [
                "#F7464A",
                "#46BFBD",
                "#FDB45C",
                "#949FB1",
                "#4D5360",
            ],
        }],
        labels: [
            "Red",
            "Green",
            "Yellow",
            "Grey",
            "Dark Grey"
        ]
    },
    options: {
        responsive: true
    }
};

function CreateDemoGraph() {
    var ctx = document.getElementById("chart-area").getContext("2d");
    window.myPie = new Chart(ctx, config);
}