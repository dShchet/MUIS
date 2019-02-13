using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;

namespace SharpServer {
    class Client {
        //Обработать запрос
        private void processReq(TcpClient Client, string reqType, Dictionary<string, string> Dict) {
            if ((Dict.ContainsKey("need")) && (Dict["need"] == "search")) {
                Console.WriteLine("[" + reqType + "]" + "processing Req");
                Stopwatch swTotal = Stopwatch.StartNew();

                string json = EOSSearch(Dict);

                swTotal.Stop();
                Console.WriteLine("TtS: {0}", swTotal.Elapsed.TotalMilliseconds.ToString().Split(',')[0]);
                SendResp(Client, 200, "application / json", json, reqType);
            } else if ((Dict.ContainsKey("need")) && (Dict["need"] == "one")) {
                if (Dict.ContainsKey("isn") && Dict.ContainsKey("rcType")) {
                    string isnTemp = Dict["isn"];
                    string rcType = Dict["rcType"];
                    int isn = System.Convert.ToInt32(isnTemp);
                    Stopwatch swTotal = Stopwatch.StartNew();

                    string origJson = EOSOneGet(isn, rcType);

                    string returnJson = origJson.Replace("{,", "{").Replace(Environment.NewLine, " ");
                    swTotal.Stop();
                    Console.WriteLine("TtO: {0}", swTotal.Elapsed.TotalMilliseconds.ToString().Split(',')[0]);
                    SendResp(Client, 200, "application / json", returnJson, reqType);
                } else {SendError(Client, "error: incorrect getOne", reqType);}
            } else if ((Dict.ContainsKey("need")) && (Dict["need"] == "lib")) {
                if (Dict.ContainsKey("isn") && Dict.ContainsKey("type")) {
                    string isnTemp = Dict["isn"];
                    string type = Dict["type"];
                    int isn = System.Convert.ToInt32(isnTemp);
                    Stopwatch swTotal1 = Stopwatch.StartNew();

                    string returnJson = EOSLib(isn, type);

                    swTotal1.Stop();
                    Console.WriteLine("TtL: {0}", swTotal1.Elapsed.TotalMilliseconds.ToString().Split(',')[0]);
                    SendResp(Client, 200, "application / json", returnJson, reqType);
                } else {SendError(Client, "error: incorrect getLib", reqType);}
            } else {SendError(Client, "error: incorrect params in request", reqType);}
        }

        //Отправить запрос
        private void SendResp(TcpClient Client, int Code, string format, string bigJson, string reqType) {
            try {

                UTF8Encoding utf8 = new UTF8Encoding();
                string Headers = "HTTP/1.1 " + Code + " \nContent-Type: " + format + " \nAccess-Control-Allow-Origin: *" +
                    " \nAccess-Control-Allow-Headers: Content-Type" +
                    " \nAccess-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS" +
                    "\n\n";
                byte[] HeadersBuffer = utf8.GetBytes(Headers);
                Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
                bigJson = bigJson.Replace("{,", "{").Replace(",}", "}").Replace("[,", "[").Replace("0:00:00", "");
                Console.WriteLine("[" + reqType + "]" + "sending json :" + bigJson);
                byte[] jsonBuffer = utf8.GetBytes(bigJson);
                Client.GetStream().Write(jsonBuffer, 0, jsonBuffer.Length);
                Client.Close();
            } catch {
                Console.WriteLine("[" + reqType + "]" + "Connection closed: Exception with SendResp");
                Client.Close();
            }
        }

        //Отправить ошибку
        private void SendError(TcpClient Client, string errorText, string reqType) {
            Console.WriteLine("[" + reqType + "]" + errorText);
            SendResp(Client, 400, "application / text", "[{\"error\":" + errorText + "}]", reqType);

        }
        class SearchRow {
            public int ISN;
            public string RegNum, DocDate, Contents, DOCKIND;
            
            public CORRESP    corresp    = new CORRESP();
            public AUTHOR     author     = new AUTHOR();
            public PERSONSIGN personsign = new PERSONSIGN();
            public class CORRESP {
                public string ORGANIZ_NAME, OUTNUM, OUTDATE, SIGN;
            }
            public class AUTHOR {
                public string CITIZEN_NAME, CITIZEN_CITY;
            }
            public class PERSONSIGN {
                public string WHO_SIGN_NAME;
            }
            public void processData(dynamic item) {
                try { ISN = item.ISN; } catch { }
                try { RegNum = item.RegNum; } catch { }
                try { DocDate = item.DocDate.ToString(); } catch { }
                try { Contents = item.Contents.Replace("\"", "&quot;"); } catch { }
                try { DOCKIND = item.DOCKIND; } catch { DOCKIND = "ROUT"; }
                try {
                    if (DOCKIND == "RCIN") {
                        var currItem = item.CORRESP[0];
                        try { corresp.ORGANIZ_NAME= currItem.ORGANIZ.NAME.Replace("\"", "&quot;"); } catch { }
                        try { corresp.OUTNUM= currItem.OUTNUM.ToString(); } catch { }
                        try { corresp.OUTDATE= currItem.OUTDATE.ToString(); } catch { }
                        try { corresp.SIGN= currItem.SIGN.ToString().Replace("\"", "&quot;"); } catch { }
                    } else if (DOCKIND == "RCLET") {
                        var currItem = item.AUTHOR[0];
                        try { author.CITIZEN_NAME=currItem.CITIZEN.NAME.Replace("\"", "&quot;"); } catch { }
                        try { author.CITIZEN_CITY=currItem.CITIZEN.CITY.Replace("\"", "&quot;"); } catch { }
                    } else {
                        DOCKIND = "RCOUT";
                        try {
                            var currItem = item.PERSONSIGNS[0];
                            personsign.WHO_SIGN_NAME=currItem.WHO_SIGN.NAME.Replace("\"", "&quot;");
                        } catch { }
                    }
                } catch { }
            }
        };

