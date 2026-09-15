using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic.FileIO;

namespace Site
{
    public partial class Budget : System.Web.UI.Page
    {
        // Накопители для Footer в GridView (сбрасываются перед каждым DataBind)
        private double _limits = 0;
        private double _zaklDog = 0;
        private double _dogInProc = 0;
        private double _neOsvLimits = 0;
        private double _oplDog = 0;
        private double _cred = 0;
        private double _oplCred = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Подсветка мастер-панели
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Panel1");
            if (panel != null)
                panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            if (!IsPostBack)
            {
                CheckBox_isp_CheckedChanged(this, EventArgs.Empty);
                LoadAllFilters();
                SelectAllByDefault();

                // Первичная отрисовка графиков
                ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
            }

            // Сортировка таблиц по умолчанию
            if (string.IsNullOrEmpty(GridView_FIO.SortExpression))
                GridView_FIO.Sort("ПроцентЗаключенных", SortDirection.Descending);
            if (string.IsNullOrEmpty(GridView_KOSGU.SortExpression))
                GridView_KOSGU.Sort("ПроцентЗаключенных", SortDirection.Descending);
        }

        // =====================================================================
        // ЗАГРУЗКА ФИЛЬТРОВ ИЗ LocalData
        // =====================================================================

        private void LoadAllFilters()
        {
            var fio = LocalData.GetFio();
            var kosgu = LocalData.GetKosgu();

            // ФИО: КФО, Дата, Направление
            chk_kfo.DataSource = fio.Select(x => x.Kfo).Distinct().OrderBy(x => x).ToList();
            chk_kfo.DataTextField = "kfo";
            chk_kfo.DataValueField = "kfo";
            // Чтобы DataTextField работал с простыми строками, используем трюк с анонимным типом:
            chk_kfo.DataSource = fio.Select(x => x.Kfo).Distinct().OrderBy(x => x)
                .Select(k => new { kfo = k }).ToList();
            chk_kfo.DataBind();

            chk_datee.DataSource = fio.Select(x => x.Datee).Distinct().OrderByDescending(x => x)
                .Select(d => new { datee = d }).ToList();
            chk_datee.DataBind();

            chk_dir.DataSource = fio.Select(x => x.Direction).Distinct().OrderBy(x => x)
                .Select(d => new { direction = d }).ToList();
            chk_dir.DataBind();

            // КОСГУ: КФО, Дата, КОСГУ
            chk_kfo2.DataSource = kosgu.Select(x => x.Kfo).Distinct().OrderBy(x => x)
                .Select(k => new { kfo = k }).ToList();
            chk_kfo2.DataBind();

            chk_datee2.DataSource = kosgu.Select(x => x.Datee).Distinct().OrderByDescending(x => x)
                .Select(d => new { datee = d }).ToList();
            chk_datee2.DataBind();

            chk_kosgu.DataSource = kosgu.Select(x => x.Kosgu).Distinct().OrderBy(x => x)
                .Select(k => new { kosgu = k }).ToList();
            chk_kosgu.DataBind();
        }

        private void SelectAllByDefault()
        {
            foreach (ListItem item in chk_kfo.Items) item.Selected = true;
            foreach (ListItem item in chk_dir.Items) item.Selected = true;
            if (chk_datee.Items.Count > 0) chk_datee.Items[0].Selected = true;

            foreach (ListItem item in chk_kfo2.Items) item.Selected = true;
            foreach (ListItem item in chk_kosgu.Items) item.Selected = true;
            if (chk_datee2.Items.Count > 0) chk_datee2.Items[0].Selected = true;

            UpdateHiddenFields(chk_kfo, "HiddenField_kfo", isKfo: true);
            UpdateHiddenFields(chk_datee, "HiddenField_datee", isKfo: false);
            UpdateHiddenFields(chk_dir, "HiddenField_dir", isKfo: false);
            UpdateHiddenFields(chk_kfo2, "HiddenField_kfo2", isKfo: true);
            UpdateHiddenFields(chk_datee2, "HiddenField_datee2", isKfo: false);
            UpdateHiddenFields(chk_kosgu, "HiddenField_kosgu", isKfo: false);

            BindGridViews();
        }

        // =====================================================================
        // ОБНОВЛЕНИЕ HiddenField ПО ВЫБРАННЫМ ЭЛЕМЕНТАМ
        // =====================================================================

