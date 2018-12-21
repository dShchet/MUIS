// $ npm install pm2 -g
// $ pm2 start server.js (<!-- $ pm2 stop $ pm2 restart -->)
// https://pm2.io

//CONFIG
var defaultPort=8080;
var dbConfig = {
  user: 'sa',
  password: 'kl193011',
  server: 'RUBICON',
  options: {
    encrypt: true,
    database: 'MUIS'
  }
};
var isUserASystAdmin = true;//change on false if you are not


//Initiallising node modules
var express = require("express");
var bodyParser = require("body-parser");
var sql = require("mssql");
var app = express();
app.use(bodyParser.json());

app.use('/lib/',                        express.static(__dirname + '/lib'));
app.use('*/lib/',                       express.static(__dirname + '/lib'));
app.use('/',                            express.static(__dirname + '/loginPage'));
app.use('/logout',                      express.static(__dirname + '/logoutPage'));
app.use('/choice',                      express.static(__dirname + '/choicePage'));
app.use('/search',                      express.static(__dirname + "/searchPage"));
app.use('/inn:innId',                   express.static(__dirname + '/innPage'));
app.use('/otdel:innId:otdelId',         express.static(__dirname + '/otdelPage'));
app.use('/delo:innId:otdelId:arbitrag', express.static(__dirname + '/deloPage'));

//CORS Middleware
app.use(function (req, res, next) {
  //Enabling CORS 
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, contentType,Content-Type, Accept, Authorization");
  next();
});

//Setting up server
var server = app.listen(process.env.PORT || defaultPort, function () {
  var port = server.address().port;
  console.log("App now running on port", port);
});


var sqlStreamOpen=true;
function sqlStreamWrap(res, query){
    if(sqlStreamOpen==false){
      console.log('open? '+sqlStreamOpen+' waiting '+query);
        setTimeout(function (){sqlStreamWrap(res, query)}, 50)
    }else{
      console.log('open? '+sqlStreamOpen+' go '+query);
        sqlStreamOpen=false;
        executeQuery(res, query);
    }
}

var mainBase=dbConfig.options.database;
if(isUserASystAdmin){base='USE ['+ mainBase +'] '} else { base = "" }


//GG876RAO_FranVV
//Function to connect to database and execute query
var executeQuery = function (res, query) {
  sql.connect(dbConfig, function (err) {
    if (err) {
      console.log("Error while connecting database :- " + err);
      res.send(err);sqlStreamOpen=true;
    } else {
      // create Request object
      var request = new sql.Request();
      // query to the database
      request.query(query, function (err, result) {
        if (err) {
          res.send(err);
          console.log("Error while querying database :- " + err);
          sql.close();sqlStreamOpen=true;
        } else {
          res.send(result.recordset);
          console.log("executed Query");
          sql.close();sqlStreamOpen=true;
        }
      });
    }
  });
}

//GET ALL
// http://localhost:8080/api/users
app.get("/api/users", function (req, res) {
  // var query = "select * from [user]";
  var query = base+"SELECT TOP 100 [INN], [NAME_SHORT], [NAME_FULL], [LEGAL_ADDR], [TELEPHON], [Е_MAIL], [KPP], [BANK], [BANK_ACCOUNT], [KOR_ACCOUNT], [BIK], [OGRN], [BOSS]  FROM ["+mainBase+"].[dbo].[REGCARD]"
  sqlStreamWrap(res, query);
  console.log("GET ALL");
});

