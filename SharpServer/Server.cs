using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;

namespace SharpServer
{    
    class Client
    {
        //Обработать запрос
        private void processReq(TcpClient Client, string reqType, Dictionary<string, string> Dict)
        {
            if ((Dict.ContainsKey("need")) && (Dict["need"] == "search"))
            {
                Console.WriteLine("[" + reqType + "]" + "processing Req");
                string EOSsourceVal;// "Vocabulary"
                string EOSdateFrom; //от
                string EOSdateTo; //до

                if (Dict.ContainsKey("source")) { EOSsourceVal = Dict["source"]; } else { EOSsourceVal = "Table"; }
                if (Dict.ContainsKey("dateFrom")) { EOSdateFrom = Dict["dateFrom"]; } else { EOSdateFrom = "01/01/1998"; }
                if (Dict.ContainsKey("dateTo")) { EOSdateTo = Dict["dateTo"]; } else { EOSdateTo = "01/01/2018"; }
                string rngList = EOSSearch(EOSsourceVal, EOSdateFrom, EOSdateTo);
                SendResp(Client, 200, "application / json", rngList, reqType);
            }
            else if ((Dict.ContainsKey("need")) && (Dict["need"] == "one"))
            {
                if (Dict.ContainsKey("isn") && Dict.ContainsKey("rcType"))
                {
                    string isnTemp = Dict["isn"];
                    string rcType = Dict["rcType"];
                    int isn = System.Convert.ToInt32(isnTemp);
                    //Console.WriteLine("isn is " + "[" + isn + "]");
                    //Console.WriteLine("isn rcTypeTemp " + "[" + rcType + "]");
                    string returnJson = EOSOneGet(isn, rcType);
                    //Console.WriteLine("returnJson is " + "[" + returnJson + "]");
                    SendResp(Client, 200, "application / json", returnJson, reqType);
                } else {
                    SendError(Client, "error: incorrect getOne", reqType);
                }
                

            } else
            {
                SendError(Client, "error: incorrect params in request", reqType);
            }
        }
        
        //Отправить запрос
        private void SendResp(TcpClient Client, int Code, string format, string json, string reqType)
        {
            try
            {
                Console.WriteLine("[" + reqType + "]" + "trying send json :" + json);
                UTF8Encoding utf8 = new UTF8Encoding();
                string Headers = "HTTP/1.1 " + Code + " \nContent-Type: " + format + " \nAccess-Control-Allow-Origin: *"+
                    " \nAccess-Control-Allow-Headers: Content-Type"+
                    " \nAccess-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS" +
                    "\n\n";
                byte[] HeadersBuffer = utf8.GetBytes(Headers);
                Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
                byte[] jsonBuffer = utf8.GetBytes(json);
                Client.GetStream().Write(jsonBuffer, 0, jsonBuffer.Length);
                Client.Close();
            }
            catch {
                Console.WriteLine("[" + reqType + "]" + "Connection closed: Exception with SendResp");
                Client.Close();
            }
        }
        
        //Отправить ошибку
        private void SendError(TcpClient Client, string errorText, string reqType)
        {
            Console.WriteLine("[" + reqType + "]" + errorText);
            SendResp(Client, 400, "application / text", errorText, reqType);
            
        }

        public string EOSSearch(string Source, string dateFrom, string dateTo)
        {
            Type headType = Type.GetTypeFromProgID("Eapi.Head");//создать класса головных объектов
            dynamic head = null;

            try
            {
                head = Activator.CreateInstance(headType);//создать головной объект
                //string[] arg = Environment.GetCommandLineArgs();
                //if (arg.Length == 5)
                //{
                if (!head.OpenWithParams("tver", "123")) // открытие соединения с параметрами логина и паролем
                    // OpenWithParamsEx 1-Сервер 2-Владелец 3-Логин 4-Пароль 
                    head = null;
                //}
                //else if (!head.Open())
                //    head = null;
            }
            catch (Exception)
            {
                head = null;
            }

            if (head == null)
            {
                Console.WriteLine(//Обычно возникает если логин не верный
                "Не удалось установить соединение с БД ДЕЛО.\n" +
                "Хранимые процедуры доступны только на просмотр.");
            }
            try
            {
                dynamic ResultSet = head.GetResultSet; //создание хранилища 

                //Если необходимо получить информацию из справочников системы
                //ResultSet.Source = head.GetCriterion("Vocabulary");//SearchVocab
                //Если необходимо получить перечень документов или резолюций
                ResultSet.Source = head.GetCriterion(Source);//SearchTables

                //Задание критериев отбора (необходимо)
                ResultSet.Source.Params["Rc.DocDate"] = dateFrom + ":" + dateTo;//фильрация по дате
                ResultSet.Fill();//Выполнение SQL Запросов и запись данных

                int ItemCnt = ResultSet.ItemCnt;
                string superJson = "[";
                for (int i = 0; i < ItemCnt; i++)
                {
                    var item = ResultSet.Item(i);
                    if (i > 0) { superJson = superJson + ","; }
                    Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
                        try { tempDictionary.Add("ISN", item.ISN.ToString() ); } catch { }
                        try { tempDictionary.Add("RegNum", item.RegNum); } catch { }
                        try { tempDictionary.Add("DocDate", item.DocDate.ToString()); } catch { }
                        try { tempDictionary.Add("DOCKIND", item.DOCKIND.ToString()); } catch {
                                tempDictionary.Add("DOCKIND", "RCOUT");
                            }
                        try { tempDictionary.Add("Contents", item.Contents.Replace("\"", "&quot;")); } catch { }
                    try
                    {if (item.CORRESPCNT > 0)
                        {
                            string sign = "";
                            //try { sign += item.CORRESP[0].ORGANIZ.Replace("\"", "&quot;"); } catch { }
                            try { sign += /*" " +*/ item.CORRESP[0].SIGN; } catch { }
                            tempDictionary.Add("Sign", sign);
                        }
                    }
                    catch { }
                    var kvs = tempDictionary.Select(kvp => string.Format("\"{0}\":\"{1}\"", kvp.Key, string.Concat("", kvp.Value)));
                    string jsonChar = string.Concat("{", string.Join(",", kvs), "}");
                    superJson = superJson + jsonChar;
                    //SuperArr[i] = tempDictionary;
                }
                superJson = superJson + "]";
                return superJson;

            }
            catch (Exception)
            {
                head = null;
                return "";
            }
            finally
            {
                if (head != null && head.Active)
                {
                    head.Close();//закрыть соединение

                }
            }
        }

