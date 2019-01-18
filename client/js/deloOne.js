
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
deloOneGet(isn, rcType);
function deloOneGet(isn, rcType){
    data1=[{"ISN":"4694","DOCGROUP":{"ISN":"3678","NAME":"Обращения граждан"},"REGNUM":"Ан-1","SPECIMEN":"1,2","DOCDATE":"04.06.2012 ","CONSIST":"3 листа","CONTENTS":"Проверка регистрации обращения гражданина.","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"True","SECURITY":{"ISN":"16","NAME":"коммерческая тайна"},"NOTE":"примечание","ADDRESS_FLAG":"2","ISCONTROL":"2","PLANDATE":"04.07.2012 ","FACTDATE":"03.07.2012 ","DELTA":"-1","LINKCNT":"1","LINKREF":[{"ISN":"4999","TYPELINK":"Ответ на запрос","ORDERNUM":"1","LINKINFO":"сам док_пдф","URL":"192.168.299.299"}],"RUBRICCNT":"2","RUBRIC":[{"ISN":"4056964","NAME":"Производство товаров, качество продукции","INDEX":"1.1.1"},{"ISN":"4056956","NAME":"Вопросы экологии и землепользования","INDEX":"1.3"}],"ADDPROPSRUBRICCNT":"0","ADDRCNT":"3","ADDR":[{"ISN":"4698","REG_DATE":"24.01.2019 ","CONSIST":"Большой","SENDDATE":"02.01.2019 16:12:00","ORDERNUM":"1","PERSON":"всем","KINDADDR":"ORGANIZ","NOTE":"примеравссч","REG_N":"852","DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"ORGANIZ":{"ISN":"4220","POSTINDEX":"","CITY":"Москва","NAME":"Сбербанк России","ADDRESS":"ул. Балчуг, 2","EMAIL":""}},{"ISN":"4700","REG_DATE":"","CONSIST":"сотав","SENDDATE":"04.01.2019 16:13:00","ORDERNUM":"3","PERSON":"","KINDADDR":"CITIZEN","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"3776","NAME":"Заказная почта"},"CITIZEN":{"ISN":"3939","NAME":"Нестерова Е.П.","ADDRESS":"ул. Ленина, д.5. кв. 56","REGION":"Московская область","POSTINDEX":"143960","CITY":"Реутов","EMAIL":""}},{"ISN":"4701","REG_DATE":"","CONSIST":"","SENDDATE":"02.01.2019 16:13:00","ORDERNUM":"4","PERSON":"","KINDADDR":"DEPARTMENT","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"DEPARTMENT":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1","EMAIL":""}}],"FILESCNT":"2","FILES":[{"ISN":"4704","NAME":"4694-4704.docx","DESCRIPT":"somefile.docx","CONTENTS":"System.__ComObject","EDSCNT":"0","EDS":"System.__ComObject"},{"ISN":"4707","NAME":"4694-4707.xlsx","DESCRIPT":"other somefile.xlsx","CONTENTS":"System.__ComObject","EDSCNT":"0","EDS":"System.__ComObject"}],"JOURNACQCNT":"0","PROTCNT":"10","PROTOCOL":[{"WHEN":"14.12.2018 11:21:56","WHAT":"Регистрация РК"},{"WHEN":"18.01.2019 16:08:20","WHAT":"Уд.<Кому>"},{"WHEN":"18.01.2019 16:10:46","WHAT":"Ред. контрольности РК"},{"WHEN":"18.01.2019 16:13:47","WHAT":"Уд.адресата"},{"WHEN":"18.01.2019 16:13:47","WHAT":"Уд.адресата"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Ред. грифа доступа"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение Рег.№"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Ред. основного раздела РК"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение флага &quot;Срочно&quot;"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение флага &quot;РК персон. доступа&quot;"}],"ALLRESOL":"True","RESOLCNT":"2","RESOLUTION":[{"ISN":"85","KIND":"2","ITEMNUM":"88","AUTHOR_ISN":"","AUTHOR_NAME":"","SURNAME":"","TEXT":"сам текст","RESOLDATE":"","SENDDATE":"18.01.2019 16:12:04","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"31.01.2019 ","FACTDATE":"26.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"3","REPLY":[{"ISN":"86","EXECUTOR_ISN":"3626","EXECUTOR_NAME":"Гончаров А.О. - Зам. ген. директора"},{"ISN":"87","EXECUTOR_ISN":"3628","EXECUTOR_NAME":"Усманов С.У. - Зам. ген. директора"},{"ISN":"88","EXECUTOR_ISN":"4057914","EXECUTOR_NAME":"Наша организация"}],"RESOLCNT":"0"},{"ISN":"82","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3624","AUTHOR_NAME":"Захаров П.Ф. - Генеральный директор","SURNAME":"Захаров П.Ф.","TEXT":"сам текст","RESOLDATE":"18.01.2019 ","SENDDATE":"18.01.2019 16:10:44","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"True","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"26.01.2019 ","FACTDATE":"20.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"83","EXECUTOR_ISN":"3656","EXECUTOR_NAME":"Юренев А.Т. - Зам. нач. управления"},{"ISN":"84","EXECUTOR_ISN":"3654","EXECUTOR_NAME":"Толкачев О.Е. - Нач. управления"}],"RESOLCNT":"0"}],"JOURNALCNT":"5","JOURNAL":[{"ISN":"4996","ADDRESSEE_ISN":"3646","ADDRESSEE_NAME":"Фалеев А.Д. - Нач. отдела № 1","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"02.01.2019 16:13:00"},{"ISN":"4991","ADDRESSEE_ISN":"3656","ADDRESSEE_NAME":"Юренев А.Т. - Зам. нач. управления","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 16:10:00"},{"ISN":"4992","ADDRESSEE_ISN":"3654","ADDRESSEE_NAME":"Толкачев О.Е. - Нач. управления","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"18.01.2019 16:10:00"},{"ISN":"4994","ADDRESSEE_ISN":"3626","ADDRESSEE_NAME":"Гончаров А.О. - Зам. ген. директора","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 16:12:00"},{"ISN":"4995","ADDRESSEE_ISN":"3628","ADDRESSEE_NAME":"Усманов С.У. - Зам. ген. директора","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"18.01.2019 16:12:00"}],"FORWARDCNT":"2","FORWARD":[{"ISN":"4997","ADR_ISN":"3642","ADR_NAME":"Отдел № 1","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"18.01.2019 16:14:05"},{"ISN":"4998","ADR_ISN":"3626","ADR_NAME":"Гончаров А.О. - Зам. ген. директора","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"18.01.2019 16:14:16"}],"ADDPROPSCNT":"0","DOCKIND":"RCLET","ADDRESSESCNT":"2","ADDRESSES":[{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},{"ISN":"3640","NAME":"Экерман Р.Ю. - Зам. начальника управления"}],"DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"TELEGRAM":"","CORRESPCNT":"1","CORRESP":[{"ISN":"4232","KIND":"2","ORGANIZ":"Министерство экономического развития РФ (Минэкономразвития РФ)","OUTNUM":"156","OUTDATE":"10.01.2019 ","SIGN":"сам","CONTENTS":"содерж","NOTE":"прим"}],"ISCOLLECTIVE":"True","ISANONIM":"True","AUTHORCNT":"5","AUTHOR":[{"ISN":"4695","CITIZEN_NAME":"Тихорин В.И.","CITIZEN_ADDRESS":"ул Мойка, д. 4, кв 56","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Санкт-Петербург","ORDERNUM":"1","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"4702","CITIZEN_NAME":"Денисов Г.Р.","CITIZEN_ADDRESS":"ул. Перовская , д. 345, кв. 15","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"2","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"4703","CITIZEN_NAME":"Эльдина Е.Д.","CITIZEN_ADDRESS":"ул. Таганская, д. 45, кв. 89","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"3","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"5000","CITIZEN_NAME":"Ананьева И.В.","CITIZEN_ADDRESS":"ул. Радищева Д. 21, кв 15","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Рязань","ORDERNUM":"4","DATE_CR":"18.01.2019 16:35:22","DATE_UPD":"18.01.2019 16:35:22"},{"ISN":"5001","CITIZEN_NAME":"Родин Н.В.","CITIZEN_ADDRESS":"ул. Советская. д.112, кв. 87","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"5","DATE_CR":"18.01.2019 16:35:22","DATE_UPD":"18.01.2019 16:35:22"}]}];

    // $.ajax({
    //     url: INFO.deloAdr+"?need="+"one"+"&isn="+isn+"&rcType="+rcType,
    //     type: "GET", contentType: "application/json",
    //     success: function (data2) {
              data=data1[0];
              //data=data2[0];
            function header(){
                var html=SECURITY=DELIVERY="";
                var REGNUM=data.REGNUM ? data.REGNUM+' ':"";
                if(data.SECURITY){SECURITY=data.SECURITY.NAME ? '<p>Доступ: '+data.SECURITY.NAME+'</p>':"";}
                var SPECIMEN=data.SPECIMEN ? '<p>Экз №: '+data.SPECIMEN+'</p>':"";
                var CONSIST=data.CONSIST ? '<p>Состав: '+data.CONSIST+'</p>':"";
                var DOCGROUP=data.DOCGROUP.NAME ? '<p>'+data.DOCGROUP.NAME+'</p>':"";
                var DOCDATE=data.DOCDATE ? '<p>от '+data.DOCDATE.split(' ')[0]+'</p>':"";
                var CONTENTS=data.CONTENTS ? '<p>Содерж.: '+data.CONTENTS+'</p>':"";
                var PLANDATE=data.PLANDATE ? '<p>План.: '+data.PLANDATE+'</p>':"";
                var FACTDATE=data.FACTDATE ? '<p>Факт.: '+data.FACTDATE+'</p>':"";
                if(data.DELIVERY) { DELIVERY=data.DELIVERY ?'<p>Доставка: '+data.DELIVERY.NAME+'</p>':"";}
                var ISCOLLECTIVE=(data.ISCOLLECTIVE==="True") ? '<p>Коллективное':"";
                var NOTE=data.NOTE ? '<p>Примечание: '+data.NOTE+'</p>':"";
                html+='<div data-isn="' + data.ISN + '">';
                html+='<div>'+REGNUM+DOCGROUP+'</div>';
                html+='<div>'+DOCDATE+SPECIMEN+SECURITY+CONSIST+DELIVERY+'</div>';
                html+='<div>'+PLANDATE+FACTDATE+ISCOLLECTIVE+'</div>';
                html+='<div>'+CONTENTS+'</div>';
                html+='<div>'+NOTE+'</div>';
                return html;
            };
            function FILES(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ФАЙЛЫ ('+data.FILESCNT+')</b></div>';
                if(data.FILES){
                    html+='<div class="caseDetails">';
                    if(data.FILESCNT>0){
                        for (let i = 0; i < data.FILESCNT; i++) {
                            var el = data.FILES[i];
                            html+='<div class="caseItem">';
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'" data-name="'+el.NAME+'">';
                                html+='<div>'+el.DESCRIPT+'</div>';
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function SIGNS(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ПОДПИСАЛИ ('+data.PERSONSIGNSCNT+')</b></div>';
                if(data.PERSONSIGNS){
                    html+='<div class="caseDetails">';
                    if(data.PERSONSIGNSCNT>0){
                        for (let i = 0; i < data.PERSONSIGNSCNT; i++) {
                            var el=data.PERSONSIGNS[i];
                            html+='<div class="caseItem">';
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.WHO_SIGN_ISN+'">';
                            html+='<div> '+el.WHO_SIGN_NAME+'</div>';
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function EXECUTOR(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ИСПОЛНИТЕЛИ ('+1+')</b></div>';
                //if(data.PERSONSIGNS){
                html+='<div class="caseItem">';
                    html+='<div class="caseDetails">';
                    //if(data.PERSONSIGNSCNT>0){
                        //for (let i = 0; i < data.PERSONSIGNSCNT; i++) {
                            var i=0;
                            if(data.EXECUTOR){
                                var el = data.EXECUTOR;
                                html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                                html+='<div class="itemNumber">'+(i+1)+'</div>';
                                html+='<div> '+el.NAME+'</div>';
                                html+='</div>';
                            }
                        //}
                    //}
                    html+='</div>';
                    html+='</div>';
                //}
                html+='</div>';
                return html;
            }
            function VISA(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ВИЗЫ ('+data.VISACNT+')</b></div>';
                if(data.VISA){
                    html+='<div class="caseDetails">';
                    if(data.VISACNT>0){
                        for (let i = 0; i < data.VISACNT; i++) {
                            var el = data.VISA[i];
                            var date=el.DATE.split(' ')[0];
                            var time=el.DATE.split(' ')[1];time=(time=="")?"":' в: '+time;
                            html+='<div class="caseItem">';
                            html+='<div class="itemNumber">'+(i+1)+' </div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<div data-isn="'+el.EMPLOY_ISN+'">'+el.EMPLOY_NAME+' Дата: '+date+time+'</div>';
                            html+='<div>Прим.:'+el.NOTE+'</div>';
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function AUTHOR(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>АВТОРЫ ('+data.AUTHORCNT+')</b></div>';
                if(data.AUTHOR){
                    html+='<div class="caseDetails">';
                    if(data.AUTHORCNT>0){
                        for (let i = 0; i < data.AUTHORCNT; i++) {
                            html+='<div class="caseItem">';
                            var NAME="";
                            var el = data.AUTHOR[i];
                            NAME=el.CITIZEN_NAME ? el.CITIZEN_NAME+" ":"";
                            var DATE_UPD=el.DATE_UPD ? '<p>Дата: '+el.DATE_UPD.split(' ')[0]+'</p>':"";
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p data-isn="'+el.ISN+'"> '+NAME+DATE_UPD+'</p>'
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function CORRESP(){
                data.CORRESP=data.CORRESP?data.CORRESP:0;
                data.CORRESPCNT=data.CORRESPCNT?data.CORRESPCNT:0;
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>КОРРЕСПОНДЕНТЫ ('+data.CORRESPCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.CORRESPCNT>0)&&(data.CORRESP)){
                    for (let i = 0; i < data.CORRESPCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.CORRESP[i];
                        var ORGANIZ=el.ORGANIZ ? el.ORGANIZ+" ":"";
                        var OUTNUM=el.OUTNUM ? 'Исх. №: '+el.OUTNUM+' ':"";
                        var OUTDATE=el.OUTDATE ? 'Дата: '+el.OUTDATE+' ':"";
                        var SIGN=el.SIGN ? 'Подписал: '+el.SIGN+' ':"";
                        var NOTE=el.NOTE ? '<div>Прим.: '+el.NOTE+'</div>':"";
                        html+='<div class="itemNumber">'+(i+1)+' </div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<p data-isn="'+el.ISN+'"> '+ORGANIZ+OUTNUM+OUTDATE+SIGN+'</p>';
                        html+=NOTE;
                        html+='</div>';
                        html+='</div>';
                    }
                }
                html+='</div>';
                html+='</div>';
                return html;
            }
            function komy(){
                var html="";
                if((data.ADDRESSES)||(data.ADDRESSESCNT)){
                    html+='<div class="case">'+
                    '<div class="caseShort closed"><b>КОМУ ('+data.ADDRESSESCNT+')</b></div>';
                        html+='<div class="caseDetails">';
                            for (let i = 0; i < data.ADDRESSESCNT; i++){
                                html+='<div class="caseItem">';
                                html+='<div><div class="itemNumber">'+(i+1)+' </div>';
                                html+='<div class="caseDetR" data-isn="'+data.ADDRESSES[i].ISN+'"> '+data.ADDRESSES[i].NAME+'</div>';
                                html+='</div>';
                                html+='</div>';
                            }
                    html+='</div>'
                    html+='</div>';
                }
                return html;
            }
            function RUBRIC(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>РУБРИКИ ('+data.RUBRICCNT+')</b></div>';
                if((data.RUBRIC)||(data.RUBRICCNT)){
                    html+='<div class="caseDetails">';
                        for (let i = 0; i < data.RUBRICCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.RUBRIC[i];
                            var NAME=el.NAME ? '<p>'+el.NAME+' </p>':"";
                            var INDEX=el.INDEX ? '<p>('+el.INDEX+') </p>':"";
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p data-isn="'+el.ISN+'"> '+NAME+INDEX+'</p>';
                            html+='</div>';
                            html+='</div>';
                        }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function LINK(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>СВЯЗКИ ('+data.LINKCNT+')</b></div>';
                if((data.LINKREF)||(data.LINKCNT)){
                    html+='<div class="caseDetails">';
                        for (let i = 0; i < data.LINKCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.LINKREF[i];
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p>'+el.TYPELINK+' <a href="'+el.URL+'">'+el.LINKINFO+'</a></p>';
                            html+='</div>';
                            html+='</div>';
                        }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function ADDR(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>АДРЕСАТЫ ('+data.ADDRCNT+')</b></div>';
                if(data.ADDR){
                    html+='<div class="caseDetails">';
                    if(data.ADDRCNT>0){
                        for (let i = 0; i < data.ADDRCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.ADDR[i];
                            var ORIGFLAG=REGION=POSTINDEX=CITY=ADDRESS=EMAIL=DELIVERY="";
                            if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG="";}
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            var datesmall=el.SENDDATE.split(' ')[0];
                            var time=el.SENDDATE.split(' ')[1].slice(0, -3);
                            var typeStruct=el[el["KINDADDR"]];
                            var CONSIST=el.CONSIST ? "<p><b>Состав:</b> "+el.CONSIST+"</p>":"";
                            if(typeStruct){
                                var REGION=typeStruct.REGION ? "<p>"+typeStruct.REGION+"</p>":"";
                                var POSTINDEX=typeStruct.POSTINDEX ? "<p>"+typeStruct.POSTINDEX+"</p>":"";
                                var CITY=typeStruct.CITY ? "<p>"+typeStruct.CITY+"</p>":"";
                                var ADDRESS=typeStruct.ADDRESS ? "<p>"+typeStruct.ADDRESS+"</p>":"";
                                var PERSON=typeStruct.PERSON ? "<p><b>Кому:</b> "+typeStruct.PERSON+"</p>":"";
                                var DATE=(el.SENDDATE)?('<div><b>Дата:</b> '+datesmall+' <b>в:</b> '+time+"</p>"+ORIGFLAG+'</div>'):"";
                                if(el.DELIVERY&&el.DELIVERY.ISN){DELIVERY="<p><b>Вид отправки:</b> "+el.DELIVERY.NAME+"</p>"}else{DELIVERY="";}
                                var EMAIL=typeStruct.EMAIL?"<p><b>Email:</b> <a href='mailto:"+typeStruct.EMAIL+"'>"+typeStruct.EMAIL+"</a></p>":"";
                                html+='<div><i>'+typeStruct.NAME+'</i></div>';
                                html+='<div>'+DATE+ORIGFLAG+DELIVERY+CONSIST+PERSON+'</div>';
                                if(REGION||POSTINDEX||CITY||ADDRESS){html+='<div><b>Адрес:</b> '+
                                REGION+POSTINDEX+CITY+ADDRESS+EMAIL+'</div>';}
                            }
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function COEXEC(){
                data.COEXEC=data.COEXEC?data.COEXEC:0;
                data.COEXECCNT=data.COEXECCNT?data.COEXECCNT:0;
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>СОИСПОЛНИТЕЛИ  ('+data.COEXECCNT+')</b></div>';
                if(data.COEXEC){
                    html+='<div class="caseDetails">';
                    if(data.COEXECCNT>0){
                        for (let i = 0; i < data.COEXECCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.COEXEC[i];
                            var ORGANIZ=el.ORGANIZ_NAME ? ("<p data-isn='"+el.ORGANIZ_ISN+"'>"+el.ORGANIZ_NAME+"</p> "):"";
                            var OUTNUM=el.OUTNUM ? 'Исх. №: '+el.OUTNUM+' ':"";
                            var OUTDATE=el.OUTDATE ? 'Дата: '+el.OUTDATE+' ':"";
                            var SIGN=el.SIGN ? 'Подписал: '+el.SIGN+' ':"";
                            html+='<div class="itemNumber">'+(i+1)+' </div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<div data-isn="'+el.ISN+'"> ';
                            html+='<div>'+ORGANIZ+'</div>';
                            html+='<div>'+OUTNUM+OUTDATE+SIGN+'</div>';
                            html+='</div>';
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function RESOLUTION(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ПОРУЧЕНИЯ ('+data.RESOLCNT+')</b></div>';
                if(data.RESOLCNT){
                    html+='<div class="caseDetails">';
                    if(data.RESOLCNT>0){
                        for (let i = 0; i < data.RESOLCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.RESOLUTION[i];
                            html+='<div class="itemNumber">'+(i+1)+' </div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            if(el.KIND==1){
                                var AUTHOR=el.AUTHOR_NAME ? ("<b>Автор:</b> <p data-isn='"+el.AUTHOR_ISN+"'><i>" + el.AUTHOR_NAME+"</i>" ) : "";
                                var RESOLDATE=el.RESOLDATE ? ("<b>От:</b> " + el.RESOLDATE +" "): "";
                                html+='<div>'+AUTHOR+RESOLDATE+'</div>';
                            }else if(el.KIND==2){
                                html+='<div><b>Пункт №</b> '+el.ITEMNUM+'</div>';
                            }
                            var PLANDATE=el.PLANDATE ? ("<b>План:</b> " + el.PLANDATE +" "): "";
                            var FACTDATE=el.FACTDATE ? ("<b>Факт:</b> " + el.FACTDATE +" "): "";
                            var CATEGORY=el.RESPRJPRIORITY ? ("<b>Категория:</b> " + el.RESPRJPRIORITY +" "): "";
                            var TEXT=el.TEXT ? ("<b>Текст:</b> " + el.TEXT +" "): "";
                            html+='<div>'+PLANDATE+FACTDATE+CATEGORY+'</div>';
                            html+='<div>'+TEXT+'</div>';
                            if(el.REPLYCNT>0){
                                html+='<div>Исполнители ('+el.REPLYCNT+"):";
                                for (let i = 0; i < el.REPLYCNT; i++){
                                    var el2=el.REPLY[i];
                                    html+="<div data-isn='"+el2.EXECUTOR_ISN+"'><i>"+el2.EXECUTOR_NAME+"</i></div>";
                                }
                                html+="</div>";
                            }
                            html+='</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function JOURNAL(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ЖУРНАЛ ПЕРЕДАЧИ ДОКУМЕНТА ('+data.JOURNALCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.JOURNALCNT>0)&&(data.JOURNAL)){
                    for (let i = 0; i < data.JOURNALCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.JOURNAL[i];
                        var ORIGFLAG=ORIGNUM="";
                        ORIGNUM=((el.ORIGNUM)&&(el.ORIGNUM!="0"))? " "+el.ORIGNUM:"";
                        if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG=underfined;}
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<div> '+el.SENDDATE.slice(0, -3)+' '+el.ADDRESSEE_NAME+' '+ORIGFLAG+el.ORIGNUM+'</div>';
                        html+='</div>';
                        html+='</div>';
                    }
                }
                html+='</div>';
                html+='</div>';
                return html;
            }
            function FORWARD(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ПЕРЕСЫЛКА ('+data.FORWARDCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.FORWARDCNT>0)&&(data.FORWARD)){
                    for (let i = 0; i < data.FORWARDCNT; i++) {
                        html+='<div class="caseItem">';
                        var NAME=USER="";
                        var el = data.FORWARD[i];
                        NAME=el.ADR_NAME ? el.ADR_NAME+" ":"";
                        USER=el.USER_NAME ? "Отправитель:" + el.USER_NAME +" ": "";
                        var SENDDATE=el.SENDDATE ? "Дата:" + el.SENDDATE.slice(0, -3) : "";
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        if(NAME){html+='<div>'+NAME+SENDDATE+'</div>';}
                        if(USER){html+='<div>'+USER+'</div>';}
                        html+='</div>';
                        html+='</div>';
                    }
                }
                html+='</div>';
                html+='</div>';
                return html;
            }
            var footer='</div>';

            var row="";
            row+=header();
            row+=FILES();
            if (rcType=="RCOUT"){row+=SIGNS();}
            if (rcType=="RCOUT"){row+=EXECUTOR();}
            if (rcType=="RCOUT"){row+=VISA();}
            if (rcType=="RCLET"){row+=AUTHOR();}
            if (rcType=="RCIN" ){row+=CORRESP();}
            //if ((rcType=="RCLET")||(rcType=="RCIN")){row+=CORRESP();}
            row+=komy();
            row+=RUBRIC();
            row+=LINK();
            row+=RESOLUTION();
            row+=ADDR();
            if(rcType=="RCOUT"){row+=COEXEC();}
            row+=JOURNAL();
            row+=FORWARD();
            row+=footer;
            $(".result").html(row);            
            $(".caseShort").click(function(){$(this).toggleClass('closed')});
    //      },
    //      error: function (jqXHR, exception) {
    //         $(".result").html(exception);
    //         console.log(exception);
    //         console.log(jqXHR);},
    // });
}



