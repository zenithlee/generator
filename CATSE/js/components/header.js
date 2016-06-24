var Header = function(parentID, newID){
    var self = this;
    self.html = headerTemplate;
    self.parentID = "";

    self.constructor= function(parentID, newID){
        self.parentID = parentID;
        self.myID = newID;
    };

    self.show = function(title) {
        var html = self.html.replace("{title}", title);
        var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = html;
        parent.appendChild(node);
    };

    self.constructor(parentID, newID);
};