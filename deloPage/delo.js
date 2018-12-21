var infoData={};
var titleData={};
var url=window.location.pathname.replace('delo:','').replace(/\//g, '');
var inn   = url.split(':')[0];
var category = url.split(':')[1];
switch (category) {
    case "arbitr":     catName="Арбитраж";                break;
    case "landLease":  catName="Аренда земли";            break;
    case "roomRental": catName="Аренда помещений";        break;
    case "general":    catName='"Дела общей юрисдикции"'; break;
    case "assigned":   catName='"Назначенные дела"';      break;
    case "viewed":     catName='"Дела на рассмотрении"';  break;
    default:catName='[ошибка]';
}
var error =0;
if(catName=="") error+=" Неверная категория;";
var infoData={};
function GetData() {
    $.ajax({
        url: "http://localhost:8080/api/delo/"+inn+'.'+category,
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            infoData=data[0];
            console.log(infoData);
            info();
         }
    });
}
function GetUser(inn, catName) {
    $.ajax({
        url: "http://localhost:8080/api/user/"+inn,
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            titleData=data[0];
            $(".title #name").append(titleData['NAME_FULL']);
            $(".title #cat").append(catName);
         }
    });
}
function info() {
    var data=infoData;
    var text="";
    var COURT_DATE;
    var dateOpt = {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      weekday: 'long',
    };
    if(data.NAME_FULL_CLAIMANT){text+="<div><b>Истец: </b><p>"+data.NAME_FULL_CLAIMANT+"</p></div>"}
    if(data.NAME_FULL_DEFENDANT){text+="<div><b>Ответчик: </b><p onclick='goto("+data.INN_DEFENDANT+")'>"+data.NAME_FULL_DEFENDANT+"</p></div>"}
    if(data.REG_DATE){
        var regDate=new Date(data.REG_DATE);
        var dateText=regDate.toLocaleString("ru", dateOpt);
        text+="<div><b>Дата регистрации: </b><p>"+dateText+"</p></div>"}
    if(data.COURT_DATE){
        var courtDate=new Date(data.COURT_DATE);
        var dateText=courtDate.toLocaleString("ru", dateOpt);
        text+="<div><b>Дата заседания: </b><p>"+dateText+"</p></div>"}
    if(data.JUDGE){text+="<div><b>Судья:</b><p>"+data.JUDGE+"</p></div>"}
    if(data.AMOUNT_OF_CLAIM){text+="<div><b>Размер денег:</b><p>"+data.AMOUNT_OF_CLAIM+"р.</p></div>"}
    if(data.CASE_NUMBER){text+="<div><b>Номер дела:</b><p>"+data.CASE_NUMBER+"</p></div>"}
    if(data.COURTROOM){text+="<div><b>Номер комнаты:</b><p>"+data.COURTROOM+"</p></div>"}
    if(data.SSP_FIO_CONTACT){text+="<div><b>Телефон судьи:</b><p>"+data.SSP_FIO_CONTACT+"</p></div>"}
    if(data.INN_DEFENDANT){text+="<div><b>ИНН ответчика:</b><p>"+data.INN_DEFENDANT+"</p></div>"}
    $('.fields').append(text);
}

// function goto(inn){
    // window.location.href="/delo:"+inn+":"+otdel;
// }
GetUser(inn, catName)
GetData();