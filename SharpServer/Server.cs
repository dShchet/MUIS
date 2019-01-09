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
                if (Dict.ContainsKey("isn")) {
                    string isnTemp = Dict["isn"];
                    int isn = System.Convert.ToInt32(isnTemp);
                    //Console.WriteLine("isn is " + "[" + isn + "]");
                    string returnJson = EOSOneGet(isn);
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
                    if (i > 0) { superJson = superJson + ","; }
                    Dictionary<string, string> tempDictionary = new Dictionary<string, string>
                    {
                        {"ISN", ResultSet.Item(i).ISN.ToString() },
                        {"RegNum", ResultSet.Item(i).RegNum },
                        {"DocDate", ResultSet.Item(i).DocDate.ToString() },
                        {"Contents", ResultSet.Item(i).Contents.Replace("\"", "&quot;") }
                    };
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

        public string EOSOneGet(int isn)
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

                dynamic MyRcIn = head.GetRow("RcIn", isn);

                //Console.WriteLine(MyRcIn.REGNUM);
                //Console.WriteLine(MyRcIn.DocDate.ToString());
                //Console.WriteLine(MyRcIn.Contents.Replace("\"", "&quot;"));
                //Console.WriteLine(MyRcIn.REGNUM);
                
                //Console.WriteLine("still good3");
                string superJson = "[";
                //string superJson = "[{\"everything\":\"is good\"}]";
                //superJson = "{\"evrething\":\"is good\"}";
                //for (int i = 0; i < ItemCnt; i++)
                //{
                superJson += "{";
                //superJson += "\"ISN\"" + ":" + "\"" + isn.ToString() + "\"" + ",";
                //superJson += "\"RegNum\"" + ":" + "\"" + MyRcIn.RegNum + "\"" + ",";
                //superJson += "\"DocDate\"" + ":" + "\"" + MyRcIn.DocDate.ToString() + "\"" + ",";
                //superJson += "\"Contents\"" + ":" + "\"" + MyRcIn.Contents.Replace("\"", "&quot;") + "\"";
                //Console.WriteLine(MyRcIn.CORRESP444);
                Console.WriteLine("MyRcIn.CORRESP");
                //if (MyRcIn.CORRESP[0].KIND == 1) {
                //var item = MyRcIn.CORRESP[0];
                var item = MyRcIn;
                try { superJson += "\"ISN\":\"" + item.isn.ToString() + "\""; } catch { Console.WriteLine("nN ISN"); }
                try { Console.WriteLine("DOCGROUP  " + item.DOCGROUP.ToString()); } catch { Console.WriteLine("nN DOCGROUP"); }
                try { superJson += ",\"REGNUM\":\"" + item.REGNUM.ToString() + "\""; } catch { Console.WriteLine("nN REGNUM"); }
                try { superJson += ",\"ORDERNUM\":\"" + item.ORDERNUM.ToString() + "\""; } catch { Console.WriteLine("nN ORDERNUM"); }
                try { superJson += ",\"SPECIMEN\":\"" + item.SPECIMEN.ToString() + "\""; } catch { Console.WriteLine("nN SPECIMEN"); }
                try { superJson += ",\"DOCDATE\":\"" + item.DOCDATE.ToString() + "\""; } catch { Console.WriteLine("nN DOCDATE"); }
                try { superJson += ",\"DOCKIND\":\"" + item.DOCKIND.ToString() + "\""; } catch { Console.WriteLine("nN DOCKIND"); }
                try { superJson += ",\"CONSIST\":\"" + item.CONSIST.ToString() + "\""; } catch { Console.WriteLine("nN CONSIST"); }
                try { superJson += ",\"CONTENTS\":\"" + item.CONTENTS.ToString() + "\""; } catch { Console.WriteLine("nN CONTENTS"); }
                try { Console.WriteLine("CARDREG  " + item.CARDREG.ToString()); } catch { Console.WriteLine("nN CARDREG"); }
                try { Console.WriteLine("CABREG  " + item.CABREG.ToString()); } catch { Console.WriteLine("nN CABREG"); }
                try { superJson += ",\"ACCESSMODE\":\"" + item.ACCESSMODE.ToString() + "\""; } catch { Console.WriteLine("nN ACCESSMODE"); }
                try { Console.WriteLine("SECURITY  " + item.SECURITY.ToString()); } catch { Console.WriteLine("nN SECURITY"); }
                try { Console.WriteLine("ADDRESSEE  " + item.ADDRESSEE.ToString()); } catch { Console.WriteLine("nN ADDRESSEE"); }
                try { superJson += ",\"ADDRESSESCNT\":\"" + item.ADDRESSESCNT.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESSESCNT"); }
                try { Console.WriteLine("ADDRESSES  " + item.ADDRESSES.ToString()); } catch { Console.WriteLine("nN ADDRESSES"); }
                if (item.ADDRESSESCNT > 0)
                {
                    superJson += ",\"ADDRESSES\":[";
                    for (int i = 0; i < item.ADDRESSESCNT; i++)
                    {
                        if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                        var currItem = item.ADDRESSES[i];
                        try { superJson += "\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                        try { superJson += ",\"SURNAME\":\"" + currItem.SURNAME + "\""; } catch { Console.WriteLine("nN SURNAME"); }
                        try { superJson += ",\"POST\":\"" + currItem.POST + "\""; } catch { Console.WriteLine("nN POST"); }
                        superJson += "}";
                    }
                    superJson += "]";
                }
                try { Console.WriteLine("DELIVERY  " + item.DELIVERY.ToString()); } catch { Console.WriteLine("nN DELIVERY"); }
                try { superJson += ",\"TELEGRAM\":\"" + item.TELEGRAM.ToString() + "\""; } catch { Console.WriteLine("nN TELEGRAM"); }
                try { superJson += ",\"NOTE\":\"" + item.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                try { superJson += ",\"ADDRESS_FLAG\":\"" + item.ADDRESS_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESS_FLAG"); }
                try { superJson += ",\"ISCONTROL\":\"" + item.ISCONTROL.ToString() + "\""; } catch { Console.WriteLine("nN ISCONTROL"); }
                try { superJson += ",\"PLANDATE\":\"" + item.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("nN PLANDATE"); }
                try { superJson += ",\"FACTDATE\":\"" + item.FACTDATE.ToString() + "\""; } catch { Console.WriteLine("nN FACTDATE"); }
                try { superJson += ",\"DELTA\":\"" + item.DELTA.ToString() + "\""; } catch { Console.WriteLine("nN DELTA"); }
                try { superJson += ",\"CARDCNT\":\"" + item.CARDCNT.ToString() + "\""; } catch { Console.WriteLine("nN CARDCNT"); }
                try { Console.WriteLine("CARD  " + item.CARD.ToString()); } catch { Console.WriteLine("nN CARD"); }
                try { superJson += ",\"CORRESPCNT\":\"" + item.CORRESPCNT.ToString() + "\""; } catch { Console.WriteLine("nN CORRESPCNT"); }
                try { Console.WriteLine("CORRESPCNT  " + item.CORRESPCNT); } catch { Console.WriteLine("nN CORRESPCNT"); }
                try { Console.WriteLine("CORRESP  " + item.CORRESP.ToString()); } catch { Console.WriteLine("nN CORRESP"); }
                if (item.CORRESPCNT > 0) { 
                    superJson += ",\"CORRESP\":[";
                    for (int i = 0; i < item.CORRESPCNT; i++)
                    {
                        if (i != 0) { superJson += ",{"; }else{ superJson += "{"; }
                        var currItem = item.CORRESP[i];
                        if (currItem.KIND == 1)
                        {
                            try { superJson += "\"ORGANIZ\":\"" + currItem.ORGANIZ.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN KIND"); }
                            try { superJson += ",\"OUTNUM\":\"" + currItem.OUTNUM.ToString() + "\""; } catch { Console.WriteLine("nN KIND"); }
                            try { superJson += ",\"OUTDATE\":\"" + currItem.OUTDATE.ToString() + "\""; } catch { Console.WriteLine("nN KIND"); }
                            try { superJson += ",\"SIGN \":\"" + currItem.SIGN.ToString() + "\""; } catch { Console.WriteLine("nN SIGN"); }
                            try { superJson += ",\"NOTE \":\"" + currItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                        }
                        else{ /*Вид записи: 1 – информация о Корреспонденте документа; 2 – информация о сопроводительном документа; Записи первого вида могут принадлежать только РК вида "Входящий" от организаций, второго вида – как "Входящим" от организаций так и "Письмам" от физических лиц.*/}

                        superJson += "}";
                    }
                    superJson += "]";
                }
                try { superJson += ",\"LINKCNT\":\"" + item.LINKCNT.ToString() + "\""; } catch { Console.WriteLine("nN LINKCNT"); }
                try { Console.WriteLine("LINKREF  " + item.LINKREF.ToString()); } catch { Console.WriteLine("nN LINKREF"); }
                try { superJson += ",\"RUBRICCNT\":\"" + item.RUBRICCNT.ToString() + "\""; } catch { Console.WriteLine("nN RUBRICCNT"); }
                if (item.RUBRICCNT > 0)
                {
                    superJson += ",\"RUBRIC\":[";
                    for (int i = 0; i < item.RUBRICCNT; i++)
                    {
                        if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                        var currItem = item.RUBRIC[i];
                        try { superJson += "\"ISN \":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                        try { superJson += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                        superJson += "}";
                    }
                    superJson += "]";
                }
                try { superJson += ",\"ADDPROPSRUBRICCNT\":\"" + item.ADDPROPSRUBRICCNT.ToString() + "\""; } catch { Console.WriteLine("nN ADDPROPSRUBRICCNT"); }
                try { Console.WriteLine("ADDPROPSRUBRIC  " + item.ADDPROPSRUBRIC.ToString()); } catch { Console.WriteLine("nN ADDPROPSRUBRIC"); }
                try { superJson += ",\"VALUEADDPROPSRUBRIC\":\"" + item.VALUEADDPROPSRUBRIC.ToString() + "\""; } catch { Console.WriteLine("nN VALUEADDPROPSRUBRIC"); }
                try { superJson += ",\"ADDRCNT\":\"" + item.ADDRCNT.ToString() + "\""; } catch { Console.WriteLine("nN ADDRCNT"); }
                if (item.ADDRCNT > 0)
                {
                    superJson += ",\"ADDR\":[";
                    for (int i = 0; i < item.ADDRCNT; i++)
                    {
                        if (i != 0) { superJson += ",{"; } else { superJson += "{"; }
                        var currItem = item.ADDR[i];
                        try { superJson += "\"ISN \":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                        try { superJson += ",\"PERSON\":\"" + currItem.PERSON + "\""; } catch { Console.WriteLine("nN PERSON"); }
                        try { superJson += ",\"REG_N\":\"" + currItem.REG_N + "\""; } catch { Console.WriteLine("nN REG_N"); }
                        try { superJson += ",\"NOTE\":\"" + currItem.NOTE + "\""; } catch { Console.WriteLine("nN NOTE"); }
                        try { superJson += ",\"REG_DATE\":\"" + currItem.REG_DATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                        try { superJson += ",\"KINDADDR\":\"" + currItem.KINDADDR + "\""; } catch { Console.WriteLine("nN KINDADDR"); }
                        if (currItem.KINDADDR == "ORGANIZ")
                        {
                            try { superJson += ",\"NAME \":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                            try { superJson += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                            try { superJson += ",\"EMAIL \":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL "); }
                        }
                        if (currItem.KINDADDR == "CITIZEN")
                        {
                            try { superJson += ",\"NAME \":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                            try { superJson += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                            try { superJson += ",\"EMAIL \":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL "); }
                        }
                        if (currItem.KINDADDR == "DEPARTMENT")
                        {
                            try { superJson += ",\"NAME \":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME "); }
                            try { superJson += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                            try { superJson += ",\"EMAIL \":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL "); }
                        }
                        superJson += "}";
                    }
                    superJson += "]";
                }
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
                try { Console.WriteLine("RESOLUTION  " + item.RESOLUTION.ToString()); } catch { Console.WriteLine("nN RESOLUTION"); }
                try { superJson+= ",\"JOURNALCNT\":\"" + item.JOURNALCNT.ToString() + "\""; } catch { Console.WriteLine("nN JOURNALCNT"); }
                try { Console.WriteLine("JOURNAL  " + item.JOURNAL.ToString()); } catch { Console.WriteLine("nN JOURNAL"); }
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

                Console.WriteLine("MyRcIn.CORRESP444");
                superJson += "}";
                //Dictionary<string, string> tempDictionary = new Dictionary<string, string>
                //    {
                //        {"ISN", isn.ToString() },
                //        {"RegNum", MyRcIn.RegNum },
                //        {"DocDate", MyRcIn.DocDate.ToString() },
                //        {"Contents", MyRcIn.Contents.Replace("\"", "&quot;") }
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

