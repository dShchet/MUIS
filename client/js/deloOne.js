
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
    $.ajax({
        url: INFO.deloAdr+"?need="+"one"+"&isn="+isn,
        type: "GET", contentType: "application/json",
        success: function (data) {
            console.log(data);
            data=data[0];
            var row= ""+
                "<div class='result-row' data-rowid='" + data.ISN + "'>"+
                    "<div class='result-data'><b>" + data.RegNum + "</b>"+"<b style='float:right'>" + data.DocDate.split(' ')[0] + "</b>"+
                        "<p>" + data.Contents + "</p>"+
                    "</div>"+
                "</div>";
            $(".result").html(row);            
            
         },
         error: function (jqXHR, exception) {
            console.log("Ошибка: "+jqXHR+"; exception: "+exception);
            console.log(jqXHR);},
    });
}



