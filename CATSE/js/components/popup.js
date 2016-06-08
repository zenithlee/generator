Popup = new function(){

    var self = this;
    self.html = popupTemplate;
    self.myID = "";

    self.create = function(newID,containerId,title, text){
        self.myID = newID;

        var html = self.html.replace("{id}", self.myID);
        html = html.replace("{title}", title);
        html = html.replace("{text}", text);
        var el = $("#"+containerId).append(html);
        $("#"+self.myID).click(self.clicked);
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
