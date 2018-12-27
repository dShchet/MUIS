
makeTitle()//Вывести заголовок
makeButtons();//Вывести кнопки


//Вывести заголовок
function makeTitle() {
    $.ajax({
        url: INFO.serverAdr + "api/userTitle/" + page.inn.name,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var titleData={};
            $.each(dataset, function (index, data) {
                titleData[data.INN]=data;})
                $(".title").html(titleData[page.inn.name]['NAME_FULL']+
                "<p>в</p>"+page.otdel.name+
                "рассматривает дела в следующих категориях:</p>");
        },error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);}
    });
}

//Вывести блок кнопок
function makeButtons() {//Для кнопок
    $.ajax({
        url: INFO.serverAdr + "api/otdel/" + page.inn.name,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var listHtml = "";
            $.each(dataset, function (index, category) {
                var rowOtdel=category["OTDEL_PRAVO"];
                var data=cookCase(rowOtdel);
                listHtml +="<a class='link' href="+
                    INFO.clientAdr+"case:"+page.inn.name+
                    ":"+page.otdel.raw+":"+data.url+" >"+data.name+"</a>"
            })
            $(".fields").html(listHtml);
        },error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);}
    });
}