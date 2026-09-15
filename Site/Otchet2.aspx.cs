using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Otchet2 : System.Web.UI.Page
    {
        // Флаг: используются ли выбранные объекты
        private List<string> SelectedObjects
        {
            get
            {
                if (Session["otchet2_selected"] == null)
                    Session["otchet2_selected"] = new List<string>();
                return (List<string>)Session["otchet2_selected"];
            }
            set { Session["otchet2_selected"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Otch");
            if (panel != null) panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            ScriptManager.RegisterStartupScript(this, GetType(), "width", "width();", true);

            if (!IsPostBack)
            {
                LoadFilters();
                LoadObjectsCheckBoxList();
            }
        }

        // =====================================================================
        // ЗАГРУЗКА DropDownList
        // =====================================================================

        private void LoadFilters()
        {
            var objects = LocalData.GetObjects()
                .Where(o => o.Postuplenie != "ПЕРЕДАЧА")
                .ToList();

            // Округа
            if (DropDownList_District.Items.Count <= 1)
            {
                DropDownList_District.DataSource = objects
                    .Select(o => o.District).Where(v => !string.IsNullOrEmpty(v))
                    .Distinct().OrderBy(v => v)
                    .Select(v => new { District = v }).ToList();
                DropDownList_District.DataTextField = "District";
                DropDownList_District.DataValueField = "District";
                DropDownList_District.DataBind();
            }

            // Районы
            if (DropDownList_Category.Items.Count <= 1)
            {
                DropDownList_Category.DataSource = objects
                    .Select(o => o.Raion).Where(v => !string.IsNullOrEmpty(v))
                    .Distinct().OrderBy(v => v)
                    .Select(v => new { Raion = v }).ToList();
                DropDownList_Category.DataTextField = "Raion";
                DropDownList_Category.DataValueField = "Raion";
                DropDownList_Category.DataBind();
            }

            // Руководители
            if (DropDownList_Supervisor.Items.Count <= 1)
            {
                DropDownList_Supervisor.DataSource = objects
                    .Select(o => o.Supervisor).Where(v => !string.IsNullOrEmpty(v))
                    .Distinct().OrderBy(v => v)
                    .Select(v => new { Supervisor = v }).ToList();
                DropDownList_Supervisor.DataTextField = "Supervisor";
                DropDownList_Supervisor.DataValueField = "Supervisor";
                DropDownList_Supervisor.DataBind();
            }

            // Типы
            if (DropDownList_Tip.Items.Count <= 1)
            {
                DropDownList_Tip.DataSource = objects
                    .Select(o => o.Tip).Where(v => !string.IsNullOrEmpty(v))
                    .Distinct().OrderBy(v => v)
                    .Select(v => new { Tip = v }).ToList();
                DropDownList_Tip.DataTextField = "Tip";
                DropDownList_Tip.DataValueField = "Tip";
                DropDownList_Tip.DataBind();
            }
        }

        // =====================================================================
        // ЗАГРУЗКА CheckBoxList_Objects
        // =====================================================================

        private void LoadObjectsCheckBoxList()
        {
            var filtered = GetFilteredObjects();

            CheckBoxList_Objects.DataSource = filtered
                .Select(o => new { JoinedField = BuildJoinedField(o) })
                .ToList();
            CheckBoxList_Objects.DataTextField = "JoinedField";
            CheckBoxList_Objects.DataValueField = "JoinedField";
            CheckBoxList_Objects.DataBind();

            // Восстанавливаем выбор из сессии
            foreach (ListItem item in CheckBoxList_Objects.Items)
            {
                if (SelectedObjects.Contains(item.Value))
                    item.Selected = true;
            }
        }

        /// <summary>
        /// Собирает строку "Имя объекта Округ Категория" для отображения в CheckBoxList.
        /// </summary>
        private static string BuildJoinedField(LocalData.ObjectRecord obj)
        {
            return string.Join(" ",
                new[] { obj.NameObject, obj.District, obj.Category }
                    .Where(s => !string.IsNullOrEmpty(s)));
        }

        /// <summary>
        /// Возвращает объекты, отфильтрованные по DropDownList'ам и поиску.
        /// </summary>
        private List<LocalData.ObjectRecord> GetFilteredObjects()
        {
            var query = LocalData.GetObjects()
                .Where(o => o.Postuplenie != "ПЕРЕДАЧА")
                .AsEnumerable();

            // Фильтры
            if (DropDownList_District.SelectedValue != "%")
                query = query.Where(o => o.District == DropDownList_District.SelectedValue);
            if (DropDownList_Category.SelectedValue != "%")
                query = query.Where(o => o.Raion == DropDownList_Category.SelectedValue);
            if (DropDownList_Supervisor.SelectedValue != "%")
                query = query.Where(o => o.Supervisor == DropDownList_Supervisor.SelectedValue);
            if (DropDownList_Tip.SelectedValue != "%")
                query = query.Where(o => o.Tip == DropDownList_Tip.SelectedValue);

            // Поиск
            string search = TextBox_Search.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o =>
                    Contains(o.NameObject, search) ||
                    Contains(o.District, search) ||
                    Contains(o.Category, search) ||
                    Contains(o.Adress, search) ||
                    Contains(o.Raion, search));
            }

            return query.OrderBy(o => o.NameObject).ToList();
        }

        private static bool Contains(string source, string search) =>
            !string.IsNullOrEmpty(source) && source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        // =====================================================================
        // ОБРАБОТЧИКИ ФИЛЬТРОВ И ПОИСКА
        // =====================================================================

        protected void TextBox_Search_TextChanged(object sender, EventArgs e)
        {
            LoadObjectsCheckBoxList();
            CheckBoxList_Objects_SelectedIndexChanged(this, EventArgs.Empty);
        }

        protected void DropDownList_District_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadObjectsCheckBoxList();
        }

        protected void DropDownList_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadObjectsCheckBoxList();
        }

        protected void DropDownList_Supervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadObjectsCheckBoxList();
        }

        protected void DropDownList_Tip_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadObjectsCheckBoxList();
        }

        // =====================================================================
        // ВЫБРАТЬ ВСЕ / СНЯТЬ ВСЕ
        // =====================================================================

        protected void CheckBox_choose_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in CheckBoxList_Objects.Items)
                item.Selected = CheckBox_choose.Checked;

            CheckBox_choose.Text = CheckBox_choose.Checked ? "Снять все" : "Выбрать все";

            // Сохраняем выбранные в сессию
            SelectedObjects = CheckBoxList_Objects.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
                .ToList();

            CheckBoxList_Objects_SelectedIndexChanged(this, EventArgs.Empty);
        }

        // =====================================================================
        // ВЫБОР ОБЪЕКТОВ
        // =====================================================================

        protected void CheckBoxList_Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Собираем выбранные объекты
            var selected = CheckBoxList_Objects.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
                .ToList();

            SelectedObjects = selected;

            // Строим данные для GridView_objects
            var data = new List<object>();

            foreach (var joined in selected)
            {
                // Ищем объект по JoinedField
                var obj = LocalData.GetObjects().FirstOrDefault(o => BuildJoinedField(o) == joined);
                if (obj == null) continue;

                var doc = LocalData.GetObjectDocuments().FirstOrDefault(d => d.NameObject == obj.NameObject);
                var zones = LocalData.GetSportsZones().Where(z => z.NameObject == obj.NameObject).ToList();
                var zonesStr = zones.Count > 0 ? string.Join(", ", zones.Select(z => z.NameZone)) : "—";

                data.Add(new
                {
                    NameObject = obj.NameObject,
                    UrlImage = obj.UrlImage ?? "/Imges/no-image.png",

                    // Основная
                    obj.Adress,
                    obj.District,
                    obj.Raion,
                    obj.Indexx,
                    obj.Statuss,

                    // Здания
                    CadastralNumber = obj.CadastralNumber ?? "—",
                    ConstructionYear = obj.ConstructionYear ?? "—",
                    Levels = obj.Levels ?? "—",
                    CommissioningDate = obj.CommissioningDate ?? "—",
                    SqureBuildings = obj.SqureBuildings ?? "—",

                    // ЗУ
                    GreenSpaces = obj.GreenSpaces ?? "—",
                    SqureTerritory = obj.SqureTerritory ?? "—",
                    TerritoriesUsed = obj.TerritoriesUsed ?? "—",

                    // Инженерные
                    Electrosnab = obj.Electrosnab ?? "—",
                    Vodsnab = obj.Vodsnab ?? "—",
                    OZDS = obj.OZDS ?? "—",

                    // Документы
                    OrderOKS = doc?.OrderOKS ?? "—",
                    AktPriemStroi = doc?.AktPriemStroi ?? "—",
                    ExtractOKS = doc?.ExtractOKS ?? "—",

                    // Прочие
                    SqureCleaningBuildings = "—",
                    SqureCleaningTerritory = "—",
                    KolZonePriema = "—",
                    KolAparKKT = "—",
                    KolKruchkov = "—",
                    SpecTex = "—",

                    // Спортзоны
                    SportsZone = zonesStr
                });
            }

            GridView_objects.DataSource = data;
            GridView_objects.DataBind();

            // Включаем/выключаем кнопки
            bool hasData = data.Count > 0;
            Button_download.Enabled = hasData;
            Button_unfold.Enabled = hasData;
            Button_collapse.Enabled = hasData;
            Button_download_vidim.Enabled = hasData;
        }

        // =====================================================================
        // ДАННЫЕ ДЛЯ DetailsView (через RowDataBound)
        // =====================================================================

        protected void GridView_objects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            // Клик по имени объекта — на карту
            if (e.Row.Cells.Count > 0)
            {
                e.Row.Cells[0].Attributes["onclick"] = "javascript:ClickOtch(this)";
            }

            // DetailsView'ы уже привязаны к DataItem через DataField
            // Дополнительная логика: PDF-иконки, подсветка
            BindDetailsView(e.Row, "DetailsView_Main_info");
            BindDetailsView(e.Row, "DetailsView_Buildings");
            BindDetailsView(e.Row, "DetailsView_Area");
            BindDetailsView(e.Row, "DetailsView_Ing_Sys");
            BindDetailsView(e.Row, "DetailsView_Documents");
            BindDetailsView(e.Row, "DetailsView_Other_Data");
            BindDetailsView(e.Row, "DetailsView_Sports_Zone");

            // PDF-иконки в документах
            PDF(e.Row);

            // Подсветка спортивных зон
            SportsZone(e.Row);
        }

        /// <summary>
        /// Привязывает DetailsView к текущей строке GridView.
        /// </summary>
        private void BindDetailsView(GridViewRow row, string controlId)
        {
            var dv = row.FindControl(controlId) as DetailsView;
            if (dv == null) return;

            dv.DataSource = new[] { row.DataItem };
            dv.DataBind();
        }

        // =====================================================================
        // PDF-ИКОНКИ
        // =====================================================================

        protected void PDF(GridViewRow row)
        {
            var dv = row.FindControl("DetailsView_Documents") as DetailsView;
            if (dv == null) return;

            foreach (DetailsViewRow dvRow in dv.Rows)
            {
                if (dvRow.Cells.Count < 2) continue;

                string cellText = dvRow.Cells[1].Text;
                if (string.IsNullOrEmpty(cellText) ||
                    cellText == "&nbsp;" ||
                    cellText == "—" ||
                    cellText == "-" ||
                    cellText == "Нет информации" ||
                    cellText == "Не получен" ||
                    cellText == "Отсутствует" ||
                    cellText == "Получен")
                    continue;

                // Разбираем строку с путями через ";"
                var paths = cellText.Split(';').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s));
                dvRow.Cells[1].Controls.Clear();

                foreach (var path in paths)
                {
                    var hl = new HyperLink
                    {
                        ID = "pril_" + Guid.NewGuid().ToString("N").Substring(0, 6),
                        NavigateUrl = path,
                        Text = "<img src='/Imges/pdf.svg' style='width: 35px;' title='" +
                               path.Replace("/DocumentsObjects/", "") + "' ></img>",
                        Target = "_blank"
                    };
                    dvRow.Cells[1].Controls.Add(hl);
                }
            }
        }

        // =====================================================================
        // ПОДСВЕТКА СПОРТИВНЫХ ЗОН
        // =====================================================================

        protected void SportsZone(GridViewRow row)
        {
            var dv = row.FindControl("DetailsView_Sports_Zone") as DetailsView;
            if (dv == null) return;

            string nameObject = "";
            if (row.Cells.Count > 0) nameObject = Server.HtmlDecode(row.Cells[0].Text);

            foreach (DetailsViewRow dvRow in dv.Rows)
            {
                if (dvRow.Cells.Count < 2) continue;

                dvRow.Cells[1].Text = Server.HtmlDecode(dvRow.Cells[1].Text);
                dvRow.Cells[1].Attributes["onclick"] =
                    "javascript:ClickCell('" + nameObject.Replace("&quot;", "\u0022") + "')";
            }
        }

        // =====================================================================
        // РАЗВЕРНУТЬ / СВЕРНУТЬ ВСЕ
        // =====================================================================

        protected void Button_unfold_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView_objects.Rows)
            {
                var chk = row.FindControl("CheckBoxList_Select") as CheckBoxList;
                if (chk == null) continue;

                foreach (ListItem item in chk.Items) item.Selected = true;

                SetAllDetailsViews(row, true);
            }
        }

        protected void Button_collapse_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView_objects.Rows)
            {
                var chk = row.FindControl("CheckBoxList_Select") as CheckBoxList;
                if (chk == null) continue;

                foreach (ListItem item in chk.Items) item.Selected = false;

                SetAllDetailsViews(row, false);
            }
        }

        /// <summary>
        /// Показывает/скрывает все DetailsView в строке.
        /// </summary>
        private void SetAllDetailsViews(GridViewRow row, bool visible)
        {
            string[] ids = {
                "DetailsView_Main_info", "DetailsView_Buildings", "DetailsView_Area",
                "DetailsView_Ing_Sys", "DetailsView_Documents", "DetailsView_Other_Data",
                "DetailsView_Sports_Zone"
            };

            foreach (var id in ids)
            {
                var dv = row.FindControl(id) as DetailsView;
                if (dv != null) dv.Visible = visible;
            }
        }

        // =====================================================================
        // ВЫБОР РАЗДЕЛОВ ВНУТРИ КАРТОЧКИ
        // =====================================================================

        protected void CheckBoxList_Select_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBoxList;
            if (chk == null) return;

            var row = chk.NamingContainer as GridViewRow;
            if (row == null) return;

            // Показываем/скрываем DetailsView в зависимости от чекбоксов
            string[] ids = {
                "DetailsView_Main_info", "DetailsView_Buildings", "DetailsView_Area",
                "DetailsView_Ing_Sys", "DetailsView_Documents", "DetailsView_Other_Data",
                "DetailsView_Sports_Zone"
            };

            foreach (var id in ids)
            {
                var dv = row.FindControl(id) as DetailsView;
                if (dv == null) continue;

                var item = chk.Items.FindByValue(id);
                if (item != null) dv.Visible = item.Selected;
            }
        }

        // =====================================================================
        // ДАННЫЕ ДЛЯ ОТЧЁТА (GridView1)
        // =====================================================================

        protected void bind_data()
        {
            var selected = SelectedObjects;
            var rows = new List<object>();

            // Собираем все поля по первому объекту (как шаблон)
            if (selected.Count == 0) return;

            // Получаем объекты по JoinedField
            var objects = new List<LocalData.ObjectRecord>();
            foreach (var joined in selected)
            {
                var obj = LocalData.GetObjects().FirstOrDefault(o => BuildJoinedField(o) == joined);
                if (obj != null) objects.Add(obj);
            }

            if (objects.Count == 0) return;

            // Формируем строки: свойство + значения по объектам
            var firstObjProps = LocalData.GetFullObjectProperties(objects[0].NameObject);

            foreach (var prop in firstObjProps)
            {
                var values = new List<string>();
                foreach (var obj in objects)
                {
                    var props = LocalData.GetFullObjectProperties(obj.NameObject);
                    var match = props.FirstOrDefault(p => p.Key == prop.Key);
                    values.Add(match.Value ?? "—");
                }
                rows.Add(new { Свойство = prop.Key, Значения = values });
            }

            // Строим DataTable
            var dt = new DataTable();
            dt.Columns.Add("Свойство");
            for (int i = 0; i < objects.Count; i++)
                dt.Columns.Add("Объект" + (i + 1));

            foreach (var row in rows)
            {
                dynamic d = row;
                var dr = dt.NewRow();
                dr["Свойство"] = d.Свойство;
                for (int i = 0; i < d.Значения.Count; i++)
                    dr["Объект" + (i + 1)] = d.Значения[i];
                dt.Rows.Add(dr);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        // =====================================================================
        // СКАЧИВАНИЕ
        // =====================================================================

        protected void Button_download_Click(object sender, EventArgs e)
        {
            bind_data();
            download(GridView1, "Отчёт_объекты");
        }

        protected void Button_download_vidim_Click(object sender, EventArgs e)
        {
            bind_data();

            // Скрываем строки, которые не видны
            for (int i = 0; i < GridView1.Rows.Count; i++)
                GridView1.Rows[i].Visible = false;

            // Показываем строки по видимым DetailsView
            foreach (GridViewRow row in GridView_objects.Rows)
            {
                int key = 0;

                string[] ids = {
                    "DetailsView_Main_info", "DetailsView_Buildings",
                    "DetailsView_Area", "DetailsView_Documents"
                };

                foreach (var id in ids)
                {
                    var dv = row.FindControl(id) as DetailsView;
                    if (dv == null) continue;

                    if (dv.Visible)
                    {
                        int rows = dv.Rows.Count;
                        for (int r = 0; r < rows && key < GridView1.Rows.Count; r++)
                        {
                            GridView1.Rows[key].Visible = true;
                            key++;
                        }
                    }
                    else
                    {
                        key += dv.Rows.Count;
                    }
                }
            }

            download(GridView1, "Отчёт_объекты");
        }

        public override void VerifyRenderingInServerForm(Control control) { /* для экспорта */ }

        protected void download(GridView grid, string otch_name)
        {
            if (grid.HeaderRow != null)
                grid.HeaderRow.Cells[0].Text = "Имя объекта";

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
        // ROWDATABOUND для GridView1
        // =====================================================================

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
            }
        }

        // =====================================================================
        // ПОДСВЕТКА В CheckBoxList_Objects
        // =====================================================================

        protected void CheckBoxList_Objects_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem it in CheckBoxList_Objects.Items)
            {
                if (it.Text.Contains("span")) continue;

                string line = it.Text;
                string[] words = line.Split(' ');

                if (words.Length >= 2)
                {
                    string lastWord = words[words.Length - 2] + " " + words[words.Length - 1];
                    it.Text = it.Text.Replace(lastWord, "<span class='second_part'>" + lastWord + "</span>");
                }
            }
        }
    }
}