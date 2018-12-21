var infoData={}, titleData={};
var url=window.location.pathname.replace('otdel:','').replace(/\//g, '');
//получаем номер ИНН
var inn   = url.split(':')[0];
//получаем номер название отдела в заголовок
var otdel = url.split(':')[1];
if(otdel=="PRAVO"){otdel="Правовом отделе"}
if(otdel=="KEZO" ){otdel="Комитете имущественных и земельных отношений"}

makeTitle()//Сделать заголовок
makeButtons();//Сделать кнопки

function makeButtons() {//Для кнопок
    $.ajax({
        url: INFO.serverAdr + "api/otdel/" + inn,
        type: "GET",
        contentType: "application/json",
        success: function (users) {
            var rows = "";
            $.each(users, function (index, user) {
                rows += makeButton(user);
                infoData[index]=user['OTDEL_PRAVO'];
            })
            $(".fields").append(rows);
         }
    });
}

function makeTitle() {//Для заголовка
    $.ajax({
        url: INFO.serverAdr + "api/user/" + inn,
        type: "GET",
        contentType: "application/json",
        success: function (users) {
            $.each(users, function (index, data) {
                titleData[data.INN]=data;
            })
            $(".title #name").append(titleData[inn]['NAME_FULL']);
            $(".title #otdel").append(otdel);
         }
    });
}

function makeButton (data) {
    var podrVal, buttonName;
    switch (data.OTDEL_PRAVO) {
        case "ARBITRATION":     podrVal='arbitr'; buttonName="Арбитраж";              break;
        case "ISP_LAND_LEASE":  podrVal="landLease";   buttonName="Аренда земли";          break;
        case "ISP_ROOM_RENTAL": podrVal="roomRental";   buttonName="Аренда помещений";      break;
        case "GEN_JURISD":      podrVal='general'; buttonName="Дела общей юрисдикции"; break;
        case "CAS_SCHE_DES":    podrVal='assigned'; buttonName="Назначенные дела";      break;
        case "CASES_RREVIEW":   podrVal='viewed'; buttonName="Дела на рассмотрении";  break;
        default:console.log("Error with data.OTDEL_PRAVO: " + data.OTDEL_PRAVO);
    }
    return  "<a class='link' href="+INFO.clientAdr+"delo:"+inn+":"+podrVal+" >"+buttonName+"</a>";
}
