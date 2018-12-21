
function GetUsers() {
    $.ajax({
        url: "http://localhost:8080/api/users",
        type: "GET",
        contentType: "application/json",
        success: function (users) {
            var rows = "";
            $.each(users, function (index, user) {
                rows += row(user);
                infoData[user.INN]=user;
            })
            $("table tbody").empty();
            $("table tbody").append(rows);
         }
    });
}
infoData={};
function getFilter(inn, name) {
    console.log("getFilter");
    if(inn==""){inn="ALL"}
    if(name==""){name="ALL"}
    $.ajax({
        url: "http://localhost:8080/api/filter/"+inn+"."+name,
        type: "GET",
        contentType: "application/json",
        success: function (users) {
            var rows = "";
            $.each(users, function (index, user) {
                rows += row(user);
                infoData[user.INN]=user;
            })
            $("table tbody").empty();
            $("table tbody").append(rows);
         }
    });
}
var row = function (user) {
    return "<tr data-rowid='" + user.INN + "'><td><b><a href='inn/"+user.INN+"'>" + user.INN + "</b><p><a href='inn/"+user.INN+"'>" + user.NAME_FULL + "</a></p></td><td ><button class='infoBtn' onclick='info("+user.INN+")'>Info</button></td>";
}
$("#filterInn, #filterName").keyup(function(e){
    e.preventDefault();
    var filterInn  = $("#filterInn").val();
    var filterName = $("#filterName").val();
    getFilter(filterInn, filterName);
})
        
function info(inn) {
    $('body').toggleClass('nopopup');
    $('.popup .fields').empty();
    var data=infoData[inn];
    var text="";
    if(data.INN){text+="<div><b>ИНН:</b><p>"+data.INN+"</p></div>"}
    if(data.NAME_FULL){text+="<div><b>Название: </b><p>"+data.NAME_FULL+"</p></div>"}
    if(data.LEGAL_ADDR){text+="<div><b>Адрес: </b><p>"+data.LEGAL_ADDR+"</p></div>"}
    if(data.TELEPHON){text+="<div><b>Телефон: </b><p>"+data.TELEPHON+"</p></div>"}
    if(data.BANK){text+="<div><b>Банк:</b><p>"+data.BANK+"</p></div>"}
    if(data.BANK_ACCOUNT){text+="<div><b>Банк аккаунт: </b><p>"+data.BANK_ACCOUNT+"</p></div>"}
    if(data.BIK){text+="<div><b>БИК: </b><p>"+data.BIK+"</p></div>"}
    if(data.KOR_ACCOUNT){text+="<div><b>КОР Аккаунт:</b><p>"+data.KOR_ACCOUNT+"</p></div>"}
    if(data.KPP){text+="<div><b>KPP: </b><p>"+data.KPP+"</p></div>"}
    if(data.OGRN){text+="<div><b>ОГРН: </b><p>"+data.OGRN+"</p></div>"}
    if(data.Е_MAIL){text+="<div><b>Email: </b><p>"+data.Е_MAIL+"</p></div>"}
    $('.popup .fields').append(text);
}
$(".close, .shadow").click(function(){
  $('body').toggleClass('nopopup');
})
function goto(id){
  console.log(id);
}