using System;
using System.Web.UI;

namespace Site
{
    public partial class FlaseError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Страница статична — ничего не делаем.
        }

        protected void Button_homes_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтры и поиск перед возвратом
            Session["select_filters1"] = "%";
            Session["select_filters2"] = "%";
            Session["select_filters3"] = "%";
            Session["select_filters4"] = "%";
            Session["Search_Master"] = "";
            Session["Search_Master2"] = "";

            Response.Redirect("Documents.aspx");
        }
    }
}