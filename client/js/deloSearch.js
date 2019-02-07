
var page=2;
var step=10;
var dataset=[];
var search={
    type: "week",
    dateFrom: new Date(2000,0,1),
    dateFromDot:"01.01.2000",
    dateTo: new Date(2019,0,31),
    // dateTo: new Date(),
    dateToDot:"31.01.2019",
};
var docH={"0":{}};
var docSel={};
// console.log(search);
dataset=[{"ISN":"4324","RegNum":"В-1","DocDate":"27.04.2012 ","Contents":"О применении инструкции №35 ФНС России","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Министерство финансов РФ (Минфин РФ)","OUTNUM":"07-1-1234","OUTDATE":"25.04.2012 ","SIGN":"Силуанов А.Г."}]},{"ISN":"4328","RegNum":"Р-2","DocDate":"27.04.2012 ","Contents":"О заседании акционеров","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Сбербанк России","OUTNUM":"36-15","OUTDATE":"16.04.2012 ","SIGN":"Греф Г.О."}]},{"ISN":"4333","RegNum":"Ф-3","DocDate":"27.04.2012 ","Contents":"О направлении регламента ","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Филиал Вологодской области","OUTNUM":"45/2012","OUTDATE":"12.04.2012 ","SIGN":"Воронина С.В."}]},{"ISN":"4337","RegNum":"В-1","DocDate":"27.04.2012 ","Contents":"Жалоба на организацию проведения ЕГЭ","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Валявская Т.М.","CITIZEN_CITY":"Ставрополь"}]},{"ISN":"4341","RegNum":"04-1","DocDate":"27.04.2012 ","Contents":"Запрос информации о выплатах","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4353","RegNum":"03.1-2","DocDate":"27.04.2012 ","Contents":"О заседании акционеров. Предложения.","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4363","RegNum":"Л-1","DocDate":"27.04.2012 ","Contents":"Об аренде","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4403","RegNum":"1","DocDate":"27.02.2011 ","Contents":"Об утверждении штатного расписания","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4418","RegNum":"2","DocDate":"28.03.2011 ","Contents":"Об утверждении инструкции по делопроизводству","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4432","RegNum":"3","DocDate":"28.04.2011 ","Contents":"Об утверждении инструкции по работе с СКЗИ","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4452","RegNum":"1-лс","DocDate":"27.04.2012 ","Contents":"О приеме на работу Стрельникова А.Д. в Отдел №2 Управления по основной деятельности на должность ведущего специалиста","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4460","RegNum":"1","DocDate":"27.04.2012 ","Contents":"Об утверждении списка сотрудников, имеющих допуск по грифу &quot;Коммерческая тайна&quot;","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4482","RegNum":"05-04/1","DocDate":"04.05.2012 ","Contents":"О подготовке отчетности о состоянии договорной работы","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Адвокатов П.Б. - Нач. отдела"}},{"ISN":"4488","RegNum":"03-03/2","DocDate":"04.05.2012 ","Contents":"Справка о проведении проверки в филиале по г. Санкт-Петербургу","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Портнов И.А. - Начальник управления"}},{"ISN":"4496","RegNum":"В-4","DocDate":"04.05.2012 ","Contents":"О переводе на новый порядок финансирования","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Министерство финансов РФ (Минфин РФ)","OUTNUM":"02-03-10-1603","OUTDATE":"28.04.2011 ","SIGN":"Силуанов А.Г."}]},{"ISN":"4512","RegNum":"ПР/1-2012","DocDate":"04.05.2012 ","Contents":"Об утверждении  плана работы на второе полугодие 2012 года","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4529","RegNum":"Д-2","DocDate":"04.05.2012 ","Contents":"Жалоба на работу жилищно-коммунальных служб","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Денисов Г.Р.","CITIZEN_CITY":"Москва"}]},{"ISN":"4644","RegNum":"Д-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации исходящего.","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4675","RegNum":"Р-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4694","RegNum":"Ан-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4724","RegNum":"Р-2","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4743","RegNum":"Кол-2","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4773","RegNum":"Р-3","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4822","RegNum":"Р-4","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4853","RegNum":"Р-5","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4868","RegNum":"Кол-4","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4892","RegNum":"Р-6","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4907","RegNum":"Р-7","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4922","RegNum":"Р-8","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]}];

$(".toSearch").click(function(e){
    var need=source=dateFrom=dateTo="";
    e.preventDefault();
    $("#need").val() ? need = $("#need").val() :need  ="search";
    $("#source").val() ? source = $("#source").val() :source  ="search";
    $("#dateFrom").val() ? dateFrom = $("#dateFrom").val() :dateFrom  ="search";
    $("#dateTo").val() ? dateTo = $("#dateTo").val() :dateTo  ="search";
    // getData(need, source, dateFrom, dateTo);
})

//Отправить запрос
function getData() {
    $(".resultTitle").html("<div class='center'>Загрузка...</div>");
    amount=0//Сбросить количество
    var need="search";
    var source="table";
    //console.log(search);
    var sendUrl=INFO.deloAdr;
    sendUrl+="?need="+need;
    sendUrl+="&source="+search.type;
    sendUrl+="&dateFrom="+search.dateFrom_send;
    sendUrl+="&dateTo="+search.dateTo_send;

    function makeResultTitle(amount){
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

    // console.log(sendUrl);
    // $.ajax({
    //     url: sendUrl,
    //     type: "GET", contentType: "text/plain",
    //     success: function (data) {
    //         dataset=data;
    //         console.log(dataset);
    //         console.log(sendUrl);
            makeResultTitle(dataset.length);
            page=1
            buildData();
    //     },
    //     error: function (jqXHR, exception) {
    //         $(".resultTitle").html("<div class='center'>Ошибка</div>");
    //        console.log("Ошибка: "+jqXHR+"; exception: "+exception);
    //        console.log(jqXHR);},
    // });
}

//Сформировать вывод (либо после загрузки, либо обновление)
function buildData(pageValue){
    page=(pageValue)?pageValue:page;
    // console.log("step "+step);
    // console.log("page "+page);
    //вывести список елементов
    var list="";
    var from =(page-1)*step;
    var to   =page*step;
    var datasetPage = dataset.slice(from, to);
    $.each(datasetPage, function (index, data) {
        list += buildRow(data);
    });
    $(".resultList").html(list);

    //привязать ссылки к элементам
        //установить href ссылкам
        $('.result-row').attr('href',function(){
            var href=INFO.clientAdr+"delo:" + $(this).data('rsisn')+"&type:"+$(this).data('rstype')
            return href});

        //обработка выделения и перехода ссылке  
        $('.result-row').click(function(e){
            e.preventDefault();
            var sel = getSelection().toString();
            if(!sel){
                window.open($(this).attr('href'), '_blank')
            }
        })

    //вывести пагинацию
    stepsAmount=Math.ceil(dataset.length/step);
    var html="";
    if(stepsAmount>1){
        html+="<div class='stepsWrap'>";
        for (i = 1; i < stepsAmount+1; i++) {
            if(i==page){
                  html+="<div onclick='buildData( "+i+")'class='step active' >"+i+"</div>";
            }else{html+="<div onclick='buildData( "+i+")'class='step' >"+i+"</div>";}
        }
        html+="</div>";
    }
    if(dataset.length>10){
        html+="<div class='stepsSizeWrap'>";
        html+="<select onchange='step=this.options[this.selectedIndex].value;buildData(1);'>";
        var arr=[5,10,30,50,100,200,500,1000];
        for (i = 0; i < arr.length; i++){
            if((arr[i]<=dataset.length)&&(step==arr[i])){
                html+="<option value='"+arr[i]+"' selected>"+arr[i]+"</option>";    
            }else if(arr[i-1]<=dataset.length){
                html+="<option value='"+arr[i]+"'>"+arr[i]+"</option>";
            }
        }
        html+="</select>";
        html+="</div>";
    }
    $(".resultSteps").html(html);
}

//сформировать строчку одной компании
function buildRow(data) {
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

//После загрузки страницы
$(function() {

    $("#dateFrom").data("date", search.dateFrom);
    $("#dateTo").data("date", search.dateTo);
    $("#dateFrom").val(search.dateFromDot);
    $("#dateTo").val(search.dateToDot);

    function makeDotDate(date){
        var year =date.getFullYear();
        var month=["01","02","03","04","05","06","07","08","09","10","11","12"][date.getMonth()];
        var day=date.getDate();
        return day+"."+month+"."+year;
    }

    function createCalendars(){
        var inputDateFrom = $('#dateFrom')
        .datepicker({autoClose: true})
        .data('datepicker');
        inputDateFrom.selectDate(new Date($('#dateFrom').data("date")));
        
        var inputDateTo = $('#dateTo')
        .datepicker({autoClose: true})
        .data('datepicker')
        inputDateTo.selectDate(new Date($('#dateTo').data("date")));
    }
    createCalendars();

    $(".selectType select").on("change",function(){
        var value=$(this).val();
        $(".selectMore").removeClass("week in let out inside jornal resol resolWeek files prj today");
        $(".selectMore").addClass(value);
        if((value=="week")||(value=="resolWeek")){
            search.dateTo=new Date();
            search.dateFrom.setDate(search.dateTo.getDate()-7); 
            search.dateToDot=makeDotDate(search.dateTo)
            search.dateFromDot=makeDotDate(search.dateFrom)
            $("#dateFrom").data("date", search.dateFrom);
            $("#dateTo").data("date", search.dateTo);
            $("#dateFrom").val(search.dateFromDot);
            $("#dateTo").val(search.dateToDot);
            createCalendars();
        }
        if(value=="today"){
            search.dateFrom=search.dateTo=new Date();
            search.dateFromDot=search.dateToDot=makeDotDate(search.dateFrom);
            $("#dateFrom").data("date", search.dateFrom);
            $("#dateTo").data("date", search.dateTo);
            $("#dateFrom").val(search.dateFromDot);
            $("#dateTo").val(search.dateToDot);
            createCalendars();
        }
    });
    
    $(".toSearch").click(function(e){
        
        search.dateFrom = $("#dateFrom").val();
        search.dateTo   = $("#dateTo").val();
        var selectedArea=$(".selectType select").val();
        switch (selectedArea) {
            case 'week'      : {
                search.type= 'All';
            } break;
            case 'in'        : {
                search.type= 'In';
            } break;
            case 'let'       : {
                search.type= 'Let';
            } break;
            case 'out'       : {
                search.type= 'Out';
            } break;
            case 'inside'    : {
                search.type= 'All';
            } break;
            case 'jornal'    : {
                search.type= 'Jor';
            } break;
            case 'resol'     : {
                search.type= 'Res';
            } break;
            case 'resolWeek' : {
                search.type= 'Res';

            } break;
            // case 'files'     : {
            //     search.type= 'All';
            // } break;
            case 'prj'       : {
                search.type= 'Prj';
            } break;
            case 'today'     : {
                search.type= 'All';
            } break;
        }
        
        if(checkDateFormat(search.dateFrom)&&(search.dateFrom!="")){
            search.dateFrom_send=search.dateFrom.replace(/[.]/g,"/");
        }else{
            alert("Неправельная дата До");
        }
        if(checkDateFormat(search.dateTo)&&(search.dateTo!="")){
            search.dateTo_send=search.dateTo.replace(/[.]/g,"/")
        }else{
            alert("Неправельная дата После");
        }
        if((search.dateTo_send!="")&&(search.dateFrom_send!="")){
            // console.log(search)
           getData();
        }else{alert("incorrect dates");}
    });

    function showDoc(){
        $('body').toggleClass('nopopup');
        // $('.popup .title').html('Группы документов');
        var html="";
        // $(".popupWrap").toggleClass('docH');
        getDoc(0);
        // getDoc(3670);
        // getDoc(3682);


        
    }
    showDoc();
    function bindNode(){
        // $('.node').click(function(){
        //     var id=$(this).attr('id');
        //     var isn=id.replace('doc_',"");
        //     // console.log(id);
        //     console.log("bindNode with " +isn);
        //     getDoc(isn);
        // });
    }
    function buildNodeLeft(isn){
        var html="<div class='children'>";
        el=docH[isn];
        var children=el['childs'];
        for (i = 0; i < children.length; i++){
            var item=docH[children[i]];
            var itemId="doc_"+item["ISN"];
            if(item["ISNODE"]=="True"){
                tempClass="nodeL";
                html+="<div>";
                html+="<div class='openBox'></div>";
            }else{
                tempClass="notNodeL";
                html+="<div class='docH_row_L'>";
            }          
            html+="<p id='"+itemId+"' class='"+tempClass+"'>"+item["NAME"]+"</p>";
            html+="</div>";
        }
        html+="</div>";
        $(html).insertAfter("#doc_"+isn);
        $(".nodeL").unbind( "click" );
        $("#doc_"+isn).addClass('opened');
        $('.nodeL').bind("click",function(){
            var id=$(this).attr('id');
            var isn=id.replace('doc_',"");
            getDoc(isn);
        });
    }
    function buildNodeRight(isn){
        var html="<div style='padding-left:20px'>";
        el=docH[isn];
        if(isn!=0){
            html+="<div class='docH_parent_R'><p class='parent-right ' data-id='"+el["ParentIsn"]+"'>"+el["NAME"]+"</p></div>";
        }
        var children=el['childs'];
        for (i = 0; i < children.length; i++){
            var item=docH[children[i]];
            var itemId="doc_"+item["ISN"];
            var tempClass=(item["ISNODE"]=="True")?"nodeR":"notnodeR";
            html+="<div class='docH_row_R'><div class='sel' data-isn='"+itemId+"'></div>"
            +"<p data-isn='"+itemId+"' class='"+tempClass+"'>"+item["NAME"]+"</p></div>";
        }
        html+="</div>";
        $('.docH_right').html(html);
        $('.parent-right').bind("click",function(){
            var isn=$(this).data('id');
            getDoc(isn, true);
        });
        $('.nodeR').bind("click",function(){
            var isn=$(this).data('isn').replace('doc_',"");
            getDoc(isn);
        });
        $('.sel').bind("click",function(){
            el=$(this);
            var isn=$(this).data('isn').replace('doc_',"");
            if(el.hasClass('selected')){
                delete docSel[isn];
                el.removeClass('selected');
            }else{
                el.addClass('selected');
                docSel[isn]={};
                docSel[isn]["NAME"]=docH[isn]["NAME"];
                docSel[isn]["DCODE"]=docH[isn]["DCODE"];
            }
            console.log(docSel);
        });
    }

    function getDoc(isn, up){
        
        var html="";     
        if((docH.hasOwnProperty(isn))&&((up==true)||(docH[isn].hasOwnProperty('childs')))){
            console.log('load from memory');
           // buildNodeLeft(isn) //встроить Слева
            buildNodeRight(isn)//встроить Справа
        }else{
            var url = INFO.deloAdr+"?need="+"lib"+"&isn="+isn+"&type=doc";
            console.log(url);
            $('.docH_right').html('Загрузка');
            $.ajax({
                url: INFO.deloAdr+"?need="+"lib"+"&isn="+isn+"&type=doc",
                type: "GET", contentType: "text/plain",
                success: function (newData) {
                    var childs=[];
                    for (var key in newData) {
                        var item=newData[key];
                        childs.push(item["ISN"]);
                        docH[item["ISN"]]=item;
                    }
                    docH[isn]["childs"]=childs; 
                    buildNodeLeft(isn) //встроить Слева
                    buildNodeRight(isn)//встроить Справа
                },
                error: function (jqXHR, exception) {
                   console.log("Ошибка: "+jqXHR+"; exception: "+exception);
                   console.log(jqXHR);},
            });
        }
        //return html;
    }
    $(".btnMenu, .menuClose, .menuShadow").click(function(){
        $('body').toggleClass('menuOpen');
    });
    //обработчик скрытия попапа
    $(".fullScreen").click(function(){
        $('.popupWrap').toggleClass('docH-fullsceen');
        // $(".popupWrap").toggleClass('docH');
    });
    // $('#s-in-dateReg').click(function(){
    //     function makeDotDate(date){
    //         var year =date.getFullYear();
    //         var month=["01","02","03","04","05","06","07","08","09","10","11","12"][date.getMonth()];
    //         var day=date.getDate();
    //         return day+"."+month+"."+year;
    //     }
    //     var val =$(this).val();
    //     var toDate = new Date();
    //     var fromDate = new Date();
    //     switch (val) {
    //         // case 'hnd'    :{} break;
    //         // case 'now'    :{} break;
    //         case 'week'   :{fromDate.setDate(fromDate.getDate()-7);    } break;
    //         case 'month'  :{fromDate.setMonth(fromDate.getMonth()-1);  } break;
    //         case 'quarter':{fromDate.setMonth(fromDate.getMonth()-3);  } break;
    //         case 'year'   :{fromDate.setYear(fromDate.getFullYear()-1);} break;
    //     }
    //     if(val!='hnd'){
    //         search.dateFrom=makeDotDate(fromDate);
    //         search.dateTo=makeDotDate(toDate);
    //         search.dateFromDot=makeDotDate(fromDate);
    //         search.dateToDot=makeDotDate(toDate);
    //         console.log(search.dateFrom);
    //         $("#dateFrom").data("date", fromDate);
    //         $("#dateTo").data("date", toDate);
    //         $("#dateFrom").val(search.fromDate);
    //         $("#dateTo").val(search.dateTo);
    //         createCalendars();
    //     }        
    // });

    // var now=fixDate(Date.now() , 'num');
    // datefrom.val(now);
    
    // datefrom.attr("value", Date.now());
    // inputDateFrom.selectDate(new Date());
    // dateto.prop('disabled', true);
    
    // inputDateTo.selectDate(new Date());
    // datefrom.prop('disabled', true);
    //console.log(now);

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
    //обработчик скрытия попапа
    $(".close, .shadow").click(function(){
        $('body').toggleClass('nopopup');
        console.log(docSel);
        var html=value="";
        var i=0;
        for (var key in docSel) {
            var item=docSel[key];
            html+="<div class='oneGroup' class='group_"+key+"'><div data-isn='"+key+"' class='removeOne'>X</div><p>"+item["NAME"]+"</p></div>"
            if(i==0){
                value+=    item["DCODE"]; }else{
                value+="|"+item["DCODE"];
            }
            i=1;
        }
        console.log(value);
        $('.selectArea').html(html);
    });
});
