using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Default : System.Web.UI.Page
    {
        public bool flag_search = false;

        // Состояние фильтров в Session (чтобы сохранялось между запросами)
        private string F1 { get => (Session["select_filters1"] as string) ?? "%"; set => Session["select_filters1"] = value; }
        private string F2 { get => (Session["select_filters2"] as string) ?? "%"; set => Session["select_filters2"] = value; }
        private string F3 { get => (Session["select_filters3"] as string) ?? "%"; set => Session["select_filters3"] = value; }
        private string F4 { get => (Session["select_filters4"] as string) ?? "%"; set => Session["select_filters4"] = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Panel4");
            if (panel != null) panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            if (!IsPostBack)
            {
                LoadFilters();
                RestoreFiltersFromSession();
                BindAll();
                CheckBoxList_filter1_SelectedIndexChanged(this, EventArgs.Empty);

                if (!string.IsNullOrEmpty(Session["Search_Master"] as string))
                {
                    TextSearch.Text = Session["Search_Master"].ToString();
                    Btn_poisk_Click(this, EventArgs.Empty);
                }

                // Заход с карты (?map=ИмяОбъекта)
                if (Request.QueryString["map"] != null)
                {
                    TextSearch.Text = Request.QueryString["map"];
                    ApplySearch();
                }
            }

            TextSearch.Attributes.Add("onkeypress",
                "return clickButton(event,'" + Btn_poisk.ClientID + "')");
            UpdateObjectCount();
            btn_dow_hidden_TableObject.Visible = flag_search;
        }

        // =====================================================================
        // ЗАГРУЗКА И ВОССТАНОВЛЕНИЕ ФИЛЬТРОВ
        // =====================================================================

        private void LoadFilters()
        {
            var objects = LocalData.GetObjects();

            DropDownList1.DataSource = objects
                .Select(o => o.District).Where(d => !string.IsNullOrEmpty(d))
                .Distinct().OrderBy(d => d)
                .Select(d => new { District = d }).ToList();
            DropDownList1.DataTextField = "District";
            DropDownList1.DataValueField = "District";
            DropDownList1.DataBind();

            DropDownList2.DataSource = objects
                .Select(o => o.Raion).Where(d => !string.IsNullOrEmpty(d))
                .Distinct().OrderBy(d => d)
                .Select(d => new { Raion = d }).ToList();
            DropDownList2.DataTextField = "Raion";
            DropDownList2.DataValueField = "Raion";
            DropDownList2.DataBind();

            DropDownList3.DataSource = objects
                .Select(o => o.Supervisor).Where(d => !string.IsNullOrEmpty(d))
                .Distinct().OrderBy(d => d)
                .Select(d => new { Supervisor = d }).ToList();
            DropDownList3.DataTextField = "Supervisor";
            DropDownList3.DataValueField = "Supervisor";
            DropDownList3.DataBind();

            DropDownListTip.DataSource = objects
                .Select(o => o.Tip).Where(d => !string.IsNullOrEmpty(d))
                .Distinct().OrderBy(d => d)
                .Select(d => new { Tip = d }).ToList();
            DropDownListTip.DataTextField = "Tip";
            DropDownListTip.DataValueField = "Tip";
            DropDownListTip.DataBind();
        }

        private void RestoreFiltersFromSession()
        {
            SetSelectedIfExists(DropDownList1, F1);
            SetSelectedIfExists(DropDownList2, F2);
            SetSelectedIfExists(DropDownList3, F3);
            SetSelectedIfExists(DropDownListTip, F4);
        }

        private void SetSelectedIfExists(DropDownList ddl, string value)
        {
            if (!string.IsNullOrEmpty(value) && ddl.Items.FindByValue(value) != null)
                ddl.SelectedValue = value;
        }

        // =====================================================================
        // ФИЛЬТРАЦИЯ
        // =====================================================================

        private List<LocalData.ObjectRecord> GetFilteredObjects(bool applyFilters = true)
        {
            var query = LocalData.GetObjects().Where(o => o.Postuplenie != "ПЕРЕДАЧА");

            if (applyFilters)
            {
                if (F1 != "%") query = query.Where(o => o.District == F1);
                if (F2 != "%") query = query.Where(o => o.Raion == F2);
                if (F3 != "%") query = query.Where(o => o.Supervisor == F3);
                if (F4 != "%") query = query.Where(o => o.Tip == F4);
            }

            return query.OrderBy(o => o.NameObject).ToList();
        }

        private List<LocalData.ObjectRecord> GetSearchedObjects()
        {
            var query = LocalData.GetObjects().Where(o => o.Postuplenie != "ПЕРЕДАЧА");
            var search = TextSearch.Text.Trim();
            if (string.IsNullOrEmpty(search)) return query.ToList();

            return query.Where(o =>
                Contains(o.District, search) || Contains(o.NameObject, search) ||
                Contains(o.Category, search) || Contains(o.Adress, search) ||
                Contains(o.SportsZone, search) || Contains(o.Supervisor, search) ||
                Contains(o.Raion, search) || Contains(o.Tip, search))
                .ToList();
        }

        private static bool Contains(string source, string search) =>
            !string.IsNullOrEmpty(source) && source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        private void BindAll(bool searchMode = false)
        {
            var data = searchMode ? GetSearchedObjects() : GetFilteredObjects();

            var view = data.Select(o => new
            {
                o.Lat,
                o.Lagg,
                o.UrlMark,
                o.Adress,
                o.NumberObject,
                o.Raion,
                o.District,
                o.Tip,
                o.Supervisor,
                o.NameObject,
                o.Category,
                o.SportsZone
            }).ToList();

            // === TableObject: без колонки UrlMark ===
            TableObject.DataSource = view;
            TableObject.DataBind();

            // === Скрытый GridView для экспорта ===
            GridView_TableObject_hidden.DataSource = view;
            GridView_TableObject_hidden.DataBind();

            // === rptMarkers для JS-карты: нужны все поля, включая UrlMark ===
            rptMarkers.DataSource = view;
            rptMarkers.DataBind();
        }

        private void ApplySearch()
        {
            BindAll(searchMode: true);
            flag_search = true;
        }

        private void UpdateObjectCount()
        {
            TableObject.AllowPaging = false;
            TableObject.DataBind();
            Kol_vo_object.Text = "Объектов на карте: " + TableObject.Rows.Count;
            TableObject.AllowPaging = true;
            TableObject.DataBind();
        }

        // =====================================================================
        // ОБРАБОТЧИКИ ФИЛЬТРОВ
        // =====================================================================

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) { F1 = DropDownList1.SelectedValue; BindAll(); UpdateObjectCount(); }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e) { F2 = DropDownList2.SelectedValue; BindAll(); UpdateObjectCount(); }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e) { F3 = DropDownList3.SelectedValue; BindAll(); UpdateObjectCount(); }
        protected void DropDownListTip_SelectedIndexChanged(object sender, EventArgs e) { F4 = DropDownListTip.SelectedValue; BindAll(); UpdateObjectCount(); }

        protected void Btn_poisk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextSearch.Text))
            {
                Session["Search_Master"] = TextSearch.Text;
                ApplySearch();
            }
            UpdateObjectCount();
        }

        protected void Btn_update_Click(object sender, EventArgs e)
        {
            F1 = F2 = F3 = F4 = "%";
            Session["Search_Master"] = "";
            flag_search = false;
            Response.Redirect(Request.Path);
        }

        // =====================================================================
        // GridView: скрытие колонок и клики
        // =====================================================================

        protected void TableObject_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Автоматически сгенерированные колонки (AutoGenerateColumns=true) идут
            // в том порядке, в каком поля объявлены в анонимном типе view:
            //   0: Lat, 1: Lagg, 2: UrlMark, 3: Adress, 4: NumberObject,
            //   5: Raion, 6: District, 7: Tip, 8: Supervisor,
            //   9: NameObject, 10: Category, 11: SportsZone
            //
            // Нам нужно показать только:
            //   - Adress (3)
            //   - NameObject (9)
            //
            // Всё остальное — скрыть.

            if (e.Row.Cells.Count < 12) return;

            e.Row.Cells[0].Visible = false;  // Lat
            e.Row.Cells[1].Visible = false;  // Lagg
            e.Row.Cells[2].Visible = false;  // UrlMark
                                             // 3: Adress — оставляем
            e.Row.Cells[4].Visible = false;  // NumberObject
            e.Row.Cells[5].Visible = false;  // Raion
            e.Row.Cells[6].Visible = false;  // District
            e.Row.Cells[7].Visible = false;  // Tip
            e.Row.Cells[8].Visible = false;  // Supervisor
                                             // 9: NameObject — оставляем
            e.Row.Cells[10].Visible = false; // Category
            e.Row.Cells[11].Visible = false; // SportsZone
        }

        protected void TableObject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                e.Row.Attributes["onclick"] = "javascript:ClickRow2(this)";
        }

        // =====================================================================
        // ФИЛЬТРЫ: чекбоксы
        // =====================================================================

        protected void CheckBox_object_CheckedChanged(object sender, EventArgs e)
        {
            bool visible = !CheckBox_object.Checked;
            DropDownList1.Visible = visible;
            DropDownList2.Visible = visible;
            DropDownList3.Visible = visible;
            DropDownListTip.Visible = visible;
        }

        protected void CheckBox_prof_CheckedChanged(object sender, EventArgs e) { Proil_check.Visible = CheckBox_prof.Checked; }
        protected void CheckBox_neprof_CheckedChanged(object sender, EventArgs e) { CheckBoxList4.Visible = CheckBox_neprof.Checked; }
        protected void CheckBox_tip_CheckedChanged(object sender, EventArgs e) { CheckBoxList_tip.Visible = CheckBox_tip.Checked; }
        protected void CheckBox_sporter_CheckedChanged(object sender, EventArgs e) { CheckBoxList_sporter.Visible = CheckBox_sporter.Checked; }

        protected void Btn_filt_ServerClick(object sender, EventArgs e) { Fi_def.Visible = !Fi_def.Visible; }

        protected void CheckBoxList_filter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_CheckBoxList_filter1.Text = string.Join("','",
                CheckBoxList_filter1.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
        }

        protected void CheckBoxList_filter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_CheckBoxList_filter2.Text = string.Join("','",
                CheckBoxList_filter2.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
        }

        protected void CheckBoxList_filter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label_CheckBoxList_filter3.Text = string.Join("','",
                CheckBoxList_filter3.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
        }

        // Убрали static filter4 — теперь вычисляем на лету
        protected void CheckBoxList_tip_SelectedIndexChanged(object sender, EventArgs e) { UpdateFilter4(); }
        protected void CheckBoxList_sporter_SelectedIndexChanged(object sender, EventArgs e) { UpdateFilter4(); }
        protected void DropDownFiltersProfils_dr_SelectedIndexChanged(object sender, EventArgs e) { UpdateFilter4(); }
        protected void CheckBoxList4_SelectedIndexChanged(object sender, EventArgs e) { UpdateFilter4(); }

        private void UpdateFilter4()
        {
            var values = new List<string>();
            values.AddRange(CheckBoxList_tip.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
            values.AddRange(CheckBoxList_sporter.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
            values.AddRange(DropDownFiltersProfils_dr.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
            values.AddRange(CheckBoxList4.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));

            Label_CheckBoxList_filter4.Text = string.Join("','", values);
        }

        protected void btn_fult_accept_Click(object sender, EventArgs e)
        {
            // Применяем фильтры 1–3 к списку объектов
            var all = LocalData.GetObjects().Where(o => o.Postuplenie != "ПЕРЕДАЧА").ToList();

            var f1 = SplitFilter(Label_CheckBoxList_filter1.Text);
            var f2 = SplitFilter(Label_CheckBoxList_filter2.Text);
            var f3 = SplitFilter(Label_CheckBoxList_filter3.Text);
            var f4 = SplitFilter(Label_CheckBoxList_filter4.Text);

            if (f1.Count > 0) all = all.Where(o => f1.Contains(o.Postuplenie)).ToList();
            if (f2.Count > 0) all = all.Where(o => f2.Contains(o.FunckSoderj)).ToList();
            if (f3.Count > 0) all = all.Where(o => f3.Contains(o.DopPoten)).ToList();
            if (f4.Count > 0) all = all.Where(o => f4.Contains(o.KlassPoDepar) || f4.Contains(o.Tip)).ToList();

            var view = all.Select(o => new { o.Lat, o.Lagg, o.UrlMark, o.Adress, o.NumberObject, o.Raion, o.District, o.Tip, o.Supervisor, o.NameObject, o.Category, o.SportsZone }).ToList();

            TableObject.DataSource = view;
            TableObject.DataBind();
            GridView_TableObject_hidden.DataSource = view;
            GridView_TableObject_hidden.DataBind();
            rptMarkers.DataSource = view;
            rptMarkers.DataBind();

            Fi_def.Visible = false;
            UpdateObjectCount();
        }

        private static List<string> SplitFilter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return new List<string>();
            return raw.Split(new[] { "','" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => s.Trim().Trim('\''))
                      .Where(s => !string.IsNullOrEmpty(s))
                      .ToList();
        }

        protected void btn_fult_cancel_Click(object sender, EventArgs e)
        {
            BindAll();
            Fi_def.Visible = false;
            UpdateObjectCount();
        }

        // =====================================================================
        // Экспорт в Excel
        // =====================================================================

        public override void VerifyRenderingInServerForm(Control control) { /* для экспорта */ }

        protected void btn_dow_hidden_TableObject_Click(object sender, ImageClickEventArgs e)
        {
            GridView_TableObject_hidden.Visible = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Объекты.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            var sw = new StringWriter();
            var hw = new HtmlTextWriter(sw);
            GridView_TableObject_hidden.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
            GridView_TableObject_hidden.Visible = false;
        }
    }
}