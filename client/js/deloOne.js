
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
    data1=[{"ISN":"4853","DOCGROUP":{"ISN":"3674","NAME":"Входящие из разных организаций и предприятий"},"REGNUM":"Р-5","SPECIMEN":"1,2","DOCDATE":"04.06.2012 ","CONSIST":"3 листа","CONTENTS":"Проверка регистрации входящего документа.","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"True","SECURITY":{"ISN":"1","NAME":"общий"},"NOTE":"примечание","ADDRESS_FLAG":"2","ISCONTROL":"2","PLANDATE":"04.07.2012 ","FACTDATE":"03.07.2012 ","DELTA":"-1","LINKCNT":"1","LINKREF":[{"ISN":"4969","TYPELINK":"Исполнено","ORDERNUM":"1","LINKINFO":"Проект П-1 от 17/01/2019 Приказы по основной деятельности"}],"RUBRICCNT":"2","RUBRIC":[{"ISN":"4056964","NAME":"Производство товаров, качество продукции","INDEX":"1.1.1"},{"ISN":"4056956","NAME":"Вопросы экологии и землепользования","INDEX":"1.3"}],"ADDPROPSRUBRICCNT":"0","ADDRCNT":"7","ADDR":[{"ISN":"4858","REG_DATE":"30.01.2019 ","CONSIST":"сост","SENDDATE":"04.01.2019 10:27:00","ORDERNUM":"1","PERSON":"пригожину","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"прим","DELIVERY":{"ISN":"3775","NAME":"Фельдсвязь"},"ORGANIZ":{"ISN":"4223","POSTINDEX":"","CITY":"Москва","NAME":"Межпромбанк","ADDRESS":"ул. Малая Красносельская, 2/8, корп. 4","EMAIL":"info@mprobank.ru"}},{"ISN":"4859","REG_DATE":"","CONSIST":"сост","SENDDATE":"04.01.2019 10:26:00","ORDERNUM":"3","PERSON":"","KINDADDR":"CITIZEN","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"CITIZEN":{"ISN":"3939","NAME":"Нестерова Е.П.","ADDRESS":"ул. Ленина, д.5. кв. 56","REGION":"Московская область","POSTINDEX":"143960","CITY":"Реутов","EMAIL":""}},{"ISN":"4860","REG_DATE":"","CONSIST":"","SENDDATE":"24.01.2019 10:28:00","ORDERNUM":"4","PERSON":"","KINDADDR":"DEPARTMENT","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"DEPARTMENT":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1","EMAIL":""}},{"ISN":"4945","REG_DATE":"29.01.2019 ","CONSIST":"3 листа","SENDDATE":"04.01.2019 10:28:00","ORDERNUM":"5","PERSON":"Юманина Н.М.","KINDADDR":"ORGANIZ","NOTE":"прим","REG_N":"777","DELIVERY":{"ISN":"3779","NAME":"Спецсвязь"},"ORGANIZ":{"ISN":"4246","POSTINDEX":"167000","CITY":"Сыктывкар","NAME":"Филиал республики Коми","ADDRESS":"ул. Бабушкина, д.10","EMAIL":""}},{"ISN":"4947","REG_DATE":"","CONSIST":"3 листа","SENDDATE":"","ORDERNUM":"6","PERSON":"Розова И.С.","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4243","POSTINDEX":"160000","CITY":"Вологда","NAME":"Филиал Вологодской области","ADDRESS":"ул. Гоголя, д.13","EMAIL":""}},{"ISN":"4952","REG_DATE":"","CONSIST":"3 листа","SENDDATE":"","ORDERNUM":"7","PERSON":"","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4057793","POSTINDEX":"107113","CITY":"Москва","NAME":"Наша организация","ADDRESS":"ул. Шумкина, 20, стр.1","EMAIL":"market@eos.ru"}},{"ISN":"4956","REG_DATE":"","CONSIST":"3 листа","SENDDATE":"","ORDERNUM":"8","PERSON":"","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4234","POSTINDEX":"125993","CITY":"Москва","NAME":"Министерство культуры РФ (Минкультуры РФ)","ADDRESS":"Малый Гнездниковский пер., 7","EMAIL":"info@mkrf.ru"}}],"FILESCNT":"0","JOURNACQCNT":"0","PROTCNT":"7","PROTOCOL":[{"WHEN":"14.12.2018 15:36:54","WHAT":"Регистрация РК"},{"WHEN":"17.01.2019 10:33:34","WHAT":"Изменение флага &quot;Срочно&quot;"},{"WHEN":"17.01.2019 10:33:34","WHAT":"Изменение флага &quot;РК персон. доступа&quot;"},{"WHEN":"17.01.2019 10:37:44","WHAT":"Ред. контрольности РК"},{"WHEN":"17.01.2019 10:46:49","WHAT":"Уд.адресата"},{"WHEN":"17.01.2019 10:46:49","WHAT":"Уд.адресата"},{"WHEN":"17.01.2019 10:46:49","WHAT":"Уд.адресата"}],"ALLRESOL":"True","RESOLCNT":"3","RESOLUTION":[{"RESOLCNT":"0","ISN":"66","KIND":"2","ITEMNUM":"1","AUTHOR":{"ISN":"","NAME":"","SURNAME":""},"TEXT":"сом пункт текст","RESOLDATE":"","SENDDATE":"17.01.2019 10:50:58","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"10.07.2019 ","FACTDATE":"24.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"67","EXECUTOR":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1"}},{"ISN":"68","EXECUTOR":{"ISN":"4057914","NAME":"Наша организация"}}],"RESOLCNT":"0"},{"RESOLCNT":"0","ISN":"69","KIND":"1","ITEMNUM":"","AUTHOR":{"ISN":"3650","NAME":"Шевченко А.Л. - Нач. отдела № 2","SURNAME":"Шевченко А.Л."},"TEXT":"cjv1","RESOLDATE":"13.01.2019 ","SENDDATE":"17.01.2019 12:06:18","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"True","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"17.01.2019 ","FACTDATE":"19.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"70","EXECUTOR":{"ISN":"3636","NAME":"Лихачева С.Л. - Специалист"}},{"ISN":"71","EXECUTOR":{"ISN":"4057915","NAME":"Министерство культуры РФ (Минкультуры РФ)"}}],"RESOLCNT":"0"},{"RESOLCNT":"0","ISN":"60","KIND":"1","ITEMNUM":"","AUTHOR":{"ISN":"3648","NAME":"Кречетов А.В. - Ведущий специалист","SURNAME":"Кречетов А.В."},"TEXT":"текст","RESOLDATE":"17.01.2019 ","SENDDATE":"17.01.2019 10:37:43","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"23.01.2019 ","FACTDATE":"18.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"5","REPLY":[{"ISN":"61","EXECUTOR":{"ISN":"3656","NAME":"Юренев А.Т. - Зам. нач. управления"}},{"ISN":"62","EXECUTOR":{"ISN":"3654","NAME":"Толкачев О.Е. - Нач. управления"}},{"ISN":"63","EXECUTOR":{"ISN":"4057911","NAME":"Розова И.С. - Филиал Вологодской области"}},{"ISN":"64","EXECUTOR":{"ISN":"4057912","NAME":"Долгин И.П. - Филиал Вологодской области"}},{"ISN":"65","EXECUTOR":{"ISN":"4057913","NAME":"Министерство экономического развития РФ (Минэкономразвития РФ)"}}],"RESOLCNT":"0"}],"JOURNALCNT":"7","JOURNAL":[{"ISN":"4939","ADDRESSEE":{"ISN":"3614","NAME":"Управление по основной деятельности"},"ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"17.01.2019 10:05:00"},{"ISN":"4940","ADDRESSEE":{"ISN":"3664","NAME":"Королева И.В. - Ведущий специалист"},"ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"17.01.2019 10:06:00"},{"ISN":"4941","ADDRESSEE":{"ISN":"3662","NAME":"Ломакин Р.А. - Нач. отдела кадров"},"ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"17.01.2019 10:06:00"},{"ISN":"4950","ADDRESSEE":{"ISN":"3656","NAME":"Юренев А.Т. - Зам. нач. управления"},"ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"17.01.2019 10:37:00"},{"ISN":"4951","ADDRESSEE":{"ISN":"3654","NAME":"Толкачев О.Е. - Нач. управления"},"ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"17.01.2019 10:37:00"},{"ISN":"4953","ADDRESSEE":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1"},"ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"17.01.2019 10:50:00"},{"ISN":"4957","ADDRESSEE":{"ISN":"3636","NAME":"Лихачева С.Л. - Специалист"},"ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"17.01.2019 12:06:00"}],"FORWARDCNT":"2","FORWARD":[{"ISN":"4866","ADDRESSEE":{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},"USER":{"ISN":"3611","NAME":"Тверская У.Л."},"SENDDATE":"14.12.2018 15:36:54"},{"ISN":"4867","ADDRESSEE":{"ISN":"3626","NAME":"Гончаров А.О. - Зам. ген. директора"},"USER":{"ISN":"3611","NAME":"Тверская У.Л."},"SENDDATE":"14.12.2018 15:36:54"}],"ADDPROPSCNT":"0","DOCKIND":"RCIN","ADDRESSESCNT":"2","ADDRESSES":[{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},{"ISN":"3626","NAME":"Гончаров А.О. - Зам. ген. директора"}],"DELIVERY":{"ISN":"3774","NAME":"Почта"},"TELEGRAM":"","CORRESPCNT":"7","CORRESP":[{"ISN":"4216","ORGANIZ":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В.","NOTE":""},{},{"ISN":"4220","ORGANIZ":"Сбербанк России","OUTNUM":"","OUTDATE":"","SIGN":"","NOTE":""},{},{"ISN":"4223","ORGANIZ":"Межпромбанк","OUTNUM":"","OUTDATE":"","SIGN":"","NOTE":""},{},{"ISN":"4225","ORGANIZ":"Внешторгбанк","OUTNUM":"235-T","OUTDATE":"12.05.2012 ","SIGN":"Петренко П.П. - Главный начальник","NOTE":"ghbv"}]}];

    // $.ajax({
    //     url: INFO.deloAdr+"?need="+"one"+"&isn="+isn+"&rcType="+rcType,
    //     type: "GET", contentType: "application/json",
    //     success: function (data2) {
            data=data1[0];
            console.log("data:");
            console.log(data.ADDRCNT);
            function header(){
                var SECURITY=DELIVERY="";
                var REGNUM=data.REGNUM ? '<p>'+data.REGNUM+' </p>':"";
                if(data.SECURITY){SECURITY=data.SECURITY.NAME ? '<p>Доступ: '+data.SECURITY.NAME+' </p>':"";}
                var SPECIMEN=data.SPECIMEN ? '<p>Экз №: '+data.SPECIMEN+' </p>':"";
                var CONSIST=data.CONSIST ? '<p>Состав: '+data.CONSIST+' </p>':"";
                var DOCGROUP=data.DOCGROUP.NAME ? '<p>'+data.DOCGROUP.NAME+' </p>':"";
                var DOCDATE=data.DOCDATE ? '<p>от '+data.DOCDATE.split(' ')[0]+' </p>':"";
                var CONTENTS=data.CONTENTS ? '<p>Содерж.: '+data.CONTENTS+' </p>':"";
                var PLANDATE=data.PLANDATE ? '<p>План.: '+data.PLANDATE+' </p>':"";
                var FACTDATE=data.FACTDATE ? '<p>Факт.: '+data.FACTDATE+' </p>':"";
                if(data.DELIVERY) { DELIVERY=data.DELIVERY ?'<p>Доставка: '+data.DELIVERY.NAME+' </p>':"";}
                var ISCOLLECTIVE=(data.ISCOLLECTIVE==="True") ? '<p>Коллективное</p>':"";
                var NOTE=data.NOTE ? '<p>Примечание '+data.NOTE+' </p>':"";
                html='<div class="result-row" data-rowid="' + data.ISN + '">'+
                '<div class="result-data">'+ REGNUM+ DOCGROUP+ DOCDATE+ SPECIMEN+ SECURITY+ CONSIST+ DELIVERY;
                html+='<div>'+PLANDATE+FACTDATE+ ISCOLLECTIVE+'</div>';
                html+='<div>'+CONTENTS+'</div>';
                html+='<div>'+NOTE+'</div>';
                html+='</div>';
                return html;
            };
            var footer='</div>';
            function CORRESP(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>КОРРЕСПОНДЕНТЫ ('+data.CORRESPCNT+')</b></div>';
                if(data.CORRESP){
                    html+='<div class="caseDetails">';
                    if(data.CORRESPCNT>0){
                        for (let i = 0; i < data.CORRESPCNT; i++) {
                            var el = data.CORRESP[i];
                            var ORGANIZ=el.ORGANIZ ? el.ORGANIZ+" ":"";
                            var OUTNUM=el.OUTNUM ? 'Исх. №: '+el.OUTNUM+' ':"";
                            var OUTDATE=el.OUTDATE ? 'Дата: '+el.OUTDATE+' ':"";
                            var SIGN=el.SIGN ? 'Подписал: '+el.SIGN+' ':"";
                            var NOTE=el.NOTE ? '<div>Прим.: '+el.NOTE+'</div>':"";
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+' </p>';
                            html+='<p data-isn="'+el.ISN+'"> '+ORGANIZ+OUTNUM+OUTDATE+SIGN+'</p>';
                            html+=NOTE;
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
                '<div class="caseShort caseShort-closed"><b>АВТОРЫ ('+data.AUTHORCNT+')</b></div>';
                if(data.AUTHOR){
                    html+='<div class="caseDetails">';
                    if(data.AUTHORCNT>0){
                        for (let i = 0; i < data.AUTHORCNT; i++) {
                            var NAME=""
                            var el = data.AUTHOR[i];
                            if(el.CITIZEN){ NAME=el.CITIZEN.NAME ? el.CITIZEN.NAME+" ":"";}
                            var DATE_UPD=el.DATE_UPD ? '<p>Дата: '+el.DATE_UPD.split(' ')[0]+'</p>':"";
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<p data-isn="'+el.ISN+'"> '+NAME+DATE_UPD+'</p>'
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function RUBRIC(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>РУБРИКИ ('+data.RUBRICCNT+')</b></div>';
                if((data.RUBRIC)||(data.RUBRICCNT)){
                    html+='<div class="caseDetails">';
                        for (let i = 0; i < data.RUBRICCNT; i++) {
                            var el = data.RUBRIC[i];
                            var NAME=el.NAME ? '<p>'+el.NAME+' </p>':"";
                            var INDEX=el.INDEX ? '<p>('+el.INDEX+') </p>':"";
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<p data-isn="'+el.ISN+'"> '+NAME+INDEX+'</p>';
                            html+='</div>';
                        }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function komy(){
                var html="";
                if((data.ADDRESSES)||(data.ADDRESSESCNT)){
                    html+='<div class="case">'+
                    '<div class="caseShort caseShort-closed"><b>КОМУ ('+data.ADDRESSESCNT+')</b></div>';
                        html+='<div class="caseDetails">';
                            for (let i = 0; i < data.ADDRESSESCNT; i++){
                                html+='<p class="itemNumber">'+(i+1)+' </p>'+'<p data-isn="'+data.ADDRESSES[i].ISN+'"> '+data.ADDRESSES[i].NAME+'</p>';
                            }
                    html+='</div>'
                    html+='</div>';
                }
                return html;
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
                            var ISN=NAME="";
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            if(el.EMPLOY){
                                var ISN=el.EMPLOY.ISN ? el.EMPLOY.ISN:"";
                                var NAME=el.EMPLOY.NAME ? el.EMPLOY.NAME:"";
                                html+='<div data-isn="'+ISN+'">'+NAME+' Дата: '+date+time+'</div>';
                            }
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function FILES(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>ФАЙЛЫ ('+data.FILESCNT+')</b></div>';
                if(data.FILES){
                    html+='<div class="caseDetails">';
                    if(data.FILESCNT>0){
                        for (let i = 0; i < data.FILESCNT; i++) {
                            var el = data.FILES[i];
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'" data-name="'+el.NAME+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<div>'+el.DESCRIPT+'</div>';
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            
            function ADDR(){
                var html="";
                console.log("data.ADDRCNT ");
                console.log(data);
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>АДРЕСАТЫ ('+data.ADDRCNT+')</b></div>';
                if(data.ADDR){
                    html+='<div class="caseDetails">';
                    if(data.ADDRCNT>0){
                        for (let i = 0; i < data.ADDRCNT; i++) {
                            var el = data.ADDR[i];
                            var ORIGFLAG=REGION=POSTINDEX=CITY=ADDRESS=EMAIL=DELIVERY="";
                            if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG="";}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            var data=el.SENDDATE.split(' ')[0];
                            var time=el.SENDDATE.split(' ')[1];
                            var typeStruct=el[el["KINDADDR"]];
                            if(typeStruct){
                                var REGION=typeStruct.REGION ? typeStruct.REGION+" ":"";
                                var POSTINDEX=typeStruct.POSTINDEX ? typeStruct.POSTINDEX+" ":"";
                                var CITY=typeStruct.CITY ? typeStruct.CITY+" ":"";
                                var ADDRESS=typeStruct.ADDRESS ? typeStruct.ADDRESS+" ":"";
                                var PERSON=typeStruct.PERSON ? "Кому: "+typeStruct.PERSON+" ":"";
                                var CONSIST=typeStruct.CONSIST ? "Состав: "+typeStruct.CONSIST+" ":"";
                                var DATE='<div>Дата: '+data+' в: '+time+' '+ORIGFLAG+'</div>';
                                if(el.DELIVERY){DELIVERY="Вид отправки: "+el.DELIVERYю.NAME}else{DELIVERY="";}
                                var EMAIL=typeStruct.EMAIL?" Email: "+typeStruct.EMAIL:"";
                                html+='<div>'+typeStruct.NAME+'</div>';
                                if(el.SENDDATE){html+=DATE+ORIGFLAG+DELIVERY+CONSIST+PERSON;}
                                if(REGION||POSTINDEX||CITY||ADDRESS){html+='<div>Адрес:'+
                                REGION+POSTINDEX+CITY+ADDRESS+EMAIL+'</div>';}
                            }
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
                '<div class="caseShort caseShort-closed"><b>ЖУРНАЛ ПЕРЕДАЧИ ДОКУМЕНТА ('+data.JOURNALCNT+')</b></div>';
                if(data.JOURNAL){
                    html+='<div class="caseDetails">';
                    if(data.JOURNALCNT>0){
                        for (let i = 0; i < data.JOURNALCNT; i++) {
                            var el = data.JOURNAL[i];
                            var ORIGFLAG=ORIGNUM="";
                            var ADDRESSEE=el.ADDRESSEE;
                            ORIGNUM=((el.ORIGNUM)&&(el.ORIGNUM!="0"))? " "+el.ORIGNUM:"";
                            if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG=underfined;}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            if(ADDRESSEE){html+='<div> '+el.SENDDATE+' '+ADDRESSEE.NAME+' '+ORIGFLAG+ORIGNUM+'</div>';}
                            html+='</div>';
                        }
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function FORWARD(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>ПЕРЕСЫЛКА ('+data.FORWARDCNT+')</b></div>';
                if(data.FORWARD){
                    html+='<div class="caseDetails">';
                    if(data.FORWARDCNT>0){
                        for (let i = 0; i < data.FORWARDCNT; i++) {
                            var NAME=USER="";
                            var el = data.FORWARD[i];
                            if(el.ADDRESSEE){NAME=el.ADDRESSEE.NAME ? el.ADDRESSEE.NAME+" ":"";}
                            if(el.ADDRESSEE){USER=el.USER.NAME ? "Отправитель:" + el.USER.NAME +" ": "";}
                            var SENDDATE=el.SENDDATE ? "Дата:" + el.SENDDATE : "";
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            if(NAME){html+='<div>'+NAME+SENDDATE+'</div>';}
                            if(USER){html+='<div>'+USER+'</div>';}
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
                            if(data.PERSONSIGNS[i].WHO_SIGN){
                            var el=data.PERSONSIGNS[i].WHO_SIGN
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<div> '+el.NAME+'</div>';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            };
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
                            var i=0;
                            if(data.EXECUTOR){
                                var el = data.EXECUTOR;
                                html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                                html+='<p class="itemNumber">'+(i+1)+'</p>';
                                html+='<div> '+el.NAME+'</div>';
                                html+='</div>';
                            }
                        //}
                    //}
                    html+='</div>';
                //}
                html+='</div>';
                return html;
            }
            var row=header();
            row+=FILES();
            if (rcType=="RCOUT")                   {row+=signs();}
            if (rcType=="RCOUT")                   {row+=executor();}
            if (rcType=="RCOUT")                   {row+=visu();}
            if (rcType=="RCLET")                   {row+=AUTHOR();}
            row+=CORRESP();
            row+=komy();
            row+=RUBRIC();
            row+=ADDR();
            row+=JOURNAL();
            row+=FORWARD();
            row+=footer;
            $(".result").html(row);            
            $(".caseShort").click(function(){$(this).toggleClass('caseShort-closed')});
//          },
//          error: function (jqXHR, exception) {
//             $(".result").html(exception);
//             console.log(exception);
//             console.log(jqXHR);},
//     });
}



