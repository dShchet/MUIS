
var casesNumber //количество дел

GetTitle()
GetCases();

//обработчик скрытия попапа
$(".close, .shadow").click(function(){
    $('body').toggleClass('nopopup');
})

//Собрать заголовок
function GetTitle() {
    $(".result").html("<div class='center'>Загрузка...</div>");
    $.ajax({
        url: INFO.serverAdr + "api/userTitle/"+page.inn.name,
        type: "GET", contentType: "application/json",
        success: function (data) {
            var titleData={};
            var titleData=data[0]['NAME_FULL'];
            $(".main .title").html("<p>"+titleData+"</p>");
         },error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);},
    });
}

//Получить список дел
function GetCases() {
    casesNumber=0;
    $.ajax({
        url: INFO.serverAdr + "api/case/"+page.inn.name+'.'+page.case.raw,
        type: "GET", contentType: "application/json",
        success: function (dataset) {
            var caseList="";
            if(dataset.leght==1){}
            $.each(dataset, function (index, data) {
                casesNumber+=1;
                caseList+=makeOneCase(data);
            });
            if(casesNumber==0){$('.fields').html("<div class='center'>Ничего не найдено</div>");
            }else if(casesNumber==1){$('.fields').addClass('oneCase'); $('.fields').html(caseList);
            }else{$('.fields').html(caseList);bindCases();}
            bindInfoBtns()
         },error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);},
    });
}

