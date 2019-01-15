
var infoData={};//данные подробностей организаций
var amount=0;//количество записей

// $("#toSearch").click(function(e){
//     var need, source, dateFrom, dateTo;
//     e.preventDefault();
//     $("#need").val() ? need = $("#need").val() :need  ="search";
//     $("#source").val() ? source = $("#source").val() :source  ="search";
//     $("#dateFrom").val() ? dateFrom = $("#dateFrom").val() :dateFrom  ="search";
//     $("#dateTo").val() ? dateTo = $("#dateTo").val() :dateTo  ="search";
    
//     toFilter(need, source, dateFrom, dateTo);
// })
page.pathCLean= page.path.substring(page.path.indexOf(":") + 1).replace(/\//g, "");
    var isn= page.pathCLean.split('&type:')[0];
    var rcType=page.pathCLean.split('&type:')[1]
    console.log(isn);
    console.log(rcType);
deloOneGet(isn, rcType);

function deloOneGet(isn, rcType){
    //console.log(isn);
    data1=[{"ISN":"4460","DOCGROUP":{"ISN":"3686","NAME":"Приказы по основной деятельности"},"REGNUM":"1","ORDERNUM":"1","SPECIMEN":"1","DOCDATE":"27.04.2012 0:00:00","CONSIST":"2","CONTENTS":"Об утверждении списка сотрудников, имеющих допуск по грифу &quot;Коммерческая тайна&quot;","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"False","SECURITY":{"ISN":"6","NAME":"для служебного пользования"},"NOTE":"","ADDRESS_FLAG":"3","ISCONTROL":"","PLANDATE":"","FACTDATE":"","CARDCNT":"7","CARD":[{"DATE":"27.04.2012 14:24:02"},{"DATE":"27.04.2012 14:24:02"},{"DATE":"04.05.2012 10:49:43"},{"DATE":"04.05.2012 10:49:43"},{"DATE":"04.05.2012 10:49:43"},{"DATE":"04.05.2012 10:49:43"},{"DATE":"04.05.2012 10:49:43"}],"LINKCNT":"0","RUBRICCNT":"0","ADDPROPSRUBRICCNT":"0","ADDRCNT":"7","ADDR":[{"ISN":"4469","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"True","ORDERNUM":"1","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3612","NAME":"Руководство","EMAIL":""}},{"ISN":"4470","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"False","ORDERNUM":"2","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3630","NAME":"Управление делами","EMAIL":""}},{"ISN":"4471","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"False","ORDERNUM":"3","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3614","NAME":"Управление по основной деятельности","EMAIL":""}},{"ISN":"4472","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"False","ORDERNUM":"4","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3616","NAME":"Планово-экономическое управление","EMAIL":""}},{"ISN":"4473","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"False","ORDERNUM":"5","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3618","NAME":"Юридический отдел","EMAIL":""}},{"ISN":"4474","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"False","ORDERNUM":"6","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3620","NAME":"Отдел кадров","EMAIL":""}},{"ISN":"4475","DATE_UPD":"","SENDDATE":"04.05.2012 10:49:00","ORIGFLAG":"False","ORDERNUM":"7","DATE_CR":"","PERSON":"","KINDADDR":"DEPARTMENT","DEPARTMENT":{"ISN":"3622","NAME":"Архив","EMAIL":""}}],"FILESCNT":"0","JOURNACQCNT":"0","PROTCNT":"0","ALLRESOL":"True","RESOLCNT":"0","JOURNALCNT":"7","JOURNAL":[{"ISN":"4504","ADDRESSEE":{"ISN":"3612","NAME":"Руководство"},"ORIGFLAG":"True","SENDDATE":"04.05.2012 10:49:00"},{"ISN":"4505","ADDRESSEE":{"ISN":"3630","NAME":"Управление делами"},"ORIGFLAG":"False","SENDDATE":"04.05.2012 10:49:00"},{"ISN":"4506","ADDRESSEE":{"ISN":"3614","NAME":"Управление по основной деятельности"},"ORIGFLAG":"False","SENDDATE":"04.05.2012 10:49:00"},{"ISN":"4507","ADDRESSEE":{"ISN":"3616","NAME":"Планово-экономическое управление"},"ORIGFLAG":"False","SENDDATE":"04.05.2012 10:49:00"},{"ISN":"4508","ADDRESSEE":{"ISN":"3618","NAME":"Юридический отдел"},"ORIGFLAG":"False","SENDDATE":"04.05.2012 10:49:00"},{"ISN":"4509","ADDRESSEE":{"ISN":"3620","NAME":"Отдел кадров"},"ORIGFLAG":"False","SENDDATE":"04.05.2012 10:49:00"},{"ISN":"4510","ADDRESSEE":{"ISN":"3622","NAME":"Архив"},"ORIGFLAG":"False","SENDDATE":"04.05.2012 10:49:00"}],"FORWARDCNT":"0","ADDPROPSCNT":"0","USER_CR":"System.__ComObject","DATE_CR":"06.12.2018 10:24:01","RUBRIC_first":"System.__ComObject","CARD_first":"System.__ComObject","ADDR_first":"System.__ComObject","JOURNAL_first":"System.__ComObject","JOURNACQ_first":"System.__ComObject","ADDPROPS_first":"System.__ComObject","ADDPROPSRUBRIC_first":"System.__ComObject","PROTOCOL_first":"System.__ComObject","FORWARD_first":"System.__ComObject","ERRCODE":"0","PERSONSIGNSCNT":"1","PERSONSIGNS":[{"WHO_SIGN":{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},"ORDERNUM":"1"}],"PERSONSIGN":"System.__ComObject","EXECUTOR":{"ISN":"3662","NAME":"Ломакин Р.А. - Нач. отдела кадров","POST":"Нач. отдела кадров"},"COEXECCNT":"0","VISACNT":"3","VISA":[{"ISN":"4466","EMPLOY":{"ISN":"3658","NAME":"Адвокатов П.Б. - Нач. отдела"},"DATE":"26.04.2012 0:00:00"},{"ISN":"4467","EMPLOY":{"ISN":"3628","NAME":"Усманов С.У. - Зам. ген. директора"},"DATE":"26.04.2012 0:00:00"},{"ISN":"4468","EMPLOY":{"ISN":"3626","NAME":"Гончаров А.О. - Зам. ген. директора"},"DATE":"26.04.2012 0:00:00"}],"PERSONSIGNS_first":"System.__ComObject","VISA_first":"System.__ComObject","COEXEC_first":"System.__ComObject"}];
    $.ajax({
        url: INFO.deloAdr+"?need="+"one"+"&isn="+isn+"&rcType="+rcType,
        type: "GET", contentType: "application/json",
        success: function (data2) {
            console.log(data2);
            data=data2[0];
            var temp={};
            function header(){
                var header ="";
                header='<div class="result-row" data-rowid="' + data.ISN + '">'+
                '<div class="result-data"><b>'+data.REGNUM+' '+data.DOCGROUP.NAME+' '+data.DOCDATE.split(' ')[0]+'</b>'+
                    '<p>Содерж.: ' + data.CONTENTS + '</p>'+'<p>от '+data.DOCDATE.split(' ')[0]+'</p>'+
                    '<p>Состав '+data.CONSIST+' Доступ'+data.SECURITY.NAME+' Экз №:'+data.ORDERNUM+'</p>'+
                '</div>';
                return header
            };
            var footer='</div>';
            function corresp(){
                var corresp="";
                if(data.CORRESP){
                    corresp='<div class="case">'+
                    '<div class="caseShort"><b>КОРРЕСПОНДЕНТЫ ('+data.CORRESPCNT+')</b></div>';
                        '<div class="caseDetails">';
                        if(data.ADDRESSESCNT>0){row+=
                        '<p>1</p>';
                        }corresp+=
                        '<p data-isn="'+data.CORRESP[0].ISN+'"> '+data.CORRESP[0].ORGANIZ+'</p>'+
                        '<p>Исх. №: '+data.CORRESP[0].OUTNUM+'</p>'+
                        '<p>Дата: '+data.CORRESP[0].OUTDATE+'</p>'+
                        '<p>Подписал: '+data.CORRESP[0].SIGN+'</p>'+
                    '</div>'
                    '</div>';
                }
                return corresp
            }
            function visu(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>ВИЗЫ ('+data.VISACNT+')</b></div>';
                if(data.VISA){
                    html+='<div class="caseDetails">';
                    if(data.VISACNT>0){
                        for (let i = 0; i < data.VISACNT; i++) {
                            var el = data.VISA[i];
                            var date=el.DATE.split(' ')[0];
                            var time=el.DATE.split(' ')[1];if(time!="0:00:00"){time=' в: '+time}else{time=""}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<div data-isn="'+el.EMPLOY.ISN+'">'+el.EMPLOY.NAME+' Дата: '+date+time+'</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function addr(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>АДРЕСАТЫ ('+data.ADDRCNT+')</b></div>';
                if(data.ADDR){
                    html+='<div class="caseDetails">';
                    if(data.ADDRCNT>0){
                        for (let i = 0; i < data.ADDRCNT; i++) {
                            var el = data.ADDR[i];
                            var ORIGFLAG;
                            var date=el.SENDDATE.split(' ')[0];
                            var time=el.SENDDATE.split(' ')[1];
                            var typeStruct=el["KINDADDR"];
                            if(el.ORIGFLAG=="True"){ORIGFLAG="оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="копия";}else{ORIGFLAG=underfined;}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<div>'+el[typeStruct].NAME+'</div>';
                            html+='<div>Дата: '+date+' в: '+time+' '+ORIGFLAG+'</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function journal(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>ЖУРНАЛ ПЕРЕДАЧИ ДОКУМЕНТА ('+data.JOURNALCNT+')</b></div>';
                if(data.JOURNAL){
                    html+='<div class="caseDetails">';
                    if(data.JOURNALCNT>0){
                        for (let i = 0; i < data.JOURNALCNT; i++) {
                            var el = data.JOURNAL[i];
                            var ORIGFLAG;
                            if(el.ORIGFLAG=="True"){ORIGFLAG="оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="копия";}else{ORIGFLAG=underfined;}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<div> '+el.SENDDATE+' '+el.ADDRESSEE.NAME+' '+ORIGFLAG+'</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function signs(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>ПОДПИСАЛИ ('+data.PERSONSIGNSCNT+')</b></div>';
                if(data.PERSONSIGNS){
                    html+='<div class="caseDetails">';
                    if(data.PERSONSIGNSCNT>0){
                        for (let i = 0; i < data.PERSONSIGNSCNT; i++) {
                            var el = data.PERSONSIGNS[i];
                            html+='<div class="caseDetR" data-isn="'+el.WHO_SIGN.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<div> '+el.WHO_SIGN.NAME+'</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function executor(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>ИСПОЛНИТЕЛИ ('+1+')</b></div>';
                //if(data.PERSONSIGNS){
                    html+='<div class="caseDetails">';
                    //if(data.PERSONSIGNSCNT>0){
                        //for (let i = 0; i < data.PERSONSIGNSCNT; i++) {
                            i=0;
                            var el = data.EXECUTOR;
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<div> '+el.NAME+'</div>';
                            html+='</div>';
                        //}
                    //}
                    html+='</div>';
                //}
                html+='</div>';
                return html;
            }
            var row=header()+
            signs()+
            executor()+
            visu()+
            corresp()+
            addr()+
            journal()+
            footer;
            $(".result").html(row);            
            $(".caseShort").click(function(){$(this).toggleClass('caseShort-closed')});
         },
         error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);
            console.log(jqXHR);},
    });
}



