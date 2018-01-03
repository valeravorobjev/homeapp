"use strict";
/*-----------------------------------------
    NoUiSlider - Property Fields

    1. Price Range
    2. Area Size
    3. Lot Size
    4. Year Built
------------------------------------------*/


// 1. Price Range

//if ($('#property-price-range')[0]) {
//    var propertyPriceRange = document.getElementById('property-price-range');
//    var propertyPriceRangeValues = [
//        document.getElementById('property-price-upper'),
//        document.getElementById('property-price-lower')
//    ]

//    var inputLow = $('input[name="costLow"]');
//    var inputHight = $('input[name="costHight"]');

//    noUiSlider.create(propertyPriceRange, {
//        start: [10, 700000],
//        connect: true,
//        range: {
//            'min': 10,
//            'max': 700000
//        }
//    });

//    propertyPriceRange.noUiSlider.on('update', function (values, handle) {
//        propertyPriceRangeValues[handle].innerHTML = values[handle];

//        if (handle == 0)
//            inputLow.val(values[handle]);
//        else if (handle == 1) {
//            inputHight.val(values[handle]);
//        }

//    });
//}

// 2. Property Area Size

if ($('#property-area-range')[0]) {
    var propertyAreaRange = document.getElementById('property-area-range');
    var propertyAreaRangeValues = [
        document.getElementById('property-area-upper'),
        document.getElementById('property-area-lower')
    ]

    var input1Low = $('input[name="areaLow"]');
    var input1Hight = $('input[name="areaHight"]');

    noUiSlider.create(propertyAreaRange, {
        start: [10, 1000],
        connect: true,
        range: {
            'min': 10,
            'max': 1000
        }
    });

    propertyAreaRange.noUiSlider.on('update', function (values, handle) {
        propertyAreaRangeValues[handle].innerHTML = values[handle];

        if (handle == 0)
            input1Low.val(values[handle]);
        else if (handle == 1) {
            input1Hight.val(values[handle]);
        }
    });
}

// 3. Lot Size

if ($('#property-lot-range')[0]) {
    var propertyLotRange = document.getElementById('property-lot-range');
    var propertyLotRangeValues = [
        document.getElementById('property-lot-upper'),
        document.getElementById('property-lot-lower')
    ]

    var input2Low = $('input[name="livingLow"]');
    var input2Hight = $('input[name="livingHight"]');

    noUiSlider.create(propertyLotRange, {
        start: [10, 1000],
        connect: true,
        range: {
            'min': 10,
            'max': 1000
        }
    });

    propertyLotRange.noUiSlider.on('update', function (values, handle) {
        propertyLotRangeValues[handle].innerHTML = values[handle];

        if (handle == 0)
            input2Low.val(values[handle]);
        else if (handle == 1) {
            input2Hight.val(values[handle]);
        }
    });
}

// 3. Year Built

if ($('#property-year-built')[0]) {
    var propertyYbRange = document.getElementById('property-year-built');
    var propertyYbRangeValues = [
        document.getElementById('property-yb-upper'),
        document.getElementById('property-yb-lower')
    ]

    var input3Low = $('input[name="yearBuiltLow"]');
    var input3Hight = $('input[name="yearBuiltHight"]');

    noUiSlider.create(propertyYbRange, {
        start: [1960, 2017],
        connect: true,
        range: {
            'min': 1960,
            'max': 2017
        }
    });

    propertyYbRange.noUiSlider.on('update', function (values, handle) {
        propertyYbRangeValues[handle].innerHTML = Math.round(values[handle]);

        if (handle == 0)
            input3Low.val(values[handle]);
        else if (handle == 1) {
            input3Hight.val(values[handle]);
        }
    });
}



