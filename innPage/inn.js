var infoData={};
var titleData={};
//получаем номер ИНН
var inn= window.location.pathname.replace('inn:','').replace(/\//g, '');

makeTitle()
makeButtons();

function makeButtons() {//Для кнопок
    $.ajax({
        url: INFO.serverAdr + "data/"+inn,
        type: "GET",
        contentType: "application/json",
        success: function (users) {
            var rows= "";
            $.each(users, function (index, user) {
                rows+= makeButton(user);
                infoData[index]=user;
            })
            $(".fields").append(rows);
            console.log(infoData);
         }
    });
}
function makeTitle() {//Для заголовка
    $.ajax({
        url: INFO.serverAdr + "api/user/"+inn,
        type: "GET",
        contentType: "application/json",
        success: function (users) {
            $.each(users, function (index, user) {
                titleData[user.INN]=user;
            })
            $("#name").append(titleData[inn]['NAME_FULL']);
         }
    });
}
function makeButton (data) {
    var podrVal, buttonName;
    switch (data.OTDEL) {
        case "PRAVO": 
        podrVal='PRAVO';
        buttonName="Правовой отдел"; break;
        case "KEZO":  
        podrVal='KEZO'; 
        buttonName="Комитет имущественных и земельных отношений"; break;
        default: console.log("Error with data.OTDEL: " + data.OTDEL);
    }
    return  "<a class='link' href="+INFO.clientAdr+"otdel:"+inn+":"+podrVal+">"+buttonName+"</a>";
}

