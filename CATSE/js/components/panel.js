var Panel = function(parentID, newID, width) {
    var self = this;
    self.htmlWithHeading = PanelTemplateWithHeading;
    self.parentID = "";
    self.width = width;

    self.constructor= function(parentID, newID){
        self.parentID = parentID;
        self.myID = newID;
    };

    self.show = function(text) {
        var html = PanelTemplate.replace("{text}", text);
        html = html.replace("{width}", self.width);
        self.addContent(html);
    };

    self.showWithHeading = function(heading,text) {
        var html = PanelTemplateWithHeading.replace("{text}", text);
        html = html.replace("{heading}", heading);
        html = html.replace("{width}", self.width);
        self.addContent(html);
    };

    self.showWithImage = function(imageURL, heading, text) {
        var html = PanelTemplateWithImage.replace("{text}", text);
        html = html.replace(/\{heading\}/g, heading);
        html = html.replace("{image}", imageURL);
        html = html.replace("{width}", self.width);
        self.addContent(html);
    };

    self.addContent = function(html){
        /*var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = html;
        parent.appendChild(node);
        */
        $("#"+self.parentID).append( html);
    };

    self.constructor(parentID, newID, width);
};