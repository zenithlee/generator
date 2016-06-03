Header = new function(){

    var self = this;
    self.html ='';

    self.create = function(newID){
        self.myID = newID;
    }

    self.show = function(containerId,title) {
        //var html = self.html.replace("{title}", title);
        var html = navbar;
        var parent = document.getElementById(containerId);
        var node = document.createElement("div");
        node.id = self.myID;
        node.className = "";
        node.innerHTML = html;
        parent.appendChild(node);
    }
};