;
const INFO={
  serverAdr:"http://localhost:8080/",
  clientAdr:"http://localhost:8080/"
};

var logged = Cookies.get('miusLogged');
if(logged!='Logged'){
    var loginUrl=INFO.serverAdr;
    window.location.replace(loginUrl);
}

$(function() {
  $(".btnMenu, .menuClose, .menuShadow").click(function(){
      $('body').toggleClass('menuOpen');
    });
  });