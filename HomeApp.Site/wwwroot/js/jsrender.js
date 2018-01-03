function render(data, template, container) {
    var tmpl = $.templates(template);
    var html = tmpl.render(data);
    $(container).append(html);
}