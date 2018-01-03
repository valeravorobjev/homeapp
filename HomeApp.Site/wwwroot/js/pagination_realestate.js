function pagination(template, container) {
    var pageSize = 16;
    var nextPage = 2;
    var take = pageSize;
    var skip = pageSize * (nextPage - 1);

    var url = $("input[name='url']").val();
    var opt = $("input[name='operationType']").val();
    var address = $("input[name='address']").val();
    var unitType = $("input[name='unitType']").val();
    var bathCount = $("input[name='bathCount']").val();
    var bedCount = $("input[name='bedCount']").val();
    var costLow = $("input[name='costLow']").val();
    var costHight = $("input[name='costHight']").val();
    var areaHight = $("input[name='areaHight']").val();
    var areaLow = $("input[name='areaLow']").val();

    var simbol = "?";

    if (url.includes("?")) {
        simbol = "&";
    }

    var arr = [];
    if (address.length > 0)
        arr.push('address=' + address);
    if (opt.length > 0)
        arr.push('operationType=' + opt);
    if (unitType.length > 0)
        arr.push('unitType=' + unitType);
    if (bathCount.length > 0)
        arr.push('bathCount=' + bathCount);
    if (bedCount.length > 0)
        arr.push('bedCount=' + bedCount);
    if (costLow.length > 0)
        arr.push('costLow=' + costLow);
    if (costHight.length > 0)
        arr.push('costHight=' + costHight);
    if (areaHight.length > 0)
        arr.push('areaHight=' + areaHight);
    if (areaLow.length > 0)
        arr.push('areaLow=' + areaLow);

    var query = arr.join('&');
    

    $("#LoadMore")
        .click(() => {
            $.ajax({
                url: $("input[name='url']").val() + simbol + query + '&skip=' + skip + '&take=' + take
            })
                .done((data) => {
                    nextPage++;
                    skip = pageSize * (nextPage - 1);
                    var totalPage = Math.round((data.count / pageSize));
                    if (nextPage > totalPage) {
                        $(".load-more").remove();
                    }
                    render(data.realEstates, template, container);
                });
        });
}