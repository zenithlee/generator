/**
 * Created by karll on 2016/06/17.
 */

var Graph = function(parentID, newID){
    var self = this;
    self.id  = "";
    self.parentID = "";
    self.html = '<div class="col-sm-4"><div class="list-group" id="{graphid}">hello</div>inside</div>';

    self.constructor = function(parentID, newID){
        self.parentID = parentID;
        self.id = newID;
        self.html = self.html.replace("{graphid}", newID)
    };

    self.show = function(){
        //var html = self.html.replace("{title}", title);
        var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = self.html;
        parent.appendChild(node);
    };

    self.constructor(parentID, newID);
};
