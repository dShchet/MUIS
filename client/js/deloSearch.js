
var page=2;
var step=10;
var dataset;
dataset=[{"ISN":"4324","RegNum":"В-1","DocDate":"27.04.2012 ","Contents":"О применении инструкции №35 ФНС России","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Министерство финансов РФ (Минфин РФ)","OUTNUM":"07-1-1234","OUTDATE":"25.04.2012 ","SIGN":"Силуанов А.Г."}]},{"ISN":"4328","RegNum":"Р-2","DocDate":"27.04.2012 ","Contents":"О заседании акционеров","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Сбербанк России","OUTNUM":"36-15","OUTDATE":"16.04.2012 ","SIGN":"Греф Г.О."}]},{"ISN":"4333","RegNum":"Ф-3","DocDate":"27.04.2012 ","Contents":"О направлении регламента ","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Филиал Вологодской области","OUTNUM":"45/2012","OUTDATE":"12.04.2012 ","SIGN":"Воронина С.В."}]},{"ISN":"4337","RegNum":"В-1","DocDate":"27.04.2012 ","Contents":"Жалоба на организацию проведения ЕГЭ","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Валявская Т.М.","CITIZEN_CITY":"Ставрополь"}]},{"ISN":"4341","RegNum":"04-1","DocDate":"27.04.2012 ","Contents":"Запрос информации о выплатах","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4353","RegNum":"03.1-2","DocDate":"27.04.2012 ","Contents":"О заседании акционеров. Предложения.","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4363","RegNum":"Л-1","DocDate":"27.04.2012 ","Contents":"Об аренде","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4403","RegNum":"1","DocDate":"27.02.2011 ","Contents":"Об утверждении штатного расписания","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4418","RegNum":"2","DocDate":"28.03.2011 ","Contents":"Об утверждении инструкции по делопроизводству","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4432","RegNum":"3","DocDate":"28.04.2011 ","Contents":"Об утверждении инструкции по работе с СКЗИ","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4452","RegNum":"1-лс","DocDate":"27.04.2012 ","Contents":"О приеме на работу Стрельникова А.Д. в Отдел №2 Управления по основной деятельности на должность ведущего специалиста","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4460","RegNum":"1","DocDate":"27.04.2012 ","Contents":"Об утверждении списка сотрудников, имеющих допуск по грифу &quot;Коммерческая тайна&quot;","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4482","RegNum":"05-04/1","DocDate":"04.05.2012 ","Contents":"О подготовке отчетности о состоянии договорной работы","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Адвокатов П.Б. - Нач. отдела"}},{"ISN":"4488","RegNum":"03-03/2","DocDate":"04.05.2012 ","Contents":"Справка о проведении проверки в филиале по г. Санкт-Петербургу","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Портнов И.А. - Начальник управления"}},{"ISN":"4496","RegNum":"В-4","DocDate":"04.05.2012 ","Contents":"О переводе на новый порядок финансирования","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Министерство финансов РФ (Минфин РФ)","OUTNUM":"02-03-10-1603","OUTDATE":"28.04.2011 ","SIGN":"Силуанов А.Г."}]},{"ISN":"4512","RegNum":"ПР/1-2012","DocDate":"04.05.2012 ","Contents":"Об утверждении  плана работы на второе полугодие 2012 года","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4529","RegNum":"Д-2","DocDate":"04.05.2012 ","Contents":"Жалоба на работу жилищно-коммунальных служб","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Денисов Г.Р.","CITIZEN_CITY":"Москва"}]},{"ISN":"4644","RegNum":"Д-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации исходящего.","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4675","RegNum":"Р-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4694","RegNum":"Ан-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4724","RegNum":"Р-2","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4743","RegNum":"Кол-2","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4773","RegNum":"Р-3","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4822","RegNum":"Р-4","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4853","RegNum":"Р-5","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4868","RegNum":"Кол-4","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4892","RegNum":"Р-6","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4907","RegNum":"Р-7","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4922","RegNum":"Р-8","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]}];

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
    $(".resultTitle").html("<div class='center'>Загрузка...</div>");
    amount=0//Сбросить количество
    function makeAmountTitle(amount){
        if(amount==0){$(".resultTitle").html("<div class='center'>Ничего не найдено</div>");}
        else if((amount>0)||(amount<100)){
            var naideno= "Найдено ";
            var lastDigit=amount.toString().split('').pop();
            if (lastDigit=="1"){var zapis=" запись";naideno= "Найдена ";}
            if ((lastDigit=="5")||(lastDigit=="6")||(lastDigit=="7")||(lastDigit=="8")||(lastDigit=="9")||(lastDigit=="0")){
                var zapis=" записей"}
            if ((lastDigit=="2")||(lastDigit=="3")||(lastDigit=="4")){var zapis=" записи"}
            $(".resultTitle").html("<div class='center'>"+ naideno+ amount+ zapis+ "</div>");
        }
        else if(amount>=100){
            $(".resultTitle").html("<div class='center'>Первые 100 записей</div>");
        }
    }

    function makePagination(length, step, page){
        stpesAmount=Math.ceil(length/step);
        var html="";
        html+="<div class='stepsWrap'>";
        for (i = 1; i < stpesAmount+1; i++) {
            if(i==page){
                html+="<div class='step active' onclick='makeRows( "+i+")'>"+i+"</div>";
            }else{
                html+="<div class='step' onclick='makeRows( "+i+")'>"+i+"</div>";
            }
        }
        html+="</div>";
        $(".resultSteps").html(html);
    }
    // $.ajax({
    //     url: INFO.deloAdr+"?need="+need+
    //     "&source="+source+"&dateFrom="+dateFrom+"&dateTo="+dateTo,
    //     type: "GET", contentType: "text/plain",
    //     success: function (data) {
    //     dataset=data;
            console.log(dataset);
            makeAmountTitle(dataset.length);
            makeRows(page);
            makePagination(dataset.length, step, page)
    //     },
    //     error: function (jqXHR, exception) {
    //         $(".resultTitle").html("<div class='center'>Ошибка</div>");
    //        console.log("Ошибка: "+jqXHR+"; exception: "+exception);
    //        console.log(jqXHR);},
    // });
}
function makeRows(page){
    var list="";
    var from =(page-1)*step;
    var to   =page*step;
    var datasetPage = dataset.slice(from, to);
    $.each(datasetPage, function (index, data) {
        list += printRow(data);
    });
    $(".resultList").html(list);
    bindDelos();
}

