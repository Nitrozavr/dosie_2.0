using System;
using System.IO;
using System.Web.UI;

namespace Site
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var panel = (System.Web.UI.WebControls.Panel)Master.FindControl("Panel3");
            if (panel != null) panel.BackColor = System.Drawing.Color.FromArgb(248, 145, 59);

            TextMSG.Attributes.Add("onkeypress", "return clickButton(event,'" + Otpravit.ClientID + "')");
            FIOText.Attributes.Add("onkeypress", "return clickButton(event,'" + Otpravit.ClientID + "')");
            NumberText.Attributes.Add("onkeypress", "return clickButton(event,'" + Otpravit.ClientID + "')");
            DoljnostText.Attributes.Add("onkeypress", "return clickButton(event,'" + Otpravit.ClientID + "')");

            if (!IsPostBack)
            {
                // Автозаполнение из LocalData по логину
                var login = Session["login"] as string;
                if (string.IsNullOrEmpty(login)) return;

                var user = LocalData.GetUsers().Find(u =>
                    string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    FIOText.Text = user.FIO;
                    DoljnostText.Text = "";
                    NumberText.Text = "";
                }
            }
        }

        protected void Otpravit_Click(object sender, EventArgs e)
        {
            // === Валидация ===
            if (string.IsNullOrWhiteSpace(FIOText.Text) ||
                string.IsNullOrWhiteSpace(NumberText.Text) ||
                NumberText.Text.Trim() == "+7()--" ||
                string.IsNullOrWhiteSpace(DoljnostText.Text) ||
                string.IsNullOrWhiteSpace(TextMSG.Text) ||
                string.IsNullOrWhiteSpace(TemaList.SelectedValue) ||
                string.IsNullOrWhiteSpace(Deistvie_list.SelectedValue) ||
                string.IsNullOrWhiteSpace(SpochnosList.SelectedValue))
            {
                ShowAlert("Все поля должны быть заполнены!");
                return;
            }

            // === Сохранение файла (если загружен) ===
            string savedFilePath = null;
            if (!string.IsNullOrEmpty(oFile.Value))
            {
                string folder = Server.MapPath("./Uploads/");
                string fileName = Path.GetFileName(oFile.PostedFile.FileName);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                savedFilePath = Path.Combine(folder, fileName);

                if (File.Exists(savedFilePath))
                {
                    ShowAlert("Файл с таким именем уже есть на сервере.");
                    return;
                }

                oFile.PostedFile.SaveAs(savedFilePath);
            }

            // === Заявка принята ===
            // Здесь можно:
            //  - сохранить в List<Application> (в LocalData)
            //  - записать в лог-файл
            //  - отправить email (если SEND_EMAIL = true)

            ShowAlert("Заявка сохранена! (демо-режим)");

            // === Очистка формы ===
            TextMSG.Text = "";
            TemaList.ClearSelection();
            Deistvie_list.ClearSelection();
            SpochnosList.ClearSelection();
        }

        private void ShowAlert(string message)
        {
            string safe = message.Replace("'", "\\'").Replace("\r", "").Replace("\n", "");
            Response.Write($"<script>window.alert('{safe}');</script>");
        }
    }
}