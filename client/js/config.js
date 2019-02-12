;
// var DEBUG=true;
var DEBUG=false;
const INFO={}
INFO.serverAdr= "http://localhost:8080/";
INFO.deloAdr  = "http://localhost:7788/";
INFO.clientAdr= "http://localhost:8080/";
INFO.templ={};
INFO.templ.case=[
    ["arbitr"    ,"PRAVO_ARBITRATION"    ,'Арбитраж'],
    ["landLease" ,"PRAVO_ISP_LAND_LEASE" ,'Аренда земли'],
    ["roomRental","PRAVO_ISP_ROOM_RENTAL",'Аренда помещений'],
    ["general"   ,"PRAVO_CAS_SCHE_DES"   ,'Дела общей юрисдикции'],
    ["assigned"  ,"PRAVO_CAS_SCHED_VAC"  ,'Назначенные дела'],
    ["viewed"    ,"PRAVO_CASES_RREVIEW"  ,'Дела на рассмотрении']
];

INFO.templ.otdel=[
    ["PRAVO","Правовой отдел",'Правовом отделе'],
    ["KEZO" ,"КИЗО" ,'Комитете имущественных и земельных отношений']
  ];
