
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
var isn= page.pathCLean.split(':')[0];

deloOneGet(isn);

function deloOneGet(isn){
    //console.log(isn);
    data=[
        {
            "ISN": "4324",
            "REGNUM": "В-1",
            "ORDERNUM": "1",
            "SPECIMEN": "1",
            "DOCDATE": "27.04.2012 0:00:00",
            "DOCKIND": "RCIN",
            "CONSIST": "15",
            "CONTENTS": "О применении инструкции №35 ФНС России",
            "ACCESSMODE": "False",
            "ADDRESSESCNT": "1",
            "ADDRESSES": [
                {
                    "ISN": "3624",
                    "NAME": "Захаров П.Ф. - Генеральный директор",
                    "SURNAME": "Захаров П.Ф.",
                    "POST": "Генеральный директор"
                }
            ],
            "TELEGRAM": "",
            "NOTE": "",
            "ADDRESS_FLAG": "0",
            "ISCONTROL": "",
            "PLANDATE": "",
            "FACTDATE": "",
            "CARDCNT": "1",
            "CORRESPCNT": "1",
            "CORRESP": [
                {
                    "ISN": "4238",
                    "ORGANIZ": "Министерство финансов РФ (Минфин РФ)",
                    "OUTNUM": "07-1-1234",
                    "OUTDATE": "25.04.2012 0:00:00",
                    "SIGN": "Силуанов А.Г.",
                    "NOTE": ""
                }
            ],
            "LINKCNT": "0",
            "RUBRICCNT": "0",
            "ADDPROPSRUBRICCNT": "0",
            "ADDRCNT": "0",
            "FILESCNT": "0",
            "JOURNACQCNT": "0",
            "PROTCNT": "0",
            "ALLRESOL": "True",
            "RESOLCNT": "0",
            "JOURNALCNT": "0",
            "FORWARDCNT": "0",
            "ADDPROPSCNT": "0",
            "ERRCODE": "0"
        }
    ];
    // $.ajax({
    //     url: INFO.deloAdr+"?need="+"one"+"&isn="+isn,
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
                temp.ADDRESSES=+
                "<div class=''>"+
                    "<div><b>КОМУ ("+data.ADDRESSESCNT+")</b></div>"+
                    "<p>1</p>"+
                    "<p> "+data.ADDRESSES[0].NAME+"</p>"+
                "</div>";
            }else{temp.CORRESP="";}
            if(data.CORRESP){
                temp.CORRESP=+
                "<div class=''>"+
                    "<div><b>КОРРЕСПОНДЕНТЫ ("+data.CORRESPCNT+")</b></div>"+
                    "<p>1</p>"+
                    "<p> "+data.CORRESP[0].ORGANIZ+"</p>"+
                    "<p>Исх. №: "+data.CORRESP[0].OUTNUM+"</p>"+
                    "<p>Дата: "+data.CORRESP[0].OUTDATE+"</p>"+
                    "<p>Подписал: "+data.CORRESP[0].SIGN+"</p>"+
                "</div>";
            }else{temp.CORRESP="";}
            var row= ""+
                "<div class='result-row' data-rowid='" + data.ISN + "'>"+
                    "<div class='result-data'><b>" + data.REGNUM + "</b>"+"<b style='float:right'>" + data.DOCDATE.split(' ')[0] + "</b>"+
                        "<p>" + data.CONTENTS + "</p>"+
                    "</div>"+
                    temp.CORRESP+
                    temp.ADDRESSES+
                "</div>";
            $(".result").html(row);            
            
        //  },
        //  error: function (jqXHR, exception) {
        //     console.log("Ошибка: "+jqXHR+"; exception: "+exception);
        //     console.log(jqXHR);},
    // });
}



