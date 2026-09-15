using System;
using System.Collections.Generic;
using System.Linq;

namespace Site
{
    /// <summary>
    /// Локальное хранилище данных вместо БД.
    /// Данные живут в памяти приложения и теряются при перезапуске.
    /// </summary>
    public static class LocalData
    {
        private static readonly object _lock = new object();

        // =====================================================================
        // === ПОЛЬЗОВАТЕЛИ ===
        // =====================================================================

        public class UserRecord
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string FIO { get; set; }
            public bool Budget { get; set; }   // доступ к Budget.aspx
            public bool Redakt { get; set; }   // доступ к редактору
            public bool Admin { get; set; }    // админ
        }

        private static List<UserRecord> _users = new List<UserRecord>
        {
            new UserRecord { Login = "admin",    Password = "adminus!", FIO = "Администратор", Budget = true,  Redakt = true,  Admin = true  },
            new UserRecord { Login = "ivanov",   Password = "12345",    FIO = "Иванов И.И.",    Budget = true,  Redakt = false, Admin = false },
            new UserRecord { Login = "petrov",   Password = "qwerty",   FIO = "Петров П.П.",    Budget = false, Redakt = false, Admin = false },
            new UserRecord { Login = "sidorova", Password = "password", FIO = "Сидорова С.С.",  Budget = true,  Redakt = false, Admin = false }
        };

        public static List<UserRecord> GetUsers()
        {
            lock (_lock) return _users.ToList();
        }

