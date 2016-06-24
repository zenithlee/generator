/// <reference path="../typings/globals/jquery/index.d.ts"/>
var CATCHANNEL = "cat.catchannel";

var main = function(){
    var self = this;
    self.channel = postal.channel("hi");

    self.init = function() {
        document.body.innerHTML = "<div id='main'></div>";

        var mh = new Header("main","myHeader");
        mh.show("My Header");

        Popup.create("pop", "main", "Info", "How you doin'?");

        Table.create("myTable", "main");

        var _Graph = new BarGraph("main", "myGraph");
        _Graph.show("./data/data.tsv");

        var _Pie = new PieChart("main", "myGraph2");
        _Pie.show("./data/piedata.csv");

        //row
        $("#main").append("<div id='myRow' class='row show-grid'>");

        var _Panel = new Panel("myRow", "myPanel");
        _Panel.show("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
        _Panel.showWithHeading("The Title","Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");

        var _List = new ListGroup("myRow","lg");
        _List.show("myRow");
        _List.addItem("item1","This is a Title", "Some text to show in this lorem ipsum boxy", true);
        _List.addItem("item2","This is another Title", "Some more text to show in this lorem ipsum boxy");
        _List.addItem("item3","This is yet another Title", "Some yet more text to show in this lorem ipsum boxy");

        ///row
        $("#main").append("</div>");

        this.setup();

        self.test();
    };

    self.test = function() {
        $("body").append("<div id='what' class='container col-lg-3'><p>part 1</p><p>part 2</p><p>part 3</p><p>part 4</p><p>part 5</p></div>");
        d3.selectAll("#what p")
            .data([4, 8, 15, 16, 23, 42])
            .style("font-size", function(d) { return d + "px"; });
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




