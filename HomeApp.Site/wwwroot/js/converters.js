$.views.converters("operationTypeLocal", function (val) {
    var ruMap = new Map();
    ruMap.set(1, "Продажа");
    ruMap.set(2, "Аренда");
    ruMap.set(3, "Длительная аренда");
    ruMap.set(4, "Посуточная аренда");

    return ruMap.get(val);
});

$.views.converters("unitTypeLocal", function (val) {
    var ruMap = new Map();
    ruMap.set(0, "Квартира");
    ruMap.set(1, "Комната");
    ruMap.set(2, "Койко место");
    ruMap.set(3, "Дом");
    ruMap.set(4, "Часть дома");
    ruMap.set(5, "Таунхауз");
    ruMap.set(6, "Офис");
    ruMap.set(7, "Торговая площадь");
    ruMap.set(8, "Склад");
    ruMap.set(9, "Пансионат");
    ruMap.set(10, "Отель");
    ruMap.set(11, "Общепит");
    ruMap.set(12, "Гараж");
    ruMap.set(13, "Производство");
    ruMap.set(14, "Автосервис");
    ruMap.set(15, "Готовый бизнес");
    ruMap.set(16, "Здание");
    ruMap.set(17, "Бытовые услуги");

    return ruMap.get(val);
});

$.views.converters("oldTypeLocal", function (val) {
    var ruMap = new Map();
    ruMap.set(0, "Новостройка");
    ruMap.set(1, "Вторичное жильё");

    return ruMap.get(val);
});

$.views.converters("houseTypeLocal", function (val) {
    var ruMap = new Map();
    ruMap.set(0, "Панельный");
    ruMap.set(1, "Монолитно кирпичный");
    ruMap.set(2, "Монолитный");
    ruMap.set(3, "Блочный");
    ruMap.set(4, "Кирпичный");
    ruMap.set(5, "Деревянный");
    ruMap.set(6, "Сталинский");

    return ruMap.get(val);
});

$.views.converters("money",
    function (val) {
        var currency = "₽";
        return  val.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1 ") + " " + currency;
    });
