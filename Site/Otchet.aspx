<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Otchet.aspx.cs" Inherits="Site.Otchet" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    #ContentPlaceHolder1_Table_zone .row td { background: #e7e7e7; }
    #ContentPlaceHolder1_Table_zone .row td:first-child { background: white; }
    #ContentPlaceHolder1_Table_zone .row td:hover { color: #e22b36; }
    #ContentPlaceHolder1_Table_zone .row td:first-child:hover { color: unset; }
    .selectric { width: 300px; }
    .th_table { height: 40px; }
    td img { width: 35px; height: 35px; }
</style>

<script src="JS/vivus.min.js"></script>

<script>
    $(document).ready(function () {
        function hoverHandler(selector, fromRegex, to1, to2) {
            $(selector + ' label').hover(function () {
                var atr = $(this).attr('for');
                var two = atr.replaceAll(fromRegex, to1);
                var three = atr.replaceAll(fromRegex, to2);

                if ($("input[id$=" + atr + "]").prop('checked') == false) {
                    var style = { "border-bottom": "1px solid #f8913b", "background": "white" };
                    $("label[for$=" + atr + "]").css(style);
                    $("label[for$=" + two + "]").css(style);
                    $("label[for$=" + three + "]").css(style);

                    $(selector + ' label').mouseleave(function () {
                        var reset = { "border-bottom": "1px solid #e7e7e7", "background": "white" };
                        $("label[for$=" + atr + "]").css(reset);
                        $("label[for$=" + two + "]").css(reset);
                        $("label[for$=" + three + "]").css(reset);
                    });
                }
            });
        }

        hoverHandler('.one_box', /CheckBoxList1/gi, 'CheckBoxList2', 'CheckBoxList3');
        hoverHandler('.two_box', /CheckBoxList2/gi, 'CheckBoxList1', 'CheckBoxList3');
        hoverHandler('.tree_box', /CheckBoxList3/gi, 'CheckBoxList1', 'CheckBoxList2');
    });
</script>

