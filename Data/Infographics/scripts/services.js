function GetDataFromService() {
    var result = $.get(URL);

    result.success(function(e){
        console.log(e);
    })
}

function SetGraphData(data) {
    if (window.myPie == undefined) return; 
    var newDataset = {
        backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
        data: [Math.random() * 40, Math.random() * 40, Math.random() * 40, 40, 50]
    };

    newDataset.backgroundColor = GetPalette();
    
    for (row = 1; row < 20; row++) {
        var items = [];
        for (col = 0; col < 20; col++) {
            var chr = String.fromCharCode(col + 65);
            var index = chr + (row);
            if (data[index] != undefined && (data[index]!="")) {
                items[col] = data[index];
            }
        }
        if ( items.length>0){
            newDataset.data.push(items);
        }
    }
    //newDataset.data = [data["a1"], data["b1"], data["c1"], data["d1"], data["e1"]];
    //newDataset.data = items;

    config.data.datasets = [];
    config.data.datasets.push(newDataset);
    window.myPie.update();
}

function CreateGraph() {
    CreateDemoGraph();
}

function GetPalette() {
    return ["#F7464A","#46BFBD","#FDB45C","#949FB1","#4D5360"];
}

function GetData() {
    return [Math.random() * 40, Math.random() * 40, Math.random() * 40, 40, 50];

    $.getJSON("http://sentientlabs.co/infographics/service/a=123", function (e) {
        console.log(e);
    });
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

function Test() {
    //$('#addDataset').click(function () {
        var newDataset = {
            backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
            data: [Math.random()*40, Math.random()*40, Math.random()*40, 40, 50]
        };

        newDataset.backgroundColor = GetPalette();
        newDataset.data = GetData();

        config.data.datasets = [];
        config.data.datasets.push(newDataset);
        window.myPie.update();
    //});
}