        /// <summary>
        /// Проверка логина/пароля. Возвращает null, если не найден.
        /// </summary>
        public static UserRecord Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            lock (_lock)
            {
                return _users.FirstOrDefault(u =>
                    string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase) &&
                    u.Password == password);
            }
        }

        // =====================================================================
        // === БЮДЖЕТ: ФИО ===
        // =====================================================================

        public class FioRecord
        {
            public string Datee { get; set; }
            public string Kfo { get; set; }
            public string Direction { get; set; }
            public double Limitt { get; set; }
            public double ZaklyuchennyyeDogovory { get; set; }
            public double DogovoryVP { get; set; }
            public double NeOsvoyenyLimity { get; set; }
            public double OplachenyDogovory { get; set; }
        }

        private static List<FioRecord> _fio = new List<FioRecord>
        {
            // Тестовые данные: 2 КФО, 2 даты, 3 направления
            new FioRecord { Datee = "2022-01-01", Kfo = "1", Direction = "АИП",      Limitt = 1000000, ZaklyuchennyyeDogovory = 800000,  DogovoryVP = 50000,  NeOsvoyenyLimity = 150000, OplachenyDogovory = 700000 },
            new FioRecord { Datee = "2022-01-01", Kfo = "1", Direction = "ЦФКиС",    Limitt = 500000,  ZaklyuchennyyeDogovory = 450000,  DogovoryVP = 20000,  NeOsvoyenyLimity = 30000,  OplachenyDogovory = 400000 },
            new FioRecord { Datee = "2022-01-01", Kfo = "1", Direction = "СШОР",     Limitt = 300000,  ZaklyuchennyyeDogovory = 250000,  DogovoryVP = 10000,  NeOsvoyenyLimity = 40000,  OplachenyDogovory = 200000 },
            new FioRecord { Datee = "2022-01-01", Kfo = "2", Direction = "АИП",      Limitt = 800000,  ZaklyuchennyyeDogovory = 600000,  DogovoryVP = 30000,  NeOsvoyenyLimity = 170000, OplachenyDogovory = 500000 },
            new FioRecord { Datee = "2022-01-01", Kfo = "2", Direction = "ЦФКиС",    Limitt = 400000,  ZaklyuchennyyeDogovory = 350000,  DogovoryVP = 15000,  NeOsvoyenyLimity = 35000,  OplachenyDogovory = 300000 },
            new FioRecord { Datee = "2022-01-01", Kfo = "2", Direction = "СШОР",     Limitt = 200000,  ZaklyuchennyyeDogovory = 150000,  DogovoryVP = 5000,   NeOsvoyenyLimity = 45000,  OplachenyDogovory = 100000 },
            new FioRecord { Datee = "2022-06-01", Kfo = "1", Direction = "АИП",      Limitt = 1200000, ZaklyuchennyyeDogovory = 950000,  DogovoryVP = 70000,  NeOsvoyenyLimity = 180000, OplachenyDogovory = 850000 },
            new FioRecord { Datee = "2022-06-01", Kfo = "1", Direction = "ЦФКиС",    Limitt = 600000,  ZaklyuchennyyeDogovory = 520000,  DogovoryVP = 25000,  NeOsvoyenyLimity = 55000,  OplachenyDogovory = 450000 },
            new FioRecord { Datee = "2022-06-01", Kfo = "1", Direction = "СШОР",     Limitt = 350000,  ZaklyuchennyyeDogovory = 300000,  DogovoryVP = 15000,  NeOsvoyenyLimity = 35000,  OplachenyDogovory = 250000 },
            new FioRecord { Datee = "2022-06-01", Kfo = "2", Direction = "АИП",      Limitt = 900000,  ZaklyuchennyyeDogovory = 700000,  DogovoryVP = 40000,  NeOsvoyenyLimity = 160000, OplachenyDogovory = 600000 },
            new FioRecord { Datee = "2022-06-01", Kfo = "2", Direction = "ЦФКиС",    Limitt = 450000,  ZaklyuchennyyeDogovory = 400000,  DogovoryVP = 20000,  NeOsvoyenyLimity = 30000,  OplachenyDogovory = 350000 },
            new FioRecord { Datee = "2022-06-01", Kfo = "2", Direction = "СШОР",     Limitt = 250000,  ZaklyuchennyyeDogovory = 200000,  DogovoryVP = 10000,  NeOsvoyenyLimity = 40000,  OplachenyDogovory = 150000 }
        };

        public static List<FioRecord> GetFio()
        {
            lock (_lock) return _fio.ToList();
        }

        public static void ReplaceFio(List<FioRecord> newData)
        {
            lock (_lock) _fio = newData ?? new List<FioRecord>();
        }

        // =====================================================================
        // === БЮДЖЕТ: КОСГУ ===
        // =====================================================================

        public class KosguRecord
        {
            public string Datee { get; set; }
            public string Kfo { get; set; }
            public string Kosgu { get; set; }
            public double Limit2022 { get; set; }
            public double Debt2021 { get; set; }
            public double PaidDebt2021 { get; set; }
            public double SignedAnAgreement { get; set; }
            public double ContractsInTheProcess { get; set; }
            public double PaidContracts2022 { get; set; }
            public double LimitsNotUsed2022 { get; set; }
        }

        private static List<KosguRecord> _kosgu = new List<KosguRecord>
        {
            // Тестовые данные: 2 КФО, 2 даты, 4 КОСГУ
            new KosguRecord { Datee = "2022-01-01", Kfo = "1", Kosgu = "211 ФОТ",                 Limit2022 = 500000, Debt2021 = 10000, PaidDebt2021 = 8000,  SignedAnAgreement = 450000, ContractsInTheProcess = 20000, PaidContracts2022 = 400000, LimitsNotUsed2022 = 50000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "1", Kosgu = "223 Коммуналка",          Limit2022 = 150000, Debt2021 = 5000,  PaidDebt2021 = 4000,  SignedAnAgreement = 140000, ContractsInTheProcess = 5000,  PaidContracts2022 = 120000, LimitsNotUsed2022 = 10000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "1", Kosgu = "224 Аренда",              Limit2022 = 200000, Debt2021 = 8000,  PaidDebt2021 = 7000,  SignedAnAgreement = 180000, ContractsInTheProcess = 10000, PaidContracts2022 = 160000, LimitsNotUsed2022 = 20000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "1", Kosgu = "310 ОС",                  Limit2022 = 300000, Debt2021 = 15000, PaidDebt2021 = 12000, SignedAnAgreement = 250000, ContractsInTheProcess = 30000, PaidContracts2022 = 200000, LimitsNotUsed2022 = 50000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "2", Kosgu = "211 ФОТ",                 Limit2022 = 400000, Debt2021 = 8000,  PaidDebt2021 = 6000,  SignedAnAgreement = 360000, ContractsInTheProcess = 15000, PaidContracts2022 = 300000, LimitsNotUsed2022 = 40000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "2", Kosgu = "223 Коммуналка",          Limit2022 = 120000, Debt2021 = 4000,  PaidDebt2021 = 3000,  SignedAnAgreement = 110000, ContractsInTheProcess = 4000,  PaidContracts2022 = 90000,  LimitsNotUsed2022 = 10000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "2", Kosgu = "224 Аренда",              Limit2022 = 180000, Debt2021 = 6000,  PaidDebt2021 = 5000,  SignedAnAgreement = 160000, ContractsInTheProcess = 8000,  PaidContracts2022 = 140000, LimitsNotUsed2022 = 20000 },
            new KosguRecord { Datee = "2022-01-01", Kfo = "2", Kosgu = "310 ОС",                  Limit2022 = 250000, Debt2021 = 12000, PaidDebt2021 = 10000, SignedAnAgreement = 200000, ContractsInTheProcess = 25000, PaidContracts2022 = 160000, LimitsNotUsed2022 = 50000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "1", Kosgu = "211 ФОТ",                 Limit2022 = 600000, Debt2021 = 12000, PaidDebt2021 = 9000,  SignedAnAgreement = 550000, ContractsInTheProcess = 25000, PaidContracts2022 = 500000, LimitsNotUsed2022 = 50000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "1", Kosgu = "223 Коммуналка",          Limit2022 = 180000, Debt2021 = 6000,  PaidDebt2021 = 5000,  SignedAnAgreement = 170000, ContractsInTheProcess = 6000,  PaidContracts2022 = 150000, LimitsNotUsed2022 = 10000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "1", Kosgu = "224 Аренда",              Limit2022 = 240000, Debt2021 = 9000,  PaidDebt2021 = 8000,  SignedAnAgreement = 220000, ContractsInTheProcess = 12000, PaidContracts2022 = 200000, LimitsNotUsed2022 = 20000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "1", Kosgu = "310 ОС",                  Limit2022 = 360000, Debt2021 = 18000, PaidDebt2021 = 15000, SignedAnAgreement = 300000, ContractsInTheProcess = 35000, PaidContracts2022 = 250000, LimitsNotUsed2022 = 60000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "2", Kosgu = "211 ФОТ",                 Limit2022 = 480000, Debt2021 = 10000, PaidDebt2021 = 8000,  SignedAnAgreement = 430000, ContractsInTheProcess = 18000, PaidContracts2022 = 380000, LimitsNotUsed2022 = 50000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "2", Kosgu = "223 Коммуналка",          Limit2022 = 144000, Debt2021 = 5000,  PaidDebt2021 = 4000,  SignedAnAgreement = 132000, ContractsInTheProcess = 5000,  PaidContracts2022 = 110000, LimitsNotUsed2022 = 12000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "2", Kosgu = "224 Аренда",              Limit2022 = 216000, Debt2021 = 7000,  PaidDebt2021 = 6000,  SignedAnAgreement = 192000, ContractsInTheProcess = 10000, PaidContracts2022 = 170000, LimitsNotUsed2022 = 24000 },
            new KosguRecord { Datee = "2022-06-01", Kfo = "2", Kosgu = "310 ОС",                  Limit2022 = 300000, Debt2021 = 14000, PaidDebt2021 = 12000, SignedAnAgreement = 240000, ContractsInTheProcess = 30000, PaidContracts2022 = 200000, LimitsNotUsed2022 = 60000 }
        };

        public static List<KosguRecord> GetKosgu()
        {
            lock (_lock) return _kosgu.ToList();
        }

        public static void ReplaceKosgu(List<KosguRecord> newData)
        {
            lock (_lock) _kosgu = newData ?? new List<KosguRecord>();
        }

        // =====================================================================
        // === ОБЪЕКТЫ (для Default.aspx) ===
        // =====================================================================

        public class ObjectRecord
        {
            public string UrlMark { get; set; }
            public double Lat { get; set; }
            public double Lagg { get; set; }
            public string NameObject { get; set; }
            public string Adress { get; set; }
            public string NumberObject { get; set; }
            public string Category { get; set; }
            public string District { get; set; }
            public string Supervisor { get; set; }
            public string Tip { get; set; }
            public string Raion { get; set; }
            public string Postuplenie { get; set; }
        }

        private static List<ObjectRecord> _objects = new List<ObjectRecord>
        {
            // 6 тестовых объектов в разных округах Москвы
            new ObjectRecord { UrlMark = "/Logo/marker1.svg", Lat = 55.7558, Lagg = 37.6173, NameObject = "Спорткомплекс №1", Adress = "ул. Тверская, 1",   NumberObject = "001", Category = "Спортивный объект", District = "ЦАО",  Supervisor = "Иванов И.И.", Tip = "Зал",    Raion = "Тверской",  Postuplenie = "АИП" },
            new ObjectRecord { UrlMark = "/Logo/marker2.svg", Lat = 55.7600, Lagg = 37.6200, NameObject = "Бассейн №2",       Adress = "ул. Арбат, 10",      NumberObject = "002", Category = "Спортивный объект", District = "ЦАО",  Supervisor = "Петров П.П.", Tip = "Бассейн", Raion = "Арбат",     Postuplenie = "ЦФКиС" },
            new ObjectRecord { UrlMark = "/Logo/marker3.svg", Lat = 55.8000, Lagg = 37.5000, NameObject = "Ледовый дворец",   Adress = "Ленинградский пр-т, 5", NumberObject = "003", Category = "Спортивный объект", District = "САО",  Supervisor = "Иванов И.И.", Tip = "Лёд",     Raion = "Аэропорт",  Postuplenie = "АИП" },
            new ObjectRecord { UrlMark = "/Logo/marker4.svg", Lat = 55.7000, Lagg = 37.6000, NameObject = "Стадион №4",       Adress = "ул. Ленина, 20",     NumberObject = "004", Category = "Спортивный объект", District = "ЮАО",  Supervisor = "Сидорова С.С.", Tip = "Стадион", Raion = "Донской",   Postuplenie = "СШОР" },
            new ObjectRecord { UrlMark = "/Logo/marker5.svg", Lat = 55.6500, Lagg = 37.5500, NameObject = "ФОК №5",           Adress = "ул. Мира, 15",       NumberObject = "005", Category = "Спортивный объект", District = "ЮЗАО", Supervisor = "Петров П.П.", Tip = "Зал",     Raion = "Академический", Postuplenie = "ЦФКиС" },
            new ObjectRecord { UrlMark = "/Logo/marker6.svg", Lat = 55.8500, Lagg = 37.7000, NameObject = "Спортплощадка №6", Adress = "ул. Космонавтов, 3", NumberObject = "006", Category = "Спортивный объект", District = "СВАО", Supervisor = "Сидорова С.С.", Tip = "Площадка", Raion = "Бабушкинский", Postuplenie = "АИП" }
        };

        public static List<ObjectRecord> GetObjects()
        {
            lock (_lock) return _objects.ToList();
        }

        public static void ReplaceObjects(List<ObjectRecord> newData)
        {
            lock (_lock) _objects = newData ?? new List<ObjectRecord>();
        }

        // =====================================================================
        // === СБРОС (для отладки) ===
        // =====================================================================

        /// <summary>
        /// Сбрасывает все данные к тестовым. Можно вызвать из страницы отладки.
        /// </summary>
        public static void Reset()
        {
            lock (_lock)
            {
                // Сброс пользователей к стандартным
                _users = new List<UserRecord>
                {
                    new UserRecord { Login = "admin",    Password = "adminus!", FIO = "Администратор", Budget = true,  Redakt = true,  Admin = true  },
                    new UserRecord { Login = "ivanov",   Password = "12345",    FIO = "Иванов И.И.",    Budget = true,  Redakt = false, Admin = false },
                    new UserRecord { Login = "petrov",   Password = "qwerty",   FIO = "Петров П.П.",    Budget = false, Redakt = false, Admin = false },
                    new UserRecord { Login = "sidorova", Password = "password", FIO = "Сидорова С.С.",  Budget = true,  Redakt = false, Admin = false }
                };
                // _fio, _kosgu, _objects НЕ трогаем — они заполнены при инициализации
            }
        }
    }
}