        public string EOSOneGet(int isn, string rcType)
        {
            Type headType = Type.GetTypeFromProgID("Eapi.Head");//создать класса головных объектов
            dynamic head = null;

            try
            {
                head = Activator.CreateInstance(headType);//создать головной объект
                //string[] arg = Environment.GetCommandLineArgs();
                //if (arg.Length == 5)
                //{
                if (!head.OpenWithParams("tver", "123")) // открытие соединения с параметрами логина и паролем
                    // OpenWithParamsEx 1-Сервер 2-Владелец 3-Логин 4-Пароль 
                    head = null;
                //}
                //else if (!head.Open())
                //    head = null;
            }
            catch (Exception)
            {
                head = null;
            }

            if (head == null)
            {
                Console.WriteLine(//Обычно возникает если логин не верный
                "Не удалось установить соединение с БД ДЕЛО.\n" +
                "Хранимые процедуры доступны только на просмотр.");
            }
            try
            {
                dynamic item;
                if (rcType == "RcIn")
                {
                    item = head.GetRow("RcIn", isn);
                }
                else if (rcType == "RCOUT")
                {
                    item = head.GetRow("RcOut", isn);
                }
                else if (rcType == "RCLET")
                {
                    item = head.GetRow("RcLet", isn);
                }
                else { Console.WriteLine("errror: wrong rcType");
                    item = head.GetRow("RcIn", isn);
                }

                    //Console.WriteLine(MyRcCOntainer.REGNUM);
                    //Console.WriteLine(MyRcCOntainer.DocDate.ToString());
                    //Console.WriteLine(MyRcCOntainer.Contents.Replace("\"", "&quot;"));
                    //Console.WriteLine(MyRcCOntainer.REGNUM);

                    //Console.WriteLine("still good3");
                    string superJson = "[";
                //string superJson = "[{\"everything\":\"is good\"}]";
                //superJson = "{\"evrething\":\"is good\"}";
                //for (int i = 0; i < ItemCnt; i++)
                //{
                superJson += "{";
                //superJson += "\"ISN\"" + ":" + "\"" + isn.ToString() + "\"" + ",";
                //superJson += "\"RegNum\"" + ":" + "\"" + MyRcCOntainer.RegNum + "\"" + ",";
                //superJson += "\"DocDate\"" + ":" + "\"" + MyRcCOntainer.DocDate.ToString() + "\"" + ",";
                //superJson += "\"Contents\"" + ":" + "\"" + MyRcCOntainer.Contents.Replace("\"", "&quot;") + "\"";
                //Console.WriteLine(MyRcCOntainer.CORRESP444);
                Console.WriteLine("MyRcCOntainer.CORRESP");
                //if (MyRcCOntainer.CORRESP[0].KIND == 1) {
                //var item = MyRcCOntainer.CORRESP[0];
                string DOCKIND;
                try { superJson += "\"ISN\":\"" + item.isn.ToString() + "\""; } catch { Console.WriteLine("nN ISN"); }
                try { DOCKIND = item.DOCKIND.ToString();  superJson += ",\"DOCKIND\":\"" + rcType + "\"";}
                catch
                {
                    DOCKIND = "RCOUT";
                    superJson += ",\"DOCKIND\":\"" + DOCKIND + "\"";
                }
                try { Console.WriteLine("DOCGROUP  " + item.DOCGROUP.ToString()); } catch { Console.WriteLine("nN DOCGROUP"); }
                try { superJson += ",\"REGNUM\":\"" + item.REGNUM.ToString() + "\""; } catch { Console.WriteLine("nN REGNUM"); }
                try { superJson += ",\"ORDERNUM\":\"" + item.ORDERNUM.ToString() + "\""; } catch { Console.WriteLine("nN ORDERNUM"); }
                try { superJson += ",\"SPECIMEN\":\"" + item.SPECIMEN.ToString() + "\""; } catch { Console.WriteLine("nN SPECIMEN"); }
                try { superJson += ",\"DOCDATE\":\"" + item.DOCDATE.ToString() + "\""; } catch { Console.WriteLine("nN DOCDATE"); }
                if (DOCKIND == "RCOUT") {
                    Console.WriteLine("PERSONSIGNSCNT now");
                    try { superJson += ",\"PERSONSIGN\":\"" + item.PERSONSIGN.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGNSCNT"); }

                    try { superJson += ",\"PERSONSIGNSCNT\":\"" + item.PERSONSIGNSCNT.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGNSCNT"); }

                    try { superJson += ",\"PERSONSIGNS\":\"" + item.PERSONSIGNS.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGNSCNT"); }
                    try { superJson += ",\"EXECUTOR\":\"" + item.PERSONSIGNS.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGNSCNT"); }
                    try { superJson += ",\"PERSONSIGNS\":\"" + item.PERSONSIGNS.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGNSCNT"); }
                    try
                    {
                        if (item.PERSONSIGNSCNT > 0)
                        {
                        superJson += ",\"PERSONSIGNS\":[";
                            for (int i = 0; i < item.PERSONSIGNSCNT; i++)
                            {
                                if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                                var currItem = item.PERSONSIGNS[i];
                                try {
                                    superJson += "\"WHO_SIGN\":{";
                                    try{ superJson += "\"ISN\":\"" + currItem.WHO_SIGN.ISN + "\""; } catch { }
                                    try{ superJson += ",\"NAME\":\"" + currItem.WHO_SIGN.NAME + "\"";}catch { }
                                    superJson += "}";
                                } catch { Console.WriteLine("nN ISN"); }
                                try { superJson += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN ORDERNUM"); }
                                superJson += "}";
                            }
                            superJson += "]";
                        }
                    }
                    catch { }

                }
                try
                {
                    superJson += ",\"EXECUTOR\":{";
                    try { superJson += "\"ISN\":\"" + item.EXECUTOR.ISN + "\""; } catch { }
                    try { superJson += ",\"NAME\":\"" + item.EXECUTOR.NAME + "\""; } catch { }
                    superJson += "}";
                }
                catch { Console.WriteLine("nN CARDCNT"); }
                try
                {
                    superJson += ",\"VISACNT\":\"" + item.VISACNT.ToString() + "\"";
                    if (item.VISACNT > 0)
                    {
                        superJson += ",\"VISA\":[";
                        for (int i = 0; i < item.VISACNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.VISA[i];
                            try { superJson += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            superJson += ",\"EMPLOY\":{";
                                try { superJson += "\"ISN\":\"" + currItem.EMPLOY.ISN + "\""; } catch { }
                                try { superJson += ",\"NAME\":\"" + currItem.EMPLOY.NAME + "\""; } catch { }
                            superJson += "}";
                            superJson += "}";
                        }
                        superJson += "]";
                    }
                }
                catch { Console.WriteLine("nN ADDRESSESCNT"); }

                try { superJson += ",\"CONSIST\":\"" + item.CONSIST.ToString() + "\""; } catch { Console.WriteLine("nN CONSIST"); }
                try { superJson += ",\"CONTENTS\":\"" + item.CONTENTS.ToString() + "\""; } catch { Console.WriteLine("nN CONTENTS"); }
                try { Console.WriteLine("CARDREG  " + item.CARDREG.ToString()); } catch { Console.WriteLine("nN CARDREG"); }
                try { Console.WriteLine("CABREG  " + item.CABREG.ToString()); } catch { Console.WriteLine("nN CABREG"); }
                try { superJson += ",\"ACCESSMODE\":\"" + item.ACCESSMODE.ToString() + "\""; } catch { Console.WriteLine("nN ACCESSMODE"); }
                try { Console.WriteLine("SECURITY  " + item.SECURITY.ToString()); } catch { Console.WriteLine("nN SECURITY"); }
                try { Console.WriteLine("ADDRESSEE  " + item.ADDRESSEE.ToString()); } catch { Console.WriteLine("nN ADDRESSEE"); }
                try {
                    superJson += ",\"ADDRESSESCNT\":\"" + item.ADDRESSESCNT.ToString() + "\"";
                    if (item.ADDRESSESCNT > 0)
                    {
                        superJson += ",\"ADDRESSES\":[";
                        for (int i = 0; i < item.ADDRESSESCNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.ADDRESSES[i];
                            try { superJson += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { superJson += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            try { superJson += ",\"SURNAME\":\"" + currItem.SURNAME + "\""; } catch { Console.WriteLine("nN SURNAME"); }
                            try { superJson += ",\"POST\":\"" + currItem.POST + "\""; } catch { Console.WriteLine("nN POST"); }
                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN ADDRESSESCNT"); }
                
                try { Console.WriteLine("DELIVERY  " + item.DELIVERY.ToString()); } catch { Console.WriteLine("nN DELIVERY"); }
                try { superJson += ",\"TELEGRAM\":\"" + item.TELEGRAM.ToString() + "\""; } catch { Console.WriteLine("nN TELEGRAM"); }
                try { superJson += ",\"NOTE\":\"" + item.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                try { superJson += ",\"ADDRESS_FLAG\":\"" + item.ADDRESS_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESS_FLAG"); }
                try { superJson += ",\"ISCONTROL\":\"" + item.ISCONTROL.ToString() + "\""; } catch { Console.WriteLine("nN ISCONTROL"); }
                try { superJson += ",\"PLANDATE\":\"" + item.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("nN PLANDATE"); }
                try { superJson += ",\"FACTDATE\":\"" + item.FACTDATE.ToString() + "\""; } catch { Console.WriteLine("nN FACTDATE"); }
                try { superJson += ",\"DELTA\":\"" + item.DELTA.ToString() + "\""; } catch { Console.WriteLine("nN DELTA"); }
                try {
                    superJson += ",\"CARDCNT\":\"" + item.CARDCNT.ToString() + "\"";
                    if (item.CARDCNT > 0)
                    {
                        superJson += ",\"CARD\":[";
                        for (int i = 0; i < item.CARDCNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.CARD[i];
                            try { superJson += "\"DATE\":\"" + currItem.DATE + "\""; } catch { Console.WriteLine("nN ISN"); }
                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN CARDCNT"); }
                try {
                    superJson += ",\"CORRESPCNT\":\"" + item.CORRESPCNT.ToString() + "\"";
                    if (item.CORRESPCNT > 0)
                    {
                        superJson += ",\"CORRESP\":[";
                        for (int i = 0; i < item.CORRESPCNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.CORRESP[i];
                            if (currItem.KIND == 1)
                            {
                                try { superJson += "\"ISN\":\"" + currItem.ORGANIZ.ISN + "\""; } catch { Console.WriteLine("nN ORGANIZ.ISN"); }
                                try { superJson += ",\"ORGANIZ\":\"" + currItem.ORGANIZ.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN ORGANIZ.NAME"); }
                                try { superJson += ",\"OUTNUM\":\"" + currItem.OUTNUM.ToString() + "\""; } catch { Console.WriteLine("nN OUTNUM"); }
                                try { superJson += ",\"OUTDATE\":\"" + currItem.OUTDATE.ToString() + "\""; } catch { Console.WriteLine("nN OUTDATE"); }
                                try { superJson += ",\"SIGN\":\"" + currItem.SIGN.ToString() + "\""; } catch { Console.WriteLine("nN SIGN"); }
                                try { superJson += ",\"NOTE\":\"" + currItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                            }
                            else { /*Вид записи: 1 – информация о Корреспонденте документа; 2 – информация о сопроводительном документа; Записи первого вида могут принадлежать только РК вида "Входящий" от организаций, второго вида – как "Входящим" от организаций так и "Письмам" от физических лиц.*/}

                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN CORRESPCNT"); }
                
                try { superJson += ",\"LINKCNT\":\"" + item.LINKCNT.ToString() + "\""; } catch { Console.WriteLine("nN LINKCNT"); }
                try { Console.WriteLine("LINKREF  " + item.LINKREF.ToString()); } catch { Console.WriteLine("nN LINKREF"); }
                try {
                    superJson += ",\"RUBRICCNT\":\"" + item.RUBRICCNT.ToString() + "\"";
                    if (item.RUBRICCNT > 0)
                    {
                        superJson += ",\"RUBRIC\":[";
                        for (int i = 0; i < item.RUBRICCNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.RUBRIC[i];
                            try { superJson += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { superJson += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN RUBRICCNT"); }
                try { superJson += ",\"ADDPROPSRUBRICCNT\":\"" + item.ADDPROPSRUBRICCNT.ToString() + "\""; } catch { Console.WriteLine("nN ADDPROPSRUBRICCNT"); }
                try { Console.WriteLine("ADDPROPSRUBRIC  " + item.ADDPROPSRUBRIC.ToString()); } catch { Console.WriteLine("nN ADDPROPSRUBRIC"); }
                try { superJson += ",\"VALUEADDPROPSRUBRIC\":\"" + item.VALUEADDPROPSRUBRIC.ToString() + "\""; } catch { Console.WriteLine("nN VALUEADDPROPSRUBRIC"); }
                try {
                    superJson += ",\"ADDRCNT\":\"" + item.ADDRCNT.ToString() + "\"";
                    if (item.ADDRCNT > 0)
                    {
                        superJson += ",\"ADDR\":[";
                        for (int i = 0; i < item.ADDRCNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.ADDR[i];
                            try { superJson += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { superJson += ",\"PERSON\":\"" + currItem.PERSON + "\""; } catch { Console.WriteLine("nN PERSON"); }
                            try { superJson += ",\"REG_N\":\"" + currItem.REG_N + "\""; } catch { Console.WriteLine("nN REG_N"); }
                            try { superJson += ",\"NOTE\":\"" + currItem.NOTE + "\""; } catch { Console.WriteLine("nN NOTE"); }
                            try { superJson += ",\"REG_DATE\":\"" + currItem.REG_DATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { superJson += ",\"DATE_UPD\":\"" + currItem.DATE_UPD + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { superJson += ",\"ANSWER_DATE\":\"" + currItem.ANSWER_DATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { superJson += ",\"SENDDATE \":\"" + currItem.SENDDATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { superJson += ",\"DATE_CR\":\"" + currItem.DATE_CR + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try {
                                    superJson += ",\"DELIVERY\":{";
                                    try { superJson += "\"ISN\":\"" + currItem.DELIVERY.ISN + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"NAME\":\"" + currItem.DELIVERY.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    superJson += "}";
                            } catch { Console.WriteLine("nN DELIVERY"); }
                            try { superJson += ",\"KINDADDR\":\"" + currItem.KINDADDR + "\""; } catch { Console.WriteLine("nN KINDADDR"); }
                            if (currItem.KINDADDR == "ORGANIZ")
                            {
                                try {
                                    superJson += ",\"ORGANIZ\":{";
                                    try { superJson += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"FULLNAME\":\"" + currItem.ADDRESSEE.FULLNAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"POSTINDEX\":\"" + currItem.ADDRESSEE.POSTINDEX + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"LAW_ADDRESS\":\"" + currItem.ADDRESSEE.LAW_ADDRESS + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"INN\":\"" + currItem.ADDRESSEE.INN + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"OKPO\":\"" + currItem.ADDRESSEE.OKPO + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"OKONH\":\"" + currItem.ADDRESSEE.OKONH + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"CITY\":\"" + currItem.ADDRESSEE.CITY + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"OKONH\":\"" + currItem.ADDRESSEE.OKONH + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                                    try { superJson += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                    try { superJson += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL "); }
                                    superJson += "}";
                                } catch { Console.WriteLine("nN ORGANIZ"); }
                            }
                            if (currItem.KINDADDR == "CITIZEN")
                            {
                                try { superJson += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                                try { superJson += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                try { superJson += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL "); }
                            }
                            if (currItem.KINDADDR == "DEPARTMENT")
                            {
                                try { superJson += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                                try { superJson += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                try { superJson += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL "); }
                            }
                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN ADDRCNT"); }
                
                try { superJson += ",\"FILESCNT\":\"" + item.FILESCNT.ToString() + "\""; } catch { Console.WriteLine("nN FILESCNT"); }
                try { Console.WriteLine("FILES  " + item.FILES.ToString()); } catch { Console.WriteLine("nN FILES"); }
                try { superJson += ",\"JOURNACQCNT\":\"" + item.JOURNACQCNT.ToString() + "\""; } catch { Console.WriteLine("nN JOURNACQCNT"); }
                try { Console.WriteLine("JOURNACQ  " + item.JOURNACQ.ToString()); } catch { Console.WriteLine("nN JOURNACQ"); }
                try { superJson += ",\"NUM_FLAG\":\"" + item.NUM_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN NUM_FLAG"); }
                try { superJson += ",\"PROTCNT\":\"" + item.PROTCNT.ToString() + "\""; } catch { Console.WriteLine("nN PROTCNT"); }
                try { Console.WriteLine("PROTOCOL  " + item.PROTOCOL.ToString()); } catch { Console.WriteLine("nN PROTOCOL"); }
                try { superJson+= ",\"CARDVIEW\":\"" + item.CARDVIEW.ToString() + "\""; } catch { Console.WriteLine("nN CARDVIEW"); }
                try { superJson += ",\"ALLRESOL\":\"" + item.ALLRESOL.ToString() + "\""; } catch { Console.WriteLine("nN ALLRESOL"); }
                try { superJson += ",\"RESOLCNT\":\"" + item.RESOLCNT.ToString() + "\""; } catch { Console.WriteLine("nN RESOLCNT"); }
                try {
                    superJson += ",\"PROTCNT\":\"" + item.PROTCNT.ToString() + "\"";
                    if (item.PROTCNT > 0)
                    {
                        superJson += ",\"PROTOCOL(\":[";
                        for (int i2 = 0; i2 < item.PROTCNT; i2++)
                        {
                            if (i2 != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem2 = item.PROTOCOL[i2];
                            try { superJson += "\"PROTOCOL_WHEN\":\"" + currItem2.PROTOCOL.WHEN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                            try { superJson += "\"PROTOCOL_WHAT\":\"" + currItem2.PROTOCOL.WHAT.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.ISN"); }

                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN RESOLCNT"); }
                
                //if (item.RESOLCNT > 0)
                //{
                    superJson += ",\"RESOLUTION\":[";
                    for (int i = 0; i < item.RESOLCNT; i++)
                    {
                        if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                        var currItem = item.RESOLUTION[i];
                        var adrItem = currItem.AUTHOR;

                        try { superJson += "\"AUTHOR_ISN\":\"" + adrItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                        try { superJson += ",\"AUTHOR_NAME\":\"" + adrItem.NAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                        try { superJson += ",\"AUTHOR_SURNAME\":\"" + adrItem.SURNAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                        try { superJson += ",\"templ_TEXT\":\"" + currItem.TEXT.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                        try { superJson += ",\"templ_SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }
                        try { superJson += ",\"templ_PLANDATE\":\"" + currItem.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("nPLANDATE"); }
                        try { superJson += ",\"templ_MIDDATE\":\"" + currItem.MIDDATE.ToString() + "\""; } catch { Console.WriteLine("nMIDDATE"); }
                        try { superJson += ",\"templ_RESPONS\":\"" + currItem.RESPONS.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                        try { superJson += ",\"templ_RESPONS.NAME\":\"" + currItem.RESPONS.NAME.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                        try { superJson += ",\"templ_RESPONS.ISN\":\"" + currItem.RESPONS.ISN.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                        try { superJson += ",\"templ_RESPONS[0].NAME\":\"" + currItem.RESPONS[0].NAME.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                        try { superJson += ",\"templ_RESPONS[0].ISN\":\"" + currItem.RESPONS[0].ISN.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                        try { superJson += ",\"templ_REPLYCNT\":\"" + currItem.REPLYCNT.ToString() + "\""; } catch { Console.WriteLine("nRESOLCNT"); }
                        if (currItem.REPLYCNT > 0)
                        {
                            superJson += ",\"REPLY\":[";
                            for (int i2 = 0; i2 < currItem.REPLYCNT; i2++)
                            {
                                Console.WriteLine("i is " + i + " and i2 is " + i2);
                                if (i2 != 0) { superJson += ",{"; } else { superJson += "{"; }
                                var currItem2 = currItem.REPLY[i];
                                try { superJson += "\"REPLY_ISN\":\"" + currItem2.REPLY.ISN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR.ISN"); }
                                try { superJson += "\"REPLY_TEXT\":\"" + currItem2.REPLY.TEXT.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                               
                                superJson += "}";
                            }
                            superJson += "]";
                        }
                        try { superJson += ",\"templ_RESOLCNT\":\"" + currItem.RESOLCNT.ToString() + "\""; } catch { Console.WriteLine("nRESOLCNT"); }
                        if (currItem.RESOLCNT > 0)
                        {
                            superJson += ",\"RESOLUTION\":[";
                            for (int i2 = 0; i2 < currItem.RESOLCNT; i2++)
                            {
                                Console.WriteLine("i is "+ i+" and i2 is "+i2);
                                if (i2 != 0) { superJson += ",{"; } else { superJson += "{"; }
                                var currItem2 = currItem.RESOLUTION[i];
                                try{superJson +=   "\"AUTHOR_NAME\":\"" + currItem2.AUTHOR.NAME.ToString() + "\",";}catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                try{superJson +=   "\"AUTHOR_ISN\":\"" + currItem2.AUTHOR.ISN.ToString() + "\",";}catch { Console.WriteLine("nN AUTHOR.ISN"); }
                                try { superJson += "\"templ_SENDDATE\":\"" + currItem2.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }
                                
                                superJson += "}";
                            }
                            superJson += "]";
                        }
                        superJson += "}";
                    }
                    superJson += "]";
                //}
                try {
                    superJson += ",\"JOURNALCNT\":\"" + item.JOURNALCNT.ToString() + "\"";
                    if (item.JOURNALCNT > 0)
                    {
                        superJson += ",\"JOURNAL\":[";
                        for (int i = 0; i < item.JOURNALCNT; i++)
                        {
                            if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                            var currItem = item.JOURNAL[i];
                            try { superJson += "\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                            try { superJson += ",\"RCKIND\":\"" + currItem.RCKIND.ToString() + "\""; } catch { Console.WriteLine("nNRCKIND"); }
                            try { superJson += ",\"RC\":\"" + currItem.RC.ToString() + "\""; } catch { Console.WriteLine("nNRC"); }
                            try { superJson += ",\"KIND\":\"" + currItem.KIND.ToString() + "\""; } catch { Console.WriteLine("nNKIND"); }

                            //try { superJson += ",\"AUTHORCARD\":\"" + currItem.AUTHORCARD.ToString() + "\""; } catch { Console.WriteLine("nNAUTHORCARD"); }
                            superJson += ",\"AUTHORCARD\":{";
                            try { superJson += "\"ISN\":\"" + currItem.AUTHORCARD.ISN.ToString() + "\""; } catch { Console.WriteLine("nNAUTHORCARD"); }
                            try { superJson += ",\"NAME\":\"" + currItem.AUTHORCARD.NAME.ToString() + "\""; } catch { Console.WriteLine("nNAUTHORCARD"); }

                            superJson += "}";
                            try { superJson += ",\"AUTHORCAB\":\"" + currItem.AUTHORCAB.ToString() + "\""; } catch { Console.WriteLine("nNAUTHORCAB"); }
                            superJson += ",\"AUTHORCAB\":{";
                            try { superJson += "\"ISN\":\"" + currItem.AUTHORCAB.ISN.ToString() + "\""; } catch { Console.WriteLine("nNAUTHORCARD"); }
                            try { superJson += ",\"NAME\":\"" + currItem.AUTHORCAB.NAME.ToString() + "\""; } catch { Console.WriteLine("nNAUTHORCARD"); }

                            superJson += "}";
                            //try { superJson += ",\"ADDRESSEE\":\"" +currItem.ADDRESSEE.ToString() + "\""; } catch { Console.WriteLine("nNADDRESSEE"); }
                            Console.WriteLine("0001");

                            //if (currItem.RESOLCNT > 0)
                            //{
                            //    superJson += ",\"ADDRESSEE\":{";
                            //    for (int i2 = 0; i2 < currItem.RESOLCNT; i2++)
                            //    {


                            //var adrItem = currItem.ADDRESSES;
                            //Console.WriteLine("00012");
                            //superJson += ",\"ADDRESSEE\":{";
                            //try { superJson += "\"ADDR_ISN[0]\":\"" + adrItem[0].ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            //try { superJson += "\"ADDR_ISN\":\"" + adrItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            //try { superJson += "\"ADDR_ISNSome\":\"" + "Some" + "\""; } catch { Console.WriteLine("nN ISN"); }
                            //try { superJson += ",\"ADDR_NAME\":\"" + adrItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //try { superJson += ",\"ADDR_SURNAME\":\"" + adrItem.SURNAME + "\""; } catch { Console.WriteLine("nN SURNAME"); }
                            //superJson += "}";

                            Console.WriteLine("0002");
                            try { superJson += ",\"ORIGFLAG\":\"" + currItem.ORIGFLAG.ToString() + "\""; } catch { Console.WriteLine("nNORIGFLAG"); }
                            try { superJson += ",\"SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nNSENDDATE"); }
                            try { superJson += ",\"OWNERFLAG\":\"" + currItem.OWNERFLAG.ToString() + "\""; } catch { Console.WriteLine("nNOWNERFLAG"); }
                            try { superJson += ",\"ROLL\":\"" + currItem.ROLL.ToString() + "\""; } catch { Console.WriteLine("nNROLL"); }
                            try { superJson += ",\"NOMENCL\":\"" + currItem.NOMENCL.ToString() + "\""; } catch { Console.WriteLine("nNNOMENCL"); }
                            try { superJson += ",\"NUMFLAG\":\"" + currItem.NUMFLAG.ToString() + "\""; } catch { Console.WriteLine("nNNUMFLAG"); }
                            try { superJson += ",\"ORIGNUM\":\"" + currItem.ORIGNUM.ToString() + "\""; } catch { Console.WriteLine("nNORIGNUM"); }
                            try { superJson += ",\"COPYNUM\":\"" + currItem.COPYNUM.ToString() + "\""; } catch { Console.WriteLine("nNCOPYNUM"); }
                            try { superJson += ",\"EDOCUMENT\":\"" + currItem.EDOCUMENT.ToString() + "\""; } catch { Console.WriteLine("nNEDOCUMENT"); }
                            try { superJson += ",\"NOTES\":\"" + currItem.NOTES.ToString() + "\""; } catch { Console.WriteLine("nNNOTES"); }
                            try { superJson += ",\"USER_CR\":\"" + currItem.USER_CR.ToString() + "\""; } catch { Console.WriteLine("nNUSER_CR"); }
                            var userItem = currItem.USER_CR;
                            Console.WriteLine("0003");
                            superJson += ",\"USER_CR\":{";
                            try { superJson += "\"User_ISN\":\"" + userItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                            try { superJson += ",\"User_WEIGHT\":\"" + userItem.WEIGHT.ToString() + "\""; } catch { Console.WriteLine("nNWEIGHT"); }
                            try { superJson += ",\"User_NAME\":\"" + userItem.NAME.ToString() + "\""; } catch { Console.WriteLine("nNNAME"); }
                            try { superJson += ",\"User_DELETED\":\"" + userItem.DELETED.ToString() + "\""; } catch { Console.WriteLine("nNDELETED"); }
                            try { superJson += ",\"User_ID\":\"" + userItem.ID.ToString() + "\""; } catch { Console.WriteLine("nNID"); }
                            try { superJson += ",\"User_PASSWORD\":\"" + userItem.PASSWORD.ToString() + "\""; } catch { Console.WriteLine("nNPASSWORD"); }
                            try { superJson += ",\"User_DEPART\":\"" + userItem.DEPART.ToString() + "\""; } catch { Console.WriteLine("nNDEPART"); }
                            try { superJson += ",\"User_ADMIN\":\"" + userItem.ADMIN.ToString() + "\""; } catch { Console.WriteLine("nNADMIN"); }
                            try { superJson += ",\"User_FUNCLIST\":\"" + userItem.FUNCLIST.ToString() + "\""; } catch { Console.WriteLine("nNFUNCLIST"); }
                            try { superJson += ",\"User_OPTLIST\":\"" + userItem.OPTLIST.ToString() + "\""; } catch { Console.WriteLine("nNOPTLIST"); }
                            try { superJson += ",\"User_NOTE\":\"" + userItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nNNOTE"); }
                            try { superJson += ",\"User_DEPDL\":\"" + userItem.DEPDL.ToString() + "\""; } catch { Console.WriteLine("nNDEPDL"); }
                            try { superJson += ",\"User_CARDCNT\":\"" + userItem.CARDCNT.ToString() + "\""; } catch { Console.WriteLine("nNCARDCNT"); }
                            try { superJson += ",\"User_CARD\":\"" + userItem.CARD.ToString() + "\""; } catch { Console.WriteLine("nNCARD"); }
                            try { superJson += ",\"User_SECURCNT\":\"" + userItem.SECURCNT.ToString() + "\""; } catch { Console.WriteLine("nNSECURCNT"); }
                            try { superJson += ",\"User_SECURITY\":\"" + userItem.SECURITY.ToString() + "\""; } catch { Console.WriteLine("nNSECURITY"); }
                            try { superJson += ",\"User_ADDREZOLDEP\":\"" + userItem.ADDREZOLDEP.ToString() + "\""; } catch { Console.WriteLine("nNADDREZOLDEP"); }
                            try { superJson += ",\"User_LISTEXECREZOL\":\"" + userItem.LISTEXECREZOL.ToString() + "\""; } catch { Console.WriteLine("nNLISTEXECREZOL"); }
                            try { superJson += ",\"User_GETLIST\":\"" + userItem.GETLIST.ToString() + "\""; } catch { Console.WriteLine("nNGETLIST"); }
                            try { superJson += ",\"User_LISTCHECKREZOL\":\"" + userItem.LISTCHECKREZOL.ToString() + "\""; } catch { Console.WriteLine("nNLISTCHECKREZOL"); }
                            try { superJson += ",\"User_LISTEXECVISA\":\"" + userItem.LISTEXECVISA.ToString() + "\""; } catch { Console.WriteLine("nNLISTEXECVISA"); }
                            try { superJson += ",\"User_LISTEXECSIGN\":\"" + userItem.LISTEXECSIGN.ToString() + "\""; } catch { Console.WriteLine("nNLISTEXECSIGN"); }
                            try { superJson += ",\"User_LISTREADCONFRC\":\"" + userItem.LISTREADCONFRC.ToString() + "\""; } catch { Console.WriteLine("nNLISTREADCONFRC"); }
                            try { superJson += ",\"User_LISTREADCONFFILE\":\"" + userItem.LISTREADCONFFILE.ToString() + "\""; } catch { Console.WriteLine("nNLISTREADCONFFILE"); }
                            try { superJson += ",\"User_USERDEP\":\"" + userItem.USERDEP.ToString() + "\""; } catch { Console.WriteLine("nNUSERDEP"); }
                            try { superJson += ",\"User_CARD\":\"" + userItem.CARD.ToString() + "\""; } catch { Console.WriteLine("nNCARD"); }
                            try { superJson += ",\"User_SECURITY\":\"" + userItem.SECURITY.ToString() + "\""; } catch { Console.WriteLine("nNSECURITY"); }
                            try { superJson += ",\"User_CERTPROFILECNT\":\"" + userItem.CERTPROFILECNT.ToString() + "\""; } catch { Console.WriteLine("nNCERTPROFILECNT"); }
                            try { superJson += ",\"User_CERTPROFILE\":\"" + userItem.CERTPROFILE.ToString() + "\""; } catch { Console.WriteLine("nNCERTPROFILE"); }
                            try { superJson += ",\"User_ERRCODE\":\"" + userItem.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                            try { superJson += ",\"User_ERRTEXT\":\"" + userItem.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nNERRTEXT"); }
                            Console.WriteLine("0004");
                            superJson += "}";
                            try { superJson += ",\"DATE_CR\":\"" + currItem.DATE_CR.ToString() + "\""; } catch { Console.WriteLine("nNDATE_CR"); }

                            userItem = currItem.USER_UPD;
                            Console.WriteLine("0003");
                            superJson += ",\"USER_UPD\":{";
                            try { superJson += "\"User_ISN\":\"" + userItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                            try { superJson += ",\"User_WEIGHT\":\"" + userItem.WEIGHT.ToString() + "\""; } catch { Console.WriteLine("nNWEIGHT"); }
                            try { superJson += ",\"User_NAME\":\"" + userItem.NAME.ToString() + "\""; } catch { Console.WriteLine("nNNAME"); }
                            try { superJson += ",\"User_DELETED\":\"" + userItem.DELETED.ToString() + "\""; } catch { Console.WriteLine("nNDELETED"); }
                            try { superJson += ",\"User_ID\":\"" + userItem.ID.ToString() + "\""; } catch { Console.WriteLine("nNID"); }
                            try { superJson += ",\"User_PASSWORD\":\"" + userItem.PASSWORD.ToString() + "\""; } catch { Console.WriteLine("nNPASSWORD"); }
                            try { superJson += ",\"User_DEPART\":\"" + userItem.DEPART.ToString() + "\""; } catch { Console.WriteLine("nNDEPART"); }
                            try { superJson += ",\"User_ADMIN\":\"" + userItem.ADMIN.ToString() + "\""; } catch { Console.WriteLine("nNADMIN"); }
                            try { superJson += ",\"User_FUNCLIST\":\"" + userItem.FUNCLIST.ToString() + "\""; } catch { Console.WriteLine("nNFUNCLIST"); }
                            try { superJson += ",\"User_OPTLIST\":\"" + userItem.OPTLIST.ToString() + "\""; } catch { Console.WriteLine("nNOPTLIST"); }
                            try { superJson += ",\"User_NOTE\":\"" + userItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nNNOTE"); }
                            try { superJson += ",\"User_DEPDL\":\"" + userItem.DEPDL.ToString() + "\""; } catch { Console.WriteLine("nNDEPDL"); }
                            try { superJson += ",\"User_CARDCNT\":\"" + userItem.CARDCNT.ToString() + "\""; } catch { Console.WriteLine("nNCARDCNT"); }
                            try { superJson += ",\"User_CARD\":\"" + userItem.CARD.ToString() + "\""; } catch { Console.WriteLine("nNCARD"); }
                            try { superJson += ",\"User_SECURCNT\":\"" + userItem.SECURCNT.ToString() + "\""; } catch { Console.WriteLine("nNSECURCNT"); }
                            try { superJson += ",\"User_SECURITY\":\"" + userItem.SECURITY.ToString() + "\""; } catch { Console.WriteLine("nNSECURITY"); }
                            try { superJson += ",\"User_ADDREZOLDEP\":\"" + userItem.ADDREZOLDEP.ToString() + "\""; } catch { Console.WriteLine("nNADDREZOLDEP"); }
                            try { superJson += ",\"User_LISTEXECREZOL\":\"" + userItem.LISTEXECREZOL.ToString() + "\""; } catch { Console.WriteLine("nNLISTEXECREZOL"); }
                            try { superJson += ",\"User_GETLIST\":\"" + userItem.GETLIST.ToString() + "\""; } catch { Console.WriteLine("nNGETLIST"); }
                            try { superJson += ",\"User_LISTCHECKREZOL\":\"" + userItem.LISTCHECKREZOL.ToString() + "\""; } catch { Console.WriteLine("nNLISTCHECKREZOL"); }
                            try { superJson += ",\"User_LISTEXECVISA\":\"" + userItem.LISTEXECVISA.ToString() + "\""; } catch { Console.WriteLine("nNLISTEXECVISA"); }
                            try { superJson += ",\"User_LISTEXECSIGN\":\"" + userItem.LISTEXECSIGN.ToString() + "\""; } catch { Console.WriteLine("nNLISTEXECSIGN"); }
                            try { superJson += ",\"User_LISTREADCONFRC\":\"" + userItem.LISTREADCONFRC.ToString() + "\""; } catch { Console.WriteLine("nNLISTREADCONFRC"); }
                            try { superJson += ",\"User_LISTREADCONFFILE\":\"" + userItem.LISTREADCONFFILE.ToString() + "\""; } catch { Console.WriteLine("nNLISTREADCONFFILE"); }
                            try { superJson += ",\"User_USERDEP\":\"" + userItem.USERDEP.ToString() + "\""; } catch { Console.WriteLine("nNUSERDEP"); }
                            try { superJson += ",\"User_CARD\":\"" + userItem.CARD.ToString() + "\""; } catch { Console.WriteLine("nNCARD"); }
                            try { superJson += ",\"User_SECURITY\":\"" + userItem.SECURITY.ToString() + "\""; } catch { Console.WriteLine("nNSECURITY"); }
                            try { superJson += ",\"User_CERTPROFILECNT\":\"" + userItem.CERTPROFILECNT.ToString() + "\""; } catch { Console.WriteLine("nNCERTPROFILECNT"); }
                            try { superJson += ",\"User_CERTPROFILE\":\"" + userItem.CERTPROFILE.ToString() + "\""; } catch { Console.WriteLine("nNCERTPROFILE"); }
                            try { superJson += ",\"User_ERRCODE\":\"" + userItem.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                            try { superJson += ",\"User_ERRTEXT\":\"" + userItem.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nNERRTEXT"); }
                            Console.WriteLine("0005");
                            superJson += "}";

                            try { superJson += ",\"DATE_UPD\":\"" + currItem.DATE_UPD.ToString() + "\""; } catch { Console.WriteLine("nNDATE_UPD"); }
                            try { superJson += ",\"ERRCODE\":\"" + currItem.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                            try { superJson += ",\"ERRTEXT\":\"" + currItem.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nNERRTEXT"); }
                            superJson += "}";
                        }
                        superJson += "]";
                    }
                } catch { Console.WriteLine("nN JOURNALCNT"); }
                
                try { superJson+= ",\"FORWARDCNT\":\"" + item.FORWARDCNT.ToString() + "\""; } catch { Console.WriteLine("nN FORWARDCNT"); }
                try { Console.WriteLine("FORWARD  " + item.FORWARD.ToString()); } catch { Console.WriteLine("nN FORWARD"); }
                try { superJson+= ",\"ADDPROPSCNT\":\"" + item.ADDPROPSCNT.ToString() + "\""; } catch { Console.WriteLine("nN ADDPROPSCNT"); }
                try { Console.WriteLine("ADDPROPS  " + item.ADDPROPS.ToString()); } catch { Console.WriteLine("nN ADDPROPS"); }
                try { superJson+= ",\"VALUEADDPROPS\":\"" + item.VALUEADDPROPS.ToString() + "\""; } catch { Console.WriteLine("nN VALUEADDPROPS"); }
                try { superJson+= ",\"##USERREAD\":\"" + item.USERREAD.ToString() + "\""; } catch { Console.WriteLine("nN ##USERREAD"); }
                try { Console.WriteLine("USER_CR  " + item.USER_CR.ToString()); } catch { Console.WriteLine("nN USER_CR"); }
                try { Console.WriteLine("DATE_CR  " + item.DATE_CR.ToString()); } catch { Console.WriteLine("nN DATE_CR"); }
                try { Console.WriteLine("CORRESP  " + item.CORRESP.ToString()); } catch { Console.WriteLine("nN CORRESP"); }
                try { superJson+= ",\"LINKS\":\"" + item.LINKS.ToString() + "\""; } catch { Console.WriteLine("nN LINKS"); }
                try { Console.WriteLine("ADDRESSES  " + item.ADDRESSES.ToString()); } catch { Console.WriteLine("nN ADDRESSES"); }
                try { Console.WriteLine("RUBRIC  " + item.RUBRIC.ToString()); } catch { Console.WriteLine("nN RUBRIC"); }
                try { Console.WriteLine("CARD  " + item.CARD.ToString()); } catch { Console.WriteLine("nN CARD"); }
                try { Console.WriteLine("ADDR  " + item.ADDR.ToString()); } catch { Console.WriteLine("nN ADDR"); }
                try { Console.WriteLine("FILES  " + item.FILES.ToString()); } catch { Console.WriteLine("nN FILES"); }
                try { Console.WriteLine("JOURNAL  " + item.JOURNAL.ToString()); } catch { Console.WriteLine("nN JOURNAL"); }
                try { superJson+= ",\"RESOL\":\"" + item.RESOL.ToString() + "\""; } catch { Console.WriteLine("nN RESOL"); }
                try { Console.WriteLine("JOURNACQ  " + item.JOURNACQ.ToString()); } catch { Console.WriteLine("nN JOURNACQ"); }
                try { Console.WriteLine("ADDPROPS  " + item.ADDPROPS.ToString()); } catch { Console.WriteLine("nN ADDPROPS"); }
                try { Console.WriteLine("ADDPROPSRUBRIC  " + item.ADDPROPSRUBRIC.ToString()); } catch { Console.WriteLine("nN ADDPROPSRUBRIC"); }
                try { Console.WriteLine("PROTOCOL  " + item.PROTOCOL.ToString()); } catch { Console.WriteLine("nN PROTOCOL"); }
                try { Console.WriteLine("FORWARD  " + item.FORWARD.ToString()); } catch { Console.WriteLine("nN FORWARD"); }
                try { superJson+= ",\"ERRCODE\":\"" + item.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nN ERRCODE"); }
                try { superJson+= ",\"ERRTEXT\":\"" + item.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nN ERRTEXT"); }                //}

                //Console.WriteLine("MyRcCOntainer.CORRESP444");
                superJson += "}";
                //Dictionary<string, string> tempDictionary = new Dictionary<string, string>
                //    {
                //        {"ISN", isn.ToString() },
                //        {"RegNum", MyRcCOntainer.RegNum },
                //        {"DocDate", MyRcCOntainer.DocDate.ToString() },
                //        {"Contents", MyRcCOntainer.Contents.Replace("\"", "&quot;") }
                //    };
                //var kvs = tempDictionary.Select(kvp => string.Format("\"{0}\":\"{1}\"", kvp.Key, string.Concat("", kvp.Value)));
                //string jsonChar = string.Concat("{", string.Join(",", kvs), "}");
                //superJson += jsonChar;
                //    //SuperArr[i] = tempDictionary;
                //}
                superJson = superJson + "]";
                return superJson;

            }
            catch (Exception)
            {
                head = null;
                return "";
            }
            finally
            {
                if (head != null && head.Active)
                {
                    head.Close();//закрыть соединение

                }
            }
        }

        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient Client)
        {
            string Request = "";
            byte[] Buffer = new byte[1024];
            int Count;
            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
            {
                Request += Encoding.ASCII.GetString(Buffer, 0, Count);
                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                {
                    break;
                }
                
            }
            Console.WriteLine("-----------------------------");
            //Console.WriteLine("New Request [" + Request + "]");
            string[] RequestArr = Request.Split(' ');
            Console.WriteLine("RequestArr.Length [" + RequestArr.Length + "]");
            string reqType = Request.Split(' ')[0];
            Console.WriteLine(" reqType [" + reqType + "]");
            try
            {
            string paramsString = Request.Split(' ')[1];
                if ((paramsString!="/")&&(RequestArr.Length>1)) {
                    Console.WriteLine("[" + reqType + "]"+" paramsString [" + paramsString + "]");

                    var matches = Regex.Matches(paramsString, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
                    Dictionary<string, string> Dict = matches.Cast<Match>().ToDictionary(
                        m => Uri.UnescapeDataString(m.Groups[2].Value),
                        m => Uri.UnescapeDataString(m.Groups[3].Value)
                    );

                    if (Dict.ContainsKey("need"))
                    {
                        processReq(Client, reqType, Dict);
                    }
                    else {SendError(Client, "Error: not exist paramsArr['need']", reqType); }
                }
                else {SendError(Client, "Error with Request, there is no params", reqType);}
            }
            catch {SendError(Client, "Error with Request", reqType);}
        }

    }

    class Server
    {
        TcpListener Listener; // Объект, принимающий TCP-клиентов

        // Запуск сервера
        public Server(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port); // Создаем "слушателя" для указанного порта
            Listener.Start(); // Запускаем его

            // В бесконечном цикле
            while (true)
            {
                TcpClient Client = Listener.AcceptTcpClient();
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                Thread.Start(Client);

            }
        }

        static void ClientThread(Object StateInfo)
        {
            new Client((TcpClient)StateInfo);
        }

        // Остановка сервера
        ~Server()
        {
            Console.WriteLine("S_ 04");
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("S_ Server is running on port 7788");
            new Server(7788);
        }
    }
}

