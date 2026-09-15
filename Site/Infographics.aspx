<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Infographics.aspx.cs" Inherits="Site.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/jspdf-1.3.5.min.js"></script>
    <script src="JS/canvas-0.4.1.min.js"></script>

    <style>
        .chart { min-width: 500px; }
        .selectric-wrapper { width: 200px; }
        .Tab_obj tr:hover { border-top: 1px solid transparent; border-bottom: 1px solid #e7e7e7; }
        .Tab_obj td { padding: 20px; }
        .hide { text-align: center; }
        .hide tr td:first-child { display: none; }
        .hide tr td:nth-child(2) { background: #a6a6a6; }
        .hide tr td:nth-child(3) { background: #d9d9d9; }
        .hide tr td:nth-child(4) { background: #d9d9d9; }
        .hide tr:last-child td { display: unset; padding: 10px; background: unset; }
        .hide td { padding: 15px 20px; }
        .hide tr td:nth-child(2) { padding: 15px 0px; width: 275px; background: #a6a6a6; font-weight: 600; }
        .hide tr:first-child td { background: #a6a6a6; font-weight: 600; }
        .hide tr:nth-child(-n+4) td:nth-child(2) { background: #E22B36; color: white; font-weight: 600; }
        .hide tr:last-child td:nth-child(2) { padding: unset; width: unset; background: unset; font-weight: unset; }

        .skryt tr td:first-child { display: none; }
        .skryt tr th:first-child { display: none; }

        .spravka { background: #f2f2f2; margin-left: 100px; margin-top: -70px; }
        .spravka tr { padding: 10px; display: list-item; }
        .spravka tr:first-child { margin-top: unset; }

        .obrasch { margin-top: 32px; font-size: 16px; display: list-item; margin-left: 20px; }

        .rowsootn { border: 3px solid white; font-size: 13px; }
        .rowsootn td { padding: 10px; }
        .rowsootn td:first-child { font-weight: 600; background: #d9d9d9; }
        .rowsootn td:nth-child(2) { background: #f2f2f2; }
        .rowsootn td:nth-child(3) { background: #d9d9d9; }
        .rowsootn td:nth-child(4) { background: #f2f2f2; }
        .rowsootn td:nth-child(5) { background: #e94f5b; color: white; }

        ::marker { color: #E22B36; }

        .gv2 { margin-top: 200px; }
        .gvtop1 { margin-left: 100px; }
        .gvtop24 { margin-left: 50px; }
        .gvtop3 { margin-left: 200px; }
        .kolobr { margin-left: 30px; align-items: end; }

        @media (max-width: 1040px) {
            .media { flex-direction: column; align-items: center; }
            .media2 { flex-direction: column; }
            .gv2 { margin-top: unset; }
            .gvtop1 { margin-left: unset; margin-top: 40px; }
            .gvtop24 { margin-left: unset; margin-top: 50px; }
            .gvtop3 { margin-left: unset; margin-top: 100px; }
            .gvspravka { margin-left: -70px; margin-top: 100px; }
            .kolobr { align-items: unset; margin-left: 10px; }
            .chart { min-width: 300px; max-width: 300px; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="/JS/moment.js"></script>
    <script src="/JS/ru.js"></script>

    <div class="content">
        <div class="a-table">

            <section class="finder">
                <h3>ИНФОГРАФИКА</h3>
            </section>

            <%-- Скрытые Label'ы для хранения вычисленных дат --%>
            <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Label_date_sootnosh" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Label ID="Month3" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month4" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month5" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month6" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month7" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month8" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month9" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month10" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="Month11" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Label ID="NameMonth3" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth4" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth5" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth6" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth7" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth8" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth9" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth10" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="NameMonth11" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Label ID="Label5" runat="server" Text="Выберите месяц" style="color: #adadad; font-size: 11px; margin-left: 5px"></asp:Label>

            <div class="find" style="padding: unset">
                <div class="selectsandim">
                    <asp:Label ID="LabelData1" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="LabelData2" runat="server" Text="" Visible="false"></asp:Label>

                    <asp:DropDownList ID="DropDownList_Month" runat="server" CssClass="select" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="-01-" Text="Январь"></asp:ListItem>
                        <asp:ListItem Value="-02-" Text="Февраль"></asp:ListItem>
                        <asp:ListItem Value="-03-" Text="Март"></asp:ListItem>
                        <asp:ListItem Value="-04-" Text="Апрель"></asp:ListItem>
                        <asp:ListItem Value="-05-" Text="Май"></asp:ListItem>
                        <asp:ListItem Value="-06-" Text="Июнь"></asp:ListItem>
                        <asp:ListItem Value="-07-" Text="Июль"></asp:ListItem>
                        <asp:ListItem Value="-08-" Text="Август"></asp:ListItem>
                        <asp:ListItem Value="-09-" Text="Сентябрь"></asp:ListItem>
                        <asp:ListItem Value="-10-" Text="Октябрь"></asp:ListItem>
                        <asp:ListItem Value="-11-" Text="Ноябрь"></asp:ListItem>
                        <asp:ListItem Value="-12-" Text="Декабрь"></asp:ListItem>
                    </asp:DropDownList>

                    <label class="switch">
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <span class="slider round"></span>
                    </label>
                </div>

                <asp:Panel ID="Panel_finder" runat="server" CssClass="rightfind">
                    <asp:CheckBox ID="CheckBox_redact" runat="server" Text="Редактировать" CssClass="zone_box" Visible="false" OnCheckedChanged="CheckBox_redact_CheckedChanged" AutoPostBack="true" />
                    <button onclick="saveAsPDF();" Class="tablebtn3">Сохранить в PDF</button>

                    <asp:Panel ID="Panel_find" runat="server" CssClass="rightfind" Visible="false">
                        <asp:TextBox ID="TextBox_Ot" AutoComplete="off" TextMode="Date" CssClass="input2" style="width: 200px" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox_Do" AutoComplete="off" TextMode="Date" CssClass="input2" style="width: 200px" runat="server"></asp:TextBox>

                        <button id="Btn_poisk" runat="server" onserverclick="Btn_poisk_Click" style="border: none; background: none; cursor: pointer;">
                            <svg display="none">
                                <symbol id="svg-find" viewBox="0 0 612 612">
                                    <g>
                                        <path d="M382.5,0C255.759,0,153,102.759,153,229.5c0,53.034,18.149,101.707,48.367,140.568L8.415,563.021 C2.812,568.625,0,575.949,0,583.312c0,7.344,2.812,14.688,8.415,20.292C14,609.208,21.343,612,28.688,612 s14.688-2.792,20.272-8.396l192.971-192.972C280.793,440.851,329.467,459,382.5,459C509.241,459,612,356.241,612,229.5 S509.241,0,382.5,0z M382.5,401.625c-94.917,0-172.125-77.208-172.125-172.125c0-94.917,77.208-172.125,172.125-172.125 c94.917,0,172.125,77.208,172.125,172.125C554.625,324.417,477.417,401.625,382.5,401.625z"/>
                                    </g>
                                </symbol>
                            </svg>
                            <svg class="searchsvg" style="display:block">
                                <use href="#svg-find"></use>
                            </svg>
                        </button>

                        <button id="Btn_update" runat="server" onserverclick="Btn_update_Click" style="border: none; background: none; cursor: pointer;">
                            <svg display="none">
                                <symbol id="svg-refresh" viewBox="0 0 64 64">
                                    <g>
                                        <path d="M45.274,29.772c-1.094,-8.15 -8.075,-14.435 -16.525,-14.435c-9.203,0 -16.674,7.471 -16.674,16.674c0,9.203 7.471,16.675 16.674,16.675c4.423,0 8.664,-1.757 11.791,-4.884l2.862,2.863c-3.886,3.886 -9.157,6.069 -14.653,6.069c-11.437,0 -20.723,-9.285 -20.723,-20.723c0,-11.437 9.286,-20.723 20.723,-20.723c10.623,0 19.379,7.994 20.582,18.294l3.551,-3.551l3.118,3.117l-8.792,8.792l-8.796,-8.796l3.118,-3.117l3.744,3.745Z"/>
                                    </g>
                                </symbol>
                            </svg>
                            <svg class="searchsvg" style="width: 35px; height: 33px; padding: 2px; display: block;">
                                <use href="#svg-refresh"></use>
                            </svg>
                        </button>
                    </asp:Panel>
                </asp:Panel>
            </div>
                        <%-- ============================================================ --%>
            <%-- ГРАФИК 1: Статистика обращений граждан (по месяцам) --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("NameObject") %>',
                                    "test": '<%# Eval("Count") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    let obj = new Array();
                    let numb = new Array();
                    var bgColors = ['rgb(226, 43, 54)', 'rgb(0, 0, 0)', 'rgb(89, 89, 89)', 'rgb(127, 127, 127)', 'rgb(191, 191, 191)'];
                    for (var i = 0; i < markers.length; i++) {
                        obj[i] = markers[i].objectl;
                        numb[i] = markers[i].test;
                        if (i > 4) bgColors.push("rgb(191, 191, 191)");
                    }

                    const data = {
                        labels: obj,
                        datasets: [{
                            label: "Все ОГ",
                            backgroundColor: bgColors,
                            data: numb,
                        }]
                    };

                    const config = {
                        type: 'pie',
                        data: data,
                        options: {
                            plugins: { legend: { display: false } }
                        }
                    };
                    new Chart(document.getElementById('myChart'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 2: ТОП-1 объект по обращениям --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers2" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("FilterValue") %>',
                                    "test": '<%# Eval("Count") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    let obj = new Array();
                    let numb = new Array();
                    for (var i = 0; i < markers.length; i++) {
                        if (i < 4) {
                            obj[i] = markers[i].objectl;
                            numb[i] = markers[i].test;
                        }
                    }

                    const data = {
                        labels: obj,
                        datasets: [{
                            datalabels: { color: '#ffffff' },
                            label: "Все ОГ",
                            backgroundColor: ['rgb(226, 43, 54)', 'rgb(0, 0, 0)', 'rgb(89, 89, 89)', 'rgb(127, 127, 127)', 'rgb(191, 191, 191)'],
                            data: numb,
                        }]
                    };

                    const config = {
                        type: 'pie',
                        data: data,
                        options: {},
                        plugins: [ChartDataLabels]
                    };
                    new Chart(document.getElementById('mmyChart2'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 3: ТОП-2 объект по обращениям --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers5" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("FilterValue") %>',
                                    "test": '<%# Eval("Count") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    let obj = new Array();
                    let numb = new Array();
                    for (var i = 0; i < markers.length; i++) {
                        if (i == 0) {
                            var filt = markers[i].objectl;
                            var elem = document.getElementById('<%=Label_1Top.ClientID%>');
                            if (elem) elem.innerText = filt;
                        }
                        if (i < 4) {
                            obj[i] = markers[i].objectl;
                            numb[i] = markers[i].test;
                        }
                    }

                    const data = {
                        labels: obj,
                        datasets: [{
                            datalabels: { color: '#ffffff' },
                            label: "Все ОГ",
                            backgroundColor: ['rgb(226, 43, 54)', 'rgb(0, 0, 0)', 'rgb(89, 89, 89)', 'rgb(127, 127, 127)', 'rgb(191, 191, 191)'],
                            data: numb,
                        }]
                    };

                    const config = {
                        type: 'pie',
                        data: data,
                        options: {},
                        plugins: [ChartDataLabels]
                    };
                    new Chart(document.getElementById('myChart5'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 4: ТОП-3 объект по обращениям --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers6" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("FilterValue") %>',
                                    "test": '<%# Eval("Count") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    let obj = new Array();
                    let numb = new Array();
                    for (var i = 0; i < markers.length; i++) {
                        if (i == 0) {
                            var filt = markers[i].objectl;
                            var elem = document.getElementById('<%=Label_2Top.ClientID%>');
                            if (elem) elem.innerText = filt;
                        }
                        if (i < 4) {
                            obj[i] = markers[i].objectl;
                            numb[i] = markers[i].test;
                        }
                    }

                    const data = {
                        labels: obj,
                        datasets: [{
                            datalabels: { color: '#ffffff' },
                            label: "Все ОГ",
                            backgroundColor: ['rgb(226, 43, 54)', 'rgb(0, 0, 0)', 'rgb(89, 89, 89)', 'rgb(127, 127, 127)', 'rgb(191, 191, 191)'],
                            data: numb,
                        }]
                    };

                    const config = {
                        type: 'pie',
                        data: data,
                        options: {},
                        plugins: [ChartDataLabels]
                    };
                    new Chart(document.getElementById('myChart6'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 5: ТОП-4 объект по обращениям --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers7" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("FilterValue") %>',
                                    "test": '<%# Eval("Count") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    let obj = new Array();
                    let numb = new Array();
                    for (var i = 0; i < markers.length; i++) {
                        if (i == 0) {
                            var filt = markers[i].objectl;
                            var elem = document.getElementById('<%=Label_3Top.ClientID%>');
                            if (elem) elem.innerText = filt;
                        }
                        if (i < 4) {
                            obj[i] = markers[i].objectl;
                            numb[i] = markers[i].test;
                        }
                    }

                    const data = {
                        labels: obj,
                        datasets: [{
                            datalabels: { color: '#ffffff' },
                            label: "Все ОГ",
                            backgroundColor: ['rgb(226, 43, 54)', 'rgb(0, 0, 0)', 'rgb(89, 89, 89)', 'rgb(127, 127, 127)', 'rgb(191, 191, 191)'],
                            data: numb,
                        }]
                    };

                    const config = {
                        type: 'pie',
                        data: data,
                        options: {},
                        plugins: [ChartDataLabels]
                    };
                    new Chart(document.getElementById('myChart7'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 6: Увеличение обращений по направлениям --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers8" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("FilterValue") %>',
                                    "test": '<%# Eval("Total") %>',
                                    "jan": '<%# Eval("PrevMonth") %>',
                                    "feb": '<%# Eval("CurrentMonth") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    moment.locale('ru');
                    var oneMonthAgo = moment().subtract(1, 'months').format('MMMM');
                    var twoMonthAgo = moment().subtract(2, 'months').format('MMMM');
                    const result = oneMonthAgo.charAt(0).toUpperCase() + oneMonthAgo.slice(1);
                    const result2 = twoMonthAgo.charAt(0).toUpperCase() + twoMonthAgo.slice(1);

                    let obj = new Array(), numb = new Array(), janu = new Array(), febr = new Array();
                    for (var i = 0; i < markers.length; i++) {
                        obj[i] = markers[i].objectl;
                        numb[i] = markers[i].test;
                        janu[i] = markers[i].jan;
                        febr[i] = markers[i].feb;
                    }

                    const data = {
                        labels: obj,
                        datasets: [
                            { label: result2, data: janu, backgroundColor: ['rgb(226, 43, 54)'] },
                            { label: result, data: febr, backgroundColor: ['rgb(231, 231, 231)'] }
                        ]
                    };

                    const config = {
                        type: 'bar',
                        data: data,
                        options: {
                            scales: { x: { ticks: { font: { size: 8 } } } },
                            elements: { bar: { borderWidth: 2 } },
                            responsive: true,
                            plugins: {
                                legend: { position: 'left' },
                                title: {
                                    display: true,
                                    text: 'Увеличение количества обращений по направлениям',
                                    font: { size: 18, family: 'GOTHAPROREG' },
                                    padding: { bottom: 30 }
                                }
                            }
                        }
                    };
                    new Chart(document.getElementById('myChart8'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 7: Уменьшение обращений по направлениям --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers9" runat="server">
                            <ItemTemplate>
                                {
                                    "objectl": '<%# Eval("FilterValue") %>',
                                    "test": '<%# Eval("Total") %>',
                                    "jan": '<%# Eval("PrevMonth") %>',
                                    "feb": '<%# Eval("CurrentMonth") %>'
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    moment.locale('ru');
                    var oneMonthAgo = moment().subtract(1, 'months').format('MMMM');
                    var twoMonthAgo = moment().subtract(2, 'months').format('MMMM');
                    const result = oneMonthAgo.charAt(0).toUpperCase() + oneMonthAgo.slice(1);
                    const result2 = twoMonthAgo.charAt(0).toUpperCase() + twoMonthAgo.slice(1);

                    let obj = new Array(), numb = new Array(), janu = new Array(), febr = new Array();
                    for (var i = 0; i < markers.length; i++) {
                        obj[i] = markers[i].objectl;
                        numb[i] = markers[i].test;
                        janu[i] = markers[i].jan;
                        febr[i] = markers[i].feb;
                    }

                    const data = {
                        labels: obj,
                        datasets: [
                            { label: result2, data: janu, backgroundColor: ['rgb(226, 43, 54)'] },
                            { label: result, data: febr, backgroundColor: ['rgb(231, 231, 231)'] }
                        ]
                    };

                    const config = {
                        type: 'bar',
                        data: data,
                        options: {
                            scales: { x: { ticks: { font: { size: 8 } } } },
                            elements: { bar: { borderWidth: 2 } },
                            responsive: true,
                            plugins: {
                                legend: { position: 'left' },
                                title: {
                                    display: true,
                                    text: 'Уменьшение количества обращений по направлениям',
                                    font: { size: 18, family: 'GOTHAPROREG' },
                                    padding: { bottom: 30 }
                                }
                            }
                        }
                    };
                    new Chart(document.getElementById('myChart9'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- ГРАФИК 8: Динамика поступивших ОГ --%>
            <%-- ============================================================ --%>
            <script>
                $(document).ready(function () {
                    var markers = [
                        <asp:Repeater ID="rptMarkers3" runat="server">
                            <ItemTemplate>
                                {
                                    "istoch": '<%# Eval("Istock") %>',
                                    "m11": <%# Eval("M11") %>,
                                    "m10": <%# Eval("M10") %>,
                                    "m9": <%# Eval("M9") %>,
                                    "m8": <%# Eval("M8") %>,
                                    "m7": <%# Eval("M7") %>,
                                    "m6": <%# Eval("M6") %>,
                                    "m5": <%# Eval("M5") %>,
                                    "m4": <%# Eval("M4") %>,
                                    "m3": <%# Eval("M3") %>,
                                    "m2": <%# Eval("M2") %>,
                                    "m1": <%# Eval("M1") %>
                                }
                            </ItemTemplate>
                            <SeparatorTemplate>,</SeparatorTemplate>
                        </asp:Repeater>
                    ];

                    let label = new Array();
                    for (let i = 11; i > 0; i--) {
                        label.push(moment().subtract(i, 'months').format('MMMM').charAt(0).toUpperCase() + moment().subtract(i, 'months').format('MMMM').slice(1) + ' ' + moment().subtract(i, 'months').format('yyyy'));
                    }

                    let vse_mos = [], vse_tik = [], vse_pomosh = [];
                    for (var i = 0; i < markers.length; i++) {
                        var arr = [markers[i].m11, markers[i].m10, markers[i].m9, markers[i].m8, markers[i].m7, markers[i].m6, markers[i].m5, markers[i].m4, markers[i].m3, markers[i].m2, markers[i].m1];
                        if (markers[i].istoch === 'MОСЭДО') vse_mos = arr;
                        else if (markers[i].istoch === 'Тикеты') vse_tik = arr;
                        else if (markers[i].istoch === 'Помощь') vse_pomosh = arr;
                    }

                    let itog = vse_mos.concat(vse_tik).concat(vse_pomosh).reduce(function (sum, elem) { return sum + Number(elem); }, 0);

                    const data = {
                        labels: label,
                        datasets: [
                            { datalabels: { color: '#ffffff' }, label: 'MОСЭДО', data: vse_mos, backgroundColor: ['rgb(127, 127, 127)'] },
                            { datalabels: { color: '#ffffff' }, label: 'Тикеты', data: vse_tik, backgroundColor: ['rgb(226, 43, 54)'] },
                            { datalabels: { color: '#ffffff' }, label: 'Помощь', data: vse_pomosh, backgroundColor: ['rgb(10, 10, 10)'] }
                        ]
                    };

                    const config = {
                        type: 'bar',
                        data: data,
                        plugins: [ChartDataLabels],
                        options: {
                            plugins: { title: { display: true, text: 'Итого: ' + String(itog) } },
                            responsive: true,
                            scales: { x: { stacked: true }, y: { stacked: true } }
                        }
                    };
                    new Chart(document.getElementById('myChart3'), config);
                });
            </script>

            <%-- ============================================================ --%>
            <%-- PDF-ЭКСПОРТ --%>
            <%-- ============================================================ --%>
            <script>
                function saveAsPDF() {
                    doc = '';
                    html2canvas(document.getElementById('<%=Panel_obrasch.ClientID%>'), {
                        onrendered: function (canvas) {
                            var img = canvas.toDataURL("image/png", 1.0);
                            doc = new jsPDF("p", "mm", "a4");
                            doc.addImage(img, 'PNG', 5, 5, 200, 300);
                            doc.setProperties({ title: "Инфографика" });
                        }
                    });
                    html2canvas(document.getElementById('<%=Panel_Top.ClientID%>'), {
                        onrendered: function (canvas) {
                            var img = canvas.toDataURL("image/png", 1.0);
                            doc.addPage();
                            doc.addImage(img, 'PNG', 5, 5, 200, 130);
                        }
                    });
                    html2canvas(document.getElementById('<%=Panel_sravnenie.ClientID%>'), {
                        onrendered: function (canvas) {
                            var img = canvas.toDataURL("image/png", 1.0);
                            doc.addPage();
                            doc.addImage(img, 'PNG', 5, 5, 200, 130);
                        }
                    });
                    html2canvas(document.getElementById('<%=Panel_dynamics.ClientID%>'), {
                        onrendered: function (canvas) {
                            var img = canvas.toDataURL("image/png", 1.0);
                            doc.addPage();
                            doc.addImage(img, 'PNG', 5, 5, 200, 130);
                        }
                    });
                    html2canvas(document.getElementById('<%=Panel_sootnosh.ClientID%>'), {
                        onrendered: function (canvas) {
                            var img = canvas.toDataURL("image/png", 1.0);
                            doc.addPage();
                            doc.addImage(img, 'PNG', 5, 5, 200, 60);
                        }
                    });
                    html2canvas(document.getElementById('<%=Panel_text.ClientID%>'), {
                        onrendered: function (canvas) {
                            var img = canvas.toDataURL("image/png", 1.0);
                            doc.addPage();
                            doc.addImage(img, 'PNG', 5, 5, 200, 100);
                            var a = document.createElement("a");
                            document.body.appendChild(a);
                            a.style = "display: none";
                            var blob = doc.output('blob');
                            var blobURL = URL.createObjectURL(blob);
                            a.href = blobURL;
                            a.download = 'Инфографика.pdf';
                            a.click();
                            window.URL.revokeObjectURL(url);
                        }
                    });
                }
            </script>
                        <%-- ============================================================ --%>
            <%-- СТАТИСТИКА ОБРАЩЕНИЙ ГРАЖДАН --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="Panel_obrasch" runat="server">
                <section class="finder">
                    <asp:Label ID="Label_name" runat="server" CssClass="withrotate h3" style="display: block; padding-left: unset">
                        СТАТИСТИКА ОБРАЩЕНИЙ ГРАЖДАН ЗА
                        <asp:Label ID="Label6" runat="server" style="text-transform:uppercase"></asp:Label>
                    </asp:Label>
                </section>

                <div style="display:flex; flex-direction:row;">
                    <div class="allgr media">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" GridLines="None" table-layout="fixed" ShowFooter="true" ShowHeader="false" CssClass="hide" style="margin-top: unset; text-align: center" runat="server" OnRowCommand="GridView1_RowCommand">
                                    <HeaderStyle Width="100px" Height="40px" />
                                    <RowStyle Height="45px" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" AutoPostBack="true" CommandName="Hide" Text="Скрыть таблицу"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" AutoPostBack="true" CommandName="Show" Text="Раскрыть таблицу"></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle Height="45px" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div>
                            <asp:Label ID="Label_itogo" runat="server" Text="ИТОГО: " style="font-family: Mossport; font-size: 32px; color: #7f7f7f; display: inline-block" Visible="false"></asp:Label>
                            <asp:Label ID="Label_kol_vo" runat="server" style="font-family: Mossport; font-size: 32px; color: #E22B36; display: inline-block" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <asp:Panel ID="Panel_statistic" runat="server" Visible="false" style="margin-left: 60px" CssClass="chart">
                        <canvas id="myChart"></canvas>
                    </asp:Panel>
                </div>
            </asp:Panel>

            <%-- ============================================================ --%>
            <%-- ТОП-3 ОБЪЕКТОВ ПО ОБРАЩЕНИЯМ --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="Panel_Top" runat="server">
                <section class="finder">
                    <asp:Label ID="Label8" runat="server" CssClass="withrotate h3" style="display: block; padding-left: unset">
                        ТОП-3 ОБЪЕКТОВ ПО ОБРАЩЕНИЯМ ЗА
                        <asp:Label ID="Label9" runat="server" style="text-transform:uppercase"></asp:Label>
                    </asp:Label>
                </section>

                <div style="display: flex; flex-direction:column;">
                    <div style="display :flex" class="media">
                        <asp:GridView ID="GridView2" GridLines="None" table-layout="fixed" style="padding: 20px;" CssClass="Tab_obj gv2" runat="server" ShowHeader="false">
                            <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                            <RowStyle CssClass="row2" Height="45px" />
                        </asp:GridView>

                        <asp:Panel ID="Panel_Top1" runat="server" Visible="false" style="display: flex; flex-direction: column; align-items: center;" CssClass="gvtop1">
                            <canvas id="mmyChart2" class="canv"></canvas>
                            <asp:Label ID="Label_Top1" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        </asp:Panel>

                        <asp:Panel ID="Panel_Top2" runat="server" Visible="false" style="display: flex; flex-direction: column; align-items: center;" CssClass="gvtop24">
                            <canvas id="myChart5" class="canv"></canvas>
                            <asp:Label ID="Label_Top2" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        </asp:Panel>
                    </div>

                    <div style="display :flex; justify-content: center; margin-top: -50px;" class="media">
                        <asp:Panel ID="Panel_Top3" runat="server" Visible="false" style="display: flex; flex-direction: column; align-items: center;" CssClass="gvtop3">
                            <canvas id="myChart6" class="canv"></canvas>
                            <asp:Label ID="Label_Top3" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        </asp:Panel>

                        <asp:Panel ID="Panel_Top4" runat="server" Visible="false" style="display: flex; flex-direction: column; align-items: center;" CssClass="gvtop24">
                            <canvas id="myChart7" class="canv"></canvas>
                            <asp:Label ID="Label_Top4" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <%-- ============================================================ --%>
            <%-- СРАВНЕНИЕ ОБРАЩЕНИЙ ГРАЖДАН --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="Panel_sravnenie" runat="server">
                <section class="finder">
                    <asp:Label ID="Label11" runat="server" CssClass="withrotate h3" style="display: block; padding-left: unset">
                        СРАВНЕНИЕ ОБРАЩЕНИЙ ГРАЖДАН ДВУХ КРАЙНИХ МЕСЯЦЕВ ПО НАПРАВЛЕНИЯМ
                    </asp:Label>
                </section>
                <div>
                    <canvas id="myChart8"></canvas>
                </div>
                <div style="margin-top: 40px">
                    <canvas id="myChart9"></canvas>
                </div>
            </asp:Panel>

            <%-- ============================================================ --%>
            <%-- ДИНАМИКА ПОСТУПИВШИХ ОГ ЗА ВЕСЬ ПЕРИОД --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="Panel_dynamics" runat="server">
                <section class="finder">
                    <asp:Label ID="Label12" runat="server" CssClass="withrotate h3" style="display: block; padding-left: unset">
                        ДИНАМИКА ПОСТУПИВШИХ ОГ ЗА ВЕСЬ ПЕРИОД
                    </asp:Label>
                </section>
                <canvas id="myChart3"></canvas>
            </asp:Panel>

            <div class="test">
                <asp:GridView ID="GridView_dynamics" CssClass="Tab_obj2" GridLines="None" runat="server" OnRowCreated="GridView_dynamics_RowCreated" OnRowDataBound="GridView_dynamics_RowDataBound">
                    <HeaderStyle CssClass="th_table2" Width="100px" Height="40px" />
                    <RowStyle CssClass="row" Height="45px" />
                </asp:GridView>
            </div>

            <asp:Button ID="Button_update2" runat="server" Text="Обновить" OnClick="Button_update2_Click" AutoPostBack="True" CssClass="submitbtn" />
            <br /><br />

            <%-- ============================================================ --%>
            <%-- СООТНОШЕНИЕ ОБОСНОВАННЫХ ЖАЛОБ --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="Panel_sootnosh" runat="server">
                <section class="finder">
                    <asp:Label ID="Label10" runat="server" CssClass="withrotate h3" style="text-transform:uppercase; display: block; padding-left: unset"></asp:Label>
                </section>

                <div class="test">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView_Redakt" runat="server" CssClass="Tab_obj2 skryt" AutoGenerateColumns="false" OnRowDataBound="GridView_Redakt_RowDataBound" GridLines="None" OnRowCreated="GridView_Redakt_RowCreated" OnRowCommand="GridView_Redakt_RowCommand" OnRowDeleting="GridView_Redakt_RowDeleting">
                                <HeaderStyle CssClass="th_table2" Width="100px" Height="40px" />
                                <RowStyle CssClass="row" Height="45px" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" />
                                    <asp:BoundField DataField="TypeObr" HeaderText="Тип обращения" />
                                    <asp:BoundField DataField="ObjectName" HeaderText="Объект" />
                                    <asp:BoundField DataField="Vopros" HeaderText="Вопрос" />
                                    <asp:BoundField DataField="Zayavitel" HeaderText="Заявитель" />
                                    <asp:BoundField DataField="Rezultat" HeaderText="Результат" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="AddNew" AutoPostBack="True">Добавить строку</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Link" ShowDeleteButton="true" DeleteText="Удалить" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <asp:Button ID="Button_update" runat="server" Text="Обновить" OnClick="Button_update_Click" AutoPostBack="true" CssClass="submitbtn" />

                <div class="test">
                    <asp:GridView ID="GridView4_NoRedakt" AutoGenerateColumns="false" style="text-align:center; min-width: 1000px" GridLines="None" runat="server">
                        <HeaderStyle Width="100px" Height="40px" />
                        <RowStyle CssClass="rowsootn" Height="45px" />
                        <Columns>
                            <asp:BoundField DataField="TypeObr" HeaderText="Тип обращения" />
                            <asp:BoundField DataField="ObjectName" HeaderText="Объект" />
                            <asp:BoundField DataField="Vopros" HeaderText="Вопрос" />
                            <asp:BoundField DataField="Zayavitel" HeaderText="Заявитель" />
                            <asp:BoundField DataField="Rezultat" HeaderText="Результат" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <br /><br />

            <%-- ============================================================ --%>
            <%-- ТЕКСТОВАЯ СПРАВКА --%>
            <%-- ============================================================ --%>
            <asp:Panel ID="Panel_text" runat="server">
                <section class="finder">
                    <asp:Label ID="Label16" runat="server" CssClass="withrotate h3" style="text-transform:uppercase; display: block; padding-left: unset" Text="Текстовая справка"></asp:Label>
                </section>

                <div class="test">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView_Spravka_Redakt" runat="server" GridLines="None" CssClass="Tab_obj2 skryt" AutoGenerateColumns="false" OnRowCreated="GridView_Spravka_Redakt_RowCreated" OnRowDataBound="GridView_Spravka_Redakt_RowDataBound" OnRowCommand="GridView_Spravka_Redakt_RowCommand" OnRowDeleting="GridView_Spravka_Redakt_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" />
                                    <asp:BoundField DataField="TextSpravki" HeaderText="Текст" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="AddNew2" AutoPostBack="True">Добавить строку</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Link" ShowDeleteButton="true" DeleteText="Удалить" />
                                </Columns>
                                <HeaderStyle CssClass="th_table2" Width="100px" Height="40px" />
                                <RowStyle CssClass="row" Height="45px" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <asp:Button ID="Button_update3" runat="server" Text="Обновить" CssClass="submitbtn" OnClick="Button_update3_Click" />

                <asp:Label ID="Label17" runat="server" style="max-width: 450px; display: inline-block; font-size: 14px; margin-bottom: 20px" CssClass="name_object" Text="По итогам рассмотренных Общим отделом обращений граждан, поступивших в ГБУ МосСпортОбъект, наиболее частыми вопросами являлись:"></asp:Label>

                <div style="display: flex;" class="media2">
                    <div style="display: flex; flex-direction: column; height: 175px;">
                        <asp:Label ID="Label_Name1_obrasch" runat="server" Text="" CssClass="name_object obrasch" Visible="false"></asp:Label>
                        <asp:Label ID="Label_Name2_obrasch" runat="server" Text="" CssClass="name_object obrasch" Visible="false"></asp:Label>
                        <asp:Label ID="Label_Name3_obrasch" runat="server" Text="" CssClass="name_object obrasch" Visible="false"></asp:Label>
                    </div>

                    <div style="display: flex; flex-direction: column; height: 175px; border-left: 1px solid #cfcfcf; padding-left: 10px;" class="kolobr">
                        <asp:Label ID="Label_Count1_obrasch" runat="server" Text="" Visible="false" style="font-family: Mossport; font-size: 26px; color: #E22B36; margin-top: 20px; display: inline-block; letter-spacing: 1px"></asp:Label>
                        <asp:Label ID="Label_Count2_obrasch" runat="server" Text="" Visible="false" style="font-family: Mossport; font-size: 26px; color: #E22B36; margin-top: 20px; display: inline-block; letter-spacing: 1px"></asp:Label>
                        <asp:Label ID="Label_Count3_obrasch" runat="server" Text="" Visible="false" style="font-family: Mossport; font-size: 26px; color: #E22B36; margin-top: 20px; display: inline-block; letter-spacing: 1px"></asp:Label>
                    </div>

                    <div style="max-width: 670px" class="gvspravka">
                        <asp:GridView ID="GridView_Spravka_Noredakt" runat="server" GridLines="None" CssClass="skryt spravka" AutoGenerateColumns="false" ShowHeader="false">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="TextSpravki" HeaderText="Текст" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <asp:Label ID="Label18" runat="server" Text="Наибольшее количество обращений поступило в отношении:" CssClass="name_object obrasch" style="display: block; text-align: center; margin-top: 40px"></asp:Label>

                <div style="display: flex; max-width: 1170px; justify-content: space-around; margin-top: 20px" class="media">
                    <div style="display: flex; flex-direction: column; align-items: center; border-bottom: 1px solid #cfcfcf; padding-bottom: 10px;">
                        <asp:Label ID="Label_Name_Top1" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        <asp:Label ID="Label_Count_Top1" runat="server" Text="" Visible="false" style="font-family: Mossport; font-size: 28px; color: #E22B36; margin-top: 20px; display: inline-block; letter-spacing: 1px"></asp:Label>
                        <asp:Label ID="Label_1Top" CssClass="name_object obrasch" style="font-size:12px; margin-top:15px" runat="server"></asp:Label>
                    </div>

                    <div style="display: flex; flex-direction: column; align-items: center; border-bottom: 1px solid #cfcfcf; padding-bottom: 10px;">
                        <asp:Label ID="Label_Name_Top2" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        <asp:Label ID="Label_Count_Top2" runat="server" Text="" Visible="false" style="font-family: Mossport; font-size: 28px; color: #E22B36; margin-top: 20px; display: inline-block; letter-spacing: 1px"></asp:Label>
                        <asp:Label ID="Label_2Top" CssClass="name_object obrasch" style="font-size:12px; margin-top:15px" runat="server" Text=""></asp:Label>
                    </div>

                    <div style="display: flex; flex-direction: column; align-items: center; border-bottom: 1px solid #cfcfcf; padding-bottom: 10px;">
                        <asp:Label ID="Label_Name_Top3" runat="server" Text="" CssClass="name_object" Visible="false"></asp:Label>
                        <asp:Label ID="Label_Count_Top3" runat="server" Text="" Visible="false" style="font-family: Mossport; font-size: 28px; color: #E22B36; margin-top: 20px; display: inline-block; letter-spacing: 1px"></asp:Label>
                        <asp:Label ID="Label_3Top" CssClass="name_object obrasch" style="font-size:12px; margin-top:15px" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>
</asp:Content>