//GET Filter INN
// http://localhost:8080/api/filter/777
app.get("/api/filter/:innId.:nameId", function (req, res) {
  // var query = "select * from [user]";
  var innId  = req.params["innId"];
  var nameId  = req.params["nameId"];
  if((innId!="ALL")&&(nameId!="ALL")){
    var query = base+"SELECT TOP 100 [INN], [NAME_SHORT], [NAME_FULL], [LEGAL_ADDR], [TELEPHON], [Е_MAIL], [KPP], [BANK], [BANK_ACCOUNT], [KOR_ACCOUNT], [BIK], [OGRN], [BOSS]  FROM ["+mainBase+"].[dbo].[REGCARD] WHERE [NAME_FULL] LIKE '%"+ nameId +"%'" + "AND [INN] LIKE"+ "'%"+ innId+"%'";
    console.log("GET ALL");
  }
  if(innId=="ALL"){
    var query = base+"SELECT TOP 100 [INN], [NAME_SHORT], [NAME_FULL], [LEGAL_ADDR], [TELEPHON], [Е_MAIL], [KPP], [BANK], [BANK_ACCOUNT], [KOR_ACCOUNT], [BIK], [OGRN], [BOSS]  FROM ["+mainBase+"].[dbo].[REGCARD] WHERE [NAME_FULL] LIKE '%"+ nameId +"%'";
    console.log("GET by Name");
  }
  if(nameId=="ALL"){
    var query = base+"SELECT TOP 100 [INN], [NAME_SHORT], [NAME_FULL], [LEGAL_ADDR], [TELEPHON], [Е_MAIL], [KPP], [BANK], [BANK_ACCOUNT], [KOR_ACCOUNT], [BIK], [OGRN], [BOSS]  FROM ["+mainBase+"].[dbo].[REGCARD] WHERE [INN] LIKE '%"+ innId +"%'";
    console.log("GET by INN");
  }
  if((innId=="ALL")&&(nameId=="ALL")){
    var query = base+"SELECT TOP 100 [INN], [NAME_SHORT], [NAME_FULL], [LEGAL_ADDR], [TELEPHON], [Е_MAIL], [KPP], [BANK], [BANK_ACCOUNT], [KOR_ACCOUNT], [BIK], [OGRN], [BOSS]  FROM ["+mainBase+"].[dbo].[REGCARD]";
    console.log("GET ALL");
  }
  sqlStreamWrap(res, query);
  
});

//GET Filter Name
// http://localhost:8080/api/users
app.get("/api/filterName/:nameId", function (req, res) {
  // var query = "select * from [user]";
  var nameId  = req.params["nameId"];
  var query = base+"SELECT TOP 10 [INN] , [NAME_FULL] , [NAME_SHORT] , [LEGAL_ADDR] , [TELEPHON]  FROM  ["+mainBase+"].[dbo].[REGCARD] WHERE [NAME_SHORT] LIKE '%"+ nameId +"%'";
  sqlStreamWrap(res, query);
  console.log("GET Filter Data");
});

//GET One
// http://localhost:8080/api/user/29
app.get("/api/user/:userId", function (req, res) {
  // var query = "select * from [user]";
  var userID  = req.params["userId"];
  var query = base+"SELECT TOP 10 [INN] , [NAME_FULL] , [NAME_SHORT] , [LEGAL_ADDR] , [TELEPHON]  FROM  ["+mainBase+"].[dbo].[REGCARD]  WHERE [INN] = '" + userID+"'";
  console.log("GET with ID: " + userID);
  sqlStreamWrap(res, query);
});

app.get("/data/:innId", function (req, res) {
  var innId  = req.params["innId"];
  var query = base+"SELECT * FROM [dbo].[GET_OTDEL] ('"+innId+"') ";
  // executeQuery(res, query);
  sqlStreamWrap(res, query);
});
app.get("/api/otdel/:otdelId", function (req, res) {
  var otdelId  = req.params["otdelId"];
  var query = base+"SELECT * FROM [dbo].[GET_OTDEL_PRAVO] ('"+otdelId+"') ";
  // console.log(query);
  sqlStreamWrap(res, query);
});

//Get case details by innID and category
// http://localhost:8080/api/delo/5036094403.arbtrg
app.get("/api/delo/:innId.:catId", function (req, res) {
  var innId  = req.params["innId"];
  var catId  = req.params["catId"];
  var table="";
  if(catId=='arbitr')     {table = "[GET_PRAVO_ARBITRATION]";}
  if(catId=='landLease')  {table = "[GET_PRAVO_ISP_LAND_LEASE]";}
  if(catId=='roomRental') {table = "[GET_PRAVO_ISP_ROOM_RENTAL]";}
  if(catId=='general')    {table = "[GET_PRAVO_GEN_JURISD]";}
  if(catId=='assigned')   {table = "[GET_PRAVO_CAS_SCHE_DES]";}
  if(catId=='viewed')     {table = "[GET_PRAVO_CASES_RREVIEW]";}
  if(table && innId){
    var query = base+"SELECT * FROM [dbo]."+table+" ('"+innId+"')";
    sqlStreamWrap(res, query);
  }else{
    console.log("noSuchData/api/delo: "+"category: " + catId+" innId: "+innId); 
    res.send('noSuchData')}
});

