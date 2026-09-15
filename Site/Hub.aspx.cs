using System;
using System.Web.UI;

namespace Site
{
    public partial class Hub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Страница статична — ничего не делаем.
        }

        protected void Button_Dosie_Click(object sender, EventArgs e)
        {
            Session["section"] = "dosie";
            Response.Redirect("Default.aspx");
        }

        protected void Button_Doc_Click(object sender, EventArgs e)
        {
            Session["section"] = "doc";
            Response.Redirect("Documents.aspx");
        }

        protected void Button_Stat_Click(object sender, EventArgs e)
        {
            Session["section"] = "stat";
            Response.Redirect("Budget.aspx");
        }
    }
}