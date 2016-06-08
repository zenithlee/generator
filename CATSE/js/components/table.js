Table = new function() {
    var self = this;
    self.myID = "";

    self.create = function(newID,containerID) {
        self.myID = newID;
        var html = tableTemplate.replace( "{id}", newID);
        $("#" +containerID).append(html);
        $("#" + self.myID).DataTable({"scrollX":true});

    };

    self.bind = function() {

    };

    self.show = function(message) {

    };

    self.hide = function() {

    }

};
