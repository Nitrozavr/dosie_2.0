using System;
using System.Collections.Generic;
using System.Linq;
using static Site.LocalData;

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
            public string Id { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string FIO { get; set; }
            public string Name { get; set; }
            public string Familia { get; set; }
            public string Otchestvo { get; set; }
            public string Otdel { get; set; }
            public string Doljnost { get; set; }
            public string Number { get; set; }
            public int KolAuth { get; set; }
            public string DateAuth { get; set; }

            // Права доступа (соответствуют чекбоксам в Users.aspx)
            public bool RedactObject { get; set; }
            public bool OsnovInf { get; set; }
            public bool IngSys { get; set; }
            public bool TehDoc { get; set; }
            public bool Kategor { get; set; }
            public bool Proch { get; set; }
            public bool SportZone { get; set; }
            public bool RedactZone { get; set; }
            public bool Useres { get; set; }
            public bool RedactBloknot1 { get; set; }
            public bool RedactBloknot2 { get; set; }
            public bool ReesterRedact { get; set; }
            public bool ReesterProsroch { get; set; }
            public bool RedaktOG { get; set; }
            public bool RedaktDrObject { get; set; }
            public bool Reester { get; set; }
            public bool SystemStatus { get; set; }
            public bool RedaktStatus { get; set; }
            public bool Priem { get; set; }
            public bool AdminPriem { get; set; }
            public bool Documents { get; set; }
            public bool Budget { get; set; }
            public bool RedaktBudget { get; set; }

            // Совместимость с Authorization.aspx.cs
            public bool Admin { get; set; }
            public bool Redakt { get; set; }
        }

        private static List<UserRecord> _users = new List<UserRecord>
        {
            new UserRecord { Id = "1", Login = "admin", Password = "adminus!", FIO = "Администратор",
                Name = "Админ", Familia = "Админ", Otchestvo = "Админ", Otdel = "Руководство",
                Doljnost = "Администратор", Number = "+7(999)111-11-11", KolAuth = 0, DateAuth = "2026-09-15",
                RedactObject = true, OsnovInf = true, IngSys = true, TehDoc = true, Kategor = true,
                Proch = true, SportZone = true, RedactZone = true, Useres = true, RedactBloknot1 = true,
                RedactBloknot2 = true, ReesterRedact = true, ReesterProsroch = true, RedaktOG = true,
                RedaktDrObject = true, Reester = true, SystemStatus = true, RedaktStatus = true,
                Priem = true, AdminPriem = true, Documents = true, Budget = true, RedaktBudget = true,
                Admin = true, Redakt = true },

            new UserRecord { Id = "2", Login = "ivanov", Password = "12345", FIO = "Иванов И.И.",
                Name = "Иван", Familia = "Иванов", Otchestvo = "Иванович", Otdel = "Технический",
                Doljnost = "Инженер", Number = "+7(999)222-22-22", KolAuth = 5, DateAuth = "2026-09-10",
                RedactObject = true, OsnovInf = true, IngSys = true, TehDoc = true, Kategor = true,
                Proch = false, SportZone = false, RedactZone = false, Useres = false,
                Reester = true, SystemStatus = true, Budget = true, Admin = false, Redakt = false },

            new UserRecord { Id = "3", Login = "petrov", Password = "qwerty", FIO = "Петров П.П.",
                Name = "Пётр", Familia = "Петров", Otchestvo = "Петрович", Otdel = "Юридический",
                Doljnost = "Юрист", Number = "+7(999)333-33-33", KolAuth = 2, DateAuth = "2026-09-12",
                RedactObject = false, Useres = false, Admin = false, Redakt = false },

            new UserRecord { Id = "4", Login = "sidorova", Password = "password", FIO = "Сидорова С.С.",
                Name = "Светлана", Familia = "Сидорова", Otchestvo = "Сергеевна", Otdel = "Руководство",
                Doljnost = "Руководитель", Number = "+7(999)444-44-44", KolAuth = 10, DateAuth = "2026-09-14",
                RedactObject = true, OsnovInf = true, IngSys = true, Useres = false,
                Admin = false, Redakt = false }
        };

        public static List<UserRecord> GetUsers()
        {
            lock (_lock) return _users.ToList();
        }

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

        public static void ReplaceUsers(List<UserRecord> newData)
        {
            lock (_lock) _users = newData ?? new List<UserRecord>();
        }

        /// <summary>
        /// Обновляет права доступа конкретного пользователя по его Id.
        /// </summary>
        public static void UpdateUserPermissions(UserRecord updatedUser)
        {
            if (updatedUser == null) return;

            lock (_lock)
            {
                var existing = _users.FirstOrDefault(u => u.Id == updatedUser.Id);
                if (existing == null) return;

                existing.RedactObject = updatedUser.RedactObject;
                existing.OsnovInf = updatedUser.OsnovInf;
                existing.IngSys = updatedUser.IngSys;
                existing.TehDoc = updatedUser.TehDoc;
                existing.Kategor = updatedUser.Kategor;
                existing.Proch = updatedUser.Proch;
                existing.SportZone = updatedUser.SportZone;
                existing.RedactZone = updatedUser.RedactZone;
                existing.Useres = updatedUser.Useres;
                existing.RedactBloknot1 = updatedUser.RedactBloknot1;
                existing.RedactBloknot2 = updatedUser.RedactBloknot2;
                existing.ReesterRedact = updatedUser.ReesterRedact;
                existing.ReesterProsroch = updatedUser.ReesterProsroch;
                existing.RedaktOG = updatedUser.RedaktOG;
                existing.RedaktDrObject = updatedUser.RedaktDrObject;
                existing.Reester = updatedUser.Reester;
                existing.SystemStatus = updatedUser.SystemStatus;
                existing.RedaktStatus = updatedUser.RedaktStatus;
                existing.Priem = updatedUser.Priem;
                existing.AdminPriem = updatedUser.AdminPriem;
                existing.Documents = updatedUser.Documents;
                existing.Budget = updatedUser.Budget;
                existing.RedaktBudget = updatedUser.RedaktBudget;
            }
        }

        /// <summary>
        /// Обновляет одно право пользователя по имени свойства.
        /// </summary>
        public static void UpdateUserPermission(string userId, string permissionName, bool value)
        {
            lock (_lock)
            {
                var user = _users.FirstOrDefault(u => u.Id == userId);
                if (user == null) return;

                var prop = typeof(UserRecord).GetProperty(permissionName);
                if (prop != null && prop.PropertyType == typeof(bool))
                    prop.SetValue(user, value);
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
        // === ОБЪЕКТЫ (для Default.aspx, Contact.aspx) ===
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
            public string NumberSuper { get; set; }
            public string Tip { get; set; }
            public string Raion { get; set; }
            public string Postuplenie { get; set; }
            public string FunckSoderj { get; set; }
            public string DopPoten { get; set; }
            public string KlassPoDepar { get; set; }
            public string SportsZone { get; set; }
            public string Indexx { get; set; }
            public string UrlImage { get; set; }
            public string UrlDow { get; set; }
            public string CadastralNumber { get; set; }
            public string ConstructionYear { get; set; }
            public string Levels { get; set; }
            public string CommissioningDate { get; set; }
            public string SqureBuildings { get; set; }
            public string SqureTerritory { get; set; }
            public string TerritoriesUsed { get; set; }
            public string GreenSpaces { get; set; }
            public string Statuss { get; set; }
            public string Electrosnab { get; set; }
            public string Vodsnab { get; set; }
            public string OZDS { get; set; }
        }

        private static List<ObjectRecord> _objects = new List<ObjectRecord>
{
    // === 1. ЦАО, Тверской ===
    new ObjectRecord {
        UrlMark = "/Logo/marker.svg",
        Lat = 55.796374, Lagg = 37.507289,
        NameObject = "Спорткомплекс №1",
        Adress = "ул. Тверская, 1",
        NumberObject = "001",
        Category = "Спортивный объект",
        District = "ЦАО",
        Supervisor = "Иванов И.И.",
        NumberSuper = "+7(495)111-11-11",
        Tip = "Зал",
        Raion = "Тверской",
        Postuplenie = "АИП",
        FunckSoderj = "Объект",
        DopPoten = "Подходит под 636",
        KlassPoDepar = "Зал",
        SportsZone = "Спортивная зона 1",
        Indexx = "125009"
    },

    // === 2. ЦАО, Арбат ===
    new ObjectRecord {
        UrlMark = "/Logo/marker.svg",
        Lat = 55.712750,  Lagg = 37.468640,
        NameObject = "Бассейн №2",
        Adress = "ул. Арбат, 10",
        NumberObject = "002",
        Category = "Спортивный объект",
        District = "ЦАО",
        Supervisor = "Петров П.П.",
        NumberSuper = "+7(495)222-22-22",
        Tip = "Бассейн",
        Raion = "Арбат",
        Postuplenie = "ЦФКиС",
        FunckSoderj = "Объект",
        DopPoten = "Подходит под сдачу в аренду",
        KlassPoDepar = "Бассейн",
        SportsZone = "Спортивная зона 2",
        Indexx = "119019"
    },

    // === 3. САО, Аэропорт (Ленинградский проспект) ===
    new ObjectRecord {
        UrlMark = "/Logo/marker.svg",
        Lat = 55.635193, Lagg = 37.534106,
        NameObject = "Ледовый дворец",
        Adress = "Ленинградский пр-т, 5",
        NumberObject = "003",
        Category = "Спортивный объект",
        District = "САО",
        Supervisor = "Иванов И.И.",
        NumberSuper = "+7(495)333-33-33",
        Tip = "Лёд",
        Raion = "Аэропорт",
        Postuplenie = "АИП",
        FunckSoderj = "Объект",
        DopPoten = "Подходит под 636",
        KlassPoDepar = "Лёд",
        SportsZone = "Спортивная зона 3",
        Indexx = "125167"
    },

    // === 4. ЮАО, Донской (ул. Ленина — условный адрес) ===
    new ObjectRecord {
        UrlMark = "/Logo/marker.svg",
        Lat = 55.708900, Lagg = 37.612300,
        NameObject = "Стадион №4",
        Adress = "ул. Ленина, 20",
        NumberObject = "004",
        Category = "Спортивный объект",
        District = "ЮАО",
        Supervisor = "Сидорова С.С.",
        NumberSuper = "+7(495)444-44-44",
        Tip = "Стадион",
        Raion = "Донской",
        Postuplenie = "СШОР",
        FunckSoderj = "Объект",
        DopPoten = "Подходит под реновацию/для инвестора",
        KlassPoDepar = "Стадион",
        SportsZone = "Спортивная зона 4",
        Indexx = "115280"
    },

    // === 5. ЦАО, Мещанский (ул. Мира — рядом с Проспектом Мира) ===
    new ObjectRecord {
        UrlMark = "/Logo/marker.svg",
        Lat = 55.615557, Lagg = 37.710786,
        NameObject = "ФОК №5",
        Adress = "ул. Мира, 15",
        NumberObject = "005",
        Category = "Спортивный объект",
        District = "ЦАО",
        Supervisor = "Петров П.П.",
        NumberSuper = "+7(495)555-55-55",
        Tip = "Зал",
        Raion = "Мещанский",
        Postuplenie = "ЦФКиС",
        FunckSoderj = "Объект",
        DopPoten = "Подходит под сдачу в аренду",
        KlassPoDepar = "Зал",
        SportsZone = "Спортивная зона 5",
        Indexx = "129090"
    },

    // === 6. СВАО, Бабушкинский (ул. Космонавтов) ===
    new ObjectRecord {
        UrlMark = "/Logo/marker.svg",
        Lat = 55.713195, Lagg = 37.764421,
        NameObject = "Спортплощадка №6",
        Adress = "ул. Космонавтов, 3",
        NumberObject = "006",
        Category = "Спортивный объект",
        District = "СВАО",
        Supervisor = "Сидорова С.С.",
        NumberSuper = "+7(495)666-66-66",
        Tip = "Площадка",
        Raion = "Бабушкинский",
        Postuplenie = "АИП",
        FunckSoderj = "Объект",
        DopPoten = "Подходит под 636",
        KlassPoDepar = "Площадка",
        SportsZone = "Спортивная зона 6",
        Indexx = "129301"
    }
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
        // === ДОКУМЕНТЫ (для Documents.aspx) ===
        // =====================================================================

        public class DocumentRecord
        {
            public string Doc { get; set; }
            public string DocType { get; set; }
            public string StorageLocation { get; set; }
            public string ResponsiblePerson { get; set; }
            public string ResponsibleDepartment { get; set; }
            public string Department { get; set; }
            public string RegulatoryDocumentIn { get; set; }
            public string RegulatoryDocumentEx { get; set; }
            public string Frequency { get; set; }
        }

        private static List<DocumentRecord> _documents = new List<DocumentRecord>
        {
            new DocumentRecord { Doc = "Журнал инструктажа", DocType = "Журнал", StorageLocation = "Офис", ResponsiblePerson = "Иванов И.И.", ResponsibleDepartment = "ОТ", Department = "Охрана труда", RegulatoryDocumentIn = "Приказ 1979", RegulatoryDocumentEx = "Инструкция №1", Frequency = "Ежегодно" },
            new DocumentRecord { Doc = "Акт проверки", DocType = "Акт", StorageLocation = "Архив", ResponsiblePerson = "Петров П.П.", ResponsibleDepartment = "ОТ", Department = "Охрана труда", RegulatoryDocumentIn = "Приказ 1979", RegulatoryDocumentEx = "Инструкция №2", Frequency = "Ежемесячно" },
            new DocumentRecord { Doc = "Паспорт объекта", DocType = "Паспорт", StorageLocation = "Офис", ResponsiblePerson = "Сидорова С.С.", ResponsibleDepartment = "Тех", Department = "Технический", RegulatoryDocumentIn = "Приказ 1980", RegulatoryDocumentEx = "Инструкция №3", Frequency = "При вводе" },
            new DocumentRecord { Doc = "План эвакуации", DocType = "План", StorageLocation = "Стенд", ResponsiblePerson = "Иванов И.И.", ResponsibleDepartment = "ОТ", Department = "Пожарная безопасность", RegulatoryDocumentIn = "Приказ 1979", RegulatoryDocumentEx = "Инструкция №4", Frequency = "1 раз в квартал" },
            new DocumentRecord { Doc = "Инструкция по ТБ", DocType = "Инструкция", StorageLocation = "Офис", ResponsiblePerson = "Петров П.П.", ResponsibleDepartment = "ОТ", Department = "Охрана труда", RegulatoryDocumentIn = "Приказ 1979", RegulatoryDocumentEx = "Инструкция №5", Frequency = "Ежегодно" }
        };

        public static List<DocumentRecord> GetDocuments()
        {
            lock (_lock) return _documents.ToList();
        }

        public static void ReplaceDocuments(List<DocumentRecord> newData)
        {
            lock (_lock) _documents = newData ?? new List<DocumentRecord>();
        }

        // =====================================================================
        // === СБРОС (для отладки) ===
        // =====================================================================

        public static void Reset()
        {
            lock (_lock)
            {
                _users = new List<UserRecord>
                {
                    new UserRecord { Id = "1", Login = "admin",    Password = "adminus!", FIO = "Администратор", Useres = true, Budget = true, Redakt = true, Admin = true, RedactObject = true, OsnovInf = true, IngSys = true, TehDoc = true, Kategor = true, Proch = true, SportZone = true, RedactZone = true, SystemStatus = true, Reester = true, Priem = true, AdminPriem = true, Documents = true, RedaktBudget = true, RedactBloknot1 = true, RedactBloknot2 = true, ReesterRedact = true, ReesterProsroch = true, RedaktOG = true, RedaktDrObject = true, RedaktStatus = true },
                    new UserRecord { Id = "2", Login = "ivanov",   Password = "12345",    FIO = "Иванов И.И.",    Budget = true,  Redakt = false, Admin = false },
                    new UserRecord { Id = "3", Login = "petrov",   Password = "qwerty",   FIO = "Петров П.П.",    Budget = false, Redakt = false, Admin = false },
                    new UserRecord { Id = "4", Login = "sidorova", Password = "password", FIO = "Сидорова С.С.",  Budget = true,  Redakt = false, Admin = false }
                };
            }
        }
        // =====================================================================
        // === ИНФОГРАФИКА: ОБРАЩЕНИЯ ГРАЖДАН (doc_rows_og) ===
        // =====================================================================

        public class DocRowRecord
        {
            public int Id { get; set; }
            public string NumberDoc { get; set; }
            public string NameObject { get; set; }
            public string DataReg { get; set; }
            public string Filter1 { get; set; }
            public string Filter2 { get; set; }
            public string Filter3 { get; set; }
            public string Istock { get; set; }
        }

        private static List<DocRowRecord> _docRows = new List<DocRowRecord>
        {
            new DocRowRecord { Id = 1,  NumberDoc = "001", NameObject = "Спорткомплекс №1", DataReg = "2024-01-15", Filter1 = "Жалоба на качество", Filter2 = "Обслуживание", Filter3 = "Персонал", Istock = "Тикеты" },
            new DocRowRecord { Id = 2,  NumberDoc = "002", NameObject = "Бассейн №2",       DataReg = "2024-01-20", Filter1 = "Жалоба на качество", Filter2 = "Чистота",      Filter3 = "Уборка",   Istock = "Почта" },
            new DocRowRecord { Id = 3,  NumberDoc = "003", NameObject = "Спорткомплекс №1", DataReg = "2024-02-05", Filter1 = "Предложение",        Filter2 = "Развитие",      Filter3 = "Инфраструктура", Istock = "MОСЭДО" },
            new DocRowRecord { Id = 4,  NumberDoc = "004", NameObject = "Ледовый дворец",   DataReg = "2024-02-10", Filter1 = "Жалоба на качество", Filter2 = "Обслуживание", Filter3 = "Персонал", Istock = "Тикеты" },
            new DocRowRecord { Id = 5,  NumberDoc = "005", NameObject = "Стадион №4",       DataReg = "2024-02-15", Filter1 = "Предложение",        Filter2 = "Развитие",      Filter3 = "Инфраструктура", Istock = "Почта" },
            new DocRowRecord { Id = 6,  NumberDoc = "006", NameObject = "Бассейн №2",       DataReg = "2024-03-01", Filter1 = "Жалоба на качество", Filter2 = "Обслуживание",  Filter3 = "Персонал", Istock = "Тикеты" },
            new DocRowRecord { Id = 7,  NumberDoc = "007", NameObject = "Спорткомплекс №1", DataReg = "2024-03-12", Filter1 = "Жалоба на качество", Filter2 = "Чистота",      Filter3 = "Уборка",   Istock = "MОСЭДО" },
            new DocRowRecord { Id = 8,  NumberDoc = "008", NameObject = "Ледовый дворец",   DataReg = "2024-03-20", Filter1 = "Предложение",        Filter2 = "Развитие",      Filter3 = "Инфраструктура", Istock = "Почта" },
            new DocRowRecord { Id = 9,  NumberDoc = "009", NameObject = "Спорткомплекс №1", DataReg = "2024-04-05", Filter1 = "Жалоба на качество", Filter2 = "Обслуживание",  Filter3 = "Персонал", Istock = "Тикеты" },
            new DocRowRecord { Id = 10, NumberDoc = "010", NameObject = "Бассейн №2",       DataReg = "2024-04-15", Filter1 = "Благодарность",      Filter2 = "Обслуживание",  Filter3 = "Персонал", Istock = "Тикеты" }
        };

        public static List<DocRowRecord> GetDocRows()
        {
            lock (_lock) return _docRows.ToList();
        }

        public static void ReplaceDocRows(List<DocRowRecord> newData)
        {
            lock (_lock) _docRows = newData ?? new List<DocRowRecord>();
        }

        // =====================================================================
        // === ИНФОГРАФИКА: РЕДАКТИРУЕМЫЕ ЗАПИСИ (infographics) ===
        // =====================================================================

        public class InfographicsRecord
        {
            public int Id { get; set; }
            public string DataReg { get; set; }
            public string TypeObr { get; set; }
            public string ObjectName { get; set; }
            public string Vopros { get; set; }
            public string Zayavitel { get; set; }
            public string Rezultat { get; set; }
        }

        private static List<InfographicsRecord> _infographics = new List<InfographicsRecord>
        {
            new InfographicsRecord { Id = 1, DataReg = "2024-04-01", TypeObr = "Жалоба",      ObjectName = "Спорткомплекс №1", Vopros = "Качество обслуживания",      Zayavitel = "Иванов И.И.", Rezultat = "Рассмотрено" },
            new InfographicsRecord { Id = 2, DataReg = "2024-04-01", TypeObr = "Предложение", ObjectName = "Бассейн №2",       Vopros = "Улучшение инфраструктуры",   Zayavitel = "Петров П.П.", Rezultat = "Принято" }
        };

        public static List<InfographicsRecord> GetInfographics()
        {
            lock (_lock) return _infographics.ToList();
        }

        public static void ReplaceInfographics(List<InfographicsRecord> newData)
        {
            lock (_lock) _infographics = newData ?? new List<InfographicsRecord>();
        }

        public static void AddInfographics(InfographicsRecord record)
        {
            lock (_lock)
            {
                record.Id = _infographics.Count > 0 ? _infographics.Max(x => x.Id) + 1 : 1;
                _infographics.Add(record);
            }
        }

        public static void UpdateInfographics(InfographicsRecord record)
        {
            lock (_lock)
            {
                var existing = _infographics.FirstOrDefault(x => x.Id == record.Id);
                if (existing == null) return;
                existing.TypeObr = record.TypeObr;
                existing.ObjectName = record.ObjectName;
                existing.Vopros = record.Vopros;
                existing.Zayavitel = record.Zayavitel;
                existing.Rezultat = record.Rezultat;
            }
        }

        public static void DeleteInfographics(int id)
        {
            lock (_lock) _infographics.RemoveAll(x => x.Id == id);
        }

        // =====================================================================
        // === ИНФОГРАФИКА: ТЕКСТОВАЯ СПРАВКА (spravka) ===
        // =====================================================================

        public class SpravkaRecord
        {
            public int Id { get; set; }
            public string DataReg { get; set; }
            public string TextSpravki { get; set; }
        }

        private static List<SpravkaRecord> _spravka = new List<SpravkaRecord>
        {
            new SpravkaRecord { Id = 1, DataReg = "2024-04-01", TextSpravki = "По итогам рассмотрения обращений граждан за апрель 2024 года..." },
            new SpravkaRecord { Id = 2, DataReg = "2024-04-01", TextSpravki = "Наибольшее количество обращений поступило по вопросам качества обслуживания." }
        };

        public static List<SpravkaRecord> GetSpravka()
        {
            lock (_lock) return _spravka.ToList();
        }

        public static void AddSpravka(SpravkaRecord record)
        {
            lock (_lock)
            {
                record.Id = _spravka.Count > 0 ? _spravka.Max(x => x.Id) + 1 : 1;
                _spravka.Add(record);
            }
        }

        public static void UpdateSpravka(SpravkaRecord record)
        {
            lock (_lock)
            {
                var existing = _spravka.FirstOrDefault(x => x.Id == record.Id);
                if (existing != null) existing.TextSpravki = record.TextSpravki;
            }
        }

        public static void DeleteSpravka(int id)
        {
            lock (_lock) _spravka.RemoveAll(x => x.Id == id);
        }

        // =====================================================================
        // === ИНФОГРАФИКА: ИСТОЧНИКИ (istochnik) ===
        // =====================================================================

        public class IstochnikRecord
        {
            public string Istock { get; set; }
            public int M11 { get; set; }
            public int M10 { get; set; }
            public int M9 { get; set; }
            public int M8 { get; set; }
            public int M7 { get; set; }
            public int M6 { get; set; }
            public int M5 { get; set; }
            public int M4 { get; set; }
            public int M3 { get; set; }
            public int M2 { get; set; }
            public int M1 { get; set; }
            public int Current { get; set; }
        }

        private static List<IstochnikRecord> _istochniki = new List<IstochnikRecord>
        {
            new IstochnikRecord { Istock = "MОСЭДО", M11 = 2, M10 = 3, M9 = 5, M8 = 4, M7 = 6, M6 = 8, M5 = 7,  M4 = 9,  M3 = 10, M2 = 12, M1 = 15, Current = 20 },
            new IstochnikRecord { Istock = "Тикеты", M11 = 5, M10 = 6, M9 = 8, M8 = 7, M7 = 9, M6 = 11, M5 = 10, M4 = 12, M3 = 14, M2 = 16, M1 = 18, Current = 22 },
            new IstochnikRecord { Istock = "Помощь", M11 = 1, M10 = 2, M9 = 3, M8 = 2, M7 = 4, M6 = 5, M5 = 4,  M4 = 6,  M3 = 7,  M2 = 8,  M1 = 10, Current = 12 }
        };

        public static List<IstochnikRecord> GetIstochniki()
        {
            lock (_lock) return _istochniki.ToList();
        }

        public static void ReplaceIstochniki(List<IstochnikRecord> newData)
        {
            lock (_lock) _istochniki = newData ?? new List<IstochnikRecord>();
        }
        // =====================================================================
        // === СПОРТИВНЫЕ ЗОНЫ ===
        // =====================================================================

        public class SportsZoneRecord
        {
            public string NameObject { get; set; }
            public string NameZone { get; set; }
            public string Square { get; set; }
            public string Size { get; set; }
            public string ZoneType { get; set; }
            public string TypeComp { get; set; }
            public string Coating { get; set; }
            public string ChangingRooms { get; set; }
            public string PossibleUse { get; set; }
            public string SportsEquipment { get; set; }
            public string EPS { get; set; }
            public string EPS_IAS { get; set; }
            public string UrlImage { get; set; }
        }

        private static List<SportsZoneRecord> _sportsZones = new List<SportsZoneRecord>
        {
            new SportsZoneRecord { NameObject = "Спорткомплекс №1", NameZone = "Зал №1", Square = "500", Size = "20x25", ZoneType = "Универсальный", TypeComp = "Региональный", Coating = "Паркет", ChangingRooms = "2", PossibleUse = "Баскетбол, волейбол", SportsEquipment = "Есть", EPS = "Да", EPS_IAS = "Да", UrlImage = "/Photo_zone/zone1.jpg" },
            new SportsZoneRecord { NameObject = "Спорткомплекс №1", NameZone = "Зал №2", Square = "300", Size = "15x20", ZoneType = "Тренажерный", TypeComp = "Муниципальный", Coating = "Резина", ChangingRooms = "1", PossibleUse = "Тренажеры", SportsEquipment = "Есть", EPS = "Да", EPS_IAS = "Нет", UrlImage = "/Photo_zone/zone2.jpg" },
            new SportsZoneRecord { NameObject = "Бассейн №2",      NameZone = "Дорожка 1", Square = "200", Size = "10x25", ZoneType = "Бассейн", TypeComp = "Региональный", Coating = "Плитка", ChangingRooms = "3", PossibleUse = "Плавание", SportsEquipment = "Есть", EPS = "Да", EPS_IAS = "Да", UrlImage = "/Photo_zone/zone3.jpg" }
        };

        public static List<SportsZoneRecord> GetSportsZones()
        {
            lock (_lock) return _sportsZones.ToList();
        }

        public static void ReplaceSportsZones(List<SportsZoneRecord> newData)
        {
            lock (_lock) _sportsZones = newData ?? new List<SportsZoneRecord>();
        }

        // =====================================================================
        // === ДОКУМЕНТЫ ОБЪЕКТА ===
        // =====================================================================

        public class ObjectDocumentRecord
        {
            public string NameObject { get; set; }
            public string OrderOKS { get; set; }
            public string AktPriemStroi { get; set; }
            public string ExtractOKS { get; set; }
            public string TerminationOKS { get; set; }
            public string OrderZU { get; set; }
            public string Contract { get; set; }
            public string ExtractZU { get; set; }
            public string TerminationZU { get; set; }
            public string TexPasport { get; set; }
            public string Eksplikation { get; set; }
            public string PoetapPlan { get; set; }
            public string TerritoryPlans { get; set; }
        }

        private static List<ObjectDocumentRecord> _objectDocuments = new List<ObjectDocumentRecord>
        {
            new ObjectDocumentRecord
            {
                NameObject = "Спорткомплекс №1",
                OrderOKS = "/DocumentsObjects/order.pdf",
                AktPriemStroi = "/DocumentsObjects/akt.pdf",
                ExtractOKS = "Получен",
                TerminationOKS = "Отсутствует",
                OrderZU = "/DocumentsObjects/order_zu.pdf",
                Contract = "/DocumentsObjects/contract.pdf",
                ExtractZU = "Получен",
                TerminationZU = "Отсутствует",
                TexPasport = "/DocumentsObjects/tex_pasport.pdf",
                Eksplikation = "Не получен",
                PoetapPlan = "/DocumentsObjects/poetap.pdf",
                TerritoryPlans = "Нет информации"
            },
            new ObjectDocumentRecord
            {
                NameObject = "Бассейн №2",
                OrderOKS = "/DocumentsObjects/order2.pdf",
                AktPriemStroi = "Получен",
                ExtractOKS = "Получен",
                TerminationOKS = "Отсутствует",
                OrderZU = "Не получен",
                Contract = "/DocumentsObjects/contract2.pdf",
                ExtractZU = "Получен",
                TerminationZU = "Отсутствует",
                TexPasport = "/DocumentsObjects/tex_pasport2.pdf",
                Eksplikation = "/DocumentsObjects/eksp2.pdf",
                PoetapPlan = "Нет информации",
                TerritoryPlans = "Нет информации"
            }
        };

        public static List<ObjectDocumentRecord> GetObjectDocuments()
        {
            lock (_lock) return _objectDocuments.ToList();
        }

        // =====================================================================
        // === ИЗОБРАЖЕНИЯ ОБЪЕКТОВ ===
        // =====================================================================

        public class ObjectImageRecord
        {
            public string NameObject { get; set; }
            public string UrlImage { get; set; }
        }

        private static List<ObjectImageRecord> _objectImages = new List<ObjectImageRecord>
{
    // === Спорткомплекс №1 (папка sportcomplex1) ===
    new ObjectImageRecord { NameObject = "Спорткомплекс №1", UrlImage = "/photo_obj/sportcomplex1/1484671217-1.jpg" },
    new ObjectImageRecord { NameObject = "Спорткомплекс №1", UrlImage = "/photo_obj/sportcomplex1/1506967659-1.jpg" },
    new ObjectImageRecord { NameObject = "Спорткомплекс №1", UrlImage = "/photo_obj/sportcomplex1/1509273441-1.jpg" },
    new ObjectImageRecord { NameObject = "Спорткомплекс №1", UrlImage = "/photo_obj/sportcomplex1/1594856909-1.jpg" },
    new ObjectImageRecord { NameObject = "Спорткомплекс №1", UrlImage = "/photo_obj/sportcomplex1/1594856915-1.jpg" },

    // === Бассейн №2 (папка bassein2) ===
    new ObjectImageRecord { NameObject = "Бассейн №2", UrlImage = "/photo_obj/bassein2/1283650147-1.jpg" },
    new ObjectImageRecord { NameObject = "Бассейн №2", UrlImage = "/photo_obj/bassein2/1286493838-1.jpg" },
    new ObjectImageRecord { NameObject = "Бассейн №2", UrlImage = "/photo_obj/bassein2/1399052089-1.jpg" },
    new ObjectImageRecord { NameObject = "Бассейн №2", UrlImage = "/photo_obj/bassein2/1579848324-1.jpg" },
    new ObjectImageRecord { NameObject = "Бассейн №2", UrlImage = "/photo_obj/bassein2/1582570343-1.jpg" },

    // === Ледовый дворец (папка ledoviy) ===
    new ObjectImageRecord { NameObject = "Ледовый дворец", UrlImage = "/photo_obj/ledoviy/1478451412-1.jpg" },
    new ObjectImageRecord { NameObject = "Ледовый дворец", UrlImage = "/photo_obj/ledoviy/1478456011-1.jpg" },
    new ObjectImageRecord { NameObject = "Ледовый дворец", UrlImage = "/photo_obj/ledoviy/1484127450-1.jpg" },
    new ObjectImageRecord { NameObject = "Ледовый дворец", UrlImage = "/photo_obj/ledoviy/1484671213-1.jpg" },
    new ObjectImageRecord { NameObject = "Ледовый дворец", UrlImage = "/photo_obj/ledoviy/1509175385-1.jpg" },

    // === Стадион №4 (папка stadion4) ===
    new ObjectImageRecord { NameObject = "Стадион №4", UrlImage = "/photo_obj/stadion4/1527914293-1.jpg" },
    new ObjectImageRecord { NameObject = "Стадион №4", UrlImage = "/photo_obj/stadion4/1579543082-1.jpg" },
    new ObjectImageRecord { NameObject = "Стадион №4", UrlImage = "/photo_obj/stadion4/1579824196-1.jpg" },
    new ObjectImageRecord { NameObject = "Стадион №4", UrlImage = "/photo_obj/stadion4/1583381503-1.jpg" },
    new ObjectImageRecord { NameObject = "Стадион №4", UrlImage = "/photo_obj/stadion4/1586611076-1.jpg" },

    // === ФОК №5 (папка fok5) ===
    new ObjectImageRecord { NameObject = "ФОК №5", UrlImage = "/photo_obj/fok5/1418266272-1.jpg" },
    new ObjectImageRecord { NameObject = "ФОК №5", UrlImage = "/photo_obj/fok5/1461766660-1.jpg" },
    new ObjectImageRecord { NameObject = "ФОК №5", UrlImage = "/photo_obj/fok5/1461767004-1.jpg" },
    new ObjectImageRecord { NameObject = "ФОК №5", UrlImage = "/photo_obj/fok5/1585389597-1.jpg" },
    new ObjectImageRecord { NameObject = "ФОК №5", UrlImage = "/photo_obj/fok5/1587559171-1.jpg" },

    // === Спортплощадка №6 (папка sportploshad6) — после переименования ===
    new ObjectImageRecord { NameObject = "Спортплощадка №6", UrlImage = "/photo_obj/sportploshad6/1509467286-1.jpg" },
    new ObjectImageRecord { NameObject = "Спортплощадка №6", UrlImage = "/photo_obj/sportploshad6/1527660670-1.jpg" },
    new ObjectImageRecord { NameObject = "Спортплощадка №6", UrlImage = "/photo_obj/sportploshad6/3.jpg" },
    new ObjectImageRecord { NameObject = "Спортплощадка №6", UrlImage = "/photo_obj/sportploshad6/4.jpg" },
    new ObjectImageRecord { NameObject = "Спортплощадка №6", UrlImage = "/photo_obj/sportploshad6/5.jpg" }
};

        public static List<ObjectImageRecord> GetObjectImages(string nameObject)
        {
            lock (_lock)
            {
                return _objectImages
                    .Where(x => x.NameObject == nameObject)
                    .ToList();
            }
        }

        // =====================================================================
        // === ПОЛНЫЙ СПИСОК СВОЙСТВ ОБЪЕКТА (для Otchet.aspx) ===
        // =====================================================================

        /// <summary>
        /// Возвращает все свойства объекта как список (Ключ, Значение).
        /// Используется для страницы отчётов (сравнение объектов).
        /// </summary>
        public static List<KeyValuePair<string, string>> GetFullObjectProperties(string nameObject)
        {
            var result = new List<KeyValuePair<string, string>>();
            var obj = GetObjects().FirstOrDefault(o => o.NameObject == nameObject);

            if (obj == null)
            {
                result.Add(new KeyValuePair<string, string>("Имя объекта", "Объект не найден"));
                return result;
            }

            // Основная информация
            result.Add(new KeyValuePair<string, string>("Имя объекта", obj.NameObject ?? ""));
            result.Add(new KeyValuePair<string, string>("Адрес", obj.Adress ?? ""));
            result.Add(new KeyValuePair<string, string>("Округ", obj.District ?? ""));
            result.Add(new KeyValuePair<string, string>("Район", obj.Raion ?? ""));
            result.Add(new KeyValuePair<string, string>("Индекс", obj.Indexx ?? ""));
            result.Add(new KeyValuePair<string, string>("Категория", obj.Category ?? ""));
            result.Add(new KeyValuePair<string, string>("Руководитель", obj.Supervisor ?? ""));
            result.Add(new KeyValuePair<string, string>("Номер объекта", obj.NumberObject ?? ""));
            result.Add(new KeyValuePair<string, string>("Статус", obj.Statuss ?? ""));
            result.Add(new KeyValuePair<string, string>("Тип", obj.Tip ?? ""));

            // Здания
            result.Add(new KeyValuePair<string, string>("Кадастровый номер здания", obj.CadastralNumber ?? ""));
            result.Add(new KeyValuePair<string, string>("Год постройки", obj.ConstructionYear ?? ""));
            result.Add(new KeyValuePair<string, string>("Этажность", obj.Levels ?? ""));
            result.Add(new KeyValuePair<string, string>("Дата ввода в эксплуатацию", obj.CommissioningDate ?? ""));
            result.Add(new KeyValuePair<string, string>("Площадь зданий и сооружений", obj.SqureBuildings ?? ""));

            // Земельный участок
            result.Add(new KeyValuePair<string, string>("Зелёные насаждения", obj.GreenSpaces ?? ""));
            result.Add(new KeyValuePair<string, string>("Площадь территорий", obj.SqureTerritory ?? ""));
            result.Add(new KeyValuePair<string, string>("Используемая территория", obj.TerritoriesUsed ?? ""));

            // Инженерные системы
            result.Add(new KeyValuePair<string, string>("Система электроснабжения", obj.Electrosnab ?? ""));
            result.Add(new KeyValuePair<string, string>("Система водоснабжения", obj.Vodsnab ?? ""));
            result.Add(new KeyValuePair<string, string>("ОЗДС", obj.OZDS ?? ""));

            // Документы
            var doc = GetObjectDocuments().FirstOrDefault(d => d.NameObject == nameObject);
            if (doc != null)
            {
                result.Add(new KeyValuePair<string, string>("Распоряжение / РДГИ (ОКС)", doc.OrderOKS ?? ""));
                result.Add(new KeyValuePair<string, string>("Акт приема-передачи", doc.AktPriemStroi ?? ""));
                result.Add(new KeyValuePair<string, string>("Выписка ЕГРН (ОКС)", doc.ExtractOKS ?? ""));
                result.Add(new KeyValuePair<string, string>("Прекращение права (ОКС)", doc.TerminationOKS ?? ""));
                result.Add(new KeyValuePair<string, string>("Распоряжение / РДГИ (ЗУ)", doc.OrderZU ?? ""));
                result.Add(new KeyValuePair<string, string>("Договор БП/аренды", doc.Contract ?? ""));
                result.Add(new KeyValuePair<string, string>("Выписка ЕГРН (ЗУ)", doc.ExtractZU ?? ""));
                result.Add(new KeyValuePair<string, string>("Прекращение права (ЗУ)", doc.TerminationZU ?? ""));
                result.Add(new KeyValuePair<string, string>("Технический паспорт", doc.TexPasport ?? ""));
                result.Add(new KeyValuePair<string, string>("Экспликации", doc.Eksplikation ?? ""));
                result.Add(new KeyValuePair<string, string>("Поэтажные планы", doc.PoetapPlan ?? ""));
                result.Add(new KeyValuePair<string, string>("Планы территории", doc.TerritoryPlans ?? ""));
            }

            // Спортивные зоны
            var zones = GetSportsZones().Where(z => z.NameObject == nameObject).ToList();
            var zonesStr = zones.Count > 0
                ? string.Join(", ", zones.Select(z => z.NameZone))
                : "Нет спортивных зон";
            result.Add(new KeyValuePair<string, string>("Спортивные зоны", zonesStr));

            return result;
        }
    }
}