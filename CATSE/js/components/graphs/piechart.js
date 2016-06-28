/**
 * Created by karll on 2016/06/17.
 */

var PieChart = function(parentID, newID){
    var self = this;
    self.id  = "";
    self.parentID = "";
    self.html = '<div class="col-sm-4" id="{graphid}">Graph Title</div>';
    self.showLabels = false;
    self.showLegend = true;

    var width = 360,
        height = 200,
        radius = Math.min(width, height) / 2;

    var color = d3.scale.ordinal()
        .range(["#98abc5", "#8a89a6", "#7b6888", "#6b486b", "#a05d56", "#d0743c", "#ff8c00"]);

    var arc = d3.svg.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);

    var labelArc = d3.svg.arc()
        .outerRadius(radius - 40)
        .innerRadius(radius - 40);

    var pie = d3.layout.pie()
        .sort(null)
        .value(function(d) { return d.population; });

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
            .attr("width", width)
            .attr("height", height)
            .append("g")
            .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

        d3.csv(dataFilePath, self.type, function(error, data) {
            if (error) throw error;

            var g = svg.selectAll(".arc")
                .data(pie(data))
                .enter().append("g")
                .attr("class", "arc");

            g.append("path")
                .attr("d", arc)
                .style("fill", function(d) { return color(d.data.age); });

            if ( self.showLabels == true ) {
                g.append("text")
                    .attr("transform", function (d) {
                        return "translate(" + labelArc.centroid(d) + ")";
                    })
                    .attr("dy", ".35em")
                    .text(function (d) {
                        return d.data.age;
                    });
            }

            if ( self.showLegend == true ){

                //var legend = d3.select(id).append("table").attr('class','legend');
                // create one row per segment.
                //var tr = legend.append("tbody").selectAll("tr").data(lD).enter().append("tr");

                var legend = svg.append("g")
                    .attr("class", "legend")
                    .attr("x",  65)
                    .attr("y", 25)
                    .attr("height", 100)
                    .attr("width", 100);


                legend.selectAll('g').data(data)
                    .enter()
                    .append('g')
                    .each(function(d, i) {
                        var g = d3.select(this);
                        g.append("rect")
                            .attr("x", 120)
                            .attr("y", i*25-80)
                            .attr("width", 60)
                            .attr("height", 20)
                            .style("fill", color(i));

                        g.append("text")
                            .attr("x", 130)
                            .attr("y", i * 25-65)
                            .attr("height",30)
                            .attr("width",100)
                            .style("fill", "black")
                            .text(function (d) {
                                return d.age;
                            });

                    });

            }
        });

        setInterval(function () {
            var svg = d3.select("#"+self.id)
                .transition();

                svg.select("d")
                .attr("d",22);

        }, 2000)
    };

    self.randomize = function(d) {
        if (!d.randomizer) d.randomizer = self.randomizer(10);
        return d.map(d.randomizer);
    };

    self.randomizer = function(d) {
        var k = d3.max(d) * .02;
        return function(d) {
            return Math.max(min, Math.min(max, d + k * (Math.random() - .5)));
        };
    };

    function type(d) {
        d.population = +d.population;
        return d;
    }

    self.constructor(parentID, newID);

};
