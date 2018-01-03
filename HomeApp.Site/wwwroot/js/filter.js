function setSortValue(sv) {
    if (!sv) {
        sv = $('#select-search-action').val();
    }
    $("input[name='sort']").val(sv);
}

$(document)
    .ready(() => {
        var svr = $("input[name='sort']").val();
        if (svr) {
            setSortValue(svr);
            $('#select-search-action').val(svr).change();
        }

        $('#select-search-action').on("select2:select", function () {
            setSortValue();
            $("#SortForm").submit();
        });
    });