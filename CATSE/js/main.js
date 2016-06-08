var CATCHANNEL = "cat.catchannel";

main = new function() {
    var self = this;
    self.channel = {};

    this.init = function() {
        document.body.innerHTML = "<div id='main'></div>";

        Header.create("header");
        Header.show("main", "My Header");

        ListGroup.create("lg");
        ListGroup.show("main");
        ListGroup.addItem("item1","This is a Title", "Some text to show in this lorem ipsum boxy", true);
        ListGroup.addItem("item2","This is another Title", "Some more text to show in this lorem ipsum boxy");
        ListGroup.addItem("item3","This is yet another Title", "Some yet more text to show in this lorem ipsum boxy");

        Popup.create("pop", "main", "Info", "How you doin'?");

        Table.create("myTable", "main");

        this.setup();
    };

    this.setup = function() {
        self.channel = postal.channel("hi");
        //postal.addWireTap(function(d,e) {
          //  console.log(JSON.stringify(e, null, 4));
        //});
        postal.subscribe(
         {  channel:CATCHANNEL,
            topic:"chat",
             callback:function(data, enveloper) {
                self.message(data);
             }
             });
    };

    self.message = function(event){
        console.log(event);
    }

};

$(document).ready(function(){
    main.init();
});