//Сформировать одно дело
function makeOneCase(data) {
    var text="";
    text='<div class="case">'+
    '<div class="caseShort" data-id="'+data.ID+'">'+
    '<div class="caseDate-short">'+fixDate(data.REG_DATE, "num")+'</div>'+
    '<table class="table">'+
        '<tr>'+
            '<td><p>Истец</p> <b><button class="infoBtn" data-inn="'+data.INN_CLAIMANT+'" >Info</button>ИНН: '+data.INN_CLAIMANT+'</b></td>'+
            '<td><p>Ответчик</p> <b><button class="infoBtn" data-inn="'+data.INN_DEFENDANT+'">Info</button>ИНН: '+data.INN_DEFENDANT+'</b></td>'+
        '</tr>'+
        '<tr>'+
            '<td>'+data.NAME_SHORT_CLAIMANT+'</td>'+
            '<td>'+data.NAME_SHORT_DEFENDANT+'</td>'+
        '</tr>'+
    '</table>'+
    '</div>'+
    '<div class="caseDetails closed" data-id="ID">';
        if(data.LEASE_CONTRACT_N){
            text+=""+
            '<div class="">'+
                '<p>Договор аренды №: </p>'+'<p>'+data.LEASE_CONTRACT_N+'</p>'+
            '</div>'
        }
        if(data.LEASE_CONTRACT_DATE){text+='<div><p>Дата договора аренды: </p><p>'+fixDate(data.LEASE_CONTRACT_DATE, "num")+'</p></div>'}
        if(data.TRANSFER_REG_N){text+='<div><p>Переход дела: </p><p>'+data.TRANSFER_REG_N+'</p></div>'}
        if(data.TRANSFER_REG_DATE){text+='<div><p>Дата перехода дела: </p><p>'+fixDate(data.TRANSFER_REG_DATE, "num")+'</p></div>'}
        if(data.TRANSFER_ID){text+='<div><p>ID перехода дела: </p><p>'+data.TRANSFER_ID+'</p></div>'}
        if(data.CASE_NUMBER){text+='<div><p>№ дела в суде: </p><p>'+data.CASE_NUMBER+'</p></div>'}
        if(data.CASE_MATERIALS){text+='<div><p>Материалы дела: </p><p>'+data.CASE_MATERIALS+'</p></div>'}
        if(data.JUDGE){text+='<div><p>Судья: </p><p>'+data.JUDGE+'</p></div>'}
        if(data.COURTROOM){text+='<div><p>Зал: </p><p>'+data.COURTROOM+'</p></div>'}
        if(data.COURT_NAME){text+='<div><p>Позиция: </p><p>'+data.COURT_NAME+'</p></div>'}
        if(data.COURT_NAME){text+='<div><p>Наименование суда: </p><p>'+data.COURT_POSITION+'</p></div>'}
        if(data.COURT_REPRES){text+='<div><p>Представитель: </p><p>'+data.COURT_REPRES+'</p></div>'}
        if(data.COURT_DATE){
            if(page.case.raw=="viewed"){
                text+='<div><p>Дата подачи в суд: </p><p>'+fixDate(data.COURT_DATE, "num")+'</p></div>'
            }else{
                text+='<div><p>Дата суда: </p><p>'+fixDate(data.COURT_DATE, "weekday")+'</p></div>'
            }
        }
        if(data.CLAIM){text+='<div><p>Исковые требования: </p><p>'+data.CLAIM+'</p></div>'}
        if(data.COURT_RECOVERY_NUMBER){text+='<div><p>№ решения суда о взыскании: </p><p>'+data.COURT_RECOVERY_NUMBER+'</p></div>'}
        if(data.COURT_RECOVERY_DATE){text+='<div><p>Дата решения суда о взыскании: </p><p>'+data.COURT_RECOVERY_DATE+'</p></div>'}
        if(data.POST_N ){text+='<div><p>Номер почтового идентификатора: </p><p>'+data.POST_N +'</p></div>'}
        if(data.POST_DATE){
            if(page.case.raw=="roomRental"){
                text+='<div><p>Дата почтового идентификатора: </p><p>'+fixDate(data.POST_DATE, "num")+'</p></div>'
            }else{
                text+='<div><p>Дата направления в ССП: </p><p>'+fixDate(data.POST_DATE, "num")+'</p></div>'
            }
        }
        if(data.SER_NO_IL){text+='<div><p>Серия и номер исполнительного листа: </p><p>'+data.SER_NO_IL+'</p></div>'}
        if(data.SSP_NAME ){text+='<div><p>Наименование отдела ССП: </p><p>'+data.SSP_NAME +'</p></div>'}
        if(data.SSP_FIO){text+='<div><p>ФИО судебного пристава - исполнителя: </p><p>'+data.SSP_FIO+'</p></div>'}
        if(data.SSP_FIO_CONTACT){text+='<div><p>Контакты судебного пристава: </p><p>'+data.SSP_FIO_CONTACT+'</p></div>'}
        if(data.ISP_NOMER){text+='<div><p>Номер исполнительного производства: </p><p>'+data.ISP_NOMER+'</p></div>'}
        if(data.ISP_DATE ){text+='<div><p>Дата исполнительного производства: </p><p>'+fixDate(data.ISP_DATE, "num") +'</p></div>'}
        if(data.CLAIM_RECOVERED_COURT){text+='<div><p>Заявленные сумма иска: </p><p>'+data.CLAIM_RECOVERED_COURT+' ₽</p></div>'}
        if(data.INTEREST_RECOVERED_COURT ){text+='<div><p>Заявленные сумма пени: </p><p>'+data.INTEREST_RECOVERED_COURT +' ₽</p></div>'}
        if(data.SUMMA_RECOVERED_COURT){text+='<div><p>Общая сумма:: </p><p>'+data.SUMMA_RECOVERED_COURT+'</p></div>'}
        if(data.COLLECT_PERIOD_BEG ){text+='<div><p>Период взыскания с </p><p>'+data.COLLECT_PERIOD_BEG +'</p></div>'}
        if(data.COLLECT_PERIOD_END ){text+='<div><p>Период взыскания по </p><p>'+data.COLLECT_PERIOD_END +'</p></div>'}
        if(data.TERMINATION_LEASE){text+='<div><p>Флаг расторжения договора: </p><p>'+data.TERMINATION_LEASE+'</p></div>'}
        if(data.KIZO_DATE_OUT){text+='<div><p>Дата, исх.№ КИЗО- материал для подготовки иск. заявления: </p><p>'+fixDate(data.KIZO_DATE_OUT, "num")+'</p></div>'}
        if(data.KIZO_NUMBER_OUT){text+='<div><p>Исх.№ КИЗО- материал для подготовки иск. заявления: </p><p>'+data.KIZO_NUMBER_OUT+'</p></div>'}
        if(data.BUSINESS_MOVEMENT){text+='<div><p>Движение дела: </p><p>'+data.BUSINESS_MOVEMENT+'</p></div>'}
        if(data.AMOUNT_OF_INTEREST ){text+='<div><p>Сумма пени: </p><p>'+data.AMOUNT_OF_INTEREST +' ₽</p></div>'}
        if(data.COURT_RESULT ){
            if(data.COURT_RESULT==1){
                text+='<div><p>Статус дела: закончено</p></div>';
            }else{
                text+='<div><p>Статус дела: не закончено</p></div>';
            }
        }
        if(data.COURT_RESULT_DATE){text+='<div><p>дата окончания дела: </p><p>'+fixDate(data.COURT_RESULT_DATE, "num")+'</p></div>'}
        if(data.COURT_RESULT_TEXT){text+='<div><p>Результат дела, текст: </p><p>'+data.COURT_RESULT_TEXT+'</p></div>'}
        if(data.RULING ){text+='<div><p>Постановление в адм./отд.: </p><p>'+data.RULING +'</p></div>'}
        if(data.AMOUNT_OF_CLAIM){
            if((page.case.raw=="roomRental")||(page.case.raw=="landLease")){
                text+='<div><p>Сумма задолжности: </p><p>'+data.AMOUNT_OF_CLAIM+' ₽</p></div>'
            }else{
                text+='<div><p>Сумма иска: </p><p>'+data.AMOUNT_OF_CLAIM+' ₽</p></div>'
            }
        }
        if(data.ISP_RESULT ){
            if(data.ISP_RESULT=="1"){
                text+='<div><p>Результат исполнительного производства: выполнено</p></div>';
            }else{
                text+='<div><p>Результат исполнительного производства: в производстве</p></div>';
            }
        }
        if(data.ISP_RESULT_DATE){text+='<div><p>Результат исполнительного производства - дата: </p><p>'+fixDate(data.ISP_RESULT_DATE, "num")+'</p></div>'}
        if(data.ISP_RESULT_TEXT){text+='<div><p>Результат исполнительного производства - текст: </p><p>'+data.ISP_RESULT_TEXT+'</p></div>'}
        if(data.RENTAL_TYP ){
            if(data.RENTAL_TYP=="1"){
                text+='<div><p>Тип аренды: аренда помещения</p></div>';
            }else{
                text+='<div><p>Тип аренды: аренда земли</p></div>';
            }
        }
        if(data.RENTAL_ADR ){text+='<div><p>Адрес объекта аренды: </p><p>'+data.RENTAL_ADR +'</p></div>'}
        if(data.RENTAL_DESCR ){text+='<div><p>Описание объекта аренды: </p><p>'+data.RENTAL_DESCR +'</p></div>'}
        if(data.SPECIALIST ){text+='<div><p>Специалист: </p><p>'+data.SPECIALIST +'</p></div>'}
        if(data.PRIM){
            text+=""+
            '<div class="prim">'+
                '<div class="prim-content">'+data.PRIM+'</div>'+
            '</div>'
        }
        text+=""+
        '</div>'+
    '</div>';
    return text;
}

