var listgroup = '<div class="col-sm-4">\
    <div class="list-group">\
    <a href="#" class="list-group-item active">\
    <h4 class="list-group-item-heading">List group item heading</h4>\
<p class="list-group-item-text">Donec id elit non mi porta gravida at eget metus. Maecenas sed diam eget risus varius blandit.</p>\
</a>\
<a href="#" class="list-group-item">\
    <h4 class="list-group-item-heading">List group item heading</h4>\
<p class="list-group-item-text">Donec id elit non mi porta gravida at eget metus. Maecenas sed diam eget risus varius blandit.</p>\
</a>\
<a href="#" class="list-group-item">\
    <h4 class="list-group-item-heading">List group item heading</h4>\
<p class="list-group-item-text">Donec id elit non mi porta gravida at eget metus. Maecenas sed diam eget risus varius blandit.</p>\
</a>\
</div>\
</div></div>';

ListGroup = new function() {
    this.show = function( parent ){
        var el = document.createElement("div")
        el.innerHTML = listgroup;
        document.body.appendChild(el);
    }
}