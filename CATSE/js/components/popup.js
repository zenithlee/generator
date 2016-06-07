Popup = new function(){

    var self = this;
    self.html = popupTemplate;
    self.myID = "";

    self.create = function(newID,containerId,title, text){
        self.myID = newID;

        var html = self.html.replace("{id}", self.myID);
        html = html.replace("{title}", title);
        html = html.replace("{text}", text);
        var parent = document.getElementById(containerId);
        var el = document.createElement("div");
        el.id = self.myID;
        el.className = "col-sm-4";
        el.style.cssText = "left:50%; top:40%; position:absolute; z-index:10; transform:translateX(-50%); display:none;";

        el.innerHTML = html;
        parent.appendChild(el);
        el.addEventListener("click", this.clicked);
        self.bind();
        self.hide();
    };

    self.bind = function() {
        postal.subscribe(
        {  channel:CATCHANNEL,
            topic:"chat",
            callback:function(data, enveloper) {
                self.show(data.title);
            }
        });
    };

    self.show = function(message) {
        $("#"+self.myID).show();
        $("#text").html( message);
    };

    self.hide = function() {
        $("#"+self.myID).hide();
    };

    self.clicked = function(event) {
        self.hide();
    }
};
