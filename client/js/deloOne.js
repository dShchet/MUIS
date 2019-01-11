
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
    data=[
        {
            "ISN": "4675",
            "REGNUM": "Р-1",
            "ORDERNUM": "1",
            "SPECIMEN": "1,2",
            "DOCDATE": "04.06.2012 0:00:00",
            "DOCKIND": "RCIN",
            "CONSIST": "3 листа",
            "CONTENTS": "Проверка регистрации входящего документа.",
            "ACCESSMODE": "False",
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
            "TELEGRAM": "",
            "NOTE": "примечание",
            "ADDRESS_FLAG": "2",
            "ISCONTROL": "",
            "PLANDATE": "04.07.2012 0:00:00",
            "FACTDATE": "03.07.2012 0:00:00",
            "DELTA": "-1",
            "CARDCNT": "1",
            "CARD": [
                {
                    "DATE": "14.12.2018 11:21:54"
                }
            ],
            "CORRESPCNT": "4",
            "CORRESP": [
                {
                    "ISN": "4216",
                    "ORGANIZ": "КБ &quot;Восток&quot;",
                    "OUTNUM": "П-112",
                    "OUTDATE": "12.05.2012 0:00:00",
                    "SIGN": "Карелин В.В.",
                    "NOTE": ""
                },
                {
                    "ISN": "4220",
                    "ORGANIZ": "Сбербанк России",
                    "OUTNUM": "",
                    "OUTDATE": "",
                    "SIGN": "",
                    "NOTE": ""
                },
                {
                    "ISN": "4223",
                    "ORGANIZ": "Межпромбанк",
                    "OUTNUM": "",
                    "OUTDATE": "",
                    "SIGN": "",
                    "NOTE": ""
                },
                {
                    "ISN": "4225",
                    "ORGANIZ": "Внешторгбанк",
                    "OUTNUM": "235-T",
                    "OUTDATE": "12.05.2012 0:00:00",
                    "SIGN": "Петренко П.П. - Главный начальник",
                    "NOTE": ""
                }
            ],
            "LINKCNT": "0",
            "RUBRICCNT": "2",
            "RUBRIC": [
                {
                    "ISN": "4056964",
                    "NAME": "Производство товаров, качество продукции"
                },
                {
                    "ISN": "4056956",
                    "NAME": "Вопросы экологии и землепользования"
                }
            ],
            "ADDPROPSRUBRICCNT": "0",
            "ADDRCNT": "4",
            "ADDR": [
                {
                    "ISN": "4679",
                    "PERSON": "",
                    "REG_N": "",
                    "NOTE": "",
                    "REG_DATE": "",
                    "KINDADDR": "ORGANIZ",
                    "NAME": "Сбербанк России",
                    "ADDRESS": "ул. Балчуг, 2",
                    "EMAIL": ""
                },
                {
                    "ISN": "4680",
                    "PERSON": "",
                    "REG_N": "",
                    "NOTE": "",
                    "REG_DATE": "",
                    "KINDADDR": "ORGANIZ",
                    "NAME": "Межпромбанк",
                    "ADDRESS": "ул. Малая Красносельская, 2/8, корп. 4",
                    "EMAIL": "info@mprobank.ru"
                },
                {
                    "ISN": "4681",
                    "PERSON": "",
                    "REG_N": "",
                    "NOTE": "",
                    "REG_DATE": "",
                    "KINDADDR": "CITIZEN",
                    "NAME": "Нестерова Е.П.",
                    "ADDRESS": "ул. Ленина, д.5. кв. 56",
                    "EMAIL": ""
                },
                {
                    "ISN": "4682",
                    "PERSON": "",
                    "REG_N": "",
                    "NOTE": "",
                    "REG_DATE": "",
                    "KINDADDR": "DEPARTMENT",
                    "NAME": "Фалеев А.Д. - Нач. отдела № 1",
                    "EMAIL": ""
                }
            ],
            "FILESCNT": "2",
            "JOURNACQCNT": "0",
            "PROTCNT": "1",
            "ALLRESOL": "True",
            "RESOLCNT": "0",
            "PROTOCOL(": [
                {}
            ],
            "RESOLUTION": [],
            "JOURNALCNT": "0",
            "FORWARDCNT": "2",
            "ADDPROPSCNT": "0",
            "ERRCODE": "0"
        }
    ];
    // $.ajax({
    //     url: INFO.deloAdr+"?need="+"one"+"&isn="+isn+"&rcType="+rcType,
    //     type: "GET", contentType: "application/json",
    //     success: function (data) {
            console.log(data);
            data=data[0];
            var temp={};
            // data.CORRESP=[
            //     {
            //         "ISN": "4238",
            //         "ORGANIZ": "Министерство финансов РФ (Минфин РФ)",
            //         "OUTNUM": "07-1-1234",
            //         "OUTDATE": "25.04.2012 0:00:00",
            //         "SIGN ": "Силуанов А.Г.",
            //         "NOTE ": ""
            //     }
            // ]
            if(data.ADDRESSES){
                temp.ADDRESSES=
                '<div class="case">'+
                    '<div class="caseShort"><b>КОМУ ('+data.ADDRESSESCNT+')</b></div>'+
                    '<p>1</p>'+
                    '<p>'+data.ADDRESSES[0].NAME+'</p>'+
                '</div>';
            }else{temp.CORRESP="";}
            if(data.CORRESP){
                temp.CORRESP=
                '<div class="case">'+
                    '<div class="caseShort"><b>КОРРЕСПОНДЕНТЫ ('+data.CORRESPCNT+')</b></div>';
                    if(data.CORRESP){
                        temp.CORRESP=+
                    '<div class="caseDetails">'+
                        '<p>1</p>'+
                        '<p> '+data.CORRESP[0].ORGANIZ+'</p>'+
                        '<p>Исх. №: '+data.CORRESP[0].OUTNUM+'</p>'+
                        '<p>Дата: '+data.CORRESP[0].OUTDATE+'</p>'+
                        '<p>Подписал: '+data.CORRESP[0].SIGN+'</p>'+
                    '</div>';
                    }
                '</div>';
            }else{temp.CORRESP="";}
            var row=
                '<div class="result-row" data-rowid="' + data.ISN + '">'+
                    '<div class="result-data"><b>' + data.REGNUM + '</b>'+'<b style="float:right">' + data.DOCDATE.split(' ')[0] + '</b>'+
                        '<p>' + data.CONTENTS + '</p>'+
                    '</div>'+
                    '<div class="case">'+
                        '<div class="caseShort"><b>КОРРЕСПОНДЕНТЫ ('+data.CORRESPCNT+')</b></div>';
                        if(data.CORRESP){row+=
                        '<div class="caseDetails">';
                            if(data.ADDRESSESCNT>0){row+=
                            '<p>1</p>';
                            }row+=
                            '<p data-isn="'+data.CORRESP[0].ISN+'"> '+data.CORRESP[0].ORGANIZ+'</p>'+
                            '<p>Исх. №: '+data.CORRESP[0].OUTNUM+'</p>'+
                            '<p>Дата: '+data.CORRESP[0].OUTDATE+'</p>'+
                            '<p>Подписал: '+data.CORRESP[0].SIGN+'</p>'+
                        '</div>';
                        }row+=
                    '</div>'+
                    '<div class="case">'+
                        '<div class="caseShort"><b>КОМУ ('+data.ADDRESSESCNT+')</b></div>';
                        if(data.ADDRESSES){row+=
                        '<div class="caseDetails">';
                            if(data.ADDRESSESCNT>0){row+=
                            '<p>1</p>';
                            }row+=
                            '<p data-isn="'+data.ADDRESSES[0].ISN+'">'+data.ADDRESSES[0].NAME+'</p>'+
                        '</div>';
                        }row+=
                    '</div>'+
                '</div>';
            $(".result").html(row);            
            
        //  },
        //  error: function (jqXHR, exception) {
        //     console.log("Ошибка: "+jqXHR+"; exception: "+exception);
        //     console.log(jqXHR);},
    // });
}



