

main = new function() {
    this.init = function() {
        document.body.innerHTML = "<div id='main'></div>";

        Header.create("header");
        Header.show("main", "My Header");

        ListGroup.create("lg");
        ListGroup.show("main");
        ListGroup.addItem("item1","This is the Title", "Some text to show in this lorem ipsum boxy");
    }
}

window.onload = function() {    
    main.init();
}




