<%@ Page Title="Пользователи" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="True" CodeBehind="Users.aspx.cs" Inherits="Site.Users" enableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type="checkbox"] { display: unset; }
        .th_table th:nth-child(2) { position: sticky; top: 0; left: 0; z-index: 1; background: white; }
        .row td:nth-child(2) { position: sticky; left: 0; z-index: 1; background: white; }
        .th_table { font-size: 13px; color: #E22B36; position: sticky; top: 0; border-bottom: 1px solid #E22B36; border-top: 1px solid #E22B36; background: white; }
        .th_table th { position: sticky; top: 0; }
        .tablebtn3 { margin: 0; }
        .dropbtn { font-size: 19px; padding: 4px 12px 5px 12px; cursor: pointer; border-radius: 5px; background: white; color: #606266; border: 1px solid #dcdfe6; font-family: GOTHAPROBOL; }
        .dropbtn:hover, .dropbtn:focus { cursor: pointer; }
        .dropdown { position: relative; display: flex; flex-direction: row; align-items: center; }
        .dropdown-content { display: none; position: relative; min-width: 160px; z-index: 1; transition-delay: .5s; }
        .show { display: block; }
        .chosen-container { margin: 15px 10px 15px 0; }
        .chosen-container-active.chosen-with-drop .chosen-single { border: 1px solid #E22B36; border-bottom-right-radius: 0; border-bottom-left-radius: 0; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content">
    <div class="a-table">

        <div class="find">
            <div class="selectsandim">
                <h3>ПОЛЬЗОВАТЕЛИ</h3>
            </div>
        </div>

        <asp:Panel ID="Panel1" runat="server" Visible="true">
            <div class="find">
                <div class="selectsandim">
                    <asp:Button ID="Button_save" runat="server" OnClick="Button_save_Click"
                        CssClass="tablebtn3" Text="Сохранить" />
                </div>
                <div class="rightfind">
                    <asp:TextBox ID="TextBoxSerch" CssClass="input2" Width="200px" runat="server"></asp:TextBox>
                    <button id="Poisk" runat="server" onserverclick="Poisk_ServerClick"
                        style="border: none; background: none; cursor: pointer;">
                        <svg class="searchsvg" style="display:block">
                            <use href="#svg-find"></use>
                        </svg>
                    </button>
                    <button id="Refresh" runat="server" onserverclick="Refresh_ServerClick"
                        style="border: none; background: none; cursor: pointer;">
                        <svg class="searchsvg sizesvg">
                            <use href="#svg-refresh"></use>
                        </svg>
                    </button>
                </div>
            </div>

            <div class="back2">
                <div class="test" style="overflow: scroll; max-width: 1170px; max-height: 800px">
                    <asp:UpdatePanel ID="UpdatePanel_users" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView_users" runat="server"
                                OnRowDataBound="GridView_users_RowDataBound"
                                OnRowCreated="GridView_users_RowCreated"
                                CssClass="reester_table2" GridLines="None"
                                EmptyDataText="По вашему запросу ничего не найдено."
                                AutoGenerateColumns="false" DataKeyNames="Id">
                                <HeaderStyle CssClass="th_table" Height="40px"></HeaderStyle>
                                <RowStyle CssClass="row" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" />
                                    <asp:BoundField DataField="Familia" HeaderText="Фамилия" />
                                    <asp:BoundField DataField="Name" HeaderText="Имя" />
                                    <asp:BoundField DataField="Otchestvo" HeaderText="Отчество" />
                                    <asp:BoundField DataField="Otdel" HeaderText="Отдел" />
                                    <asp:BoundField DataField="Doljnost" HeaderText="Должность" />
                                    <asp:BoundField DataField="Number" HeaderText="Номер" />
                                    <asp:TemplateField HeaderText="Ред. объектов"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Осн. информация"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Инж. системы"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Тех. документация"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Категорирование"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Прочие данные"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Спорт. зоны"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ред. спорт. зон"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Пользователи"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Админ блокнота"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Секретарь блокнота"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ред. реестра"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Просроч. поручения"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Редактор ОГ"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ред. др. объектов"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Реестр задач"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Система статус"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ред. статус"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Приём/передача"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Админ приёма"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                    <asp:BoundField DataField="KolAuth" HeaderText="Посещений" />
                                    <asp:BoundField DataField="DateAuth" HeaderText="Последнее посещение" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>