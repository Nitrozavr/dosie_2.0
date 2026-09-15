<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Otchet2.aspx.cs" Inherits="Site.Otchet2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .selectric { border: 1px solid #adadad; margin-top: unset; margin-bottom: unset; }
    .selectric:hover { border: 1px solid #333333; }
    .selectric .button:after { border-top-color: #adadad; }
    .selectric .label { height: 47px; line-height: 47px; font-size: 13px; }
    .selectric .button { height: 47px; }
    .selectric-wrapper { padding: 0 0 0 10px; width: 100%; }
    .selectric-items { left: unset; }

    #UpdatePanel4 .buttons {
        background: #dadada; border: none; padding: 15px; margin-left: 10px;
        font-family: GOTHAPROMED; color: #333333; cursor: pointer; width: 100%;
    }
    #UpdatePanel4 .buttons:hover { background: #c1c1c1; }

    .chk input[type="checkbox"] + label {
        font-size: 13px; font-family: GOTHAPROMED; padding: 15px;
        color: #333333; background: #dadada; width: 330px; text-align: center; display: block;
    }
    .chk input[type="checkbox"]:checked + label { background: #c1c1c1; cursor: pointer; }
    .chk input[type="checkbox"]:hover + label { background: #c1c1c1; cursor: pointer; }

    .txtbox {
        background: #f3f3f3; height: 30px; border: none; padding: 10px 10px 10px 30px;
        font-family: GOTHAPROMED; color: #333333; transition: 0.7s ease-out;
        letter-spacing: 0.7px; width: 100%;
    }
    .txtbox:hover { background: #e8e8e8; }
    .txtbox:focus { outline: none; padding: 10px 10px 10px 30px; }

    .chklist { width: 100%; table-layout: fixed; }
    .chklist label { font-family: GOTHAPROREG; letter-spacing: 0.5px; cursor: pointer; }
    .chklist tr { cursor: pointer; }
    .chklist td { border-bottom: 1px solid #ADADAD; }
    .chklist input[type="checkbox"] + label { background: #f8f8f8; padding: 30px 20px; display: block; font-size: 12px; }
    .chklist input[type="checkbox"]:checked + label { background: white; color: #f8913b; padding: 30px 20px; display: block; font-size: 12px; }
    .chklist input[type="checkbox"]:hover + label { background: white; color: #f8913b; padding: 30px 20px; display: block; font-size: 12px; }

    .second_part { display: block; text-align: end; color: #c2c2bc; font-size: 12px; }

    td img { width: unset; height: unset; border-radius: unset; }

    .gv_main_row {
        box-shadow: 2px 1px 20px 0px rgba(34, 60, 80, 0.2); margin-top: 15px;
        margin-left: 15px; display: flex; flex-wrap: wrap; justify-content: space-between;
        padding: 15px; border-radius: 10px;
    }

    .name_object {
        background: #f8913b; color: white; font-family: MOSSPORT; text-transform: uppercase;
        font-size: 40px; margin-top: unset; padding: 20px; height: fit-content;
        max-width: 400px; cursor: pointer;
    }

    .chklist_category label { cursor: pointer; text-transform: uppercase; }
    .chklist_category tr { cursor: pointer; display: unset; text-align: center; padding: unset; }
    .chklist_category input[type="checkbox"] + label {
        background: white; padding: 20px 10px; height: 110px; display: flex; font-size: 10px;
        flex-direction: column; align-items: center; border: 1px solid #e5e5e5;
    }
    .chklist_category input[type="checkbox"]:checked + label {
        background: #1a364b; color: white; padding: 20px 10px; height: 110px; display: flex;
        font-size: 10px; flex-direction: column; align-items: center; border: 1px solid #e5e5e5;
    }
    .rightfind svg { width: 40px; }
    .chklist_category input[type="checkbox"]:checked + label svg { fill: white; }

    .header_inf {
        background: #1a364b; color: white; font-family: GOTHAPROMED; text-transform: uppercase;
        font-size: 14px; margin-top: unset; height: 30px; border-radius: unset !important;
        padding: 10px !important;
    }
    .header_inf span { padding-left: 10px; }
    .dv_in { height: 40px; }
    .dv_in td:nth-child(2) { text-align: end; }
    .inner_dv { table-layout: fixed; width: 100%; }

    .find { max-width: 1170px; }
    .selectsandim { width: 100%; max-width: 330px; }
    .rightfind { width: 100%; max-width: 840px; }
    .aspNetDisabled { opacity: 0.7; }
    .aspNetDisabled:hover { cursor: unset; background: #e5e5e5; }

    #UpdatePanel4 { width: 100%; max-width: 840px; display: flex; align-items: center; }

    .chklist_icon { width: 30px; height: unset; margin-bottom: 7px; border-radius: unset; }
    .sub_header { font-family: GOTHAPROMED; margin: 30px 0 0 0; display: block; }
    .photo_object img { width: 350px; height: 235px; border-radius: 10px; }

    #DetailsView_Sports_Zone td[onclick] {
        background: #f8f8f8; padding: 26px 20px; text-align: center; border: 1px solid #ADADAD;
    }
    #DetailsView_Sports_Zone td[onclick]:hover {
        background: white; color: #e22b36; padding: 26px 20px;
        text-align: center; cursor: pointer;
    }

    .cadastr { word-wrap: break-word; max-width: 390px; height: 200px; vertical-align: middle; }
    .morelink { border-bottom: 1px solid #e22b36; font-family: GOTHAPROMED; }

    @media (max-width: 800px) {
        .chklist_category tr { display: flex; flex-direction: column; }
        .gv_main_row { flex-direction: column; }
        .chklist_category input[type="checkbox"] + label { display: flex; align-items: center; }
        .chklist_category input[type="checkbox"]:checked + label { display: flex; align-items: center; }
        .rightfind { flex-direction: column; }
        .selectric-wrapper { padding: 15px 0; }
        .ddls { margin: 20px 0; }
        .selectsandim { max-width: unset; }
        #UpdatePanel4 { flex-direction: column; align-items: end; margin: 20px 0; }
        .buttons { margin: 15px 0; }
    }
</style>

<script>
    function myFunctionSMain(parameter) {
        __doPostBack('<%= TextBox_Search.ClientID %>', '');
    }
</script>

<script>
    function width() {
        var screenWidth = screen.width;
        if (screenWidth < 1170) {
            screenWidth = screenWidth - 30;
            $('div .border').css({ "width": screenWidth + "px" });
        }

        var ellipsestext = "...";
        var moretext = "Раскрыть";
        var lesstext = "Скрыть";

        $('#<%=GridView_objects.ClientID %> .cadastr').each(function () {
            var content = $(this).html();
            if (!(content.indexOf('moreelipses') !== -1) && content.length > 75
                && !(content.indexOf('select') !== -1) && !(content.indexOf('input') !== -1)
                && !(content.indexOf('a') !== -1)) {
                var c = content.substr(0, 75);
                var h = content.substr(75, content.length - 75);
                var html = c + '<span class="moreelipses">' + ellipsestext + '</span>'
                    + '<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;'
                    + '<a href="" class="morelink">' + moretext + '</a></span>';
                $(this).html(html);
            }
        });

        $(".morelink").click(function (e) {
            e.stopImmediatePropagation();
            if ($(this).hasClass("less")) {
                $(this).removeClass("less");
                $(this).html(moretext);
            } else {
                $(this).addClass("less");
                $(this).html(lesstext);
            }
            $(this).parent().prev().toggle();
            $(this).prev().toggle();
            return false;
        });
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="content">
    <div class="a-table">
        <section class="finder">
            <h3 style="font-weight: 500; font-size: 30px;">ОТЧЁТЫ</h3>
        </section>

        <%-- Поиск + фильтры --%>
        <div class="find">
            <div class="selectsandim">
                <div style="width: 100%;">
                    <asp:Image ID="Image_search" runat="server" ImageUrl="/Imges/search.png" Width="20" style="position: absolute; margin: 5px;" />
                    <asp:TextBox ID="TextBox_Search" onkeyup="myFunctionSMain()" CssClass="txtbox" runat="server" OnTextChanged="TextBox_Search_TextChanged"></asp:TextBox>
                </div>
            </div>
            <div class="rightfind ddls">
                <asp:DropDownList ID="DropDownList_District" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="District" DataValueField="District">
                    <asp:ListItem Value="%" Text="Все округа"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="DropDownList_Category" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Raion" DataValueField="Raion">
                    <asp:ListItem Value="%" Text="Все районы"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="DropDownList_Supervisor" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Supervisor" DataValueField="Supervisor">
                    <asp:ListItem Value="%" Text="Все руководители"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="DropDownList_Tip" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Tip" DataValueField="Tip">
                    <asp:ListItem Value="%" Text="Все типы"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <%-- Кнопка «Выбрать все» + кнопки скачивания --%>
        <div class="find" style="padding: 10px 0 20px 0;">
            <div class="selectsandim">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="CheckBox_choose" AutoPostBack="true" CssClass="chk" runat="server" Text="Выбрать все" OnCheckedChanged="CheckBox_choose_CheckedChanged" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CheckBox_choose" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <asp:UpdatePanel ID="UpdatePanel4" runat="server" ClientIDMode="Static">
                <ContentTemplate>
                    <asp:Button ID="Button_download" CssClass="buttons" runat="server" Text="Скачать отчет" Enabled="false" OnClick="Button_download_Click" />
                    <asp:Button ID="Button_unfold" CssClass="buttons" runat="server" Text="Развернуть все" Enabled="false" OnClick="Button_unfold_Click" />
                    <asp:Button ID="Button_collapse" CssClass="buttons" runat="server" Text="Свернуть все" Enabled="false" OnClick="Button_collapse_Click" />
                    <asp:Button ID="Button_download_vidim" CssClass="buttons" runat="server" Text="Скачать видимые данные" Enabled="false" OnClick="Button_download_vidim_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList_Objects" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="Button_download" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
                <%-- ============================================================ --%>
        <%-- ЛЕВАЯ ЧАСТЬ: СПИСОК ОБЪЕКТОВ --%>
        <%-- ============================================================ --%>
        <div class="find" style="border-top:1px solid #ADADAD; padding: unset; justify-content: flex-start;">
            <div class="selectsandim" style="max-height: 736px; overflow-y: scroll; align-items: unset;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:CheckBoxList ID="CheckBoxList_Objects" OnPreRender="CheckBoxList_Objects_DataBound"
                            AutoPostBack="true" CssClass="chklist"
                            DataTextField="JoinedField" DataValueField="JoinedField"
                            runat="server" OnSelectedIndexChanged="CheckBoxList_Objects_SelectedIndexChanged">
                        </asp:CheckBoxList>

                        <asp:Label ID="Label_Objects" Visible="false" runat="server" Text=""></asp:Label>

                        <%-- Скрытая таблица для скачивания отчёта --%>
                        <asp:GridView ID="GridView1" runat="server" CssClass="Tab_obj2" OnRowDataBound="GridView1_RowDataBound">
                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                            <RowStyle CssClass="row" Height="40px" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TextBox_Search" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="CheckBox_choose" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="DropDownList_District" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="DropDownList_Category" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="DropDownList_Supervisor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="DropDownList_Tip" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <%-- ============================================================ --%>
            <%-- ПРАВАЯ ЧАСТЬ: КАРТОЧКИ ОБЪЕКТОВ --%>
            <%-- ============================================================ --%>
            <div class="rightfind" style="align-items: unset; max-width: 840px;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView_objects" GridLines="None" AutoGenerateColumns="false"
                            ShowHeader="false" CssClass="gv_main" runat="server"
                            OnRowDataBound="GridView_objects_RowDataBound">
                            <RowStyle CssClass="gv_main_row" />
                            <Columns>

                                <%-- Название объекта --%>
                                <asp:BoundField DataField="NameObject" ItemStyle-CssClass="name_object" />

                                <%-- Фото объекта --%>
                                <asp:ImageField DataImageUrlField="UrlImage" ItemStyle-CssClass="photo_object"></asp:ImageField>

                                <%-- Чекбоксы разделов --%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBoxList ID="CheckBoxList_Select" CssClass="chklist_category"
                                            AutoPostBack="true" RepeatDirection="Horizontal" runat="server"
                                            OnSelectedIndexChanged="CheckBoxList_Select_SelectedIndexChanged">
                                            <asp:ListItem Value="DetailsView_Main_info" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 1100 1100'><path d='M688 226c0-2-1-3-2-5 0 0 0-1-1-1s-2-3-3-4L471 6c-1-1-2-2-4-3 0 0-1 0-1-1s-3-1-4-2H155c-28 0-51 23-51 51v690c0 28 23 51 51 51h483c28 0 51-23 51-51V230c0-2 0-3 0-4zm-60-14H476V61l152 151zm25 530c0 9-7 15-15 15H155c-9 0-15-7-15-15V52c0-9 7-15 15-15h286v193c0 10 8 17 17 17h193v495zm-81-185c0 10-8 17-17 17H235c-10 0-17-8-17-17s8-17 17-17h319c10-1 17 6 17 17zm0 105c0 10-8 17-17 17H235c-10 0-17-8-17-17s8-17 17-17h319c10 0 17 8 17 17zm0-210c0 10-8 17-17 17H235c-10 0-17-8-17-17s8-17 17-17h319c10 0 17 8 17 17zM235 328h319c10 0 17 8 17 17s-8 17-17 17H235c-10 0-17-8-17-17-1-8 7-17 17-17zm-18-87c0-10 8-17 17-17h110c10 0 17 8 17 17s-8 17-17 17H235c-10 1-18-7-18-17zm0-104c0-10 8-17 17-17h110c10 0 17 8 17 17s-8 17-17 17H235c-10 0-18-8-18-17z'/></svg> Основная информация"></asp:ListItem>
                                            <asp:ListItem Value="DetailsView_Buildings" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 60 60'><path d='M26 22h-4v-4h4v4zm0-10h-4v4h4v-4zm6 18h-4v4h4v-4zm0-18h-4v4h4v-4zm0 12h-4v4h4v-4zm0-18h-4v4h4V6zm0 12h-4v4h4v-4zM26 6h-4v4h4V6zm-6 12h-4v4h4v-4zm0 12h-4v4h4v-4zm0-6h-4v4h4v-4zM36 4v42h3v2H9v-2h3V4H9V2h6V0h19v2h3v2h-3zM27 41c0-2-1-3-3-3s-3 1-3 3v5h6v-5zm7-37H14v42h5v-5c0-3 2-5 5-5s5 2 5 5v5h5V4zM26 30h-4v4h4v-4zm0-6h-4v4h4v-4zm-6-12h-4v4h4v-4zm0-6h-4v4h4V6z'/></svg> Сведения о зданиях/сооружениях"></asp:ListItem>
                                            <asp:ListItem Value="DetailsView_Area" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 40 40'><path d='M16 3C8 3 3 9 3 16s6 13 13 13 13-6 13-13S23 3 16 3zm0 2c6 0 11 5 11 11s-5 11-11 11S5 22 5 16 10 5 16 5zm-1 5v2h2v-2h-2zm0 4v8h2v-8h-2z'/></svg> Сведения о земельном участке"></asp:ListItem>
                                            <asp:ListItem Value="DetailsView_Ing_Sys" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'><circle cx='44' cy='115' r='3'/><path d='M71 29c0-10-5-19-14-23l-1 0h-4v23c-6 4-13 4-19 0V5h-3l-1 0C22 10 17 19 17 29v2c0 0 0 1 0 1s0 0 0 1 0 1 0 1 0 0 0 1 0 0 0 1 0 1 0 1 0 1 0 1 0 0 0 1 0 1 0 1 0 1 0 1 0 1 0 1c1 3 3 5 5 7l1 1c4 4 6 14 6 21v44c0 6 5 11 11 11s11-5 11-11V71c0-7 3-17 6-21l1-1c2-2 4-5 5-7 0 0 0-1 1-1 0 0 0-1 0-1s0-1 0-1 0 0 0-1 0 0 0-1 0 0 0-1 0 0 0-1 0 0 0-1 0 0 0-1 0 0 0-1v0c0-1 0-1 0-2zM30 10v20l1 1c4 3 8 5 13 5s9-2 13-5l1-1V10c5 5 9 12 9 19 0 1 0 1 0 2v0c0 1 0 1 0 2v0c0 1 0 1 0 2v0c0 1 0 1 0 2v0c-1 3-3 5-5 7l-1 1c-4 4-10 6-16 6s-12-2-16-6l-1-1c-2-2-4-5-5-7v0c0-1 0-1-1-2v0c0-1 0-1 0-2v0c0-1 0-1 0-2v0c0-1 0-1 0-2 0-7 4-14 10-19zM51 71v44c0 4-3 7-7 7s-7-3-7-7V71c0-4-1-11-3-17 3 2 6 2 10 2s7-1 10-2c-2 6-3 13-3 17zM105 71l1-1V62H96V16c1 0 1-1 1-1 1-1 1-2 1-4L98 2h-8l-1 9c0 1 0 3 1 4 0 0 1 1 1 1v46H82v8l1 1c1 1 2 2 2 3s-1 3-2 3l-1 1v36c0 7 5 12 12 12s12-5 12-12V78l-1-1c-1-1-2-2-2-3s1-3 2-3zM94 6h0l1 5c0 0 0 0 0 1s0 0-1 0 0 0-1 0 0 0 0-1l1-5zM86 68V66h16v2c-2 1-3 4-3 6s1 5 3 6v32H86V80c2-1 3-4 3-6s-1-5-3-6zm8 54c-4 0-7-3-8-6h16c-1 3-4 6-8 6z'/></svg> Ключевой раздел 1"></asp:ListItem>
                                            <asp:ListItem Value="DetailsView_Documents" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 1100 1100'><path d='M688 226c0-2-1-3-2-5 0 0 0-1-1-1s-2-3-3-4L471 6c-1-1-2-2-4-3 0 0-1 0-1-1s-3-1-4-2H155c-28 0-51 23-51 51v690c0 28 23 51 51 51h483c28 0 51-23 51-51V230c0-2 0-3 0-4zm-60-14H476V61l152 151zm25 530c0 9-7 15-15 15H155c-9 0-15-7-15-15V52c0-9 7-15 15-15h286v193c0 10 8 17 17 17h193v495zm-81-185c0 10-8 17-17 17H235c-10 0-17-8-17-17s8-17 17-17h319c10-1 17 6 17 17zm0 105c0 10-8 17-17 17H235c-10 0-17-8-17-17s8-17 17-17h319c10 0 17 8 17 17zm0-210c0 10-8 17-17 17H235c-10 0-17-8-17-17s8-17 17-17h319c10 0 17 8 17 17zM235 328h319c10 0 17 8 17 17s-8 17-17 17H235c-10 0-17-8-17-17-1-8 7-17 17-17zm-18-87c0-10 8-17 17-17h110c10 0 17 8 17 17s-8 17-17 17H235c-10 1-18-7-18-17zm0-104c0-10 8-17 17-17h110c10 0 17 8 17 17s-8 17-17 17H235c-10 0-18-8-18-17z'/></svg> Документы"></asp:ListItem>
                                            <asp:ListItem Value="DetailsView_Other_Data" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 1100 1100'><path d='M688 226c0-2-1-3-2-5 0 0 0-1-1-1s-2-3-3-4L471 6c-1-1-2-2-4-3 0 0-1 0-1-1s-3-1-4-2H155c-28 0-51 23-51 51v690c0 28 23 51 51 51h483c28 0 51-23 51-51V230c0-2 0-3 0-4zm-60-14H476V61l152 151zm25 530c0 9-7 15-15 15H155c-9 0-15-7-15-15V52c0-9 7-15 15-15h286v193c0 10 8 17 17 17h193v495z'/></svg> Прочие данные" style="display:none"></asp:ListItem>
                                            <asp:ListItem Value="DetailsView_Sports_Zone" Text="<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 320 320'><path d='M280 188V57H9V241h271v-52zM266 174h-24v-51h24v51zm-128-7c-7-3-13-10-13-18s5-16 13-18v37zm14-37c7 3 13 10 13 18s-5 16-13 18v-37zm-128-7h24v51H24v-51zm0 66h38v-79H24V72h114v44c-15 3-27 17-27 33s11 30 27 33v44H24v-38zm128 37v-44c15-3 27-17 27-33s-11-30-27-33V70h114v38h-38v79h38v38H152v-1z'/></svg> Спортивные зоны" style="display:none"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- DetailsView'ы для разделов --%>
                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <%-- ОСНОВНАЯ ИНФОРМАЦИЯ --%>
                                        <asp:DetailsView ID="DetailsView_Main_info" GridLines="None" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Основная информация"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Адрес" DataField="Adress" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Округ" DataField="District" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Район" DataField="Raion" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Индекс" DataField="Indexx" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Статус" DataField="Statuss" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                        <%-- СВЕДЕНИЯ О ЗДАНИЯХ --%>
                                        <asp:DetailsView ID="DetailsView_Buildings" GridLines="None" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Сведения о зданиях/сооружениях"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Кадастровый номер здания" DataField="CadastralNumber" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Год постройки" DataField="ConstructionYear" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Этажность" DataField="Levels" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Дата ввода в эксплуатацию" DataField="CommissioningDate" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Площадь зданий и сооружений" DataField="SqureBuildings" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                        <%-- ЗЕМЕЛЬНЫЙ УЧАСТОК --%>
                                        <asp:DetailsView ID="DetailsView_Area" GridLines="None" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label3" runat="server" Text="Сведения о земельном участке"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Зеленые насаждения" DataField="GreenSpaces" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Площадь территорий" DataField="SqureTerritory" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Используемая территория" DataField="TerritoriesUsed" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                        <%-- ИНЖЕНЕРНЫЕ СИСТЕМЫ --%>
                                        <asp:DetailsView ID="DetailsView_Ing_Sys" GridLines="None" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Text="Инженерные системы"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Система электроснабжения" DataField="Electrosnab" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Система водоснабжения" DataField="Vodsnab" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="ОЗДС" DataField="OZDS" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                        <%-- ДОКУМЕНТЫ --%>
                                        <asp:DetailsView ID="DetailsView_Documents" GridLines="None" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label5" runat="server" Text="Документы"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Распоряжение / РДГИ" DataField="OrderOKS" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Акт приема-передачи" DataField="AktPriemStroi" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Выписка ЕГРН" DataField="ExtractOKS" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                        <%-- ПРОЧИЕ ДАННЫЕ --%>
                                        <asp:DetailsView ID="DetailsView_Other_Data" GridLines="None" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server" style="display:none">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label6" runat="server" Text="Прочие данные"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Площадь убираемых помещений" DataField="SqureCleaningBuildings" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Площадь убираемой территории" DataField="SqureCleaningTerritory" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Количество зон приема посетителей" DataField="KolZonePriema" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Количество аппаратов ККТ" DataField="KolAparKKT" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Количество крючков в гардеробе" DataField="KolKruchkov" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                                <asp:BoundField HeaderText="Наличие специальной техники" DataField="SpecTex" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                        <%-- СПОРТИВНЫЕ ЗОНЫ --%>
                                        <asp:DetailsView ID="DetailsView_Sports_Zone" GridLines="None" ClientIDMode="Static" AutoGenerateRows="false" Visible="false" CssClass="inner_dv" runat="server" style="display:none">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label7" runat="server" Text="Спортивные зоны"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="header_inf" />
                                            <Fields>
                                                <asp:BoundField HeaderText="Спортивные зоны" DataField="SportsZone" ConvertEmptyStringToNull="true" NullDisplayText="-" />
                                            </Fields>
                                            <RowStyle CssClass="dv_in" />
                                        </asp:DetailsView>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="CheckBoxList_Objects" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <asp:UpdateProgress ID="UpdateProgress4" runat="server">
                    <ProgressTemplate>
                        <div class="progress">
                            <asp:Image ID="Image1" src="Logo/preload.gif" Width="50px" Height="50px" runat="server" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>

    </div>
</div>

</asp:Content>