        public string EOSSearch(Dictionary<string, string> Dict) {
            Type headType = Type.GetTypeFromProgID("Eapi.Head");//создать класса головных объектов}
            dynamic head = null;
            try {
                Console.WriteLine("Creating instance");
                head = Activator.CreateInstance(headType); //создать головной объект
                string name;
                if (1 == 1) { name = "tver"; } else { name = "сержантов1"; }
                if (!head.OpenWithParams(name, "123")) { // открытие соединения с параметрами логина и паролем                    
                    head = null;                        // OpenWithParamsEx 1-Сервер 2-Владелец 3-Логин 4-Пароль 
                    Console.WriteLine("head = null");
                }
            } catch (Exception) { head = null; }
            if (head == null) {//Обычно возникает если логин не верный
                Console.WriteLine("Не удалось установить соединение с БД ДЕЛО");
            };
            try {
                string Source;
                string dateFrom; //от
                string dateTo; //до
                if (Dict.ContainsKey("source")) { Source = Dict["source"]; } else { Source = "all"; }
                if (Dict.ContainsKey("dateFrom")) { dateFrom = Dict["dateFrom"]; } else { dateFrom = "01/01/1998"; }
                if (Dict.ContainsKey("dateTo")) { dateTo = Dict["dateTo"]; } else { dateTo = "01/01/2018"; }

                dynamic ResultSet = head.GetResultSet; //создание хранилища 
                ResultSet.Source = head.GetCriterion("Table");//SearchTables
                try {
                    Console.WriteLine("ResultSet.ERRCODE is " + ResultSet.ERRCODE + "\n"
                        + "ResultSet.ERRTEXT is " + ResultSet.ERRTEXT);
                } catch { Console.WriteLine("no error"); }
                try {
                    ResultSet.Source.Params["Rc.DocDate"] = dateFrom + ":" + dateTo;
                } catch (Exception ex) { Console.WriteLine(ex.GetType().FullName + "\n" + ex.Message); }
                try { ResultSet.Source.Params["Rc.DocGroup"] = Source; } catch (Exception ex) {
                    Console.WriteLine(ex.GetType().FullName + "\n" + ex.Message);
                }
                ResultSet.Fill();

                int ItemCnt = ResultSet.ItemCnt;
                Console.WriteLine("ItemCnt is " + ItemCnt.ToString());
                string json = "";
                List<SearchRow> SearchList = new List<SearchRow>();
                for (int i = 0; i < ItemCnt; i++) {
                    var item = ResultSet.Item(i);
                    SearchRow itemRow = new SearchRow();
                    itemRow.processData(item);
                    SearchList.Add(itemRow);
                }
                json = JsonConvert.SerializeObject(SearchList);
                return json;
            } catch (Exception) {
                head = null;
                return "";
            } finally {//закрыть соединение
                if (head != null && head.Active) { head.Close(); }
            }
        }

        public class OneGet {
            
            public Dictionary<string, string> DOCGROUP = new Dictionary<string, string>();
            public Dictionary<string, string> CARDREG = new Dictionary<string, string>();
            public Dictionary<string, string> CABREG = new Dictionary<string, string>();
            public Dictionary<string, string> SECURITY = new Dictionary<string, string>();
            public Dictionary<string, string> LINKREF = new Dictionary<string, string>();
            public Dictionary<string, string> RUBRIC = new Dictionary<string, string>();
            public Dictionary<string, string> ADDPROPSRUBRIC = new Dictionary<string, string>();
            public Dictionary<string, string> VALUEADDPROPSRUBRIC = new Dictionary<string, string>();
            public Dictionary<string, string> ADDRel = new Dictionary<string, string>();
            //List<ADDRel> ADDR = new List<ADDRel>();

            public int ISN;
            //public string REGNUM, SPECIMEN, DOCDATE, CONSIST, CONTENTS, ACCESSMODE, NOTE, ADDRESS_FLAG, ISCONTROL, PLANDATE, FACTDATE, DELTA, LINKCNT, RUBRICCNT, ADDPROPSRUBRICCNT, ADDRCNT,,,,,,,,