// LOGIN
app.get("/api/login/:login.:pass", function(req , res){
  var login = req.params["login"];
  var pass  = req.params["pass"];
  // if((login=="f")&&(pass=="n")){return res.send("right");
    // }else{res.send('wrong');}
  console.log('login with: '+login+" and pass: "+pass);
  var query = base+"SELECT TOP 100 [PAS_USER] FROM [dbo].[NAMEPAS] WHERE [NAM_USER] = '" + login+"'";
  function sqlStreamWrap_login(res, query){
    if(sqlStreamOpen==false){
        setTimeout(function (){sqlStreamWrap_login(res, query)}, 50)
    }else{
        sqlStreamOpen=false;
        executeQuery_login(res, query);
    }
  }
  var executeQuery_login = function (res, query) {
    sql.connect(dbConfig, function (err) {
      if (err) {
        console.log("Error while connecting database :- " + err);
        res.send(err);sqlStreamOpen=true;
      } else {
        var request = new sql.Request();
        request.query(query, function (err, result) {
          if (err) {
            console.log("Error while querying database :- " + err);
            res.send(err);sqlStreamOpen=true;
          } else {
            // res.send(result.recordset);
            if(result.recordset[0]&&result.recordset[0].PAS_USER){
              if(result.recordset[0].PAS_USER==pass){res.send("right")
            }else{res.send("wrong");}
            }
            sql.close();sqlStreamOpen=true;
          }
        });
      }
    });
  }
  sqlStreamWrap_login(res, query);
});

// //POST API
// // http://localhost:8080/api/add/29.UPERNAME.letleT.456.777
//  app.post("/api/add/:resId.:englId.:rusId.:typId.:spravId", function(req , res){
//     var INN        = req.params["resId"];
//     var NAME_FULL  = req.params["englId"];
//     var NAME_SHORT = req.params["rusId"];
//     var LEGAL_ADDR = req.params["typId"];
//     var TELEPHON   = req.params["spravId"];
//     var query = base+"INSERT INTO  ["+mainBase+"].[dbo].[REGCARD]  ([INN] ,[NAME_FULL] ,[NAME_SHORT] ,[LEGAL_ADDR] ,[TELEPHON]) VALUES ("+INN+", '"+NAME_FULL+"', '"+NAME_SHORT+"', "+LEGAL_ADDR+", "+TELEPHON+")";
//   sqlStreamWrap(res, query);
//     console.log("POST INSERT ");
// });

// // PUT API
// //http://localhost:8080/api/update/29.TTTT.letleT.55.767
//  app.post("/api/update/:resId.:englId.:rusId.:typId.:spravId", function(req , res){
//     var INN        = req.params["resId"];
//     var NAME_FULL  = req.params["englId"];
//     var NAME_SHORT = req.params["rusId"];
//     var LEGAL_ADDR = req.params["typId"];
//     var TELEPHON   = req.params["spravId"];
//     var query = base+"UPDATE  ["+mainBase+"].[dbo].[REGCARD]  SET [NAME_FULL] = '"+NAME_FULL+"' , [NAME_SHORT] = '"+NAME_SHORT+"' , [LEGAL_ADDR] = "+LEGAL_ADDR+" , [TELEPHON] = "+TELEPHON+" WHERE [INN] = "+ INN;
//   console.log(query);
//   sqlStreamWrap(res, query);
//     console.log("POST Update");
// });

// DELETE API
// http://localhost:8080/api/user/29
//  app.delete("/api/user/:id", function(req , res){
//   var query = "DELETE ["+mainBase+"].[dbo].[REGCARD] WHERE [INN] =" + req.params.id;
//   executeQuery (res, query);
//   console.log('DELETE with id: '+req.params.id);
// });
