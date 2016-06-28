var PanelTemplate = '<div class="col-sm-{width}">\
<div class="panel panel-default">\
    <div class="panel-body">\
{text}\
    </div>\
</div>\
</div>';

var PanelTemplateWithHeading = '<div class="col-sm-{width}">\
<div class="panel panel-default panel-info">\
<div class="panel-heading">{heading}</div>\
    <div class="panel-body">\
{text}\
    </div>\
</div></div>';

var PanelTemplateWithImage = '<div class="col-sm-{width}">\
<div class="media">\
    <div class="media-left">\
    <a href="#">\
    <img class="media-object" src="{image}" alt="{heading}">\
    </a>\
    </div>\
    <div class="media-body">\
    <h4 class="media-heading">{heading}</h4>\
    {text}\
</div></div></div>';
