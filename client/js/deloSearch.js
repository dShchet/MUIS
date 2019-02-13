
var page=1;//Начальная страница
var step=10;//Количество записей на странице
var dataset=[];//для приема РК записей
var search={//Дефолтные значения даты поиска
    type: "week",
    dateFrom: new Date(2000,0,1),
    dateFromDot:"01.01.2000",
    dateTo: new Date(2019,0,31),
    // dateTo: new Date(),
    dateToDot:"31.01.2019",
};
//Коды групп в Интеркоме
//var docH_local={"0":{"childs": ["3670", "3680", "3682"], "selected": false}, "3670": {"NAME": "Входящие", "ISN": "3670", "DCODE": "0.2TZ.", "ISNODE": "True", "ParentIsn": "0"}, "3680": {"NAME": "Исходящие", "ISN": "3680", "DCODE": "0.2U9.", "ISNODE": "True", "ParentIsn": "0"}, "3682": {"NAME": "Внутренние", "ISN": "3682", "DCODE": "0.2UB.", "ISNODE": "True", "ParentIsn": "0"}}
//Коды групп в Администрации
var docH_Adm=docHJSON;//from docHJSON.js
//Пустые коды групп для дебага и в администрации
var docH_empty={"0":{}};
if(DEBUG==true){
    // var docH=docH_local;
    var docH=docH_Adm;
}else{
    var docH=docH_empty;
}

var docSel={};//Выбранные группы

//демо данные
DEBUG_dataset=[{"ISN":"4324","RegNum":"В-1","DocDate":"27.04.2012 ","Contents":"О применении инструкции №35 ФНС России","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Министерство финансов РФ (Минфин РФ)","OUTNUM":"07-1-1234","OUTDATE":"25.04.2012 ","SIGN":"Силуанов А.Г."}]},{"ISN":"4328","RegNum":"Р-2","DocDate":"27.04.2012 ","Contents":"О заседании акционеров","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Сбербанк России","OUTNUM":"36-15","OUTDATE":"16.04.2012 ","SIGN":"Греф Г.О."}]},{"ISN":"4333","RegNum":"Ф-3","DocDate":"27.04.2012 ","Contents":"О направлении регламента ","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Филиал Вологодской области","OUTNUM":"45/2012","OUTDATE":"12.04.2012 ","SIGN":"Воронина С.В."}]},{"ISN":"4337","RegNum":"В-1","DocDate":"27.04.2012 ","Contents":"Жалоба на организацию проведения ЕГЭ","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Валявская Т.М.","CITIZEN_CITY":"Ставрополь"}]},{"ISN":"4341","RegNum":"04-1","DocDate":"27.04.2012 ","Contents":"Запрос информации о выплатах","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4353","RegNum":"03.1-2","DocDate":"27.04.2012 ","Contents":"О заседании акционеров. Предложения.","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4363","RegNum":"Л-1","DocDate":"27.04.2012 ","Contents":"Об аренде","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4403","RegNum":"1","DocDate":"27.02.2011 ","Contents":"Об утверждении штатного расписания","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4418","RegNum":"2","DocDate":"28.03.2011 ","Contents":"Об утверждении инструкции по делопроизводству","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4432","RegNum":"3","DocDate":"28.04.2011 ","Contents":"Об утверждении инструкции по работе с СКЗИ","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4452","RegNum":"1-лс","DocDate":"27.04.2012 ","Contents":"О приеме на работу Стрельникова А.Д. в Отдел №2 Управления по основной деятельности на должность ведущего специалиста","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4460","RegNum":"1","DocDate":"27.04.2012 ","Contents":"Об утверждении списка сотрудников, имеющих допуск по грифу &quot;Коммерческая тайна&quot;","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4482","RegNum":"05-04/1","DocDate":"04.05.2012 ","Contents":"О подготовке отчетности о состоянии договорной работы","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Адвокатов П.Б. - Нач. отдела"}},{"ISN":"4488","RegNum":"03-03/2","DocDate":"04.05.2012 ","Contents":"Справка о проведении проверки в филиале по г. Санкт-Петербургу","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Портнов И.А. - Начальник управления"}},{"ISN":"4496","RegNum":"В-4","DocDate":"04.05.2012 ","Contents":"О переводе на новый порядок финансирования","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"Министерство финансов РФ (Минфин РФ)","OUTNUM":"02-03-10-1603","OUTDATE":"28.04.2011 ","SIGN":"Силуанов А.Г."}]},{"ISN":"4512","RegNum":"ПР/1-2012","DocDate":"04.05.2012 ","Contents":"Об утверждении  плана работы на второе полугодие 2012 года","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4529","RegNum":"Д-2","DocDate":"04.05.2012 ","Contents":"Жалоба на работу жилищно-коммунальных служб","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Денисов Г.Р.","CITIZEN_CITY":"Москва"}]},{"ISN":"4644","RegNum":"Д-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации исходящего.","DOCKIND":"RCOUT","PERSONSIGN":{"WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор"}},{"ISN":"4675","RegNum":"Р-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4694","RegNum":"Ан-1","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4724","RegNum":"Р-2","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4743","RegNum":"Кол-2","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4773","RegNum":"Р-3","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4822","RegNum":"Р-4","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4853","RegNum":"Р-5","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4868","RegNum":"Кол-4","DocDate":"04.06.2012 ","Contents":"Проверка регистрации обращения гражданина.","DOCKIND":"RCLET","AUTHOR":[{"CITIZEN_NAME":"Тихорин В.И.","CITIZEN_CITY":"Санкт-Петербург"}]},{"ISN":"4892","RegNum":"Р-6","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4907","RegNum":"Р-7","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]},{"ISN":"4922","RegNum":"Р-8","DocDate":"04.06.2012 ","Contents":"Проверка регистрации входящего документа.","DOCKIND":"RCIN","CORRESP":[{"ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В."}]}];




