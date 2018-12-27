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
            if ((Dict.ContainsKey("need"))&&(Dict["need"]=="search"))
            {
                Console.WriteLine("processing Req");
                string EOSsourceVal;// "Vocabulary"
                string EOSdateFrom; //от
                string EOSdateTo; //до

                if (Dict.ContainsKey("source")) { EOSsourceVal = Dict["source"]; } else { EOSsourceVal = "Table"; }
                if (Dict.ContainsKey("dateFrom")) { EOSdateFrom = Dict["dateFrom"]; } else { EOSdateFrom = "01/01/1998"; }
                if (Dict.ContainsKey("dateTo")) { EOSdateTo = Dict["dateTo"]; } else { EOSdateTo = "01/01/2018"; }
                Console.WriteLine("Dict " + "source "+ Dict["source"]+ "dateFrom"+ Dict["dateFrom"]+ "dateTo"+ Dict["dateTo"]);
                //dynamic ResultSet = EOSSearch(EOSsourceVal, EOSdateFrom, EOSdateTo);
                string ResultSet = EOSSearch(EOSsourceVal, EOSdateFrom, EOSdateTo);
                Console.WriteLine("Got EOS data ");
                //if (ResultSet.ErrCode < 0)
                //{
                //    string errorText= "error: " + ResultSet.ErrCode.ToString() + "\n" + ResultSet.ErrText;
                //    SendError(Client, errorText);
                //}
                //else{
                    Console.WriteLine("ResultSet-[");
                    //Console.WriteLine(ResultSet);
                    //Console.WriteLine("ResultSet.Length is " + ResultSet.Length);
                    Console.WriteLine("ResultSet-]");
                    //string json = "{" +
                    //"\"reqType\": \"" + reqType + "\"," +
                    //"\"NEED\": \"" + Dict["need"] + "\"," +
                    //"\"some2\": \"" + Dict["some2"] + "\"}";
                    //for (int i = 0; i < ResultSet.Length; i++)
                    //{
                    //    Console.WriteLine("ResultSet.Item " + i +" "+ResultSet.Item(i).RegNum);
                    //}
                    Console.WriteLine("start sending ResultSet is back. ResultSet is "+ ResultSet);
                    SendResp(Client, 200, "application / text", ResultSet);

                //}
            }
            else
            {
                SendError(Client, "error: incorrect params in request");
            }
        }
        
        //Отправить запрос
        private void SendResp(TcpClient Client, int Code, string format, string json)
        {
            try
            {
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
                Console.WriteLine("Connection closed: Exception with SendResp");
                Client.Close();
            }
        }
        
        //Отправить ошибку
        private void SendError(TcpClient Client, string errorText)
        {
            Console.WriteLine(errorText);
            SendResp(Client, 400, "application / text", errorText);
            
        }

        public dynamic EOSSearch(string Source, string dateFrom, string dateTo)
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
                
                Console.WriteLine("ResultSet.Item(2).RegNum is  [" + ResultSet.Item(2).RegNum + "]");
                return ResultSet.Item(2).RegNum;
                //return ResultSet;
                
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmMain(head));
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
            Console.WriteLine("New Request [" + Request + "]");
            string[] RequestArr = Request.Split(' ');
            Console.WriteLine("RequestArr.Length [" + RequestArr.Length + "]");
            string reqType = Request.Split(' ')[0];
            Console.WriteLine(" reqType [" + reqType + "]");
            try
            {
            string paramsString = Request.Split(' ')[1];
                if (paramsString!="/") {
                    Console.WriteLine(" paramsString [" + paramsString + "]");

                    var matches = Regex.Matches(paramsString, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
                    Dictionary<string, string> Dict = matches.Cast<Match>().ToDictionary(
                        m => Uri.UnescapeDataString(m.Groups[2].Value),
                        m => Uri.UnescapeDataString(m.Groups[3].Value)
                    );

                    if (Dict.ContainsKey("need"))
                    {
                        processReq(Client, reqType, Dict);
                    }
                    else {SendError(Client, "Error: not exist paramsArr['need']"); }
                }
                else {SendError(Client, "Error with Request, there is no params");}
            }
            catch {SendError(Client, "Error with Request");}
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

