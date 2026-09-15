using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
    public partial class Mosedo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Видимость панелей навигации в зависимости от выбранного раздела
            string section = Session["section"] as string;

            if (section == "dosie") Panel_dosie.Visible = true;
            else if (section == "doc") Panel_doc.Visible = true;
            else Panel_stat.Visible = true;

            TextSearchMain.Attributes.Add("onkeypress",
                "return clickButton(event,'" + Button_TextSearchMain.ClientID + "')");
            ScriptManager1.RegisterAsyncPostBackControl(GridViewSearchMain);

            Label2.Text = DateTime.Now.Year.ToString();

            // Показываем пункт "Пользователи" только для админа
            string username = Session["username"] as string;
            if (username == "admin" || username == "admin2")
            {
                Userss_Menu.Visible = true;
            }
            else if (username == "admin2")
            {
                Userss_Menu.Visible = false;
            }

            Label1.Text = username ?? "Гость";
        }

        // =====================================================================
        // ВЫХОД
        // =====================================================================

        protected void Button_vihod_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Authorization.aspx");
        }

        // =====================================================================
        // СКВОЗНОЙ ПОИСК
        // =====================================================================

        protected void TextSearchMain_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextSearchMain.Text))
            {
                string url = HttpContext.Current.Request.Url.AbsolutePath;

                GridViewSearchMain.Visible = true;
                BindSearchResults(url == "/Documents.aspx");

                Div_search.Style.Add("box-shadow", "0px 0px 4px 3px rgba(0, 0, 0, 0.2)");
            }
            else
            {
                Div_search.Style.Add("box-shadow", "unset");
                GridViewSearchMain.Visible = false;
            }
        }

        /// <summary>
        /// Заполняет GridViewSearchMain через LocalData.
        /// Если isDocuments == true — ищем по документам, иначе — по объектам.
        /// </summary>
        private void BindSearchResults(bool isDocuments)
        {
            string search = TextSearchMain.Text.Trim();

            if (isDocuments)
            {
                var docs = LocalData.GetDocuments()
                    .Where(d => Contains(d.Doc, search))
                    .Select(d => new { Doc = d.Doc })
                    .ToList();

                GridViewSearchMain.DataSource = docs;
            }
            else
            {
                var objects = LocalData.GetObjects()
                    .Where(o => o.Postuplenie != "ПЕРЕДАЧА" &&
                        (Contains(o.NameObject, search) ||
                         Contains(o.Category, search) ||
                         Contains(o.Adress, search) ||
                         Contains(o.SportsZone, search) ||
                         Contains(o.Supervisor, search) ||
                         Contains(o.Raion, search) ||
                         Contains(o.Tip, search) ||
                         Contains(o.District, search)))
                    .Select(o => new { NameObject = o.NameObject })
                    .ToList();

                GridViewSearchMain.DataSource = objects;
            }

            GridViewSearchMain.DataBind();
        }

        private static bool Contains(string source, string search) =>
            !string.IsNullOrEmpty(source) && source.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

        protected void GridViewSearchMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            string url = HttpContext.Current.Request.Url.AbsolutePath;

            if (url == "/Documents.aspx")
            {
                e.Row.Cells[0].Attributes["OnClick"] =
                    Page.ClientScript.GetPostBackClientHyperlink(this.GridViewSearchMain, "Select$" + e.Row.RowIndex);
            }
            else
            {
                e.Row.Attributes["onclick"] = "javascript:ClickRow2(this)";
            }
        }

        protected void GridViewSearchMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Search_Master2"] = GridViewSearchMain.SelectedRow.Cells[0].Text;
            Response.Redirect("Documents.aspx");
        }

        protected void Button_TextSearchMain_Click(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;

            Session["Search_Master2"] = TextSearchMain.Text;
            Session["Search_Master"] = TextSearchMain.Text;

            Response.Redirect(url == "/Documents.aspx" ? "Documents.aspx" : "Default.aspx");
        }

        // =====================================================================
        // КНОПКИ НАВИГАЦИИ (если где-то используются)
        // =====================================================================

        protected void Button_dosie_Click(object sender, EventArgs e) => Response.Redirect("Default.aspx");
        protected void Button_documents_Click(object sender, EventArgs e) => Response.Redirect("Documents.aspx");
        protected void Button_budget_Click(object sender, EventArgs e) => Response.Redirect("Budget.aspx");
    }
}