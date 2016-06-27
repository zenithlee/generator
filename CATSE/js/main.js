/// <reference path="../typings/globals/jquery/index.d.ts"/>
var CATCHANNEL = "cat.catchannel";

var main = function(){
    var self = this;
    self.channel = postal.channel("hi");

    self.init = function() {
        document.body.innerHTML = "<div id='tickerContainer'></div>";
        document.body.innerHTML += "<div id='main'></div>";

        var mh = new Header("main","myHeader");
        mh.show("TEST EVERYTHING");

        var tick = new Ticker("tickerContainer", "myTicker");
        tick.show();
        tick.addContent("123");
        tick.addContent("abcdefg");


        Popup.create("pop", "main", "Info", "How you doin'?");

        Table.create("myTable", "main");

        var _Graph = new BarGraph("main", "myGraph");
        _Graph.show("./data/data.tsv");

        var _Pie = new PieChart("main", "myGraph2");
        _Pie.show("./data/piedata.csv");

        var _Box = new BoxWhiskers("main", "myBox");
        _Box.show("Dynamic Data", "./data/morley.csv");

        //row
        $("#main").append("<div id='myRow' class='row show-grid'>");

        var _Panel = new Panel("myRow", "myPanel");
        _Panel.show("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");


        var _List = new ListGroup("myRow","lg");
        _List.show("myRow");
        _List.addItem("item1","This is a Title", "Some text to show in this lorem ipsum boxy", true);
        _List.addItem("item2","This is another Title", "Some more text to show in this lorem ipsum boxy");
        _List.addItem("item3","This is yet another Title", "Some yet more text to show in this lorem ipsum boxy");

        ///row
        $("#main").append("</div>");

        var _Panel2 = new Panel("myRow", "myPanel");
        _Panel2.showWithHeading("<a name='about'></a> ABOUT The Title","Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");

        this.setup();

        //self.test();
    };

    self.test = function() {
        $("body").append("<div id='what' class='container col-lg-3'><p>part 1</p><p>part 2</p><p>part 3</p><p>part 4</p><p>part 5</p></div>");
        d3.selectAll("#what p")
            .data([4, 8, 15, 16, 23, 42])
            .style("font-size", function(d) { return d + "px"; });

        $("body").append('<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">\ ' +
            '<rect x="10" y="10" height="130" width="500" style="fill: #000000"/>\ ' +
            '<image x="20" y="20" width="300" height="80" xlink:href="http://www.mrgamez.com/wp-content/uploads/2012/10/mr-games.png" />\ ' +
            '<line x1="25" y1="80" x2="350" y2="80" style="stroke: #ffffff; stroke-width: 3;"/> ');

    }

    self.setup = function() {

        //postal.addWireTap(function(d,e) {
          //  console.log(JSON.stringify(e, null, 4));
        //});
        /*
        postal.subscribe(
         {  channel:CATCHANNEL,
            topic:"chat",
             callback:function(data, enveloper) {
                this.message(data);
             }
             });
             */
    };

    self.message = function(event){
        console.log(event);
    }

};

window.onload = (function(){
    var _main = new main();
    _main.init();
});




