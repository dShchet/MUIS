
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
    var isn= page.pathCLean.split('&type:')[0];
    var rcType=page.pathCLean.split('&type:')[1]
deloOneGet(isn, rcType);
function deloOneGet(isn, rcType){
    //data1=[{"ISN":"4694","DOCGROUP":{"ISN":"3678","NAME":"Обращения граждан"},"REGNUM":"Ан-1","SPECIMEN":"1,2","DOCDATE":"04.06.2012 ","CONSIST":"3 листа","CONTENTS":"Проверка регистрации обращения гражданина.","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"True","SECURITY":{"ISN":"16","NAME":"коммерческая тайна"},"NOTE":"примечание","ADDRESS_FLAG":"2","ISCONTROL":"2","PLANDATE":"04.07.2012 ","FACTDATE":"03.07.2012 ","DELTA":"-1","LINKCNT":"1","LINKREF":[{"ISN":"4999","TYPELINK":"Ответ на запрос","ORDERNUM":"1","LINKINFO":"сам док_пдф","URL":"192.168.299.299"}],"RUBRICCNT":"2","RUBRIC":[{"ISN":"4056964","NAME":"Производство товаров, качество продукции","INDEX":"1.1.1"},{"ISN":"4056956","NAME":"Вопросы экологии и землепользования","INDEX":"1.3"}],"ADDPROPSRUBRICCNT":"0","ADDRCNT":"3","ADDR":[{"ISN":"4698","REG_DATE":"24.01.2019 ","CONSIST":"Большой","SENDDATE":"02.01.2019 16:12:00","ORDERNUM":"1","PERSON":"всем","KINDADDR":"ORGANIZ","NOTE":"примеравссч","REG_N":"852","DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"ORGANIZ":{"ISN":"4220","POSTINDEX":"","CITY":"Москва","NAME":"Сбербанк России","ADDRESS":"ул. Балчуг, 2","EMAIL":""}},{"ISN":"4700","REG_DATE":"","CONSIST":"сотав","SENDDATE":"04.01.2019 16:13:00","ORDERNUM":"3","PERSON":"","KINDADDR":"CITIZEN","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"3776","NAME":"Заказная почта"},"CITIZEN":{"ISN":"3939","NAME":"Нестерова Е.П.","ADDRESS":"ул. Ленина, д.5. кв. 56","REGION":"Московская область","POSTINDEX":"143960","CITY":"Реутов","EMAIL":""}},{"ISN":"4701","REG_DATE":"","CONSIST":"","SENDDATE":"02.01.2019 16:13:00","ORDERNUM":"4","PERSON":"","KINDADDR":"DEPARTMENT","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"DEPARTMENT":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1","EMAIL":""}}],"FILESCNT":"2","FILES":[{"ISN":"4704","NAME":"4694-4704.docx","DESCRIPT":"somefile.docx","CONTENTS":"System.__ComObject","EDSCNT":"0","EDS":"System.__ComObject"},{"ISN":"4707","NAME":"4694-4707.xlsx","DESCRIPT":"other somefile.xlsx","CONTENTS":"System.__ComObject","EDSCNT":"0","EDS":"System.__ComObject"}],"JOURNACQCNT":"0","PROTCNT":"10","PROTOCOL":[{"WHEN":"14.12.2018 11:21:56","WHAT":"Регистрация РК"},{"WHEN":"18.01.2019 16:08:20","WHAT":"Уд.<Кому>"},{"WHEN":"18.01.2019 16:10:46","WHAT":"Ред. контрольности РК"},{"WHEN":"18.01.2019 16:13:47","WHAT":"Уд.адресата"},{"WHEN":"18.01.2019 16:13:47","WHAT":"Уд.адресата"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Ред. грифа доступа"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение Рег.№"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Ред. основного раздела РК"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение флага &quot;Срочно&quot;"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение флага &quot;РК персон. доступа&quot;"}],"ALLRESOL":"True","RESOLCNT":"2","RESOLUTION":[{"ISN":"85","KIND":"2","ITEMNUM":"88","AUTHOR_ISN":"","AUTHOR_NAME":"","SURNAME":"","TEXT":"сам текст","RESOLDATE":"","SENDDATE":"18.01.2019 16:12:04","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"31.01.2019 ","FACTDATE":"26.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"3","REPLY":[{"ISN":"86","EXECUTOR_ISN":"3626","EXECUTOR_NAME":"Гончаров А.О. - Зам. ген. директора"},{"ISN":"87","EXECUTOR_ISN":"3628","EXECUTOR_NAME":"Усманов С.У. - Зам. ген. директора"},{"ISN":"88","EXECUTOR_ISN":"4057914","EXECUTOR_NAME":"Наша организация"}],"RESOLCNT":"0"},{"ISN":"82","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3624","AUTHOR_NAME":"Захаров П.Ф. - Генеральный директор","SURNAME":"Захаров П.Ф.","TEXT":"сам текст","RESOLDATE":"18.01.2019 ","SENDDATE":"18.01.2019 16:10:44","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"True","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"26.01.2019 ","FACTDATE":"20.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"83","EXECUTOR_ISN":"3656","EXECUTOR_NAME":"Юренев А.Т. - Зам. нач. управления"},{"ISN":"84","EXECUTOR_ISN":"3654","EXECUTOR_NAME":"Толкачев О.Е. - Нач. управления"}],"RESOLCNT":"0"}],"JOURNALCNT":"5","JOURNAL":[{"ISN":"4996","ADDRESSEE_ISN":"3646","ADDRESSEE_NAME":"Фалеев А.Д. - Нач. отдела № 1","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"02.01.2019 16:13:00"},{"ISN":"4991","ADDRESSEE_ISN":"3656","ADDRESSEE_NAME":"Юренев А.Т. - Зам. нач. управления","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 16:10:00"},{"ISN":"4992","ADDRESSEE_ISN":"3654","ADDRESSEE_NAME":"Толкачев О.Е. - Нач. управления","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"18.01.2019 16:10:00"},{"ISN":"4994","ADDRESSEE_ISN":"3626","ADDRESSEE_NAME":"Гончаров А.О. - Зам. ген. директора","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 16:12:00"},{"ISN":"4995","ADDRESSEE_ISN":"3628","ADDRESSEE_NAME":"Усманов С.У. - Зам. ген. директора","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"18.01.2019 16:12:00"}],"FORWARDCNT":"2","FORWARD":[{"ISN":"4997","ADR_ISN":"3642","ADR_NAME":"Отдел № 1","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"18.01.2019 16:14:05"},{"ISN":"4998","ADR_ISN":"3626","ADR_NAME":"Гончаров А.О. - Зам. ген. директора","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"18.01.2019 16:14:16"}],"ADDPROPSCNT":"0","DOCKIND":"RCLET","ADDRESSESCNT":"2","ADDRESSES":[{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},{"ISN":"3640","NAME":"Экерман Р.Ю. - Зам. начальника управления"}],"DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"TELEGRAM":"","CORRESPCNT":"1","CORRESP":[{"ISN":"4232","KIND":"2","ORGANIZ":"Министерство экономического развития РФ (Минэкономразвития РФ)","OUTNUM":"156","OUTDATE":"10.01.2019 ","SIGN":"сам","CONTENTS":"содерж","NOTE":"прим"}],"ISCOLLECTIVE":"True","ISANONIM":"True","AUTHORCNT":"5","AUTHOR":[{"ISN":"4695","CITIZEN_NAME":"Тихорин В.И.","CITIZEN_ADDRESS":"ул Мойка, д. 4, кв 56","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Санкт-Петербург","ORDERNUM":"1","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"4702","CITIZEN_NAME":"Денисов Г.Р.","CITIZEN_ADDRESS":"ул. Перовская , д. 345, кв. 15","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"2","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"4703","CITIZEN_NAME":"Эльдина Е.Д.","CITIZEN_ADDRESS":"ул. Таганская, д. 45, кв. 89","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"3","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"5000","CITIZEN_NAME":"Ананьева И.В.","CITIZEN_ADDRESS":"ул. Радищева Д. 21, кв 15","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Рязань","ORDERNUM":"4","DATE_CR":"18.01.2019 16:35:22","DATE_UPD":"18.01.2019 16:35:22"},{"ISN":"5001","CITIZEN_NAME":"Родин Н.В.","CITIZEN_ADDRESS":"ул. Советская. д.112, кв. 87","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"5","DATE_CR":"18.01.2019 16:35:22","DATE_UPD":"18.01.2019 16:35:22"}]}];
// data1=dataIN =[{"ISN":"4853","DOCGROUP":{"ISN":"3674","NAME":"Входящие из разных организаций и предприятий"},"REGNUM":"Р-5","SPECIMEN":"1,2","DOCDATE":"04.06.2012 ","CONSIST":"3 листа","CONTENTS":"Проверка регистрации входящего документа.","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"True","SECURITY":{"ISN":"1","NAME":"общий"},"NOTE":"примечание","ADDRESS_FLAG":"2","ISCONTROL":"2","PLANDATE":"04.07.2012 ","FACTDATE":"03.07.2012 ","DELTA":"-1","LINKCNT":"1","LINKREF":[{"ISN":"4969","TYPELINK":"Исполнено","ORDERNUM":"1","LINKINFO":"Проект П-1 от 17/01/2019 Приказы по основной деятельности","URL":""}],"RUBRICCNT":"2","RUBRIC":[{"ISN":"4056964","NAME":"Производство товаров, качество продукции","INDEX":"1.1.1"},{"ISN":"4056956","NAME":"Вопросы экологии и землепользования","INDEX":"1.3"}],"ADDPROPSRUBRICCNT":"0","ADDRCNT":"7","ADDR":[{"ISN":"4858","REG_DATE":"30.01.2019 ","CONSIST":"сост","SENDDATE":"04.01.2019 10:27:00","ORDERNUM":"1","PERSON":"пригожину","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"прим","DELIVERY":{"ISN":"3775","NAME":"Фельдсвязь"},"ORGANIZ":{"ISN":"4223","POSTINDEX":"","CITY":"Москва","NAME":"Межпромбанк","ADDRESS":"ул. Малая Красносельская, 2/8, корп. 4","EMAIL":"info@mprobank.ru"}},{"ISN":"4859","REG_DATE":"","CONSIST":"сост","SENDDATE":"04.01.2019 10:26:00","ORDERNUM":"3","PERSON":"","KINDADDR":"CITIZEN","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"CITIZEN":{"ISN":"3939","NAME":"Нестерова Е.П.","ADDRESS":"ул. Ленина, д.5. кв. 56","REGION":"Московская область","POSTINDEX":"143960","CITY":"Реутов","EMAIL":""}},{"ISN":"4860","REG_DATE":"","CONSIST":"","SENDDATE":"24.01.2019 10:28:00","ORDERNUM":"4","PERSON":"","KINDADDR":"DEPARTMENT","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"DEPARTMENT":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1","EMAIL":""}},{"ISN":"4945","REG_DATE":"29.01.2019 ","CONSIST":"3 листа","SENDDATE":"04.01.2019 10:28:00","ORDERNUM":"5","PERSON":"Юманина Н.М.","KINDADDR":"ORGANIZ","NOTE":"прим","REG_N":"777","DELIVERY":{"ISN":"3779","NAME":"Спецсвязь"},"ORGANIZ":{"ISN":"4246","POSTINDEX":"167000","CITY":"Сыктывкар","NAME":"Филиал республики Коми","ADDRESS":"ул. Бабушкина, д.10","EMAIL":""}},{"ISN":"4947","REG_DATE":"","CONSIST":"3 листа","SENDDATE":"","ORDERNUM":"6","PERSON":"Розова И.С.","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4243","POSTINDEX":"160000","CITY":"Вологда","NAME":"Филиал Вологодской области","ADDRESS":"ул. Гоголя, д.13","EMAIL":""}},{"ISN":"4952","REG_DATE":"","CONSIST":"3 листа","SENDDATE":"","ORDERNUM":"7","PERSON":"","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4057793","POSTINDEX":"107113","CITY":"Москва","NAME":"Наша организация","ADDRESS":"ул. Шумкина, 20, стр.1","EMAIL":"market@eos.ru"}},{"ISN":"4956","REG_DATE":"","CONSIST":"3 листа","SENDDATE":"","ORDERNUM":"8","PERSON":"","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4234","POSTINDEX":"125993","CITY":"Москва","NAME":"Министерство культуры РФ (Минкультуры РФ)","ADDRESS":"Малый Гнездниковский пер., 7","EMAIL":"info@mkrf.ru"}}],"FILESCNT":"0","JOURNACQCNT":"0","PROTCNT":"8","PROTOCOL":[{"WHEN":"14.12.2018 15:36:54","WHAT":"Регистрация РК"},{"WHEN":"17.01.2019 10:33:34","WHAT":"Изменение флага &quot;Срочно&quot;"},{"WHEN":"17.01.2019 10:33:34","WHAT":"Изменение флага &quot;РК персон. доступа&quot;"},{"WHEN":"17.01.2019 10:37:44","WHAT":"Ред. контрольности РК"},{"WHEN":"17.01.2019 10:46:49","WHAT":"Уд.адресата"},{"WHEN":"17.01.2019 10:46:49","WHAT":"Уд.адресата"},{"WHEN":"17.01.2019 10:46:49","WHAT":"Уд.адресата"},{"WHEN":"21.01.2019 11:51:36","WHAT":"Уд.корреспондента"}],"ALLRESOL":"True","RESOLCNT":"3","RESOLUTION":[{"ISN":"66","KIND":"2","ITEMNUM":"1","AUTHOR_ISN":"","AUTHOR_NAME":"","SURNAME":"","TEXT":"сом пункт текст","RESOLDATE":"","SENDDATE":"17.01.2019 10:50:58","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"10.07.2019 ","FACTDATE":"24.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"67","EXECUTOR_ISN":"3646","EXECUTOR_NAME":"Фалеев А.Д. - Нач. отдела № 1"},{"ISN":"68","EXECUTOR_ISN":"4057914","EXECUTOR_NAME":"Наша организация"}],"RESOLCNT":"0"},{"ISN":"69","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3650","AUTHOR_NAME":"Шевченко А.Л. - Нач. отдела № 2","SURNAME":"Шевченко А.Л.","TEXT":"cjv1","RESOLDATE":"13.01.2019 ","SENDDATE":"17.01.2019 12:06:18","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"True","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"17.01.2019 ","FACTDATE":"19.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"70","EXECUTOR_ISN":"3636","EXECUTOR_NAME":"Лихачева С.Л. - Специалист"},{"ISN":"71","EXECUTOR_ISN":"4057915","EXECUTOR_NAME":"Министерство культуры РФ (Минкультуры РФ)"}],"RESOLCNT":"0"},{"ISN":"60","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3648","AUTHOR_NAME":"Кречетов А.В. - Ведущий специалист","SURNAME":"Кречетов А.В.","TEXT":"текст","RESOLDATE":"17.01.2019 ","SENDDATE":"17.01.2019 10:37:43","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"23.01.2019 ","FACTDATE":"18.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"5","REPLY":[{"ISN":"61","EXECUTOR_ISN":"3656","EXECUTOR_NAME":"Юренев А.Т. - Зам. нач. управления"},{"ISN":"62","EXECUTOR_ISN":"3654","EXECUTOR_NAME":"Толкачев О.Е. - Нач. управления"},{"ISN":"63","EXECUTOR_ISN":"4057911","EXECUTOR_NAME":"Розова И.С. - Филиал Вологодской области"},{"ISN":"64","EXECUTOR_ISN":"4057912","EXECUTOR_NAME":"Долгин И.П. - Филиал Вологодской области"},{"ISN":"65","EXECUTOR_ISN":"4057913","EXECUTOR_NAME":"Министерство экономического развития РФ (Минэкономразвития РФ)"}],"RESOLCNT":"0"}],"JOURNALCNT":"7","JOURNAL":[{"ISN":"4939","ADDRESSEE_ISN":"3614","ADDRESSEE_NAME":"Управление по основной деятельности","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"17.01.2019 10:05:00"},{"ISN":"4940","ADDRESSEE_ISN":"3664","ADDRESSEE_NAME":"Королева И.В. - Ведущий специалист","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"17.01.2019 10:06:00"},{"ISN":"4941","ADDRESSEE_ISN":"3662","ADDRESSEE_NAME":"Ломакин Р.А. - Нач. отдела кадров","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"17.01.2019 10:06:00"},{"ISN":"4950","ADDRESSEE_ISN":"3656","ADDRESSEE_NAME":"Юренев А.Т. - Зам. нач. управления","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"17.01.2019 10:37:00"},{"ISN":"4951","ADDRESSEE_ISN":"3654","ADDRESSEE_NAME":"Толкачев О.Е. - Нач. управления","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"17.01.2019 10:37:00"},{"ISN":"4953","ADDRESSEE_ISN":"3646","ADDRESSEE_NAME":"Фалеев А.Д. - Нач. отдела № 1","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"17.01.2019 10:50:00"},{"ISN":"4957","ADDRESSEE_ISN":"3636","ADDRESSEE_NAME":"Лихачева С.Л. - Специалист","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"17.01.2019 12:06:00"}],"FORWARDCNT":"2","FORWARD":[{"ISN":"4866","ADR_ISN":"3624","ADR_NAME":"Захаров П.Ф. - Генеральный директор","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"14.12.2018 15:36:54"},{"ISN":"4867","ADR_ISN":"3626","ADR_NAME":"Гончаров А.О. - Зам. ген. директора","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"14.12.2018 15:36:54"}],"ADDPROPSCNT":"0","DOCKIND":"RCIN","ADDRESSESCNT":"2","ADDRESSES":[{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},{"ISN":"3626","NAME":"Гончаров А.О. - Зам. ген. директора"}],"DELIVERY":{"ISN":"3774","NAME":"Почта"},"TELEGRAM":"","CORRESP":[{"ISN":"4854","KIND":"1","ORGANIZ_ISN":"4216","ORGANIZ_NAME":"КБ &quot;Восток&quot;","OUTNUM":"П-112","OUTDATE":"12.05.2012 ","SIGN":"Карелин В.В.","CONTENTS":"","NOTE":""},{"ISN":"4861","KIND":"1","ORGANIZ_ISN":"4220","ORGANIZ_NAME":"Сбербанк России","OUTNUM":"","OUTDATE":"","SIGN":"","CONTENTS":"","NOTE":""},{"ISN":"4863","KIND":"1","ORGANIZ_ISN":"4225","ORGANIZ_NAME":"Внешторгбанк","OUTNUM":"235-T","OUTDATE":"12.05.2012 ","SIGN":"Петренко П.П. - Главный начальник","CONTENTS":"","NOTE":"ghbv"}],"CORRESPCNT":"3"}];
// data1=dataLET=[{"ISN":"4694","DOCGROUP":{"ISN":"3678","NAME":"Обращения граждан"},"REGNUM":"Ан-1","SPECIMEN":"1,2","DOCDATE":"04.06.2012 ","CONSIST":"3 листа","CONTENTS":"Проверка регистрации обращения гражданина.","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"True","SECURITY":{"ISN":"16","NAME":"коммерческая тайна"},"NOTE":"примечание","ADDRESS_FLAG":"2","ISCONTROL":"2","PLANDATE":"04.07.2012 ","FACTDATE":"03.07.2012 ","DELTA":"-1","LINKCNT":"1","LINKREF":[{"ISN":"4999","TYPELINK":"Ответ на запрос","ORDERNUM":"1","LINKINFO":"сам док_пдф","URL":"192.168.299.299"}],"RUBRICCNT":"2","RUBRIC":[{"ISN":"4056964","NAME":"Производство товаров, качество продукции","INDEX":"1.1.1"},{"ISN":"4056956","NAME":"Вопросы экологии и землепользования","INDEX":"1.3"}],"ADDPROPSRUBRICCNT":"0","ADDRCNT":"3","ADDR":[{"ISN":"4698","REG_DATE":"24.01.2019 ","CONSIST":"Большой","SENDDATE":"02.01.2019 16:12:00","ORDERNUM":"1","PERSON":"всем","KINDADDR":"ORGANIZ","NOTE":"примеравссч","REG_N":"852","DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"ORGANIZ":{"ISN":"4220","POSTINDEX":"","CITY":"Москва","NAME":"Сбербанк России","ADDRESS":"ул. Балчуг, 2","EMAIL":""}},{"ISN":"4700","REG_DATE":"","CONSIST":"сотав","SENDDATE":"04.01.2019 16:13:00","ORDERNUM":"3","PERSON":"","KINDADDR":"CITIZEN","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"3776","NAME":"Заказная почта"},"CITIZEN":{"ISN":"3939","NAME":"Нестерова Е.П.","ADDRESS":"ул. Ленина, д.5. кв. 56","REGION":"Московская область","POSTINDEX":"143960","CITY":"Реутов","EMAIL":""}},{"ISN":"4701","REG_DATE":"","CONSIST":"","SENDDATE":"02.01.2019 16:13:00","ORDERNUM":"4","PERSON":"","KINDADDR":"DEPARTMENT","NOTE":"прим","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"DEPARTMENT":{"ISN":"3646","NAME":"Фалеев А.Д. - Нач. отдела № 1","EMAIL":""}}],"FILESCNT":"2","FILES":[{"ISN":"4704","NAME":"4694-4704.docx","DESCRIPT":"somefile.docx","CONTENTS":"System.__ComObject","EDSCNT":"0","EDS":"System.__ComObject"},{"ISN":"4707","NAME":"4694-4707.xlsx","DESCRIPT":"other somefile.xlsx","CONTENTS":"System.__ComObject","EDSCNT":"0","EDS":"System.__ComObject"}],"JOURNACQCNT":"0","PROTCNT":"10","PROTOCOL":[{"WHEN":"14.12.2018 11:21:56","WHAT":"Регистрация РК"},{"WHEN":"18.01.2019 16:08:20","WHAT":"Уд.<Кому>"},{"WHEN":"18.01.2019 16:10:46","WHAT":"Ред. контрольности РК"},{"WHEN":"18.01.2019 16:13:47","WHAT":"Уд.адресата"},{"WHEN":"18.01.2019 16:13:47","WHAT":"Уд.адресата"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Ред. грифа доступа"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение Рег.№"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Ред. основного раздела РК"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение флага &quot;Срочно&quot;"},{"WHEN":"18.01.2019 16:16:47","WHAT":"Изменение флага &quot;РК персон. доступа&quot;"}],"ALLRESOL":"True","RESOLCNT":"2","RESOLUTION":[{"ISN":"85","KIND":"2","ITEMNUM":"88","AUTHOR_ISN":"","AUTHOR_NAME":"","SURNAME":"","TEXT":"сам текст","RESOLDATE":"","SENDDATE":"18.01.2019 16:12:04","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"31.01.2019 ","FACTDATE":"26.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"3","REPLY":[{"ISN":"86","EXECUTOR_ISN":"3626","EXECUTOR_NAME":"Гончаров А.О. - Зам. ген. директора"},{"ISN":"87","EXECUTOR_ISN":"3628","EXECUTOR_NAME":"Усманов С.У. - Зам. ген. директора"},{"ISN":"88","EXECUTOR_ISN":"4057914","EXECUTOR_NAME":"Наша организация"}],"RESOLCNT":"0"},{"ISN":"82","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3624","AUTHOR_NAME":"Захаров П.Ф. - Генеральный директор","SURNAME":"Захаров П.Ф.","TEXT":"сам текст","RESOLDATE":"18.01.2019 ","SENDDATE":"18.01.2019 16:10:44","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"True","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"26.01.2019 ","FACTDATE":"20.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"83","EXECUTOR_ISN":"3656","EXECUTOR_NAME":"Юренев А.Т. - Зам. нач. управления"},{"ISN":"84","EXECUTOR_ISN":"3654","EXECUTOR_NAME":"Толкачев О.Е. - Нач. управления"}],"RESOLCNT":"0"}],"JOURNALCNT":"5","JOURNAL":[{"ISN":"4996","ADDRESSEE_ISN":"3646","ADDRESSEE_NAME":"Фалеев А.Д. - Нач. отдела № 1","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"02.01.2019 16:13:00"},{"ISN":"4991","ADDRESSEE_ISN":"3656","ADDRESSEE_NAME":"Юренев А.Т. - Зам. нач. управления","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 16:10:00"},{"ISN":"4992","ADDRESSEE_ISN":"3654","ADDRESSEE_NAME":"Толкачев О.Е. - Нач. управления","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"18.01.2019 16:10:00"},{"ISN":"4994","ADDRESSEE_ISN":"3626","ADDRESSEE_NAME":"Гончаров А.О. - Зам. ген. директора","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 16:12:00"},{"ISN":"4995","ADDRESSEE_ISN":"3628","ADDRESSEE_NAME":"Усманов С.У. - Зам. ген. директора","ORIGFLAG":"True","ORIGNUM":"2","SENDDATE":"18.01.2019 16:12:00"}],"FORWARDCNT":"2","FORWARD":[{"ISN":"4997","ADR_ISN":"3642","ADR_NAME":"Отдел № 1","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"18.01.2019 16:14:05"},{"ISN":"4998","ADR_ISN":"3626","ADR_NAME":"Гончаров А.О. - Зам. ген. директора","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"18.01.2019 16:14:16"}],"ADDPROPSCNT":"0","DOCKIND":"RCLET","ADDRESSESCNT":"2","ADDRESSES":[{"ISN":"3624","NAME":"Захаров П.Ф. - Генеральный директор"},{"ISN":"3640","NAME":"Экерман Р.Ю. - Зам. начальника управления"}],"DELIVERY":{"ISN":"3777","NAME":"Телефонограмма"},"TELEGRAM":"","CORRESPCNT":"1","CORRESP":[{"ISN":"4232","KIND":"2","ORGANIZ":"Министерство экономического развития РФ (Минэкономразвития РФ)","OUTNUM":"156","OUTDATE":"10.01.2019 ","SIGN":"сам","CONTENTS":"содерж","NOTE":"прим"}],"ISCOLLECTIVE":"True","ISANONIM":"True","AUTHORCNT":"5","AUTHOR":[{"ISN":"4695","CITIZEN_NAME":"Тихорин В.И.","CITIZEN_ADDRESS":"ул Мойка, д. 4, кв 56","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Санкт-Петербург","ORDERNUM":"1","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"4702","CITIZEN_NAME":"Денисов Г.Р.","CITIZEN_ADDRESS":"ул. Перовская , д. 345, кв. 15","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"2","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"4703","CITIZEN_NAME":"Эльдина Е.Д.","CITIZEN_ADDRESS":"ул. Таганская, д. 45, кв. 89","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"3","DATE_CR":"14.12.2018 11:21:56","DATE_UPD":"14.12.2018 11:21:56"},{"ISN":"5000","CITIZEN_NAME":"Ананьева И.В.","CITIZEN_ADDRESS":"ул. Радищева Д. 21, кв 15","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Рязань","ORDERNUM":"4","DATE_CR":"18.01.2019 16:35:22","DATE_UPD":"18.01.2019 16:35:22"},{"ISN":"5001","CITIZEN_NAME":"Родин Н.В.","CITIZEN_ADDRESS":"ул. Советская. д.112, кв. 87","CITIZEN_REGION":"System.__ComObject","CITIZEN_CITY":"Москва","ORDERNUM":"5","DATE_CR":"18.01.2019 16:35:22","DATE_UPD":"18.01.2019 16:35:22"}]}];
// data1=dataOUT=[{"ISN":"4512","DOCGROUP":{"ISN":"3692","NAME":"Протоколы совещаний"},"REGNUM":"ПР/1-2012","SPECIMEN":"1","DOCDATE":"04.05.2012 ","CONSIST":"2","CONTENTS":"Об утверждении  плана работы на второе полугодие 2012 года","CARDREG":{"ISN":"0","NAME":"Центральная картотека"},"CABREG":{"ISN":"4089","NAME":"Ген. директор"},"ACCESSMODE":"False","SECURITY":{"ISN":"1","NAME":"общий"},"NOTE":"","ADDRESS_FLAG":"1","ISCONTROL":"2","PLANDATE":"","FACTDATE":"","LINKCNT":"2","LINKREF":[{"ISN":"4977","TYPELINK":"Дополнен","ORDERNUM":"1","LINKINFO":"some doc","URL":"192.168.222"},{"ISN":"4984","TYPELINK":"Исполнено","ORDERNUM":"2","LINKINFO":"some22doc","URL":"loclhost"}],"RUBRICCNT":"2","RUBRIC":[{"ISN":"3767","NAME":"Предложение","INDEX":"2.2.3."},{"ISN":"3765","NAME":"Жалоба","INDEX":"2.2.2."}],"ADDPROPSRUBRICCNT":"0","ADDRCNT":"1","ADDR":[{"ISN":"4985","REG_DATE":"","CONSIST":"2","SENDDATE":"","ORDERNUM":"1","PERSON":"","KINDADDR":"ORGANIZ","NOTE":"","REG_N":"","DELIVERY":{"ISN":"","NAME":""},"ORGANIZ":{"ISN":"4057793","POSTINDEX":"107113","CITY":"Москва","NAME":"Наша организация","ADDRESS":"ул. Шумкина, 20, стр.1","EMAIL":"market@eos.ru"}}],"FILESCNT":"0","JOURNACQCNT":"0","PROTCNT":"1","PROTOCOL":[{"WHEN":"18.01.2019 10:28:07","WHAT":"Ред. контрольности РК"}],"ALLRESOL":"True","RESOLCNT":"3","RESOLUTION":[{"ISN":"73","KIND":"2","ITEMNUM":"88","AUTHOR_ISN":"","AUTHOR_NAME":"","SURNAME":"","TEXT":"текст","RESOLDATE":"","SENDDATE":"18.01.2019 10:28:05","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"03.01.2019 ","FACTDATE":"08.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"74","EXECUTOR_ISN":"3656","EXECUTOR_NAME":"Юренев А.Т. - Зам. нач. управления"},{"ISN":"75","EXECUTOR_ISN":"3654","EXECUTOR_NAME":"Толкачев О.Е. - Нач. управления"}],"RESOLCNT":"1"},{"ISN":"79","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3650","AUTHOR_NAME":"Шевченко А.Л. - Нач. отдела № 2","SURNAME":"Шевченко А.Л.","TEXT":"текст","RESOLDATE":"18.01.2019 ","SENDDATE":"18.01.2019 11:56:56","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"True","PLANDATE":"24.01.2019 ","FACTDATE":"23.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"80","EXECUTOR_ISN":"3632","EXECUTOR_NAME":"Плахов А.В. - Управляющий делами"},{"ISN":"81","EXECUTOR_ISN":"3634","EXECUTOR_NAME":"Каблукова М.В. - Зам. управделами"}],"RESOLCNT":"0"},{"ISN":"76","KIND":"1","ITEMNUM":"","AUTHOR_ISN":"3634","AUTHOR_NAME":"Каблукова М.В. - Зам. управделами","SURNAME":"Каблукова М.В.","TEXT":"САМ текст","RESOLDATE":"18.01.2019 ","SENDDATE":"18.01.2019 11:46:25","ACCEPTFLAG":"False","ISCONTROL":"2","ISPRIVATE":"False","CANVIEW":"True","ISCASCADE":"False","PLANDATE":"20.01.2019 ","FACTDATE":"19.01.2019 ","RESPRJPRIORITY":"","REPLYCNT":"2","REPLY":[{"ISN":"77","EXECUTOR_ISN":"3660","EXECUTOR_NAME":"Рубановский Л.Б. - Гл. юрист"},{"ISN":"78","EXECUTOR_ISN":"4057914","EXECUTOR_NAME":"Наша организация"}],"RESOLCNT":"0"}],"JOURNALCNT":"11","JOURNAL":[{"ISN":"4522","ADDRESSEE_ISN":"3632","ADDRESSEE_NAME":"Плахов А.В. - Управляющий делами","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"04.05.2012 10:55:00"},{"ISN":"4523","ADDRESSEE_ISN":"3638","ADDRESSEE_NAME":"Портнов И.А. - Начальник управления","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"04.05.2012 10:55:00"},{"ISN":"4524","ADDRESSEE_ISN":"3654","ADDRESSEE_NAME":"Толкачев О.Е. - Нач. управления","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"04.05.2012 10:55:00"},{"ISN":"4525","ADDRESSEE_ISN":"3658","ADDRESSEE_NAME":"Адвокатов П.Б. - Нач. отдела","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"04.05.2012 10:55:00"},{"ISN":"4526","ADDRESSEE_ISN":"3662","ADDRESSEE_NAME":"Ломакин Р.А. - Нач. отдела кадров","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"04.05.2012 10:55:00"},{"ISN":"4527","ADDRESSEE_ISN":"3666","ADDRESSEE_NAME":"Соломатин Ю.В. - Зав. архивом","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"04.05.2012 10:55:00"},{"ISN":"4987","ADDRESSEE_ISN":"3632","ADDRESSEE_NAME":"Плахов А.В. - Управляющий делами","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 9:56:00"},{"ISN":"4981","ADDRESSEE_ISN":"3656","ADDRESSEE_NAME":"Юренев А.Т. - Зам. нач. управления","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 10:28:00"},{"ISN":"4982","ADDRESSEE_ISN":"3654","ADDRESSEE_NAME":"Толкачев О.Е. - Нач. управления","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"18.01.2019 10:28:00"},{"ISN":"4986","ADDRESSEE_ISN":"3660","ADDRESSEE_NAME":"Рубановский Л.Б. - Гл. юрист","ORIGFLAG":"True","ORIGNUM":"1","SENDDATE":"18.01.2019 11:46:00"},{"ISN":"4988","ADDRESSEE_ISN":"3634","ADDRESSEE_NAME":"Каблукова М.В. - Зам. управделами","ORIGFLAG":"False","ORIGNUM":"0","SENDDATE":"18.01.2019 11:56:00"}],"FORWARDCNT":"6","FORWARD":[{"ISN":"4516","ADR_ISN":"3632","ADR_NAME":"Плахов А.В. - Управляющий делами","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"04.05.2012 10:55:03"},{"ISN":"4517","ADR_ISN":"3638","ADR_NAME":"Портнов И.А. - Начальник управления","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"04.05.2012 10:55:03"},{"ISN":"4518","ADR_ISN":"3654","ADR_NAME":"Толкачев О.Е. - Нач. управления","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"04.05.2012 10:55:03"},{"ISN":"4519","ADR_ISN":"3658","ADR_NAME":"Адвокатов П.Б. - Нач. отдела","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"04.05.2012 10:55:03"},{"ISN":"4520","ADR_ISN":"3662","ADR_NAME":"Ломакин Р.А. - Нач. отдела кадров","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"04.05.2012 10:55:03"},{"ISN":"4521","ADR_ISN":"3666","ADR_NAME":"Соломатин Ю.В. - Зав. архивом","USER_ISN":"3611","USER_NAME":"Тверская У.Л.","SENDDATE":"04.05.2012 10:55:03"}],"ADDPROPSCNT":"0","PERSONSIGNSCNT":"1","PERSONSIGNS":[{"WHO_SIGN_ISN":"3624","WHO_SIGN_NAME":"Захаров П.Ф. - Генеральный директор","ORDERNUM":"1"}],"EXECUTOR":{"ISN":"3634","NAME":"Каблукова М.В. - Зам. управделами"},"COEXECCNT":"2","COEXEC":[{"ISN":"4978","ORGANIZ_ISN":"4246","ORGANIZ_NAME":"Филиал республики Коми","OUTNUM":"156","OUTDATE":"09.01.2019 ","SIGN":"Юманина Н.М."},{"ISN":"4979","ORGANIZ_ISN":"4240","ORGANIZ_NAME":"Филиал г. Санкт-Петербурга","OUTNUM":"185","OUTDATE":"04.01.2019 ","SIGN":"Панина М.В."}],"VISACNT":"2","VISA":[{"ISN":"4980","EMPLOY_ISN":"3654","EMPLOY_NAME":"Толкачев О.Е. - Нач. управления","NOTE":"888","DATE":"15.01.2019 "},{"ISN":"4983","EMPLOY_ISN":"3636","EMPLOY_NAME":"Лихачева С.Л. - Специалист","NOTE":"999","DATE":"20.01.2019 "}]}];

    $.ajax({
        url: INFO.deloAdr+"?need="+"one"+"&isn="+isn+"&rcType="+rcType,
        type: "GET", contentType: "text/plain",
        success: function (data2) {
            //   data=data1[0];
              data=data2[0];
            function header(){
                var html=REGNUM=SECURITY=DELIVERY=SPECIMEN=CONSIST=
                PLANDATE=FACTDATE=ISCOLLECTIVE=NOTE=DOCGROUP=DOCDATE="";
                html+='<div class="head"  data-isn="' + data.ISN + '">';
                REGNUM=data.REGNUM ? ('<p>РК № '+data.REGNUM+'</p>'):"";
                DOCGROUP=data.DOCGROUP.NAME ? '<p>'+data.DOCGROUP.NAME+'</p>':"";
                DOCDATE=data.DOCDATE ? '<p><b>От </b>'+data.DOCDATE.split(' ')[0]+'</p>':"";
                SPECIMEN=data.SPECIMEN ? '<p><b>Экз №: </b>'+data.SPECIMEN+'</p>':"";
                if(data.SECURITY){SECURITY=data.SECURITY.NAME ? '<p><b>Доступ: </b>'+data.SECURITY.NAME+'</p>':"";}
                CONSIST=data.CONSIST ? '<p><b>Состав: </b>'+data.CONSIST+'</p>':"";
                if(data.DELIVERY) { DELIVERY=data.DELIVERY ?'<p><b>Доставка: </b>'+data.DELIVERY.NAME+'</p>':"";}
                PLANDATE=data.PLANDATE ? '<p><b>План.: </b>'+data.PLANDATE+'</p>':"";
                FACTDATE=data.FACTDATE ? '<p><b>Факт.: </b>'+data.FACTDATE+'</p>':"";
                ISCOLLECTIVE=(data.ISCOLLECTIVE==="True") ? '<p>Коллективное</p>':"";
                CONTENTS=data.CONTENTS ? '<p><b>Содерж.: </b>'+data.CONTENTS+'</p>':"";
                NOTE=data.NOTE ? '<p><b>Примечание: </b>'+data.NOTE+'</p>':"";
                html+='<div>'+REGNUM+DOCGROUP+'</div>';
                html+='<div>'+DOCDATE+SPECIMEN+SECURITY+CONSIST+DELIVERY+'</div>';
                html+='<div>'+PLANDATE+FACTDATE+ISCOLLECTIVE+'</div>';
                html+='<div>'+CONTENTS+'</div>';
                html+='<div>'+NOTE+'</div>';
                html+='</div>';
                return html;
            };
            function FILES(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ФАЙЛЫ ('+data.FILESCNT+')</b></div>';
                html+='<div class="caseDetails">';
                    if((data.FILES)||(data.FILESCNT)){
                    for (let i = 0; i < data.FILESCNT; i++) {
                        var el = data.FILES[i];
                        html+='<div class="caseItem">';
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR">';
                        html+='<div><i data-isn="'+el.ISN+'" data-name="'+el.NAME+'">'+el.DESCRIPT+'</i></div>';
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function SIGNS(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ПОДПИСАЛИ ('+data.PERSONSIGNSCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.PERSONSIGNS)||(data.PERSONSIGNSCNT)){
                    for (let i = 0; i < data.PERSONSIGNSCNT; i++) {
                        var el=data.PERSONSIGNS[i];
                        html+='<div class="caseItem">';
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" >';
                        html+='<p><i data-isn="'+el.WHO_SIGN_ISN+'">'+el.WHO_SIGN_NAME+'</i></p>';
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function EXECUTOR(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ИСПОЛНИТЕЛИ ('+1+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.EXECUTOR)||(data.PERSONSIGNSCNT)){
                    html+='<div class="caseItem">';
                    //for (let i = 0; i < data.PERSONSIGNSCNT; i++) {
                        var i=0;
                        if(data.EXECUTOR){
                            var el = data.EXECUTOR;
                            html+='<div class="caseDetR">';
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" >';
                            html+='<p><i data-isn="'+el.ISN+'"> '+el.NAME+'</i></p>';
                            html+='</div>';
                            html+='</div>';
                        }
                    //}
                    html+='</div>';
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function VISA(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ВИЗЫ ('+data.VISACNT+')</b></div>';
                if((data.VISA)||(data.VISACNT)){
                    html+='<div class="caseDetails">';
                    for (let i = 0; i < data.VISACNT; i++) {
                        var DATE=date=time="";
                        var el = data.VISA[i];
                        if(el.DATE){
                            date=el.DATE.split(' ')[0];
                            time=el.DATE.split(' ')[1];
                            time=(time=="")?"":' в: '+time;
                            DATE='<p><b>Дата: </b>'+date+time+'</p>';
                        }
                        NAME=(el.EMPLOY_NAME&&el.EMPLOY_ISN) ? "<p><i data-isn='"+el.EMPLOY_ISN+"'>"+el.EMPLOY_NAME+"</i><p>":"";
                        NOTE=el.NOTE ? "<p><b>Прим.: </b>"+el.NOTE+"</p>":"";
                        html+='<div class="caseItem">';
                        html+='<div class="itemNumber">'+(i+1)+' </div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<div data-isn="'+el.EMPLOY_ISN+'">'+NAME+DATE+'</div>';
                        html+='<div>'+NOTE+'</div>';
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function AUTHOR(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>АВТОРЫ ('+data.AUTHORCNT+')</b></div>';
                if((data.AUTHOR)||(data.AUTHORCNT)){
                    html+='<div class="caseDetails">';
                    for (let i = 0; i < data.AUTHORCNT; i++) {
                        html+='<div class="caseItem">';
                        var NAME=DATE_UPD=CITIZEN_CITY="";
                        var el = data.AUTHOR[i];
                        NAME=el.CITIZEN_NAME ? el.CITIZEN_NAME+" ":"";
                        CITIZEN_CITY=el.CITIZEN_CITY ? '<p>- '+el.CITIZEN_CITY+'</p>':"";
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<div><i data-isn="'+el.ISN+'"> '+NAME+CITIZEN_CITY+'</i></div>'
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function CORRESP(){
                data.CORRESP=data.CORRESP?data.CORRESP:0;
                data.CORRESPCNT=data.CORRESPCNT?data.CORRESPCNT:0;
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>КОРРЕСПОНДЕНТЫ ('+data.CORRESPCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.CORRESPCNT>0)&&(data.CORRESP)){
                    for (let i = 0; i < data.CORRESPCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.CORRESP[i];
                        var ORGANIZ=el.ORGANIZ_NAME ? '<p><i data-isn="'+el.ORGANIZ_ISN+'">'+el.ORGANIZ_NAME+"</i></p>":"";
                        var OUTNUM=el.OUTNUM ? '<p><b>Исх. №: </b>'+el.OUTNUM+'</p>':"";
                        var OUTDATE=el.OUTDATE ? '<p><b>Дата: </b>'+el.OUTDATE+'</p>':"";
                        var SIGN=el.SIGN ? '<p><b>Подписал: </b>'+el.SIGN+'</p>':"";
                        var NOTE=el.NOTE ? '<p><b>Прим.: </b>'+el.NOTE+'</p>':"";
                        html+='<div class="itemNumber">'+(i+1)+' </div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<div> '+ORGANIZ+OUTNUM+OUTDATE+SIGN+'</div>';
                        html+='<div>'+NOTE+'</div>';
                        html+='</div>';
                        html+='</div>';
                        console.log(el.ORGANIZ_NAME);
                    }
                }
                html+='</div>';
                html+='</div>';
                return html;
            }
            function komy(){
                var html="";
                if((data.ADDRESSES)||(data.ADDRESSESCNT)){
                    html+='<div class="case">'+
                    '<div class="caseShort closed"><b>КОМУ ('+data.ADDRESSESCNT+')</b></div>';
                        html+='<div class="caseDetails">';
                            for (let i = 0; i < data.ADDRESSESCNT; i++){
                                html+='<div class="caseItem">';
                                html+='<div class="itemNumber">'+(i+1)+' </div>';
                                html+='<div class="caseDetR">';
                                html+='<div><i data-isn="'+data.ADDRESSES[i].ISN+'"> '+data.ADDRESSES[i].NAME+'</i></div>';
                                html+='</div>';
                                html+='</div>';
                            }
                    html+='</div>'
                    html+='</div>';
                }
                return html;
            }
            function RUBRIC(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>РУБРИКИ ('+data.RUBRICCNT+')</b></div>';
                if((data.RUBRIC)||(data.RUBRICCNT)){
                    html+='<div class="caseDetails">';
                        for (let i = 0; i < data.RUBRICCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.RUBRIC[i];
                            var NAME=el.NAME ? '<p>'+el.NAME+' </p>':"";
                            var INDEX=el.INDEX ? '<p>('+el.INDEX+') </p>':"";
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<div><i data-isn="'+el.ISN+'"> '+NAME+INDEX+'</i></div>';
                            html+='</div>';
                            html+='</div>';
                        }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function LINK(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>СВЯЗКИ ('+data.LINKCNT+')</b></div>';
                if((data.LINKREF)||(data.LINKCNT)){
                    html+='<div class="caseDetails">';
                        for (let i = 0; i < data.LINKCNT; i++) {
                            html+='<div class="caseItem">';
                            var el = data.LINKREF[i];
                            html+='<div class="itemNumber">'+(i+1)+'</div>';
                            html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                            html+='<div><p>'+el.TYPELINK+' <i href="'+el.URL+'">'+el.LINKINFO+'</i></p></div>';
                            html+='</div>';
                            html+='</div>';
                        }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function PORUCH(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ПОРУЧЕНИЯ ('+data.RESOLCNT+')</b></div>';
                if((data.RESOLCNT)||(data.RESOLCNT)){
                    html+='<div class="caseDetails">';
                    for (let i = 0; i < data.RESOLCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.RESOLUTION[i];
                        html+='<div class="itemNumber">'+(i+1)+' </div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        if(el.KIND==1){
                            var AUTHOR=el.AUTHOR_NAME ? ("<p><b>Автор: </b><i data-isn='"+el.AUTHOR_ISN+"'>" + el.AUTHOR_NAME+"</i></p>" ) : "";
                            var RESOLDATE=el.RESOLDATE ? ("<p><b>От:</b> " + el.RESOLDATE +"</p>"): "";
                            html+='<div>'+AUTHOR+RESOLDATE+'</div>';
                        }else if(el.KIND==2){
                            html+='<div><b>Пункт №</b> '+el.ITEMNUM+'</div>';
                        }
                        var PLANDATE=el.PLANDATE ? ("<p><b>План:</b> " + el.PLANDATE +"</p>"): "";
                        var FACTDATE=el.FACTDATE ? ("<p><b>Факт:</b> " + el.FACTDATE +"</p>"): "";
                        var CATEGORY=el.RESPRJPRIORITY ? ("<b>Категория:</b> " + el.RESPRJPRIORITY +" "): "";
                        var TEXT=el.TEXT ? ("<b>Текст:</b> " + el.TEXT +" "): "";
                        html+='<div>'+PLANDATE+FACTDATE+CATEGORY+'</div>';
                        html+='<div>'+TEXT+'</div>';
                        if(el.REPLYCNT>0){
                            html+='<div>Исполнители ('+el.REPLYCNT+"):";
                            for (let i = 0; i < el.REPLYCNT; i++){
                                var el2=el.REPLY[i];
                                html+="<div data-isn='"+el2.EXECUTOR_ISN+"'><i>"+el2.EXECUTOR_NAME+"</i></div>";
                            }
                            html+="</div>";
                        }
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function ADDR(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>АДРЕСАТЫ ('+data.ADDRCNT+')</b></div>';
                if((data.ADDR)||(data.ADDRCNT)){
                    html+='<div class="caseDetails">';
                    for (let i = 0; i < data.ADDRCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.ADDR[i];
                        var ORIGFLAG=REGION=POSTINDEX=CITY=ADDRESS=EMAIL=DELIVERY="";
                        if(el.ORIGFLAG=="True"){ORIGFLAG="Оригинал";}else if(el.ORIGFLAG=="False"){ORIGFLAG="Копия";}else{ORIGFLAG="";}
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        if(el.SENDDATE){
                            var datesmall=el.SENDDATE.split(' ')[0];
                            var time=el.SENDDATE.split(' ')[1].slice(0, -3);
                            var DATE='<p><b>Дата:</b> '+datesmall+' <b>в:</b> '+time+"</p>";
                        }else{DATE=""}
                        var typeStruct=el[el["KINDADDR"]];
                        var CONSIST=el.CONSIST ? "<p><b>Состав:</b> "+el.CONSIST+"</p>":"";
                        var PERSON=el.PERSON ? "<p><b>Кому:</b> "+el.PERSON+"</p>":"";
                        var NOTE=el.NOTE ? "<p><b>Примечание: </b> "+el.NOTE+"</p>":"";
                        var REG_N=el.REG_N ? "<p><b>Рег. №: </b> "+el.REG_N+"</p>":"";
                        var REG_DATE=el.REG_DATE ? "<p><b>Рег. дата: </b> "+el.REG_DATE+"</p>":"";
                        if(typeStruct){
                            var REGION=typeStruct.REGION ? "<p>"+typeStruct.REGION+"</p>":"";
                            var POSTINDEX=typeStruct.POSTINDEX ? "<p>"+typeStruct.POSTINDEX+"</p>":"";
                            var CITY=typeStruct.CITY ? "<p>"+typeStruct.CITY+"</p>":"";
                            var ADDRESS=typeStruct.ADDRESS ? "<p>"+typeStruct.ADDRESS+"</p>":"";
                            if(el.DELIVERY&&el.DELIVERY.ISN){DELIVERY="<p><b>Вид отправки:</b> "+el.DELIVERY.NAME+"</p>"}else{DELIVERY="";}
                            var EMAIL=typeStruct.EMAIL?"<p><b>Email:</b> <a href='mailto:"+typeStruct.EMAIL+"'><i>"+typeStruct.EMAIL+"</i></a></p>":"";
                            html+='<div><i>'+typeStruct.NAME+'</i></div>';
                            html+='<div>'+DATE+ORIGFLAG+DELIVERY+CONSIST+PERSON+'</div>';
                            if(REGION||POSTINDEX||CITY||ADDRESS){html+='<div><b>Адрес:</b> '+
                            REGION+POSTINDEX+CITY+ADDRESS+EMAIL+'</div>';}
                            html+='<div>'+NOTE+'</div>';
                            html+='<div>'+REG_N+REG_DATE+'</div>';
                        }
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function COEXEC(){
                data.COEXEC=data.COEXEC?data.COEXEC:0;
                data.COEXECCNT=data.COEXECCNT?data.COEXECCNT:0;
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>СОИСПОЛНИТЕЛИ  ('+data.COEXECCNT+')</b></div>';
                if((data.COEXEC)||(data.COEXECCNT)){
                    html+='<div class="caseDetails">';
                    for (let i = 0; i < data.COEXECCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.COEXEC[i];
                        var ORGANIZ=el.ORGANIZ_NAME ? ("<p><i data-isn='"+el.ORGANIZ_ISN+"'>"+el.ORGANIZ_NAME+"</i></p>"):"";
                        var OUTNUM=el.OUTNUM ? '<p><b>Исх. №: </b>'+el.OUTNUM+'</p>':"";
                        var OUTDATE=el.OUTDATE ? '<p><b>Дата: </b>'+el.OUTDATE+'</p>':"";
                        var SIGN=el.SIGN ? '<p><b>Подписал: </b>'+el.SIGN+'</p>':"";
                        html+='<div class="itemNumber">'+(i+1)+' </div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<div data-isn="'+el.ISN+'"> ';
                        html+='<div>'+ORGANIZ+'</div>';
                        html+='<div>'+OUTNUM+OUTDATE+SIGN+'</div>';
                        html+='</div>';
                        html+='</div>';
                        html+='</div>';
                    }
                    html+='</div>';
                }
                html+='</div>';
                return html;
            }
            function JOURNAL(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ЖУРНАЛ ПЕРЕДАЧИ ДОКУМЕНТА ('+data.JOURNALCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.JOURNALCNT>0)&&(data.JOURNAL)){
                    for (let i = 0; i < data.JOURNALCNT; i++) {
                        html+='<div class="caseItem">';
                        var el = data.JOURNAL[i];
                        var ORIGFLAG=ORIGNUM=SENDDATE=NAME="";
                        ORIGNUM=((el.ORIGNUM)&&(el.ORIGNUM!=0))? " "+el.ORIGNUM:"";
                        if(el.ORIGFLAG=="True"){ORIGFLAG="<p>Оригинал </p>";}else if(el.ORIGFLAG=="False"){ORIGFLAG="<p>Копия </p>";}else{ORIGFLAG=underfined;}
                        NAME=el.ADDRESSEE_NAME ? ("<p><i>"+el.ADDRESSEE_NAME+"</i></p>"):"";
                        SENDDATE=el.SENDDATE ? ("<p>" + el.SENDDATE.slice(0, -3) +"</p>"): "";
                        
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        html+='<div> '+SENDDATE+NAME+ORIGFLAG+ORIGNUM+'</div>';
                        html+='</div>';
                        html+='</div>';
                    }
                }
                html+='</div>';
                html+='</div>';
                return html;
            }
            function FORWARD(){
                var html="";
                html='<div class="case">'+
                '<div class="caseShort closed"><b>ПЕРЕСЫЛКА ('+data.FORWARDCNT+')</b></div>';
                html+='<div class="caseDetails">';
                if((data.FORWARDCNT>0)&&(data.FORWARD)){
                    for (let i = 0; i < data.FORWARDCNT; i++) {
                        html+='<div class="caseItem">';
                        var NAME=USER=SENDDATE="";
                        var el = data.FORWARD[i];
                        NAME=el.ADR_NAME ? ("<p><i>"+el.ADR_NAME+"</i></p>"):"";
                        SENDDATE=el.SENDDATE ? ("<p><b>Дата: </b>" + el.SENDDATE.slice(0, -3) +"</p>"): "";
                        USER=el.USER_NAME ? ("<p><b>Отправитель: </b>" + el.USER_NAME +"</p>"): "";
                        html+='<div class="itemNumber">'+(i+1)+'</div>';
                        html+='<div class="caseDetR" data-isn="'+el.ISN+'">';
                        if(NAME){html+='<div>'+NAME+SENDDATE+'</div>';}
                        if(USER){html+='<div>'+USER+'</div>';}
                        html+='</div>';
                        html+='</div>';
                    }
                }
                html+='</div>';
                html+='</div>';
                return html;
            }
            var footer='</div>';

            var row="";
            row+=header();
            row+=FILES();
            if (rcType=="RCOUT"){row+=SIGNS();}
            if (rcType=="RCOUT"){row+=EXECUTOR();}
            if (rcType=="RCOUT"){row+=VISA();}
            if (rcType=="RCLET"){row+=AUTHOR();}
            if (rcType=="RCIN" ){row+=CORRESP();}
            //if ((rcType=="RCLET")||(rcType=="RCIN")){row+=CORRESP();}
            row+=komy();
            row+=RUBRIC();
            row+=LINK();
            row+=PORUCH();
            row+=ADDR();
            if(rcType=="RCOUT"){row+=COEXEC();}
            row+=JOURNAL();
            row+=FORWARD();
            row+=footer;
            $(".result").html(row);            
            $(".caseShort").click(function(){$(this).toggleClass('closed')});
         },
         error: function (jqXHR, exception) {
            $(".result").html(exception);
            console.log(exception);
            console.log(jqXHR);},
    });
}



