
//незалогиненые идут в логин
var logged = Cookies.get('miusLogged');
if(logged!='Logged'){
    var loginUrl=INFO.serverAdr;
    window.location.replace(loginUrl);
}
//определение страницы и хлебных крошек
var page={inn:{},otdel:{},case:{},};
page.path=window.location.pathname;
if(page.path.startsWith("/search"))      {page.deep=1; page.name="search"
}else if(page.path.startsWith("/delo"))  {page.deep=1; page.name="delo"
}else if(page.path.startsWith("/inn"))   {page.deep=2; page.name="inn"
}else if(page.path.startsWith("/otdel")) {page.deep=3; page.name="otdel"
}else if(page.path.startsWith("/case"))  {page.deep=4; page.name="case"
}else{console.log("error! wrong page.path: " + page.path);}

  if(page.deep > 0){//Search page and deeper
} if(page.deep > 1){//INN page and deeper
  page.searchLink="<a href='"+INFO.clientAdr+"search/'>Поиск</a><span>/</span>";
  page.pathCLean= page.path.substring(page.path.indexOf(":") + 1).replace(/\//g, "");
  page.inn.name= page.pathCLean.split(':')[0];
  page.inn.link= INFO.clientAdr+"inn:"+page.inn.name;
  if(page.name=="inn"){
    page.breadcrumbs=page.searchLink+"<i>"+page.inn.name+"</i>";
  }
} if(page.deep > 2){//Otdel page and deeper
  page.otdel.raw=page.pathCLean.split(':')[1];
  page.otdel.name =cookOtdel(page.otdel.raw)["name"];
  page.otdel.title=cookOtdel(page.otdel.raw)["title"];
  page.otdel.link=INFO.clientAdr+"otdel:"+page.inn.name+":"+page.otdel.raw;
  if(page.name=="otdel"){
    page.breadcrumbs=page.searchLink+
    "<a href='"+page.inn.link+"/'>"+page.inn.name+"</a><span>/</span>"+
    "<i>"+page.otdel.name+"</i>";} 
} if(page.deep > 3){//case page
  page.case.raw= page.pathCLean.split(':')[2];
  page.case.name=cookCase(page.case.raw)["name"]
  if(page.name=="case"){
    page.breadcrumbs= page.searchLink+
    "<a href='"+page.inn.link+"/'>"+page.inn.name+"</a><span>/</span>"+
    "<a href='"+page.otdel.link+"/'>"+page.otdel.name+"</a><span>/</span>"+
    "<i>"+page.case.name+"</i>";
  }
}

console.log(page);

//"PRAVO"-->"Правовой отдел"
function cookOtdel(rawOtdel){
  var data={};
  for (var i = 0, len = INFO.templ.otdel.length; i < len; i++) {
    if(rawOtdel==INFO.templ.otdel[i][0]){
      data.name =INFO.templ.otdel[i][1];
      data.title=INFO.templ.otdel[i][2];
    }
  }
  if(data=={}){console.log("error! wrong rawOtdel: " + rawOtdel);}
  return data;
}

//arbitr/PRAVO_ARBITRATION-->data={url:"arbitr", name: 'Арбитраж'}

function cookCase(rawCase){
  var data={};
  for (var i = 0, len = INFO.templ.case.length; i < len; i++) {
    if((rawCase==INFO.templ.case[i][0])||(rawCase==INFO.templ.case[i][1])){
      data.url =INFO.templ.case[i][0];
      data.name=INFO.templ.case[i][2];
    }
  }
  if(data=={}){console.log("error! wrong rawCase: " + rawCase);}
  return data;

  // var data={};
  //       if((rawCase=="arbitr")    ||(rawCase=="PRAVO_ARBITRATION")){
  //           data.url="arbitr";     data.name= 'Арбитраж';
  // }else if((rawCase=="landLease") ||(rawCase=="PRAVO_ISP_LAND_LEASE")){
  //           data.url="landLease";  data.name= 'Аренда земли';
  // }else if((rawCase=="roomRental")||(rawCase=="PRAVO_ISP_ROOM_RENTAL")){
  //           data.url="roomRental"; data.name= 'Аренда помещений';
  // }else if((rawCase=="general")   ||(rawCase=="PRAVO_CAS_SCHE_DES")){
  //           data.url="general";    data.name='Дела общей юрисдикции';
  // }else if((rawCase=="assigned")  ||(rawCase=="PRAVO_CAS_SCHED_VAC")){
  //           data.url="assigned";   data.name='Назначенные дела';
  // }else if((rawCase=="viewed")    ||(rawCase=="PRAVO_CASES_RREVIEW")){ 
  //           data.url="viewed";     data.name='Дела на рассмотрении';
  // }else{console.log("error! wrong rawCase: " + rawCase);}
  // return data;
}

//перевести дату в правильный формат
function fixDate(date , format){
  if(format=="long"){
      var dateOpt = {year: 'numeric', month: 'long', day: 'numeric', weekday: 'long'};
  }else if(format=="num"){
      var dateOpt = {year: 'numeric', month: 'numeric', day: 'numeric'};
  }else if(format=="weekday"){
      var dateOpt = {year: 'numeric', month: 'numeric', day: 'numeric',weekday: 'long'};
  }else{console.log("error: wrong date format: "+format)}
  return new Date(date).toLocaleString("ru", dateOpt);
}

$(function() {//после загрузки страницы
  $(".btnMenu, .menuClose, .menuShadow").click(function(){
      $('body').toggleClass('menuOpen');
  });
  $(".breadcrumbs").append(page.breadcrumbs)
});
