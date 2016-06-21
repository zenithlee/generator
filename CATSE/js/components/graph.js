/**
 * Created by karll on 2016/06/17.
 */

var Graph = function(parentID, newID){
    var self = this;
    self.id  = "";
    self.parentID = "";
    self.html = '<div class="col-sm-4" id="{graphid}">Graph Title</div>';

    var margin = {top: 20, right: 20, bottom: 30, left: 40},
        width = 360 - margin.left - margin.right,
        height = 200 - margin.top - margin.bottom;

    var x = d3.scale.ordinal()
        .rangeRoundBands([0, width], .1);

    var y = d3.scale.linear()
        .range([height, 0]);

    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom");

    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .ticks(10, "%");



    self.constructor = function(parentID, newID){
        self.parentID = parentID;
        self.id = newID;
        self.html = self.html.replace("{graphid}", newID)
    };

    self.show = function(dataFilePath){
        //var html = self.html.replace("{title}", title);
        var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = self.html;
        parent.appendChild(node);

        self.setupGraph(dataFilePath);
    };

    self.setupGraph = function(dataFilePath) {

        var svg = d3.select("#"+self.id).append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");


        d3.tsv(dataFilePath, self.type, function(error, data) {
            if (error) throw error;

            x.domain(data.map(function (d) {
                return d.letter;
            }));
            y.domain([0, d3.max(data, function (d) {
                return d.frequency;
            })]);

            svg.append("g")
                .attr("class", "x axis")
                .attr("transform", "translate(0," + height + ")")
                .call(xAxis);

            svg.append("g")
                .attr("class", "y axis")
                .call(yAxis)
                .append("text")
                .attr("transform", "rotate(-90)")
                .attr("y", 6)
                .attr("dy", ".71em")
                .style("text-anchor", "end")
                .text("Frequency");

            svg.selectAll(".bar")
                .data(data)
                .enter().append("rect")
                .attr("class", "bar")
                .attr("x", function (d) {
                    return x(d.letter);
                })
                .attr("width", x.rangeBand())
                .attr("y", function (d) {
                    return y(d.frequency);
                })
                .attr("height", function (d) {
                    return height - y(d.frequency);
                });
        });
    };

    self.type = function(d) {
        d.frequency = +d.frequency;
        return d;
    }

    self.constructor(parentID, newID);

};
