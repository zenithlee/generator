var BoxWhiskers = function(parentID, newID) {
    var self = this;
    self.id = "";
    self.parentID = "";
    self.html = '<div class="col-sm-4" id="{graphid}">{title}</div>';

    var margin = {top: 3, right: 25, bottom: 3, left: 25},
        width = 70 - margin.left - margin.right,
        height = 200 - margin.top - margin.bottom;

    var min = Infinity,
        max = -Infinity;

    // Returns a function to compute the interquartile range.
    self.iqr = function(k) {
        return function(d, i) {
            var q1 = d.quartiles[0],
                q3 = d.quartiles[2],
                iqr = (q3 - q1) * k,
                i = -1,
                j = d.length;
            while (d[++i] < q1 - iqr);
            while (d[--j] > q3 + iqr);
            return [i, j];
        };
    };

    var bchart = d3.box()
        .whiskers(self.iqr(1.5))
        .width(width)
        .height(height);

    self.constructor = function(parentID, newID){
        self.parentID = parentID;
        self.id = newID;
        self.html = self.html.replace("{graphid}", newID);
    };

    self.show = function(title, dataFilePath){
        //var html = self.html.replace("{title}", title);
        var parent = document.getElementById(self.parentID);
        var node = document.createElement("div");
        node.id = self.id;
        node.className = "";
        node.innerHTML = self.html.replace( "{title}", title);
        parent.appendChild(node);

        self.setupGraph(dataFilePath);
    };

    self.setupGraph = function(dataFilePath) {
        d3.csv(dataFilePath, function (error, csv) {
            if (error) throw error;

            var data = [];

            csv.forEach(function (x) {
                var e = Math.floor(x.Expt - 1),
                    r = Math.floor(x.Run - 1),
                    s = Math.floor(x.Speed),
                    d = data[e];
                if (!d) d = data[e] = [s];
                else d.push(s);
                if (s > max) max = s;
                if (s < min) min = s;
            });

            bchart.domain([min, max]);

            var svg = d3.select("#"+self.id).selectAll("svg")
                .data(data)
                .enter().append("svg")
                .attr("class", "box")
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.bottom + margin.top)
                .append("g")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")")
                .call(bchart);

            setInterval(function () {
                svg.datum(self.randomize).call(bchart.duration(1000));
            }, 2000);
        });
    }

    self.randomize = function(d) {
        if (!d.randomizer) d.randomizer = self.randomizer(d);
        return d.map(d.randomizer);
    };

    self.randomizer = function(d) {
        var k = d3.max(d) * .02;
        return function(d) {
            return Math.max(min, Math.min(max, d + k * (Math.random() - .5)));
        };
    };


    self.constructor(parentID, newID);
};