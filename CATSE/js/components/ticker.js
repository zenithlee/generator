var Ticker = function(parentID, newID) {
    var self = this;
    self.html = TickerTemplate;
    self.parentID = "";
    self.id = "";
    self.scrollAmt = 0;
    self.speed = 50;

    self.constructor = function (parentID, newID) {
        self.parentID = parentID;
        self.id = newID;
    };

    self.show = function(text) {
        var html = TickerTemplate;
        html = html.replace("{id}", self.id);
        $("#"+self.parentID).append(html);
        setInterval(self.scroll, self.speed);
    };

    self.addContent = function(html){
        $("#"+self.id).append(html + " ");
    };

    self.scroll = function(){
        self.scrollAmt--;
        var me = $("#"+self.id);
        if ( self.scrollAmt < -me.width() ) self.scrollAmt = window.innerWidth;
        $("#"+self.id).css("left",self.scrollAmt);
    };

    self.constructor(parentID, newID);
};
