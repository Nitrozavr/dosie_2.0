using System;
using System.Web.UI;

namespace Site
{
    public partial class RedaktObj : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Проверка доступа: только admin
            string username = Session["username"] as string;
            if (username != "admin" && username != "admin2" && username != "Администратор")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Подсветка панели на мастер-странице (если есть)
            var panel = Master.FindControl("RedaktObjj") as System.Web.UI.WebControls.Panel;
            if (panel != null)
                panel.BackColor = System.Drawing.Color.FromArgb(192, 37, 46);
        }
    }
}