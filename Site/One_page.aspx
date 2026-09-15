<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="One_page.aspx.cs" Inherits="Site.One_page" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    #ContentPlaceHolder1_Otch { margin-top: 10px; }

    @media (max-width: 1040px) {
        #ContentPlaceHolder1_Otch { margin-top: 100px; }
    }

    .Tab_obj { margin-top: 0px; }
    .inf_otch { padding-top: 25px; }
    #ContentPlaceHolder1_Panel_tex td img { width: 35px; height: 35px; }
</style>

<div class="content">
    <div class="button-top">
        <svg display="none">
            <symbol id="svg-top" viewBox="0 0 612 612">
                <g>
                    <path d="M306,0C136.992,0,0,136.992,0,306s136.992,306,306,306s306-137.012,306-306S475.008,0,306,0z M469.251,392.751 c-7.478,7.478-19.584,7.478-27.043,0L306,256.543L169.811,392.731c-7.478,7.479-19.584,7.479-27.043,0 c-7.478-7.478-7.478-19.584,0-27.042l146.44-146.44c4.59-4.59,10.863-6.005,16.812-4.973c5.929-1.052,12.221,0.383,16.811,4.973 l146.44,146.44C476.71,373.167,476.71,385.273,469.251,392.751z"/>
                </g>
            </symbol>
        </svg>
        <svg class="topbtn"><use href="#svg-top"></use></svg>
    </div>

    <script>
        $(window).scroll(function () {
            var height = $(window).scrollTop();
            if (height > 300) { $('.topbtn').fadeIn(); } else { $('.topbtn').fadeOut(); }
        });
        $(document).ready(function () {
            $(".topbtn").click(function (event) {
                event.preventDefault();
                $("html, body").animate({ scrollTop: 0 }, "slow");
                return false;
            });
        });
    </script>

    <%-- ============================================================ --%>
    <%-- ЛЕВЫЙ СТОЛБЕЦ: КАРТОЧКА ОБЪЕКТА --%>
    <%-- ============================================================ --%>
    <asp:Panel ID="Danny" CssClass="a-table" runat="server">

        <div class="photogv">
            <asp:Label ID="Label1" runat="server" Text="Объект" style="font-size: 36px; color: white; letter-spacing: 1px;" CssClass="objecttext2"></asp:Label>
        </div>

        <div class="border" style="margin-top: 20px; max-width: 1170px; border-radius: 5px;"></div>

        <%-- Скрытый CheckBoxList со списком объектов (заполняется из LocalData) --%>
        <asp:CheckBoxList ID="Check_Object" AppendDataBoundItems="True" runat="server" AutoPostBack="True"
            style="display: none;" DataTextField="NameObject" DataValueField="NameObject"
            OnDataBound="Check_Object_DataBound"></asp:CheckBoxList>

        <asp:Panel ID="Vibor" CssClass="present" runat="server">

            <div class="showmap"></div>

            <%-- Скрипт галереи изображений (заполняется в Page_Load) --%>
            <script type="text/javascript">
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers" runat="server">
                            <ItemTemplate>
                                {
                                    "url_image": '<%# Eval("UrlImage") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];
                    for (let i = 0; i < markers.length; i++) {
                        $('.border').append($('<div>' +
                            '<a data-fancybox="gallery" href="' + markers[i].url_image + '">' +
                            '<img src="' + markers[i].url_image + '" width="600" height="400" style="border-radius: 5px; filter: brightness(60%);" >' +
                            '</a>' +
                            '</div>'));
                    }
                });
            </script>

            <script>
                $(document).ready(function () {
                    $('.border').slick({
                        speed: 300,
                        slidesToShow: 3,
                        slidesToScroll: 3,
                        responsive: [
                            { breakpoint: 1024, settings: { slidesToShow: 3, slidesToScroll: 3 } },
                            { breakpoint: 600, settings: { slidesToShow: 2, slidesToScroll: 2 } },
                            { breakpoint: 480, settings: { slidesToShow: 1, slidesToScroll: 1 } }
                        ]
                    });
                });
            </script>

            <script>
                function width() {
                    var screenWidth = screen.width;
                    if (screenWidth < 1170) {
                        screenWidth = screenWidth - 30;
                        $('div .border').css({ "width": screenWidth + "px" });
                    }

                    var ellipsestext = "...";
                    var moretext = "Читать";
                    var lesstext = "Скрыть";

                    $('#<%=Table_kat.ClientID %> .row td').each(function () {
                        var content = $(this).html();
                        if (!(content.indexOf('moreelipses') !== -1) && content.length > 35
                            && !(content.indexOf('select') !== -1) && !(content.indexOf('input') !== -1)
                            && !(content.indexOf('a') !== -1)) {
                            var c = content.substr(0, 35);
                            var h = content.substr(35, content.length - 35);
                            var html = c + '<span class="moreelipses">' + ellipsestext + '</span>' +
                                '<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;' +
                                '<a href="" class="morelink">' + moretext + '</a></span>';
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
        </asp:Panel>

        <%-- ============================================================ --%>
        <%-- БЛОК КНОПОК --%>
        <%-- ============================================================ --%>
        <div class="find">
            <div class="selectsandim" style="height: 50px">
                <asp:Button ID="Ves_otchet" runat="server" Text="Скачать отчет" CssClass="tablebtn3" style="margin: 15px 10px 0px 0" OnClick="Ves_otchet_Click" />
                <asp:Button ID="Razvernyt" runat="server" Text="Развернуть все" CssClass="tablebtn3" style="margin: 15px 10px 0px 0" OnClick="Razvernyt_Click" />
                <asp:Button ID="Svernyt" runat="server" Text="Свернуть все" CssClass="tablebtn3" style="margin: 15px 10px 0px 0" OnClick="Svernyt_Click" />
                <asp:Button ID="Vidim" runat="server" Text="Скачать видимые данные" CssClass="tablebtn3" style="margin: 15px 10px 0px 0; display: none" OnClick="Vidim_Click" Visible="false" />
            </div>

            <div class="rightfind">
                <asp:Button ID="OpenMaps" runat="server" Text="Показать на карте" CssClass="tablebtn3" OnClick="OpenMaps_Click" style="z-index: 1" />
                <asp:GridView runat="server" ShowHeader="False" GridLines="None" CssClass="downSpres" OnRowCreated="Dow_present_RowCreated">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="UrlDow" ControlStyle-CssClass="tablebtn3"
                            HeaderText="Скачать презентацию" Text="Скачать презентацию" Visible="false" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <%-- Скрытый GridView для полного отчёта --%>
        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated">
            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
        </asp:GridView>

        <%-- ============================================================ --%>
        <%-- БЛОК РАЗДЕЛОВ ИНФОРМАЦИИ --%>
        <%-- ============================================================ --%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Otch" CssClass="gridviews" runat="server">
                    <asp:CheckBoxList ID="Chk_view" runat="server" style="visibility: hidden; position:absolute">
                        <asp:ListItem Value="Chk_inf">Основная информация</asp:ListItem>
                        <asp:ListItem Value="Chk_kat">Сведения о зданиях/сооружениях</asp:ListItem>
                        <asp:ListItem Value="Chk_ing">Инженерные системы</asp:ListItem>
                        <asp:ListItem Value="Chk_tex">Документы</asp:ListItem>
                        <asp:ListItem Value="Chk_in" Enabled="false">Иная документация</asp:ListItem>
                        <asp:ListItem Value="Chk_proch">Прочие данные</asp:ListItem>
                        <asp:ListItem Value="Chk_zone">Спортивные зоны</asp:ListItem>
                        <asp:ListItem Value="Chk_tabl">Прейскурант</asp:ListItem>
                        <asp:ListItem Value="Chk_sved">Сведения о земельном участке</asp:ListItem>
                    </asp:CheckBoxList>
                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: ОСНОВНАЯ ИНФОРМАЦИЯ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_osnova" runat="server">
                        <asp:ImageButton ID="btn_osnov_inf" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_osnov_inf_Click" AutoPostBack="true" />
                        <asp:Label ID="Osnov_inf" runat="server" Text="Основная информация" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_inf" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_inf_Click" Visible="false" style="display: none" />

                        <asp:GridView ID="Table_inf" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%;" OnRowCreated="Table_inf_RowCreated" OnRowDataBound="Table_inf_RowDataBound" ShowHeader="false">
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>

                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: СВЕДЕНИЯ О ЗДАНИЯХ / СООРУЖЕНИЯХ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_kat" runat="server">
                        <asp:ImageButton ID="btn_kat" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_kat_Click" />
                        <asp:Label ID="Kategor" runat="server" Text="Сведения о зданиях/сооружениях" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_cat" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_cat_Click" Visible="false" style="display: none" />

                        <asp:GridView ID="Table_kat" runat="server" GridLines="None" CssClass="Tab_obj" style="width:100%;" OnRowCreated="Table_kat_RowCreated">
                            <HeaderStyle BorderStyle="None" CssClass="th_table" />
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>

                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: СВЕДЕНИЯ О ЗЕМЕЛЬНОМ УЧАСТКЕ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_sved" runat="server">
                        <asp:ImageButton ID="btn_sved" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_sved_Click" />
                        <asp:Label ID="Svedenia" runat="server" Text="Сведения о земельном участке" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_sved" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_sved_Click" Visible="false" style="display: none" />

                        <asp:GridView ID="Table_sved" runat="server" GridLines="None" CssClass="Tab_obj" style="width:100%;" OnRowCreated="Table_sved_RowCreated">
                            <HeaderStyle BorderStyle="None" CssClass="th_table" />
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>

                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: ИНЖЕНЕРНЫЕ СИСТЕМЫ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_inj" runat="server" Visible="true">
                        <asp:ImageButton ID="btn_inj_sys" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_inj_sys_Click" />
                        <asp:Label ID="Ing_sys" runat="server" Text="Инженерные системы" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_ing" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_ing_Click" Visible="false" style="display: none" />

                        <asp:GridView ID="Table_ing" Width="840px" GridLines="None" CssClass="Tab_obj" runat="server" style="width:100%;" OnRowCreated="Table_ing_RowCreated">
                            <HeaderStyle CssClass="th_table" />
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>
                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: ДОКУМЕНТЫ (ОКС, ЗУ, Техническая) --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_tex" runat="server">
                        <asp:ImageButton ID="btn_tex_doc" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_tex_doc_Click" />
                        <asp:Label ID="Tex_doc" runat="server" Text="Документы" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_tex" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_tex_Click" Visible="false" style="display: none" />

                        <asp:Panel ID="Panel_doc" runat="server" style="display: flex; flex-direction: column;">

                            <asp:Label ID="Label_OKS" runat="server" Text="Документация по ОКС"
                                CssClass="inf_otch maininfo_label_sub" Visible="false"
                                style="font-size: 20px; display: none"></asp:Label>

                            <asp:GridView ID="GridView_OKS" Width="840px" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%;" OnRowCreated="Table_tex_RowCreated">
                                <HeaderStyle BorderStyle="None" CssClass="th_table" />
                                <RowStyle CssClass="row" Height="30px" />
                            </asp:GridView>

                            <asp:Label ID="Label_ZU" runat="server" Text="ЗУ документация"
                                CssClass="inf_otch maininfo_label_sub" Visible="false"
                                style="font-size: 20px; display: none"></asp:Label>

                            <asp:GridView ID="GridView_ZU" Width="840px" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%; display: none" OnRowCreated="Table_tex_RowCreated" Visible="false">
                                <HeaderStyle BorderStyle="None" CssClass="th_table" />
                                <RowStyle CssClass="row" Height="30px" />
                            </asp:GridView>

                            <asp:Label ID="Label_Tex" runat="server" Text="Техническая документация"
                                CssClass="inf_otch maininfo_label_sub" Visible="false"
                                style="font-size: 20px; display: none"></asp:Label>

                            <asp:GridView ID="Table_tex" Width="840px" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%;display: none" OnRowCreated="Table_tex_RowCreated" Visible="false">
                                <HeaderStyle BorderStyle="None" CssClass="th_table" />
                                <RowStyle CssClass="row" Height="30px" />
                            </asp:GridView>

                            <asp:GridView ID="GridView_documents" runat="server" Visible="false" Width="840px" CssClass="Tab_obj" GridLines="None" style="width:100%; display: none">
                                <HeaderStyle BorderStyle="None" CssClass="th_table" />
                                <RowStyle CssClass="row" Height="30px" />
                            </asp:GridView>
                        </asp:Panel>
                    </asp:Panel>

                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: ИНАЯ ДОКУМЕНТАЦИЯ (пока скрыта) --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_in" runat="server" Visible="false">
                        <asp:ImageButton ID="btn_in_doc" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_in_doc_Click" />
                        <asp:Label ID="In_doc" runat="server" Text="Иная документация" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_in" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_in_Click" />

                        <asp:GridView ID="Table_in" Width="840px" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%;" OnRowCreated="Table_in_RowCreated">
                            <HeaderStyle BorderStyle="None" CssClass="th_table" />
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>

                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: ПРОЧИЕ ДАННЫЕ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_proch" runat="server" Visible="false">
                        <asp:ImageButton ID="btn_proch_inf" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_proch_inf_Click" />
                        <asp:Label ID="Proch_doc" runat="server" Text="Прочие данные" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_proch" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_proch_Click" />

                        <asp:GridView ID="Table_proch" Width="840px" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%;" OnRowCreated="Table_proch_RowCreated">
                            <HeaderStyle BorderStyle="None" CssClass="th_table" />
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>
                    </asp:Panel>
                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: СПОРТИВНЫЕ ЗОНЫ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_sport_zone" runat="server" Visible="false">
                        <asp:ImageButton ID="Btn_sports_zone" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="Btn_sports_zone_Click" />
                        <asp:Label ID="Sports_zone" runat="server" Text="Спортивные зоны" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_dow_zone" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_zone_Click" />

                        <asp:Button ID="Vibrat" runat="server" Text="Выбрать все" CssClass="tablebtn3" Width="110px" OnClick="Vibrat_Click" />
                        <asp:Button ID="Snat" runat="server" Text="Снять все" CssClass="tablebtn3" Width="110px" OnClick="Snat_Click" />

                        <asp:Panel ID="Zone" runat="server" CssClass="block_otch_onepage">

                            <%-- Блок со спортзонами для выборки --%>
                            <asp:Panel ID="Panel_vibor_zone" runat="server" CssClass="vibor_zone">
                                <asp:CheckBoxList ID="ListSports_zone" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    DataTextField="NameZone" DataValueField="NameZone" CssClass="zone_box"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                            </asp:Panel>

                            <%-- Таблица с представлением выбранных данных --%>
                            <asp:Panel ID="Panel15" runat="server" CssClass="vibor_zone2">
                                <div class="test">
                                    <asp:GridView ID="Table_zone" CssClass="gv" runat="server" GridLines="None"
                                        OnRowCreated="Table_zone_RowCreated" OnRowDataBound="Table_zone_RowDataBound">
                                        <Columns>
                                            <asp:ImageField AlternateText="Фотография отсутствует" DataImageUrlField="UrlImage" HeaderText="Фотография">
                                                <ItemStyle Height="300px" Width="300px" />
                                            </asp:ImageField>
                                        </Columns>
                                        <HeaderStyle CssClass="th_table" Height="40px" />
                                        <RowStyle CssClass="row" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>

                    <%-- ============================================================ --%>
                    <%-- ПАНЕЛЬ: ПРЕЙСКУРАНТ --%>
                    <%-- ============================================================ --%>
                    <asp:Panel ID="Panel_tabl" runat="server" Visible="false">
                        <asp:ImageButton ID="btn_tabl_inf" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="30px" Width="30px" CssClass="btn_inf" OnClick="btn_tabl_Click" />
                        <asp:Label ID="label_tabl" runat="server" Text="Прейскурант" CssClass="inf_otch"></asp:Label>
                        <asp:ImageButton ID="btn_tabl_tabl" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_tabl_Click" />

                        <asp:GridView ID="Tabl_tabl" Width="840px" CssClass="Tab_obj" GridLines="None" runat="server" style="width:100%;" OnRowCreated="Table_tabl_RowCreated">
                            <HeaderStyle BorderStyle="None" CssClass="th_table" />
                            <RowStyle CssClass="row" Height="30px" />
                        </asp:GridView>

                        <asp:Label ID="label_if_empty" runat="server" Text="Прейскуранта пока нет." Visible="false"></asp:Label>
                    </asp:Panel>

                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Razvernyt" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Svernyt" EventName="Click" />
                <asp:PostBackTrigger ControlID="btn_dow_inf" />
                <asp:PostBackTrigger ControlID="btn_dow_cat" />
                <asp:PostBackTrigger ControlID="btn_dow_sved" />
                <asp:PostBackTrigger ControlID="btn_dow_ing" />
                <asp:PostBackTrigger ControlID="btn_dow_tex" />
                <asp:PostBackTrigger ControlID="btn_dow_in" />
                <asp:PostBackTrigger ControlID="btn_dow_proch" />
                <asp:PostBackTrigger ControlID="btn_dow_zone" />
                <asp:PostBackTrigger ControlID="btn_tabl_tabl" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- ============================================================ --%>
    <%-- СКРЫТЫЕ ДАННЫЕ ДЛЯ ГРАФИКОВ / PDF (заполняются в .cs) --%>
    <%-- ============================================================ --%>

    <asp:Panel ID="Panel_hidden" runat="server" Visible="false" style="display:none;">
        <asp:GridView ID="GridView_hidden" runat="server" Visible="false"></asp:GridView>
    </asp:Panel>

</div>
</asp:Content>