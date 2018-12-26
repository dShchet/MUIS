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
            if ((Dict.ContainsKey("need")) && (Dict.ContainsKey("some2")))
            {
                Console.WriteLine("processing Req");
                

                string EOSsourceVal = "Table"; //"Vocabulary"
                string EOSdateFrom = "01/01/1998"; //от
                string EOSdateTo = "01/01/2018"; //до

                EOSSearch(EOSsourceVal, EOSdateFrom, EOSdateTo);

                string json = "{" +
                    "\"reqType\": \"" + reqType + "\"," +
                    "\"NEED\": \"" + Dict["need"] + "\"," +
                    "\"some2\": \"" + Dict["some2"] + "\"}";
                SendResp(Client, 200, "application / json", json);
            }
            else
            {
                Console.WriteLine("error: doesnt contain Need");
                SendResp(Client, 400, "application / text", "error: doesnt contain 'need'");
            }

        }
        //Отправить запрос
        private void SendResp(TcpClient Client, int Code, string format, string json)
        {
            string Headers = "HTTP/1.1 " + Code + " \nContent-Type: " + format + "\n\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
            Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
            byte[] jsonBuffer = Encoding.ASCII.GetBytes(json);
            Client.GetStream().Write(jsonBuffer, 0, jsonBuffer.Length);
            Client.Close();
        }

        static void EOSSearch(string Source, string dateFrom, string dateTo)
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
                Console.WriteLine(//Обычно возникает если логин не верный
                    "Не удалось установить соединение с БД ДЕЛО.\n" +
                    "Хранимые процедуры доступны только на просмотр.");
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

                if (ResultSet.ErrCode < 0)
                {//обработка ошибок
                    Console.WriteLine("Ошибка: " + ResultSet.ErrCode.ToString() + "\n" + ResultSet.ErrText);
                }
                else
                {
                    Console.WriteLine("Всего значений: " + ResultSet.ItemCnt.ToString());
                    Console.WriteLine("Номер РК : " + ResultSet.Item(5).RegNum + "\n" + "RK содержание: " + ResultSet.Item(5).Contents);
                }
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmMain(head));
            }
            catch (Exception)
            {
                head = null;
            }
            finally
            {
                if (head != null && head.Active)
                {
                    head.Close();//закртыть соединение
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

            Match ReqMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");
            
            string RequestUri = ReqMatch.Groups[0].Value;


            string[] tokens = RequestUri.Split(' ');
            string reqType = tokens[0];
            string paramsString = (tokens[1]);

            var matches = Regex.Matches(paramsString, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            Dictionary<string, string> Dict = matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups[2].Value),
                m => Uri.UnescapeDataString(m.Groups[3].Value)
            );
            
            if (Dict.ContainsKey("need"))
            {
                processReq(Client, reqType, Dict);}
            else { Console.WriteLine("error: paramsArr['need'] not exist"); }
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

