
//Проверка логина
function checkLoginInfo(login, pass) {
    $.ajax({
        url: INFO.clientAdr+"api/login/"+login+'.'+pass,
        type: "GET", contentType: "application/json",
        success: function (data) {
            console.log(data);
          if (data=="wrong"){$(".loginBlock").addClass('invalidCode');}
          if(data=="right"){
            $(".loginBlock").removeClass('invalidCode');
            Cookies.set('miusLogged', 'Logged');
            setTimeout(function(){
                window.location.href=INFO.clientAdr+"search"
              },200);
            }else{$(".loginBlock").addClass('invalidCode');}
            console.log(data + " pass");
         },
         error:function(data){
             console.log("errord+"+data)
          $(".loginBlock").addClass('invalidCode');
         }

    });
}

//Обработчик инпут логина
$('#inputLogin').keyup(function(){
  if(!$("#inputLogin").val()){$(".loginWrap").addClass('empty')}else{
      $(".loginWrap").removeClass('empty');}
})

//Обработчик инпут пасса
$('#inputPass').keyup(function(){
  if(!$("#inputPass").val()){$(".passWrap").addClass('empty')}else{
      $(".passWrap").removeClass('empty');}
})

//Обработчик сабмита
$(".loginForm .btn").click(function(e){
    e.preventDefault();
    var inputLogin  = $("#inputLogin").val();
    var inputPass = $("#inputPass").val();
  if(!$("#inputLogin").val()){$(".loginWrap").addClass('empty')}else{
      $(".loginWrap").removeClass('empty');}
  if(!$("#inputPass").val()){$(".passWrap").addClass('empty')}else{
      $(".passWrap").removeClass('empty');}
    checkLoginInfo(inputLogin, inputPass);
})
