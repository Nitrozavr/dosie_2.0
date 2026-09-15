using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Otchet : System.Web.UI.Page
    {
        public string name_zone;

        protected void Page_Load(object sender, EventArgs e)
        {
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Otch");
            if (panel != null) panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            Skryt();
            TextSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + Btn_poisk.ClientID + "')");

            if (!IsPostBack)
            {
                // Восстанавливаем фильтры из Session
                Session["select_filters1"] = Session["select_filters1"] ?? "%";
                Session["select_filters2"] = Session["select_filters2"] ?? "%";
                Session["select_filters3"] = Session["select_filters3"] ?? "%";
                Session["select_filters4"] = Session["select_filters4"] ?? "%";
            }

            // Загружаем выпадающие списки
            LoadFilters();

            // Загружаем CheckBoxList'ы
            LoadCheckBoxLists();

            // Привязываем таблицы
            BindAllTables();

            // Скрываем/показываем элементы
            Skryt();

            // Обработка объекта из URL
            if (!IsPostBack && Request.QueryString["map"] != null)
            {
                TextSearch.Text = Request.QueryString["map"];
                ApplySearch();
            }

            // Синхронизация чекбоксов
            peredacha_check_list();
            peredacha_check();

            // Если есть выбранные объекты — показываем панели
            foreach (ListItem chkn in Check_Object.Items)
            {
                if (chkn.Selected)
                {
                    pokaz();
                    chk();
                }
            }
        }

        // =====================================================================
        // ЗАГРУЗКА ФИЛЬТРОВ
        // =====================================================================

        private void LoadFilters()
        {
            var objects = LocalData.GetObjects()
                .Where(o => o.Postuplenie != "ПЕРЕДАЧА")
                .ToList();

            // DropDownList1 — округа
            if (DropDownList1.Items.Count <= 1)
            {
                DropDownList1.DataSource = objects
                    .Select(o => o.District).Where(d => !string.IsNullOrEmpty(d))
                    .Distinct().OrderBy(d => d)
                    .Select(d => new { District = d }).ToList();
                DropDownList1.DataTextField = "District";
                DropDownList1.DataValueField = "District";
                DropDownList1.DataBind();
                SetSelectedIfExists(DropDownList1, Session["select_filters1"] as string);
            }

            // DropDownList2 — категории
            if (DropDownList2.Items.Count <= 1)
            {
                DropDownList2.DataSource = objects
                    .Select(o => o.Category).Where(c => !string.IsNullOrEmpty(c))
                    .Distinct().OrderBy(c => c)
                    .Select(c => new { Category = c }).ToList();
                DropDownList2.DataTextField = "Category";
                DropDownList2.DataValueField = "Category";
                DropDownList2.DataBind();
                SetSelectedIfExists(DropDownList2, Session["select_filters2"] as string);
            }

            // DropDownList3 — руководители
            if (DropDownList3.Items.Count <= 1)
            {
                DropDownList3.DataSource = objects
                    .Select(o => o.Supervisor).Where(s => !string.IsNullOrEmpty(s))
                    .Distinct().OrderBy(s => s)
                    .Select(s => new { Supervisor = s }).ToList();
                DropDownList3.DataTextField = "Supervisor";
                DropDownList3.DataValueField = "Supervisor";
                DropDownList3.DataBind();
                SetSelectedIfExists(DropDownList3, Session["select_filters3"] as string);
            }

            // DropDownList4 — типы
            if (DropDownList4.Items.Count <= 1)
            {
                DropDownList4.DataSource = objects
                    .Select(o => o.Tip).Where(t => !string.IsNullOrEmpty(t))
                    .Distinct().OrderBy(t => t)
                    .Select(t => new { Tip = t }).ToList();
                DropDownList4.DataTextField = "Tip";
                DropDownList4.DataValueField = "Tip";
                DropDownList4.DataBind();
                SetSelectedIfExists(DropDownList4, Session["select_filters4"] as string);
            }
        }

        private static void SetSelectedIfExists(DropDownList ddl, string value)
        {
            if (!string.IsNullOrEmpty(value) && ddl.Items.FindByValue(value) != null)
                ddl.SelectedValue = value;
        }

        // =====================================================================
        // ЗАГРУЗКА CheckBoxList'ов
        // =====================================================================

        private void LoadCheckBoxLists()
        {
            var objects = LocalData.GetObjects()
                .Where(o => o.Postuplenie != "ПЕРЕДАЧА")
                .OrderBy(o => o.NameObject)
                .ToList();

            // CheckBoxList1 — имена объектов
            CheckBoxList1.DataSource = objects.Select(o => new { o.NameObject }).ToList();
            CheckBoxList1.DataTextField = "NameObject";
            CheckBoxList1.DataValueField = "NameObject";
            CheckBoxList1.DataBind();

            // CheckBoxList2 — округа
            CheckBoxList2.DataSource = objects.Select(o => new { o.District, o.NameObject }).ToList();
            CheckBoxList2.DataTextField = "District";
            CheckBoxList2.DataValueField = "NameObject";
            CheckBoxList2.DataBind();

            // CheckBoxList3 — категории
            CheckBoxList3.DataSource = objects.Select(o => new { o.Category, o.NameObject }).ToList();
            CheckBoxList3.DataTextField = "Category";
            CheckBoxList3.DataValueField = "NameObject";
            CheckBoxList3.DataBind();

            // CheckBoxList4 — пустой, нужен для позиционирования
            CheckBoxList4.DataSource = objects.Select(o => new { o.Category, o.NameObject }).ToList();
            CheckBoxList4.DataTextField = "Category";
            CheckBoxList4.DataValueField = "NameObject";
            CheckBoxList4.DataBind();

            // Check_Object — «источник истины»
            Check_Object.DataSource = objects.Select(o => new { o.NameObject }).ToList();
            Check_Object.DataTextField = "NameObject";
            Check_Object.DataValueField = "NameObject";
            Check_Object.DataBind();
        }
        // =====================================================================
        // ПРИВЯЗКА ВСЕХ ТАБЛИЦ
        // =====================================================================

        private void BindAllTables()
        {
            // Получаем выбранные объекты
            var selectedObjects = Check_Object.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
                .ToList();

            // Если ничего не выбрано — берём первый объект
            if (selectedObjects.Count == 0)
            {
                var first = LocalData.GetObjects()
                    .Where(o => o.Postuplenie != "ПЕРЕДАЧА")
                    .FirstOrDefault();
                if (first != null) selectedObjects.Add(first.NameObject);
            }

            // Формируем данные: строки — свойства, столбцы — объекты
            var properties = new List<KeyValuePair<string, List<string>>>();

            // Собираем «базовые» ключи с первого объекта
            if (selectedObjects.Count > 0)
            {
                var firstProps = LocalData.GetFullObjectProperties(selectedObjects[0]);
                foreach (var p in firstProps)
                {
                    properties.Add(new KeyValuePair<string, List<string>>(p.Key, new List<string>()));
                }
            }

            // Заполняем значения для каждого объекта
            foreach (var objName in selectedObjects)
            {
                var objProps = LocalData.GetFullObjectProperties(objName);
                for (int i = 0; i < properties.Count; i++)
                {
                    var match = objProps.FirstOrDefault(p => p.Key == properties[i].Key);
                    properties[i].Value.Add(match.Value ?? "—");
                }
            }

            // Строим «транспонированные» DataTable для каждой панели
            GridView1.DataSource = ToDataTableMulti(properties);
            GridView1.DataBind();

            // Панель 1: Основная информация
            Table_inf.DataSource = ToDataTableMulti(properties.Take(10).ToList());
            Table_inf.DataBind();

            // Панель 2: Сведения о зданиях
            Table_kat.DataSource = ToDataTableMulti(properties.Skip(10).Take(5).ToList());
            Table_kat.DataBind();

            // Панель 3: Земельный участок
            Table_sved.DataSource = ToDataTableMulti(properties.Skip(15).Take(3).ToList());
            Table_sved.DataBind();

            // Панель 4: Инженерные системы
            Table_ing.DataSource = ToDataTableMulti(properties.Skip(18).Take(3).ToList());
            Table_ing.DataBind();

            // Панель 5: Документы
            GridView_OKS.DataSource = ToDataTableMulti(properties.Skip(21).Take(4).ToList());
            GridView_OKS.DataBind();
            GridView_ZU.DataSource = ToDataTableMulti(properties.Skip(25).Take(4).ToList());
            GridView_ZU.DataBind();
            Table_tex.DataSource = ToDataTableMulti(properties.Skip(29).Take(4).ToList());
            Table_tex.DataBind();

            // Панель 6: Иная документация (пустая)
            Table_in.DataSource = new List<object>();
            Table_in.DataBind();

            // Панель 7: Прочие данные
            Table_proch.DataSource = ToDataTableMulti(properties.Skip(33).Take(6).ToList());
            Table_proch.DataBind();

            // Панель 8: Спортивные зоны
            Table_zone.DataSource = ToDataTableMulti(properties.Skip(39).Take(1).ToList());
            Table_zone.DataBind();
        }

        // =====================================================================
        // ТРАНСПОНИРОВАНИЕ: List<KeyValuePair> → DataTable
        // =====================================================================

        /// <summary>
        /// Превращает список «Свойство → Список значений» в DataTable.
        /// Первая колонка — название свойства, остальные — значения по объектам.
        /// </summary>
        private static DataTable ToDataTableMulti(List<KeyValuePair<string, List<string>>> data)
        {
            var dt = new DataTable();

            // Определяем максимальное количество колонок
            int maxCols = 0;
            foreach (var item in data)
                if (item.Value.Count > maxCols) maxCols = item.Value.Count;

            // Первая колонка — «Свойство»
            dt.Columns.Add("Свойство");

            // Остальные колонки — «Объект 1», «Объект 2», ...
            for (int i = 0; i < maxCols; i++)
                dt.Columns.Add("Объект" + (i + 1));

            // Заполняем строки
            foreach (var item in data)
            {
                var row = dt.NewRow();
                row["Свойство"] = item.Key;

                for (int i = 0; i < maxCols; i++)
                {
                    row["Объект" + (i + 1)] = i < item.Value.Count ? item.Value[i] : "—";
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        // =====================================================================
        // СТАРЫЙ МЕТОД (оставлен для совместимости)
        // =====================================================================

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }
        // =====================================================================
        // РАСКРЫТИЕ / СВОРАЧИВАНИЕ ПАНЕЛЕЙ
        // =====================================================================

        protected void btn_osnov_inf_Click(object sender, ImageClickEventArgs e) => TogglePanel(0, btn_osnov_inf, Panel_osnova, Osnov_inf, btn_dow_inf, Panel_Table_inf, Table_inf);
        protected void btn_kat_Click(object sender, ImageClickEventArgs e) => TogglePanel(1, btn_kat, Panel_kat, Kategor, btn_dow_cat, Panel_Table_kat, Table_kat);
        protected void btn_sved_Click(object sender, ImageClickEventArgs e) => TogglePanel(2, btn_sved, Panel_sved, Svedenia, btn_dow_sved, Panel_Table_sved, Table_sved);
        protected void btn_inj_sys_Click(object sender, ImageClickEventArgs e) => TogglePanel(3, btn_inj_sys, Panel_inj, Ing_sys, btn_dow_ing, Panel_Table_ing, Table_ing);
        protected void btn_proch_inf_Click(object sender, ImageClickEventArgs e) => TogglePanel(6, btn_proch_inf, Panel_proch, Proch_doc, btn_dow_proch, Panel_Table_proch, Table_proch);

        protected void btn_tex_doc_Click(object sender, ImageClickEventArgs e)
        {
            Chk_view.Items[4].Selected = !Chk_view.Items[4].Selected;

            if (Chk_view.Items[4].Selected)
            {
                btn_tex_doc.ImageUrl = "/Logo/Стрелка.svg";
                Panel_tex.CssClass = "panel_click";
                Tex_doc.CssClass = "inf_otch_click";
                btn_dow_tex.Visible = true;
                Label_OKS.Visible = true;
                Label_ZU.Visible = true;
                Label_Tex.Visible = true;
                Panel_doc.Visible = true;
                pokaz_table(Table_tex);
                pokaz_table(GridView_OKS);
                pokaz_table(GridView_ZU);
                pokaz_table(GridView_documents);
            }
            else
            {
                btn_tex_doc.ImageUrl = "/Logo/Стрелка вправо.svg";
                Panel_tex.CssClass = "panel_state";
                Tex_doc.CssClass = "inf_otch";
                btn_dow_tex.Visible = false;
                Label_OKS.Visible = false;
                Label_ZU.Visible = false;
                Label_Tex.Visible = false;
                Panel_doc.Visible = false;
                skrit_table(Table_tex);
                skrit_table(GridView_OKS);
                skrit_table(GridView_ZU);
                skrit_table(GridView_documents);
            }
            UpdateVidim();
        }

        protected void btn_in_doc_Click(object sender, ImageClickEventArgs e) => TogglePanel(5, btn_in_doc, Panel_in, In_doc, btn_dow_in, Panel_in_doc, Table_in);
        protected void Btn_sports_zone_Click(object sender, ImageClickEventArgs e) => TogglePanel(7, Btn_sports_zone, Panel_sport_zone, Sports_zone, btn_dow_zone, Panel_Table_zone, Table_zone);

        /// <summary>
        /// Универсальный переключатель панели.
        /// </summary>
        private void TogglePanel(int chkIndex, ImageButton arrow, Panel panel, Label title, ImageButton download, Panel tablePanel, GridView table)
        {
            Chk_view.Items[chkIndex].Selected = !Chk_view.Items[chkIndex].Selected;

            if (Chk_view.Items[chkIndex].Selected)
            {
                arrow.ImageUrl = "/Logo/Стрелка.svg";
                panel.CssClass = "panel_click";
                title.CssClass = "inf_otch_click";
                download.Visible = true;
                tablePanel.Visible = true;
                pokaz_table(table);
            }
            else
            {
                arrow.ImageUrl = "/Logo/Стрелка вправо.svg";
                panel.CssClass = "panel_state";
                title.CssClass = "inf_otch";
                download.Visible = false;
                tablePanel.Visible = false;
                skrit_table(table);
            }
            UpdateVidim();
        }

        private void UpdateVidim()
        {
            Vidim.Visible = btn_dow_inf.Visible || btn_dow_cat.Visible || btn_dow_sved.Visible
                || btn_dow_ing.Visible || btn_dow_tex.Visible || btn_dow_in.Visible
                || btn_dow_proch.Visible || btn_dow_zone.Visible;
        }

        // =====================================================================
        // РАЗВЕРНУТЬ ВСЁ / СВЕРНУТЬ ВСЁ
        // =====================================================================

        protected void Razvernyt_Click(object sender, EventArgs e)
        {
            foreach (ListItem chk in Chk_view.Items) chk.Selected = true;

            SetPanel(btn_osnov_inf, Panel_osnova, Osnov_inf, btn_dow_inf, Panel_Table_inf, true);
            SetPanel(btn_kat, Panel_kat, Kategor, btn_dow_cat, Panel_Table_kat, true);
            SetPanel(btn_sved, Panel_sved, Svedenia, btn_dow_sved, Panel_Table_sved, true);
            SetPanel(btn_inj_sys, Panel_inj, Ing_sys, btn_dow_ing, Panel_Table_ing, true);
            SetPanel(btn_tex_doc, Panel_tex, Tex_doc, btn_dow_tex, Panel_doc, true);
            SetPanel(btn_in_doc, Panel_in, In_doc, btn_dow_in, Panel_in_doc, true);
            SetPanel(btn_proch_inf, Panel_proch, Proch_doc, btn_dow_proch, Panel_Table_proch, true);
            SetPanel(Btn_sports_zone, Panel_sport_zone, Sports_zone, btn_dow_zone, Panel_Table_zone, true);

            chk();
        }

        protected void Svernyt_Click(object sender, EventArgs e)
        {
            foreach (ListItem chk in Chk_view.Items) chk.Selected = false;

            SetPanel(btn_osnov_inf, Panel_osnova, Osnov_inf, btn_dow_inf, Panel_Table_inf, false);
            SetPanel(btn_kat, Panel_kat, Kategor, btn_dow_cat, Panel_Table_kat, false);
            SetPanel(btn_sved, Panel_sved, Svedenia, btn_dow_sved, Panel_Table_sved, false);
            SetPanel(btn_inj_sys, Panel_inj, Ing_sys, btn_dow_ing, Panel_Table_ing, false);
            SetPanel(btn_tex_doc, Panel_tex, Tex_doc, btn_dow_tex, Panel_doc, false);
            SetPanel(btn_in_doc, Panel_in, In_doc, btn_dow_in, Panel_in_doc, false);
            SetPanel(btn_proch_inf, Panel_proch, Proch_doc, btn_dow_proch, Panel_Table_proch, false);
            SetPanel(Btn_sports_zone, Panel_sport_zone, Sports_zone, btn_dow_zone, Panel_Table_zone, false);

            Skrit_vse_table();
        }

        private void SetPanel(ImageButton arrow, Panel panel, Label title, ImageButton download, Panel tablePanel, bool expanded)
        {
            arrow.ImageUrl = expanded ? "/Logo/Стрелка.svg" : "/Logo/Стрелка вправо.svg";
            panel.CssClass = expanded ? "panel_click" : "panel_state";
            title.CssClass = expanded ? "inf_otch_click" : "inf_otch";
            download.Visible = expanded;
            if (tablePanel != null) tablePanel.Visible = expanded;
        }

        // =====================================================================
        // ВЫБРАТЬ ВСЕ / СНЯТЬ ВСЕ
        // =====================================================================

        protected void Button1_Click1(object sender, EventArgs e)
        {
            foreach (ListItem item in CheckBoxList1.Items) item.Selected = true;
            foreach (ListItem item in CheckBoxList2.Items) item.Selected = true;
            foreach (ListItem item in CheckBoxList3.Items) item.Selected = true;
            foreach (ListItem item in Check_Object.Items) item.Selected = true;

            BindAllTables();
            pokaz();
            chk();
            Panel_svg.Visible = false;
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            TextSearch.Text = "";
            foreach (ListItem item in CheckBoxList1.Items) item.Selected = false;
            foreach (ListItem item in CheckBoxList2.Items) item.Selected = false;
            foreach (ListItem item in CheckBoxList3.Items) item.Selected = false;
            foreach (ListItem item in Check_Object.Items) item.Selected = false;

            foreach (ListItem chk in Chk_view.Items) chk.Selected = false;
            Skryt();
            chk_bool();
            chk();
        }

        // =====================================================================
        // ФИЛЬТРАЦИЯ ПО DropDownList
        // =====================================================================

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) { Session["select_filters1"] = DropDownList1.SelectedValue; filter("Osnova"); }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e) { Session["select_filters2"] = DropDownList2.SelectedValue; filter("Osnova"); }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e) { Session["select_filters3"] = DropDownList3.SelectedValue; filter("Osnova"); }
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e) { Session["select_filters4"] = DropDownList4.SelectedValue; filter("Osnova"); }

        // =====================================================================
        // ПОИСК
        // =====================================================================

        protected void Btn_poisk_Click(object sender, EventArgs e)
        {
            ApplySearch();
        }

        protected void Btn_update_Click(object sender, EventArgs e)
        {
            Response.Redirect("Otchet.aspx");
        }

        private void ApplySearch()
        {
            string search = TextSearch.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                filter("Osnova");
                return;
            }

            // Фильтруем объекты по всем полям
            var filtered = LocalData.GetObjects()
                .Where(o => o.Postuplenie != "ПЕРЕДАЧА" &&
                    (Contains(o.NameObject, search) ||
                     Contains(o.District, search) ||
                     Contains(o.Category, search) ||
                     Contains(o.Adress, search) ||
                     Contains(o.Raion, search) ||
                     Contains(o.Supervisor, search) ||
                     Contains(o.Tip, search)))
                .OrderBy(o => o.NameObject)
                .ToList();

            // Заполняем CheckBoxList'ы
            CheckBoxList1.DataSource = filtered.Select(o => new { o.NameObject }).ToList();
            CheckBoxList1.DataTextField = "NameObject";
            CheckBoxList1.DataValueField = "NameObject";
            CheckBoxList1.DataBind();

            CheckBoxList2.DataSource = filtered.Select(o => new { o.District, o.NameObject }).ToList();
            CheckBoxList2.DataTextField = "District";
            CheckBoxList2.DataValueField = "NameObject";
            CheckBoxList2.DataBind();

            CheckBoxList3.DataSource = filtered.Select(o => new { o.Category, o.NameObject }).ToList();
            CheckBoxList3.DataTextField = "Category";
            CheckBoxList3.DataValueField = "NameObject";
            CheckBoxList3.DataBind();

            Check_Object.DataSource = filtered.Select(o => new { o.NameObject }).ToList();
            Check_Object.DataTextField = "NameObject";
            Check_Object.DataValueField = "NameObject";
            Check_Object.DataBind();

            // Автовыбор всех найденных
            foreach (ListItem item in CheckBoxList1.Items) item.Selected = true;
            foreach (ListItem item in Check_Object.Items) item.Selected = true;
            Smena_filtra2();
            chk_bool();
        }

        private static bool Contains(string source, string search) =>
            !string.IsNullOrEmpty(source) && source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        // =====================================================================
        // ФИЛЬТРАЦИЯ (для DropDownList)
        // =====================================================================

        public void filter(string dummy)
        {
            var district = DropDownList1.SelectedValue;
            var category = DropDownList2.SelectedValue;
            var supervisor = DropDownList3.SelectedValue;
            var tip = DropDownList4.SelectedValue;

            var filtered = LocalData.GetObjects()
                .Where(o => o.Postuplenie != "ПЕРЕДАЧА")
                .Where(o => district == "%" || o.District == district)
                .Where(o => category == "%" || o.Category == category)
                .Where(o => supervisor == "%" || o.Supervisor == supervisor)
                .Where(o => tip == "%" || o.Tip == tip)
                .OrderBy(o => o.NameObject)
                .ToList();

            CheckBoxList1.DataSource = filtered.Select(o => new { o.NameObject }).ToList();
            CheckBoxList1.DataTextField = "NameObject";
            CheckBoxList1.DataValueField = "NameObject";
            CheckBoxList1.DataBind();

            CheckBoxList2.DataSource = filtered.Select(o => new { o.District, o.NameObject }).ToList();
            CheckBoxList2.DataTextField = "District";
            CheckBoxList2.DataValueField = "NameObject";
            CheckBoxList2.DataBind();

            CheckBoxList3.DataSource = filtered.Select(o => new { o.Category, o.NameObject }).ToList();
            CheckBoxList3.DataTextField = "Category";
            CheckBoxList3.DataValueField = "NameObject";
            CheckBoxList3.DataBind();

            Check_Object.DataSource = filtered.Select(o => new { o.NameObject }).ToList();
            Check_Object.DataTextField = "NameObject";
            Check_Object.DataValueField = "NameObject";
            Check_Object.DataBind();

            foreach (ListItem item in CheckBoxList1.Items) item.Selected = true;
            foreach (ListItem item in Check_Object.Items) item.Selected = true;
            Smena_filtra2();
            chk_bool();
        }

        // =====================================================================
        // СИНХРОНИЗАЦИЯ ЧЕКБОКСОВ
        // =====================================================================

        public void peredacha_check()
        {
            foreach (ListItem item1 in CheckBoxList1.Items)
            {
                foreach (ListItem chkn in Check_Object.Items)
                {
                    if (chkn.Value == item1.Value && chkn.Selected != item1.Selected)
                        chkn.Selected = item1.Selected;
                }
            }
        }

        public void peredacha_check_list()
        {
            foreach (ListItem item1 in CheckBoxList1.Items)
            {
                foreach (ListItem item2 in CheckBoxList2.Items)
                {
                    foreach (ListItem item3 in CheckBoxList3.Items)
                    {
                        if (item1.Value == item2.Value && item1.Value == item3.Value && item2.Value == item3.Value)
                        {
                            if (item1.Selected != item2.Selected && item1.Selected != item3.Selected)
                            {
                                item2.Selected = item1.Selected;
                                item3.Selected = item1.Selected;
                            }
                            else if (item2.Selected != item1.Selected && item2.Selected != item3.Selected)
                            {
                                item1.Selected = item2.Selected;
                                item3.Selected = item2.Selected;
                            }
                            else if (item3.Selected != item1.Selected && item3.Selected != item2.Selected)
                            {
                                item1.Selected = item3.Selected;
                                item2.Selected = item3.Selected;
                            }
                        }
                    }
                }
            }
        }

        public void Smena_filtra()
        {
            foreach (ListItem item in CheckBoxList1.Items)
                foreach (ListItem chk in Check_Object.Items)
                    if (chk.Value == item.Value) chk.Selected = item.Selected;
            chk();
        }

        public void Smena_filtra2()
        {
            foreach (ListItem chk in Check_Object.Items)
            {
                foreach (ListItem item in CheckBoxList1.Items)
                    if (chk.Value == item.Value) item.Selected = chk.Selected;
                foreach (ListItem item in CheckBoxList2.Items)
                    if (chk.Value == item.Value) item.Selected = chk.Selected;
                foreach (ListItem item in CheckBoxList3.Items)
                    if (chk.Value == item.Value) item.Selected = chk.Selected;
            }
            chk();
        }

        // =====================================================================
        // ОБРАБОТЧИКИ ЧЕКБОКСОВ
        // =====================================================================

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e) { BindAllTables(); chk(); chk_bool(); }
        protected void CheckBoxList2_SelectedIndexChanged1(object sender, EventArgs e) { BindAllTables(); chk(); chk_bool(); }
        protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e) { BindAllTables(); chk(); chk_bool(); }

        protected void CheckBoxList1_DataBound(object sender, EventArgs e)
        {
            HighlightMatches(CheckBoxList1);
            if (CheckBoxList1.Items.Count != 0) { Button1.Visible = true; Button2.Visible = true; Label1.Visible = false; }
            else { Button1.Visible = false; Button2.Visible = false; Label1.Visible = true; chk_bool(); }
        }

        protected void CheckBoxList2_DataBound(object sender, EventArgs e) => HighlightMatches(CheckBoxList2);
        protected void CheckBoxList3_DataBound(object sender, EventArgs e) => HighlightMatches(CheckBoxList3);

        private void HighlightMatches(CheckBoxList cbl)
        {
            string search = TextSearch.Text.Trim();
            if (string.IsNullOrEmpty(search)) return;

            foreach (ListItem item in cbl.Items)
            {
                string clean = item.Text
                    .Replace("&quot;", "\u0022")
                    .Replace("<span style = 'background-color:#FF0000; border-radius: 5px; color:#FFFFFF;'>", "")
                    .Replace("</span>", "");

                item.Text = Regex.Replace(clean, Regex.Escape(search),
                    m => $"<span style='background-color:#FF0000; border-radius: 5px; color:#FFFFFF;'>{m.Value}</span>",
                    RegexOptions.IgnoreCase);

                if (!IsPostBack && item.Value == Request.QueryString["map"])
                    item.Selected = true;
            }
        }

        protected void Check_Object_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem chkn in Check_Object.Items)
                if (chkn.Value == Request.QueryString["map"]) chkn.Selected = true;
        }

        // =====================================================================
        // СКАЧИВАНИЕ
        // =====================================================================

        protected void btn_dow_inf_Click(object sender, ImageClickEventArgs e) => download(Table_inf, Osnov_inf.Text);
        protected void btn_dow_cat_Click(object sender, ImageClickEventArgs e) => download(Table_kat, Kategor.Text);
        protected void btn_dow_sved_Click(object sender, ImageClickEventArgs e) => download(Table_sved, Svedenia.Text);
        protected void btn_dow_ing_Click(object sender, ImageClickEventArgs e) => download(Table_ing, Ing_sys.Text);
        protected void btn_dow_in_Click(object sender, ImageClickEventArgs e) => download(Table_in, In_doc.Text);
        protected void btn_dow_proch_Click(object sender, ImageClickEventArgs e) => download(Table_proch, Proch_doc.Text);
        protected void btn_dow_zone_Click(object sender, ImageClickEventArgs e) => download(Table_zone, Sports_zone.Text);

        protected void btn_dow_tex_Click(object sender, ImageClickEventArgs e)
        {
            GridView_documents.Visible = true;
            download(GridView_documents, Tex_doc.Text);
            GridView_documents.Visible = false;
        }

        protected void Ves_otchet_Click(object sender, EventArgs e)
        {
            pokaz_table(GridView1);
            download(GridView1, "Отчёт_объекты");
            skrit_table(GridView1);
        }

        protected void Vidim_Click(object sender, EventArgs e)
        {
            foreach (ListItem check in Chk_view.Items)
            {
                if (!check.Selected) continue;

                switch (check.Text)
                {
                    case "Основная информация": pokaz_table_diapazon(GridView1, 0, 9); break;
                    case "Сведения о зданиях/сооружениях": pokaz_table_diapazon(GridView1, 10, 14); break;
                    case "Сведения о земельном участке": pokaz_table_diapazon(GridView1, 15, 17); break;
                    case "Инженерные системы": pokaz_table_diapazon(GridView1, 18, 20); break;
                    case "Документы": pokaz_table_diapazon(GridView1, 21, 32); break;
                    case "Иная документация": pokaz_table_diapazon(GridView1, 33, 38); break;
                    case "Прочие данные": pokaz_table_diapazon(GridView1, 33, 38); break;
                    case "Спортивные зоны": pokaz_table_diapazon(GridView1, 39, 39); break;
                }
            }
            download(GridView1, "Отчёт_объекты");
            skrit_table(GridView1);
        }

        public override void VerifyRenderingInServerForm(Control control) { /* для экспорта */ }

        public void download(GridView grid, string otch_name)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + otch_name + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1251");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grid.AllowPaging = false;

                if (grid.HeaderRow != null)
                {
                    grid.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in grid.HeaderRow.Cells)
                        cell.BackColor = grid.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in grid.Rows)
                {
                    if (row.Cells.Count > 1)
                    {
                        row.Cells[1].Width = 150;
                        row.Cells[0].Width = 200;
                    }
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.VerticalAlign = VerticalAlign.Middle;
                        cell.CssClass = "textmode";
                    }
                }
                grid.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        // =====================================================================
        // ПОКАЗ / СКРЫТИЕ
        // =====================================================================

        public void Skryt()
        {
            Danny.Visible = false;
            Vidim.Visible = false;
            Ves_otchet.Visible = false;
            Svernyt.Visible = false;
            Razvernyt.Visible = false;

            btn_osnov_inf.Visible = false;
            btn_kat.Visible = false;
            btn_inj_sys.Visible = false;
            btn_tex_doc.Visible = false;
            btn_in_doc.Visible = false;
            btn_proch_inf.Visible = false;
            Osnov_inf.Visible = false;
            Kategor.Visible = false;
            Ing_sys.Visible = false;
            Tex_doc.Visible = false;
            In_doc.Visible = false;
            Proch_doc.Visible = false;
            btn_dow_inf.Visible = false;
            btn_dow_cat.Visible = false;
            btn_dow_ing.Visible = false;
            btn_dow_tex.Visible = false;
            btn_dow_in.Visible = false;
            btn_dow_proch.Visible = false;
            btn_dow_zone.Visible = false;
        }

        public void pokaz()
        {
            Danny.Visible = true;
            Ves_otchet.Visible = true;
            Svernyt.Visible = true;
            Razvernyt.Visible = true;
            btn_osnov_inf.Visible = true;
            btn_kat.Visible = true;
            btn_inj_sys.Visible = true;
            btn_tex_doc.Visible = true;
            btn_in_doc.Visible = true;
            btn_proch_inf.Visible = true;
            Osnov_inf.Visible = true;
            Kategor.Visible = true;
            Ing_sys.Visible = true;
            Tex_doc.Visible = true;
            In_doc.Visible = true;
            Proch_doc.Visible = true;
        }

        public void Skrit_vse_table()
        {
            skrit_table(Table_inf);
            skrit_table(Table_kat);
            skrit_table(Table_sved);
            skrit_table(Table_ing);
            skrit_table(Table_tex);
            skrit_table(GridView_OKS);
            skrit_table(GridView_ZU);
            skrit_table(GridView_documents);
            skrit_table(Table_in);
            skrit_table(Table_proch);
            skrit_table(Table_zone);
            skrit_table(GridView1);
        }

        public void skrit_table(GridView grid)
        {
            if (grid == null) return;
            if (grid.HeaderRow != null)
                foreach (TableCell cell in grid.HeaderRow.Cells)
                    cell.Visible = false;

            foreach (GridViewRow row in grid.Rows)
            {
                row.Visible = false;
                foreach (TableCell cell in row.Cells) cell.Visible = false;
            }
        }

        public void pokaz_table(GridView grid)
        {
            if (grid == null) return;

            foreach (ListItem chk in Check_Object.Items)
            {
                if (!chk.Selected) continue;

                if (grid.HeaderRow != null)
                {
                    foreach (TableCell cell in grid.HeaderRow.Cells)
                    {
                        if (cell.Text.Replace("&quot;", "\u0022") == chk.Value.Trim())
                        {
                            cell.Visible = true;
                            grid.HeaderRow.Cells[0].Visible = true;
                            foreach (GridViewRow row in grid.Rows)
                            {
                                row.Visible = true;
                                row.Cells[0].Visible = true;
                                int idx = GetIndex(cell.Text.Replace("&quot;", "\u0022"), grid);
                                if (idx < row.Cells.Count) row.Cells[idx].Visible = true;
                            }
                        }
                    }
                }
            }
        }

        public void pokaz_table_diapazon(GridView grid, int min, int max)
        {
            if (grid == null || grid.HeaderRow == null) return;

            foreach (ListItem chk in Check_Object.Items)
            {
                if (!chk.Selected) continue;

                foreach (TableCell cell in grid.HeaderRow.Cells)
                {
                    if (cell.Text.Replace("&quot;", "\u0022") == chk.Value.Trim())
                    {
                        cell.Visible = true;
                        grid.HeaderRow.Cells[0].Visible = true;
                        foreach (GridViewRow row in grid.Rows)
                        {
                            if (row.RowIndex >= min && row.RowIndex <= max)
                            {
                                row.Visible = true;
                                row.Cells[0].Visible = true;
                                int idx = GetIndex(cell.Text.Replace("&quot;", "\u0022"), grid);
                                if (idx < row.Cells.Count) row.Cells[idx].Visible = true;
                            }
                        }
                    }
                }
            }
        }

        public void chk()
        {
            foreach (ListItem check in Chk_view.Items)
            {
                if (!check.Selected) continue;

                switch (check.Text)
                {
                    case "Основная информация": btn_dow_inf.Visible = true; pokaz_table(Table_inf); break;
                    case "Сведения о зданиях/сооружениях": btn_dow_cat.Visible = true; pokaz_table(Table_kat); break;
                    case "Сведения о земельном участке": btn_dow_sved.Visible = true; pokaz_table(Table_sved); break;
                    case "Инженерные системы": btn_dow_ing.Visible = true; pokaz_table(Table_ing); break;
                    case "Документы":
                        btn_dow_tex.Visible = true;
                        pokaz_table(Table_tex);
                        pokaz_table(GridView_OKS);
                        pokaz_table(GridView_ZU);
                        pokaz_table(GridView_documents);
                        break;
                    case "Иная документация": btn_dow_in.Visible = true; pokaz_table(Table_in); break;
                    case "Прочие данные": btn_dow_proch.Visible = true; pokaz_table(Table_proch); break;
                    case "Спортивные зоны": btn_dow_zone.Visible = true; pokaz_table(Table_zone); break;
                }
            }
            UpdateVidim();
        }

        public void chk_bool()
        {
            int count = Check_Object.Items.Cast<ListItem>().Count(chk => !chk.Selected);
            if (Check_Object.Items.Count == count)
            {
                foreach (ListItem chk in Chk_view.Items) chk.Selected = false;
                Skryt();
            }
        }

        int GetIndex(string columnName, GridView grid)
        {
            int idx = 0;
            if (grid.HeaderRow == null) return idx;
            foreach (TableCell cell in grid.HeaderRow.Cells)
            {
                if (cell.Text.Replace("&quot;", "\u0022") == columnName) break;
                idx++;
            }
            return idx;
        }

        // =====================================================================
        // ROWCREATED / ROWDATABOUND
        // =====================================================================

        public void shirina(GridView grid)
        {
            foreach (GridViewRow row in grid.Rows)
            {
                foreach (TableCell cell in row.Cells) cell.Width = 150;
                row.Cells[0].Width = 200;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        protected void Table_inf_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_inf);
        protected void Table_kat_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_kat);
        protected void Table_sved_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_sved);
        protected void Table_ing_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_ing);
        protected void GridView_OKS_RowCreated(object sender, GridViewRowEventArgs e) => shirina(GridView_OKS);
        protected void GridView_ZU_RowCreated(object sender, GridViewRowEventArgs e) => shirina(GridView_ZU);
        protected void GridView_documents_RowDataBound(object sender, GridViewRowEventArgs e) => shirina(GridView_documents);
        protected void Table_tex_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_tex);
        protected void Table_in_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_in);
        protected void Table_proch_RowCreated(object sender, GridViewRowEventArgs e) => shirina(Table_proch);

        protected void Table_zone_RowCreated(object sender, GridViewRowEventArgs e)
        {
            shirina(Table_zone);
            foreach (GridViewRow row in Table_zone.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Width = 150;
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.Style.Add("padding", "10px");
                }
                row.Cells[0].Width = 200;
            }
        }

        protected void Table_zone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
                    e.Row.Cells[i].Attributes["onclick"] = "javascript:ClickCell3('" + Server.HtmlDecode(e.Row.Cells[i].Text) + "')";
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
            }
        }

        protected void Table_inf_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
            }
        }

        // =====================================================================
        // НОВЫЙ ДИЗАЙН
        // =====================================================================

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Otchet2.aspx");
        }
    }
}