//обработчик открытия подробностей дела
function bindCases(){
    $('.caseShort').click(function(){
        var sel = getSelection().toString();
        if(!sel){
            var caseDetails=$($(this).next());
            if(caseDetails.hasClass('closed')){
                caseDetails.clearQueue();
                caseDetails.stop();
                caseDetails.toggleClass('closed');
                var height=$(caseDetails).get(0).scrollHeight;
                caseDetails.css({ 'height': 0 + "px" });
                caseDetails.animate({height: height}, 500 );
            }else{
                caseDetails.clearQueue();
                caseDetails.stop();
                caseDetails.toggleClass('closed');
                caseDetails.animate({height: 0}, 500 );
            }
        }
    })
}

//при нажатии на (I)
function bindInfoBtns(){
    $('.infoBtn').bind('click', function(e) {
        var inn= $(this).data("inn");
        $('body').toggleClass('nopopup');
        $('.popFields').html("<div class='center'>Загрузка...</div>");
        $.ajax({
            url: INFO.serverAdr + "api/user/"+inn,
            type: "GET", contentType: "application/json",
            success: function (dataset) {
                var data=dataset[0];
                if(dataset.length!=0){
                    var text="";
                    if(data.NAME_FULL){text+="<div><p style='font-size: 19px;'>"+data.NAME_FULL+"</p></div>"}
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
                    $('.popFields').html(text);
                }else{$('.popFields').html("<div class='center'>Ничего не найдено</div>");}
            },
            error: function (jqXHR, exception) {
                console.log("Ошибка: "+jqXHR+"; exception: "+exception);}
        });
        e.stopPropagation();
        e.preventDefault();
    })
}



