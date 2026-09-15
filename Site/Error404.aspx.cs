using System;
using System.Web.UI;

namespace Site
{
    public partial class Error404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ничего не делаем — страница статична.
        }

        protected void Button_home_Click(object sender, EventArgs e)
        {
            // Сбрасываем фильтры и поиск перед возвратом на главную
            Session["select_filters1"] = "%";
            Session["select_filters2"] = "%";
            Session["select_filters3"] = "%";
            Session["select_filters4"] = "%";
            Session["Search_Master"] = "";
            Session["Search_Master2"] = "";

            Response.Redirect("Default.aspx");
        }
    }
}