using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Infoo");
            if (panel != null) panel.BackColor = System.Drawing.Color.FromArgb(192, 37, 46);

            // Устанавливаем даты для скрытых Label'ов
            SetDateLabels();

            // Управление видимостью по чекбоксу
            ApplyRedactVisibility();

            // Заполняем Repeater'ы (графики)
            LoadAllRepeaters();

            // Заполняем GridView'ы
            BindGridViews();

            // Заголовки
            UpdateHeaders();
        }

        // =====================================================================
        // ДАТЫ
        // =====================================================================

        private void SetDateLabels()
        {
            var today = DateTime.Today;

            var dates = new DateTime[11];
            for (int i = 0; i < 11; i++) dates[i] = today.AddMonths(-(i + 1));

            Label1.Text = FormatYearMonth(dates[0]);
            Label2.Text = FormatYearMonth(dates[1]);
            Month3.Text = FormatYearMonth(dates[2]);
            Month4.Text = FormatYearMonth(dates[3]);
            Month5.Text = FormatYearMonth(dates[4]);
            Month6.Text = FormatYearMonth(dates[5]);
            Month7.Text = FormatYearMonth(dates[6]);
            Month8.Text = FormatYearMonth(dates[7]);
            Month9.Text = FormatYearMonth(dates[8]);
            Month10.Text = FormatYearMonth(dates[9]);
            Month11.Text = FormatYearMonth(dates[10]);

            Label3.Text = dates[0].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            Label4.Text = dates[1].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth3.Text = dates[2].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth4.Text = dates[3].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth5.Text = dates[4].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth6.Text = dates[5].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth7.Text = dates[6].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth8.Text = dates[7].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth9.Text = dates[8].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth10.Text = dates[9].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            NameMonth11.Text = dates[10].ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
        }

        private static string FormatYearMonth(DateTime d) => d.ToString("yyyy-MM-");

        // =====================================================================
        // ЗАГОЛОВКИ
        // =====================================================================

        private void UpdateHeaders()
        {
            string month = DropDownList_Month?.SelectedItem?.Text ?? "—";
            int year = DateTime.Now.Year;

            Label6.Text = $"{month} {year} г.";
            Label9.Text = $"{month} {year} г.";
            Label10.Text = $"Соотношение обоснованных жалоб по отношению к общему количеству обращений за {month} {year} г.";
            Label_date_sootnosh.Text = $"{year}{DropDownList_Month?.SelectedValue}01";
        }

        // =====================================================================
        // ВИДИМОСТЬ ПО ЧЕКБОКСУ «РЕДАКТИРОВАТЬ»
        // =====================================================================

        private void ApplyRedactVisibility()
        {
            bool redact = CheckBox_redact.Checked;

            Button_update.Visible = redact;
            Button_update2.Visible = redact;
            Button_update3.Visible = redact;

            Panel_obrasch.Visible = !redact;
            Panel_Top.Visible = !redact;
            Panel_sravnenie.Visible = !redact;
            Panel_dynamics.Visible = !redact;
            Panel_text.Visible = !redact;

            GridView4_NoRedakt.Visible = !redact;
            GridView_Redakt.Visible = redact;

            GridView_Spravka_Noredakt.Visible = !redact;
            GridView_Spravka_Redakt.Visible = redact;

            GridView1.Visible = !redact;
            GridView2.Visible = !redact;
            GridView_dynamics.Visible = redact;
        }

        // =====================================================================
        // ЗАПОЛНЕНИЕ REPEATER'ОВ (ГРАФИКИ)
        // =====================================================================

        private void LoadAllRepeaters()
        {
            string month = DropDownList_Month?.SelectedValue ?? "-01-";
            var docs = LocalData.GetDocRows();

            // 1. Статистика по объектам (rptMarkers → myChart)
            var byObject = docs
                .Where(d => !string.IsNullOrEmpty(d.NameObject) && d.DataReg.Contains(month))
                .GroupBy(d => d.NameObject)
                .Select(g => new { NameObject = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();
            rptMarkers.DataSource = byObject;
            rptMarkers.DataBind();

            // 2-5. Топ-N объектов и фильтры по ним
            var topObjects = byObject.Take(4).ToList();
            rptMarkers2.DataSource = GetFiltersForObject(docs, topObjects.ElementAtOrDefault(0)?.NameObject, month);
            rptMarkers2.DataBind();
            rptMarkers5.DataSource = GetFiltersForObject(docs, topObjects.ElementAtOrDefault(1)?.NameObject, month);
            rptMarkers5.DataBind();
            rptMarkers6.DataSource = GetFiltersForObject(docs, topObjects.ElementAtOrDefault(2)?.NameObject, month);
            rptMarkers6.DataBind();
            rptMarkers7.DataSource = GetFiltersForObject(docs, topObjects.ElementAtOrDefault(3)?.NameObject, month);
            rptMarkers7.DataBind();

            // 6. Увеличение обращений по направлениям
            rptMarkers8.DataSource = GetComparison(docs, month, increase: true);
            rptMarkers8.DataBind();

            // 7. Уменьшение обращений по направлениям
            rptMarkers9.DataSource = GetComparison(docs, month, increase: false);
            rptMarkers9.DataBind();

            // 8. Динамика по источникам
            rptMarkers3.DataSource = LocalData.GetIstochniki()
                .Select(i => new
                {
                    Istock = i.Istock,
                    M11 = i.M11,
                    M10 = i.M10,
                    M9 = i.M9,
                    M8 = i.M8,
                    M7 = i.M7,
                    M6 = i.M6,
                    M5 = i.M5,
                    M4 = i.M4,
                    M3 = i.M3,
                    M2 = i.M2,
                    M1 = i.M1
                }).ToList();
            rptMarkers3.DataBind();
        }

        /// <summary>Возвращает список фильтров (filter/filter2/filter3) для конкретного объекта и месяца.</summary>
        private static List<object> GetFiltersForObject(
            List<LocalData.DocRowRecord> docs, string objectName, string month)
        {
            if (string.IsNullOrEmpty(objectName)) return new List<object>();

            var filtered = docs.Where(d => d.NameObject == objectName && d.DataReg.Contains(month)).ToList();

            var values = new List<string>();
            values.AddRange(filtered.Where(d => !string.IsNullOrEmpty(d.Filter1)).Select(d => d.Filter1));
            values.AddRange(filtered.Where(d => !string.IsNullOrEmpty(d.Filter2)).Select(d => d.Filter2));
            values.AddRange(filtered.Where(d => !string.IsNullOrEmpty(d.Filter3)).Select(d => d.Filter3));

            return values
                .GroupBy(v => v)
                .Select(g => (object)new { FilterValue = g.Key, Count = g.Count() })
                .OrderByDescending(x => ((dynamic)x).Count)
                .ToList();
        }

        /// <summary>Сравнение обращений по двум последним месяцам (увеличение/уменьшение).</summary>
        private static List<object> GetComparison(
            List<LocalData.DocRowRecord> docs, string month, bool increase)
        {
            var today = DateTime.Today;
            string m1 = today.AddMonths(-1).ToString("yyyy-MM-");
            string m2 = today.AddMonths(-2).ToString("yyyy-MM-");

            var filters = new List<string>();
            filters.AddRange(docs.Where(d => !string.IsNullOrEmpty(d.Filter1)).Select(d => d.Filter1));
            filters.AddRange(docs.Where(d => !string.IsNullOrEmpty(d.Filter2)).Select(d => d.Filter2));
            filters.AddRange(docs.Where(d => !string.IsNullOrEmpty(d.Filter3)).Select(d => d.Filter3));

            var result = filters.Distinct()
                .Select(f => new
                {
                    FilterValue = f,
                    Total = docs.Count(d => d.Filter1 == f || d.Filter2 == f || d.Filter3 == f),
                    PrevMonth = docs.Count(d => (d.Filter1 == f || d.Filter2 == f || d.Filter3 == f) && d.DataReg.Contains(m2)),
                    CurrentMonth = docs.Count(d => (d.Filter1 == f || d.Filter2 == f || d.Filter3 == f) && d.DataReg.Contains(m1))
                })
                .Where(x => increase ? x.CurrentMonth > x.PrevMonth : x.CurrentMonth <= x.PrevMonth)
                .OrderByDescending(x => x.Total)
                .Select(x => (object)x)
                .ToList();

            return result;
        }
        // =====================================================================
        // GRIDVIEW'Ы
        // =====================================================================

        private void BindGridViews()
        {
            string month = DropDownList_Month?.SelectedValue ?? "-01-";
            var docs = LocalData.GetDocRows();

            // GridView1 — статистика обращений (по объектам)
            var stats = docs
                .Where(d => !string.IsNullOrEmpty(d.NameObject) && d.DataReg.Contains(month))
                .GroupBy(d => d.NameObject)
                .Select(g => new
                {
                    NameObject = g.Key,
                    Count = g.Count(),
                    Percent = docs.Count(d => d.DataReg.Contains(month)) > 0
                        ? Math.Round((double)g.Count() / docs.Count(d => d.DataReg.Contains(month)) * 100, 2) + "%"
                        : "0%"
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            GridView1.DataSource = stats;
            GridView1.DataBind();

            int totalCount = docs.Count(d => !string.IsNullOrEmpty(d.NameObject) && d.DataReg.Contains(month));
            if (totalCount > 0 && !CheckBox_redact.Checked)
            {
                Label_itogo.Visible = true;
                Label_kol_vo.Text = totalCount.ToString();
                Label_kol_vo.Visible = true;
                Panel_statistic.Visible = true;
                Panel_text.Visible = true;
            }
            else
            {
                Label_itogo.Visible = false;
                Label_kol_vo.Visible = false;
                Panel_statistic.Visible = false;
            }

            // GridView2 — топ-3 объектов (для отображения таблицей)
            var top3 = stats.Take(4).ToList();
            GridView2.DataSource = top3;
            GridView2.DataBind();

            // Panel_Top1..4 — заголовки и счётчики
            SetTopPanel(Panel_Top1, Label_Top1, top3.ElementAtOrDefault(0)?.NameObject, top3.ElementAtOrDefault(1)?.Count);
            SetTopPanel(Panel_Top2, Label_Top2, top3.ElementAtOrDefault(1)?.NameObject, top3.ElementAtOrDefault(2)?.Count);
            SetTopPanel(Panel_Top3, Label_Top3, top3.ElementAtOrDefault(2)?.NameObject, top3.ElementAtOrDefault(3)?.Count);
            SetTopPanel(Panel_Top4, Label_Top4, top3.ElementAtOrDefault(3)?.NameObject, null);

            // GridView_dynamics — источники
            GridView_dynamics.DataSource = LocalData.GetIstochniki();
            GridView_dynamics.DataBind();

            // GridView_Redakt и GridView4_NoRedakt — редактируемые записи
            var infographics = LocalData.GetInfographics()
                .Where(x => (x.DataReg ?? "").Contains(month))
                .ToList();

            GridView_Redakt.DataSource = infographics;
            GridView_Redakt.DataBind();
            GridView4_NoRedakt.DataSource = infographics;
            GridView4_NoRedakt.DataBind();

            // GridView_Spravka_* — текстовая справка
            var spravka = LocalData.GetSpravka()
                .Where(x => (x.DataReg ?? "").Contains(month))
                .ToList();

            GridView_Spravka_Redakt.DataSource = spravka;
            GridView_Spravka_Redakt.DataBind();
            GridView_Spravka_Noredakt.DataSource = spravka;
            GridView_Spravka_Noredakt.DataBind();

            // Текстовая справка: топ-3 фильтров
            BindSpravkaTop3(docs, month);
        }

        private static void SetTopPanel(Panel panel, Label label, string name, int? count)
        {
            if (string.IsNullOrEmpty(name))
            {
                panel.Visible = false;
                label.Visible = false;
                return;
            }
            panel.Visible = true;
            label.Text = name;
            label.Visible = true;
        }

        private void BindSpravkaTop3(List<LocalData.DocRowRecord> docs, string month)
        {
            var filters = new List<string>();
            filters.AddRange(docs.Where(d => !string.IsNullOrEmpty(d.Filter1) && d.DataReg.Contains(month)).Select(d => d.Filter1));
            filters.AddRange(docs.Where(d => !string.IsNullOrEmpty(d.Filter2) && d.DataReg.Contains(month)).Select(d => d.Filter2));
            filters.AddRange(docs.Where(d => !string.IsNullOrEmpty(d.Filter3) && d.DataReg.Contains(month)).Select(d => d.Filter3));

            var top3 = filters
                .GroupBy(f => f)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList();

            var labels = new[] { Label_Name1_obrasch, Label_Name2_obrasch, Label_Name3_obrasch };
            var counts = new[] { Label_Count1_obrasch, Label_Count2_obrasch, Label_Count3_obrasch };

            for (int i = 0; i < 3; i++)
            {
                if (i < top3.Count)
                {
                    labels[i].Text = top3[i].Name;
                    labels[i].Visible = true;
                    counts[i].Text = top3[i].Count + " ОБРАЩЕНИЙ";
                    counts[i].Visible = true;
                }
                else
                {
                    labels[i].Visible = false;
                    counts[i].Visible = false;
                }
            }
        }

        // =====================================================================
        // ОБРАБОТЧИКИ ФИЛЬТРОВ
        // =====================================================================

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHeaders();
            LoadAllRepeaters();
            BindGridViews();
        }

        protected void Btn_poisk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox_Ot.Text)) LabelData1.Text = TextBox_Ot.Text;
            if (!string.IsNullOrEmpty(TextBox_Do.Text)) LabelData2.Text = TextBox_Do.Text;
            LoadAllRepeaters();
        }

        protected void Btn_update_Click(object sender, EventArgs e)
        {
            Response.Redirect("Infographics.aspx");
        }

        // =====================================================================
        // РЕДАКТИРОВАНИЕ: GridView_Redakt
        // =====================================================================

        protected void GridView_Redakt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                LocalData.AddInfographics(new LocalData.InfographicsRecord
                {
                    DataReg = Label_date_sootnosh.Text,
                    TypeObr = "",
                    ObjectName = "",
                    Vopros = "",
                    Zayavitel = "",
                    Rezultat = ""
                });
                BindGridViews();
            }
        }

        protected void GridView_Redakt_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;
            var list = LocalData.GetInfographics().ToList();
            if (index >= 0 && index < list.Count)
            {
                LocalData.DeleteInfographics(list[index].Id);
                BindGridViews();
            }
        }

        protected void GridView_Redakt_RowCreated(object sender, GridViewRowEventArgs e) { /* обработка не требуется */ }
        protected void GridView_Redakt_RowDataBound(object sender, GridViewRowEventArgs e) { /* обработка не требуется */ }

        protected void Button_update_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView_Redakt.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow) continue;
                int id = int.Parse(((Label)row.Cells[0].Controls[0]).Text);
                var record = new LocalData.InfographicsRecord
                {
                    Id = id,
                    TypeObr = GetCellText(row, 1),
                    ObjectName = GetCellText(row, 2),
                    Vopros = GetCellText(row, 3),
                    Zayavitel = GetCellText(row, 4),
                    Rezultat = GetCellText(row, 5)
                };
                LocalData.UpdateInfographics(record);
            }
            BindGridViews();
        }

        private static string GetCellText(GridViewRow row, int index)
        {
            return index < row.Cells.Count ? row.Cells[index].Text : "";
        }

        // =====================================================================
        // РЕДАКТИРОВАНИЕ: GridView_Spravka_Redakt
        // =====================================================================

        protected void GridView_Spravka_Redakt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew2")
            {
                LocalData.AddSpravka(new LocalData.SpravkaRecord
                {
                    DataReg = Label_date_sootnosh.Text,
                    TextSpravki = ""
                });
                BindGridViews();
            }
        }

        protected void GridView_Spravka_Redakt_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;
            var list = LocalData.GetSpravka().ToList();
            if (index >= 0 && index < list.Count)
            {
                LocalData.DeleteSpravka(list[index].Id);
                BindGridViews();
            }
        }

        protected void GridView_Spravka_Redakt_RowCreated(object sender, GridViewRowEventArgs e) { }
        protected void GridView_Spravka_Redakt_RowDataBound(object sender, GridViewRowEventArgs e) { }

        protected void Button_update3_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView_Spravka_Redakt.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow) continue;
                int id = int.Parse(((Label)row.Cells[0].Controls[0]).Text);
                LocalData.UpdateSpravka(new LocalData.SpravkaRecord
                {
                    Id = id,
                    TextSpravki = GetCellText(row, 1)
                });
            }
            BindGridViews();
        }

        // =====================================================================
        // РЕДАКТИРОВАНИЕ: GridView_dynamics
        // =====================================================================

        protected void GridView_dynamics_RowCreated(object sender, GridViewRowEventArgs e) { }
        protected void GridView_dynamics_RowDataBound(object sender, GridViewRowEventArgs e) { }

        protected void Button_update2_Click(object sender, EventArgs e)
        {
            // В локальной версии — просто перепривязываем
            BindGridViews();
        }

        // =====================================================================
        // GridView1: Hide / Show
        // =====================================================================

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Hide")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                    if (i > 5) GridView1.Rows[i].Visible = false;
            }
            if (e.CommandName == "Show")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                    if (i > 5) GridView1.Rows[i].Visible = true;
            }
        }

        protected void CheckBox_redact_CheckedChanged(object sender, EventArgs e)
        {
            ApplyRedactVisibility();
            BindGridViews();
        }
    }
}