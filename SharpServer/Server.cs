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
                }
                else
                {
                    SendError(Client, "error: incorrect getOne", reqType);
                }


            }
            else
            {
                SendError(Client, "error: incorrect params in request", reqType);
            }
        }

        //Отправить запрос
        private void SendResp(TcpClient Client, int Code, string format, string bigJson, string reqType)
        {
            try
            {
                Console.WriteLine("[" + reqType + "]" + "sending json :" + bigJson);
                UTF8Encoding utf8 = new UTF8Encoding();
                string Headers = "HTTP/1.1 " + Code + " \nContent-Type: " + format + " \nAccess-Control-Allow-Origin: *" +
                    " \nAccess-Control-Allow-Headers: Content-Type" +
                    " \nAccess-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS" +
                    "\n\n";
                byte[] HeadersBuffer = utf8.GetBytes(Headers);
                Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
                byte[] jsonBuffer = utf8.GetBytes(bigJson);
                Client.GetStream().Write(jsonBuffer, 0, jsonBuffer.Length);
                Client.Close();
            }
            catch
            {
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
                string json = "[";
                for (int i = 0; i < ItemCnt; i++)
                {
                    var item = ResultSet.Item(i);
                    if (i > 0) { json = json + ","; }
                    Dictionary<string, string> tempDictionary = new Dictionary<string, string>();
                    try { tempDictionary.Add("ISN", item.ISN.ToString()); } catch { }
                    try { tempDictionary.Add("RegNum", item.RegNum); } catch { }
                    try { tempDictionary.Add("DocDate", item.DocDate.ToString()); } catch { }
                    try { tempDictionary.Add("DOCKIND", item.DOCKIND.ToString()); }
                    catch
                    {
                        tempDictionary.Add("DOCKIND", "RCOUT");
                    }
                    try { tempDictionary.Add("Contents", item.Contents.Replace("\"", "&quot;")); } catch { }
                    try
                    {
                        if (item.CORRESPCNT > 0)
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
                    json = json + jsonChar;
                    //SuperArr[i] = tempDictionary;
                }
                json = json + "]";
                return json;

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
                else
                {
                    Console.WriteLine("errror: wrong rcType");
                    item = head.GetRow("RcIn", isn);
                }

                //Console.WriteLine(MyRcCOntainer.REGNUM);
                //Console.WriteLine(MyRcCOntainer.DocDate.ToString());
                //Console.WriteLine(MyRcCOntainer.Contents.Replace("\"", "&quot;"));
                //Console.WriteLine(MyRcCOntainer.REGNUM);

                //Console.WriteLine("still good3");
                string json = "[";
                //string json = "[{\"everything\":\"is good\"}]";
                //json = "{\"evrething\":\"is good\"}";
                //for (int i = 0; i < ItemCnt; i++)
                //{
                json += "{";
                //json += "\"ISN\"" + ":" + "\"" + isn.ToString() + "\"" + ",";
                //json += "\"RegNum\"" + ":" + "\"" + MyRcCOntainer.RegNum + "\"" + ",";
                //json += "\"DocDate\"" + ":" + "\"" + MyRcCOntainer.DocDate.ToString() + "\"" + ",";
                //json += "\"Contents\"" + ":" + "\"" + MyRcCOntainer.Contents.Replace("\"", "&quot;") + "\"";
                //Console.WriteLine(MyRcCOntainer.CORRESP444);
                Console.WriteLine("MyRcCOntainer.CORRESP");
                //if (MyRcCOntainer.CORRESP[0].KIND == 1) {
                //var item = MyRcCOntainer.CORRESP[0];
                try { json += "\"ISN\":\"" + item.ISN.ToString() + "\""; } catch { Console.WriteLine("nN ISN"); }
                try
                {
                    json += ",\"DOCGROUP\":{";
                    json += "\"ISN\":\"" + item.DOCGROUP.ISN.ToString() + "\"";
                    json += ",\"NAME\":\"" + item.DOCGROUP.NAME.ToString() + "\"";
                    json += "}";
                }
                catch { Console.WriteLine("nN DOCGROUP"); }
                try { json += ",\"REGNUM\":\"" + item.REGNUM.ToString() + "\""; } catch { Console.WriteLine("nN REGNUM"); }
                try { json += ",\"ORDERNUM\":\"" + item.ORDERNUM.ToString() + "\""; } catch { Console.WriteLine("nN ORDERNUM"); }
                try { json += ",\"SPECIMEN\":\"" + item.SPECIMEN.ToString() + "\""; } catch { Console.WriteLine("nN SPECIMEN"); }
                try { json += ",\"DOCDATE\":\"" + item.DOCDATE.ToString() + "\""; } catch { Console.WriteLine("nN DOCDATE"); }
                try { json += ",\"CONSIST\":\"" + item.CONSIST.ToString() + "\""; } catch { Console.WriteLine("nN CONSIST"); }
                try { json += ",\"CONTENTS\":\"" + item.CONTENTS.ToString().Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN CONTENTS"); }
                try
                {
                    json += ",\"CARDREG\":{";
                    json += "\"ISN\":\"" + item.CARDREG.ISN.ToString() + "\"";
                    json += ",\"NAME\":\"" + item.CARDREG.NAME.ToString() + "\"";
                    json += "}";
                }
                catch { Console.WriteLine("nN CARDREG"); }
                try
                {
                    json += ",\"CABREG\":{";
                    json += "\"ISN\":\"" + item.CABREG.ISN.ToString() + "\"";
                    json += ",\"NAME\":\"" + item.CABREG.NAME.ToString() + "\"";
                    json += "}";
                }
                catch { Console.WriteLine("nN CABREG"); }
                try { json += ",\"ACCESSMODE\":\"" + item.ACCESSMODE.ToString() + "\""; } catch { Console.WriteLine("nN ACCESSMODE"); }
                try
                {
                    json += ",\"SECURITY\":{";
                    json += "\"ISN\":\"" + item.SECURITY.ISN.ToString() + "\"";
                    json += ",\"NAME\":\"" + item.SECURITY.NAME.ToString() + "\"";
                    json += "}";
                }
                catch { Console.WriteLine("nN SECURITY"); }
                try { json += ",\"NOTE\":\"" + item.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                try { json += ",\"ADDRESS_FLAG\":\"" + item.ADDRESS_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESS_FLAG"); }
                try { json += ",\"ISCONTROL\":\"" + item.ISCONTROL.ToString() + "\""; } catch { Console.WriteLine("nN ISCONTROL"); }
                try { json += ",\"PLANDATE\":\"" + item.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("nN PLANDATE"); }
                try { json += ",\"FACTDATE\":\"" + item.FACTDATE.ToString() + "\""; } catch { Console.WriteLine("nN FACTDATE"); }
                try { json += ",\"DELTA\":\"" + item.DELTA.ToString() + "\""; } catch { Console.WriteLine("nN DELTA"); }
                try
                {
                    json += ",\"CARDCNT\":\"" + item.CARDCNT.ToString() + "\"";
                    if (item.CARDCNT > 0)
                    {
                        json += ",\"CARD\":[";
                        for (int i = 0; i < item.CARDCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.CARD[i];
                            try { json += "\"DATE\":\"" + currItem.DATE + "\""; } catch { Console.WriteLine("nN ISN"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN CARDCNT"); }
                try
                {
                    json += ",\"LINKCNT\":\"" + item.LINKCNT.ToString() + "\"";
                    if (item.LINKCNT > 0)
                    {
                        json += ",\"LINKREF\":[";
                        for (int i = 0; i < item.LINKCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.LINKREF[i];
                            try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { json += ",\"TYPELINK\":\"" + currItem.TYPELINK.NAME + "\""; } catch { Console.WriteLine("nN TYPELINK"); }
                            try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN ORDERNUM"); }
                            try { json += ",\"LINKINFO\":\"" + currItem.LINKINFO + "\""; } catch { Console.WriteLine("nN LINKINFO"); }
                            //try { json += ",\"URL\":\"" + currItem.URL + "\""; } catch { Console.WriteLine("nN URL"); }
                            //try { json += ",\"USER_CR\":\"" + currItem.USER_CR + "\""; } catch { Console.WriteLine("nN USER_CR"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN RUBRICCNT"); }
                try
                {
                    json += ",\"RUBRICCNT\":\"" + item.RUBRICCNT.ToString() + "\"";
                    if (item.RUBRICCNT > 0)
                    {
                        json += ",\"RUBRIC\":[";
                        for (int i = 0; i < item.RUBRICCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.RUBRIC[i];
                            try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { json += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            try { json += ",\"INDEX\":\"" + currItem.INDEX + "\""; } catch { Console.WriteLine("nN INDEX  "); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN RUBRICCNT"); }
                try
                {
                    json += ",\"ADDPROPSRUBRICCNT\":\"" + item.ADDPROPSRUBRICCNT.ToString() + "\"";
                    if (item.ADDPROPSRUBRICCNT > 0)
                    {
                        json += ",\"ADDPROPSRUBRIC\":[";
                        for (int i = 0; i < item.ADDPROPSRUBRICCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.ADDPROPSRUBRIC[i];
                            try { json += "\"DOCGROUPCNT\":\"" + currItem.DOCGROUPCNT + "\""; } catch { Console.WriteLine("nN ISN"); }
                            try { json += ",\"UINAME\":\"" + currItem.UINAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            try { json += ",\"APINAME\":\"" + currItem.APINAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN ADDPROPSRUBRICCNT"); }
                try { json += ",\"VALUEADDPROPSRUBRIC\":\"" + item.VALUEADDPROPSRUBRIC.ToString() + "\""; } catch { Console.WriteLine("nN VALUEADDPROPSRUBRIC"); }

                try
                {
                    json += ",\"ADDRCNT\":\"" + item.ADDRCNT.ToString() + "\"";
                    if (item.ADDRCNT > 0)
                    {
                        json += ",\"ADDR\":[";
                        for (int i = 0; i < item.ADDRCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.ADDR[i];
                            try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                            //try { json += ",\"PERSON\":\"" + currItem.PERSON + "\""; } catch { Console.WriteLine("nN PERSON"); }
                            //try { json += ",\"REG_N\":\"" + currItem.REG_N + "\""; } catch { Console.WriteLine("nN REG_N"); }
                            //try { json += ",\"NOTE\":\"" + currItem.NOTE + "\""; } catch { Console.WriteLine("nN NOTE"); }
                            //try { json += ",\"REG_DATE\":\"" + currItem.REG_DATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { json += ",\"DATE_UPD\":\"" + currItem.DATE_UPD + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            //try { json += ",\"ANSWER_DATE\":\"" + currItem.ANSWER_DATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { json += ",\"ORIGFLAG\":\"" + currItem.ORIGFLAG + "\""; } catch { Console.WriteLine("nN REG_ORIGFLAG"); }
                            try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN REG_ORDERNUM"); }
                            try { json += ",\"DATE_CR\":\"" + currItem.DATE_CR + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            try { json += ",\"PERSON\":\"" + currItem.PERSON + "\""; } catch { Console.WriteLine("nN REG_DATE"); }
                            //try
                            //{
                            //    json += ",\"DELIVERY\":{";
                            //    try { json += "\"ISN\":\"" + currItem.DELIVERY.ISN + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //    try { json += ",\"NAME\":\"" + currItem.DELIVERY.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //    json += "}";
                            //}
                            //catch { Console.WriteLine("nN DELIVERY"); }
                            try { json += ",\"KINDADDR\":\"" + currItem.KINDADDR + "\""; } catch { Console.WriteLine("nN KINDADDR"); }
                            if (currItem.KINDADDR == "ORGANIZ")
                            {
                                json += ",\"ORGANIZ\":{";
                                try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"FULLNAME\":\"" + currItem.ADDRESSEE.FULLNAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"POSTINDEX\":\"" + currItem.ADDRESSEE.POSTINDEX + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"LAW_ADDRESS\":\"" + currItem.ADDRESSEE.LAW_ADDRESS + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"INN\":\"" + currItem.ADDRESSEE.INN + "\""; } catch { Console.WriteLine("nN NAME"); }
                                //try { json += ",\"OKPO\":\"" + currItem.ADDRESSEE.OKPO + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"CITY\":\"" + currItem.ADDRESSEE.CITY + "\""; } catch { Console.WriteLine("nN NAME"); }
                                //try { json += ",\"OKONH\":\"" + currItem.ADDRESSEE.OKONH + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                try { json += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL"); }
                                json += "}";
                            }
                            if (currItem.KINDADDR == "CITIZEN")
                            {
                                json += ",\"CITIZEN\":{";
                                try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN REGION"); }
                                try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                try { json += ",\"REGION\":\"" + currItem.ADDRESSEE.REGION.NAME + "\""; } catch { Console.WriteLine("nN REGION"); }
                                try { json += ",\"CITY\":\"" + currItem.ADDRESSEE.CITY + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                try { json += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL"); }
                                json += "}";

                            }
                            if (currItem.KINDADDR == "DEPARTMENT")
                            {
                                json += ",\"DEPARTMENT\":{";
                                try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN REGION"); }
                                try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"ADDRESS\":\"" + currItem.ADDRESSEE.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                try { json += ",\"EMAIL\":\"" + currItem.ADDRESSEE.EMAIL + "\""; } catch { Console.WriteLine("nN EMAIL"); }
                                json += "}";
                            }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN ADDRCNT"); }
                try
                {
                    json += ",\"FILESCNT\":\"" + item.FILESCNT.ToString() + "\"";
                    if (item.PROTCNT > 0)
                    {
                        json += ",\"FILES\":[";
                        for (int i2 = 0; i2 < item.FILESCNT; i2++)
                        {
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
                }
                catch { Console.WriteLine("nN FILESCNT"); }
                try
                {
                    json += ",\"JOURNACQCNT\":\"" + item.JOURNACQCNT.ToString() + "\"";
                    if (item.JOURNACQCNT > 0)
                    {
                        json += ",\"JOURNACQ\":[";
                        for (int i2 = 0; i2 < item.JOURNACQCNT; i2++)
                        {
                            if (i2 != 0) { json += ",{"; } else { json += "{"; }
                            var currItem2 = item.JOURNACQ[i2];
                            try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                            try { json += "\"EMPLOY\":\"" + currItem2.EMPLOY.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.ISN"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN JOURNACQCNT"); }
                try { json += ",\"NUM_FLAG\":\"" + item.NUM_FLAG.ToString() + "\""; } catch { Console.WriteLine("nN NUM_FLAG"); }
                try
                {
                    json += ",\"PROTCNT\":\"" + item.PROTCNT.ToString() + "\"";
                    if (item.PROTCNT > 0)
                    {
                        json += ",\"PROTOCOL\":[";
                        for (int i2 = 0; i2 < item.PROTCNT; i2++)
                        {
                            if (i2 != 0) { json += ",{"; } else { json += "{"; }
                            var currItem2 = item.PROTOCOL[i2];
                            try { json += "\"WHEN\":\"" + currItem2.WHEN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                            try { json += "\"WHAT\":\"" + currItem2.WHAT.Replace("\"", "&quot;").ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.ISN"); }

                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN PROTCNT"); }
                try { json += ",\"CARDVIEW\":\"" + item.CARDVIEW.ToString() + "\""; } catch { Console.WriteLine("nN CARDVIEW"); }
                try { json += ",\"ALLRESOL\":\"" + item.ALLRESOL.ToString() + "\""; } catch { Console.WriteLine("nN ALLRESOL"); }
                try
                {
                    json += ",\"RESOLCNT\":\"" + item.RESOLCNT.ToString() + "\"";
                    if (item.RESOLCNT > 0)
                    {
                        json += ",\"RESOLUTION\":[";
                        for (int i = 0; i < item.RESOLCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.RESOLUTION[i];
                            //json += "\"AUTHOR\":{";
                            //try { json += "\"ISN\":\"" + currItem.AUTHOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                            //try { json += ",\"NAME\":\"" + currItem.AUTHOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                            //try { json += ",\"SURNAME\":\"" + currItem.AUTHOR.SURNAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                            //json += "}";
                            //try { json += ",\"TEXT\":\"" + currItem.TEXT.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                            //try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }
                            //try { json += ",\"PLANDATE\":\"" + currItem.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("nPLANDATE"); }
                            ////try { json += ",\"templ_MIDDATE\":\"" + currItem.MIDDATE.ToString() + "\""; } catch { Console.WriteLine("nMIDDATE"); }
                            ////try { json += ",\"templ_RESPONS\":\"" + currItem.RESPONS.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                            ////try { json += ",\"templ_RESPONS.NAME\":\"" + currItem.RESPONS.NAME.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                            ////try { json += ",\"templ_RESPONS.ISN\":\"" + currItem.RESPONS.ISN.ToString() + "\""; } catch { Console.WriteLine("nRESPONS"); }
                            //try { json += ",\"REPLYCNT\":\"" + currItem.REPLYCNT.ToString() + "\""; } catch { Console.WriteLine("nRESOLCNT"); }
                            //if (currItem.REPLYCNT > 0)
                            //{
                            //    json += ",\"REPLY\":[";
                            //    for (int i2 = 0; i2 < currItem.REPLYCNT; i2++)
                            //    {
                            //        if (i2 != 0) { json += ",{"; } else { json += "{"; }
                            //        var currItem2 = currItem.REPLY[i];
                            //        try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REPLY.ISN"); }
                            //       // try { json += ",\"TEXT\":\"" + currItem2.TEXT.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                            //        json += ",\"EXECUTOR\":{";
                            //            try { json += "\"ISN\":\"" + currItem2.EXECUTOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REGION"); }
                            //            try { json += ",\"NAME\":\"" + currItem2.EXECUTOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //        json += "}";
                            //        json += "}";
                            //    }
                            //    json += "]";
                            //}
                            try { json += ",\"RESOLCNT\":\"" + currItem.RESOLCNT.ToString() + "\""; } catch { Console.WriteLine("nRESOLCNT"); }
                            if (currItem.RESOLCNT > 0)
                            {
                                json += ",\"RESOLUTION\":[";
                                for (int i2 = 0; i2 < currItem.RESOLCNT; i2++)
                                {
                                    if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem2 = currItem.RESOLUTION[i];
                                    try { json += "\"AUTHOR_NAME\":\"" + currItem2.AUTHOR.NAME.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                    try { json += "\"AUTHOR_ISN\":\"" + currItem2.AUTHOR.ISN.ToString() + "\","; } catch { Console.WriteLine("nN AUTHOR.ISN"); }
                                    try { json += "\"templ_SENDDATE\":\"" + currItem2.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }

                                    json += "}";
                                }
                                json += "]";
                            }
                            //json += "}";
                            try { json += ",\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("ISN"); }
                            try { json += ",\"RCKIND\":\"" + currItem.RCKIND.ToString() + "\""; } catch { Console.WriteLine("WEIGHT"); }
                            try { json += ",\"RC\":\"" + currItem.RC.ToString() + "\""; } catch { Console.WriteLine("WEIGHT"); }
                            try { json += ",\"KIND\":\"" + currItem.KIND.ToString() + "\""; } catch { Console.WriteLine("WEIGHT"); }
                            try { json += ",\"PARENT\":\"" + currItem.PARENT.ToString() + "\""; } catch { Console.WriteLine("WEIGHT"); }
                            try { json += ",\"WEIGHT\":\"" + currItem.WEIGHT.ToString() + "\""; } catch { Console.WriteLine("WEIGHT"); }
                            try { json += ",\"LEVEL\":\"" + currItem.LEVEL.ToString() + "\""; } catch { Console.WriteLine("LEVEL"); }
                            try { json += ",\"ITEMNUM\":\"" + currItem.ITEMNUM.ToString() + "\""; } catch { Console.WriteLine("ITEMNUM"); }
                            try
                            {
                                json += ",\"AUTHOR\":{";
                                try { json += "\"ISN\":\"" + currItem.AUTHOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                                try { json += ",\"NAME\":\"" + currItem.AUTHOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                                try { json += ",\"SURNAME\":\"" + currItem.AUTHOR.SURNAME.ToString() + "\""; } catch { Console.WriteLine("nTEXT"); }
                                json += "}";
                            }
                            catch { Console.WriteLine("AUTHOR"); }
                            try { json += ",\"TEXT\":\"" + currItem.TEXT.ToString() + "\""; } catch { Console.WriteLine("TEXT"); }
                            try { json += ",\"RESOLDATE\":\"" + currItem.RESOLDATE.ToString() + "\""; } catch { Console.WriteLine("RESOLDATE"); }
                            try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("SENDDATE"); }
                            json += ",\"INSPECTOR\":{";
                            try { json += ",\"DCODE\":\"" + currItem.INSPECTOR.DCODE.ToString() + "\""; } catch { Console.WriteLine("nNDCODE"); }
                            try { json += ",\"ISN\":\"" + currItem.INSPECTOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                            try { json += ",\"PARENT\":\"" + currItem.INSPECTOR.PARENT.ToString() + "\""; } catch { Console.WriteLine("nNPARENT"); }
                            try { json += ",\"LAYER\":\"" + currItem.INSPECTOR.LAYER.ToString() + "\""; } catch { Console.WriteLine("nNLAYER"); }
                            try { json += ",\"ISNODE\":\"" + currItem.INSPECTOR.ISNODE.ToString() + "\""; } catch { Console.WriteLine("nNISNODE"); }
                            try { json += ",\"WEIGHT\":\"" + currItem.INSPECTOR.WEIGHT.ToString() + "\""; } catch { Console.WriteLine("nNWEIGHT"); }
                            try { json += ",\"NAME\":\"" + currItem.INSPECTOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nNNAME"); }
                            try { json += ",\"SURNAME\":\"" + currItem.INSPECTOR.SURNAME.ToString() + "\""; } catch { Console.WriteLine("nNSURNAME"); }
                            try { json += ",\"CABINET\":\"" + currItem.INSPECTOR.CABINET.ToString() + "\""; } catch { Console.WriteLine("nNCABINET"); }
                            try { json += ",\"POST\":\"" + currItem.INSPECTOR.POST.ToString() + "\""; } catch { Console.WriteLine("nNPOST"); }
                            try { json += ",\"CARDINDEX\":\"" + currItem.INSPECTOR.CARDINDEX.ToString() + "\""; } catch { Console.WriteLine("nNCARDINDEX"); }
                            try { json += ",\"DELETED\":\"" + currItem.INSPECTOR.DELETED.ToString() + "\""; } catch { Console.WriteLine("nNDELETED"); }
                            try { json += ",\"STARTDATE\":\"" + currItem.INSPECTOR.STARTDATE.ToString() + "\""; } catch { Console.WriteLine("nNSTARTDATE"); }
                            try { json += ",\"ENDDATE\":\"" + currItem.INSPECTOR.ENDDATE.ToString() + "\""; } catch { Console.WriteLine("nNENDDATE"); }
                            try { json += ",\"INDEX\":\"" + currItem.INSPECTOR.INDEX.ToString() + "\""; } catch { Console.WriteLine("nNINDEX"); }
                            try { json += ",\"ISCHIEF\":\"" + currItem.INSPECTOR.ISCHIEF.ToString() + "\""; } catch { Console.WriteLine("nNISCHIEF"); }
                            try { json += ",\"ISCARD\":\"" + currItem.INSPECTOR.ISCARD.ToString() + "\""; } catch { Console.WriteLine("nNISCARD"); }
                            try { json += ",\"ORGANIZ\":\"" + currItem.INSPECTOR.ORGANIZ.ToString() + "\""; } catch { Console.WriteLine("nNORGANIZ"); }
                            try { json += ",\"CONTACT\":\"" + currItem.INSPECTOR.CONTACT.ToString() + "\""; } catch { Console.WriteLine("nNCONTACT"); }
                            try { json += ",\"NOTE\":\"" + currItem.INSPECTOR.NOTE.ToString() + "\""; } catch { Console.WriteLine("nNNOTE"); }
                            try { json += ",\"CABCNT\":\"" + currItem.INSPECTOR.CABCNT.ToString() + "\""; } catch { Console.WriteLine("nNCABCNT"); }
                            try { json += ",\"CAB\":\"" + currItem.INSPECTOR.CAB.ToString() + "\""; } catch { Console.WriteLine("nNCAB"); }
                            try { json += ",\"CBPrintInfo\":\"" + currItem.INSPECTOR.CBPrintInfo.ToString() + "\""; } catch { Console.WriteLine("nNCBPrintInfo"); }
                            try { json += ",\"CBPrintInfoExist\":\"" + currItem.INSPECTOR.CBPrintInfoExist.ToString() + "\""; } catch { Console.WriteLine("nNCBPrintInfoExist"); }
                            try { json += ",\"ERRCODE\":\"" + currItem.INSPECTOR.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                            try { json += ",\"ERRTEXT\":\"" + currItem.INSPECTOR.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nNERRTEXT"); }
                            json += "}";
                            try { json += ",\"ACCEPTFLAG\":\"" + currItem.ACCEPTFLAG.ToString() + "\""; } catch { Console.WriteLine("ACCEPTFLAG"); }
                            try { json += ",\"ISCONTROL\":\"" + currItem.ISCONTROL.ToString() + "\""; } catch { Console.WriteLine("ISCONTROL"); }
                            try { json += ",\"ISPRIVATE\":\"" + currItem.ISPRIVATE.ToString() + "\""; } catch { Console.WriteLine("ISPRIVATE"); }
                            try { json += ",\"CANVIEW\":\"" + currItem.CANVIEW.ToString() + "\""; } catch { Console.WriteLine("CANVIEW"); }
                            try { json += ",\"ISCASCADE\":\"" + currItem.ISCASCADE.ToString() + "\""; } catch { Console.WriteLine("ISCASCADE"); }
                            try { json += ",\"PLANDATE\":\"" + currItem.PLANDATE.ToString() + "\""; } catch { Console.WriteLine("PLANDATE"); }
                            try { json += ",\"MIDDATE\":\"" + currItem.MIDDATE.ToString() + "\""; } catch { Console.WriteLine("MIDDATE"); }
                            try { json += ",\"FACTDATE\":\"" + currItem.FACTDATE.ToString() + "\""; } catch { Console.WriteLine("FACTDATE"); }
                            try { json += ",\"DELTA\":\"" + currItem.DELTA.ToString() + "\""; } catch { Console.WriteLine("DELTA"); }
                            try { json += ",\"SUMMARY\":\"" + currItem.SUMMARY.ToString() + "\""; } catch { Console.WriteLine("SUMMARY"); }
                            try { json += ",\"REASON\":\"" + currItem.REASON.ToString() + "\""; } catch { Console.WriteLine("REASON"); }
                            //try{json += ",\"STATION\":{";
                            //                                try { json += "\"ISN\":\"" + currItem.STATION.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REGION"); }
                            //                                try { json += ",\"NAME\":\"" + currItem.STATION.NAME.ToString() + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //                                try { json += ",\"ERRCODE\":\"" + currItem.STATION.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //                                try { json += ",\"ERRTEXT\":\"" + currItem.STATION.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nN NAME"); }
                            //                                json += "}";
                            //                            } catch{Console.WriteLine("STATION");}
                            try { json += ",\"EXECFLAG\":\"" + currItem.EXECFLAG.ToString() + "\""; } catch { Console.WriteLine("EXECFLAG"); }
                            try { json += ",\"SENDFLAG\":\"" + currItem.SENDFLAG.ToString() + "\""; } catch { Console.WriteLine("SENDFLAG"); }
                            try
                            {
                                json += ",\"RESPONS\":{";
                                try { json += ",\"DCODE\":\"" + currItem.RESPONS.DCODE.ToString() + "\""; } catch { Console.WriteLine("nNDCODE"); }
                                try { json += ",\"ISN\":\"" + currItem.RESPONS.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                                try { json += ",\"PARENT\":\"" + currItem.RESPONS.PARENT.ToString() + "\""; } catch { Console.WriteLine("nNPARENT"); }
                                try { json += ",\"LAYER\":\"" + currItem.RESPONS.LAYER.ToString() + "\""; } catch { Console.WriteLine("nNLAYER"); }
                                try { json += ",\"ISNODE\":\"" + currItem.RESPONS.ISNODE.ToString() + "\""; } catch { Console.WriteLine("nNISNODE"); }
                                try { json += ",\"WEIGHT\":\"" + currItem.RESPONS.WEIGHT.ToString() + "\""; } catch { Console.WriteLine("nNWEIGHT"); }
                                try { json += ",\"NAME\":\"" + currItem.RESPONS.NAME.ToString() + "\""; } catch { Console.WriteLine("nNNAME"); }
                                try { json += ",\"SURNAME\":\"" + currItem.RESPONS.SURNAME.ToString() + "\""; } catch { Console.WriteLine("nNSURNAME"); }
                                try { json += ",\"CABINET\":\"" + currItem.RESPONS.CABINET.ToString() + "\""; } catch { Console.WriteLine("nNCABINET"); }
                                try { json += ",\"POST\":\"" + currItem.RESPONS.POST.ToString() + "\""; } catch { Console.WriteLine("nNPOST"); }
                                try { json += ",\"CARDINDEX\":\"" + currItem.RESPONS.CARDINDEX.ToString() + "\""; } catch { Console.WriteLine("nNCARDINDEX"); }
                                try { json += ",\"DELETED\":\"" + currItem.RESPONS.DELETED.ToString() + "\""; } catch { Console.WriteLine("nNDELETED"); }
                                try { json += ",\"STARTDATE\":\"" + currItem.RESPONS.STARTDATE.ToString() + "\""; } catch { Console.WriteLine("nNSTARTDATE"); }
                                try { json += ",\"ENDDATE\":\"" + currItem.RESPONS.ENDDATE.ToString() + "\""; } catch { Console.WriteLine("nNENDDATE"); }
                                try { json += ",\"INDEX\":\"" + currItem.RESPONS.INDEX.ToString() + "\""; } catch { Console.WriteLine("nNINDEX"); }
                                try { json += ",\"ISCHIEF\":\"" + currItem.RESPONS.ISCHIEF.ToString() + "\""; } catch { Console.WriteLine("nNISCHIEF"); }
                                try { json += ",\"ISCARD\":\"" + currItem.RESPONS.ISCARD.ToString() + "\""; } catch { Console.WriteLine("nNISCARD"); }
                                try { json += ",\"ORGANIZ\":\"" + currItem.RESPONS.ORGANIZ.ToString() + "\""; } catch { Console.WriteLine("nNORGANIZ"); }
                                try { json += ",\"CONTACT\":\"" + currItem.RESPONS.CONTACT.ToString() + "\""; } catch { Console.WriteLine("nNCONTACT"); }
                                try { json += ",\"NOTE\":\"" + currItem.RESPONS.NOTE.ToString() + "\""; } catch { Console.WriteLine("nNNOTE"); }
                                try { json += ",\"CABCNT\":\"" + currItem.RESPONS.CABCNT.ToString() + "\""; } catch { Console.WriteLine("nNCABCNT"); }
                                try { json += ",\"CAB\":\"" + currItem.RESPONS.CAB.ToString() + "\""; } catch { Console.WriteLine("nNCAB"); }
                                try { json += ",\"CBPrintInfo\":\"" + currItem.RESPONS.CBPrintInfo.ToString() + "\""; } catch { Console.WriteLine("nNCBPrintInfo"); }
                                try { json += ",\"CBPrintInfoExist\":\"" + currItem.RESPONS.CBPrintInfoExist.ToString() + "\""; } catch { Console.WriteLine("nNCBPrintInfoExist"); }
                                try { json += ",\"ERRCODE\":\"" + currItem.RESPONS.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                                try { json += ",\"ERRTEXT\":\"" + currItem.RESPONS.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nNERRTEXT"); }
                                json += "}";
                            }
                            catch { Console.WriteLine("RESPONS"); }
                            try { json += ",\"CATEGORY\":\"" + currItem.CATEGORY.ToString() + "\""; } catch { Console.WriteLine("CATEGORY"); }
                            try { json += ",\"ISN_RESPRJ_STATUS\":\"" + currItem.ISN_RESPRJ_STATUS.ToString() + "\""; } catch { Console.WriteLine("ISN_RESPRJ_STATUS"); }
                            try { json += ",\"RESPRJPRIORITY\":\"" + currItem.RESPRJPRIORITY.NAME.ToString() + "\""; } catch { Console.WriteLine("RESPRJPRIORITY"); }
                            try { json += ",\"NOTE\":\"" + currItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("NOTE"); }
                            try
                            {
                                json += ",\"CARDCNT\":\"" + currItem.CARDCNT.ToString() + "\"";
                                if (currItem.CARDCNT > 0)
                                {
                                    json += ",\"RESCARD\":[";
                                    for (int i2 = 0; i2 < currItem.CARDCNT; i2++)
                                    {
                                        if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                        var currItem2 = currItem.RESCARD[i];
                                        try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REPLY.ISN"); }
                                        try { json += ",\"NAME\":\"" + currItem2.NAME.ToString() + "\""; } catch { Console.WriteLine("nNNAME"); }
                                        try { json += ",\"ERRCODE\":\"" + currItem2.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNCARDINDEX"); }
                                        json += "}";
                                    }
                                    json += "]";
                                }
                            }
                            catch { Console.WriteLine("CARDCNT"); }

                            //try{json+=",\"REPLYCNT\":\""+currItem.REPLYCNT.ToString()+"\"";}catch{Console.WriteLine("REPLYCNT");}
                            //try{json+=",\"REPLY\":\""+currItem.REPLY.ToString()+"\"";}catch{Console.WriteLine("REPLY");}
                            try { json += ",\"REPLYCNT\":\"" + currItem.REPLYCNT.ToString() + "\""; } catch { Console.WriteLine("nRESOLCNT"); }
                            if (currItem.REPLYCNT > 0)
                            {
                                json += ",\"REPLY\":[";
                                for (int i2 = 0; i2 < currItem.REPLYCNT; i2++)
                                {
                                    if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                    var currItem2 = currItem.REPLY[i];
                                    try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REPLY.ISN"); }
                                    // try { json += ",\"TEXT\":\"" + currItem2.TEXT.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                    json += ",\"EXECUTOR\":{";
                                    try { json += "\"ISN\":\"" + currItem2.EXECUTOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nN REGION"); }
                                    try { json += ",\"NAME\":\"" + currItem2.EXECUTOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nN NAME"); }
                                    try { json += ",\"ERRCODE\":\"" + currItem.RESPONS.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                                    json += "}";
                                    try { json += ",\"ERRCODE\":\"" + currItem.RESPONS.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                                    json += "}";
                                }
                                json += "]";
                            }
                            try { json += ",\"MUSTRETURN\":\"" + currItem.MUSTRETURN.ToString() + "\""; } catch { Console.WriteLine("MUSTRETURN"); }
                            try
                            {
                                json += ",\"RESOLCNT\":\"" + currItem.RESOLCNT.ToString() + "\"";

                                if (currItem.RESOLCNT > 0)
                                {
                                    json += ",\"RESOLUTION\":[";
                                    for (int i2 = 0; i2 < currItem.RESOLCNT; i2++)
                                    {
                                        Console.WriteLine("i is " + i + " and i2 is " + i2);
                                        if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                        var currItem2 = currItem.RESOLUTION[i];
                                        try { json += "\"AUTHOR_NAME\":\"" + currItem2.AUTHOR.NAME.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                        try { json += ",\"AUTHOR_ISN\":\"" + currItem2.AUTHOR.ISN.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR.ISN"); }
                                        try { json += ",\"SENDDATE\":\"" + currItem2.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }
                                        try { json += ",\"ERRCODE\":\"" + currItem2.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }

                                        json += "}";
                                    }
                                    json += "]";
                                }

                            }
                            catch { Console.WriteLine("nRESOLCNT"); }
                            try
                            {
                                json += ",\"PROTCNT\":\"" + currItem.PROTCNT.ToString() + "\"";
                                if (currItem.PROTCNT > 0)
                                {
                                    json += ",\"PROTOCOL\":[";
                                    for (int i2 = 0; i2 < currItem.PROTCNT; i2++)
                                    {
                                        Console.WriteLine("i is " + i + " and i2 is " + i2);
                                        if (i2 != 0) { json += ",{"; } else { json += "{"; }
                                        var currItem2 = currItem.PROTOCOL[i];
                                        try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                                        try { json += ",\"ERRCODE\":\"" + currItem2.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nSENDDATE"); }
                                        json += "}";
                                    }
                                    json += "]";
                                }
                            }
                            catch { Console.WriteLine("PROTCNT"); }
                            try { json += ",\"USER_CR\":\"" + currItem.USER_CR.ToString() + "\""; } catch { Console.WriteLine("USER_CR"); }
                            try { json += ",\"DATE_CR\":\"" + currItem.DATE_CR.ToString() + "\""; } catch { Console.WriteLine("DATE_CR"); }
                            try { json += ",\"DATE_UPD\":\"" + currItem.DATE_UPD.ToString() + "\""; } catch { Console.WriteLine("DATE_UPD"); }
                            try { json += ",\"ERRCODE\":\"" + currItem.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("ERRCODE"); }
                            try { json += ",\"ERRTEXT\":\"" + currItem.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("ERRTEXTtry"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN RESOLCNT"); }
                try
                {
                    json += ",\"JOURNALCNT\":\"" + item.JOURNALCNT.ToString() + "\"";
                    if (item.JOURNALCNT > 0)
                    {
                        json += ",\"JOURNAL\":[";
                        for (int i = 0; i < item.JOURNALCNT; i++)
                        {
                            if (i != 0) { json += ",{"; } else { json += "{"; }
                            var currItem = item.JOURNAL[i];
                            try { json += "\"ISN\":\"" + currItem.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                            //try { json += ",\"RCKIND\":\"" + currItem.RCKIND.ToString() + "\""; } catch { Console.WriteLine("nNRCKIND"); }
                            //try { json += ",\"KIND\":\"" + currItem.KIND.ToString() + "\""; } catch { Console.WriteLine("nNKIND"); }
                            json += ",\"ADDRESSEE\":{";
                            try { json += "\"ISN\":\"" + currItem.ADDRESSEE.ISN + "\""; } catch { Console.WriteLine("nN REGION"); }
                            try { json += ",\"NAME\":\"" + currItem.ADDRESSEE.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                            json += "}";
                            try { json += ",\"ORIGFLAG\":\"" + currItem.ORIGFLAG.ToString() + "\""; } catch { Console.WriteLine("nNORIGFLAG"); }
                            try { json += ",\"SENDDATE\":\"" + currItem.SENDDATE.ToString() + "\""; } catch { Console.WriteLine("nNSENDDATE"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN JOURNALCNT"); }
                try
                {
                    json += ",\"FORWARDCNT\":\"" + item.FORWARDCNT.ToString() + "\"";
                    if (item.FORWARDCNT > 0)
                    {
                        json += ",\"FORWARD\":[";
                        for (int i2 = 0; i2 < item.FORWARDCNT; i2++)
                        {
                            if (i2 != 0) { json += ",{"; } else { json += "{"; }
                            var currItem2 = item.FORWARD[i2];
                            try { json += "\"ISN\":\"" + currItem2.ISN.ToString() + "\""; } catch { Console.WriteLine("nN AUTHOR_NAME"); }
                            //try
                            //{
                            //    json += ",\"AUTHORCARD\":{";
                            //    try { json += "\"ISN\":\"" + currItem2.AUTHORCARD.ISN.ToString() + "\""; } catch { }
                            //    try { json += ",\"NAME\":\"" + currItem2.AUTHORCARD.NAME.ToString() + "\""; } catch { }
                            //    try { json += ",\"STARTDATE\":\"" + currItem2.AUTHORCAB.STARTDATE.ToString() + "\""; } catch { }
                            //    json += "}";
                            //}
                            //catch { }
                            //try
                            //{
                            //    json += ",\"AUTHORCAB\":{";
                            //    try { json += "\"ISN\":\"" + currItem2.AUTHORCAB.ISN.ToString() + "\""; } catch { }
                            //    try { json += ",\"NAME\":\"" + currItem2.AUTHORCAB.NAME.ToString() + "\""; } catch { }
                            //    try { json += ",\"STARTDATE\":\"" + currItem2.AUTHORCAB.STARTDATE.ToString() + "\""; } catch { }
                            //    json += "}";
                            //}
                            //catch { }
                            try
                            {
                                json += ",\"ADDRESSEE\":{";
                                try { json += "\"ISN\":\"" + currItem2.ADDRESSEE.ISN.ToString() + "\""; } catch { }
                                try { json += ",\"NAME\":\"" + currItem2.ADDRESSEE.NAME.ToString() + "\""; } catch { }
                                //try { json += ",\"SURNAME\":\"" + currItem2.ADDRESSEE.SURNAME.ToString() + "\""; } catch { }
                                //try { json += ",\"POST\":\"" + currItem2.ADDRESSEE.POST.ToString() + "\""; } catch { }
                                //try { json += ",\"STARTDATE\":\"" + currItem2.ADDRESSEE.STARTDATE.ToString() + "\""; } catch { }
                                //try { json += ",\"ENDDATE\":\"" + currItem2.ADDRESSEE.ENDDATE.ToString() + "\""; } catch { }
                                json += "}";
                            }
                            catch { }
                            try
                            {
                                json += ",\"USER\":{";
                                try { json += "\"ISN\":\"" + currItem2.USER.ISN.ToString() + "\""; } catch { }
                                try { json += ",\"NAME\":\"" + currItem2.USER.NAME.ToString() + "\""; } catch { }
                                //try { json += ",\"ENDDATE\":\"" + currItem2.USER.ENDDATE.ToString() + "\""; } catch { }
                                //try { json += ",\"DEPART\":\"" + currItem2.USER.DEPART.ToString() + "\""; } catch { }
                                json += "}";
                            }
                            catch { }
                            try { json += ",\"STARTDATE\":\"" + currItem2.STARTDATE.ToString() + "\""; } catch { }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN FORWARDCNT"); }
                try
                {
                    json += ",\"ADDPROPSCNT\":\"" + item.JOURNACQCNT.ToString() + "\"";
                    if (item.ADDPROPSCNT > 0)
                    {
                        json += ",\"ADDPROPS\":[";
                        for (int i2 = 0; i2 < item.ADDPROPSCNT; i2++)
                        {
                            if (i2 != 0) { json += ",{"; } else { json += "{"; }
                            var currItem2 = item.ADDPROPS[i2];

                            try
                            {
                                json += "\"DOCGROUPCNT\":\"" + currItem2.DOCGROUPCNT.ToString() + "\"";
                                //if (currItem2.DOCGROUPCNT > 0)
                                //{
                                //    json += ",\"DOCGROUP\":[";
                                //    for (int i = 0; i < currItem2.DOCGROUPCNT; i++)
                                //    {
                                //        if (i != 0) { json += ",{"; } else { json += "{"; }
                                //        var currItem3 = currItem2.DOCGROUP[i];
                                //        try{ json += "\"ISN\":\"" + currItem3.ISN.ToString() + "\""; } catch { Console.WriteLine("nNISN"); }
                                //        try{json += ",\"NAME\":\"" + currItem3.NAME.ToString() + "\"";} catch { Console.WriteLine("nNISN"); }
                                //    json += "}";
                                //    }
                                //    json += "]";
                                //}
                            }
                            catch { Console.WriteLine("nN DOCGROUPCNT"); }
                            //try { json += ",\"OWNER\":\"" + currItem2.OWNER + "\""; } catch { Console.WriteLine("nNOWNER"); }
                            try { json += ",\"UINAME\":\"" + currItem2.UINAME + "\""; } catch { Console.WriteLine("nNUINAME"); }
                            try { json += ",\"APINAME\":\"" + currItem2.APINAME + "\""; } catch { Console.WriteLine("nNAPINAME"); }
                            try { json += ",\"TYPE\":\"" + currItem2.TYPE + "\""; } catch { Console.WriteLine("nNTYPE"); }
                            //try { json += ",\"MAXLEN\":\"" + currItem2.MAXLEN + "\""; } catch { Console.WriteLine("nNMAXLEN"); }
                            //try { json += ",\"PRECISION\":\"" + currItem2.PRECISION + "\""; } catch { Console.WriteLine("nNPRECISION"); }
                            //try { json += ",\"ERRCODE\":\"" + currItem2.ERRCODE + "\""; } catch { Console.WriteLine("nNERRCODE"); }
                            //try { json += ",\"ERRTEXT\":\"" + currItem2.ERRTEXT + "\""; } catch { Console.WriteLine("nNERRTEXT"); }
                            json += "}";
                        }
                        json += "]";
                    }
                }
                catch { Console.WriteLine("nN ADDPROPSCNT"); }
                try { json += ",\"VALUEADDPROPS\":\"" + item.VALUEADDPROPS.ToString() + "\""; } catch { Console.WriteLine("nN VALUEADDPROPS"); }
                try { json += ",\"USER_CR\":\"" + item.USER_CR.ToString() + "\""; } catch { Console.WriteLine("nN USER_CR"); }
                try { json += ",\"DATE_CR\":\"" + item.DATE_CR.ToString() + "\""; } catch { Console.WriteLine("nN DATE_CR"); }
                try { json += ",\"RUBRIC_first\":\"" + item.RUBRIC.ToString() + "\""; } catch { Console.WriteLine("nN RUBRIC"); }
                try { json += ",\"CARD_first\":\"" + item.CARD.ToString() + "\""; } catch { Console.WriteLine("nN CARD"); }
                try { json += ",\"ADDR_first\":\"" + item.ADDR.ToString() + "\""; } catch { Console.WriteLine("nN ADDR"); }
                try { json += ",\"JOURNAL_first\":\"" + item.JOURNAL.ToString() + "\""; } catch { Console.WriteLine("nN JOURNAL"); }
                try { json += ",\"RESOL\":\"" + item.RESOL.ToString() + "\""; } catch { Console.WriteLine("nN RESOL"); }
                try { json += ",\"JOURNACQ_first\":\"" + item.JOURNACQ.ToString() + "\""; } catch { Console.WriteLine("nN JOURNACQ"); }
                try { json += ",\"ADDPROPS_first\":\"" + item.ADDPROPS.ToString() + "\""; } catch { Console.WriteLine("nN ADDPROPS"); }
                try { json += ",\"ADDPROPSRUBRIC_first\":\"" + item.ADDPROPSRUBRIC.ToString() + "\""; } catch { Console.WriteLine("nN ADDPROPSRUBRIC"); }
                try { json += ",\"PROTOCOL_first\":\"" + item.PROTOCOL.ToString() + "\""; } catch { Console.WriteLine("nN PROTOCOL"); }
                try { json += ",\"FORWARD_first\":\"" + item.FORWARD.ToString() + "\""; } catch { Console.WriteLine("nN FORWARD"); }
                try { json += ",\"ERRCODE\":\"" + item.ERRCODE.ToString() + "\""; } catch { Console.WriteLine("nN ERRCODE"); }
                try { json += ",\"ERRTEXT\":\"" + item.ERRTEXT.ToString() + "\""; } catch { Console.WriteLine("nN ERRTEXT"); }
                if ((rcType == "RCIN") || (rcType == "RCLET"))
                {
                    try { json += ",\"DOCKIND\":\"" + item.DOCKIND.ToString() + "\""; } catch { Console.WriteLine("nN DOCKIND"); }
                    try { json += ",\"ADDRESSEE\":\"" + item.ADDRESSEE.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESSEE"); }
                    try
                    {
                        json += ",\"ADDRESSESCNT\":\"" + item.ADDRESSESCNT.ToString() + "\"";
                        if (item.ADDRESSESCNT > 0)
                        {
                            json += ",\"ADDRESSES\":[";
                            for (int i = 0; i < item.ADDRESSESCNT; i++)
                            {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.ADDRESSES[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                try { json += ",\"NAME\":\"" + currItem.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                try { json += ",\"SURNAME\":\"" + currItem.SURNAME + "\""; } catch { Console.WriteLine("nN SURNAME"); }
                                try { json += ",\"POST\":\"" + currItem.POST + "\""; } catch { Console.WriteLine("nN POST"); }
                                json += "}";
                            }
                            json += "]";
                        }
                    }
                    catch { Console.WriteLine("nN ADDRESSESCNT"); }
                    try
                    {
                        json += ",\"DELIVERY\":{";
                        try { json += "\"ISN\":\"" + item.DELIVERY.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                        try { json += ",\"NAME\":\"" + item.DELIVERY.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                        json += "}";
                    }
                    catch { Console.WriteLine("nN DELIVERY"); }

                    try { json += ",\"TELEGRAM\":\"" + item.TELEGRAM.ToString() + "\""; } catch { Console.WriteLine("nN TELEGRAM"); }
                    try
                    {
                        json += ",\"CORRESPCNT\":\"" + item.CORRESPCNT.ToString() + "\"";
                        if (item.CORRESPCNT > 0)
                        {
                            json += ",\"CORRESP\":[";
                            for (int i = 0; i < item.CORRESPCNT; i++)
                            {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.CORRESP[i];
                                if (currItem.KIND == 1)
                                {
                                    try { json += "\"ISN\":\"" + currItem.ORGANIZ.ISN + "\""; } catch { Console.WriteLine("nN ORGANIZ.ISN"); }
                                    try { json += ",\"ORGANIZ\":\"" + currItem.ORGANIZ.NAME.Replace("\"", "&quot;") + "\""; } catch { Console.WriteLine("nN ORGANIZ.NAME"); }
                                    try { json += ",\"OUTNUM\":\"" + currItem.OUTNUM.ToString() + "\""; } catch { Console.WriteLine("nN OUTNUM"); }
                                    try { json += ",\"OUTDATE\":\"" + currItem.OUTDATE.ToString() + "\""; } catch { Console.WriteLine("nN OUTDATE"); }
                                    try { json += ",\"SIGN\":\"" + currItem.SIGN.ToString() + "\""; } catch { Console.WriteLine("nN SIGN"); }
                                    try { json += ",\"NOTE\":\"" + currItem.NOTE.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }
                                }
                                else { /*Вид записи: 1 – информация о Корреспонденте документа; 2 – информация о сопроводительном документа; Записи первого вида могут принадлежать только РК вида "Входящий" от организаций, второго вида – как "Входящим" от организаций так и "Письмам" от физических лиц.*/}

                                json += "}";
                            }
                            json += "]";
                        }
                    }
                    catch { Console.WriteLine("nN CORRESPCNT"); }
                    try { json += ",\"LINKS\":\"" + item.LINKS.ToString() + "\""; } catch { Console.WriteLine("nN LINKS"); }
                    try { json += ",\"ADDRESSESList\":\"" + item.ADDRESSES.ToString() + "\""; } catch { Console.WriteLine("nN ADDRESSES"); }
                }
                if (rcType == "RCLET")
                {
                    try { json += ",\"ISCOLLECTIVE\":\"" + item.ISCOLLECTIVE.ToString() + "\""; } catch { Console.WriteLine("nN ISCOLLECTIVE"); }
                    try { json += ",\"ISANONIM\":\"" + item.ISANONIM.ToString() + "\""; } catch { Console.WriteLine("nN ISANONIM"); }
                    try
                    {
                        json += ",\"AUTHORCNT\":\"" + item.AUTHORCNT.ToString() + "\"";
                        if (item.AUTHORCNT > 0)
                        {
                            json += ",\"AUTHOR\":[";
                            for (int i = 0; i < item.AUTHORCNT; i++)
                            {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.AUTHOR[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ORGANIZ.ISN"); }
                                try
                                {
                                    json += ",\"CITIZEN\":{";
                                    try { json += "\"NAME\":\"" + currItem.CITIZEN.NAME + "\""; } catch { Console.WriteLine("nN NAME"); }
                                    try { json += ",\"ADDRESS\":\"" + currItem.CITIZEN.ADDRESS + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                    try { json += ",\"REGION\":\"" + currItem.CITIZEN.REGION + "\""; } catch { Console.WriteLine("nN REGION"); }
                                    try { json += ",\"CITY\":\"" + currItem.CITIZEN.CITY + "\""; } catch { Console.WriteLine("nN ADDRESS"); }
                                    json += "}";
                                }
                                catch { Console.WriteLine("nN CITIZEN"); }
                                try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM.ToString() + "\""; } catch { Console.WriteLine("nN OUTNUM"); }
                                try { json += ",\"DATE_CR\":\"" + currItem.SIGN.ToString() + "\""; } catch { Console.WriteLine("nN SIGN"); }
                                try { json += ",\"DATE_UPD\":\"" + currItem.DATE_UPD.ToString() + "\""; } catch { Console.WriteLine("nN NOTE"); }

                                json += "}";
                            }
                            json += "]";
                        }
                    }
                    catch { Console.WriteLine("nN CORRESPCNT"); }
                }
                if (rcType == "RCOUT")
                {
                    try
                    {
                        json += ",\"PERSONSIGNSCNT\":\"" + item.PERSONSIGNSCNT.ToString() + "\"";
                        if (item.PERSONSIGNSCNT > 0)
                        {
                            json += ",\"PERSONSIGNS\":[";
                            for (int i = 0; i < item.PERSONSIGNSCNT; i++)
                            {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.PERSONSIGNS[i];
                                json += "\"WHO_SIGN\":{";
                                try { json += "\"ISN\":\"" + currItem.WHO_SIGN.ISN + "\""; } catch { }
                                try { json += ",\"NAME\":\"" + currItem.WHO_SIGN.NAME + "\""; } catch { }
                                json += "}";
                                try { json += ",\"ORDERNUM\":\"" + currItem.ORDERNUM + "\""; } catch { Console.WriteLine("nN ORDERNUM"); }
                                json += "}";
                            }
                            json += "]";
                        }

                    }
                    catch { Console.WriteLine("nN PERSONSIGNSCNT"); }
                    try { json += ",\"PERSONSIGN\":\"" + item.PERSONSIGN.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGN"); }

                    try
                    {

                        json += ",\"EXECUTOR\":{";
                        try { json += ",\"ISN\":\"" + item.EXECUTOR.ISN.ToString() + "\""; } catch { }
                        try { json += ",\"ISN[0]\":\"" + item.EXECUTOR[0].ISN.ToString() + "\""; } catch { }
                        try { json += ",\"NAME\":\"" + item.EXECUTOR.NAME.ToString() + "\""; } catch { }
                        try { json += ",\"POST\":\"" + item.EXECUTOR.POST.ToString() + "\""; } catch { }
                        json += "}";
                    }
                    catch { Console.WriteLine("nN EXECUTOR"); }

                    try
                    {
                        json += ",\"COEXECCNT\":\"" + item.COEXECCNT.ToString() + "\"";
                        if (item.COEXECCNT > 0)
                        {
                            json += ",\"COEXEC\":[";
                            for (int i = 0; i < item.COEXECCNT; i++)
                            {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.COEXEC[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { }
                                try { json += ",\"SIGN\":\"" + currItem.SIGN + "\""; } catch { }
                                json += "}";
                            }
                            json += "]";
                        }
                    }
                    catch { Console.WriteLine("nN COEXECCNT"); }
                    try
                    {
                        json += ",\"VISACNT\":\"" + item.VISACNT.ToString() + "\"";
                        if (item.VISACNT > 0)
                        {
                            json += ",\"VISA\":[";
                            for (int i = 0; i < item.VISACNT; i++)
                            {
                                if (i != 0) { json += ",{"; } else { json += "{"; }
                                var currItem = item.VISA[i];
                                try { json += "\"ISN\":\"" + currItem.ISN + "\""; } catch { Console.WriteLine("nN ISN"); }
                                json += ",\"EMPLOY\":{";
                                try { json += "\"ISN\":\"" + currItem.EMPLOY.ISN + "\""; } catch { }
                                try { json += ",\"NAME\":\"" + currItem.EMPLOY.NAME + "\""; } catch { }
                                json += "}";
                                try { json += ",\"DATE\":\"" + currItem.DATE + "\""; } catch { }
                                json += "}";
                            }
                            json += "]";
                        }
                    }
                    catch { Console.WriteLine("nN VISACNT"); }
                    try { json += ",\"PERSONSIGNS_first\":\"" + item.PERSONSIGNS.ToString() + "\""; } catch { Console.WriteLine("nN PERSONSIGNS"); }
                    try { json += ",\"VISA_first\":\"" + item.VISA.ToString() + "\""; } catch { Console.WriteLine("nN VISA"); }
                    try { json += ",\"COEXEC_first\":\"" + item.COEXEC.ToString() + "\""; } catch { Console.WriteLine("nN COEXEC"); }
                    try { json += ",\"LINKS\":\"" + item.LINKS.ToString() + "\""; } catch { Console.WriteLine("nN LINKS"); }
                }
                //Console.WriteLine("MyRcCOntainer.CORRESP444");
                json += "}";
                //Dictionary<string, string> tempDictionary = new Dictionary<string, string>
                //    {
                //        {"ISN", isn.ToString() },
                //        {"RegNum", MyRcCOntainer.RegNum },
                //        {"DocDate", MyRcCOntainer.DocDate.ToString() },
                //        {"Contents", MyRcCOntainer.Contents.Replace("\"", "&quot;") }
                //    };
                //var kvs = tempDictionary.Select(kvp => string.Format("\"{0}\":\"{1}\"", kvp.Key, string.Concat("", kvp.Value)));
                //string jsonChar = string.Concat("{", string.Join(",", kvs), "}");
                //json += jsonChar;
                //    //SuperArr[i] = tempDictionary;
                //}
                json = json + "]";
                json = json.Replace("{,", "{").Replace(",}", "}");
                return json;

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
                if ((paramsString != "/") && (RequestArr.Length > 1))
                {
                    Console.WriteLine("[" + reqType + "]" + " paramsString [" + paramsString + "]");

                    var matches = Regex.Matches(paramsString, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
                    Dictionary<string, string> Dict = matches.Cast<Match>().ToDictionary(
                        m => Uri.UnescapeDataString(m.Groups[2].Value),
                        m => Uri.UnescapeDataString(m.Groups[3].Value)
                    );

                    if (Dict.ContainsKey("need"))
                    {
                        processReq(Client, reqType, Dict);
                    }
                    else { SendError(Client, "Error: not exist paramsArr['need']", reqType); }
                }
                else { SendError(Client, "Error with Request, there is no params", reqType); }
            }
            catch { SendError(Client, "Error with Request", reqType); }
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

