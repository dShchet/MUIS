
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
    data1=[
        {
            "ISN": "4694",
            "DOCGROUP": {
                "ISN": "3678",
                "NAME": "Обращения граждан"
            },
            "REGNUM": "Кол-1",
            "ORDERNUM": "1",
            "SPECIMEN": "1,2",
            "DOCDATE": "04.06.2012 ",
            "CONSIST": "3 листа",
            "CONTENTS": "Проверка регистрации обращения гражданина.",
            "CARDREG": {
                "ISN": "0",
                "NAME": "Центральная картотека"
            },
            "CABREG": {
                "ISN": "4089",
                "NAME": "Ген. директор"
            },
            "ACCESSMODE": "False",
            "SECURITY": {
                "ISN": "1",
                "NAME": "общий"
            },
            "NOTE": "примечание",
            "ADDRESS_FLAG": "2",
            "ISCONTROL": "",
            "PLANDATE": "04.07.2012 ",
            "FACTDATE": "03.07.2012 ",
            "DELTA": "-1",
            "CARDCNT": "1",
            "CARD": [
                {
                    "DATE": "14.12.2018 11:21:56"
                }
            ],
            "LINKCNT": "0",
            "RUBRICCNT": "2",
            "RUBRIC": [
                {
                    "ISN": "4056964",
                    "NAME": "Производство товаров, качество продукции",
                    "INDEX": "1.1.1"
                },
                {
                    "ISN": "4056956",
                    "NAME": "Вопросы экологии и землепользования",
                    "INDEX": "1.3"
                }
            ],
            "ADDPROPSRUBRICCNT": "0",
            "ADDRCNT": "4",
            "ADDR": [
                {
                    "ISN": "4698",
                    "DATE_UPD": "14.12.2018 11:21:56",
                    "SENDDATE": "",
                    "ORIGFLAG": "False",
                    "ORDERNUM": "1",
                    "DATE_CR": "14.12.2018 11:21:56",
                    "PERSON": "",
                    "KINDADDR": "ORGANIZ",
                    "ORGANIZ": {
                        "ISN": "4220",
                        "FULLNAME": "",
                        "POSTINDEX": "",
                        "LAW_ADDRESS": "",
                        "INN": "7707083893",
                        "CITY": "Москва",
                        "NAME": "Сбербанк России",
                        "ADDRESS": "ул. Балчуг, 2",
                        "EMAIL": ""
                    }
                },
                {
                    "ISN": "4699",
                    "DATE_UPD": "14.12.2018 11:21:56",
                    "SENDDATE": "",
                    "ORIGFLAG": "False",
                    "ORDERNUM": "2",
                    "DATE_CR": "14.12.2018 11:21:56",
                    "PERSON": "",
                    "KINDADDR": "ORGANIZ",
                    "ORGANIZ": {
                        "ISN": "4223",
                        "FULLNAME": "",
                        "POSTINDEX": "",
                        "LAW_ADDRESS": "",
                        "INN": "7731041005",
                        "CITY": "Москва",
                        "NAME": "Межпромбанк",
                        "ADDRESS": "ул. Малая Красносельская, 2/8, корп. 4",
                        "EMAIL": "info@mprobank.ru"
                    }
                },
                {
                    "ISN": "4700",
                    "DATE_UPD": "14.12.2018 11:21:56",
                    "SENDDATE": "",
                    "ORIGFLAG": "False",
                    "ORDERNUM": "3",
                    "DATE_CR": "14.12.2018 11:21:56",
                    "PERSON": "",
                    "KINDADDR": "CITIZEN",
                    "CITIZEN": {
                        "ISN": "3939",
                        "NAME": "Нестерова Е.П.",
                        "ADDRESS": "ул. Ленина, д.5. кв. 56",
                        "REGION": "Московская область",
                        "POSTINDEX": "143960",
                        "CITY": "Реутов",
                        "EMAIL": ""
                    }
                },
                {
                    "ISN": "4701",
                    "DATE_UPD": "14.12.2018 11:21:56",
                    "SENDDATE": "",
                    "ORIGFLAG": "False",
                    "ORDERNUM": "4",
                    "DATE_CR": "14.12.2018 11:21:56",
                    "PERSON": "",
                    "KINDADDR": "DEPARTMENT",
                    "DEPARTMENT": {
                        "ISN": "3646",
                        "NAME": "Фалеев А.Д. - Нач. отдела № 1",
                        "EMAIL": ""
                    }
                }
            ],
            "FILESCNT": "2",
            "FILES": [
                {
                    "ISN": "4704",
                    "NAME": "4694-4704.docx",
                    "DESCRIPT": "somefile.docx",
                    "CONTENTS": "System.__ComObject",
                    "EDSCNT": "0",
                    "EDS": "System.__ComObject"
                },
                {
                    "ISN": "4707",
                    "NAME": "4694-4707.xlsx",
                    "DESCRIPT": "other somefile.xlsx",
                    "CONTENTS": "System.__ComObject",
                    "EDSCNT": "0",
                    "EDS": "System.__ComObject"
                }
            ],
            "JOURNACQCNT": "0",
            "PROTCNT": "1",
            "PROTOCOL": [
                {
                    "WHEN": "14.12.2018 11:21:56",
                    "WHAT": "Регистрация РК"
                }
            ],
            "ALLRESOL": "True",
            "RESOLCNT": "0",
            "JOURNALCNT": "0",
            "FORWARDCNT": "0",
            "ADDPROPSCNT": "0",
            "USER_CR": "System.__ComObject",
            "DATE_CR": "14.12.2018 11:21:56",
            "RUBRIC_first": "System.__ComObject",
            "CARD_first": "System.__ComObject",
            "ADDR_first": "System.__ComObject",
            "JOURNAL_first": "System.__ComObject",
            "JOURNACQ_first": "System.__ComObject",
            "ADDPROPS_first": "System.__ComObject",
            "ADDPROPSRUBRIC_first": "System.__ComObject",
            "PROTOCOL_first": "System.__ComObject",
            "FORWARD_first": "System.__ComObject",
            "ERRCODE": "0",
            "DOCKIND": "RCLET",
            "ADDRESSEE": "System.__ComObject",
            "ADDRESSESCNT": "2",
            "ADDRESSES": [
                {
                    "ISN": "3624",
                    "NAME": "Захаров П.Ф. - Генеральный директор",
                    "SURNAME": "Захаров П.Ф.",
                    "POST": "Генеральный директор"
                },
                {
                    "ISN": "3626",
                    "NAME": "Гончаров А.О. - Зам. ген. директора",
                    "SURNAME": "Гончаров А.О.",
                    "POST": "Зам. ген. директора"
                }
            ],
            "DELIVERY": {
                "ISN": "3774",
                "NAME": "Почта"
            },
            "TELEGRAM": "",
            "CORRESPCNT": "0",
            "ADDRESSESList": "System.__ComObject",
            "ISCOLLECTIVE": "True",
            "ISANONIM": "False",
            "AUTHORCNT": "3",
            "AUTHOR": [
                {
                    "ISN": "4695",
                    "CITIZEN": {
                        "NAME": "Тихорин В.И.",
                        "ADDRESS": "ул Мойка, д. 4, кв 56",
                        "REGION": "System.__ComObject",
                        "CITY": "Санкт-Петербург"
                    },
                    "ORDERNUM": "1",
                    "DATE_UPD": "14.12.2018 11:21:56"
                },
                {
                    "ISN": "4702",
                    "CITIZEN": {
                        "NAME": "Денисов Г.Р.",
                        "ADDRESS": "ул. Перовская , д. 345, кв. 15",
                        "REGION": "System.__ComObject",
                        "CITY": "Москва"
                    },
                    "ORDERNUM": "2",
                    "DATE_UPD": "14.12.2018 11:21:56"
                },
                {
                    "ISN": "4703",
                    "CITIZEN": {
                        "NAME": "Эльдина Е.Д.",
                        "ADDRESS": "ул. Таганская, д. 45, кв. 89",
                        "REGION": "System.__ComObject",
                        "CITY": "Москва"
                    },
                    "ORDERNUM": "3",
                    "DATE_UPD": "14.12.2018 11:21:56"
                }
            ]
        }
    ];
    
    $.ajax({
        url: INFO.deloAdr+"?need="+"one"+"&isn="+isn+"&rcType="+rcType,
        type: "GET", contentType: "application/json",
        success: function (data2) {
            console.log(data2);
            data=data2[0];

            function header(){
                var DELIVER=SECURITY=DELIVERY="";
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
                        for (let i = 0; i < data.CORRESP.lenght; i++) {
                            var el = data.CORRESP[i];
                            var ORGANIZ=el.ORGANIZ ? el.ORGANIZ+" ":"";
                            var OUTNUM=el.OUTNUM ? '<p>Исх. №: '+el.OUTNUM+'</p>':"";
                            var OUTDATE=el.OUTDATE ? '<p>Дата: '+el.OUTDATE+'</p>':"";
                            var SIGN=el.SIGN ? '<p>Подписал: '+el.SIGN+'</p>':"";
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            html+='<p data-isn="'+el.ISN+'"> '+ORGANIZ+'</p>'+OUTNUM+OUTDATE+SIGN;
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
                        for (let i = 0; i < data.AUTHOR.lenght; i++) {
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
                        for (let i = 0; i < data.RUBRIC.lenght; i++) {
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
                            for (let i = 0; i < data.ADDRESSES.lenght; i++){
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

                        for (let i = 0; i < data.VISA.lenght; i++) {
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
                        for (let i = 0; i < data.FILES.lenght; i++) {
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
                html='<div class="case">'+
                '<div class="caseShort caseShort-closed"><b>АДРЕСАТЫ ('+data.ADDRCNT+')</b></div>';
                if(data.ADDR){
                    html+='<div class="caseDetails">';
                    if(data.ADDRCNT>0){
                        for (let i = 0; i < data.ADDR.lenght; i++) {
                            var el = data.ADDR[i];
                            var ORIGFLAG=REGION=POSTINDEX=CITY=ADDRESS=EMAIL="";
                            if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG=underfined;}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            var date=el.SENDDATE.split(' ')[0];
                            var time=el.SENDDATE.split(' ')[1];
                            var typeStruct=el[el["KINDADDR"]];
                            if(typeStruct){
                                var REGION=typeStruct.REGION ? typeStruct.REGION+" ":"";
                                var POSTINDEX=typeStruct.POSTINDEX ? typeStruct.POSTINDEX+" ":"";
                                var CITY=typeStruct.CITY ? typeStruct.CITY+" ":"";
                                var ADDRESS=typeStruct.ADDRESS ? typeStruct.ADDRESS+" ":"";
                                var EMAIL=typeStruct.EMAIL?" Email: "+typeStruct.EMAIL:"";
                                html+='<div>'+typeStruct.NAME+'</div>';
                                if(REGION||POSTINDEX||CITY||ADDRESS||EMAIL){html+='<div>Адрес:'+REGION+POSTINDEX+CITY+ADDRESS+EMAIL+'</div>';}
                            }
                            if(el.SENDDATE){html+='<div>Дата: '+date+' в: '+time+' '+ORIGFLAG+'</div>';}
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
                        for (let i = 0; i < data.JOURNAL.lenght; i++) {
                            var el = data.JOURNAL[i];
                            var ORIGFLAG;
                            var ADDRESSEE=el.ADDRESSEE;
                            if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG=underfined;}
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<p class="itemNumber">'+(i+1)+'</p>';
                            if(ADDRESSEE){html+='<div> '+el.SENDDATE+' '+ADDRESSEE.NAME+' '+ORIGFLAG+'</div>';}
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
                        for (let i = 0; i < data.FORWARD.lenght; i++) {
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
                        for (let i = 0; i < data.PERSONSIGNS.lenght; i++) {
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
                        //for (let i = 0; i < data.PERSONSIGNS.lenght; i++) {
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
            var row=header();
            row+=FILES();
            if (rcType=="RCOUT")                   {row+=signs();}
            if (rcType=="RCOUT")                   {row+=executor();}
            if (rcType=="RCOUT")                   {row+=visu();}
            if (rcType=="RCLET")                   {row+=AUTHOR();}
            if((rcType=="RCIN")||(rcType=="RCLET")){row+=CORRESP();}
            if((rcType=="RCIN")||(rcType=="RCLET")){row+=komy();}
            row+=RUBRIC();
            row+=ADDR();
            row+=JOURNAL();
            row+=FORWARD();
            row+=footer;
            $(".result").html(row);            
            $(".caseShort").click(function(){$(this).toggleClass('caseShort-closed')});
         },
         error: function (jqXHR, exception) {
            $(".result").html(exception);
            console.log(exception);
            console.log(jqXHR);},
    });
}



