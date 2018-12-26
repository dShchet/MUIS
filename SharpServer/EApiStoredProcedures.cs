using System;
using System.IO;


namespace SharpServer
{
    internal static class StoredProcedures
    {
        public static void add_rc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            ref int? aIsn, //Isn зарегистрированной РК   
            string aDueCard, //код due картотеки регистрации
            int aIsnCab, //Isn кабинета регистрации
            string aDueDocgroup, //код due группы документов
            int aOrderNum, //порядковый  номер
            string aFreeNum, //регистрационный номер
            DateTime aDocDate, //дата регистрации РК
            int aSecurlevel, //Isn грифа доступа
            string aConsists, //состав
            string aSpecimen, //номера  экземпляров
            DateTime? aPlanDate, //плановая  дата исполнения документа
            DateTime? aFactDate, //фактическая дата исполнения документа
            int? aControlState, //флаг контрольности
            string aAnnotat, //содержание
            string aNote, //примечание
            string aDuePersonWhos, //коды due должностных лиц, кому адресован входящий документ
            int? aIsnDelivery, //Isn вида доставки входящего документа
            string aDueOrganiz, //код due организации - корреспондента входящего документа
            int? aIsnContact, //Isn контакта корреспондента входящего документа для входящих документов
            string aCorrespNum, //исходящий номер входящего документа
            DateTime? aCorrespDate, //исходящая дата входящего документа
            string aCorrespSign, //лицо, подписавшее  входящий документ
            int? aIsnCitizen, //Isn гражданина – корреспондента письма
            int? aIsCollective, //признак  коллективного письма
            int? aIsAnonim, //признак  анонимного письма
            string aSigns, //список подписавших исходящий документ (коды due)
            string aDuePersonExe, //код due исполнителя исходящего документа
            int? aIsnNomenc, //Isn дела в номенклатуе дел
            int? aNothardcopy, //флаг "без досылки бумажного экземпляра"
            int? aCito, //флаг "срочно"
            int? aIsnLinkingDoc, //Isn связанной РК
            int? aIsnLinkingPrj, //Isn регистрируемого РКПД (в случае  регистрации связанной РК из проекта)
            int? aIsnClLink, //Isn типа связки
            string aCopyShablon, //маска для копирования реквизитов
            string aVisas, //cписок лиц, завизировавших документ (коды due)
            int? aEDocument,
            string aSends,
            string askipcopy_ref_file_isns,
            int? aIsnLinkTranparent,
            int? aIsnLinkTranparentPare,
            string aTelNum) //флаг "Оригинал в электронном виде"
        {
            dynamic proc = oHead.GetProc("add_rc");
            string maskDate = "yyyyMMdd";


            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, (object)aIsn);
            proc.Parameters.Append(aIsnParam);
            proc.Parameters.Append(proc.CreateParameter("aDueCard", 200, 1, 48, aDueCard));
            proc.Parameters.Append(proc.CreateParameter("aIsnCab", 3, 1, 0, aIsnCab));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 3, 1, 0, aOrderNum));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, check_null(aFreeNum)));
            proc.Parameters.Append(proc.CreateParameter("aDocDate", 200, 1, 20, check_null(aDocDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSecurlevel", 3, 1, 0, aSecurlevel));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 255, check_null(aConsists)));
            proc.Parameters.Append(proc.CreateParameter("aSpecimen", 200, 1, 64, aSpecimen));
            proc.Parameters.Append(proc.CreateParameter("aPlanDate", 200, 1, 20, check_null(aPlanDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aFactDate", 200, 1, 20, check_null(aFactDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aControlState", 3, 1, 0, (object)check_null(aControlState)));
            proc.Parameters.Append(proc.CreateParameter("aAnnotat", 200, 1, 2000, check_null(aAnnotat)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonWhos", 200, 1, 8000, check_null(aDuePersonWhos)));
            proc.Parameters.Append(proc.CreateParameter("aIsnDelivery", 3, 1, 0, (object)check_null(aIsnDelivery)));
            proc.Parameters.Append(proc.CreateParameter("aDueOrganiz", 200, 1, 48, check_null(aDueOrganiz)));
            proc.Parameters.Append(proc.CreateParameter("aIsnContact", 3, 1, 0, (object)check_null(aIsnContact)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespNum", 200, 1, 64, check_null(aCorrespNum)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespDate", 200, 1, 20, check_null(aCorrespDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespSign", 200, 1, 255, check_null(aCorrespSign)));
            proc.Parameters.Append(proc.CreateParameter("aIsnCitizen", 3, 1, 0, (object)check_null(aIsnCitizen)));
            proc.Parameters.Append(proc.CreateParameter("aIsCollective", 3, 1, 0, (object)check_null(aIsCollective)));
            proc.Parameters.Append(proc.CreateParameter("aIsAnonim", 3, 1, 0, (object)check_null(aIsAnonim)));
            proc.Parameters.Append(proc.CreateParameter("aSigns", 200, 1, 8000, check_null(aSigns)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonExe", 200, 1, 8000, check_null(aDuePersonExe)));
            proc.Parameters.Append(proc.CreateParameter("aIsnNomenc", 3, 1, 0, (object)check_null(aIsnNomenc)));
            proc.Parameters.Append(proc.CreateParameter("aNothardcopy", 3, 1, 0, (object)check_null(aNothardcopy)));
            proc.Parameters.Append(proc.CreateParameter("aCito", 3, 1, 0, (object)check_null(aCito)));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkingDoc", 3, 1, 0, (object)check_null(aIsnLinkingDoc)));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkingPrj", 3, 1, 0, (object)check_null(aIsnLinkingPrj)));
            proc.Parameters.Append(proc.CreateParameter("aIsnClLink", 3, 1, 0, (object)check_null(aIsnClLink)));
            proc.Parameters.Append(proc.CreateParameter("aCopyShablon", 200, 1, 20, check_null(aCopyShablon)));
            proc.Parameters.Append(proc.CreateParameter("aVisas", 200, 1, 8000, aVisas));
            proc.Parameters.Append(proc.CreateParameter("aEDocument", 3, 1, 0, (object)aEDocument));
            proc.Parameters.Append(proc.CreateParameter("aSends", 200, 1, 8000, aSends));
            proc.Parameters.Append(proc.CreateParameter("askipcopy_ref_file_isns", 200, 1, 8000, askipcopy_ref_file_isns));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkTranparent", 3, 1, 0, (object)aIsnLinkTranparent));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkTranparentPare", 3, 1, 0, (object)aIsnLinkTranparent));
            proc.Parameters.Append(proc.CreateParameter("aTelNum", 200, 1, 64, aTelNum));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsn = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aIsn = (int?)aIsnParam.Value;
            }
        }

        public static void edit_rc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead.
            int aIsn, //Isn зарегистрированной РК.
            string aDueCard, //параметр устарел, не используется. NULL
            int? aIsnCab, //параметр устарел, не используется. NULL
            string aFreeNum, //Регистрационный номер.
            DateTime? aDocDate, //Дата регистрации РК. 
            int aSecurlevel, //Идентификатор грифа доступа. 
            string aConsists, //Состав. 
            string aSpecimen, //Номера экземпляров.
            DateTime? aPlanDate, //Плановая Дата.
            DateTime? aFactDate, //Фактическая дата исполнения.
            int? aControlState, //Флаг контрольности.
            string aAnnotat, //Содержание. 
            string aNote, //Примeчание
            string aDuePersonWho, //Параметр устарел, не используется. String.empty
            int? aIsnDelivery, //Идентификатор вида доставки входящего документа, обязателен только для входящих.
            int? aIsCollective, //Признак  коллективного письма. 
            int? aIsAnonim, //Признак анонимного письма.
            string aDuePersonSign, //Параметр устарел, не используется.
            string aDuePersonExe, //Начиная с версии 8.11 параметр устарел, не используется.
            int? aNothardcopy, //Флаг «без досылки бум. Экземпляра».
            int? aCito, //Флаг «срочно».
            int? aOrderNum, //Порядковый номер.Начиная с версии 11.0.
            int? aEDocument, //Флаг "Оригинал в электронном виде". Начиная с версии 11.0.3.
            string aTelNum)
        {
            dynamic proc = oHead.GetProc("edit_rc");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aDueCard", 200, 1, 48, string.Empty)); //устарел
            proc.Parameters.Append(proc.CreateParameter("aIsnCab", 3, 1, 0, (object)Convert.DBNull)); //устарел
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, check_null(aFreeNum)));
            proc.Parameters.Append(proc.CreateParameter("aDocDate", 200, 1, 20, check_null(aDocDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSecurlevel", 3, 1, 0, aSecurlevel));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 255, check_null(aConsists)));
            proc.Parameters.Append(proc.CreateParameter("aSpecimen", 200, 1, 64, check_null(aSpecimen)));
            proc.Parameters.Append(proc.CreateParameter("aPlanDate", 200, 1, 20, check_null(aPlanDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aFactDate", 200, 1, 20, check_null(aFactDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aControlState", 3, 1, 0, (object)check_null(aControlState)));
            proc.Parameters.Append(proc.CreateParameter("aAnnotat", 200, 1, 2000, check_null(aAnnotat)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonWho", 200, 1, 48, string.Empty)); //Устарел
            proc.Parameters.Append(proc.CreateParameter("aIsnDelivery", 3, 1, 0, (object)check_null(aIsnDelivery)));
            proc.Parameters.Append(proc.CreateParameter("aIsCollective", 3, 1, 0, (object)check_null(aIsCollective)));
            proc.Parameters.Append(proc.CreateParameter("aIsAnonim", 3, 1, 0, (object)check_null(aIsAnonim)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonSign", 200, 1, 48, string.Empty)); //Устарел
            proc.Parameters.Append(proc.CreateParameter("aDuePersonExe", 200, 1, 48, string.Empty)); //Устарел
            proc.Parameters.Append(proc.CreateParameter("aNothardcopy", 3, 1, 0, check_null(aNothardcopy)));
            proc.Parameters.Append(proc.CreateParameter("aCito", 3, 1, 0, (object)check_null(aCito)));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 3, 1, 0, (object)check_null(aOrderNum)));
            proc.Parameters.Append(proc.CreateParameter("aEDocument", 3, 1, 0, (object)check_null(aEDocument)));
            proc.Parameters.Append(proc.CreateParameter("aTelNum", 200, 1, 64, aTelNum));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_rc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aisndoc, //Isn РК
            string acard, //код due картотеки регистрации. Устарел
            int aretnum, //использовать текущий порядковый номер при регистрации следующей РК
            int? ayear) //год регистрации РК. Устарел
        {
            dynamic proc = oHead.GetProc("del_rc");


            proc.Parameters.Append(proc.CreateParameter("aisndoc", 3, 1, 0, aisndoc));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, string.Empty)); //Устарел
            proc.Parameters.Append(proc.CreateParameter("aretnum", 3, 1, 0, aretnum));
            proc.Parameters.Append(proc.CreateParameter("ayear", 3, 1, 0, Convert.DBNull)); //Устарел

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void reserve_num(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            string aOper, //тип операции
            string aDueDocgroup, //код due группы документов
            int aYear, //год в счетчике номерообразования РК
            string card_id, //код due картотеки регистрации
            ref int? aOrderNum, //порядковый номер
            ref string aFreeNum, //регистрационный номер
            string aSessionId) //идентификатор сессии
        {
            dynamic proc = oHead.GetProc("reserve_num");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 2, aOper));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aYear", 3, 1, 0, aYear));
            proc.Parameters.Append(proc.CreateParameter("card_id", 200, 1, 48, card_id));
            dynamic aOrderNumParam = proc.CreateParameter("aOrderNum", 3, 3, 0, (object)check_null(aOrderNum));
            proc.Parameters.Append(aOrderNumParam);
            dynamic aFreeNumParam = proc.CreateParameter("aFreeNum", 200, 3, 64, check_null(aFreeNum));
            proc.Parameters.Append(aFreeNumParam);
            proc.Parameters.Append(proc.CreateParameter("aSessionId", 200, 1, 255, aSessionId));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aOrderNum = null;
                aFreeNum = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aOrderNum = (int?)aOrderNumParam.Value;
                aFreeNum = (string)aFreeNumParam.Value;
            }
        }

        public static void return_num(
            dynamic oHead,
            string aOper,
            string aDueDocgroup,
            int aYear,
            ref int? aOrderNum)
        {
            dynamic proc = oHead.GetProc("return_num");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 2, aOper));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aYear", 3, 1, 0, aYear));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 200, 1, 64, (object)check_null(aOrderNum)));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, ""));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                throw new Exception(oHead.ErrText);
            }
        }

        public static void add_forward(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Isn пересылаемой РК
            string acard, //Код due текущей картотеки.
            int acab, //Isn текущего кабинета
            string codes) //Список due адресатов, которым пересылается РК
        {
            dynamic proc = oHead.GetProc("add_forward");

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, acard));
            proc.Parameters.Append(proc.CreateParameter("acab", 3, 1, 0, acab));
            proc.Parameters.Append(proc.CreateParameter("codes", 200, 1, 2000, codes));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_forward(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aisnforward) //Isn удаляемой пересылки.
        {
            dynamic proc = oHead.GetProc("del_forward");

            proc.Parameters.Append(proc.CreateParameter("aisnforward", 3, 1, 0, aisnforward));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_journal(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Isn документа
            string aCard, //Due картотеки корреспондента
            int aCab, //Isn кабинета корреспондента
            string aCodes, //Список due адресатов
            int? aIsnNomenc, //Isn дела
            DateTime aDates, //Дата и время создания записи в журнале
            string aFlagsOriginal, //Список  “Флаг  копии”
            string aOrigNums, //Список “Номер  оригинала в журнале”
            string aCopyNums, //Список “Номер  копии в журнале”
            int aClearFldr6, //Флаг удаления из папки «В Дело» 
            int aDescructFlag) //Флаг типа записи
        {
            dynamic proc = oHead.GetProc("add_journal");
            string maskDate = "yyyyMMdd HH:mm:ss";

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("aCard", 200, 1, 48, aCard));
            proc.Parameters.Append(proc.CreateParameter("aCab", 3, 1, 0, aCab));
            proc.Parameters.Append(proc.CreateParameter("aCodes", 200, 1, 2000, aCodes));
            proc.Parameters.Append(proc.CreateParameter("aIsnNomenc", 3, 1, 0, (object)check_null(aIsnNomenc)));
            proc.Parameters.Append(proc.CreateParameter("aDates", 200, 1, 20, check_null(aDates, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aFlagsOriginal", 200, 1, 2000, check_null(aFlagsOriginal)));
            proc.Parameters.Append(proc.CreateParameter("aOrigNums", 200, 1, 2000, check_null(aOrigNums)));
            proc.Parameters.Append(proc.CreateParameter("aCopyNums", 200, 1, 2000, check_null(aCopyNums)));
            proc.Parameters.Append(proc.CreateParameter("aClearFldr6", 3, 1, 0, aClearFldr6));
            proc.Parameters.Append(proc.CreateParameter("aDescructFlag", 3, 1, 0, aDescructFlag));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_visa(
           dynamic oHead,           //Головной объект системы, поддерживающий интерфейс IHead
           int aIsn,                //Isn документа
           string aDuePersons,      //Список кодов due из справочника 
           DateTime[] aVisaDates,   //Список дат визирования.
           string aNotes)           //Список "Примечаний" к создаваемым визам.
        {
            dynamic proc = oHead.GetProc("add_visa");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aDuePersons", 200, 1, 2000, check_null(aDuePersons)));
            proc.Parameters.Append(proc.CreateParameter("aVisaDates", 200, 1, 2000, check_null(aVisaDates, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aNotes", 200, 1, 2000, check_null(aNotes)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_visa(
           dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
           int aIsnRefVisa) //Isn визы
        {
            dynamic proc = oHead.GetProc("del_visa");

            proc.Parameters.Append(proc.CreateParameter("aIsnRefVisa", 3, 1, 0, aIsnRefVisa));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_visa(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnRefVisa, //Isn визы
            DateTime aVisaDate, //Дата визирования
            string aNote) //Примечание
        {
            dynamic proc = oHead.GetProc("edit_visa");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsnRefVisa", 3, 1, 0, aIsnRefVisa));
            proc.Parameters.Append(proc.CreateParameter("aVisaDate", 200, 1, 20, check_null(aVisaDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, aNote));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_sign(
           dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
           int aIsn, //Isn документа
           string aDuePersons, //Список кодов due из справочника 
           DateTime[] aSignDates) //Список дат подписи
        {
            dynamic proc = oHead.GetProc("add_sign");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aDuePersons", 200, 1, 2000, check_null(aDuePersons)));
            proc.Parameters.Append(proc.CreateParameter("aSignDates", 200, 1, 2000, check_null(aSignDates, maskDate)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_sign(
           dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
           int aIsnDocSign) //Isn подписи
        {
            dynamic proc = oHead.GetProc("del_sign");

            proc.Parameters.Append(proc.CreateParameter("aIsnDocSign", 3, 1, 0, aIsnDocSign));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_sign(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDocSign, //Isn визы
            DateTime aSignDate) //Дата визирования
        {
            dynamic proc = oHead.GetProc("edit_sign");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsnDocSign", 3, 1, 0, aIsnDocSign));
            proc.Parameters.Append(proc.CreateParameter("aSignDate", 200, 1, 20, check_null(aSignDate, maskDate)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_soisp(
            dynamic oHead,
            int aIsn,
            string aDuesOrganiz,
            string aSoispNums,
            DateTime[] aSoispDates,
            string aSoispPersons)
        {
            dynamic proc = oHead.GetProc("add_soisp");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aDuesOrganiz", 200, 1, 8000, check_null(aDuesOrganiz)));
            proc.Parameters.Append(proc.CreateParameter("aSoispNums", 200, 1, 8000, check_null(aSoispNums)));
            proc.Parameters.Append(proc.CreateParameter("aSoispDates", 200, 1, 8000, check_null(aSoispDates, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSoispPersons", 200, 1, 8000, check_null(aSoispPersons)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_soisp(
           dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
           int aIsnRefSoisp) //Isn подписи
        {
            dynamic proc = oHead.GetProc("del_soisp");

            proc.Parameters.Append(proc.CreateParameter("aIsnRefSoisp", 3, 1, 0, aIsnRefSoisp));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_soisp(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnRefSoisp, //Isn визы
            string aSoispNum,
            DateTime aSoispDate, //Дата визирования
            string aSoispPerson) //Примечание
        {
            dynamic proc = oHead.GetProc("edit_soisp");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsnRefSoisp", 3, 1, 0, aIsnRefSoisp));
            proc.Parameters.Append(proc.CreateParameter("aSoispNum", 200, 1, 64, aSoispNum));
            proc.Parameters.Append(proc.CreateParameter("aSoispDate", 200, 1, 20, check_null(aSoispDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSoispPerson", 200, 1, 2000, aSoispPerson));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_ref_ufolder(
            dynamic oHead,
            int aIsnRefUfolder,
            int aIsnRefDoc,
            int aKindDoc,
            String aNote)
        {
            dynamic proc = oHead.GetProc("add_ref_ufolder");

            proc.Parameters.Append(proc.CreateParameter("aIsnRefUfolder", 3, 1, 0, aIsnRefUfolder));
            proc.Parameters.Append(proc.CreateParameter("aIsnRefDoc", 3, 1, 0, aIsnRefDoc));
            proc.Parameters.Append(proc.CreateParameter("aKindDoc", 3, 1, 0, aKindDoc));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 255, aNote));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_ref_ufolder(
            dynamic oHead,
            int aIsnRefUfolder,
            int aIsnRefDoc)
        {
            dynamic proc = oHead.GetProc("del_ref_ufolder");

            proc.Parameters.Append(proc.CreateParameter("aIsnRefUfolder", 3, 1, 0, aIsnRefUfolder));
            proc.Parameters.Append(proc.CreateParameter("aIsnRefDoc", 3, 1, 0, aIsnRefDoc));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_send(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Isn РК
            string acard, //due текущей картотеки
            int acab, //Isn текущего кабинета
            string aclassif, //Название справочника, из которого добавляются адресаты
            string codes, //Список кодов адресатов
            string aFlagsOriginal, //Список флагов оригинал/копия
            string aOrigNums, //Список номеров оригиналов, только для внутренних адресатов
            string aCopyNums, //Список номеров  копий, только для групп документов с нумеруемыми копиями и только для внутренних адресатов
            DateTime[] aDate, //Дата отправки
            string aSend_Person, //Список ФИО “Кому адресован” (для внутренних - null) 
            string aIsn_Delivery, //Список Isn доставки 
            int? aSendingType, //Тип отправки. Только для версии ЦБ.
            string aIsnsContact, //Список идентификаторов контактов к добавляемым адресатам.
            string aNotes,
            string aConsists) //Список примечаний
        {
            dynamic proc = oHead.GetProc("add_send");
            string maskDate = "yyyyMMdd HH:mm:ss";

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, acard));
            proc.Parameters.Append(proc.CreateParameter("acab", 3, 1, 0, acab));
            proc.Parameters.Append(proc.CreateParameter("aclassif", 200, 1, 20, aclassif));
            proc.Parameters.Append(proc.CreateParameter("codes", 200, 1, 2000, codes));
            proc.Parameters.Append(proc.CreateParameter("aFlagsOriginal", 200, 1, 2000, check_null(aFlagsOriginal)));
            proc.Parameters.Append(proc.CreateParameter("aOrigNums", 200, 1, 2000, check_null(aOrigNums)));
            proc.Parameters.Append(proc.CreateParameter("aCopyNums", 200, 1, 2000, check_null(aCopyNums)));
            proc.Parameters.Append(proc.CreateParameter("aDate", 200, 1, 2000, check_null(aDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSend_Person", 200, 1, 2000, check_null(aSend_Person)));
            proc.Parameters.Append(proc.CreateParameter("aIsn_Delivery", 200, 1, 2000, check_null(aIsn_Delivery)));
            proc.Parameters.Append(proc.CreateParameter("aSendingType", 3, 1, 0, (object)check_null(aSendingType)));
            proc.Parameters.Append(proc.CreateParameter("aIsnsContact", 200, 1, 2000, check_null(aIsnsContact)));
            proc.Parameters.Append(proc.CreateParameter("aNotes", 200, 1, 8000, check_null(aNotes)));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 8000, check_null(aConsists)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_send(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Isn документа
            int aisn_ref_send, //Isn адресата
            string acard, //due текущей картотеки
            int acab, //Isn текущего кабинета
            int aFlagOriginal, //Флаг  оригинал/копия.
            string aOrigNum, //Номер оригинала
            string aCopyNum, //Номер  копии.
            DateTime aDates, //Дата и время отправки.
            string aNote) //Примечание
        {
            dynamic proc = oHead.GetProc("edit_send");
            string maskDate = "yyyyMMdd hh:mm:ss";

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("aisn_ref_send", 3, 1, 0, aisn_ref_send));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, acard));
            proc.Parameters.Append(proc.CreateParameter("acab", 3, 1, 0, acab));
            proc.Parameters.Append(proc.CreateParameter("aFlagOriginal", 3, 1, 0, aFlagOriginal));
            proc.Parameters.Append(proc.CreateParameter("aOrigNum", 200, 1, 255, check_null(aOrigNum)));
            proc.Parameters.Append(proc.CreateParameter("aCopyNum", 200, 1, 255, check_null(aCopyNum)));
            proc.Parameters.Append(proc.CreateParameter("aDates", 200, 1, 20, check_null(aDates, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_send(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aisnrefsend, //Isn адресата
            string acard) //due текущей картотеки. С версии 8.10 параметр устарел, не используется.
        {
            dynamic proc = oHead.GetProc("del_send");

            proc.Parameters.Append(proc.CreateParameter("aisnrefsend", 3, 1, 0, aisnrefsend));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, string.Empty)); //устарел

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_outer_send(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int a_isn_doc, //Isn РК
            int a_isn_ref_send, //Isn адресата
            string a_card, //due текущей картотеки. Устарел
            int a_cab, //Isn текущего кабинета. Устарел
            int a_isn_contact, //Isn контакта
            DateTime a_send_date, //Дата отправки
            string a_send_person, //Кому адресован
            int a_isn_delivery, //Isn вида доставки
            int a_sending_type, //Тип отправки
            string a_note, //Примечание
            string a_reg_n, //Регистрационный номер документа у адресата
            DateTime a_reg_date, //Дата регистрации документа у адресата
            int a_answer) //Флаг «Требуется ли квитанция о регистрации»
        {
            dynamic proc = oHead.GetProc("edit_outer_send");
            string maskDate = "yyyyMMdd HH:mm:ss";

            proc.Parameters.Append(proc.CreateParameter("a_isn_doc", 3, 1, 0, a_isn_doc));
            proc.Parameters.Append(proc.CreateParameter("a_isn_ref_send", 3, 1, 0, a_isn_ref_send));
            proc.Parameters.Append(proc.CreateParameter("a_card", 200, 1, 48, string.Empty)); //Устарел
            proc.Parameters.Append(proc.CreateParameter("a_cab", 3, 1, 0, Convert.DBNull)); //Устарел
            proc.Parameters.Append(proc.CreateParameter("a_isn_contact", 3, 1, 0, a_isn_contact));
            proc.Parameters.Append(proc.CreateParameter("a_send_date", 200, 1, 20, check_null(a_send_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("a_send_person", 200, 1, 64, check_null(a_send_person)));
            proc.Parameters.Append(proc.CreateParameter("a_isn_delivery", 3, 1, 0, a_isn_delivery));
            proc.Parameters.Append(proc.CreateParameter("a_sending_type", 3, 1, 0, a_sending_type));
            proc.Parameters.Append(proc.CreateParameter("a_note", 200, 1, 2000, check_null(a_note)));
            proc.Parameters.Append(proc.CreateParameter("a_reg_n", 200, 1, 64, a_reg_n));
            proc.Parameters.Append(proc.CreateParameter("a_reg_date", 200, 1, 20, check_null(a_reg_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("a_answer", 3, 1, 0, a_answer));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_rubric(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Isn РК
            string codes, //Список due рубрик
            string acard) //due текущей картотеки. Устарел
        {
            dynamic proc = oHead.GetProc("add_rubric");

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("codes", 200, 1, 2000, codes));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, string.Empty)); //Устарел

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_rubric(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aisnrefrubric, //Isn рубрики
            string acard) //due текущей картотеки. Устарел
        {
            dynamic proc = oHead.GetProc("del_rubric");

            proc.Parameters.Append(proc.CreateParameter("aisnrefrubric", 3, 1, 0, aisnrefrubric));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, string.Empty)); //Устарел

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_link(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int? aisndoc, //Isn РК (РКПД)
            int? aisn_linkdoc, //Isn связанной РК (РКПД)
            int aisn_link, //Isn типа связки
            string alinked_num, //Регистрационный номер документа
            string acard, //due текущей картотеки. Устарел
            string aUrlStr)//Сетевой адрес для связки с не зарегистрированным в системе документом.
        {

            dynamic proc = oHead.GetProc("add_link");

            proc.Parameters.Append(proc.CreateParameter("aisndoc", 3, 1, 0, aisndoc));
            proc.Parameters.Append(proc.CreateParameter("aisn_linkdoc", 3, 1, 0, (object)check_null(aisn_linkdoc)));
            proc.Parameters.Append(proc.CreateParameter("aisn_link", 3, 1, 0, aisn_link));
            proc.Parameters.Append(proc.CreateParameter("alinked_num", 200, 1, 255, check_null(alinked_num)));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, string.Empty));
            proc.Parameters.Append(proc.CreateParameter("aUrlStr", 200, 1, 255, check_null(aUrlStr)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_link(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aisn_ref_link, //Isn связки
            int aisn_doc, //Isn РК (РКПД)
            int aisn_linkeddoc, //Isn связанной РК (РКПД)
            int aisn_link, //Isn вида связки
            string acard) //due текущей картотеки. Устарел
        {
            dynamic proc = oHead.GetProc("del_link");

            proc.Parameters.Append(proc.CreateParameter("aisn_ref_link", 3, 1, 0, aisn_ref_link));
            proc.Parameters.Append(proc.CreateParameter("aisn_doc", 3, 1, 0, aisn_doc));
            proc.Parameters.Append(proc.CreateParameter("aisn_linkeddoc", 3, 1, 0, check_null(aisn_linkeddoc)));
            proc.Parameters.Append(proc.CreateParameter("aisn_link", 3, 1, 0, aisn_link));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, string.Empty)); //Устарел

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_resolution(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int an_kind_resolution, //Признак вида поручения.
            int an_isn_ref_doc, //Идентификатор РК документа.
            int? an_isn_parent_resolution, //идентификатор родительской резолюции.
            string an_isn_author, //Идентификатор должностного лица - автора резолюции.
            string as_item_number, //Номер пункта
            string as_resolution_text,//Текст резолюции
            int an_isn_category, //Идентификатор категории поручения
            int an_conf, //Флаг конфиденциальности резолюции
            DateTime? ad_resolution_date,//Дата Резолюции
            DateTime? ad_send_date, //Дата рассылки, не используется.
            int an_notify_author, //Флаг рассылки в кабинет
            DateTime? ad_plan_date, //Плановая дата
            DateTime? ad_interim_date, // Промежуточная дата
            DateTime? ad_fact_date, //Фактическая дата
            string as_due_controller, //Код due контролера поручения
            int? an_control_state, //Флаг контрльности
            string as_summary, //Ход исполнения
            int an_cascade_control, //Флаг снятия с контоль "Каскадно"
            int an_control_duty, //Флаг "Резолюция отвечает за контрольность РК"
            string as_resume, //Основания для снятия с контроля
            int an_left_resolution, //Флаг "Рассылать резолюцию"
            int an_cycle, //Флаг цикла
            string as_note, // Примечание
            string as_rep_isns, //Список идентификаторов исполнителей
            string as_rep_responsible_isns, //Спиосок идентификаторов ответственных исполнителей
            int? an_cab, //Идентификатор кабинета с 8.10 устарел
            ref int? an_isn_resolution, //Идентификатор созданого поручения
            int a_is_project) // Признак для создания проекта резолюции
        {
            dynamic proc = oHead.GetProc("add_resolution");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("an_kind_resolution", 3, 1, 0, an_kind_resolution));
            proc.Parameters.Append(proc.CreateParameter("an_isn_ref_doc", 3, 1, 0, an_isn_ref_doc));
            proc.Parameters.Append(proc.CreateParameter("an_isn_parent_resolution", 3, 1, 0, check_null(an_isn_parent_resolution)));
            proc.Parameters.Append(proc.CreateParameter("an_isn_author", 200, 1, 48, an_isn_author));
            proc.Parameters.Append(proc.CreateParameter("as_item_number", 200, 1, 64, check_null(as_item_number)));
            proc.Parameters.Append(proc.CreateParameter("as_resolution_text", 200, 1, 2000, check_null(as_resolution_text)));
            proc.Parameters.Append(proc.CreateParameter("an_isn_category", 3, 1, 0, an_isn_category));
            proc.Parameters.Append(proc.CreateParameter("an_conf", 3, 1, 0, an_conf));
            proc.Parameters.Append(proc.CreateParameter("ad_resolution_date", 200, 1, 20, check_null(ad_resolution_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("ad_send_date", 200, 1, 20, check_null(ad_send_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("an_notify_author", 3, 1, 0, an_notify_author));
            proc.Parameters.Append(proc.CreateParameter("ad_plan_date", 200, 1, 20, check_null(ad_plan_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("ad_interim_date", 200, 1, 20, check_null(ad_interim_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("ad_fact_date", 200, 1, 20, check_null(ad_fact_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("as_due_controller", 200, 1, 48, check_null(as_due_controller)));
            proc.Parameters.Append(proc.CreateParameter("an_control_state", 3, 1, 0, check_null(an_control_state)));
            proc.Parameters.Append(proc.CreateParameter("as_summary", 200, 1, 2000, check_null(as_summary)));
            proc.Parameters.Append(proc.CreateParameter("an_cascade_control", 3, 1, 0, an_cascade_control));
            proc.Parameters.Append(proc.CreateParameter("an_control_duty", 3, 1, 0, an_control_duty));
            proc.Parameters.Append(proc.CreateParameter("as_resume", 200, 1, 2000, check_null(as_resume)));
            proc.Parameters.Append(proc.CreateParameter("an_left_resolution", 3, 1, 0, an_left_resolution));
            proc.Parameters.Append(proc.CreateParameter("an_cycle", 3, 1, 0, an_cycle));
            proc.Parameters.Append(proc.CreateParameter("as_note", 200, 1, 255, check_null(as_note)));
            proc.Parameters.Append(proc.CreateParameter("as_rep_isns", 200, 1, 2000, as_rep_isns));
            proc.Parameters.Append(proc.CreateParameter("as_rep_responsible_isns", 200, 1, 2000, as_rep_responsible_isns));
            proc.Parameters.Append(proc.CreateParameter("an_cab", 3, 1, 0, Convert.DBNull));
            dynamic anIsnParam = proc.CreateParameter("an_isn_resolution", 3, 3, 0, (object)an_isn_resolution);
            proc.Parameters.Append(anIsnParam);
            proc.Parameters.Append(proc.CreateParameter("an_is_project", 3, 1, 0, a_is_project));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                an_isn_resolution = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                an_isn_resolution = (int?)anIsnParam.Value;
            }
        }

        public static void del_resolution(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int an_isn_resolution, //Isn корреспондента
            int a_delete_cascade) //due текущей картотеки. С версии 8.10 параметр устарел, не используется.
        {
            dynamic proc = oHead.GetProc("del_resolution");

            proc.Parameters.Append(proc.CreateParameter("an_isn_resolution", 3, 1, 0, an_isn_resolution));
            proc.Parameters.Append(proc.CreateParameter("a_delete_cascade", 3, 1, 0, a_delete_cascade));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_resolution(
            dynamic oHead,
            int a_isn_resolution,
            DateTime? a_fact_date, //20
            DateTime? a_plan_date,
            DateTime? a_interim_date,
            DateTime? a_send_date,
            string a_summary, //2000
            string a_resume,
            string a_resolution_text,
            int? a_isn_status_exec,
            int? a_control_state,
            string a_note, //255
            int a_control_duty,
            string a_due_controller, //48
            string a_due_author,
            int? a_isn_category,
            string a_rep_isns, //2000
            string a_rep_responsible_isns)
        {
            dynamic proc = oHead.GetProc("edit_resolution");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("a_isn_resolution", 3, 1, 0, a_isn_resolution));
            proc.Parameters.Append(proc.CreateParameter("a_fact_date", 200, 1, 20, check_null(a_fact_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("a_plan_date", 200, 1, 20, check_null(a_plan_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("a_interim_date", 200, 1, 20, check_null(a_interim_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("a_send_date", 200, 1, 20, check_null(a_send_date, "yyyyMMdd HH:mm:ss")));
            proc.Parameters.Append(proc.CreateParameter("a_summary", 200, 1, 2000, check_null(a_summary)));
            proc.Parameters.Append(proc.CreateParameter("a_resume", 200, 1, 2000, check_null(a_resume)));
            proc.Parameters.Append(proc.CreateParameter("a_resolution_text", 200, 1, 2000, check_null(a_resolution_text)));
            proc.Parameters.Append(proc.CreateParameter("a_isn_status_exec", 3, 1, 0, check_null(a_isn_status_exec)));
            proc.Parameters.Append(proc.CreateParameter("a_control_state", 3, 1, 0, check_null(a_control_state)));
            proc.Parameters.Append(proc.CreateParameter("a_note", 200, 1, 255, check_null(a_note)));
            proc.Parameters.Append(proc.CreateParameter("a_control_duty", 3, 1, 0, a_control_duty));
            proc.Parameters.Append(proc.CreateParameter("a_due_controller", 200, 1, 48, check_null(a_due_controller)));
            proc.Parameters.Append(proc.CreateParameter("a_due_author", 200, 1, 48, a_due_author));
            proc.Parameters.Append(proc.CreateParameter("a_isn_category", 3, 1, 0, check_null(a_isn_category)));
            proc.Parameters.Append(proc.CreateParameter("a_rep_isns", 200, 1, 2000, a_rep_isns));
            proc.Parameters.Append(proc.CreateParameter("a_rep_responsible_isns", 200, 1, 2000, a_rep_responsible_isns));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void return_res_proj(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int an_isn_resolution)//Идентификатор проекта резолюции
        {
            dynamic proc = oHead.GetProc("Return_res_proj");

            proc.Parameters.Append(proc.CreateParameter("an_isn_resolution", 3, 1, 0, an_isn_resolution));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void accept_control(
            dynamic oHead,
            string as_isns_resolution,
            int an_cab) //Устарел
        {
            dynamic proc = oHead.GetProc("accept_control");

            proc.Parameters.Append(proc.CreateParameter("as_isns_resolution", 200, 1, 2000, as_isns_resolution));
            proc.Parameters.Append(proc.CreateParameter("an_cab", 3, 1, 0, Convert.DBNull)); //Устарел
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void set_reply(
            dynamic oHead,
            int an_isn_reply,
            string as_reply_text,
            DateTime? ad_reply_date,
            int an_status_reply,
            int? an_cab) //Устарел
        {
            dynamic proc = oHead.GetProc("set_reply");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("an_isn_reply", 3, 1, 0, an_isn_reply));
            proc.Parameters.Append(proc.CreateParameter("as_reply_text", 200, 1, 2000, as_reply_text));
            proc.Parameters.Append(proc.CreateParameter("ad_reply_date", 200, 1, 20, check_null(ad_reply_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("an_status_reply", 3, 1, 0, an_status_reply));
            proc.Parameters.Append(proc.CreateParameter("an_cab", 3, 1, 0, Convert.DBNull)); //Устарел

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void accept_execute(
            dynamic oHead,
            int an_isn_folder_item)
        {
            dynamic proc = oHead.GetProc("accept_execute");

            proc.Parameters.Append(proc.CreateParameter("an_isn_folder_item", 3, 1, 0, an_isn_folder_item));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void mark_fi_as_read(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int an_isn_folder_item, //Идентификатор  элемента 
            int an_kind_doc //Вид документа
            )
        {
            dynamic proc = oHead.GetProc("mark_fi_as_read");

            proc.Parameters.Append(proc.CreateParameter("an_isn_folder_item", 3, 1, 0, an_isn_folder_item));
            proc.Parameters.Append(proc.CreateParameter("an_kind_doc", 3, 1, 0, an_kind_doc));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void delete_fi(
            dynamic oHead,
            int an_isn_folder_item)
        {
            dynamic proc = oHead.GetProc("delete_fi");

            proc.Parameters.Append(proc.CreateParameter("an_isn_folder_item", 3, 1, 0, an_isn_folder_item));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void set_user_parm(
            dynamic oHead,
            string as_parm_name,
            string as_parm_value,
            int? af_default)
        {
            dynamic proc = oHead.GetProc("set_user_parm");

            proc.Parameters.Append(proc.CreateParameter("as_parm_name", 200, 1, 64, as_parm_name));
            proc.Parameters.Append(proc.CreateParameter("as_parm_value", 200, 1, 2000, as_parm_value));
            proc.Parameters.Append(proc.CreateParameter("af_default", 3, 1, 0, check_null(af_default)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void upd_pasw(
            dynamic oHead,
            string new_pass
            )
        {
            dynamic proc = oHead.GetProc("upd_pasw");

            proc.Parameters.Append(proc.CreateParameter("new_pass", 200, 1, 30, new_pass));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void save_organiz_web(
            dynamic oHead,
            string aOper,
            ref int? aIsn,
            ref string aDue,
            string aHighNode,
            string aClassifName,
            string aZipCode,
            string aCity,
            string aAddress,
            string aFullname,
            string aOkpo,
            string aInn,
            string aDue_region,
            string aEmail,
            int aMailForAll,
            int aIsnAddrCategory,
            string aNote,
            string aOkonh,
            string aLawAdress,
            string aSertificat,
            string aCode)
        {
            dynamic proc = oHead.GetProc("save_organiz_web");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 3, aOper));
            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, check_null(aIsn));
            proc.Parameters.Append(aIsnParam);
            dynamic aDueParam = proc.CreateParameter("aDue", 200, 3, 48, check_null(aDue));
            proc.Parameters.Append(aDueParam);
            proc.Parameters.Append(proc.CreateParameter("aHighNode", 200, 1, 48, check_null(aHighNode)));
            proc.Parameters.Append(proc.CreateParameter("aClassifName", 200, 1, 255, check_null(aClassifName)));
            proc.Parameters.Append(proc.CreateParameter("aZipCode", 200, 1, 12, check_null(aZipCode)));
            proc.Parameters.Append(proc.CreateParameter("aCity", 200, 1, 255, check_null(aCity)));
            proc.Parameters.Append(proc.CreateParameter("aAddress", 200, 1, 255, check_null(aAddress)));
            proc.Parameters.Append(proc.CreateParameter("aFullname", 200, 1, 255, check_null(aFullname)));
            proc.Parameters.Append(proc.CreateParameter("aOkpo", 200, 1, 8, check_null(aOkpo)));
            proc.Parameters.Append(proc.CreateParameter("aInn", 200, 1, 64, check_null(aInn)));
            proc.Parameters.Append(proc.CreateParameter("aDue_region", 200, 1, 48, check_null(aDue_region)));
            proc.Parameters.Append(proc.CreateParameter("aEmail", 200, 1, 64, check_null(aEmail)));
            proc.Parameters.Append(proc.CreateParameter("aMailForAll", 3, 1, 0, aMailForAll));
            proc.Parameters.Append(proc.CreateParameter("aIsnAddrCategory", 3, 1, 0, check_null(aIsnAddrCategory)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 255, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aOkonh", 200, 1, 16, check_null(aOkonh)));
            proc.Parameters.Append(proc.CreateParameter("aLawAdress", 200, 1, 255, check_null(aLawAdress)));
            proc.Parameters.Append(proc.CreateParameter("aSertificat", 200, 1, 255, check_null(aSertificat)));
            proc.Parameters.Append(proc.CreateParameter("aCode", 200, 1, 4, check_null(aCode)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsn = null;
                aDue = string.Empty;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aIsn = (int?)aIsnParam.Value;
                aDue = aDueParam.Value;
            }
        }

        public static void save_organiz_bank_recvisit(
            dynamic oHead,
            string a_oper,
            ref int? a_isn_bank_recv,
            int a_isn_organiz,
            string a_classif_name,
            string a_bank_name,
            string a_acount,
            string a_subacount,
            string a_bik,
            string a_city,
            string a_note)
        {
            dynamic proc = oHead.GetProc("save_organiz_bank_recvisit");

            proc.Parameters.Append(proc.CreateParameter("a_oper", 200, 1, 1, a_oper));
            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, (object)check_null(a_isn_bank_recv));
            proc.Parameters.Append(aIsnParam);
            proc.Parameters.Append(proc.CreateParameter("a_isn_organiz", 3, 1, 0, a_isn_organiz));
            proc.Parameters.Append(proc.CreateParameter("aa_classif_name", 200, 1, 64, a_classif_name));
            proc.Parameters.Append(proc.CreateParameter("a_bank_name", 200, 1, 64, a_bank_name));
            proc.Parameters.Append(proc.CreateParameter("a_acount", 200, 1, 24, a_acount));
            proc.Parameters.Append(proc.CreateParameter("a_subacount", 200, 1, 24, a_subacount));
            proc.Parameters.Append(proc.CreateParameter("a_bik", 200, 1, 9, a_bik));
            proc.Parameters.Append(proc.CreateParameter("aCity", 200, 1, 64, check_null(a_city)));
            proc.Parameters.Append(proc.CreateParameter("a_note", 200, 1, 255, a_note));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                a_isn_bank_recv = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                a_isn_bank_recv = (int?)aIsnParam.Value;
            }
        }

        public static void get_tech_due(
            dynamic oHead,
            int a_isn_contact,
            ref string a_due)
        {
            dynamic proc = oHead.GetProc("get_tech_due");

            proc.Parameters.Append(proc.CreateParameter("a_isn_contact", 3, 1, 0, a_isn_contact));
            dynamic a_dueParam = proc.CreateParameter("a_due", 200, 3, 48, (object)check_null(a_due));
            proc.Parameters.Append(a_dueParam);
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                a_due = string.Empty;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                a_due = (string)a_dueParam.Value;
            }
        }

        public static void save_citizen_web(
            dynamic oHead,
            string aOper,
            ref int? aIsn,
            string aSurname,
            string aZipcode,
            string aCity,
            string aAddress,
            string aDue_region,
            int aIsn_Addr_Category,
            string aCitstatusCl_Due,
            string aPhone,
            string aEmail,
            string aNote,
            string aInn,
            string aSnils,
            int aSex,
            string aSeries,
            string aNPasport,
            string aGiven)
        {
            dynamic proc = oHead.GetProc("save_citizen_web");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 3, aOper));
            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, (object)check_null(aIsn));
            proc.Parameters.Append(aIsnParam);
            proc.Parameters.Append(proc.CreateParameter("aSurname", 200, 1, 64, check_null(aSurname)));
            proc.Parameters.Append(proc.CreateParameter("aZipcode", 200, 1, 12, check_null(aZipcode)));
            proc.Parameters.Append(proc.CreateParameter("aCity", 200, 1, 255, check_null(aCity)));
            proc.Parameters.Append(proc.CreateParameter("aAddress", 200, 1, 255, check_null(aAddress)));
            proc.Parameters.Append(proc.CreateParameter("aDue_region", 200, 1, 48, check_null(aDue_region)));
            proc.Parameters.Append(proc.CreateParameter("aIsn_Addr_Category", 3, 1, 0, aIsn_Addr_Category));
            proc.Parameters.Append(proc.CreateParameter("aCitstatusCl_Due", 200, 1, 48, check_null(aCitstatusCl_Due)));
            proc.Parameters.Append(proc.CreateParameter("aEmail", 200, 1, 64, check_null(aEmail)));
            proc.Parameters.Append(proc.CreateParameter("aPhone", 200, 1, 64, check_null(aPhone)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 255, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aInn", 200, 1, 64, check_null(aInn)));
            proc.Parameters.Append(proc.CreateParameter("aSnils", 200, 1, 14, check_null(aSnils)));
            proc.Parameters.Append(proc.CreateParameter("aSex", 3, 1, 0, check_null(aSex)));
            proc.Parameters.Append(proc.CreateParameter("aSeries", 200, 1, 64, check_null(aSeries)));
            proc.Parameters.Append(proc.CreateParameter("aNPasport", 200, 1, 64, check_null(aNPasport)));
            proc.Parameters.Append(proc.CreateParameter("aGiven", 200, 1, 255, check_null(aGiven)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsn = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aOper = "";
                aIsn = (int?)aIsnParam.Value;
            }
        }

        public static void release_num_web(
            dynamic oHead,
            string aduedocgroup,
            int ayear,
            int aordernum)
        {
            dynamic proc = oHead.GetProc("release_num_web");

            proc.Parameters.Append(proc.CreateParameter("aduedocgroup", 200, 1, 48, check_null(aduedocgroup)));
            proc.Parameters.Append(proc.CreateParameter("ayear", 3, 1, 0, check_null(ayear)));
            proc.Parameters.Append(proc.CreateParameter("aordernum", 3, 1, 0, check_null(aordernum)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void release_prj_num_web(
            dynamic oHead,
            string aduedocgroup,
            int ayear,
            int aordernum)
        {
            dynamic proc = oHead.GetProc("release_prj_num_web");

            proc.Parameters.Append(proc.CreateParameter("aduedocgroup", 200, 1, 48, check_null(aduedocgroup)));
            proc.Parameters.Append(proc.CreateParameter("ayear", 3, 1, 0, check_null(ayear)));
            proc.Parameters.Append(proc.CreateParameter("aordernum", 3, 1, 0, check_null(aordernum)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void write_prot(
           dynamic oHead,
           string aTable_Id,
           string aOper_Id,
           string aSuboper_Id,
           string aOper_Describe,
           int aRef_Isn,
           string aOper_Comment)
        {
            dynamic proc = oHead.GetProc("write_prot");

            proc.Parameters.Append(proc.CreateParameter("aTable_Id", 200, 1, 1, aTable_Id));
            proc.Parameters.Append(proc.CreateParameter("aOper_Id", 200, 1, 1, aOper_Id));
            proc.Parameters.Append(proc.CreateParameter("aSuboper_Id", 200, 1, 1, aSuboper_Id));
            proc.Parameters.Append(proc.CreateParameter("aOper_Describe", 200, 1, 3, aOper_Describe));
            proc.Parameters.Append(proc.CreateParameter("aRef_Isn", 3, 1, 0, aRef_Isn));
            proc.Parameters.Append(proc.CreateParameter("aOper_Comment", 200, 1, 255, check_null(aOper_Comment)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_evnt_queue_item(
           dynamic oHead,
           int aKindEvent,
           string aObjectName,
           string aObjectId,
           string aDueDocgroup,
           string aFlags)
        {
            dynamic proc = oHead.GetProc("add_evnt_queue_item");

            proc.Parameters.Append(proc.CreateParameter("aKindEvent", 3, 1, 0, aKindEvent));
            proc.Parameters.Append(proc.CreateParameter("aObjectName", 200, 1, 48, aObjectName));
            proc.Parameters.Append(proc.CreateParameter("aObjectId", 200, 1, 255, check_null(aObjectId)));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, check_null(aDueDocgroup)));
            proc.Parameters.Append(proc.CreateParameter("aFlags", 200, 1, 2000, check_null(aFlags)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_wf_instance(
            dynamic oHead,
            int aIsnProcessConfig,
            string aMessage,
            string aObjectID,
            string aParams,
            ref int? aIsnInstance)
        {
            dynamic proc = oHead.GetProc("add_wf_instance");

            proc.Parameters.Append(proc.CreateParameter("aIsnProcessConfig", 3, 1, 0, aIsnProcessConfig));
            proc.Parameters.Append(proc.CreateParameter("aMessage", 200, 1, 255, aMessage));
            proc.Parameters.Append(proc.CreateParameter("aObjectID", 200, 1, 255, aObjectID));
            proc.Parameters.Append(proc.CreateParameter("aParams", 200, 1, 2000, check_null(aParams)));
            dynamic aIsnInstanceParam = proc.CreateParameter("aIsnInstance", 3, 3, 0, (object)check_null(aIsnInstance));
            proc.Parameters.Append(aIsnInstanceParam);
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsnInstance = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aIsnInstance = (int?)aIsnInstanceParam.Value;
            }
        }

        public static void add_prj(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            ref int? aIsn, //Isn зарегистрированной РК   
            string aDueDocGroup, //код due картотеки регистрации
            int aOrderNum, //порядковый  номер
            string aFreeNum, //регистрационный номер
            DateTime? aPrjDate, //дата регистрации РК
            int aSecurlevel, //Isn грифа доступа
            string aConsists, //состав
            string aSpecimen, //номера  экземпляров
            DateTime? aPlanDate, //плановая  дата исполнения документа
            string aAnnotat, //содержание
            string aNote, //примечание
            string aDuePersonExe, //код due исполнителя исходящего документа
            int? aIsnLinkingDoc, //Isn связанной РК
            int? aIsnLinkingPrj, //Isn регистрируемого РКПД (в случае  регистрации связанной РК из проекта)
            int? aIsnClLink, //Isn типа связки
            string aCopyShablon, //маска для копирования реквизитов
            int? aEDocument) //флаг "Оригинал в электронном виде"
        {
            dynamic proc = oHead.GetProc("add_prj");
            string maskDate = "yyyyMMdd";


            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, (object)aIsn);
            proc.Parameters.Append(aIsnParam);
            proc.Parameters.Append(proc.CreateParameter("aDueDocGroup", 200, 1, 48, aDueDocGroup));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 3, 1, 0, aOrderNum));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, check_null(aFreeNum)));
            proc.Parameters.Append(proc.CreateParameter("aPrjDate", 200, 1, 20, aPrjDate.HasValue ? aPrjDate.Value.ToString(maskDate) : DateTime.Now.ToString(maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSecurlevel", 3, 1, 0, aSecurlevel == 0 ? 1 : aSecurlevel));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 255, check_null(aConsists)));
            proc.Parameters.Append(proc.CreateParameter("aPlanDate", 200, 1, 20, check_null(aPlanDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aAnnotat", 200, 1, 2000, check_null(aAnnotat)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonExe", 200, 1, 8000, check_null(aDuePersonExe)));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkingDoc", 3, 1, 0, (object)check_null(aIsnLinkingDoc)));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkingRes", 3, 1, 0, (object)check_null(aIsnLinkingPrj)));
            proc.Parameters.Append(proc.CreateParameter("aIsnClLink", 3, 1, 0, (object)check_null(aIsnClLink)));
            proc.Parameters.Append(proc.CreateParameter("aCopyShablon", 200, 1, 20, check_null(aCopyShablon)));
            proc.Parameters.Append(proc.CreateParameter("aEDocument", 3, 1, 0, (object)aEDocument));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsn = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aIsn = (int?)aIsnParam.Value;
            }
        }

        public static void del_prj(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnBatch,
            int aRetNumber,
            int aIsnPrj)
        {
            dynamic proc = oHead.GetProc("del_prj");

            proc.Parameters.Append(proc.CreateParameter("aIsnBatch", 3, 1, 0, aIsnBatch));
            proc.Parameters.Append(proc.CreateParameter("aRetNumber", 3, 1, 0, aRetNumber));
            proc.Parameters.Append(proc.CreateParameter("aIsnPrj", 3, 1, 0, aIsnPrj));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_prj(
            dynamic oHead,
            int aIsn,
            DateTime? aPrjDate,
            int aSecurlevel,
            string aConsists,
            DateTime? aPlanDate,
            string aAnnotat,
            string aNote,
            string aDuePersonExe,
            string aFreeNum,
            int aOrderNum,
            int aEDocument)
        {
            dynamic proc = oHead.GetProc("edit_prj");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aPrjDate", 200, 1, 20, check_null(aPrjDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSeurlevel", 3, 1, 0, aSecurlevel));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 255, check_null(aConsists)));
            proc.Parameters.Append(proc.CreateParameter("aPlanDate", 200, 1, 20, check_null(aPlanDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aAnnotat", 200, 1, 2000, check_null(aAnnotat)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonExe", 200, 1, 48, aDuePersonExe));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, aFreeNum));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 3, 1, 0, (object)aOrderNum));
            proc.Parameters.Append(proc.CreateParameter("aEDocument", 3, 1, 0, (object)aEDocument));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_prj_version(
            dynamic oHead, //головной объект системы, поддерживает интерфейс IHead
            ref int? aIsn, //идентификатор новой версии РКПД. 
            int aIsnOldVersion, //Идентификатор версии РКПД из которой будет сделана новая версия.
            int aCopySecondaryVisa) //Флаг копирования в новую версию записей "вторичного визирования".
        {
            dynamic proc = oHead.GetProc("add_prj_version");

            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, (object)check_null(aIsn));
            proc.Parameters.Append(aIsnParam);
            proc.Parameters.Append(proc.CreateParameter("aIsnOldVersion", 3, 1, 0, aIsnOldVersion));
            proc.Parameters.Append(proc.CreateParameter("aCopySecondaryVisa", 3, 1, 0, aCopySecondaryVisa));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsn = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aIsn = (int?)aIsnParam.Value;
            }
        }

        public static void send_prj_reg(
            dynamic oHead,
            ref int aIsn)//Идентификатор отправляемой на регистрацию РКПД.
        {
            dynamic proc = oHead.GetProc("send_prj_reg");

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void reserve_prj_num(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            string aOper, //тип операции
            string aDueDocgroup, //код due группы документов
            int aYear, //год в счетчике номерообразования РК
            ref int? aOrderNum, //порядковый номер
            ref string aFreeNum, //регистрационный номер
            string aSessionId) //идентификатор сессии
        {
            dynamic proc = oHead.GetProc("reserve_prj_num");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 2, aOper));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aYear", 3, 1, 0, aYear));
            dynamic aOrderNumParam = proc.CreateParameter("aOrderNum", 3, 3, 0, (object)check_null(aOrderNum));
            proc.Parameters.Append(aOrderNumParam);
            dynamic aFreeNumParam = proc.CreateParameter("aFreeNum", 200, 3, 64, check_null(aFreeNum));
            proc.Parameters.Append(aFreeNumParam);
            proc.Parameters.Append(proc.CreateParameter("aSessionId", 200, 1, 255, aSessionId));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aOrderNum = null;
                aFreeNum = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aOrderNum = (int?)aOrderNumParam.Value;
                aFreeNum = (string)aFreeNumParam.Value;
            }
        }

        public static void return_prj_num(
            dynamic oHead,
            string aOper,
            string aDueDocgroup,
            int aYear,
            int? aOrderNum)
        {
            dynamic proc = oHead.GetProc("return_prj_num");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 2, aOper));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aYear", 3, 1, 0, aYear));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 3, 1, 0, (object)check_null(aOrderNum)));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, ""));
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aOrderNum = null;
                throw new Exception(oHead.ErrText);
            }

        }

        public static void save_file_wf(
            dynamic oHead,
            int action,
            int? aISN_REF_FILE,
            int? aISN_RC,
            int? aKind_Doc,
            string aDescription,
            string aCategory,
            int? aSecur,
            int? aLockFlag,
            string aFileAccessDues,
            string aFileName,
            int aDontDelFlag)
        {

            dynamic proc = oHead.GetProc("save_file_wf");
            proc.Parameters.Append(proc.CreateParameter("Action", 3, 1, 0, 1));

            dynamic fileIsnParam = proc.CreateParameter("aISN_REF_FILE", 3, 4, 3, (object)check_null(aISN_REF_FILE));
            proc.Parameters.Append(fileIsnParam);


            //proc.Parameters.Append(proc.CreateParameter("aISN_REF_FILE", 3, 1, 0, null));
            proc.Parameters.Append(proc.CreateParameter("aISN_RC", 3, 1, 0, 4571));
            proc.Parameters.Append(proc.CreateParameter("aKind_Doc", 3, 1, 0, 3));
            proc.Parameters.Append(proc.CreateParameter("aDescription", 200, 3, 255, "test.txt"));
            proc.Parameters.Append(proc.CreateParameter("aCategory", 200, 1, 64, System.DBNull.Value));
            proc.Parameters.Append(proc.CreateParameter("aSecur", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aLockFlag", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aFileAccessDues", 200, 1, 8000, System.DBNull.Value));
            proc.Parameters.Append(proc.CreateParameter("aDontDelFlag", 3, 1, 0, 0));

            proc.Parameters.Append(proc.CreateParameter("aIs_hidden", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aApply_eds", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aSend_enabled", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aSaveSourceFile", 3, 1, 0, 1));

            proc.Parameters.Append(proc.CreateParameter("aFilename", 200, 1, 2000, @"C:\Test.txt"));
            oHead.ExecuteProc(proc);


            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);

            var ISN_REF_FILE = fileIsnParam.Value == DBNull.Value ? null : (int?)fileIsnParam.Value;
            var dd = proc.Parameters.Item("aDescription").Value;

            /*	List<string> files;
				files = new List<string>();

				dynamic doc = oHead.GetRow("RcOut", 4482);

				foreach (var file in doc.Files)
				{
					string newEosFile = file.Descript;

					if (!(file.Contents is DBNull))
					{
						string pathToDownload = @"C:\";

						if (file.Contents.ErrCode == 0)
						{
							file.Contents.Prepare(pathToDownload);
							if (file.Contents.ErrCode == 0)
							{
								files.Add(newEosFile);
								MessageBox.Show("Good");
							}
							else if (file.Contents.ErrCode != 0)
							{
								MessageBox.Show("Poor");
							}
						}
					}
				}

			/*	if (!(file.Contents is DBNull))
				{
					dynamic contents = file.Contents;

					var pathToDownload = @"c:\TEmp\";

					if (contents.ErrCode == 0)
					{
						contents.Prepare(pathToDownload);
						if (contents.ErrCode == 0)
						{

							MessageBox.Show("Гуд");
						}
						else if (contents.ErrCode != 0)
						{
							MessageBox.Show("Плохо");
						}
					}
				}
				*/


            //dynamic proc = oHead.GetProc("SAVE_FILE_WF");

            //proc.Parameters.Append(proc.CreateParameter("action", 3, 1, 0, action));
            //proc.Parameters.Append(proc.CreateParameter("aISN_REF_FILE", 3, 1, 0, check_null(aISN_REF_FILE)));
            //proc.Parameters.Append(proc.CreateParameter("aISN_RC", 3, 1, 0, check_null(aISN_RC)));
            //proc.Parameters.Append(proc.CreateParameter("aKind_Doc", 3, 1, 0, check_null(aKind_Doc)));
            //proc.Parameters.Append(proc.CreateParameter("aDescription", 200, 1, 255, check_null(aDescription)));
            //proc.Parameters.Append(proc.CreateParameter("aCategory", 200, 1, 255, check_null(aCategory)));
            //proc.Parameters.Append(proc.CreateParameter("aSecur", 3, 1, 0, check_null(aSecur)));
            //proc.Parameters.Append(proc.CreateParameter("aLockFlag", 3, 1, 0, check_null(aLockFlag)));
            //proc.Parameters.Append(proc.CreateParameter("aFileAccessDues", 200, 1, 255, check_null(aFileAccessDues)));
            //proc.Parameters.Append(proc.CreateParameter("aFileName", 200, 1, 255, check_null(aFileName)));
            //proc.Parameters.Append(proc.CreateParameter("aDontDelFlag", 3, 1, 0, check_null(aDontDelFlag)));

            //oHead.ExecuteProc(proc);

            //if (oHead.ErrCode < 0)
            //	throw new Exception(oHead.ErrText);
        }

        public static void import_file_eds(
            dynamic oHead,
            int aIsnRefFile,
            string aEdsBody,
            string a_eds_body_blob,
            int? a_isn_sign_kind,
            string a_certificate_owner,
            DateTime? a_signing_date,
            string a_sign_text)
        {
            dynamic proc = oHead.GetProc("import_file_eds");
            string maskDate = "yyyyMMdd HH:mm:ss";

            proc.Parameters.Append(proc.CreateParameter("aIsnRefFile", 3, 1, 0, aIsnRefFile));
            proc.Parameters.Append(proc.CreateParameter("aEdsBody", 200, 1, 2000, check_null(aEdsBody)));
            proc.Parameters.Append(proc.CreateParameter("a_eds_body_blob", 128, 1, 32000, GetFile(a_eds_body_blob)));
            proc.Parameters.Append(proc.CreateParameter("a_isn_sign_kind", 3, 1, 0, check_null(a_isn_sign_kind)));
            proc.Parameters.Append(proc.CreateParameter("a_certificate_owner", 200, 1, 255, a_certificate_owner));
            proc.Parameters.Append(proc.CreateParameter("a_signing_date", 200, 1, 20, check_null(a_signing_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("a_sign_text", 200, 1, 255, a_sign_text));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_file_eds(
            dynamic oHead,
            int a_isn_ref_file,
            string a_eds_body,
            int a_isn_sign_kind,
            string a_due_person,
            int a_flag,
            string a_eds_body_blob,
            string a_certificate_owner,
            DateTime? a_signing_date)
        {
            dynamic proc = oHead.GetProc("add_file_eds");
            string maskDate = "yyyyMMdd HH:mm:ss";

            proc.Parameters.Append(proc.CreateParameter("a_isn_ref_file", 3, 1, 0, a_isn_ref_file));
            proc.Parameters.Append(proc.CreateParameter("a_eds_body", 200, 1, 2000, check_null(a_eds_body)));
            proc.Parameters.Append(proc.CreateParameter("a_isn_sign_kind", 3, 1, 0, check_null(a_isn_sign_kind)));
            proc.Parameters.Append(proc.CreateParameter("a_due_person", 200, 1, 48, a_due_person));
            proc.Parameters.Append(proc.CreateParameter("a_flag", 3, 1, 0, a_flag));
            proc.Parameters.Append(proc.CreateParameter("a_eds_body_blob", 128, 1, 32000, GetFile(a_eds_body_blob)));
            proc.Parameters.Append(proc.CreateParameter("a_certificate_owner", 200, 1, 255, a_certificate_owner));
            proc.Parameters.Append(proc.CreateParameter("a_signing_date", 200, 1, 20, check_null(a_signing_date, maskDate)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_file_eds(
            dynamic oHead,
            int a_isn_ref_file_eds)
        {
            dynamic proc = oHead.GetProc("del_file_eds");

            proc.Parameters.Append(proc.CreateParameter("a_isn_ref_file_eds", 3, 1, 0, a_isn_ref_file_eds));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_corresp(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Идентификатор РК.
            string acard, //Код due текущей картотеки - устарел
            int? acab, //Идентификатор кабинета - устарел
            string aCodes, //Список кодов due из справочника Организации или Гражаден
            string aCorrespNums, //Список исходящих номеров 
            DateTime[] aCorrespDates, //Список исходящих дат
            string aCorrespSigns, //Список «Кто подписал» 
            string aIsnsContact, //Список идентификаторов контакта 
            string aNeedAnswers,
            string aNotes)
        {
            dynamic proc = oHead.GetProc("add_corresp");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, check_null(acard)));
            proc.Parameters.Append(proc.CreateParameter("acab", 3, 1, 0, check_null(acab)));
            proc.Parameters.Append(proc.CreateParameter("aCodes", 200, 1, 8000, aCodes));
            proc.Parameters.Append(proc.CreateParameter("aCorrespNums", 200, 1, 8000, aCorrespNums));
            proc.Parameters.Append(proc.CreateParameter("aCorrespDates", 200, 1, 8000, check_null(aCorrespDates, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespSigns", 200, 1, 8000, check_null(aCorrespSigns)));
            proc.Parameters.Append(proc.CreateParameter("aIsnsContact", 200, 1, 8000, check_null(aIsnsContact)));
            proc.Parameters.Append(proc.CreateParameter("aNeedAnswers", 200, 1, 8000, check_null(aNeedAnswers)));
            proc.Parameters.Append(proc.CreateParameter("aNotes", 200, 1, 8000, check_null(aNotes)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);

        }

        public static void del_corresp(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsn, //Идентификатор записи о корреспонденте РК.
            string acard) //due текущей картотеки. С версии 8.10 параметр устарел, не используется.
        {
            dynamic proc = oHead.GetProc("del_corresp");

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, check_null(acard)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_corresp(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsn, //Идентификатор записи о корреспонденте РК.
            string acard, //Код due текущей картотеки - устарел
            int? acab, //Идентификатор кабинета - устарел
            string aCode, //Код due из справочника 
            int? aisn_contact, //идентификатор контакта
            string aCorrespNum, //Список исходящего номера 
            DateTime? aCorrespDate, //Список исходящей даты
            string aCorrespSign, //«Кто подписал»         
            string aNote)
        {
            dynamic proc = oHead.GetProc("edit_corresp");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("acard", 200, 1, 48, check_null(acard)));
            proc.Parameters.Append(proc.CreateParameter("acab", 3, 1, 0, check_null(acab)));
            proc.Parameters.Append(proc.CreateParameter("aCode", 200, 1, 8000, check_null(aCode)));
            proc.Parameters.Append(proc.CreateParameter("aisn_contact", 200, 1, 8000, check_null(aisn_contact)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespNum", 200, 1, 8000, aCorrespNum));
            proc.Parameters.Append(proc.CreateParameter("aCorrespDate", 200, 1, 8000, check_null(aCorrespDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespSign", 200, 1, 8000, check_null(aCorrespSign)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_cover_doc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnDoc, //Isn зарегистрированной РК   
            string aCard, //код due картотеки регистрации - устарел
            int aCab, //Isn кабинета регистрации - устарел
            string aCodes, //Список кодов due организации
            string aCorrespNums, //исходящий номер входящего документа
            DateTime[] aCorrespDates, //исходящая дата входящего документа
            string aCorrespSigns,
            string aAnnotats,
            string aConsists,
            string aNotes,
            string aIsnsContact)
        {
            dynamic proc = oHead.GetProc("add_cover_doc");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsnDoc", 3, 1, 0, aIsnDoc));
            proc.Parameters.Append(proc.CreateParameter("aCard", 200, 1, 48, check_null(aCard)));
            proc.Parameters.Append(proc.CreateParameter("aCab", 3, 1, 0, check_null(aCab)));
            proc.Parameters.Append(proc.CreateParameter("aCodes", 200, 1, 8000, aCodes));
            proc.Parameters.Append(proc.CreateParameter("aCorrespNums", 200, 1, 8000, aCorrespNums));
            proc.Parameters.Append(proc.CreateParameter("aCorrespDates", 200, 1, 20, check_null(aCorrespDates, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespSigns", 200, 1, 8000, aCorrespSigns));
            proc.Parameters.Append(proc.CreateParameter("aAnnotats", 200, 1, 2000, check_null(aAnnotats)));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 255, check_null(aConsists)));
            proc.Parameters.Append(proc.CreateParameter("aNotes", 200, 1, 8000, check_null(aNotes)));
            proc.Parameters.Append(proc.CreateParameter("aIsnsContact", 200, 1, 8000, aIsnsContact));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_cover_doc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsn, //Идентификатор записи о сопроводительном документе.  
            string aCard, //код due картотеки регистрации - устарел
            int aCab, //Isn кабинета регистрации - устарел
            string aCode, //Код due организации
            string aCorrespNum, //исходящий номер входящего документа
            DateTime? aCorrespDate, //исходящая дата входящего документа
            string aCorrespSign,
            string aAnnotat,
            string aConsist,
            string aNote)
        {
            dynamic proc = oHead.GetProc("edit_cover_doc");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aCard", 200, 1, 48, check_null(aCard)));
            proc.Parameters.Append(proc.CreateParameter("aCab", 3, 1, 0, check_null(aCab)));
            proc.Parameters.Append(proc.CreateParameter("aCode", 200, 1, 8000, aCode));
            proc.Parameters.Append(proc.CreateParameter("aCorrespNum", 200, 1, 8000, aCorrespNum));
            proc.Parameters.Append(proc.CreateParameter("aCorrespDate", 200, 1, 20, check_null(aCorrespDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespSign", 200, 1, 8000, aCorrespSign));
            proc.Parameters.Append(proc.CreateParameter("aAnnotat", 200, 1, 2000, check_null(aAnnotat)));
            proc.Parameters.Append(proc.CreateParameter("aConsist", 200, 1, 255, check_null(aConsist)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 8000, check_null(aNote)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_cover_doc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsn, //Идентификатор записи о сопроводительном документе.  
            string aCard) //код due картотеки регистрации - устарел
        {
            dynamic proc = oHead.GetProc("del_cover_doc");

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aCard", 200, 1, 48, check_null(aCard)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_ar(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnOwner, //Идентификатор владельца доп.реквизита
            string aArName, //API_NAME доп.реквизита
            string aCard, //due текущей картотеки. С версии 8.10 параметр устарел, не используется.
            string aValue) //Значение доп.реквизита
        {
            dynamic proc = oHead.GetProc("edit_ar");

            proc.Parameters.Append(proc.CreateParameter("aIsnOwner", 3, 1, 0, aIsnOwner));
            proc.Parameters.Append(proc.CreateParameter("aArName", 200, 1, 2000, aArName));
            proc.Parameters.Append(proc.CreateParameter("aCard", 200, 1, 48, check_null(aCard)));
            proc.Parameters.Append(proc.CreateParameter("aValue", 200, 1, 48, check_null(aValue)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_prjvisasign(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnPrj,
            int aKind,
            int? an_isn_prj_visa_sign,
            int? an_term,
            int an_term_flag,
            int an_parallel,
            string as_rep_isns)
        {
            dynamic proc = oHead.GetProc("add_prjvisasign");

            proc.Parameters.Append(proc.CreateParameter("aIsnPrj", 3, 1, 0, aIsnPrj));
            proc.Parameters.Append(proc.CreateParameter("aKind", 3, 1, 0, aKind));
            proc.Parameters.Append(proc.CreateParameter("an_isn_prj_visa_sign", 3, 1, 0, check_null(an_isn_prj_visa_sign)));
            proc.Parameters.Append(proc.CreateParameter("an_term", 3, 1, 0, check_null(an_term_flag)));
            proc.Parameters.Append(proc.CreateParameter("an_term_flag", 3, 1, 0, check_null(an_term_flag)));
            proc.Parameters.Append(proc.CreateParameter("an_parallel", 3, 1, 0, an_parallel));
            proc.Parameters.Append(proc.CreateParameter("as_rep_isns", 200, 1, 2000, as_rep_isns));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void send_prjvisasign(
            dynamic oHead,
            int aIsnPrj,
            int? an_term,
            int an_term_flag,
            int an_parallel,
            string as_rep_isns)
        {
            dynamic proc = oHead.GetProc("send_prjvisasign");

            proc.Parameters.Append(proc.CreateParameter("aIsnPrj", 3, 1, 0, aIsnPrj));
            proc.Parameters.Append(proc.CreateParameter("an_term", 3, 1, 0, check_null(an_term_flag)));
            proc.Parameters.Append(proc.CreateParameter("an_term_flag", 3, 1, 0, check_null(an_term_flag)));
            proc.Parameters.Append(proc.CreateParameter("an_parallel", 3, 1, 0, an_parallel));
            proc.Parameters.Append(proc.CreateParameter("as_rep_isns", 200, 1, 2000, as_rep_isns));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void recall_prjvs(
            dynamic oHead,
            int aIsn)
        {
            dynamic proc = oHead.GetProc("recall_prjvs");

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_prjvs(
            dynamic oHead,
            int aIsn,
            int aCascade)
        {
            dynamic proc = oHead.GetProc("del_prjvs");

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aCascade", 3, 1, 0, aCascade));
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void set_prj_visa_sign(
            dynamic oHead,
            int an_isn_prj_visa,
            DateTime ad_rep_date,
            int an_sign_visa_type,
            string as_rep_text, //2000
            int an_reserve_fold)
        {
            dynamic proc = oHead.GetProc("set_prj_visa_sign");
            string maskDate = "yyyyMMdd";

            proc.Parameters.Append(proc.CreateParameter("an_isn_prj_visa", 3, 1, 0, an_isn_prj_visa));
            proc.Parameters.Append(proc.CreateParameter("ad_rep_date", 200, 1, 20, check_null(ad_rep_date, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("an_sign_visa_type", 3, 1, 0, an_sign_visa_type));
            proc.Parameters.Append(proc.CreateParameter("ad_rep_date", 200, 1, 2000, check_null(as_rep_text)));
            proc.Parameters.Append(proc.CreateParameter("an_reserve_fold", 3, 1, 0, an_reserve_fold));
            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_prj_rubric(
            dynamic oHead, //головной объект системы, поддерживает интерфейс IHead
            int aisnprj, //ISN РКПД
            string codes) //Список код due рубрик
        {
            dynamic proc = oHead.GetProc("add_prj_rubric");

            proc.Parameters.Append(proc.CreateParameter("aisnprj", 3, 1, 0, aisnprj));
            proc.Parameters.Append(proc.CreateParameter("codes", 200, 1, 2000, codes));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_reminder(
            dynamic oHead, //головной объект
            int a_isn_reply, //Идентификатор исполнителя отчета.
            string a_reminder_text //Текст напоминания.
            )
        {
            dynamic proc = oHead.GetProc("add_reminder");
            proc.Parameters.Append(proc.CreateParameter("a_isn_reply", 3, 1, 0, a_isn_reply));
            proc.Parameters.Append(proc.CreateParameter("a_reminder_text", 200, 1, 2000, a_reminder_text));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_prj_rubric(
            dynamic oHead, //головной объект
            int a_isn_prj_ref_rubric //Идентификатор  рубрики РКПД.
            )
        {
            dynamic proc = oHead.GetProc("del_prj_rubric");
            proc.Parameters.Append(proc.CreateParameter("a_isn_prj_ref_rubric", 3, 1, 0, a_isn_prj_ref_rubric));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void add_prj_send(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsn, //Isn РК
            string aClassif, //Название справочника, из которого добавляются адресаты
            string aCodes, //Список идентификаторов адресатов
            string aIsnsContact, //Список идентификаторов контактов к добавляемым адресатам.
            string aSendPersons, //Список ФИО “Кому адресован” (для внутренних - null) 
            int? aSendingType) //Тип отправки. Только для версии ЦБ.
        {
            dynamic proc = oHead.GetProc("add_prj_send");

            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));
            proc.Parameters.Append(proc.CreateParameter("aClassif", 200, 1, 20, aClassif));
            proc.Parameters.Append(proc.CreateParameter("aCodes", 200, 1, 2000, aCodes));
            proc.Parameters.Append(proc.CreateParameter("aIsnsContact", 200, 1, 2000, check_null(aIsnsContact)));
            proc.Parameters.Append(proc.CreateParameter("aSendPersons", 200, 1, 2000, check_null(aSendPersons)));
            proc.Parameters.Append(proc.CreateParameter("aSendingType", 3, 1, 0, (object)check_null(aSendingType)));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void del_prj_send(
            dynamic oHead, //головной объект
            int aIsn //Идентификатор записи адресата РКПД.
            )
        {
            dynamic proc = oHead.GetProc("del_prj_send");
            proc.Parameters.Append(proc.CreateParameter("aIsn", 3, 1, 0, aIsn));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        public static void edit_ar_prj(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            int aIsnPrj, //Идентификатор владельца доп.реквизита
            string aArName, //API_NAME доп.реквизита
            string aValue) //Значение доп.реквизита
        {
            dynamic proc = oHead.GetProc("edit_ar_prj");

            proc.Parameters.Append(proc.CreateParameter("aIsnPrj", 3, 1, 0, aIsnPrj));
            proc.Parameters.Append(proc.CreateParameter("aArName", 200, 1, 2000, aArName));
            proc.Parameters.Append(proc.CreateParameter("aValue", 200, 1, 48, aValue));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        static dynamic check_null(dynamic prop, string date_mask)
        {
            dynamic result = Convert.DBNull;

            if (prop != null)
            {
                string typeName = prop.GetType().ToString();
                switch (typeName)
                {
                    case ("System.String"):
                        {
                            string strProp = prop;
                            result = string.IsNullOrEmpty(strProp) ? string.Empty : strProp;
                            break;
                        }
                    case ("System.DateTime"):
                        {
                            DateTime? propDate = prop;
                            if (!string.IsNullOrEmpty(date_mask))
                                result = propDate.HasValue ? propDate.Value.ToString(date_mask) : string.Empty;
                            else result = string.Empty;
                            break;
                        }
                    case ("System.DateTime[]"):
                        {
                            DateTime[] propDate = prop;
                            string outStrDate = "";
                            if (!string.IsNullOrEmpty(date_mask))
                            {
                                for (int i = 0; i < propDate.Length; i++)
                                {
                                    if (i != propDate.Length - 1)
                                    {
                                        if (propDate[i] != DateTime.MinValue)
                                            outStrDate += propDate[i].ToString(date_mask) + "|";
                                        else
                                            outStrDate += Convert.DBNull + "|";
                                    }
                                    else if (propDate[i] != DateTime.MinValue)
                                        outStrDate += propDate[i].ToString(date_mask);
                                }
                                result = outStrDate;
                                break;
                            }
                            result = string.Empty;
                            break;
                        }
                    case ("System.Int32"):
                        {
                            int? propInt = prop;
                            result = propInt.HasValue ? propInt.Value : Convert.DBNull;
                            break;
                        }
                    default:
                        {
                            result = Convert.DBNull;
                            break;
                        }
                }
            }
            return result;
        }

        static dynamic check_null(dynamic prop)
        {
            return check_null(prop, string.Empty);
        }

        static byte[] GetFile(string filePath)
        {
            if (filePath != null)
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var result = new byte[fs.Length];
                    fs.Read(result, 0, result.Length);
                    return result;
                }
            }
            return null;
        }
    }
}