$(document).ready(function () {
    /*-----------------------------------------------------
        Submit property steps switch
        - used in last form tab of 'submit-property.html'
    ------------------------------------------------------*/
    $('body').on('shown.bs.tab', '.submit-property__button', function () {
        var currentTab = $(this).attr('href');

        $('.submit-property__steps > li').removeClass('active');
        $('.submit-property__steps > li > a[href=' + currentTab + ']').parent().addClass('active');
    })


    /*-----------------------------------------------------
         Calendar and Calendar Widget
         - Used in dashboard index and calendar pages
    ------------------------------------------------------*/
    if ($('#calendar-widget')[0]) {
        $('.calendar-widget__body').fullCalendar({
            contentHeight: 'auto',
            theme: false,
            header: {
                right: 'next',
                center: 'title, ',
                left: 'prev'
            },
            buttonIcons: {
                prev: 'left',
                next: 'right',
            },
            defaultDate: '2016-08-12',
            editable: true,
            events: [
                {
                    title: 'Dolor Pellentesque',
                    start: '2016-08-01',
                    className: 'fc-event--cyan'
                },
                {
                    title: 'Purus Nibh',
                    start: '2016-08-07',
                    className: 'fc-event--amber'
                },
                {
                    title: 'Amet Condimentum',
                    start: '2016-08-09',
                    className: 'fc-event--green'
                },
                {
                    title: 'Tellus',
                    start: '2016-08-12',
                    className: 'fc-event--blue'
                },
                {
                    title: 'Vestibulum',
                    start: '2016-08-18',
                    className: 'fc-event--cyan'
                },
                {
                    title: 'Ipsum',
                    start: '2016-08-24',
                    className: 'fc-event--teal'
                },
                {
                    title: 'Fringilla Sit',
                    start: '2016-08-27',
                    className: 'fc-event--blue'
                },
                {
                    title: 'Amet Pharetra',
                    url: 'http://google.com/',
                    start: '2016-08-30',
                    className: 'mdc-bg-amber-500'
                }
            ]
        });

        //Display Current Date as Calendar widget header
        var mYear = moment().format('YYYY');
        var mDay = moment().format('dddd, MMM D');
        $('.calendar-widget__year').html(mYear);
        $('.calendar-widget__day').html(mDay);
    }


    /*-----------------------------------------------------
        Demo list delete
        - Used in dashboaed/listings.html
    ------------------------------------------------------*/
    if ($('[data-demo-action="delete-listing"]')[0]) {
        $('[data-demo-action="delete-listing"]').click(function (e) {
            e.preventDefault();

            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then(function () {
                swal(
                    'Deleted!',
                    'Your list has been deleted.',
                    'success'
                );
            })
        });
    }

    $('#main-search-input-address')
        .keyup(() => {
            var v = $('#main-search-input-address').val();
            $('#main-search-address').focus();
            $('#main-search-input-address').focus();
            $('#main-search-address').val(v);
        });

    $('#bed-groups > label')
        .click(function () {
            var label = $(this);

            if (label.attr('class') == 'btn') {

                var value = $(this).children().val();
                var output = $('#bedCount').val();

                var separator = ',';
                if (output.length == 0)
                    separator = '';

                $('#bedCount').val(output + separator + value)

            } else {
                var output = $('#bedCount').val().split(',');
                var value = $(this).children().val();

                for (var o in output) {
                    if (output[o] == value) {
                        output.splice(o, 1);
                        break;
                    }
                }

                $('#bedCount').val(output.join(','))
            }
        });

    $('#bath-groups > label')
    .click(function () {
        var label = $(this);

        if (label.attr('class') == 'btn') {

            var value = $(this).children().val();
            var output = $('#bathCount').val();

            var separator = ',';
            if (output.length == 0)
                separator = '';

            $('#bathCount').val(output + separator + value)

        } else {
            var output = $('#bathCount').val().split(',');
            var value = $(this).children().val();

            for (var o in output) {
                if (output[o] == value) {
                    output.splice(o, 1);
                    break;
                }
            }

            $('#bathCount').val(output.join(','))
        }
    });


    $('#operation-type-options > label').click(function () {
        var action = $('#advanced-search-form').attr('action');
        var arr = action.split('/');
        arr.splice(3, 1);
        arr.push($(this).children().val())
        $('#advanced-search-form').attr('action', arr.join('/'));
    })

});




