using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Data;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Collections;

namespace Site
{
    public partial class Authorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Password.Attributes.Add("onkeypress", "return clickButton(event,'" + ButtonVhod.ClientID + "')");
            UserName.Focus();
        }

        //protected void LoginButton_Click(object sender, EventArgs e)
        //{
        //    MySqlConnection connection = new MySqlConnection("persistsecurityinfo=True;server=localhost;user id=root;password=Di58$KaY;database=mossportobject");

        //    DataTable dt = new DataTable();

        //    MySqlDataAdapter da = new MySqlDataAdapter();
        //    MySqlCommand command = new MySqlCommand("SELECT * FROM authorization WHERE login = @uL AND password = @uP", connection);
        //    connection.Open();
        //    command.Parameters.AddWithValue("@uL", UserName.Text);
        //    command.Parameters.AddWithValue("@uP", Password.Text);
        //    da.SelectCommand = command;
        //    da.Fill(dt);
        //    connection.Close();

        //    if ((UserName.Text.Trim() != "") & (Password.Text.Trim() != ""))
        //    {
        //        Page.Validate();
        //        if (!Page.IsValid) return;
        //        {
        //            if (UserName.Text == "admin")
        //            {
        //                if (FormsAuthentication.Authenticate(UserName.Text, Password.Text))
        //                {
        //                    Session["username"] = UserName.Text.Trim();
        //                    Session["login"] = UserName.Text.Trim();
        //                    FormsAuthentication.RedirectFromLoginPage(UserName.Text.Trim(), false);

        //                }
        //                else
        //                {
        //                    Response.Write("<script language=javascript>alert('Проверьте правильность логина и пароля.');</script>");
        //                };

        //            }
        //            else if (dt.Rows.Count == 1)
        //            {

        //                Session["username"] = UserName.Text.Trim();
        //                Session["login"] = UserName.Text.Trim();
        //                FormsAuthentication.RedirectFromLoginPage(UserName.Text.Trim(), false);

        //                //привести значение сессии к нужному виду 
        //                string FIO = "";

        //                if (Convert.ToString(Session["username"]) != FIO)
        //                {
        //                    MySqlConnection conn = new MySqlConnection("persistsecurityinfo=True;server=localhost;user id=root;password=Di58$KaY;database=mossportobject");

        //                    DataTable DT = new DataTable();

        //                    MySqlDataAdapter DA = new MySqlDataAdapter();
        //                    MySqlCommand comm = new MySqlCommand("SELECT familia,name,otchestvo FROM authorization WHERE login = @uL", conn);
        //                    conn.Open();
        //                    comm.Parameters.AddWithValue("@uL", Session["username"]);
        //                    DA.SelectCommand = comm;
        //                    DA.Fill(DT);
        //                    conn.Close();

        //                    string familia = Convert.ToString(dt.Rows[0][2]).Trim();
        //                    string name = Convert.ToString(dt.Rows[0][1]).Trim();
        //                    string otchestvo = Convert.ToString(dt.Rows[0][3]).Trim();

        //                    FIO = familia + " " + name[0] + "." + otchestvo[0] + ".";


        //                    Session["username"] = FIO;

        //                }
        //            }
        //            else
        //            {
        //                Response.Write("<script language=javascript>alert('Проверьте правильность логина и пароля.');</script>");
        //            };
        //        };
        //    }
        //    else
        //    {
        //        Response.Write("<script language=javascript>alert('Все поля обязательный к заполнению.');</script>");
        //    };


        //}

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("persistsecurityinfo=True;server=localhost;user id=root;password=Di58$KaY;database=mossportobject");

            DataTable dt = new DataTable();

            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM authorization2 WHERE login = @uL AND password = @uP", connection);
            connection.Open();
            command.Parameters.AddWithValue("@uL", UserName.Text);
            command.Parameters.AddWithValue("@uP", Password.Text);
            da.SelectCommand = command;
            da.Fill(dt);
            connection.Close();

            if ((UserName.Text.Trim() != "") & (Password.Text.Trim() != ""))
            {
                Page.Validate();
                if (!Page.IsValid) return;
                {
                    if (UserName.Text == "admin")
                    {
                        if (FormsAuthentication.Authenticate(UserName.Text, Password.Text))
                        {
                            Session["username"] = UserName.Text.Trim();
                            Session["login"] = UserName.Text.Trim();
                            FormsAuthentication.RedirectFromLoginPage(UserName.Text.Trim(), false);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "text", "sw();", true);
                        };

                    }
                    else if (dt.Rows.Count == 1)
                    {

                        Session["username"] = UserName.Text.Trim();
                        Session["login"] = UserName.Text.Trim();
                        FormsAuthentication.RedirectFromLoginPage(UserName.Text.Trim(), false);

                        //привести значение сессии к нужному виду 
                        string FIO = "";

                        if (Convert.ToString(Session["username"]) != FIO)
                        {
                            MySqlConnection conn = new MySqlConnection("persistsecurityinfo=True;server=localhost;user id=root;password=Di58$KaY;database=mossportobject");

                            DataTable DT = new DataTable();

                            MySqlDataAdapter DA = new MySqlDataAdapter();
                            MySqlCommand comm = new MySqlCommand("SELECT familia,name,otchestvo FROM authorization2 WHERE login = @uL", conn);
                            conn.Open();
                            comm.Parameters.AddWithValue("@uL", Session["username"]);
                            DA.SelectCommand = comm;
                            DA.Fill(DT);
                            conn.Close();

                            string familia = Convert.ToString(dt.Rows[0][2]).Trim();
                            string name = Convert.ToString(dt.Rows[0][1]).Trim();
                            string otchestvo = Convert.ToString(dt.Rows[0][3]).Trim();

                            FIO = familia + " " + name[0] + "." + otchestvo[0] + ".";


                            Session["username"] = FIO;

                        }
                    }
                    else
                    {
                        //Response.Write("<script language=javascript>alert('Проверьте правильность логина и пароля.');</script>");

                        //Код, который вытаскивает все данные о пользавтели из AD, которые может найти
                       
                        //DirectoryEntry Dir_Client = new DirectoryEntry(@"LDAP://mso-srv-dc0.DOM.MOS.SPORT", UserName.Text, Password.Text);
                        //DirectorySearcher search = new DirectorySearcher(Dir_Client);
                        //search.Filter = $"(&(objectClass=user)(sAMAccountName={UserName.Text}))";
                        //search.SearchScope = System.DirectoryServices.SearchScope.Subtree;

                        //foreach (SearchResult result in search.FindAll())
                        //{
                        //    foreach (DictionaryEntry property in result.Properties)
                        //    {

                        //        foreach (var val in (property.Value as ResultPropertyValueCollection))
                        //        {
                        //            MessageBox.Show(property.Key.ToString() + " : " + val.ToString());
                        //        }
                        //    }
                        //}
                        //Где property.Key.ToString() - ключ для обращения к свойству, а val.ToString() - значение этого свойства

                        //Код, который достаёт конкретные свойства
                        DirectoryEntry Dir_Client = new DirectoryEntry(@"LDAP://mso-srv-dc0.DOM.MOS.SPORT", UserName.Text, Password.Text);
                        DirectorySearcher search = new DirectorySearcher(Dir_Client);
                        search.Filter = $"(&(objectClass=user)(sAMAccountName={UserName.Text}))";
                        search.PropertiesToLoad.Add("givenName"); // Имя
                        search.PropertiesToLoad.Add("sn"); // Фамилия
                        search.PropertiesToLoad.Add("ipphone"); // Номер на рабочем месте (4 цифры)
                        search.PropertiesToLoad.Add("department"); // Отдел
                        search.PropertiesToLoad.Add("description"); // Должность
                        search.PropertiesToLoad.Add("mail"); // Адресс почты
                        search.PropertiesToLoad.Add("name"); // ФИО
                        search.PropertiesToLoad.Add("cn"); // ФИО
                        search.PropertiesToLoad.Add("company"); // Учереждение
                        search.PropertiesToLoad.Add("mobile"); // Мобильный телефон
                        search.PropertyNamesOnly = true;

                        try
                        {
                            SearchResult objresult = search.FindOne();
                            if (objresult != null)
                            {
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["givenName"].Value != null ? (objresult.GetDirectoryEntry().Properties["givenName"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["sn"].Value != null ? (objresult.GetDirectoryEntry().Properties["sn"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["ipphone"].Value != null ? (objresult.GetDirectoryEntry().Properties["ipphone"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["department"].Value != null ? (objresult.GetDirectoryEntry().Properties["department"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["description"].Value != null ? (objresult.GetDirectoryEntry().Properties["description"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["mail"].Value != null ? (objresult.GetDirectoryEntry().Properties["mail"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["name"].Value != null ? (objresult.GetDirectoryEntry().Properties["name"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["cn"].Value != null ? (objresult.GetDirectoryEntry().Properties["cn"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["company"].Value != null ? (objresult.GetDirectoryEntry().Properties["company"].Value).ToString() : "NoN");
                                //MessageBox.Show(objresult.GetDirectoryEntry().Properties["mobile"].Value != null ? (objresult.GetDirectoryEntry().Properties["mobile"].Value).ToString() : "NoN");

                                MySqlConnection conn = new MySqlConnection("persistsecurityinfo=True;server=localhost;user id=root;password=Di58$KaY;database=mossportobject");
                                conn.Open();
                                
                                MySqlCommand comm1 = new MySqlCommand("INSERT INTO authorization2 (name, familia, otchestvo, otdel, doljnost, login, password, number) VALUES (@name, @familia, @otchestvo, @otdel, @doljnost, @login, @password, @number)", conn);
                                comm1.Parameters.AddWithValue("@name", objresult.GetDirectoryEntry().Properties["givenName"].Value != null ? (objresult.GetDirectoryEntry().Properties["givenName"].Value).ToString() : "NoN");
                                comm1.Parameters.AddWithValue("@familia", objresult.GetDirectoryEntry().Properties["sn"].Value != null ? (objresult.GetDirectoryEntry().Properties["sn"].Value).ToString() : "NoN");

                                string otch = objresult.GetDirectoryEntry().Properties["name"].Value != null ? (objresult.GetDirectoryEntry().Properties["name"].Value).ToString() : "NoN";
                                string lastWord = otch.Substring(otch.LastIndexOf(' ') + 1);

                                comm1.Parameters.AddWithValue("@otchestvo", lastWord);
                                comm1.Parameters.AddWithValue("@otdel", objresult.GetDirectoryEntry().Properties["department"].Value != null ? (objresult.GetDirectoryEntry().Properties["department"].Value).ToString() : "NoN");
                                comm1.Parameters.AddWithValue("@doljnost", objresult.GetDirectoryEntry().Properties["description"].Value != null ? (objresult.GetDirectoryEntry().Properties["description"].Value).ToString() : "NoN");
                                comm1.Parameters.AddWithValue("@login", UserName.Text);
                                comm1.Parameters.AddWithValue("@password", Password.Text);
                                comm1.Parameters.AddWithValue("@number", objresult.GetDirectoryEntry().Properties["mobile"].Value != null ? (objresult.GetDirectoryEntry().Properties["mobile"].Value).ToString() : "NoN");
                                comm1.ExecuteNonQuery();

                                DataTable DT = new DataTable();
                                MySqlDataAdapter DA = new MySqlDataAdapter();
                                MySqlCommand comm = new MySqlCommand("SELECT familia,name,otchestvo FROM authorization2 WHERE login = @uL", conn);
                                comm.Parameters.AddWithValue("@uL", UserName.Text);
                                DA.SelectCommand = comm;
                                DA.Fill(DT);
                                conn.Close();


                                string familia = Convert.ToString(DT.Rows[0][0]).Trim();
                                string name = Convert.ToString(DT.Rows[0][1]).Trim();
                                string otchestvo = Convert.ToString(DT.Rows[0][2]).Trim();

                                string FIO = familia + " " + name[0] + "." + otchestvo[0] + ".";

                                

                                Session["username"] = FIO;
                                Session["login"] = UserName.Text.Trim();

                                FormsAuthentication.RedirectFromLoginPage(UserName.Text.Trim(), false);
                            }
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "text", "sw();", true);
                        };

                    };
                };
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "text", "sw2();", true);
            };


        }

        
    }
}