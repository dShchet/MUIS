
var infoData={};//данные подробностей организаций
var amount=0;//количество записей

//На случай если данные остались в инпутах
var filterInn  = $("#filterInn").val();
var filterName = $("#filterName").val();
if((!filterInn=="")||(!filterName=="")){
    toFilter(filterInn, filterName);
}

//обработчик ввода в фильтры
$("#filterInn, #filterName").keyup(function(e){
    e.preventDefault();
    var filterInn  = $("#filterInn").val();
    var filterName = $("#filterName").val();
    if((!filterInn=="")||(!filterName=="")){
        toFilter(filterInn, filterName);
      }else{$(".result").html("<div class='center'>Ничего не введено</div>");}
})

//обработчик скрытия попапа
$(".close, .shadow").click(function(){
    $('body').toggleClass('nopopup');
})

//фильтрация по инн или названию
function toFilter(inn, name) {
    $(".result").html("<div class='center'>Загрузка...</div>");
    if(inn==""){inn="ALL"}
    if(name==""){name="ALL"}
    amount=0//Сбросить количество
    $.ajax({
        url: INFO.serverAdr +"api/filter/"+inn+"."+name,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var list = "";
            $.each(dataset, function (index, data) {
                amount+=1;
                infoData[data.INN]=data;
                list += printRow(data);
            });
            console.log(dataset);
            if(amount==0){$(".result").html("<div class='center'>Ничего не найдено</div>");}
            if((amount>0)||(amount<100)){
                var naideno= "Найдено ";
                var lastDigit=amount.toString().split('').pop();console.log(lastDigit);
                if (lastDigit=="1"){var zapis=" запись";naideno= "Найдена ";}
                if ((lastDigit=="5")||(lastDigit=="6")||(lastDigit=="7")||(lastDigit=="8")||(lastDigit=="9")||(lastDigit=="0")){
                    var zapis=" записей"}
                if ((lastDigit=="2")||(lastDigit=="3")||(lastDigit=="4")){var zapis=" записи"}
                $(".result").html("<div class='center'>"+ naideno+ amount+ zapis+ "</div>");
                $(".result").append(list);
            }
            if(amount>=100){
                $(".result").html("<div class='center'>Первые 100 записей</div>");
                $(".result").append(list);}
         },
         error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);},
    });
}

//при нажатии на (I)
function info(inn) {
    $('body').toggleClass('nopopup');
    $('.popFields').empty();
    var data=infoData[inn];
    var text="";
    if(data.NAME_FULL){text+="<div><b>Название: </b><p>"+data.NAME_FULL+"</p></div>"}
    if(data.INN){text+="<div><b>ИНН:</b><p>"+data.INN+"</p></div>"}
    if(data.KPP){text+="<div><b>KПП: </b><p>"+data.KPP+"</p></div>"}
    if(data.LEGAL_ADDR){text+="<div><b>Юридический адрес: </b><p>"+data.LEGAL_ADDR+"</p></div>"}
    if(data.TELEPHON){text+="<div><b>Телефон(ы): </b><p>"+data.TELEPHON+"</p></div>"}
    if(data.BANK){text+="<div><b>Банк:</b><p>"+data.BANK+"</p></div>"}
    if(data.BANK_ACCOUNT){text+="<div><b>Банковский счет: </b><p>"+data.BANK_ACCOUNT+"</p></div>"}
    if(data.BOSS){text+="<div><b>Руководитель: </b><p>"+data.BOSS+"</p></div>"}
    if(data.BIK){text+="<div><b>БИК: </b><p>"+data.BIK+"</p></div>"}
    if(data.KOR_ACCOUNT){text+="<div><b>КОР Счет:</b><p>"+data.KOR_ACCOUNT+"</p></div>"}
    if(data.OGRN){text+="<div><b>ОГРН: </b><p>"+data.OGRN+"</p></div>"}
    if(data.Е_MAIL){text+="<div><b>Email: </b><p>"+data.Е_MAIL+"</p></div>"}
    if(data.ADM_FLAG){
        if(data.ADM_FLAG==1){
            text+="<div><b>Данное юридическое лицо принадлежит Администрации</b></div>"
        }
    }
    $('.popFields').append(text);
}

//сформировать строчку одной компании
function printRow(data) {
    var row= "<div class='result-row' data-rowid='" + data.INN + "'>"+
                "<div class='result-data'><b><a href='../inn:"+data.INN+"'>" + data.INN + "</a></b>"+
                    "<p><a href='../inn:"+data.INN+"'>" + data.NAME_FULL + "</a></p>"+
                "</div>"+
                "<div class='result-info'><button class='infoBtn' onclick='info("+data.INN+")'>Info</button></div>"+
            "</div>";
    return row;
}

