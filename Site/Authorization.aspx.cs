using System;
using System.Web.UI;
using System.Web.Security;

namespace Site
{
    public partial class Authorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Password.Attributes.Add("onkeypress",
                "return clickButton(event,'" + ButtonVhod.ClientID + "')");
            UserName.Focus();

            if (Request.QueryString["client"] != null)
            {
                Session["client_url"] = Request.QueryString["client"];
                FormsAuthentication.SetAuthCookie(Session["client_url"].ToString(), false);
                Response.Redirect("ClientPage.aspx");
                return;
            }

            if (IsPostBack) return;

            Session["select_filters1"] = "%";
            Session["select_filters2"] = "%";
            Session["select_filters3"] = "%";
            Session["Search_Master"] = "";
            Session["Search_Master2"] = "";

            if (Session["id_user"] != null)
            {
                Session["username"] = Session["id_user"].ToString();
                Session["login"] = Session["id_user"].ToString();
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string login = UserName.Text.Trim();
            string password = Password.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ShowAlert("sw2();");
                return;
            }

            var user = LocalData.Authenticate(login, password);
            if (user != null)
            {
                Session["username"] = user.FIO;
                Session["login"] = user.Login;
                Session["id_user"] = user.Login;
                Session["is_admin"] = user.Admin;
                Session["can_budget"] = user.Budget;
                Session["can_redakt"] = user.Redakt;
                FormsAuthentication.RedirectFromLoginPage(user.Login, false);
            }
            else
            {
                ShowAlert("sw();");
            }
        }

        private void ShowAlert(string jsFunction)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", jsFunction, true);
        }
    }
}