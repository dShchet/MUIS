
makeTitle()   //Вывести заголовок
makeButtons();//Вывести кнопки


//Формирование заголовка
function makeTitle() {
    $.ajax({
        url: INFO.serverAdr + "api/userTitle/"+page.inn.name,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var titleData=dataset[0]['NAME_FULL'];
            $(".title").html("<p>"+titleData+
            " взаимодействует со следующими подразделениями: </p>");
        },
        error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);}
    });
}

//Формирование списка кнопок
function makeButtons() {
    $.ajax({
        url: INFO.serverAdr + "data/"+page.inn.name,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var list= "";
            $.each(dataset, function (index, data) {
                var rowOtdel=data["OTDEL"];
                var button=cookOtdel(rowOtdel);
                list+= "<a class='link' href="+INFO.clientAdr+"otdel:"+
                    page.inn.name+":"+rowOtdel+">"+button.name+"</a>";
            })
            $(".fields").html(list);
        },
        error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);}
    });
}

