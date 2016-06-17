/// <reference path="../typings/globals/jquery/index.d.ts"/>
var CATCHANNEL = "cat.catchannel";

var main = function(){
    var self = this;
    self.channel = postal.channel("hi");

    self.init = function() {
        document.body.innerHTML = "<div id='main'></div>";

        var mh = new Header("main","myHeader");
        mh.show("My Header");

        ListGroup.create("lg");
        ListGroup.show("main");
        ListGroup.addItem("item1","This is a Title", "Some text to show in this lorem ipsum boxy", true);
        ListGroup.addItem("item2","This is another Title", "Some more text to show in this lorem ipsum boxy");
        ListGroup.addItem("item3","This is yet another Title", "Some yet more text to show in this lorem ipsum boxy");

        Popup.create("pop", "main", "Info", "How you doin'?");

        Table.create("myTable", "main");

        var _Graph = new Graph("main", "myGraph");
        _Graph.show("main");

        this.setup();
    };

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