//Сформировать вывод (либо после загрузки, либо обновление)
function buildData(pageValue){
    page=(pageValue)?pageValue:page;
    // console.log("step "+step);
    // console.log("page "+page);
    //вывести список елементов
    var list=table="";
    var from =(page-1)*step;
    var to   =page*step;
    var datasetPage = dataset.slice(from, to);
    $.each(datasetPage, function (index, data) {
        list += buildRow(data);
        table+=buildTR(data);
    });
    $(".resultList").html(list);
    $(".resultTable .tbody").html(table);

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
        });

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
            if((data.DOCKIND=="RCIN")&&(data.corresp)){
                var el=data.corresp;
                console.log(el);
                var ORGANIZ=SIGN=OUTNUM=OUTDATE='';
                if(el.ORGANIZ_NAME){ORGANIZ=el.ORGANIZ_NAME}
                if(el.SIGN){SIGN="- "+el.SIGN}
                row+="<p>" + ORGANIZ + SIGN+"</p>";
                if(el.OUTNUM){OUTNUM="Исх. №:  "+el.OUTNUM+" ";}
                if(el.OUTDATE){OUTDATE="от "+el.OUTDATE}
                row+="<p>" + OUTNUM+OUTDATE+"</p>";
            }
            if((data.DOCKIND=="RCLET")&&(data.author)){
                var el=data.author;
                var NAME=CITY='';
                if(el.CITIZEN_NAME){NAME=el.CITIZEN_NAME}
                if(el.CITIZEN_CITY){CITY=" "+el.CITIZEN_CITY}
                if(el.SIGN){SIGN="- "+el.SIGN}
                row+="<p>" + NAME + CITY+"</p>";
            }
            if((data.DOCKIND=="RCOUT")&&(data.personsign)){
                var el=data.personsign;
                var SIGN='';
                if(el.WHO_SIGN_NAME){SIGN=el.WHO_SIGN_NAME}
                row+="<p>" + SIGN+"</p>";
            }
            row+=
        "</div>"+
    "</a>";
    return row;
}
function buildTR(data) {
    var row= "<a href='' class='result-tr' data-rsisn='" + data.ISN + "' data-rstype='" + data.DOCKIND + "'>"+
                "<div><b>" + data.RegNum +"</b></div><div><b>"+ data.DocDate + "</b></div>"+
                    "<div>" + data.Contents + "</div>";
            if((data.DOCKIND=="RCIN")&&(data.corresp)){
                var el=data.corresp;
                var ORGANIZ=SIGN=OUTNUM=OUTDATE='';
                if(el.ORGANIZ_NAME){ORGANIZ=el.ORGANIZ_NAME}
                if(el.SIGN){SIGN="- "+el.SIGN}
                row+="<div>" + ORGANIZ + SIGN+"</div>";
                if(el.OUTNUM){OUTNUM="Исх. №:  "+el.OUTNUM+" ";}
                if(el.OUTDATE){OUTDATE="от "+el.OUTDATE}
                row+="<div>" + OUTNUM+OUTDATE+"</div>";
            }
            if((data.DOCKIND=="RCLET")&&(data.author)){
                var el=data.author;
                var NAME=CITY='';
                if(el.CITIZEN_NAME){NAME=el.CITIZEN_NAME}
                if(el.CITIZEN_CITY){CITY=" "+el.CITIZEN_CITY}
                if(el.SIGN){SIGN="- "+el.SIGN}
                row+="<div>" + NAME + CITY+"</div>";
            }
            if((data.DOCKIND=="RCOUT")&&(data.personsign)){
                var el=data.personsign;
                var SIGN='1 ';
                if(el.WHO_SIGN_NAME){SIGN=el.WHO_SIGN_NAME}
                row+="<div><p>" + SIGN+"</p></div>";
            }
            row+="</a>";
    return row;
}

