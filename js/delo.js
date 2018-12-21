


GetTitle()
GetCases();

//Собрать заголовок
function GetTitle() {
    $(".result").html("<div class='center'>Загрузка...</div>");
    $.ajax({
        url: INFO.serverAdr + "api/user/"+page.inn.name,
        type: "GET", contentType: "application/json",
        success: function (data) {
            var titleData={};
            var titleData=data[0]['NAME_FULL'];
            $(".title").html("<p>"+titleData+" участвует в судебной тяжбе в категории "+page.delo.name+"</p>");
         },error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);},
    });
}

//Получить список дел
function GetCases() {
    $.ajax({
        url: INFO.serverAdr + "api/delo/"+page.inn.name+'.'+page.delo.raw,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var caseList="";
            if(dataset.leght==1){}
            $.each(dataset, function (index, data) {
                caseList+=makeOneCase(data);
                console.log(data);
            });
            $('.fields').html(caseList);
         },error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);},
    });
}

//Сформировать одно дело
function makeOneCase(data) {
    var text="";
    if(data.NAME_FULL_CLAIMANT){text+="<div><b>Истец: </b><p>"+data.NAME_FULL_CLAIMANT+"</p></div>"}
    if(data.NAME_FULL_DEFENDANT){text+="<div><b>Ответчик: </b><p onclick='goto("+data.INN_DEFENDANT+")'>"+data.NAME_FULL_DEFENDANT+"</p></div>"}
    if(data.REG_DATE){text+="<div><b>Дата регистрации: </b><p>"+fixDate(data.REG_DATE)+"</p></div>"}
    if(data.COURT_DATE){text+="<div><b>Дата заседания: </b><p>"+fixDate(data.COURT_DATE)+"</p></div>"}
    if(data.JUDGE){text+="<div><b>Судья:</b><p>"+data.JUDGE+"</p></div>"}
    if(data.AMOUNT_OF_CLAIM){text+="<div><b>Сумма иска:</b><p>"+data.AMOUNT_OF_CLAIM+"р.</p></div>"}
    if(data.CASE_NUMBER){text+="<div><b>Номер дела:</b><p>"+data.CASE_NUMBER+"</p></div>"}
    if(data.COURTROOM){text+="<div><b>Номер комнаты:</b><p>"+data.COURTROOM+"</p></div>"}
    if(data.SSP_FIO_CONTACT){text+="<div><b>Телефон судьи:</b><p>"+data.SSP_FIO_CONTACT+"</p></div>"}
    if(data.INN_DEFENDANT){text+="<div><b>ИНН ответчика:</b><p>"+data.INN_DEFENDANT+"</p></div>"}
    return text;
}

//перевести дату в правильный формат
function fixDate(date){
    var dateOpt = {year: 'numeric', month: 'long', day: 'numeric', weekday: 'long'};
    var tempDate=new Date(date);
    return tempDate.toLocaleString("ru", dateOpt);
}

