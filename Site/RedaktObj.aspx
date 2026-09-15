<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="RedaktObj.aspx.cs" Inherits="Site.RedaktObj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="a-table">
            <section class="finder">
                <h3>РЕДАКТОР ОБЪЕКТОВ</h3>
            </section>
            <div style="padding: 40px; text-align: center; font-size: 18px; color: #666;">
                <p>Редактор объектов доступен только при подключении к базе данных.</p>
                <p style="margin-top: 20px; font-size: 14px; color: #999;">
                    В локальной версии данные хранятся в памяти приложения.<br />
                    Для просмотра объектов используйте страницы «Карта» или «Список».
                </p>
                <p style="margin-top: 30px;">
                    <a href="Default.aspx" style="color: #f8913b; text-decoration: underline; margin-right: 20px;">Карта</a>
                    <a href="Contact.aspx" style="color: #f8913b; text-decoration: underline;">Список объектов</a>
                </p>
            </div>
        </div>
    </div>
</asp:Content>