//После загрузки страницы
$(function() {

    $("#dateFrom").data("date", search.dateFrom);
    $("#dateTo").data("date", search.dateTo);
    $("#dateFrom").val(search.dateFromDot);
    $("#dateTo").val(search.dateToDot);

    createCalendars();
    bind_selectedArea();
    
    //Отправить запрос
    function getData() {
        $(".resultTitle").html("<div class='center'>Загрузка...</div>");
        amount=0//Сбросить количество
        var need="search";
        var sendUrl=INFO.deloAdr;
        sendUrl+="?need="+need;
        sendUrl+="&source="+search.type;
        sendUrl+="&dateFrom="+search.dateFrom_send;
        sendUrl+="&dateTo="+search.dateTo_send;

        //console.log(search);
        console.log(sendUrl);
        if(DEBUG==true){
            dataset=DEBUG_dataset;
            console.log(dataset);
            makeResultTitle(dataset.length);
            page=1
            buildData();
        }else{
            $.ajax({
                url: sendUrl,
                type: "GET", contentType: "text/plain",
                success: function (data) {
                    dataset=data;
                    console.log(dataset);
                    // console.log(sendUrl);
                    makeResultTitle(dataset.length);
                    page=1
                    buildData();
                },
                error: function (jqXHR, exception) {
                    $(".resultTitle").html("<div class='center'>Ошибка</div>");
                console.log("Ошибка: "+jqXHR+"; exception: "+exception);
                console.log(jqXHR);},
            });
        }
        

        //построить заголовок
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

    }
    //При нажатии "Поиск"
    $(".toSearch").click(function(e){
        //Сохранить коды групп
        var value="";
        var i=0;
        for (var key in docSel) {
            var item=docSel[key];
            var val
            if(item["ISNODE"]=="True"){
                val=item["DCODE"] + "%";
            }else{
                val=item["DCODE"];
            }
            if(i==0){
                    value+=val;
            }else{
                value+="|"+val;
            }
            i=1;
        }
        search.type=value;

        //Сохранить даты
        search.dateFrom = $("#dateFrom").val();
        search.dateTo   = $("#dateTo").val();
        
        //Проверка дат
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
            // var d2 = new Date(dateFrom);
            // var d3 = new Date(dateTo);
            // dateFrom.slice('.', to)
            // console.log(d2)
            // console.log(d3)
           getData();
        }else{alert("incorrect dates");}
    });

    //при закрытии попапа
    $(".close, .shadow").click(function(){
        console.log(docSel);
        var html="";
        var i=0
        for (var key in docSel) {
            i=1;
            var item=docSel[key];
            html+="<div class='oneGroup group_"+key+"'>"
            +"<div data-isn='"+key+"' class='removeOne'></div>"
            +"<p>"+item["NAME"]+"</p></div>";
        }
        console.log(i)
        if(i!=0){
            html+="<p class='addSel'>Добавить</p>"
            $('.main').removeClass('noGroup');
        }else{
            html="<p class='addSel'>Уточнить группы документов</p>"
            $('.main').addClass('noGroup');
        }
        $('.selectArea').html(html);
        bind_selectedArea();
        $('body').addClass('nopopup');
    });

    //расширить/уменшить попап
    $(".fullScreen").click(function(){
        $('.popupWrap').toggleClass('docH-fullsceen');
        // $(".popupWrap").toggleClass('docH');
    });

    //построить календари
    function createCalendars(){
        var inputDateFrom = $('#dateFrom')
        .datepicker({
            // autoClose: true
        })
        .data('datepicker');
        inputDateFrom.selectDate(new Date($('#dateFrom').data("date")));
        
        var inputDateTo = $('#dateTo')
        .datepicker({
            // autoClose: true
        })
        .data('datepicker')
        inputDateTo.selectDate(new Date($('#dateTo').data("date")));
    }

    //Связать элементы в SelectArea
    function bind_selectedArea(){
        //связка добавления группы в SelectArea
        $('.selectArea .addSel').unbind( "click" );
        $('.selectArea .addSel').click(function(){
            $('body').toggleClass('nopopup');
            getDoc(0);
        });

        //связка удаления группы из SelectArea
        $('.selectArea .removeOne').unbind( "click" );
        $('.selectArea .removeOne').click(function(){
            isn=$(this).data('isn');
            docH[isn]['selected']=false;
            delete docSel[isn];
            $(".group_"+isn).remove();
            if($.isEmptyObject(docSel)){
                $('.addSel').text('Уточнить группы документов');
                $('.main').addClass('noGroup');
            }
        });
    };

    //построить левые узлы в попапе групп
    function buildNodeLeft(isn){
        //проверить что узел еще не создан
        var el=$("#doc_"+isn);
        if(el.hasClass("loaded")){
            el.addClass("open");
        }else{//Создать детей
            var html="";
            var children=docH[isn]['childs'];
            for (i = 0; i < children.length; i++){
                var item=docH[children[i]];
                var itemId="doc_"+item["ISN"];
                if(item["ISNODE"]=="True"){
                    tempClass="nodeL_parent";
                    html+="<div class='nodeL closed' id='nodeL_"+children[i]+"'>";
                    html+="<div class='openBox'></div>";
                    html+="<p id='"+itemId+"' class='"+tempClass+"'>"+item["NAME"]+"</p>";
                    html+='<div class="children"></div>';
                }else{
                    tempClass="nodeL_alone";
                    html+="<div class='docH_row_L'>";
                    html+="<p id='"+itemId+"' class='"+tempClass+"'>"+item["NAME"]+"</p>";
                }          
                html+="</div>";
            }
            //console.log(".nodeL_"+isn+" .children");
            $("#nodeL_"+isn+">.children").html(html);
            //Связка 
            el.closest('.nodeL').removeClass('closed');
            el.addClass('loaded');
            el.closest('.nodeL').addClass('loaded');
            $(".openBox").unbind( "click" );
            $('.openBox').bind("click",function(){
                var node =$(this).closest('.nodeL');
                node.toggleClass('closed')
                if(!node.hasClass('loaded')){
                    var isn=node.attr('id').replace('nodeL_','');
                    getDoc(isn);
                }
            });
            $(".nodeL_parent").unbind( "click" );
            $('.nodeL_parent').bind("click",function(){
                var id=$(this).attr('id');
                var isn=id.replace('doc_',"");
                getDoc(isn);
            });
        }
    };

    //построить правый узел в попапе групп
    function buildNodeRight(isn){
        var html="<div>";
        el=docH[isn];
        //Родитель
        if(isn!=0){
            html+="<div class='docH_parent_R' data-id='"+el["ParentIsn"]+"'><p class='parent-right ' >"+el["NAME"]+"</p></div>";
        }
        //Дети
        var children=el['childs'];
        for (i = 0; i < children.length; i++){
            var item=docH[children[i]];
            var itemId="doc_"+item["ISN"];
            var tempClass=(item["ISNODE"]=="True")?"nodeR":"notnodeR";
            html+="<div class='docH_row_R'>";
            if(item["selected"]==true){
                html+="<div class='sel selected' data-isn='"+itemId+"'></div>";
            }else{html+="<div class='sel' data-isn='"+itemId+"'></div>";}
            html+="<p data-isn='"+itemId+"' class='"+tempClass+"'>"+item["NAME"]+"</p></div>";
        }
        html+="</div>";
        $('.docH_right').html(html);
        //Связка родителя
        $('.docH_parent_R').bind("click",function(){
            var isn=$(this).data('id');
            getDoc(isn, true);
        });
        //Связка детей
        $('.nodeR').bind("click",function(){
            var isn=$(this).data('isn').replace('doc_',"");
            getDoc(isn);
        });
        //Связка чекбоксов
        $('.sel').bind("click",function(){
            el=$(this);
            var isn=$(this).data('isn').replace('doc_',"");
            if(el.hasClass('selected')){
                delete docSel[isn];
                docH[isn]["selected"]=false;
                el.removeClass('selected');
            }else{
                docH[isn]["selected"]=true;
                docSel[isn]={};
                docSel[isn]["NAME" ]=docH[isn]["NAME"];
                docSel[isn]["DCODE"]=docH[isn]["DCODE"];
                docSel[isn]["ISNODE"]=docH[isn]["ISNODE"];
                el.addClass('selected');
            }
        });
    };

    //запрос узла группы
    function getDoc(isn, up){
        var html="";     
        //Если узел уже загружен
        if((docH.hasOwnProperty(isn))&&((up==true)||(docH[isn].hasOwnProperty('childs')))){
            // console.log('load from memory')
            if(up!=true){buildNodeLeft(isn)}//встроить Слева
            buildNodeRight(isn)//встроить Справа
        }else{//Если узел еще не загружен
            var url = INFO.deloAdr+"?need="+"lib"+"&isn="+isn+"&type=doc";
            // console.log(url);
            $("#nodeL_"+isn).removeClass('closed');
            $('#nodeL_'+isn+'>.children').append('Загрузка');
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
                    docH[isn]["selected"]=false;
                    docH[isn]["childs"]=childs; 
                    buildNodeLeft(isn) //встроить Слева
                    buildNodeRight(isn)//встроить Справа
                    // console.log(docH);
                },
                error: function (jqXHR, exception) {
                   console.log("Ошибка: "+jqXHR+"; exception: "+exception);
                   console.log(jqXHR);},
            });
        }
    };

    //проверка на форматы даты
    function checkDateFormat(date){
        regexp = /^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$/;
        return (regexp.test(date)) ? true : false
    }

    function makeDotDate(date){
        var year =date.getFullYear();
        var month=["01","02","03","04","05","06","07","08","09","10","11","12"][date.getMonth()];
        var day=date.getDate();
        return day+"."+month+"."+year;
    }

});
