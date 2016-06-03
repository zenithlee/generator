var listgroup = '<div class="col-sm-4">\
    <div class="list-group" id="{groupid}">\
    <a href="#" class="list-group-item active">\
    <h4 class="list-group-item-heading">List group item heading</h4>\
<p class="list-group-item-text">Donec id elit non mi porta gravida at eget metus. Maecenas sed diam eget risus varius blandit.</p>\
</a>\
<a href="#" class="list-group-item">\
    <h4 class="list-group-item-heading">List group item heading</h4>\
<p class="list-group-item-text">Donec id elit non mi porta gravida at eget metus. Maecenas sed diam eget risus varius blandit.</p>\
</a>\
<a href="#" class="list-group-item">\
    <h4 class="list-group-item-heading">List group item heading</h4>\
<p class="list-group-item-text">Donec id elit non mi porta gravida at eget metus. Maecenas sed diam eget risus varius blandit.</p>\
</a>\
</div>\
</div></div>';

var listitem = '<h4 class="list-group-item-heading">{title}</h4>\
<p class="list-group-item-text">{text}</p>';

ListGroup = new function() {
    this.myID = "";

    this.create = function(ID){
        this.myID = ID;
    }

    this.show = function( parent ){
        var el = document.createElement("div")
        //el.id = this.myID;
        var html = listgroup.replace("{groupid}", this.myID);
        el.innerHTML = html;
        document.body.appendChild(el);
    }

    this.myNode = function() {
        var el = document.getElementById(this.myID);
        return el;
    }

    this.addItem = function(ID, title, text){
        var el = document.createElement("a");
        var html = listitem.replace("{title}", title);
        html = html.replace("{text}", text );
        el.innerHTML = html;
        el.className = "list-group-item";
        el.id=ID;
        this.myNode().appendChild(el);
        el.addEventListener("click", this.clicked);
    }

    this.clicked = function(event) {
        //set active item
        console.log("clicked");
        var el = event.currentTarget;
        el.classList.add("active");
    }
}