using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxSerch.Attributes.Add("onkeypress", "return clickButton(event,'" + Poisk.ClientID + "')");

            // Проверка доступа: только admin / admin2
            if (!IsPostBack)
            {
                var login = Session["login"] as string;
                var user = LocalData.GetUsers().FirstOrDefault(u =>
                    string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase));

                if (user == null || !user.Useres)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                BindGrid();
            }
        }

        // =====================================================================
        // ПРИВЯЗКА ДАННЫХ
        // =====================================================================

        private void BindGrid()
        {
            var users = LocalData.GetUsers();

            // Поиск
            var search = TextBoxSerch.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u =>
                    Contains(u.Familia, search) ||
                    Contains(u.Name, search) ||
                    Contains(u.Otchestvo, search) ||
                    Contains(u.Otdel, search) ||
                    Contains(u.Doljnost, search) ||
                    Contains(u.Number, search)).ToList();
            }

            GridView_users.DataSource = users;
            GridView_users.DataBind();
        }

        private static bool Contains(string source, string search) =>
            !string.IsNullOrEmpty(source) &&
            source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        // =====================================================================
        // СОЗДАНИЕ ЧЕКБОКСОВ В ЯЧЕЙКАХ (RowCreated)
        // =====================================================================

        protected void GridView_users_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            // В ячейках 7–26 создаём чекбоксы. Столбцы 0–6 — BoundField (текст).
            // Мы НЕ ставим значения здесь — это делает RowDataBound (там есть DataItem).
            CreateCheckboxInCell(e.Row, 7, "Redact_object", CheckBox1_CheckedChanged);
            CreateCheckboxInCell(e.Row, 8, "Osnov_inf", CheckBox2_CheckedChanged);
            CreateCheckboxInCell(e.Row, 9, "Ing_sys", CheckBox2_CheckedChanged);
            CreateCheckboxInCell(e.Row, 10, "Teh_doc", CheckBox2_CheckedChanged);
            CreateCheckboxInCell(e.Row, 11, "Kategor", CheckBox2_CheckedChanged);
            CreateCheckboxInCell(e.Row, 12, "Proch", CheckBox2_CheckedChanged);
            CreateCheckboxInCell(e.Row, 13, "Sport_zone", CheckBox2_CheckedChanged);
            CreateCheckboxInCell(e.Row, 14, "Redact_zone", null);
            CreateCheckboxInCell(e.Row, 15, "Useres", null);
            CreateCheckboxInCell(e.Row, 16, "Redact_bloknot1", null);
            CreateCheckboxInCell(e.Row, 17, "Redact_bloknot2", null);
            CreateCheckboxInCell(e.Row, 18, "Reester_redact", null);
            CreateCheckboxInCell(e.Row, 19, "Reester_prosroch", Test);
            CreateCheckboxInCell(e.Row, 20, "Redakt_OG", null);
            CreateCheckboxInCell(e.Row, 21, "Redakt_dr_object", null);
            CreateCheckboxInCell(e.Row, 22, "Reester", null);
            CreateCheckboxInCell(e.Row, 23, "SystemStatus", null);
            CreateCheckboxInCell(e.Row, 24, "Redakt_Status", null);
            CreateCheckboxInCell(e.Row, 25, "Priem", null);
            CreateCheckboxInCell(e.Row, 26, "Admin_Priem", null);
        }

        private static void CreateCheckboxInCell(GridViewRow row, int cellIndex,
            string id, EventHandler onCheckedChanged)
        {
            if (row.Cells.Count <= cellIndex) return;

            var cb = new CheckBox
            {
                ID = id,
                CssClass = "reester",
                AutoPostBack = onCheckedChanged != null
            };
            if (onCheckedChanged != null)
                cb.CheckedChanged += onCheckedChanged;

            row.Cells[cellIndex].Controls.Add(cb);
        }

        // =====================================================================
        // УСТАНОВКА ЗНАЧЕНИЙ ЧЕКБОКСОВ (RowDataBound)
        // =====================================================================

        protected void GridView_users_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var user = e.Row.DataItem as LocalData.UserRecord;
            if (user == null) return;

            SetCheckbox(e.Row, 7, "Redact_object", user.RedactObject);
            SetCheckbox(e.Row, 8, "Osnov_inf", user.OsnovInf);
            SetCheckbox(e.Row, 9, "Ing_sys", user.IngSys);
            SetCheckbox(e.Row, 10, "Teh_doc", user.TehDoc);
            SetCheckbox(e.Row, 11, "Kategor", user.Kategor);
            SetCheckbox(e.Row, 12, "Proch", user.Proch);
            SetCheckbox(e.Row, 13, "Sport_zone", user.SportZone);
            SetCheckbox(e.Row, 14, "Redact_zone", user.RedactZone);
            SetCheckbox(e.Row, 15, "Useres", user.Useres);
            SetCheckbox(e.Row, 16, "Redact_bloknot1", user.RedactBloknot1);
            SetCheckbox(e.Row, 17, "Redact_bloknot2", user.RedactBloknot2);
            SetCheckbox(e.Row, 18, "Reester_redact", user.ReesterRedact);
            SetCheckbox(e.Row, 19, "Reester_prosroch", user.ReesterProsroch);
            SetCheckbox(e.Row, 20, "Redakt_OG", user.RedaktOG);
            SetCheckbox(e.Row, 21, "Redakt_dr_object", user.RedaktDrObject);
            SetCheckbox(e.Row, 22, "Reester", user.Reester);
            SetCheckbox(e.Row, 23, "SystemStatus", user.SystemStatus);
            SetCheckbox(e.Row, 24, "Redakt_Status", user.RedaktStatus);
            SetCheckbox(e.Row, 25, "Priem", user.Priem);
            SetCheckbox(e.Row, 26, "Admin_Priem", user.AdminPriem);
        }

        private static void SetCheckbox(GridViewRow row, int cellIndex,
            string id, bool value)
        {
            if (row.Cells.Count <= cellIndex) return;
            var cb = row.Cells[cellIndex].FindControl(id) as CheckBox;
            if (cb != null) cb.Checked = value;
        }

        // =====================================================================
        // СОХРАНЕНИЕ ВСЕХ ПОЛЬЗОВАТЕЛЕЙ
        // =====================================================================

        protected void Button_save_Click(object sender, EventArgs e)
        {
            var users = LocalData.GetUsers();

            for (int i = 0; i < GridView_users.Rows.Count && i < users.Count; i++)
            {
                var row = GridView_users.Rows[i];
                var user = users[i];

                user.RedactObject = GetCheckboxValue(row, 7, "Redact_object");
                user.OsnovInf = GetCheckboxValue(row, 8, "Osnov_inf");
                user.IngSys = GetCheckboxValue(row, 9, "Ing_sys");
                user.TehDoc = GetCheckboxValue(row, 10, "Teh_doc");
                user.Kategor = GetCheckboxValue(row, 11, "Kategor");
                user.Proch = GetCheckboxValue(row, 12, "Proch");
                user.SportZone = GetCheckboxValue(row, 13, "Sport_zone");
                user.RedactZone = GetCheckboxValue(row, 14, "Redact_zone");
                user.Useres = GetCheckboxValue(row, 15, "Useres");
                user.RedactBloknot1 = GetCheckboxValue(row, 16, "Redact_bloknot1");
                user.RedactBloknot2 = GetCheckboxValue(row, 17, "Redact_bloknot2");
                user.ReesterRedact = GetCheckboxValue(row, 18, "Reester_redact");
                user.ReesterProsroch = GetCheckboxValue(row, 19, "Reester_prosroch");
                user.RedaktOG = GetCheckboxValue(row, 20, "Redakt_OG");
                user.RedaktDrObject = GetCheckboxValue(row, 21, "Redakt_dr_object");
                user.Reester = GetCheckboxValue(row, 22, "Reester");
                user.SystemStatus = GetCheckboxValue(row, 23, "SystemStatus");
                user.RedaktStatus = GetCheckboxValue(row, 24, "Redakt_Status");
                user.Priem = GetCheckboxValue(row, 25, "Priem");
                user.AdminPriem = GetCheckboxValue(row, 26, "Admin_Priem");
            }

            LocalData.ReplaceUsers(users);
            ShowAlert("Данные сохранены!");
        }

        private static bool GetCheckboxValue(GridViewRow row, int cellIndex, string id)
        {
            if (row.Cells.Count <= cellIndex) return false;
            var cb = row.Cells[cellIndex].FindControl(id) as CheckBox;
            return cb != null && cb.Checked;
        }

        // =====================================================================
        // СОБЫТИЯ CHECKBOX'ОВ
        // =====================================================================

        /// <summary>
        /// При включении «Ред. объектов» — включаем все подчинённые права.
        /// </summary>
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb == null) return;

            var row = cb.NamingContainer as GridViewRow;
            if (row == null) return;

            bool value = cb.Checked;
            SetCheckbox(row, 8, "Osnov_inf", value);
            SetCheckbox(row, 9, "Ing_sys", value);
            SetCheckbox(row, 10, "Teh_doc", value);
            SetCheckbox(row, 11, "Kategor", value);
            SetCheckbox(row, 12, "Proch", value);
            SetCheckbox(row, 13, "Sport_zone", value);
        }

        /// <summary>
        /// При включении любого подчинённого — включаем родительский «Ред. объектов».
        /// </summary>
        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb == null) return;

            var row = cb.NamingContainer as GridViewRow;
            if (row == null) return;

            if (cb.Checked)
            {
                SetCheckbox(row, 7, "Redact_object", true);
            }
        }

        protected void Test(object sender, EventArgs e)
        {
            // Заглушка
        }

        // =====================================================================
        // ПОИСК И ОБНОВЛЕНИЕ
        // =====================================================================

        protected void Poisk_ServerClick(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Refresh_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }

        // =====================================================================
        // CHECK() — управление видимостью разделов
        // =====================================================================



        private LocalData.UserRecord GetSelectedUser()
        {
            // Если выбран конкретный пользователь — можно добавить DDL. 
            // Пока возвращаем первого пользователя из LocalData (для демо).
            return LocalData.GetUsers().FirstOrDefault();
        }

        // =====================================================================
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
        // =====================================================================

        protected TextBox[] tbArray()
        {
            return new TextBox[] { };
        }

        protected CheckBox[] chkArray()
        {
            return new CheckBox[] { };
        }

        private void ShowAlert(string message)
        {
            string safe = message.Replace("'", "\\'").Replace("\r", "").Replace("\n", "");
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('{safe}');", true);
        }
    }
}