<div class="content fixed_block_position">
    <div class="a-table">

        <section class="finder">
            <h3>ОТЧЁТЫ</h3>
        </section>

        <%-- Блок поиска --%>
        <div class="find" style="padding: unset">
            <div class="selectsandim">
                <div style="display: flex;">
                    <asp:TextBox ID="TextSearch" runat="server" CssClass="input2" style="max-width: 200px;" autocomplete="off" MaxLength="250"></asp:TextBox>

                    <button id="Btn_poisk" runat="server" onserverclick="Btn_poisk_Click" style="border: none; background: none; cursor: pointer;">
                        <svg id="svg-search" class="svg_poisk" style="display:none" xmlns="http://www.w3.org/2000/svg" width="20px" viewBox="0 0 533.333 533.334">
                            <symbol id="svg-find" viewBox="0 0 70 70">
                                <g>
                                    <path d="M27.3950043,0.3162664c-15.0238619,0-27.2056198,12.181757-27.2056198,27.2057343 s12.1817579,27.2057323,27.2057362,27.2057323c5.7772999,0,11.1351223-1.8019867,15.5392857-4.8714409l0.0032043-0.0032043 l20.1769791,20.1770935c0.3744202,0.3744125,0.9697838,0.3840179,1.3539238,0l5.4379959-5.4379959 c0.3744125-0.3744202,0.3552017-0.995285-0.0032043-1.3538094L49.7292938,43.0644875 c3.0694542-4.4073639,4.8714447-9.7653046,4.8714447-15.5424881C54.6007385,12.498023,42.4189796,0.3162664,27.3950043,0.3162664z M27.3950043,48.3263664c-11.4904442,0-20.8043633-9.3139229-20.8043633-20.8043652S15.9046755,6.7176356,27.3950043,6.7176356 s20.8043633,9.313921,20.8043633,20.8043652S38.8854485,48.3263664,27.3950043,48.3263664z"/>
                                </g>
                            </symbol>
                        </svg>
                        <svg class="searchsvg" style="display:block">
                            <use href="#svg-find"></use>
                        </svg>
                    </button>

                    <button id="Btn_update" runat="server" onserverclick="Btn_update_Click" style="border: none; background: none; cursor: pointer;">
                        <svg display="none">
                            <symbol id="svg-refresh" viewBox="0 0 64 64" preserveAspectRatio="xMaxYMax meet">
                                <g>
                                    <path d="M45.274,29.772c-1.094,-8.15 -8.075,-14.435 -16.525,-14.435c-9.203,0 -16.674,7.471 -16.674,16.674c0,9.203 7.471,16.675 16.674,16.675c4.423,0 8.664,-1.757 11.791,-4.884l2.862,2.863c-3.886,3.886 -9.157,6.069 -14.653,6.069c-11.437,0 -20.723,-9.285 -20.723,-20.723c0,-11.437 9.286,-20.723 20.723,-20.723c10.623,0 19.379,7.994 20.582,18.294l3.551,-3.551l3.118,3.117l-8.792,8.792l-8.796,-8.796l3.118,-3.117l3.744,3.745Z"></path>
                                </g>
                            </symbol>
                        </svg>
                        <svg class="searchsvg sizesvg">
                            <use href="#svg-refresh"></use>
                        </svg>
                    </button>
                </div>
            </div>
        </div>

        <%-- Кнопки «Выбрать все» / «Снять все» --%>
        <div class="find" style="padding: unset">
            <div class="selectsandim">
                <asp:Button ID="Button1" runat="server" Text="Выбрать все" CssClass="tablebtn3" style="margin-bottom: 10px; width: 150px" OnClick="Button1_Click1" />
                <asp:Button ID="Button2" runat="server" Text="Снять все" CssClass="tablebtn3" style="margin-bottom: 10px; width: 150px" OnClick="Button2_Click1" />
            </div>
        </div>

        <%-- Кнопка «Новый дизайн» --%>
        <div class="find" style="padding: unset">
            <div class="selectsandim">
                <asp:Button ID="Button4" runat="server" Text="Новый дизайн" CssClass="tablebtn3" style="margin-bottom: 10px; width: 300px" OnClick="Button3_Click" />
            </div>
        </div>

        <div class="find" style="justify-content: unset">
            <asp:Panel ID="Vibor" runat="server" CssClass="selectsandim" style="flex-direction: column">

                <%-- Выпадающие списки фильтрации --%>
                <asp:DropDownList CssClass="select" ID="DropDownList1" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="District" DataValueField="District" style="margin-bottom: 5px; cursor: pointer" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="145px">
                    <asp:ListItem Value="%" Text="Все округа"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownList2" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Category" DataValueField="Category" style="cursor: pointer" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="160px">
                    <asp:ListItem Value="%" Text="Все категории"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownList3" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Supervisor" DataValueField="Supervisor" style="cursor: pointer" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" Width="160px">
                    <asp:ListItem Value="%" Text="Все руководители"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownList4" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Tip" DataValueField="Tip" style="cursor: pointer" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged" Width="160px">
                    <asp:ListItem Value="%" Text="Все типы"></asp:ListItem>
                </asp:DropDownList>

                <%-- 4 CheckBoxList'а + Check_Object --%>
                <div style="display: flex; flex-direction: column;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table ID="List_Object" style="border-radius: 5px; width: 310px;">
                                <tr>
                                    <td valign="top" width="230px" align="center">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True"
                                            DataTextField="NameObject" DataValueField="NameObject"
                                            OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged"
                                            CssClass="one_box" style="position: absolute; text-align:left;"
                                            OnDataBound="CheckBoxList1_DataBound">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td valign="top" width="60px" align="center">
                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" AutoPostBack="True"
                                            DataTextField="District" DataValueField="NameObject"
                                            OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged1"
                                            OnDataBound="CheckBoxList2_DataBound"
                                            CssClass="two_box" style="position: absolute; text-align:left;">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td valign="top" width="20px" align="center">
                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" AutoPostBack="True"
                                            DataTextField="Category" DataValueField="NameObject"
                                            OnSelectedIndexChanged="CheckBoxList3_SelectedIndexChanged"
                                            CssClass="tree_box" style="position: absolute; text-align:left;"
                                            OnDataBound="CheckBoxList3_DataBound">
                                        </asp:CheckBoxList>
                                        <asp:CheckBoxList ID="CheckBoxList4" runat="server"
                                            DataTextField="Category" DataValueField="NameObject"
                                            CssClass="four_box" width="0px" style="visibility: hidden;">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>

                            <asp:CheckBoxList ID="Check_Object" AppendDataBoundItems="True" runat="server"
                                AutoPostBack="True" style="display: none;"
                                DataTextField="NameObject" DataValueField="NameObject"
                                OnDataBound="Check_Object_DataBound">
                            </asp:CheckBoxList>

                            <asp:Label ID="Label1" runat="server" Text="По вашему запросу ничего не найдено." Font-Bold="True" Visible="False"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="DropDownList3" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="DropDownList4" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="progress">
                        <asp:Image ID="Image1" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                    <div class="progress">
                        <asp:Image ID="ImageProgress2" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <%-- ============================================================ --%>
            <%-- ПРАВАЯ ЧАСТЬ: SVG-ЛОГОТИП + ПАНЕЛЬ ОТЧЁТА --%>
            <%-- ============================================================ --%>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel_svg" runat="server" CssClass="draw_logo">
                        <svg version="1.0" id="otchet" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 209.000000 208.000000" preserveAspectRatio="xMidYMid meet">
                            <g transform="translate(0.000000,208.000000) scale(0.100000,-0.100000)">
                                <path d="M830 1498 c0 -345 -2 -391 -20 -464 -60 -251 -233 -460 -480 -578 -38 -18 -78 -36 -90 -39 -18 -5 -20 -13 -20 -86 0 -46 4 -81 10 -81 22 0 177 62 237 95 154 85 293 216 385 365 l57 92 23 -54 c48 -112 99 -198 119 -198 11 0 67 101 105 188 27 61 33 70 39 52 4 -12 33 -61 65 -108 71 -107 208 -241 312 -305 72 -46 210 -108 271 -123 l28 -6 -3 81 -3 82 -76 33 c-208 90 -383 266 -467 469 -57 135 -62 188 -62 597 l0 370 -79 0 -79 0 -4 -397 c-4 -325 -8 -414 -22 -483 -10 -47 -21 -91 -25 -98 -5 -8 -15 17 -26 65 -17 69 -20 129 -23 496 l-3 417 -85 0 -84 0 0 -382z"/>
                                <path d="M590 1277 c0 -216 -74 -381 -227 -509 -31 -26 -76 -57 -100 -70 l-43 -23 0 -82 c0 -56 4 -83 11 -83 25 0 158 72 221 120 133 101 236 258 278 424 15 58 20 112 20 203 l0 123 -80 0 -80 0 0 -103z"/>
                                <path d="M1340 1268 c1 -245 67 -408 234 -573 73 -73 109 -100 183 -138 50 -26 96 -47 102 -47 7 0 11 29 11 83 l-1 82 -65 40 c-36 22 -94 70 -128 106 -119 125 -164 239 -173 437 l-6 122 -78 0 -79 0 0 -112z"/>
                                <path d="M809 507 c-140 -149 -309 -257 -499 -318 l-85 -27 -3 -80 -3 -80 43 9 c141 31 323 111 458 202 41 28 118 93 170 144 l95 92 -35 66 c-19 37 -39 70 -45 75 -6 5 -43 -27 -96 -83z"/>
                                <path d="M1151 530 l-42 -70 21 -30 c34 -48 169 -167 255 -224 134 -90 299 -162 443 -194 l42 -10 0 82 0 82 -67 17 c-186 48 -419 199 -546 352 -27 33 -53 61 -57 63 -4 1 -26 -29 -49 -68z"/>
                            </g>
                        </svg>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList1" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList2" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="CheckBoxList3" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                <ProgressTemplate>
                    <div class="progress">
                        <asp:Image ID="Image2" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <script>
                new Vivus('otchet', {
                    type: 'delayed',
                    duration: 100
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ОСНОВНАЯ ПАНЕЛЬ ОТЧЁТА --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="qweqweq" runat="server" CssClass="rightfind" style="box-shadow: -1px -2px 11px 0px rgb(34 60 80 / 10%); width: 100%; display:unset; height: 100%; max-width:800px; position:relative; top:-138px; margin-left:35px">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Danny" runat="server" style="margin: 10px 25px;">

                            <%-- Кнопки управления отчётом --%>
                            <asp:Button ID="Ves_otchet" runat="server" Text="Скачать отчет" CssClass="tablebtn3" style="margin:10px 5px 15px 0" OnClick="Ves_otchet_Click" />
                            <asp:Button ID="Razvernyt" runat="server" Text="Развернуть все" CssClass="tablebtn3" style="margin:10px 5px 15px 0" OnClick="Razvernyt_Click" />
                            <asp:Button ID="Svernyt" runat="server" Text="Свернуть все" CssClass="tablebtn3" style="margin:10px 5px 15px 0" OnClick="Svernyt_Click" />
                            <asp:Button ID="Vidim" runat="server" Text="Скачать видимые данные" CssClass="tablebtn3" style="margin:10px 5px 15px 0" OnClick="Vidim_Click" />

                            <%-- Скрытый список видимых разделов --%>
                            <asp:CheckBoxList ID="Chk_view" runat="server" style="display:none;">
                                <asp:ListItem Value="Chk_inf">Основная информация</asp:ListItem>
                                <asp:ListItem Value="Chk_kat">Сведения о зданиях/сооружениях</asp:ListItem>
                                <asp:ListItem Value="Chk_sved">Сведения о земельном участке</asp:ListItem>
                                <asp:ListItem Value="Chk_ing">Инженерные системы</asp:ListItem>
                                <asp:ListItem Value="Chk_tex">Документы</asp:ListItem>
                                <asp:ListItem Value="Chk_in" Enabled="false">Иная документация</asp:ListItem>
                                <asp:ListItem Value="Chk_proch">Прочие данные</asp:ListItem>
                                <asp:ListItem Value="Chk_zone">Спортивные зоны</asp:ListItem>
                            </asp:CheckBoxList>

                            <%-- Сводная таблица (полный отчёт) --%>
                            <asp:GridView ID="GridView1" runat="server" CssClass="Tab_obj2" OnRowDataBound="GridView1_RowDataBound">
                                <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                <RowStyle CssClass="row" Height="40px" />
                            </asp:GridView>

                            <%-- ============================================================ --%>
                            <%-- БЛОК РАЗДЕЛОВ ИНФОРМАЦИИ --%>
                            <%-- ============================================================ --%>
                            <asp:Panel ID="Otch" runat="server">

                                <%-- ПАНЕЛЬ 1: ОСНОВНАЯ ИНФОРМАЦИЯ --%>
                                <asp:Panel ID="Panel_osnova" runat="server" style="padding-bottom: 10px;">
                                    <div>
                                        <asp:ImageButton ID="btn_osnov_inf" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_osnov_inf_Click" />
                                        <asp:Label ID="Osnov_inf" runat="server" Text="Основная информация" CssClass="inf_otch"></asp:Label>
                                        <asp:ImageButton ID="btn_dow_inf" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_inf_Click" />
                                    </div>
                                    <asp:Panel ID="Panel_Table_inf" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_inf" GridLines="None" CssClass="Tab_obj2" runat="server" style="border-radius: 5px;" OnRowCreated="Table_inf_RowCreated" OnRowDataBound="Table_inf_RowDataBound">
                                            <HeaderStyle CssClass="th_table" Width="100px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 2: СВЕДЕНИЯ О ЗДАНИЯХ/СООРУЖЕНИЯХ --%>
                                <asp:Panel ID="Panel_kat" runat="server" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="btn_kat" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_kat_Click" />
                                    <asp:Label ID="Kategor" runat="server" Text="Сведения о зданиях/сооружениях" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_cat" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_cat_Click" />

                                    <asp:Panel ID="Panel_Table_kat" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_kat" GridLines="None" CssClass="Tab_obj2" runat="server" style="border-radius: 5px;" OnRowCreated="Table_kat_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 3: СВЕДЕНИЯ О ЗЕМЕЛЬНОМ УЧАСТКЕ --%>
                                <asp:Panel ID="Panel_sved" runat="server" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="btn_sved" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_sved_Click" />
                                    <asp:Label ID="Svedenia" runat="server" Text="Сведения о земельном участке" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_sved" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" Visible="false" OnClick="btn_dow_sved_Click" />

                                    <asp:Panel ID="Panel_Table_sved" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_sved" CssClass="Tab_obj2" GridLines="None" runat="server" style="border-radius: 5px;" OnRowCreated="Table_sved_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 4: ИНЖЕНЕРНЫЕ СИСТЕМЫ --%>
                                <asp:Panel ID="Panel_inj" runat="server" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="btn_inj_sys" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_inj_sys_Click" />
                                    <asp:Label ID="Ing_sys" runat="server" Text="Инженерные системы" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_ing" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_ing_Click" />

                                    <asp:Panel ID="Panel_Table_ing" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_ing" GridLines="None" CssClass="Tab_obj2" runat="server" style="border-radius: 5px;" OnRowCreated="Table_ing_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 5: ДОКУМЕНТЫ --%>
                                <asp:Panel ID="Panel_tex" runat="server" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="btn_tex_doc" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_tex_doc_Click" />
                                    <asp:Label ID="Tex_doc" runat="server" Text="Документы" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_tex" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_tex_Click" />

                                    <asp:Panel ID="Panel_doc" runat="server" style="display: flex; flex-direction: column; padding-top: 15px;" Visible="false">
                                        <asp:Label ID="Label_OKS" runat="server" Text="Документация по ОКС" CssClass="inf_otch maininfo_label_sub" Visible="false" style="font-size: 20px; padding: 25px 0px 10px 0px;"></asp:Label>
                                        <asp:GridView ID="GridView_OKS" CssClass="Tab_obj2" GridLines="None" runat="server" style="border-radius: 5px;" OnRowCreated="GridView_OKS_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>

                                        <asp:Label ID="Label_ZU" runat="server" Text="ЗУ документация" CssClass="inf_otch maininfo_label_sub" Visible="false" style="font-size: 20px; padding: 25px 0px 10px 0px;"></asp:Label>
                                        <asp:GridView ID="GridView_ZU" CssClass="Tab_obj2" GridLines="None" runat="server" style="border-radius: 5px;" OnRowCreated="GridView_ZU_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>

                                        <asp:Label ID="Label_Tex" runat="server" Text="Техническая документация" CssClass="inf_otch maininfo_label_sub" Visible="false" style="font-size: 20px; padding: 25px 0px 10px 0px;"></asp:Label>
                                        <asp:GridView ID="Table_tex" CssClass="Tab_obj2" GridLines="None" runat="server" style="border-radius: 5px;" OnRowCreated="Table_tex_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>

                                        <asp:GridView ID="GridView_documents" runat="server" Width="840px" CssClass="Tab_obj" GridLines="None" Visible="false" style="width:100%;" OnRowDataBound="GridView_documents_RowDataBound">
                                            <HeaderStyle BorderStyle="None" CssClass="th_table" />
                                            <RowStyle CssClass="row" Height="30px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 6: ИНАЯ ДОКУМЕНТАЦИЯ --%>
                                <asp:Panel ID="Panel_in" runat="server" Visible="false" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="btn_in_doc" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_in_doc_Click" />
                                    <asp:Label ID="In_doc" runat="server" Text="Иная документация" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_in" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_in_Click" />

                                    <asp:Panel ID="Panel_in_doc" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_in" GridLines="None" CssClass="Tab_obj2" runat="server" style="border-radius: 5px;" OnRowCreated="Table_in_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 7: ПРОЧИЕ ДАННЫЕ --%>
                                <asp:Panel ID="Panel_proch" runat="server" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="btn_proch_inf" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_proch_inf_Click" />
                                    <asp:Label ID="Proch_doc" runat="server" Text="Прочие данные" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_proch" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_proch_Click" />

                                    <asp:Panel ID="Panel_Table_proch" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_proch" GridLines="None" CssClass="Tab_obj2" runat="server" style="border-radius: 5px;" OnRowCreated="Table_proch_RowCreated">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                                <%-- ПАНЕЛЬ 8: СПОРТИВНЫЕ ЗОНЫ --%>
                                <asp:Panel ID="Panel_sport_zone" runat="server" style="padding-bottom: 10px;">
                                    <asp:ImageButton ID="Btn_sports_zone" runat="server" ImageUrl="/Logo/Стрелка вправо.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="Btn_sports_zone_Click" />
                                    <asp:Label ID="Sports_zone" runat="server" Text="Спортивные зоны" CssClass="inf_otch"></asp:Label>
                                    <asp:ImageButton ID="btn_dow_zone" runat="server" ImageUrl="/Logo/Загрузка.svg" Height="25px" Width="25px" CssClass="btn_inf" OnClick="btn_dow_zone_Click" />

                                    <asp:Panel ID="Panel_Table_zone" runat="server" Visible="false" style="padding-top: 15px;">
                                        <asp:GridView ID="Table_zone" GridLines="None" CssClass="Tab_obj2" runat="server" style="border-radius: 5px; padding: 2px;" OnRowCreated="Table_zone_RowCreated" OnRowDataBound="Table_zone_RowDataBound">
                                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                            <RowStyle CssClass="row" Height="40px" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>

                            </asp:Panel><%-- /Panel_Otch --%>

                        </asp:Panel><%-- /Panel_Danny --%>

                        <asp:UpdateProgress ID="UpdateProgress4" runat="server">
                            <ProgressTemplate>
                                <div class="progress">
                                    <asp:Image ID="Image4" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Razvernyt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Svernyt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="CheckBoxList1" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="CheckBoxList2" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="CheckBoxList3" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        <asp:PostBackTrigger ControlID="Vidim" />
                        <asp:PostBackTrigger ControlID="btn_dow_inf" />
                        <asp:PostBackTrigger ControlID="btn_dow_cat" />
                        <asp:PostBackTrigger ControlID="btn_dow_sved" />
                        <asp:PostBackTrigger ControlID="btn_dow_ing" />
                        <asp:PostBackTrigger ControlID="btn_dow_tex" />
                        <asp:PostBackTrigger ControlID="btn_dow_in" />
                        <asp:PostBackTrigger ControlID="btn_dow_proch" />
                        <asp:PostBackTrigger ControlID="btn_dow_zone" />
                        <asp:PostBackTrigger ControlID="Ves_otchet" />
                    </Triggers>
                </asp:UpdatePanel>

            </asp:Panel><%-- /Panel_qweqweq --%>

            <asp:UpdateProgress ID="UpdateProgress5" runat="server">
                <ProgressTemplate>
                    <div class="progress">
                        <asp:Image ID="Image3" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

        </div>
    </div>
</div>

</asp:Content>