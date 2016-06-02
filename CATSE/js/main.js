

main = new function() {
    this.init = function() {
        document.body.innerHTML = "<div id='main'></div>";

        Header.show("main", "My Header");
        ListGroup.show("main");
    }
}

window.onload = function() {    
    main.init();
}




