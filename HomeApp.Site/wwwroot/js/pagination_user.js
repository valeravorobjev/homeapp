function pagination(template, container) {
    var pageSize = 12;
    var nextPage = 2;
    var take = pageSize;
    var skip = pageSize * (nextPage - 1);

    var url = $("input[name='url']").val();
    var userType = $("input[name='userType']").val();
    var simbol = "?";

    if (url.includes("?")) {
        simbol = "&";
    }

    $("#LoadMore")
        .click(() => {
            $.ajax({
                url: $("input[name='url']").val() + simbol + "userType=" + userType + '&skip=' + skip + '&take=' + take
            })
                .done((data) => {
                    nextPage++;
                    skip = pageSize * (nextPage - 1);
                    var totalPage = Math.round((data.count / pageSize));
                    if (nextPage > totalPage) {
                        $(".load-more").remove();
                    }
                    render(data.users, template, container);

                    if ($('.rmd-rate')[0]) {
                        $('.rmd-rate').each(function () {
                            var rate = $(this).data('rate-value');
                            var readOnly = $(this).data('rate-readonly');

                            $(this).rateYo({
                                rating: rate,
                                fullStar: true,
                                starWidth: '18px',
                                ratedFill: '#fcd461',
                                normalFill: '#eee',
                                readOnly: readOnly || 'false'
                            });
                        });
                    }
                });
        });
}