            public void processData(dynamic item) {

            }
        }
        public string EOSOneGet(int isn, string rcType) {
            Type headType = Type.GetTypeFromProgID("Eapi.Head");//создать класса головных объектов
            dynamic head = null;
            try {
                head = Activator.CreateInstance(headType);//создать головной объект
                string name;
                if (1 == 1) { name = "tver"; } else { name = "сержантов1"; }
                if (!head.OpenWithParams(name, "123")){ // открытие соединения с параметрами логина и паролем
                    head = null;                       // OpenWithParamsEx 1-Сервер 2-Владелец 3-Логин 4-Пароль
                }
            } catch (Exception) {head = null;}
            if (head == null) {//Обычно возникает если логин не верный
                Console.WriteLine("Не удалось установить соединение с БД ДЕЛО.");
            }
            try {
                dynamic item;
                if ((rcType == "RCIN") || (rcType == "RcIn")) {
                    item = head.GetRow("RcIn", isn);
                } else if ((rcType == "RCOUT") || (rcType == "RcOut")) {
                    item = head.GetRow("RcOut", isn);
                } else if ((rcType == "RCLET") || (rcType == "RcLet")) {
                    item = head.GetRow("RcLet", isn);
                } else {
                    Console.WriteLine("errror: wrong rcType");
                    item = head.GetRow("RcIn", isn);
                }
                string json = "";
                if (1 == 1) {
                    json += "[{";
                    try { json += "\"ISN\":\"" + item.ISN.ToString() + "\""; } catch (Exception ex) {
                        Console.WriteLine("nN ISN");
                        Console.WriteLine("\nMessage ---\n{0}", ex.Message);
                        Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink);
                        Console.WriteLine("\nSource ---\n{0}", ex.Source);
                        Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                        Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite);
                    }
                    try {
                        json += ",\"DOCGROUP\":{";
                        json += "\"ISN\":\"" + item.DOCGROUP.ISN.ToString() + "\"";
                        json += ",\"NAME\":\"" + item.DOCGROUP.NAME.ToString() + "\"";
                        json += "}";
                    } catch { Console.WriteLine("nN DOCGROUP"); }
                    try { json += ",\"REGNUM\":\"" + item.REGNUM.ToString() + "\""; } catch { Console.WriteLine("nN REGNUM"); }
                    try { json += ",\"SPECIMEN\":\"" + item.SPECIMEN.ToString() + "\""; } catch { Console.WriteLine("nN SPECIMEN"); }
                    try { json += ",\"DOCDATE\":\"" + item.DOCDATE.ToString() + "\""; } catch { Console.WriteLine("nN DOCDATE"); }
                    try { json += ",\"CONSIST\":\"" + item.CONSIST.ToString() + "\""; } catch { Console.WriteLine("nN CONSIST"); }
                    try { json += ",\"CONTENTS\":\"" + item.CONTENTS.ToString().Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN CONTENTS"); }
                    try {
                        json += ",\"CARDREG\":{";
                        json += "\"ISN\":\"" + item.CARDREG.ISN.ToString() + "\"";
                        json += ",\"NAME\":\"" + item.CARDREG.NAME.ToString() + "\"";
                        json += "}";
                    } catch { Console.WriteLine("nN CARDREG"); }
                    try {
                        json += ",\"CABREG\":{";
                        json += "\"ISN\":\"" + item.CABREG.ISN.ToString() + "\"";
                        json += ",\"NAME\":\"" + item.CABREG.NAME.ToString() + "\"";
                        json += "}";
                    } catch { Console.WriteLine("nN CABREG"); }
                    //Признак модели персониф
                    try { json += ",\"ACCESSMODE\":\"" + item.ACCESSMODE.ToString() + "\""; } catch { Console.WriteLine("nN ACCESSMODE"); }
                    try {
                        json += ",\"SECURITY\":{";
                        json += "\"ISN\":\"" + item.SECURITY.ISN.ToString() + "\"";
                        json += ",\"NAME\":\"" + item.SECURITY.NAME.ToString() + "\"";
                        json += "}";
                    } catch { Console.WriteLine("nN SECURITY"); }
                    try { json += ",\"NOTE\":\"" + item.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                    try { json += ",\"ADDRESS_FLAG\":\"" + item.ADDRESS_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESS_FLAG"); }
                    try { json += ",\"ISCONTROL\":\"" + item.ISCONTROL.ToString() + "\""; } catch { Console.WriteLine("nN ISCONTROL"); }
                    try { json += ",\"PLANDATE\":\"" + item.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("nN PLANDATE"); }
                    try { json += ",\"FACTDATE\":\"" + item.FACTDATE.ToString() + "\""; } catch { Console.WriteLine("nN FACTDATE"); }
                    try { json += ",\"DELTA\":\"" + item.DELTA.ToString() + "\""; } catch { Console.WriteLine("nN DELTA"); }
                    try {
                        json += ",\"LINKCNT\":\"" + item.LINKCNT.ToString() + "\"";
                        if (item.LINKCNT > 0) {
                            json += ",\"LINKREF\":[";
                            for (int i = 0; i < item.LINKCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.LINKREF[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                try { json += ",\"TYPELINK\":\"" + currItem.TYPELINK.NAME + "\""; } catch { Console.WriteLine("nN LINKREF.TYPELINK"); }
                                try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN LINKREF.ORDERNUM"); }
                                try { json += ",\"LINKINFO\":\"" + currItem.LINKINFO + "\""; } catch { Console.WriteLine("nN LINKREF.LINKINFO"); }
                                try { json += ",\"URL\":\"" + currItem.URL + "\""; } catch { Console.WriteLine("nN LINKREF.URL"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN LINKCNT"); }
                    try {
                        json += ",\"RUBRICCNT\":\"" + item.RUBRICCNT.ToString() + "\"";
                        if (item.RUBRICCNT > 0) {
                            json += ",\"RUBRIC\":[";
                            for (int i = 0; i < item.RUBRICCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.RUBRIC[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                try { json += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"INDEX\":\"" + currItem.INDEX + "\""; } catch { Console.WriteLine("nN INDEX  "); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN RUBRICCNT"); }
                    try {
                        json += ",\"ADDPROPSRUBRICCNT\":\"" + item.ADDPROPSRUBRICCNT.ToString() + "\"";
                        if (item.ADDPROPSRUBRICCNT > 0) {
                            json += ",\"ADDPROPSRUBRIC\":[";
                            for (int i = 0; i < item.ADDPROPSRUBRICCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.ADDPROPSRUBRIC[i];
                                try { json += "\"DOCGROUPCNT\":\"" + currItem.DOCGROUPCNT + "\""; } catch { Console.WriteLine("nN ADDPROPSRUBRIC DOCGROUPCNT"); }
                                try { json += ",\"UINAME\":\"" + currItem.UINAME + "\""; } catch { Console.WriteLine("nN ADDPROPSRUBRIC UINAME"); }
                                try { json += ",\"APINAME\":\"" + currItem.APINAME + "\""; } catch { Console.WriteLine("nN ADDPROPSRUBRIC APINAME"); }
                                json += "}";
                            }
                            json += "]";
                            json += ",\"VALUEADDPROPSRUBRIC\":[";
                            for (int i = 0; i < item.ADDPROPSRUBRICCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.VALUEADDPROPSRUBRIC[i]["11111"];
                                try { json += "\"DOCGROUPCNT\":\"" + currItem.DOCGROUPCNT + "\""; } catch { Console.WriteLine("nN VALUEADDPROPSRUBRIC DOCGROUPCNT"); }
                                try { json += ",\"UINAME\":\"" + currItem.UINAME + "\""; } catch { Console.WriteLine("nN VALUEADDPROPSRUBRIC UINAME"); }
                                try { json += ",\"APINAME\":\"" + currItem.APINAME + "\""; } catch { Console.WriteLine("nN VALUEADDPROPSRUBRIC APINAME"); }
                                json += "}";
                            }
                            json += "]";

                        }
                    } catch { Console.WriteLine("nN ADDPROPSRUBRICCNT"); }
                    try {
                        json += ",\"ADDRCNT\":\"" + item.ADDRCNT.ToString() + "\"";
                        if (item.ADDRCNT > 0) {
                            json += ",\"ADDR\":[";
                            for (int i = 0; i < item.ADDRCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.ADDR[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                try { json += ",\"REG_DATE\":\"" + currItem.REG_DATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                                try { json += ",\"CONSIST\":\"" + currItem.CONSIST + "\""; } catch { Console.WriteLine("nN RCONSIST"); }
                                try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                                try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN REG_ORDERNUM"); }
                                try { json += ",\"PERSON\":\"" + currItem.PERSON + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                                try { json += ",\"KINDADDR\":\"" + currItem.KINDADDR + "\""; } catch { Console.WriteLine("nN ADDR.KINDADDR"); }
                                try { json += ",\"NOTE\":\"" + currItem.NOTE + "\""; } catch { Console.WriteLine("nN ADDR.NOTE"); }
                                try { json += ",\"REG_N\":\"" + currItem.REG_N + "\""; } catch { Console.WriteLine("nN ADDR.REG_N"); }
                                try {
                                    json += ",\"DELIVERY\":{";
                                    try { json += "\"ISN\":\"" + currItem.DELIVERY.ISN + "\""; } catch { Console.WriteLine("nN ADDR.DELIVERY.ISN"); }
                                    try { json += ",\"NAME\":\"" + currItem.DELIVERY.NAME + "\""; } catch { Console.WriteLine("nN ADDR.DELIVERY.NAME"); }
                                    json += "}";
                                } catch { Console.WriteLine("nN ADDR.DELIVERY"); }
                                if (currItem.KINDADDR == "ORGANIZ") {
                                    json += ",\"ORGANIZ\":{";
                                    try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN ADDR.ORGANIZ.ADDRESSEE.ISN"); }
                                    try { json += ",\"POSTINDEX\":\"" + currItem.ADDRESSEE.POSTINDEX + "\""; } catch { Console.WriteLine("nN ADDRESSEE.POSTINDEX"); }
                                    try { json += ",\"CITY\":\"" + currItem.ADDRESSEE.CITY + "\""; } catch { Console.WriteLine("nN ADDRESSEE.CITY"); }
                                    try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN ADDRESSEE.NAME"); }
                                    try { json += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                    try { json += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL"); }
                                    json += "}";
                                }
                                if (currItem.KINDADDR == "CITIZEN") {
                                    json += ",\"CITIZEN\":{";
                                    try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nNADDRESSEE.ISN"); }
                                    try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN ADDRESSEE.NAME"); }
                                    try { json += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESSEE.ADDRESS"); }
                                    try { json += ",\"REGION\":\"" + currItem.ADDRESSEE.REGION.NAME + "\""; } catch { Console.WriteLine("nN ADDRESSEE.REGION.NAME"); }
                                    try { json += ",\"POSTINDEX\":\"" + currItem.ADDRESSEE.POSTINDEX + "\""; } catch { Console.WriteLine("nN ADDRESSEE.POSTINDEX"); }
                                    try { json += ",\"CITY\":\"" + currItem.ADDRESSEE.CITY + "\""; } catch { Console.WriteLine("nN ADDRESSEE.CITY"); }
                                    try { json += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN ADDRESSEE.EMAIL"); }
                                    json += "}";

                                }
                                if (currItem.KINDADDR == "DEPARTMENT") {
                                    json += ",\"DEPARTMENT\":{";
                                    try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN ADDRESSEE.ISN"); }
                                    try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN ADDRESSEE.NAME"); }
                                    try { json += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN ADDRESSEE.EMAIL"); }
                                    json += "}";
                                }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN ADDRCNT"); }
                    try {
                        json += ",\"FILESCNT\":\"" + item.FILESCNT.ToString() + "\"";
                        if (item.FILESCNT > 0) {
                            json += ",\"FILES\":[";
                            for (int i2 = 0; i2 < item.FILESCNT; i2++) {
                                if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                var currItem2 = item.FILES[i2];
                                try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN ISN"); }
                                try { json += ",\"NAME\":\"" + currItem2.NAME.ToString() + "\""; } catch { Console.WriteLine("nN Files.NAME"); }
                                try { json += ",\"DESCRIPT\":\"" + currItem2.DESCRIPT.ToString() + "\""; } catch { Console.WriteLine("nN Files.DESCRIPT"); }
                                try { json += ",\"CONTENTS\":\"" + currItem2.CONTENTS.ToString() + "\""; } catch { Console.WriteLine("nN Files.CONTENTS"); }
                                try { json += ",\"EDSCNT\":\"" + currItem2.EDSCNT.ToString() + "\""; } catch { Console.WriteLine("nN Files.EDSCNT"); }
                                try { json += ",\"EDS\":\"" + currItem2.EDS.ToString() + "\""; } catch { Console.WriteLine("nN Files.EDS"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN FILESCNT"); }
                    try {
                        json += ",\"JOURNACQCNT\":\"" + item.JOURNACQCNT.ToString() + "\"";
                        if (item.JOURNACQCNT > 0) {
                            json += ",\"JOURNACQ\":[";
                            for (int i2 = item.JOURNACQCNT; i2 > 0; i2--) {
                                if (i2 != item.JOURNACQCNT) { json += ",{"; } else { json += "{"; }
                                var currItem2 = item.JOURNACQ[i2];
                                try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                try { json += "\"EMPLOY\":\"" + currItem2.EMPLOY.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.ISN"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN JOURNACQCNT"); }
                    try { json += ",\"NUM_FLAG\":\"" + item.NUM_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN NUM_FLAG"); }
                    try {
                        json += ",\"PROTCNT\":\"" + item.PROTCNT.ToString() + "\"";
                        if (item.PROTCNT > 0) {
                            json += ",\"PROTOCOL\":[";
                            for (int i2 = 0; i2 < item.PROTCNT; i2++) {
                                if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                var currItem2 = item.PROTOCOL[i2];
                                try { json += "\"WHEN\":\"" + currItem2.WHEN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                try { json += "\"WHAT\":\"" + currItem2.WHAT.Replace("\"", "&quot;").ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.ISN"); }

                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN PROTCNT"); }
                    try { json += ",\"CARDVIEW\":\"" + item.CARDVIEW.ToString() + "\""; } catch { Console.WriteLine("nN CARDVIEW"); }
                    try { json += ",\"ALLRESOL\":\"" + item.ALLRESOL.ToString() + "\""; } catch { Console.WriteLine("nN ALLRESOL"); }
                    try {
                        try { json += ",\"RESOLCNT\":\"" + item.RESOLCNT.ToString() + "\""; } catch { json += ",\"RESOLCNT\":0"; }
                        if (item.RESOLCNT > 0) {
                            json += ",\"RESOLUTION\":[";
                            for (int i = 0; i < item.RESOLCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.RESOLUTION[i];
                                try { json += ",\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("ISN"); }
                                try { json += ",\"KIND\":\"" + currItem.KIND.ToString() + "\""; } catch { Console.WriteLine("WEIGHT"); }
                                try { json += ",\"ITEMNUM\":\"" + currItem.ITEMNUM.ToString() + "\""; } catch { Console.WriteLine("ITEMNUM"); }
                                try { json += ",\"AUTHOR_ISN\":\"" + currItem.AUTHOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                                try { json += ",\"AUTHOR_NAME\":\"" + currItem.AUTHOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                                try { json += ",\"SURNAME\":\"" + currItem.AUTHOR.SURNAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                                try { json += ",\"TEXT\":\"" + currItem.TEXT.ToString() + "\""; } catch { Console.WriteLine("TEXT"); }
                                try { json += ",\"RESOLDATE\":\"" + currItem.RESOLDATE.ToString() + "\""; } catch { Console.WriteLine("RESOLDATE"); }
                                try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("SENDDATE"); }
                                try { json += ",\"ACCEPTFLAG\":\"" + currItem.ACCEPTFLAG.ToString() + "\""; } catch { Console.WriteLine("ACCEPTFLAG"); }
                                try { json += ",\"ISCONTROL\":\"" + currItem.ISCONTROL.ToString() + "\""; } catch { Console.WriteLine("ISCONTROL"); }
                                try { json += ",\"ISPRIVATE\":\"" + currItem.ISPRIVATE.ToString() + "\""; } catch { Console.WriteLine("ISPRIVATE"); }
                                try { json += ",\"CANVIEW\":\"" + currItem.CANVIEW.ToString() + "\""; } catch { Console.WriteLine("CANVIEW"); }
                                try { json += ",\"ISCASCADE\":\"" + currItem.ISCASCADE.ToString() + "\""; } catch { Console.WriteLine("ISCASCADE"); }
                                try { json += ",\"PLANDATE\":\"" + currItem.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("PLANDATE"); }
                                try { json += ",\"FACTDATE\":\"" + currItem.FACTDATE.ToString() + "\""; } catch { Console.WriteLine("FACTDATE"); }
                                try { json += ",\"RESPRJPRIORITY\":\"" + currItem.RESPRJPRIORITY.NAME.ToString() + "\""; } catch { Console.WriteLine("nN RESPRJPRIORITY"); }
                                try { json += ",\"REPLYCNT\":\"" + currItem.REPLYCNT.ToString() + "\""; } catch { Console.WriteLine("n REPLYCNT"); }
                                if (currItem.REPLYCNT > 0) {
                                    json += ",\"REPLY\":[";
                                    for (int i2 = 0; i2 < currItem.REPLYCNT; i2++) {
                                        if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                        var currItem2 = currItem.REPLY[i2];
                                        try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REPLY.ISN"); }
                                        //try { json += ",\"DCODE\":\"" + currItem2.EXECUTOR.DCODE + "\""; } catch { Console.WriteLine("nN REGION"); }
                                        try { json += ",\"EXECUTOR_ISN\":\"" + currItem2.EXECUTOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REGION"); }
                                        try { json += ",\"EXECUTOR_NAME\":\"" + currItem2.EXECUTOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nN NAME"); }
                                        json += "}";
                                    }
                                    json += "]";
                                }
                                try {
                                    json += ",\"RESOLCNT\":\"" + currItem.RESOLCNT.ToString() + "\"";

                                } catch { Console.WriteLine("nRESOLCNT"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN RESOLCNT"); }
                    try {
                        try { json += ",\"JOURNALCNT\":\"" + item.JOURNALCNT.ToString() + "\""; } catch { json += ",\"JOURNALCNT\":0"; }
                        if (item.JOURNALCNT > 0) {
                            json += ",\"JOURNAL\":[";
                            for (int i = 0; i < item.JOURNALCNT; i++) {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.JOURNAL[i];
                                try { json += "\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                                try { json += ",\"ADDRESSEE_ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN REGION"); }
                                try { json += ",\"ADDRESSEE_NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"ORIGFLAG\":\"" + currItem.ORIGFLAG.ToString() + "\""; } catch { Console.WriteLine("nNORIGFLAG"); }
                                try { json += ",\"ORIGNUM\":\"" + currItem.ORIGNUM.ToString() + "\""; } catch { Console.WriteLine("nN ORIGNUM"); }
                                try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nNSENDDATE"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN JOURNALCNT"); }
                    try {
                        json += ",\"FORWARDCNT\":\"" + item.FORWARDCNT.ToString() + "\"";
                        if (item.FORWARDCNT > 0) {
                            json += ",\"FORWARD\":[";
                            for (int i2 = 0; i2 < item.FORWARDCNT; i2++) {
                                if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                var currItem2 = item.FORWARD[i2];
                                try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                try { json += ",\"ADR_ISN\":\"" + currItem2.ADDRESSEE.ISN.ToString() + "\""; } catch { }
                                try { json += ",\"ADR_NAME\":\"" + currItem2.ADDRESSEE.NAME.ToString() + "\""; } catch { }
                                try { json += ",\"USER_ISN\":\"" + currItem2.USER.ISN.ToString() + "\""; } catch { }
                                try { json += ",\"USER_NAME\":\"" + currItem2.USER.NAME.ToString() + "\""; } catch { }
                                try { json += ",\"SENDDATE\":\"" + currItem2.SENDDATE.ToString() + "\""; } catch { }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN FORWARDCNT"); }
                    try {
                        json += ",\"ADDPROPSCNT\":\"" + item.ADDPROPSCNT.ToString() + "\"";
                        if (item.ADDPROPSCNT > 0) {
                            json += ",\"ADDPROPS\":[";
                            for (int i2 = 0; i2 < item.ADDPROPSCNT; i2++) {
                                if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                var currItem2 = item.ADDPROPS[i2];
                                try { json += "\"DOCGROUPCNT\":\"" + currItem2.DOCGROUPCNT.ToString() + "\""; } catch { Console.WriteLine("nN DOCGROUPCNT"); }
                                try { json += ",\"UINAME\":\"" + currItem2.UINAME + "\""; } catch { Console.WriteLine("nNUINAME"); }
                                try { json += ",\"APINAME\":\"" + currItem2.APINAME + "\""; } catch { Console.WriteLine("nNAPINAME"); }
                                try { json += ",\"TYPE\":\"" + currItem2.TYPE + "\""; } catch { Console.WriteLine("nNTYPE"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    } catch { Console.WriteLine("nN ADDPROPSCNT"); }
                    try { json += ",\"VALUEADDPROPS\":\"" + item.VALUEADDPROPS.ToString() + "\""; } catch { Console.WriteLine("nN VALUEADDPROPS"); }
                    try { json += ",\"RESOL\":\"" + item.RESOL.ToString() + "\""; } catch { Console.WriteLine("nN RESOL"); }

                    if ((rcType == "RCIN") || (rcType == "RCLET")) {
                        try { json += ",\"DOCKIND\":\"" + item.DOCKIND.ToString() + "\""; } catch { Console.WriteLine("nN DOCKIND"); }
                        try {
                            json += ",\"ADDRESSESCNT\":\"" + item.ADDRESSESCNT.ToString() + "\"";
                            if (item.ADDRESSESCNT > 0) {
                                json += ",\"ADDRESSES\":[";
                                for (int i = 0; i < item.ADDRESSESCNT; i++) {
                                    if (i != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem = item.ADDRESSES[i];
                                    try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                    try { json += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                    json += "}";
                                }
                                json += "]";
                            }
                        } catch { Console.WriteLine("nN ADDRESSESCNT"); }
                        try {
                            json += ",\"DELIVERY\":{";
                            try { json += "\"ISN\":\"" + item.DELIVERY.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { json += ",\"NAME\":\"" + item.DELIVERY.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            json += "}";
                        } catch { Console.WriteLine("nN DELIVERY"); }

                        try { json += ",\"TELEGRAM\":\"" + item.TELEGRAM.ToString() + "\""; } catch { Console.WriteLine("nN TELEGRAM"); }
                        try {
                            if (rcType == "RCIN") {
                                if (item.CORRESPCNT > 0) {
                                    var cnt = 0;
                                    json += ",\"CORRESP\":[";
                                    for (int i = 0; i < item.CORRESPCNT; i++) {
                                        var currItem = item.CORRESP[i];
                                        if (currItem.KIND == 1) {
                                            cnt = cnt + 1;
                                            if (i != 0) { json += ",{"; } else { json += "{"; }
                                            try { json += "\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.ISN"); }
                                            try { json += ",\"KIND\":\"" + currItem.KIND + "\""; } catch { Console.WriteLine("nN CORRESP.KIND"); }
                                            try { json += ",\"ORGANIZ_ISN\":\"" + currItem.ORGANIZ.ISN.ToString() + "\""; } catch { Console.WriteLine("nN ORGANIZ.ISN"); }
                                            try { json += ",\"ORGANIZ_NAME\":\"" + currItem.ORGANIZ.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN ORGANIZ.NAME"); }
                                            try { json += ",\"OUTNUM\":\"" + currItem.OUTNUM.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.OUTNUM"); }
                                            try { json += ",\"OUTDATE\":\"" + currItem.OUTDATE.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.OUTDATE"); }
                                            try { json += ",\"SIGN\":\"" + currItem.SIGN.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.SIGN"); }
                                            try { json += ",\"CONTENTS\":\"" + currItem.CONTENTS.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.CONTENTS"); }
                                            try { json += ",\"NOTE\":\"" + currItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.NOTE"); }
                                            json += "}";
                                        }

                                    }
                                    json += "]";
                                    json += ",\"CORRESPCNT\":\"" + cnt + "\"";
                                }
                            } else {
                                json += ",\"CORRESPCNT\":\"" + item.CORRESPCNT.ToString() + "\"";
                                if (item.CORRESPCNT > 0) {
                                    json += ",\"CORRESP\":[";
                                    for (int i = 0; i < item.CORRESPCNT; i++) {
                                        if (i != 0) { json += ",{"; } else { json += "{"; }
                                        var currItem = item.CORRESP[i];
                                        try { json += "\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.ISN"); }
                                        try { json += ",\"KIND\":\"" + currItem.KIND + "\""; } catch { Console.WriteLine("nN CORRESP.KIND"); }
                                        try { json += ",\"ORGANIZ_ISN\":\"" + currItem.ORGANIZ.ISN.ToString() + "\""; } catch { Console.WriteLine("nN ORGANIZ.ISN"); }
                                        try { json += ",\"ORGANIZ_NAME\":\"" + currItem.ORGANIZ.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN ORGANIZ.NAME"); }
                                        try { json += ",\"OUTNUM\":\"" + currItem.OUTNUM.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.OUTNUM"); }
                                        try { json += ",\"OUTDATE\":\"" + currItem.OUTDATE.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.OUTDATE"); }
                                        try { json += ",\"SIGN\":\"" + currItem.SIGN.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.SIGN"); }
                                        try { json += ",\"CONTENTS\":\"" + currItem.CONTENTS.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.CONTENTS"); }
                                        try { json += ",\"NOTE\":\"" + currItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN CORRESP.NOTE"); }
                                        json += "}";
                                    }
                                    json += "]";
                                }
                            }
                        } catch { Console.WriteLine("nN CORRESPCNT"); }
                        try { json += ",\"LINKS\":\"" + item.LINKS.ToString() + "\""; } catch { Console.WriteLine("nN LINKS"); }
                    }
                    if (rcType == "RCLET") {
                        try { json += ",\"ISCOLLECTIVE\":\"" + item.ISCOLLECTIVE.ToString() + "\""; } catch { Console.WriteLine("nN ISCOLLECTIVE"); }
                        try { json += ",\"ISANONIM\":\"" + item.ISANONIM.ToString() + "\""; } catch { Console.WriteLine("nN ISANONIM"); }
                        try {
                            json += ",\"AUTHORCNT\":\"" + item.AUTHORCNT.ToString() + "\"";
                            if (item.AUTHORCNT > 0) {
                                json += ",\"AUTHOR\":[";
                                for (int i = 0; i < item.AUTHORCNT; i++) {
                                    if (i != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem = item.AUTHOR[i];
                                    try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN AUTHOR.ORGANIZ.ISN"); }
                                    try { json += ",\"CITIZEN_ISN\":\"" + currItem.CITIZEN.ISN + "\""; } catch { Console.WriteLine("nN AUTHOR.CITIZEN.ISN"); }
                                    try { json += ",\"CITIZEN_NAME\":\"" + currItem.CITIZEN.NAME + "\""; } catch { Console.WriteLine("nN AUTHOR.CITIZEN.NAME"); }
                                    try { json += ",\"CITIZEN_ADDRESS\":\"" + currItem.CITIZEN.ADDRESS + "\""; } catch { Console.WriteLine("nN AUTHOR.CITIZEN.ADDRESS"); }
                                    try { json += ",\"CITIZEN_REGION\":\"" + currItem.CITIZEN.REGION + "\""; } catch { Console.WriteLine("nN AUTHOR.CITIZEN.REGION"); }
                                    try { json += ",\"CITIZEN_CITY\":\"" + currItem.CITIZEN.CITY + "\""; } catch { Console.WriteLine("nN AUTHOR.CITIZEN.CITY"); }
                                    try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.OUTNUM"); }
                                    try { json += ",\"DATE_CR\":\"" + currItem.DATE_CR.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.DATE_CR"); }
                                    try { json += ",\"DATE_UPD\":\"" + currItem.DATE_UPD.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.NOTE"); }
                                    json += "}";
                                }
                                json += "]";
                            }
                        } catch { Console.WriteLine("nN CORRESPCNT"); }
                    }
                    if (rcType == "RCOUT") {
                        try {
                            json += ",\"PERSONSIGNSCNT\":\"" + item.PERSONSIGNSCNT.ToString() + "\"";
                            if (item.PERSONSIGNSCNT > 0) {
                                json += ",\"PERSONSIGNS\":[";
                                for (int i = 0; i < item.PERSONSIGNSCNT; i++) {
                                    if (i != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem = item.PERSONSIGNS[i];
                                    try { json += ",\"WHO_SIGN_ISN\":\"" + currItem.WHO_SIGN.ISN + "\""; } catch { Console.WriteLine("nN PERSONSIGNS.WHO_SIGN_ISN"); }
                                    try { json += ",\"WHO_SIGN_NAME\":\"" + currItem.WHO_SIGN.NAME + "\""; } catch { Console.WriteLine("nN PERSONSIGNS.WHO_SIGN_NAME"); }
                                    try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN PERSONSIGNS.ORDERNUM"); }
                                    json += "}";
                                }
                                json += "]";
                            }

                        } catch { Console.WriteLine("nN PERSONSIGNSCNT"); }
                        try {
                            json += ",\"EXECUTOR\":{";
                            try { json += ",\"ISN\":\"" + item.EXECUTOR.ISN.ToString() + "\""; } catch { }
                            try { json += ",\"NAME\":\"" + item.EXECUTOR.NAME.ToString() + "\""; } catch { }
                            json += "}";
                        } catch { Console.WriteLine("nN EXECUTOR"); }
                        try {
                            json += ",\"COEXECCNT\":\"" + item.COEXECCNT.ToString() + "\"";
                            if (item.COEXECCNT > 0) {
                                json += ",\"COEXEC\":[";
                                for (int i = 0; i < item.COEXECCNT; i++) {
                                    if (i != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem = item.COEXEC[i];
                                    try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { }
                                    try { json += ",\"ORGANIZ_ISN\":\"" + currItem.ORGANIZ.ISN + "\""; } catch { }
                                    try { json += ",\"ORGANIZ_NAME\":\"" + currItem.ORGANIZ.NAME + "\""; } catch { }
                                    try { json += ",\"OUTNUM\":\"" + currItem.OUTNUM + "\""; } catch { }
                                    try { json += ",\"OUTDATE\":\"" + currItem.OUTDATE.ToString() + "\""; } catch { }
                                    try { json += ",\"SIGN\":\"" + currItem.SIGN + "\""; } catch { }
                                    json += "}";
                                }
                                json += "]";
                            }
                        } catch { Console.WriteLine("nN COEXECCNT"); }
                        try {
                            json += ",\"VISACNT\":\"" + item.VISACNT.ToString() + "\"";
                            if (item.VISACNT > 0) {
                                json += ",\"VISA\":[";
                                for (int i = 0; i < item.VISACNT; i++) {
                                    if (i != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem = item.VISA[i];
                                    try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                    try { json += ",\"EMPLOY_ISN\":\"" + currItem.EMPLOY.ISN + "\""; } catch { }
                                    try { json += ",\"EMPLOY_NAME\":\"" + currItem.EMPLOY.NAME + "\""; } catch { }
                                    try { json += ",\"NOTE\":\"" + currItem.NOTE + "\""; } catch { }
                                    try { json += ",\"DATE\":\"" + currItem.DATE + "\""; } catch { }
                                    json += "}";
                                }
                                json += "]";
                            }
                        } catch { Console.WriteLine("nN VISACNT"); }
                        try { json += ",\"LINKS\":\"" + item.LINKS.ToString() + "\""; } catch { Console.WriteLine("nN LINKS"); }
                    }
                    json += "}]";
                    return json;
                } else {
                    OneGet itemDoc = new OneGet();
                    itemDoc.processData(item);
                    json = JsonConvert.SerializeObject(itemDoc);
                    return json;
                }
                

            } catch (Exception) {
                head = null;
                return "";
            } finally {
                if (head != null && head.Active) {
                    head.Close();//закрыть соединение

                }
            }
        }

        public string EOSLib(int isn, string type) {
            Type headType = Type.GetTypeFromProgID("Eapi.Head");//создать класса головных объектов
            dynamic head = null;

            try {
                head = Activator.CreateInstance(headType);//создать головной объект
                string name;
                if (1 == 1) { name = "tver"; } else { name = "сержантов1"; }
                if (!head.OpenWithParams(name, "123")) // открытие соединения с параметрами логина и паролем
                                                       // OpenWithParamsEx 1-Сервер 2-Владелец 3-Логин 4-Пароль 
                    head = null;
                //}
                //else if (!head.Open())
                //    head = null;
            } catch (Exception) {
                head = null;
            }

            if (head == null) {
                Console.WriteLine(//Обычно возникает если логин не верный
                "Не удалось установить соединение с БД ДЕЛО.\n" +
                "Хранимые процедуры доступны только на просмотр.");
            }
            try {
                string json = "";
                //json += "[{";
                if ((type == "org")) {
                    dynamic ResultSet = head.GetResultSet; //создание хранилища 
                    ResultSet.Source = head.GetCriterion("Vocabulary");
                    dynamic SearchVocab = ResultSet.Source;
                    SearchVocab.VOCABULARY = "Organiz";
                    SearchVocab.BASE = isn;
                    ResultSet.Fill();
                    if (ResultSet.ERRCODE == 0) {
                        try {
                            dynamic item = ResultSet[0];
                            json += "[{";
                            json += "\"Organiz\":{";
                            try { json += ",\"NAME\":\"" + item.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"FULLNAME\":\"" + item.FULLNAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"REGION\":\"" + item.REGION.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"INDEX\":\"" + item.POSTINDEX + "\""; } catch { }
                            try { json += ",\"CITY\":\"" + item.CITY.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"EMAIL\":\"" + item.EMAIL + "\""; } catch { }
                            try { json += ",\"NOTE\":\"" + item.NOTE.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"LAW\":\"" + item.LAW_ADDRESS.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"ADDR\":\"" + item.ADDRESS.Replace("\"", "&quot;") + "\""; } catch { }

                            try { json += ",\"INN\":\"" + item.INN + "\""; } catch { }
                            try { json += ",\"OKPO\":\"" + item.OKPO + "\""; } catch { }
                            json += "}";
                            json += "}]";
                        } catch { json = "[{error:'не найден такой элемент'}]"; }
                    }
                } else if ((type == "dep")) {
                    dynamic ResultSet = head.GetResultSet; //создание хранилища 
                    ResultSet.Source = head.GetCriterion("Vocabulary");
                    dynamic SearchVocab = ResultSet.Source;
                    SearchVocab.VOCABULARY = "Department";
                    SearchVocab.BASE = isn;
                    ResultSet.Fill();
                    if (ResultSet.ERRCODE == 0) {
                        try {
                            json += "[{";
                            dynamic item = ResultSet[0];
                            json += "\"Department\":{";
                            try { json += ",\"NAME\":\"" + item.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"SURNAME\":\"" + item.SURNAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"POST \":\"" + item.SURNAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"CARDINDEX\":\"" + item.CARDINDEX.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"INDEX\":\"" + item.INDEX.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"CABINET\":\"" + item.CABINET.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"NOTE\":\"" + item.NOTE.Replace("\"", "&quot;") + "\""; } catch { }
                            json += "}";
                            json += "}]";
                        } catch { json = "[{error:'не найден такой элемент'}]"; }
                    }
                } else if (type == "cit") {
                    dynamic ResultSet = head.GetResultSet; //создание хранилища 
                    ResultSet.Source = head.GetCriterion("Vocabulary");
                    dynamic SearchVocab = ResultSet.Source;
                    SearchVocab.VOCABULARY = "Citizen";
                    SearchVocab.BASE = isn;
                    ResultSet.Fill();
                    if (ResultSet.ERRCODE == 0) {
                        try {
                            json += "[{";
                            //json += ",\"ITEMCNT\":\"" + ResultSet.ITEMCNT + "\"";
                            if (ResultSet.ITEMCNT > 0) {
                                for (int i = 0; i < ResultSet.ITEMCNT; i++) {
                                    dynamic item = ResultSet[i];
                                    if (item.ISN == isn) {
                                        json += ",\"Citizen\":{";
                                        try { json += ",\"ISN\":\"" + item.ISN + "\""; } catch { }
                                        try { json += ",\"NAME\":\"" + item.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                                        try { json += ",\"CITY\":\"" + item.CITY + "\""; } catch { }
                                        try { json += ",\"POSTINDEX\":\"" + item.POSTINDEX + "\""; } catch { }
                                        try { json += ",\"REGION\":\"" + item.REGION.NAME + "\""; } catch { }
                                        try { json += ",\"ADDRESS\":\"" + item.ADDRESS + "\""; } catch { }
                                        try { json += ",\"PHONE\":\"" + item.PHONE + "\""; } catch { }
                                        try { json += ",\"INN\":\"" + item.INN + "\""; } catch { }
                                        try { json += ",\"SNILS\":\"" + item.SNILS + "\""; } catch { }
                                        try {
                                            json += ",\"STATUSCNT\":\"" + item.STATUSCNT.ToString() + "\"";
                                            if (item.STATUSCNT > 0) {
                                                json += ",\"STATUS\":[";
                                                for (int i2 = 0; i2 < item.STATUSCNT; i2++) {
                                                    try { json += ",\"" + item.STATUS[i2].NAME + "\""; } catch { Console.WriteLine("nN LINKREF.ORDERNUM"); }
                                                }
                                                json += "]";
                                            }
                                        } catch { Console.WriteLine("nN STATUSCNT"); }
                                        try { json += ",\"SEX\":\"" + item.SEX + "\""; } catch { }
                                        try { json += ",\"SERIES\":\"" + item.SERIES + "\""; } catch { }
                                        try { json += ",\"PASPORT\":\"" + item.PASPORT + "\""; } catch { }
                                        try { json += ",\"GIVEN\":\"" + item.GIVEN + "\""; } catch { }
                                        try { json += ",\"EMAIL\":\"" + item.EMAIL + "\""; } catch { }
                                        try { json += ",\"NOTE\":\"" + item.NOTE.Replace("\"", "&quot;") + "\""; } catch { }
                                        json += "}";
                                    }
                                }
                            }
                            json += "}]";
                        } catch { json = "[{error:'не найден такой элемент'}]"; }
                    }
                } else if ((type == "rub")) {
                    dynamic ResultSet = head.GetResultSet; //создание хранилища 
                    ResultSet.Source = head.GetCriterion("Vocabulary");
                    dynamic SearchVocab = ResultSet.Source;
                    SearchVocab.VOCABULARY = "Rubric";
                    SearchVocab.BASE = isn;
                    ResultSet.Fill();
                    if (ResultSet.ERRCODE == 0) {
                        try {
                            json += "[{";
                            dynamic item = ResultSet[0];
                            json += "\"Rubric\":{";
                            try { json += ",\"NAME\":\"" + item.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                            try { json += ",\"INDEX\":\"" + item.INDEX + "\""; } catch { }
                            try { json += ",\"NOTE\":\"" + item.NOTE.Replace("\"", "&quot;") + "\""; } catch { }
                            json += "}";
                            json += "}]";
                        } catch { json = "[{error:'не найден такой элемент'}]"; }
                    }
                } else if ((type == "doc")) {
                    dynamic ResultSet = head.GetResultSet; //создание хранилища 
                    ResultSet.Source = head.GetCriterion("Vocabulary");
                    dynamic SearchVocab = ResultSet.Source;
                    SearchVocab.VOCABULARY = "DocGroup";
                    SearchVocab.BASE = isn;
                    SearchVocab.Select = "OneLevel";
                    if (isn == 0) {SearchVocab.ItemType = "Node";}
                    ResultSet.Fill();
                    if (ResultSet.ERRCODE == 0) {
                        try {
                            json += "{";
                            for (int i = 0; i < ResultSet.ItemCnt; i++) {
                                var item = ResultSet.Item(i);
                                if (i != 0) { json += ",\"" + item.ISN + "\":{"; } else { json += "\"" + item.ISN + "\":{"; }
                                try { json += ",\"NAME\":\"" + item.NAME.Replace("\"", "&quot;") + "\""; } catch { }
                                try { json += ",\"ISN\":\"" + item.ISN + "\""; } catch { }
                                try { json += ",\"DCODE\":\"" + item.DCODE + "\""; } catch { }
                                try { json += ",\"ISNODE\":\"" + item.ISNODE.ToString() + "\""; } catch { }
                                try { json += ",\"ParentIsn\":\"" + isn + "\""; } catch { }
                                json += "}";
                            }
                            //json += "}";
                            json += "}";
                        } catch { Console.WriteLine("errror:someWrong"); }
                    } else { Console.WriteLine(ResultSet.ERRTEXT); }
                } else {
                    Console.WriteLine("errror: wrong type");
                }
                return json;

            } catch (Exception) {
                head = null;
                return "";
            } finally {
                if (head != null && head.Active) {
                    head.Close();//закрыть соединение

                }
            }
        }

        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient Client) {
            string Request = "";
            byte[] Buffer = new byte[1024];
            int Count;
            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0) {
                Request += Encoding.ASCII.GetString(Buffer, 0, Count);
                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096) {
                    break;
                }
            }
            Console.WriteLine("-----------------------------");
            string[] RequestArr = Request.Split(' ');
            string reqType = Request.Split(' ')[0];
            try {
                string paramsString = Request.Split(' ')[1];

                if ((paramsString != "/") && (RequestArr.Length > 1)) {
                    Console.WriteLine("[" + reqType + "]" + " paramsString [" + paramsString + "]");

                    var matches = Regex.Matches(paramsString, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
                    Dictionary<string, string> Dict = matches.Cast<Match>().ToDictionary(
                        m => Uri.UnescapeDataString(m.Groups[2].Value),
                        m => Uri.UnescapeDataString(m.Groups[3].Value)
                    );

                    if (Dict.ContainsKey("need")) {
                        processReq(Client, reqType, Dict);
                    } else { SendError(Client, "Error: not exist paramsArr['need']", reqType); }
                } else { SendError(Client, "Error with Request, there is no params", reqType); }
            } catch { SendError(Client, "Error with Request", reqType); }
        }

    }

    class Server {
        TcpListener Listener; // Объект, принимающий TCP-клиентов

        // Запуск сервера
        public Server(int Port) {
            Listener = new TcpListener(IPAddress.Any, Port); // Создаем "слушателя" для указанного порта
            Listener.Start(); // Запускаем его

            // В бесконечном цикле
            while (true) {
                TcpClient Client = Listener.AcceptTcpClient();
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                Thread.Start(Client);

            }
        }

        static void ClientThread(Object StateInfo) {
            new Client((TcpClient)StateInfo);
        }

        // Остановка сервера
        ~Server() {
            Console.WriteLine("S_ 04");
            // Если "слушатель" был создан
            if (Listener != null) {
                // Остановим его
                Listener.Stop();
            }
        }

        static void Main(string[] args) {
            Console.WriteLine("S_ Server is running on port 7788");
            new Server(7788);
        }
    }
}

