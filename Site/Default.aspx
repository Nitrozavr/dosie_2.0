<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="Site.Default" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="JS/map-polygons.js"></script>

    <style>
        .ymaps-2-1-79-svg-icon {
            background-image: url("/Imges/Круг.svg")
        }
        .ymaps-2-1-79-map ymaps {
            border-radius: 4px;
        }

        .zone_box input[type="checkbox"] + label {
            width: 125px;
            display: flex;
            justify-content: space-around;
            margin-right: 10px;
            margin-bottom: 10px;
            padding: 7px !important;
            border: 1px solid #dcdfe6;
            border-radius: 5px;
            color: #606266;
            background: white;
            font-size: 12px;
            height: 28px;
        }

        .zone_box input[type="checkbox"]:checked + label {
            background: #fdebeb;
            color: #E22B36;
            border: 1px solid #d42e2e;
            cursor: pointer;
            border-radius: 5px;
            padding: 7px !important;
        }

        .pagination td {
            width: 40px;
            height: 40px;
            padding: 2px 10px !important;
            min-width: 10px !important;
            border: 2px solid transparent;
        }
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
            <svg class="topbtn">
                <use href="#svg-top"></use>
            </svg>
        </div>

        <script>
            $(window).scroll(function () {
                var height = $(window).scrollTop();
                if (height > 300) {
                    $('.topbtn').fadeIn();
                } else {
                    $('.topbtn').fadeOut();
                }
            });
            $(document).ready(function () {
                $(".topbtn").click(function (event) {
                    event.preventDefault();
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                    return false;
                });
            });
        </script>

        <style>
            .selectric-wrapper {
                width: 100%;
            }
            #find .selectsandim {
                width: 1170px;
            }
            @media(max-width:1200px) {
                #find .selectsandim { width: 965px; }
                .map_div { width: 500px !important; }
                .input4 { width: 417px; }
            }
            @media (max-width: 1140px) {
                #find .selectsandim { width: 100%; }
                .filtup { top: 15px; }
            }
        </style>
    </div>

    <div class="a-table">
        <asp:Label ID="Kol_vo_object" runat="server" CssClass="objecttext" style="display: block; margin-top: 40px" Text=""></asp:Label>

        <div class="find" id="find" style="padding: unset">
            <%--блок с данными для фильтрации информации--%>
            <div class="selectsandim">
                <asp:DropDownList CssClass="select" ID="DropDownList1" AppendDataBoundItems="true" runat="server" AutoPostBack="True" Visible="true" DataTextField="District" DataValueField="District" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="%" Text="Все округа"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownList2" AppendDataBoundItems="true" runat="server" AutoPostBack="True" Visible="true" DataTextField="Raion" DataValueField="Raion" BackColor="White" ForeColor="Black" class="default" style="cursor: pointer" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem Value="%" Text="Все районы"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownList3" AppendDataBoundItems="true" runat="server" AutoPostBack="True" Visible="false" DataTextField="Supervisor" DataValueField="Supervisor" BackColor="White" ForeColor="Black" class="default" style="cursor: pointer" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                    <asp:ListItem Value="%" Text="Все руководители"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownListTip" AppendDataBoundItems="true" runat="server" AutoPostBack="True" Visible="false" DataTextField="Tip" DataValueField="Tip" class="default" OnSelectedIndexChanged="DropDownListTip_SelectedIndexChanged">
                    <asp:ListItem Value="%" Text="Все типы"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="DropDownList_Supervisor" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="%" Text="Фильтр 1"></asp:ListItem>
                    <asp:ListItem Value="%" Text="Значение 1"></asp:ListItem>
                    <asp:ListItem Value="%" Text="Значение 2"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="DropDownList_Tip" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="%" Text="Фильтр 2"></asp:ListItem>
                    <asp:ListItem Value="%" Text="Значение 1"></asp:ListItem>
                    <asp:ListItem Value="%" Text="Значение 2"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="DropDownList_Rooms" CssClass="select" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="%" Text="Количество комнат"></asp:ListItem>
                    <asp:ListItem Value="%" Text="Значение 1"></asp:ListItem>
                    <asp:ListItem Value="%" Text="Значение 2"></asp:ListItem>
                </asp:DropDownList>

                <asp:CheckBox ID="CheckBox_object" runat="server" CssClass="zone_box" Text="Другие объекты" AutoPostBack="true" Visible="false" OnCheckedChanged="CheckBox_object_CheckedChanged" />
            </div>

            <%--блок поиска--%>
            <div class="search">
                <asp:TextBox ID="TextSearch" runat="server" CssClass="input4" style="min-width: 250px;" autocomplete="off" placeholder="Поиск" MaxLength="250"></asp:TextBox>

                <div style="position:relative;">
                    <div class="filtup">
                        <div class="pozic">
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

                            <button id="Btn_filt" runat="server" class="filtup_btn" onserverclick="Btn_filt_ServerClick" style="border: none; background: none; cursor: pointer;">
                                <svg display="none">
                                    <symbol id="svg-filters" viewBox="0 0 70 70" preserveAspectRatio="xMaxYMax meet">
                                        <g>
                                            <path d="M13.3539429,19.8843689h16.6099243c0.8381958,2.1548462,2.9275513,3.6846924,5.3788452,3.6846924 s4.5406494-1.5298462,5.3788452-3.6846924h18.913269c1.2565918,0,2.2752686-1.0186768,2.2752686-2.2752686 s-1.0186768-2.2752686-2.2752686-2.2752686H40.5616455c-0.9244995-1.9573364-2.9106445-3.3148804-5.2189331-3.3148804 s-4.2944336,1.3575439-5.2189331,3.3148804H13.3539429c-1.2565918,0-2.2752686,1.0186768-2.2752686,2.2752686 S12.0973511,19.8843689,13.3539429,19.8843689z"/>
                                            <path d="M59.6348267,33.082489h-3.7023926c-0.9244995-1.9573364-2.9106445-3.3148804-5.2189331-3.3148804 s-4.2944946,1.3575439-5.2189941,3.3148804h-32.140564c-1.2565918,0-2.2752686,1.0186768-2.2752686,2.2752686 s1.0186768,2.2752686,2.2752686,2.2752686h31.9806519c0.8382568,2.1548462,2.9276123,3.6846924,5.3789062,3.6846924 s4.5406494-1.5298462,5.3788452-3.6846924h3.5424805c1.2565918,0,2.2752686-1.0186768,2.2752686-2.2752686 S60.8914185,33.082489,59.6348267,33.082489z"/>
                                            <path d="M59.6348267,50.1451721h-31.545105c-0.9244995-1.9573364-2.9106445-3.3148804-5.2189331-3.3148804 s-4.2944336,1.3575439-5.2189331,3.3148804h-4.2979126c-1.2565918,0-2.2752686,1.0186768-2.2752686,2.2752686 s1.0186768,2.2752686,2.2752686,2.2752686h4.1380005c0.8381958,2.1548462,2.9275513,3.6846924,5.3788452,3.6846924 s4.5406494-1.5298462,5.3788452-3.6846924h31.3851929c1.2565918,0,2.2752686-1.0186768,2.2752686-2.2752686 S60.8914185,50.1451721,59.6348267,50.1451721z"/>
                                        </g>
                                    </symbol>
                                </svg>
                                <svg class="searchsvg sizesvg">
                                    <use href="#svg-filters"></use>
                                    <p class="txt_btn_up">Фильтры</p>
                                </svg>
                            </button>

                            <button id="Btn_update" class="filtup_btn" runat="server" onserverclick="Btn_update_Click" style="border: none; background: none; cursor: pointer;">
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
                                <span class="txt_btn_up">Обновить</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--левая часть главной страницы со списком объектов--%>
        <div class="find" style="padding: unset;" id="sai">

            <%--правая часть главной страницы с картой и фильтрами--%>
            <div class="selectsandim map_div" style="max-width: 700px; width: 100%; align-items:normal !important; justify-content: center !important; flex-direction:row !important; height:800px ">
                <div style="position: absolute; z-index: 20;">
                    <asp:CheckBox ID="CheckBox_filter" runat="server" CssClass="zone_box" Text="Отключить слои" style="position:relative; width: 124px; top: 11px;" AutoPostBack="true" Visible="true" checked="false" />
                </div>

                <div id="mymap" style="width: 100%; height: 800px; border-radius: 3px;"></div>

                <script type="text/javascript">
                    var markers = [
                        <asp:Repeater ID="rptMarkers" runat="server">
                            <ItemTemplate>
                                {
                    "lat": '<%# Eval("Lat") %>',
                    "lagg": '<%# Eval("Lagg") %>',
                    "url_mark": '<%# Eval("UrlMark") %>',
                    "Name_object": '<%# Eval("NameObject") %>',
                    "Adress": '<%# Eval("Adress") %>',
                    "Number_object": '<%# Eval("NumberObject") %>',
                    "Raion": '<%# Eval("Raion") %>',
                    "District": '<%# Eval("District") %>',
                    "Tip": '<%# Eval("Tip") %>',
                    "Supervisor": '<%# Eval("Supervisor") %>'
                }
            </ItemTemplate>
            <SeparatorTemplate>,</SeparatorTemplate>
        </asp:Repeater>
    ];

    ymaps.ready(init);

                    function init() {
                        var myMap = new ymaps.Map("mymap", {
                            center: [55.751, 37.618],   // центр Москвы
                            zoom: 10
                        });

                        window.myMap = myMap;
                        window.objectMarkers = {};

                        // === БЕЗ кластеризации: каждый маркер добавляется напрямую ===
                        for (var i = 0; i < markers.length; i++) {
                            var data = markers[i];
                            if (!data.lat || !data.lagg) continue;

                            var lat = parseFloat(String(data.lat).replace(',', '.'));
                            var lagg = parseFloat(String(data.lagg).replace(',', '.'));

                            // Проверка на NaN
                            if (isNaN(lat) || isNaN(lagg)) {
                                console.warn('Невалидные координаты:', data.Name_object, data.lat, data.lagg);
                                continue;
                            }

                            var marker = new ymaps.Placemark(
                                [lat, lagg],
                                {
                                    balloonContentHeader: data.Name_object,
                                    hintContent: '<p><b>' + data.Name_object + '</b></p><p>' + (data.Adress || '') + '</p>',
                                    iconContent: '<img src="' + (data.url_mark || '/Logo/marker.svg') + '" style="width:24px;height:24px;display:block;" />'
                                },
                                {
                                    iconLayout: 'default#imageWithContent',
                                    iconImageHref: '/Logo/marker-bg.svg',   // ← оранжевый круг-подложка
                                    iconImageSize: [40, 40],
                                    iconImageOffset: [-20, -20],
                                    iconContentOffset: [8, 8],              // ← сдвиг SVG в центр круга
                                    iconContentSize: [24, 24]
                                }
                            );

                            window.objectMarkers[data.Name_object] = marker;

                            // Клик по маркеру → переход на страницу объекта
                            marker.events.add('click', function (e) {
                                var metka = e.get('target');
                                var nameobject = metka.properties.get('balloonContentHeader');
                                window.location.href = '/One_page.aspx?map=' + encodeURIComponent(nameobject);
                            });

                            // ⚠️ Добавляем маркер НАПРЯМУЮ на карту (не в кластер!)
                            myMap.geoObjects.add(marker);
                        }

                        // === Полигоны районов ===
                        var showPolys = !$('#<%=CheckBox_filter.ClientID %>').prop('checked');
    addMoscowPolygons(myMap, showPolys);

    myMap.controls.remove('searchControl');
    myMap.controls.remove('trafficControl');

    // === Автоподгонка под все маркеры ===
    if (markers.length > 0) {
        var minLat = 90, maxLat = -90, minLagg = 180, maxLagg = -180;
        for (var k = 0; k < markers.length; k++) {
            var la = parseFloat(markers[k].lat);
            var lo = parseFloat(markers[k].lagg);
            if (isNaN(la) || isNaN(lo)) continue;
            if (la < minLat) minLat = la;
            if (la > maxLat) maxLat = la;
            if (lo < minLagg) minLagg = lo;
            if (lo > maxLagg) maxLagg = lo;
        }
        if (minLat < maxLat && minLagg < maxLagg) {
            myMap.setBounds([[minLat, minLagg], [maxLat, maxLagg]], {
                checkZoomRange: true,
                zoomMargin: 50
            });
        }
    }
}
                </script>
            </div>
            <%--Правый блок с объектами и фильтрами--%>
            <div class="rightfind" style="max-width: 700px; align-items:unset;">

                <%--Блок результатов поиска объектов по клику--%>
                <div class="rght_1">
                    <asp:GridView ID="TableObject" runat="server" GridLines="None" DataKeyNames="NameObject"
                        table-layout="fixed" width="465px" CssClass="Tab_obj2"
                        OnRowCreated="TableObject_RowCreated" OnRowDataBound="TableObject_RowDataBound"
                        AllowPaging="True" PageSize="16" AutoGenerateColumns="false">
                        <HeaderStyle CssClass="th_table" Height="40px" />
                        <RowStyle CssClass="row" Height="35px" />
                        <PagerStyle Height="40px" CssClass="pagination" />
                        <Columns>
                            <asp:BoundField DataField="NameObject" HeaderText="Имя объекта" />
                            <asp:BoundField DataField="Adress" HeaderText="Адрес" />
                        </Columns>
                        <emptydatatemplate>
                            <asp:Label ID="Label1" runat="server" Text="По вашему запросу ничего не найдено." Font-Bold="True"></asp:Label>
                        </emptydatatemplate>
                    </asp:GridView>
                    <asp:GridView ID="GridView_TableObject_hidden" runat="server" Visible="false"
                        AutoGenerateColumns="false" DataKeyNames="NameObject"
                        table-layout="fixed" width="465px" CssClass="Tab_obj2"
                        AllowPaging="false" PageSize="16">
                        <HeaderStyle CssClass="th_table" Height="40px" />
                        <Columns>
                            <asp:BoundField DataField="NameObject" HeaderText="Имя объекта" />
                            <asp:BoundField DataField="Adress" HeaderText="Адрес" />
                        </Columns>
                        <RowStyle CssClass="row" Height="35px" />
                        <PagerStyle Height="40px" CssClass="pagination" />
                        <emptydatatemplate>
                            <asp:Label ID="Label1" runat="server" Text="По вашему запросу ничего не найдено." Font-Bold="True"></asp:Label>
                        </emptydatatemplate>
                    </asp:GridView>
                </div>

                <asp:ImageButton ID="btn_dow_hidden_TableObject" runat="server" ImageUrl="/Logo/download.svg" Height="25px" Width="25px" CssClass="btn_inf" Visible="false" OnClick="btn_dow_hidden_TableObject_Click" />

                <%--Блок фильтров по клику--%>
                <div class="rght_2">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div id="Fi_def" runat="server" class="Fi_def" Visible="false">
                                <asp:Label ID="Label_CheckBoxList_filter1" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="Label_CheckBoxList_filter2" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="Label_CheckBoxList_filter3" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="Label_CheckBoxList_filter4" runat="server" Text="" Visible="false"></asp:Label>

                                <div class="filt_chk">
                                    <div class="cont_filt">
                                        <asp:Label runat="server" CssClass="filt_label" Text="Фильтр 1"></asp:Label>
                                        <asp:CheckBoxList ID="CheckBoxList_filter1" RepeatDirection="Horizontal" RepeatColumns="3" CssClass="chchbx check_style" runat="server">
                                            <asp:ListItem value="Значение 1" Selected="True"></asp:ListItem>
                                            <asp:ListItem value="Значение 2"></asp:ListItem>
                                            <asp:ListItem value="Значение 3"></asp:ListItem>
                                            <asp:ListItem value="Значение 4"></asp:ListItem>
                                            <asp:ListItem value="Значение 5" Selected="True"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>

                                    <div class="cont_filt">
                                        <asp:Label runat="server" CssClass="filt_label" Text="Фильтр 2"></asp:Label>
                                        <asp:CheckBoxList ID="CheckBoxList_filter2" RepeatDirection="Horizontal" CssClass="chchbx check_style" runat="server">
                                            <asp:ListItem value="Объект">Значение 1</asp:ListItem>
                                            <asp:ListItem value="Модуль">Значение 2</asp:ListItem>
                                            <asp:ListItem value="Земля">Значение 3</asp:ListItem>
                                            <asp:ListItem value="Административные">Значение 4</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>

                                    <div class="cont_filt">
                                        <asp:Label runat="server" CssClass="filt_label" Text="Фильтр 3"></asp:Label>
                                        <asp:CheckBoxList ID="CheckBoxList_filter3" RepeatDirection="Horizontal" CssClass="chchbx check_style" runat="server">
                                            <asp:ListItem value="Подходит под реновацию/для инвестора">Значение 1</asp:ListItem>
                                            <asp:ListItem value="Подходит под 636">Значение 2</asp:ListItem>
                                            <asp:ListItem value="Подходит под сдачу в аренду">Значение 3</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>

                                    <div>
                                        <asp:Label runat="server" CssClass="filt_label" Text="Вложенный фильтр"></asp:Label>
                                    </div>

                                    <div class="chchbx cont_filt">
                                        <asp:CheckBox ID="CheckBox_prof" Text="Фильтр 1" runat="server" CssClass="chckbxone check_style" OnCheckedChanged="CheckBox_prof_CheckedChanged" AutoPostBack="true" />

                                        <asp:Panel ID="Proil_check" runat="server" Visible="false">
                                            <div class="cont_filt_dow">
                                                <asp:CheckBox ID="CheckBox_tip" runat="server" Text="Фильтр 1.1" CssClass="chchbx check_style" OnCheckedChanged="CheckBox_tip_CheckedChanged" AutoPostBack="true" />
                                                <div class="q1312q">
                                                    <img src='/Logo/Tochka_krug2.svg' style="width: 11px; height: 11px; left: 0px; margin-top: 0px;">
                                                </div>
                                                <asp:CheckBoxList ID="CheckBoxList_tip" runat="server" CssClass="check_style cont_filt_dow2" OnSelectedIndexChanged="CheckBoxList_tip_SelectedIndexChanged" Visible="false">
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.1.1" value="Зал"></asp:listitem>
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.1.2" value="Бассейн-полуарка"></asp:listitem>
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.1.3" value="Бассейн-арка"></asp:listitem>
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.1.4" value="Лёд"></asp:listitem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="cont_filt_dow">
                                                <asp:CheckBox ID="CheckBox_sporter" runat="server" Text="Фильтр 1.2" CssClass="chchbx check_style" OnCheckedChanged="CheckBox_sporter_CheckedChanged" AutoPostBack="true" />
                                                <div class="q1312q">
                                                    <img src='/Logo/Tochka_krug2.svg' style="width: 11px; height: 11px; left: 0px; margin-top: 0px;">
                                                </div>
                                                <asp:CheckBoxList ID="CheckBoxList_sporter" runat="server" CssClass="check_style cont_filt_dow2" OnSelectedIndexChanged="CheckBoxList_sporter_SelectedIndexChanged" Visible="false">
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.2.1" value="Новые/не требуют ремонта"></asp:listitem>
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.2.2" value="Проводятся/запланированы ремонтные работы"></asp:listitem>
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.2.3" value="Требуется реновация"></asp:listitem>
                                                    <asp:listitem Text="<img src='/Logo/Tochka_krug3.svg' /> Значение 1.2.4" value="Иные"></asp:listitem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="cont_filt_dow">
                                                <asp:CheckBoxList ID="DropDownFiltersProfils_dr" runat="server" CssClass="chchbx check_style" OnSelectedIndexChanged="DropDownFiltersProfils_dr_SelectedIndexChanged">
                                                    <asp:ListItem value="Значение 1"></asp:ListItem>
                                                    <asp:ListItem value="Значение 2"></asp:ListItem>
                                                    <asp:ListItem value="Значение 3"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="chchbx cont_filt">
                                        <asp:CheckBox ID="CheckBox_neprof" Text="Фильтр 2" runat="server" CssClass="chckbxone check_style" OnCheckedChanged="CheckBox_neprof_CheckedChanged" AutoPostBack="true" />
                                        <div class="cont_filt_dow">
                                            <asp:CheckBoxList ID="CheckBoxList4" runat="server" Visible="false" CssClass="chchbx check_style" OnSelectedIndexChanged="CheckBoxList4_SelectedIndexChanged">
                                                <asp:ListItem value="Значение 1"></asp:ListItem>
                                                <asp:ListItem value="Значение 2"></asp:ListItem>
                                                <asp:ListItem value="Значение 3"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>

                                    <div class="cont_filt" style="display:none">
                                        <asp:Label runat="server" CssClass="filt_label" Text="Доступность для инвалидов"></asp:Label>
                                        <div class="chchbx2">
                                            <asp:CheckBox ID="DostupInv" runat="server" Text="Да" CssClass="check_style" />
                                        </div>
                                    </div>
                                </div>

                                <div class="btn_filt">
                                    <div>
                                        <asp:Button ID="btn_fult_cancel" CssClass="btn_filt_ops btn_filt_canc" runat="server" Text="Отменить" OnClick="btn_fult_cancel_Click" />
                                    </div>
                                    <div>
                                        <asp:Button ID="btn_fult_accept" CssClass="btn_filt_ops btn_filt_acce" runat="server" Text="Применить" OnClick="btn_fult_accept_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CheckBox_prof" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox_neprof" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox_sporter" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox_tip" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Btn_filt" />
                            <asp:PostBackTrigger ControlID="btn_fult_cancel" />
                            <asp:PostBackTrigger ControlID="btn_fult_accept" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script src="JS/jspdf-1.3.5.min.js"></script>
    <script src="JS/canvas-0.4.1.min.js"></script>

</asp:Content>