using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Заполняем фильтры из LocalData
                LoadDistrictFilter();

                // Восстанавливаем ранее выбранные значения из Session
                var sessionDistrict = Session["select_filters1"] as string ?? "%";
                var sessionRaion = Session["select_filters2"] as string ?? "%";

                if (DropDownList1.Items.FindByValue(sessionDistrict) != null)
                    DropDownList1.SelectedValue = sessionDistrict;
                else
                    DropDownList1.SelectedValue = "%";

                LoadRaionFilter(DropDownList1.SelectedValue);

                if (DropDownList2.Items.FindByValue(sessionRaion) != null)
                    DropDownList2.SelectedValue = sessionRaion;
                else
                    DropDownList2.SelectedValue = "%";

                BindGrids();
            }

            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Spisok");
            if (panel != null)
                panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            TextSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + Btn_poisk.ClientID + "')");
        }

        // =====================================================================
        // ЗАПОЛНЕНИЕ ФИЛЬТРОВ
        // =====================================================================

        private void LoadDistrictFilter()
        {
            var districts = LocalData.GetObjects()
                .Select(o => o.District)
                .Where(d => !string.IsNullOrEmpty(d))
                .Distinct()
                .OrderBy(d => d)
                .Select(d => new { District = d })
                .ToList();

            DropDownList1.DataSource = districts;
            DropDownList1.DataTextField = "District";
            DropDownList1.DataValueField = "District";
            DropDownList1.DataBind();
        }

        private void LoadRaionFilter(string district)
        {
            var query = LocalData.GetObjects().AsEnumerable();

            if (district != "%")
                query = query.Where(o => o.District == district);

            var raions = query
                .Select(o => o.Raion)
                .Where(r => !string.IsNullOrEmpty(r))
                .Distinct()
                .OrderBy(r => r)
                .Select(r => new { Raion = r })
                .ToList();

            // Сохраняем текущее выбранное значение
            var current = DropDownList2.SelectedValue;

            DropDownList2.Items.Clear();
            DropDownList2.Items.Add(new ListItem("Все районы", "%"));
            DropDownList2.DataSource = raions;
            DropDownList2.DataTextField = "Raion";
            DropDownList2.DataValueField = "Raion";
            DropDownList2.DataBind();

            // Восстанавливаем, если возможно
            if (!string.IsNullOrEmpty(current) && DropDownList2.Items.FindByValue(current) != null)
                DropDownList2.SelectedValue = current;
        }

        // =====================================================================
        // ФИЛЬТРАЦИЯ ДАННЫХ
        // =====================================================================

        private void BindGrids()
        {
            var filtered = GetFilteredObjects();

            GridView1.DataSource = filtered.Select(o => new
            {
                District = o.District,
                NameObject = o.NameObject,
                Raion = o.Raion,
                Adress = o.Adress,
                Indexx = o.Indexx
            }).ToList();
            GridView1.DataBind();

            GridView2.DataSource = GridView1.DataSource;
            GridView2.DataBind();

            Button1.Visible = GridView1.Rows.Count > 0;
        }

        private List<LocalData.ObjectRecord> GetFilteredObjects()
        {
            var query = LocalData.GetObjects().AsEnumerable();

            // Фильтр по округу
            if (DropDownList1.SelectedValue != "%")
                query = query.Where(o => o.District == DropDownList1.SelectedValue);

            // Фильтр по району
            if (DropDownList2.SelectedValue != "%")
                query = query.Where(o => o.Raion == DropDownList2.SelectedValue);

            // Поиск по тексту
            var search = TextSearch.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o =>
                    Contains(o.District, search) ||
                    Contains(o.NameObject, search) ||
                    Contains(o.Raion, search) ||
                    Contains(o.Adress, search) ||
                    Contains(o.Indexx, search) ||
                    Contains(o.Category, search) ||
                    Contains(o.Supervisor, search) ||
                    Contains(o.NumberObject, search));
            }

            return query.ToList();
        }

        private static bool Contains(string source, string search)
        {
            return !string.IsNullOrEmpty(source) &&
                   source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // =====================================================================
        // ОБРАБОТЧИКИ СОБЫТИЙ
        // =====================================================================

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRaionFilter(DropDownList1.SelectedValue);
            Session["select_filters1"] = DropDownList1.SelectedValue;
            BindGrids();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["select_filters2"] = DropDownList2.SelectedValue;
            BindGrids();
        }

        protected void TextSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrids();
        }

        protected void Btn_update_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path);
        }

        // =====================================================================
        // ОТОБРАЖЕНИЕ: подсветка найденного текста, стили
        // =====================================================================

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Пусто — оставлено для совместимости
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var search = TextSearch.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.Text = Regex.Replace(
                        cell.Text.Replace("&quot;", "\u0022")
                                 .Replace("<span style = 'background-color:#FFFFFF; border-radius: 5px; color:#F8913B;'>", "")
                                 .Replace("</span>", ""),
                        Regex.Escape(search),
                        m => $"<span style = 'background-color:#FFFFFF; border-radius: 5px; color:#F8913B;'>{m.Value}</span>",
                        RegexOptions.IgnoreCase);
                }
            }

            e.Row.Attributes["onclick"] = "javascript:ClickRow(this)";
            e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
            e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control) { /* для экспорта */ }

        // =====================================================================
        // ЭКСПОРТ В EXCEL
        // =====================================================================

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Контакты.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView2.AllowPaging = false;

                if (GridView2.HeaderRow != null)
                {
                    GridView2.HeaderRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in GridView2.HeaderRow.Cells)
                        cell.BackColor = GridView2.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.BackColor = System.Drawing.Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.BackColor = (row.RowIndex % 2 == 0)
                            ? GridView2.AlternatingRowStyle.BackColor
                            : GridView2.RowStyle.BackColor;
                        cell.CssClass = "textmode";
                    }
                }

                GridView2.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

            GridView2.Visible = false;
        }
    }
}