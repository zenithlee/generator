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
}