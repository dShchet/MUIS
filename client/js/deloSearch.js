
var infoData={};//данные подробностей организаций
var amount=0;//количество записей


$(".toSearch").click(function(e){
    var need=source=dateFrom=dateTo="";
    e.preventDefault();
    $("#need").val() ? need = $("#need").val() :need  ="search";
    $("#source").val() ? source = $("#source").val() :source  ="search";
    $("#dateFrom").val() ? dateFrom = $("#dateFrom").val() :dateFrom  ="search";
    $("#dateTo").val() ? dateTo = $("#dateTo").val() :dateTo  ="search";
    // toFilter(need, source, dateFrom, dateTo);
})

//обработчик скрытия попапа
$(".close, .shadow").click(function(){$('body').toggleClass('nopopup');});
//фильтрация по инн или названию
function toFilter(need, source, dateFrom, dateTo) {
    $(".result").html("<div class='center'>Загрузка...</div>");
    amount=0//Сбросить количество
    console.log(INFO.deloAdr+"?need="+need+"&source="+source+"&dateFrom="+dateFrom+"&dateTo="+dateTo);
    $.ajax({
        url: INFO.deloAdr+"?need="+need+
        "&source="+source+"&dateFrom="+dateFrom+"&dateTo="+dateTo,
        type: "GET", contentType: "text/plain",
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
    //INFO.clientAdr+"delo:" + $(this).data('rsisn')+"&type:"+$(this).data('rstype')
    $('.result-row').attr('href',function(){
        var href=INFO.clientAdr+"delo:" + $(this).data('rsisn')+"&type:"+$(this).data('rstype')
        return href});
    // $('.result-row').click(function(){
    //     var sel = getSelection().toString();
    //     if(!sel){
    //         var deloOneLink = INFO.clientAdr+"delo:" + $(this).data('rsisn')+"&type:"+$(this).data('rstype');
    //         $(this).attr('href',deloOneLink);
    //         window.location = deloOneLink;
    //     }
    // })
}

//сформировать строчку одной компании
function printRow(data) {
    var row= "<a href='' class='result-row' data-rsisn='" + data.ISN + "' data-rstype='" + data.DOCKIND + "'>"+
                "<div class='result-data'><b>" + data.RegNum + "</b>"+"<b style='float:right'>" + data.DocDate + "</b>"+
                    "<p>" + data.Contents + "</p>"+
                "</div>"+
            "</a>";
    return row;
}

$('.searchLeft p').click(function(){
    var el=$(this);
    if(!el.hasClass('active')){
        $('.searchLeft p.active').removeClass("active");
        el.addClass("active");
        var data_seacrh =el.data("seacrh");   
        $(".search").removeClass('week in let out inside jornal resol resolWeek files prj today').addClass(data_seacrh);
    }
})
$(function() {
    $(".toSearch").click(function(e){
        var dateFrom=dateTo=dateFrom_send=dateTo_send="";
        e.preventDefault();
        var dateFrom = $("#s-in-dateFrom").val();
        var dateTo   = $("#s-in-dateTo").val();
        var need="search";
        var source="table";
        if(checkDateFormat(dateFrom)&&(dateFrom!="")){
            dateFrom_send=dateFrom.replace(/[.]/g,"/");
        }else{
            console.log("incorrect dateFrom: "+dateFrom);
        }
        if(checkDateFormat(dateTo)&&(dateTo!="")){
            dateTo_send=dateTo.replace(/[.]/g,"/")
        }else{
            console.log("incorrect dateTo: "+dateTo);
        }
        if((dateTo_send!="")&&(dateFrom_send!="")){
            toFilter(need, source, dateFrom_send, dateTo_send);
        }else{console.log("incorrect dates");}

    });

    $('#s-in-dateReg').change(function(){
        var val =$(this).val();
        var datefrom = $("#s-in-dateFrom");
        var dateto   = $("#s-in-dateTo");
        
    });
    // var now=fixDate(Date.now() , 'num');
    // datefrom.val(now);
    
    // datefrom.attr("value", Date.now());
    // inputDateFrom.selectDate(new Date());
    // dateto.prop('disabled', true);
    
    // inputDateTo.selectDate(new Date());
    // datefrom.prop('disabled', true);
    //console.log(now);

    var inputDateFrom = $('#s-in-dateFrom').datepicker({
        autoClose: true
    }).data('datepicker');
    var inputDateTo = $('#s-in-dateTo').datepicker({
        autoClose: true
    }).data('datepicker');

    //проверка на форматы даты
    function checkDateFormat(date){
        regexp = /^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$/;
        return (regexp.test(date)) ? true : false
    }

    //Перевод dd.mm.yyyy в формат даты
    function toDateFormat(wrongDate) {
        var regex = /(\d\d)\.(\d\d)\.(\d\d\d\d)/;
        var tempDate = regex.exec(wrongDate);
        return new Date(tempDate[3], tempDate[2], tempDate[1]);
    }
});
