var ListGroup = function(parentID, newID) {
    var self = this;

    self.GroupTemplate = '<div class="span4 col-md-offset-8 col-sm-offset-8"><div class="list-group" id="{groupid}"></div></div>';
    self.ItemTemplate = '<h4 class="list-group-item-heading">{title}</h4><p class="list-group-item-text">{text}</p>';

    self.myID = "";
    this.elements = [];

    self.constructor= function(parentID, newID){
        self.myID = newID;
        self.parentID = parentID;
    };

    this.show = function( ){
        var parent = document.getElementById(self.parentID);
        var el = document.createElement("div")
        var html = self.GroupTemplate.replace("{groupid}", this.myID);
        el.innerHTML = html;
        parent.appendChild(el);
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

    self.constructor(parentID, newID);
}