        private void UpdateHiddenFields(CheckBoxList source, string hiddenFieldId, bool isKfo)
        {
            List<Label> list = GetHiddenLabels();
            Label label = list.FirstOrDefault(l => l.ID == hiddenFieldId);
            if (label == null) return;

            var checkedList = source.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Text)
                .ToList();

            // KFO и KOSGU в SQL идут без кавычек (FIND_IN_SET / FIND_IN_SET), остальные — в кавычках
            if (isKfo)
                label.Text = string.Join(",", checkedList);
            else
                label.Text = string.Join("','", checkedList);
        }

        private List<Label> GetHiddenLabels()
        {
            return new List<Label>
            {
                HiddenField_kfo, HiddenField_datee, HiddenField_dir,
                HiddenField_kfo2, HiddenField_datee2, HiddenField_kosgu
            };
        }

        // =====================================================================
        // ФИЛЬТРАЦИЯ И ЗАПОЛНЕНИЕ GridView
        // =====================================================================

        private void BindGridViews()
        {
            BindFioGrid();
            BindKosguGrid();
        }

        private void BindFioGrid()
        {
            var kfoSet = ParseSet(HiddenField_kfo.Text, ',');
            var dateeSet = ParseSet(HiddenField_datee.Text, '\'');
            var dirSet = ParseSet(HiddenField_dir.Text, '\'');

            var source = LocalData.GetFio()
                .Where(r => kfoSet.Contains(r.Kfo)
                         && dateeSet.Contains(r.Datee)
                         && dirSet.Contains(r.Direction));

            var grouped = source
                .GroupBy(r => r.Direction)
                .Select(g => new
                {
                    Направление = g.Key,
                    ПроцентЗаключенных = SafePercent(g.Sum(x => x.ZaklyuchennyyeDogovory), g.Sum(x => x.Limitt)),
                    ПроцентОплаченных = SafePercent(g.Sum(x => x.OplachenyDogovory), g.Sum(x => x.Limitt)),
                    Лимиты = g.Sum(x => x.Limitt),
                    ЗаключеныДоговоры = g.Sum(x => x.ZaklyuchennyyeDogovory),
                    ДоговорыВПроцессе = g.Sum(x => x.DogovoryVP),
                    НеОсвоеныЛимиты = g.Sum(x => x.NeOsvoyenyLimity),
                    ОплаченыДоговоры = g.Sum(x => x.OplachenyDogovory)
                })
                .OrderByDescending(x => x.ПроцентЗаключенных)
                .ToList();

            ResetAccumulators();

            GridView_FIO.DataSource = grouped;
            GridView_FIO.DataBind();
        }

        private void BindKosguGrid()
        {
            var kfoSet = ParseSet(HiddenField_kfo2.Text, ',');
            var dateeSet = ParseSet(HiddenField_datee2.Text, '\'');
            var kosguSet = ParseSet(HiddenField_kosgu.Text, '\'');

            var source = LocalData.GetKosgu()
                .Where(r => kfoSet.Contains(r.Kfo)
                         && dateeSet.Contains(r.Datee)
                         && kosguSet.Contains(r.Kosgu));

            var grouped = source
                .GroupBy(r => r.Kosgu)
                .Select(g => new
                {
                    КОСГУ = g.Key,
                    ПроцентЗаключенных = SafePercent(g.Sum(x => x.SignedAnAgreement), g.Sum(x => x.Limit2022)),
                    ПроцентОплаченных = SafePercent(g.Sum(x => x.PaidContracts2022), g.Sum(x => x.Limit2022)),
                    Лимиты = g.Sum(x => x.Limit2022),
                    ЗаключеныДоговоры = g.Sum(x => x.SignedAnAgreement),
                    ДоговорыВПроцессе = g.Sum(x => x.ContractsInTheProcess),
                    НеОсвоеныЛимиты = g.Sum(x => x.LimitsNotUsed2022),
                    ОплаченыДоговоры = g.Sum(x => x.PaidContracts2022),
                    КредиторскаяЗадолженность = g.Sum(x => x.Debt2021),
                    ОплаченнаяКредиторская = g.Sum(x => x.PaidDebt2021)
                })
                .OrderByDescending(x => x.ПроцентЗаключенных)
                .ToList();

            ResetAccumulators();

            GridView_KOSGU.DataSource = grouped;
            GridView_KOSGU.DataBind();
        }

        private void ResetAccumulators()
        {
            _limits = 0;
            _zaklDog = 0;
            _dogInProc = 0;
            _neOsvLimits = 0;
            _oplDog = 0;
            _cred = 0;
            _oplCred = 0;
        }

        private static double SafePercent(double numerator, double denominator)
        {
            if (denominator <= 0) return 0;
            return Math.Round(numerator * 100.0 / denominator, 2);
        }

        private static HashSet<string> ParseSet(string raw, char separator)
        {
            if (string.IsNullOrWhiteSpace(raw)) return new HashSet<string>();
            return new HashSet<string>(
                raw.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(s => s.Trim().Trim('\''))
                   .Where(s => !string.IsNullOrEmpty(s)),
                StringComparer.OrdinalIgnoreCase);
        }

        // =====================================================================
        // ОБРАБОТЧИКИ СОБЫТИЙ (фильтры)
        // =====================================================================

        protected void chk_dir_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbl = sender as CheckBoxList;
            if (cbl == null) return;

            // radioMe для дат
            if (cbl.ID == "chk_datee")
                chk_datee.Attributes.Add("onclick", "radioMe(event);");
            else if (cbl.ID == "chk_datee2")
                chk_datee2.Attributes.Add("onclick", "radioMe2(event);");

            if (CheckBox_smisl.Checked)
                chk_kfo2.Attributes.Add("onclick", "radioMe3(event);");
            else
                chk_kfo2.Attributes.Add("onclick", "");

            // Логика смысловых групп
            if (CheckBox_smisl.Checked && chk_kfo2.Items.Count > 0 && chk_kfo2.Items[0].Selected)
            {
                CheckBox3.Visible = true;
                chk_kosgu_smisl1.Visible = true;
                CheckBox4.Visible = false;
                chk_kosgu_smisl2.Visible = false;
            }
            else if (CheckBox_smisl.Checked && chk_kfo2.Items.Count > 1 && chk_kfo2.Items[1].Selected)
            {
                CheckBox3.Visible = false;
                chk_kosgu_smisl1.Visible = false;
                CheckBox4.Visible = true;
                chk_kosgu_smisl2.Visible = true;
            }

            // Обновляем соответствующий HiddenField
            switch (cbl.ID)
            {
                case "chk_kfo": UpdateHiddenFields(cbl, "HiddenField_kfo", isKfo: true); break;
                case "chk_kfo2": UpdateHiddenFields(cbl, "HiddenField_kfo2", isKfo: true); break;
                case "chk_datee": UpdateHiddenFields(cbl, "HiddenField_datee", false); break;
                case "chk_datee2": UpdateHiddenFields(cbl, "HiddenField_datee2", false); break;
                case "chk_dir": UpdateHiddenFields(cbl, "HiddenField_dir", false); break;
                case "chk_kosgu": UpdateHiddenFields(cbl, "HiddenField_kosgu", false); break;
            }

            BindGridViews();
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        protected void CheckBox_isp_CheckedChanged(object sender, EventArgs e)
        {
            Panel_isp.Visible = true;
            CheckBox_isp.Checked = true;
            Panel_kosgu.Visible = false;
            CheckBox_kosgu.Checked = false;
            Section_kred.Visible = false;
            CheckBox_smisl.Checked = false;
            chk_kosgu_smisl2.Visible = false;
            CheckBox4.Visible = false;
            chk_kosgu_smisl1.Visible = false;

            downloadLink.HRef = "/Excel/ШаблонФИО.csv";
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        protected void CheckBox_kosgu_CheckedChanged(object sender, EventArgs e)
        {
            Panel_isp.Visible = false;
            CheckBox_isp.Checked = false;
            CheckBox_smisl.Checked = false;
            chk_kosgu_smisl2.Visible = false;
            CheckBox4.Visible = false;
            chk_kosgu_smisl1.Visible = false;
            Panel_kosgu.Visible = true;
            CheckBox2.Visible = true;
            CheckBox3.Visible = false;
            chk_kosgu.Visible = true;
            CheckBox_kosgu.Checked = true;
            Section_kred.Visible = true;

            downloadLink.HRef = "/Excel/ШаблонКОСГУ.csv";
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        protected void CheckBox_smisl_CheckedChanged(object sender, EventArgs e)
        {
            Panel_isp.Visible = false;
            CheckBox_isp.Checked = false;
            Panel_kosgu.Visible = true;
            CheckBox_kosgu.Checked = false;
            CheckBox2.Visible = false;
            chk_kosgu.Visible = false;
            Section_kred.Visible = true;

            CheckBox3.Visible = true;
            chk_kosgu_smisl1.Visible = true;
            if (chk_kfo2.Items.Count > 0) chk_kfo2.Items[0].Selected = true;
            if (chk_kfo2.Items.Count > 1) chk_kfo2.Items[1].Selected = false;

            chk_kosgu_smisl1_SelectedIndexChanged(chk_kosgu_smisl1, EventArgs.Empty);
            UpdateHiddenFields(chk_kfo2, "HiddenField_kfo2", isKfo: true);
            UpdateHiddenFields(chk_datee2, "HiddenField_datee2", false);

            downloadLink.HRef = "/Excel/ШаблонКОСГУ.csv";
            BindGridViews();
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        protected void chk_kosgu_smisl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbl = sender as CheckBoxList;
            if (cbl == null) return;

            HiddenField_kosgu.Text = "";
            var checkedList = cbl.Items.Cast<ListItem>()
                .Where(i => i.Selected)
                .Select(i => i.Value)
                .ToList();

            HiddenField_kosgu.Text = string.Join("','", checkedList);

            BindGridViews();
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb == null) return;

            if (cb.ID == "CheckBox1")
            {
                cb.Text = cb.Checked ? "Сбросить все" : "Выбрать все";
                foreach (ListItem item in chk_dir.Items) item.Selected = cb.Checked;
                chk_dir_SelectedIndexChanged(chk_dir, EventArgs.Empty);
            }
            else if (cb.ID == "CheckBox2")
            {
                cb.Text = cb.Checked ? "Сбросить все" : "Выбрать все";
                foreach (ListItem item in chk_kosgu.Items) item.Selected = cb.Checked;
                chk_dir_SelectedIndexChanged(chk_kosgu, EventArgs.Empty);
            }
            else if (cb.ID == "CheckBox3")
            {
                cb.Text = cb.Checked ? "Сбросить все" : "Выбрать все";
                foreach (ListItem item in chk_kosgu_smisl1.Items) item.Selected = cb.Checked;
                chk_kosgu_smisl1_SelectedIndexChanged(chk_kosgu_smisl1, EventArgs.Empty);
            }
            else if (cb.ID == "CheckBox4")
            {
                cb.Text = cb.Checked ? "Сбросить все" : "Выбрать все";
                foreach (ListItem item in chk_kosgu_smisl2.Items) item.Selected = cb.Checked;
                chk_kosgu_smisl1_SelectedIndexChanged(chk_kosgu_smisl2, EventArgs.Empty);
            }
        }

        protected void CheckBox_kred_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb == null) return;
            GridView_KOSGU.Columns[8].Visible = cb.Checked;
            GridView_KOSGU.Columns[9].Visible = cb.Checked;
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        protected void DropDownList_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Фильтрация дат по выбранному году (для демо просто обновляем всё)
            if (chk_datee.Items.Count > 0) chk_datee.Items[0].Selected = true;
            if (chk_datee2.Items.Count > 0) chk_datee2.Items[0].Selected = true;
            BindGridViews();
            ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
        }

        // =====================================================================
        // GridView: подсветка ячеек и подсчёт Footer
        // =====================================================================

        protected void GridView_FIO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Скрываем пустые строки
                if (e.Row.Cells[0].Text == "" || e.Row.Cells[0].Text == "&nbsp;"
                    || e.Row.Cells[1].Text == "&nbsp;" || e.Row.Cells[1].Text == "")
                {
                    e.Row.Visible = false;
                    return;
                }

                // Накопление итогов
                _limits += ParseDoubleSafe(e.Row.Cells[3].Text);
                _zaklDog += ParseDoubleSafe(e.Row.Cells[4].Text);
                _dogInProc += ParseDoubleSafe(e.Row.Cells[5].Text);
                _neOsvLimits += ParseDoubleSafe(e.Row.Cells[6].Text);
                _oplDog += ParseDoubleSafe(e.Row.Cells[7].Text);

                var grid = (GridView)sender;
                if (grid.ID == "GridView_KOSGU" && CheckBox_kred.Checked)
                {
                    if (e.Row.Cells.Count > 9)
                    {
                        _cred += ParseDoubleSafe(e.Row.Cells[8].Text);
                        _oplCred += ParseDoubleSafe(e.Row.Cells[9].Text);
                    }
                }

                // Подсветка колонок 1 и 2 (проценты)
                for (int i = 1; i < 3 && i < e.Row.Cells.Count; i++)
                {
                    var text = e.Row.Cells[i].Text;
                    if (text == "&nbsp;" || string.IsNullOrEmpty(text)) continue;
                    if (!double.TryParse(text, out double val)) continue;

                    e.Row.Cells[i].BackColor = GetColorByPercent(val);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                double percZakl = _limits > 0 ? Math.Round(_zaklDog / _limits, 2) : 0;
                double percOpl = _limits > 0 ? Math.Round(_oplDog / _limits, 2) : 0;

                e.Row.Cells[0].Text = "Общий итог";
                e.Row.Cells[1].Text = percZakl.ToString();
                e.Row.Cells[2].Text = percOpl.ToString();
                e.Row.Cells[3].Text = Math.Round(_limits, 2).ToString();
                e.Row.Cells[4].Text = Math.Round(_zaklDog, 2).ToString();
                e.Row.Cells[5].Text = Math.Round(_dogInProc, 2).ToString();
                e.Row.Cells[6].Text = Math.Round(_neOsvLimits, 2).ToString();
                e.Row.Cells[7].Text = Math.Round(_oplDog, 2).ToString();

                if (((GridView)sender).ID == "GridView_KOSGU" && e.Row.Cells.Count > 9)
                {
                    e.Row.Cells[8].Text = Math.Round(_cred, 2).ToString();
                    e.Row.Cells[9].Text = Math.Round(_oplCred, 2).ToString();
                }
            }
        }

        private static double ParseDoubleSafe(string text)
        {
            if (string.IsNullOrEmpty(text) || text == "&nbsp;") return 0;
            return double.TryParse(text, out double v) ? v : 0;
        }

        private static System.Drawing.Color GetColorByPercent(double val)
        {
            if (val > 130) return System.Drawing.Color.FromArgb(99, 190, 123);
            if (val > 110) return System.Drawing.Color.FromArgb(144, 203, 126);
            if (val > 100) return System.Drawing.Color.FromArgb(203, 220, 129);
            if (val > 90) return System.Drawing.Color.FromArgb(224, 226, 131);
            if (val > 80) return System.Drawing.Color.FromArgb(246, 233, 132);
            if (val > 70) return System.Drawing.Color.FromArgb(246, 233, 132);
            if (val > 55) return System.Drawing.Color.FromArgb(253, 205, 126);
            if (val > 30) return System.Drawing.Color.FromArgb(251, 166, 118);
            return System.Drawing.Color.FromArgb(249, 129, 111);
        }

        protected void GridView_FIO_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression != ((GridView)sender).SortExpression)
                e.SortDirection = SortDirection.Descending;

            ScriptManager.RegisterStartupScript(this, GetType(), "sortArrows", "myjava2();", true);
        }

        // =====================================================================
        // WEB METHODS ДЛЯ ГРАФИКОВ (замена SQL на LINQ)
        // =====================================================================

        [WebMethod]
        public static string[,] GetGraphData(string data0, string data1, string data2)
        {
            var kfoSet = ParseSetStatic(data0, ',');
            var dateeSet = ParseSetStatic(data1, '\'');
            var dirSet = ParseSetStatic(data2, '\'');

            var records = LocalData.GetFio()
                .Where(r => kfoSet.Contains(r.Kfo)
                         && dateeSet.Contains(r.Datee)
                         && dirSet.Contains(r.Direction))
                .ToList();

            var values = new string[records.Count, 5];
            for (int i = 0; i < records.Count; i++)
            {
                values[i, 0] = records[i].Limitt.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 1] = records[i].NeOsvoyenyLimity.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 2] = records[i].ZaklyuchennyyeDogovory.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 3] = records[i].OplachenyDogovory.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 4] = records[i].DogovoryVP.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            return values;
        }

        [WebMethod]
        public static string[,] GetGraphData2(string data0, string data1, string data2, bool cred)
        {
            var kfoSet = ParseSetStatic(data0, ',');
            var dateeSet = ParseSetStatic(data1, '\'');
            var kosguSet = ParseSetStatic(data2, '\'');

            var records = LocalData.GetKosgu()
                .Where(r => kfoSet.Contains(r.Kfo)
                         && dateeSet.Contains(r.Datee)
                         && kosguSet.Contains(r.Kosgu))
                .ToList();

            var values = new string[records.Count, 7];
            for (int i = 0; i < records.Count; i++)
            {
                var r = records[i];
                values[i, 0] = r.Limit2022.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 1] = r.LimitsNotUsed2022.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 2] = (cred ? r.SignedAnAgreement : r.SignedAnAgreement + r.Debt2021)
                    .ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 3] = (cred ? r.PaidContracts2022 : r.PaidContracts2022 + r.PaidDebt2021)
                    .ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 4] = r.ContractsInTheProcess.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 5] = r.Debt2021.ToString(System.Globalization.CultureInfo.InvariantCulture);
                values[i, 6] = r.PaidDebt2021.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            return values;
        }

        private static HashSet<string> ParseSetStatic(string raw, char separator)
        {
            if (string.IsNullOrWhiteSpace(raw)) return new HashSet<string>();
            return new HashSet<string>(
                raw.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(s => s.Trim().Trim('\''))
                   .Where(s => !string.IsNullOrEmpty(s)),
                StringComparer.OrdinalIgnoreCase);
        }

        // =====================================================================
        // ЗАГРУЗКА CSV В LocalData
        // =====================================================================

        protected void Button_csv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oFile.Value))
            {
                ShowSwal();
                return;
            }

            string strFileName = Path.GetFileName(oFile.PostedFile.FileName);
            string strFolder = Server.MapPath("./Excel/");
            string strFilePath = Path.Combine(strFolder, strFileName);
            oFile.PostedFile.SaveAs(strFilePath);

            try
            {
                if (CheckBox_isp.Checked)
                    LoadFioFromCsv(strFilePath);
                else
                    LoadKosguFromCsv(strFilePath);

                // Обновляем фильтры после загрузки
                LoadAllFilters();
                SelectAllByDefault();

                ShowSwal();
                ScriptManager.RegisterStartupScript(this, GetType(), "runChart", "myjava();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "uploadError",
                    $"alert('Ошибка загрузки CSV: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        private void LoadFioFromCsv(string csvFilePath)
        {
            var list = new List<LocalData.FioRecord>();

            using (var parser = new TextFieldParser(csvFilePath, System.Text.Encoding.GetEncoding("Windows-1251")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                parser.ReadLine(); // пропускаем заголовок

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields == null || fields.Length < 8) continue;

                    list.Add(new LocalData.FioRecord
                    {
                        Datee = ConvertDate(fields[0]),
                        Kfo = fields[1],
                        Direction = fields[2],
                        Limitt = ParseNum(fields[3]),
                        ZaklyuchennyyeDogovory = ParseNum(fields[4]),
                        DogovoryVP = ParseNum(fields[5]),
                        NeOsvoyenyLimity = ParseNum(fields[6]),
                        OplachenyDogovory = ParseNum(fields[7])
                    });
                }
            }

            LocalData.ReplaceFio(list);
        }

        private void LoadKosguFromCsv(string csvFilePath)
        {
            var list = new List<LocalData.KosguRecord>();

            using (var parser = new TextFieldParser(csvFilePath, System.Text.Encoding.GetEncoding("Windows-1251")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields == null || fields.Length < 10) continue;

                    list.Add(new LocalData.KosguRecord
                    {
                        Datee = ConvertDate(fields[0]),
                        Kfo = fields[1],
                        Kosgu = fields[2],
                        Limit2022 = ParseNum(fields[3]),
                        Debt2021 = ParseNum(fields[4]),
                        PaidDebt2021 = ParseNum(fields[5]),
                        SignedAnAgreement = ParseNum(fields[6]),
                        ContractsInTheProcess = ParseNum(fields[7]),
                        PaidContracts2022 = ParseNum(fields[8]),
                        LimitsNotUsed2022 = ParseNum(fields[9])
                    });
                }
            }

            LocalData.ReplaceKosgu(list);
        }

        private static string ConvertDate(string raw)
        {
            // Ожидаем дд.мм.гггг -> гггг-мм-дд
            if (string.IsNullOrEmpty(raw) || raw.Length < 10) return raw;
            try
            {
                return raw.Substring(6, 4) + "-" + raw.Substring(3, 2) + "-" + raw.Substring(0, 2);
            }
            catch
            {
                return raw;
            }
        }

        private static double ParseNum(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return 0;
            raw = raw.Replace(",", ".").Trim();
            return double.TryParse(raw, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double v) ? v : 0;
        }

        private void ShowSwal()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "swalKey", "sw();", true);
        }
    }
}