function bindDelos(){
    //INFO.clientAdr+"delo:" + $(this).data('rsisn')+"&type:"+$(this).data('rstype')
    $('.result-row').attr('href',function(){
        var href=INFO.clientAdr+"delo:" + $(this).data('rsisn')+"&type:"+$(this).data('rstype')
        return href});
        
    $('.result-row').click(function(e){
        e.preventDefault();
        var sel = getSelection().toString();
        if(!sel){
            window.open($(this).attr('href'), '_blank')
        }
    })
}

//сформировать строчку одной компании
function printRow(data) {
    var row= "<a href='' class='result-row' data-rsisn='" + data.ISN + "' data-rstype='" + data.DOCKIND + "'>"+
                "<div class='result-data'><b>" + data.RegNum + "</b>"+"<b style='float:right'>" + data.DocDate + "</b>"+
                    "<p>" + data.Contents + "</p>";
            if((data.DOCKIND=="RCIN")&&(data.CORRESP)){
                var el=data.CORRESP[0];
                var ORGANIZ=SIGN=OUTNUM=OUTDATE='';
                if(el.ORGANIZ_NAME){ORGANIZ=el.ORGANIZ_NAME}
                if(el.SIGN){SIGN="- "+el.SIGN}
                row+="<p>" + ORGANIZ + SIGN+"</p>";
                if(el.OUTNUM){OUTNUM="Исх. №:  "+el.OUTNUM+" ";}
                if(el.OUTDATE){OUTDATE="от "+el.OUTDATE}
                row+="<p>" + OUTNUM+OUTDATE+"</p>";
            }
            if((data.DOCKIND=="RCLET")&&(data.AUTHOR)){
                var el=data.AUTHOR[0];
                var NAME=CITY='';
                if(el.CITIZEN_NAME){NAME=el.CITIZEN_NAME}
                if(el.CITIZEN_CITY){CITY=" "+el.CITIZEN_CITY}
                if(el.SIGN){SIGN="- "+el.SIGN}
                row+="<p>" + NAME + CITY+"</p>";
            }
            if((data.DOCKIND=="RCOUT")&&(data.PERSONSIGN)){
                var el=data.PERSONSIGN;
                var SIGN='';
                if(el.WHO_SIGN_NAME){SIGN=el.WHO_SIGN_NAME}
                row+="<p>" + SIGN+"</p>";
            }
            row+=
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
