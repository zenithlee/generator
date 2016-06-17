var Component = function() {
    var self = this;
    this.render = function(html) {
        //var html = self.html.replace("{title}", title);
        var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = html;
        parent.appendChild(node);
    }
};