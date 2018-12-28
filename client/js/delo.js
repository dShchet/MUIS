
var infoData={};//данные подробностей организаций
var amount=0;//количество записей


$("#toSearch").click(function(e){
    var need, source, dateFrom, dateTo;
    e.preventDefault();
    $("#need").val() ? need = $("#need").val() :need  ="search";
    $("#source").val() ? source = $("#source").val() :source  ="search";
    $("#dateFrom").val() ? dateFrom = $("#dateFrom").val() :dateFrom  ="search";
    $("#dateTo").val() ? dateTo = $("#dateTo").val() :dateTo  ="search";
    
    toFilter(need, source, dateFrom, dateTo);
})

//обработчик скрытия попапа
$(".close, .shadow").click(function(){$('body').toggleClass('nopopup');});
//фильтрация по инн или названию
function toFilter(need, source, dateFrom, dateTo) {
    $(".result").html("<div class='center'>Загрузка...</div>");
    amount=0//Сбросить количество
    $.ajax({
        url: INFO.deloAdr+"?need="+need+
        "&source="+source+"&dateFrom="+dateFrom+"&dateTo="+dateTo,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var list = "";
            $.each(dataset, function (index, data) {
                amount+=1;
                infoData[data.ISN]=data;
                list += printRow(data);
            });
            if(amount==0){$(".result").html("<div class='center'>Ничего не найдено</div>");}
            if((amount>0)||(amount<100)){
                var naideno= "Найдено ";
                var lastDigit=amount.toString().split('').pop();
                if (lastDigit=="1"){var zapis=" запись";naideno= "Найдена ";}
                if ((lastDigit=="5")||(lastDigit=="6")||(lastDigit=="7")||(lastDigit=="8")||(lastDigit=="9")||(lastDigit=="0")){
                    var zapis=" записей"}
                if ((lastDigit=="2")||(lastDigit=="3")||(lastDigit=="4")){var zapis=" записи"}
                $(".result").html("<div class='center'>"+ naideno+ amount+ zapis+ "</div>");
                $(".result").append(list);bindDelos();
            }
            if(amount>=100){
                $(".result").html("<div class='center'>Первые 100 записей</div>");
                $(".result").append(list);bindDelos();}
         },
         error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);
            console.log(jqXHR);},
    });
}
function bindDelos(){
    $('.result-row').click(function(){
        var sel = getSelection().toString();
        if(!sel){
            var deloOneLink = INFO.clientAdr+"delo:" + $(this).data('rowid');
            window.location = deloOneLink;
        }
    })
}

//сформировать строчку одной компании
function printRow(data) {
    var row= "<div class='result-row' data-rowid='" + data.ISN + "'>"+
                "<div class='result-data'><b>" + data.RegNum + "</b>"+"<b style='float:right'>" + data.DocDate + "</b>"+
                    "<p>" + data.Contents + "</p>"+
                "</div>"+
            "</div>";
    return row;
}

