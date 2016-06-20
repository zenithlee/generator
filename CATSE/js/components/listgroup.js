ListGroup = new function() {
    var self = this;

    self.GroupTemplate = '<div class="col-sm-4"><div class="list-group" id="{groupid}"></div></div>';
    self.ItemTemplate = '<h4 class="list-group-item-heading">{title}</h4><p class="list-group-item-text">{text}</p>';

    self.myID = "";
    this.elements = [];

    this.create = function(ID){
        this.myID = ID;
    };

    this.show = function( parent ){
        var el = document.createElement("div")
        //el.id = this.myID;
        var html = self.GroupTemplate.replace("{groupid}", this.myID);
        el.innerHTML = html;
        document.body.appendChild(el);
    };

    this.myNode = function() {
        var el = document.getElementById(this.myID);
        return el;
    };

    this.addItem = function(ID, title, text, active){
        var el = document.createElement("a");
        var html = self.ItemTemplate.replace("{title}", title);
        html = html.replace("{text}", text );
        el.innerHTML = html;
        el.className = "list-group-item";
        if ( active == true ) el.className += " active";
        el.id=ID;
        el.data = title;
        this.myNode().appendChild(el);
        el.addEventListener("click", this.clicked);
        self.elements.push(el);
    };

    this.clicked = function(event) {
        //set active item
        console.log("clicked");

        self.elements.forEach(function(element){
            element.classList.remove("active");
        });

        var el = event.currentTarget;
        el.classList.add("active");

        postal.publish({channel:CATCHANNEL, topic:"chat", data:{title:el.data}});
    }
}