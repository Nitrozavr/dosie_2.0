<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Budget.aspx.cs" Inherits="Site.Budget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/canvas-0.4.1.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    .empty { height: 100px; }

    .budget_blockmain { display: flex; flex-direction: column; }
    .budget_blockmain_block { display: flex; flex-direction: column; }

    /*ФИО*/
    .block_ddl_kfo .chosen-container-multi .chosen-choices { overflow: auto; max-height: 110px; }
    .block_ddl_kfo .chosen-container { width: 150px !important; }

    .block_ddl_datee .chosen-container-multi .chosen-choices { overflow: auto; max-height: 172px; }
    .block_ddl_datee .chosen-container { width: 150px !important; }
    .block_ddl_datee .chosen-container .chosen-results { height: 140px !important; }

    .block_ddl_dir .chosen-container-multi .chosen-choices { overflow: auto; max-height: 248px; }
    .block_ddl_dir .chosen-container { width: 340px !important; }
    .block_ddl_dir .chosen-container .chosen-results { max-height: 150px; }

    /*КОСГУ*/
    .block_ddl_kfo2 .chosen-container-multi .chosen-choices { overflow: auto; max-height: 110px; }
    .block_ddl_kfo2 .chosen-container { width: 150px !important; }

    .block_ddl_datee2 .chosen-container-multi .chosen-choices { overflow: auto; max-height: 172px; }
    .block_ddl_datee2 .chosen-container { width: 150px !important; }
    .block_ddl_datee2 .chosen-container .chosen-results { height: 140px !important; }

    .block_ddl_kosgu .chosen-container-multi .chosen-choices { overflow: auto; max-height: 248px; }
    .block_ddl_kosgu .chosen-container { width: 340px !important; }
    .block_ddl_kosgu .chosen-container .chosen-results { max-height: 150px; }

    .check_style input[type="checkbox"]:checked + label {
        background: rgb(175 175 175);
        color: #ffffff;
        border: 1px solid transparent;
        cursor: pointer;
        border-radius: 5px;
        padding: 10px;
    }

    .check_style input[type="checkbox"] + label {
        margin-right: 0px;
        margin-bottom: 0px;
        padding: 10px;
        border: 1px solid #dcdfe6;
        border-radius: 5px;
        color: #606266;
        background: white;
        font-size: 10px;
        box-shadow: 0px 2px 6px rgb(0 0 0 / 9%);
        min-width: -webkit-fill-available;
        min-height: 30px;
    }

    .chk_menu .check_style input[type="checkbox"] + label {
        text-align: left;
        align-items: center;
        justify-content: flex-end;
    }

    .check_style input[type="checkbox"]:hover + label {
        background: rgb(160 160 160);
        color: #ffffff;
        border: 1px solid transparent;
        cursor: pointer;
        border-radius: 5px;
        padding: 10px;
    }

    .chk_scroll_budget {
        height: 365px;
        overflow: auto;
        border: 1px solid rgb(195 195 195 / 35%);
        padding: 2px;
        border-radius: 5px;
    }

    .chk_scroll_kosgu {
        height: 245px;
        overflow: auto;
        border: 1px solid rgb(195 195 195 / 35%);
        padding: 2px;
        border-radius: 5px;
    }

    .budget_substrate {
        display: flex;
        flex-direction: column;
        position: relative;
        padding: 15px 15px;
        border-radius: 20px;
        border-bottom: 1px solid transparent;
        border-top: 1px solid transparent;
        margin-top: 20px;
        margin-bottom: 20px;
        box-shadow: 0px 0px 10px 0px rgb(35 30 60 / 20%);
        justify-content: space-between;
    }

    .th_table { font-size: 13px; color: #000000; border-bottom: 1px solid #afafaf; border-top: none; }
    .Tab_obj tr:hover { border-bottom: 1px solid #f8913b; border-top: none; }

    .chk_scroll_menu::-webkit-scrollbar { width: 5px; }
    .chk_scroll_menu::-webkit-scrollbar-track { background: #ededed6e; border-radius: 15px; }
    .chk_scroll_menu::-webkit-scrollbar-thumb { background-color: rgb(179 179 179); border-radius: 20px; }

    .Tab_obj th:nth-child(n+1):nth-child(-n+1) { padding: 16px; width: 20%; }
    .Tab_obj td:nth-child(n+1):nth-child(-n+1) { padding: 2px; text-align: left; }
    .Tab_obj th:nth-child(n+2):nth-child(-n+3) { width: 150px; padding: 16px; }
    .Tab_obj td:nth-child(n+2):nth-child(-n+3) { width: 150px; padding: 2px; }
    .Tab_obj th:nth-child(n+4) { width: 120px; padding: 5px; }
    .Tab_obj td:nth-child(n+4) { width: 120px; padding: 5px; }

    .th_table a { cursor: pointer; color: black; }
    .th_table th a { display: grid; grid-template-columns: auto 0.1fr; align-items: center; }

    .bold { font-weight: 900; text-align: center; }
    .svg_budget { width: 15px; }
    .zone_box input[type="checkbox"] + label { width: 150px; }

    .withrotate {
        font-size: 12px;
        border-bottom: none;
        padding: 10px;
        color: #adadad;
        text-transform: uppercase;
    }

    h3 {
        font-family: GOTHAPROMED;
        font-size: 28px;
        color: #272B37;
        letter-spacing: 0.5px;
        padding: 20px 0;
    }

    .switch { margin-left: 15px; }

    .head__menu .inputt__file-button {
        max-width: 200px;
        padding: 10px;
        background: white;
        color: #606266;
        border: 1px solid #dcdfe6;
        font-family: GOTHAPROREG;
        font-size: 12px;
        display: flex;
        align-items: center;
        justify-content: flex-start;
        border-radius: 5px;
        cursor: pointer;
        height: 35px;
    }

    .head__menu .inputt__file-button:hover {
        background: #fdebeb;
        color: #f8913b;
        border: 1px solid #f9c2c2;
        cursor: pointer;
    }

    .head__menu .inputt__file-button-text {
        line-height: 1;
        margin-top: 1px;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        cursor: pointer;
        font-family: GOTHAPROREG;
        font-size: 12px;
    }

    .csv {
        max-width: 200px;
        padding: 10px;
        background: white;
        color: #606266;
        border: 1px solid #dcdfe6;
        font-family: GOTHAPROREG;
        font-size: 12px;
        display: flex;
        align-items: center;
        justify-content: flex-start;
        border-radius: 5px;
        cursor: pointer;
        height: 35px;
    }

    .csv:hover {
        background: #fdebeb;
        color: #f8913b;
        border: 1px solid #f9c2c2;
        cursor: pointer;
    }

    .head__menu .selectric {
        border: 1px solid #dcdfe6;
        border-radius: 5px;
        background: #FFF;
        position: relative;
        overflow: hidden;
        margin-top: 10px;
        margin-bottom: 10px;
    }

    .head__menu .selectric-wrapper { position: relative; cursor: pointer; padding: 0; }
    .chk_menu .check__ind input[type="checkbox"] + label { margin-right: 2px; margin-left: 2px; }
</style>

<%-- СКРИПТ ДЛЯ ГРАФИКОВ --%>
<script type="text/javascript">
    function myjava() {
        let kfo = document.getElementById('<%= HiddenField_kfo.ClientID %>').textContent;
        let datee = document.getElementById('<%= HiddenField_datee.ClientID %>').textContent;
        let dir = document.getElementById('<%= HiddenField_dir.ClientID %>').textContent;

        let data0 = { "data0": kfo, "data1": datee, "data2": dir };
        $.ajax({
            type: "POST",
            url: "Budget.aspx/GetGraphData",
            data: JSON.stringify(data0),
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            success: function (myresult) {
                const array2 = [];
                function convArray(array, col) {
                    while (array.length) array2.push(array.splice(0, col));
                }
                convArray(myresult.d, 5);

                let sumlimitt = 0, sumne_osvoyeny_limity = 0, sumzaklyuchennyye_dogovory = 0,
                    sumoplacheny_dogovory = 0, sumdogovory_v_protsesse_zaklyucheniya = 0;
                for (var i = 0; i < array2.length; i++) {
                    sumlimitt += parseFloat(array2[i][0]);
                    sumne_osvoyeny_limity += parseFloat(array2[i][1]);
                    sumzaklyuchennyye_dogovory += parseFloat(array2[i][2]);
                    sumoplacheny_dogovory += parseFloat(array2[i][3]);
                    sumdogovory_v_protsesse_zaklyucheniya += parseFloat(array2[i][4]);
                }

                const data2 = {
                    labels: ["", "", ""],
                    datasets: [{
                        data: [sumlimitt.toFixed(2), '', ''],
                        backgroundColor: "rgba(248, 145, 59, 1)",
                        label: "Лимиты",
                        datalabels: { color: '#ffffff' },
                        barPercentage: 0.7
                    }, {
                        data: ['', sumzaklyuchennyye_dogovory.toFixed(2), ''],
                        backgroundColor: "rgba(143, 170, 220, 1)",
                        label: "Заключены договоры",
                        datalabels: { color: '#ffffff' },
                        barPercentage: 0.7
                    }, {
                        data: ['', sumdogovory_v_protsesse_zaklyucheniya.toFixed(2), ''],
                        backgroundColor: "rgba(255, 217, 102, 1)",
                        label: "Договоры в процессе заключения",
                        datalabels: { color: '#ffffff' },
                        barPercentage: 0.7
                    }, {
                        data: ['', sumne_osvoyeny_limity.toFixed(2), ''],
                        backgroundColor: "rgba(164, 164, 164, 1)",
                        label: "Не освоены лимиты",
                        datalabels: { color: '#ffffff' },
                        barPercentage: 0.7
                    }, {
                        data: ['', '', sumoplacheny_dogovory.toFixed(2)],
                        backgroundColor: "rgba(169, 209, 142, 1)",
                        label: "Оплачены договоры",
                        datalabels: { color: '#ffffff' },
                        barPercentage: 0.7,
                    }]
                };

                const config = {
                    type: 'bar',
                    data: data2,
                    plugins: [ChartDataLabels],
                    options: {
                        interaction: { mode: 'point' },
                        scales: {
                            x: { ticks: { display: true }, stacked: true },
                            y: { ticks: { display: true }, stacked: true, grid: { display: false } }
                        },
                        indexAxis: 'y',
                        plugins: {
                            title: { display: false, text: 'Исполнение бюджета' },
                            datalabels: {
                                formatter: (value, ctx) => {
                                    const test = ctx.chart.data.datasets[ctx.datasetIndex].data[ctx.dataIndex];
                                    if (test != '') {
                                        const test2 = test / sumlimitt * 100;
                                        return `${test2.toFixed(2)}%\n${test}`;
                                    }
                                }
                            },
                            legend: { labels: { font: { size: 9 } } }
                        }
                    }
                };
                new Chart(document.getElementById('myChart'), config);
            },
            error: function (xhr) { console.error(xhr.status + ': ' + xhr.statusText); }
        });

        // === Второй график ===
        let kfo2 = document.getElementById('<%= HiddenField_kfo2.ClientID %>').textContent;
        let datee2 = document.getElementById('<%= HiddenField_datee2.ClientID %>').textContent;
        let kosgu = document.getElementById('<%= HiddenField_kosgu.ClientID %>').textContent;
        let chk_cred = document.getElementById('<%= CheckBox_kred.ClientID %>').checked;

        let data1 = { "data0": kfo2, "data1": datee2, "data2": kosgu, "cred": chk_cred };

        $.ajax({
            type: "POST",
            url: "Budget.aspx/GetGraphData2",
            data: JSON.stringify(data1),
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            success: function (myresult) {
                const array2 = [];
                function convArray(array, col) {
                    while (array.length) array2.push(array.splice(0, col));
                }
                convArray(myresult.d, 7);

                let sumlimitt = 0, sumne_osvoyeny_limity = 0, sumzaklyuchennyye_dogovory = 0,
                    sumoplacheny_dogovory = 0, sumdogovory_v_protsesse_zaklyucheniya = 0,
                    kred = 0, paid_kred = 0;
                for (var i = 0; i < array2.length; i++) {
                    sumlimitt += parseFloat(array2[i][0]);
                    sumne_osvoyeny_limity += parseFloat(array2[i][1]);
                    sumzaklyuchennyye_dogovory += parseFloat(String(array2[i][2]).replace(',', '.'));
                    sumoplacheny_dogovory += parseFloat(array2[i][3]);
                    sumdogovory_v_protsesse_zaklyucheniya += parseFloat(array2[i][4]);
                    kred += parseFloat(array2[i][5]);
                    paid_kred += parseFloat(array2[i][6]);
                }

                const data2 = {
                    labels: ["", "", ""],
                    datasets: [
                        { data: [sumlimitt.toFixed(2), '', ''], backgroundColor: "rgba(248, 145, 59, 1)", label: "Лимиты", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', sumzaklyuchennyye_dogovory.toFixed(2), ''], backgroundColor: "rgba(143, 170, 220, 1)", label: "Заключены договоры", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', sumdogovory_v_protsesse_zaklyucheniya.toFixed(2), ''], backgroundColor: "rgba(255, 217, 102, 1)", label: "Договоры в процессе заключения", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', sumne_osvoyeny_limity.toFixed(2), ''], backgroundColor: "rgba(164, 164, 164, 1)", label: "Не освоены лимиты", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', kred.toFixed(2), ''], backgroundColor: "rgba(255, 69, 0, 1)", label: "Кредиторская задолженность", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', '', sumoplacheny_dogovory.toFixed(2)], backgroundColor: "rgba(169, 209, 142, 1)", label: "Оплачены договоры", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', '', paid_kred.toFixed(2)], backgroundColor: "rgba(0, 100, 0, 1)", label: "Оплачена кредиторская задолженность", datalabels: { color: '#ffffff' }, barPercentage: 0.7 }
                    ]
                };

                const data3 = {
                    labels: ["", "", ""],
                    datasets: [
                        { data: [sumlimitt.toFixed(2), '', ''], backgroundColor: "rgba(248, 145, 59, 1)", label: "Лимиты", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', sumzaklyuchennyye_dogovory.toFixed(2), ''], backgroundColor: "rgba(143, 170, 220, 1)", label: "Заключены договоры", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', sumdogovory_v_protsesse_zaklyucheniya.toFixed(2), ''], backgroundColor: "rgba(255, 217, 102, 1)", label: "Договоры в процессе заключения", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', sumne_osvoyeny_limity.toFixed(2), ''], backgroundColor: "rgba(164, 164, 164, 1)", label: "Не освоены лимиты", datalabels: { color: '#ffffff' }, barPercentage: 0.7 },
                        { data: ['', '', sumoplacheny_dogovory.toFixed(2)], backgroundColor: "rgba(169, 209, 142, 1)", label: "Оплачены договоры", datalabels: { color: '#ffffff' }, barPercentage: 0.7 }
                    ]
                };

                const config = {
                    type: 'bar',
                    data: (document.getElementById('<%= CheckBox_kred.ClientID %>').checked) ? data2 : data3,
                    plugins: [ChartDataLabels],
                    options: {
                        interaction: { mode: 'point' },
                        scales: {
                            x: { ticks: { display: true }, stacked: true },
                            y: { ticks: { display: false }, stacked: true, grid: { display: false } }
                        },
                        indexAxis: 'y',
                        plugins: {
                            title: { display: false, text: 'Исполнение бюджета' },
                            datalabels: {
                                formatter: (value, ctx) => {
                                    const test = ctx.chart.data.datasets[ctx.datasetIndex].data[ctx.dataIndex];
                                    if (test != '') {
                                        const test2 = test / sumlimitt * 100;
                                        return `${test2.toFixed(2)}%\n${test}`;
                                    }
                                }
                            },
                            legend: { labels: { font: { size: 9 } } }
                        }
                    }
                };
                new Chart(document.getElementById('myChart2'), config);
            },
            error: function (xhr) { console.error(xhr.status + ': ' + xhr.statusText); }
        });

        $('.th_table a').each(function () {
            let content = $(this).html();
            content = content + "<img class='svg_budget' src='/Logo/Sort_icon.svg'></img>";
            $(this).html(content);
        });
    }

    function myjava2() {
        $('.th_table a').each(function () {
            let content = $(this).html();
            content = content + "<img class='svg_budget' src='/Logo/Sort_icon.svg'></img>";
            $(this).html(content);
        });
    }
</script>

<script>
    function radioMe(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;
        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= chk_datee.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');
        for (i = 0; i < chks.length; i++) {
            if (chks[i] != checker) chks[i].checked = false;
        }
    }

    function radioMe2(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;
        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= chk_datee2.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');
        for (i = 0; i < chks.length; i++) {
            if (chks[i] != checker) chks[i].checked = false;
        }
    }

    function radioMe3(e) {
        if (!e) e = window.event;
        var sender = e.target || e.srcElement;
        if (sender.nodeName != 'INPUT') return;
        var checker = sender;
        var chkBox = document.getElementById('<%= chk_kfo2.ClientID %>');
        var chks = chkBox.getElementsByTagName('INPUT');
        for (i = 0; i < chks.length; i++) {
            if (chks[i] != checker) chks[i].checked = false;
        }
    }
</script>

<script>
    $(document).ready(function () {
        $("#ContentPlaceHolder1_downloadLink").click(function () {
            $("#<%=Panel1.ClientID %>").css({ "display": "flex" });
            $("#<%=downloadLink.ClientID %>").css({ "display": "none" });
        });
    });

    function sw() {
        swal({
            title: 'База обновлена',
            position: "bottom",
            allowOutsideClick: false,
            allowEscapeKey: false,
            allowEnterKey: false,
            showConfirmButton: false,
            showCancelButton: false,
            background: null,
            backdrop: null,
            timer: 2000
        });
    }
</script>

<div class="content">
    <div class="a-table">

        <div style="display: flex; justify-content: space-between; flex-direction: row; margin-top: 60px; margin-bottom: 5px;">
            <div class="head__menu" style="display: flex; align-items: center; justify-content: space-between;">
                <div style="margin-right: 10px; margin-bottom: 10px;">
                    <asp:DropDownList ID="DropDownList_year" runat="server" AutoPostBack="true" CssClass="select" OnSelectedIndexChanged="DropDownList_year_SelectedIndexChanged">
                        <asp:ListItem Text="2022" Value="2022" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:CheckBox ID="CheckBox_isp" Text="Направления" AutoPostBack="true" OnCheckedChanged="CheckBox_isp_CheckedChanged" CssClass="zone_box" runat="server" />
                <asp:CheckBox ID="CheckBox_kosgu" Text="КОСГУ" AutoPostBack="true" OnCheckedChanged="CheckBox_kosgu_CheckedChanged" CssClass="zone_box" runat="server" />
                <asp:CheckBox ID="CheckBox_smisl" Text="Смысловые группы" AutoPostBack="true" CssClass="zone_box" runat="server" OnCheckedChanged="CheckBox_smisl_CheckedChanged" />

                <div style="margin-right: 10px; margin-bottom: 10px;">
                    <div>
                        <a id="downloadLink" runat="server" href="/Excel/ШаблонФИО.csv" visible="false">
                            <svg role="img" style="width:38px; fill:#606266;" focusable="false" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 14 14">
                                <path d="m 12.7765,2.551 -4.02,0 0,0.744 1.185,0 0,1.177 -1.185,0 0,0.375 1.185,0 0,1.1785 -1.185,0 0,0.3855 1.185,0 0,1.1145 -1.185,0 0,0.4465 1.185,0 0,1.117 -1.185,0 0,0.4465 1.185,0 0,1.1235 -1.185,0 0,0.8195 4.02,0 c 0.0635,-0.019 0.1165,-0.094 0.159,-0.224 C 12.978,11.1235 13,11.017 13,10.9365 L 13,2.687 C 13,2.623 12.978,2.5845 12.9355,2.571 12.893,2.558 12.84,2.551 12.7765,2.551 Z m -0.5215,8.107 -1.9285,0 0,-1.1225 1.9285,0 0,1.1235 0,-0.001 z m 0,-1.569 -1.9285,0 0,-1.1175 1.9285,0 0,1.1175 z m 0,-1.564 -1.9285,0 0,-1.1095 1.9285,0 0,1.1105 0,-10e-4 z m 0,-1.5 -1.9285,0 0,-1.177 1.9285,0 0,1.1775 0,-5e-4 z m 0,-1.5595 -1.9285,0 0,-1.17 1.9285,0 0,1.1775 0,-0.0075 z M 1,2.3655 1,11.666 8.08,12.8905 8.08,1.1095 1,2.3695 1,2.3655 Z"/>
                            </svg>
                        </a>
                    </div>
                    <asp:Panel ID="Panel1" runat="server" style="display: none; justify-content: space-around; align-items: center;">
                        <div class="input__wrapper" style="margin-right: 10px;">
                            <input id="oFile" type="file" runat="server" NAME="oFile" class="inputt inputt__file">
                            <label for="ContentPlaceHolder1_oFile" class="inputt__file-button">
                                <label id="for1" for="ContentPlaceHolder1_oFile" class="inputt__file-button-text">Выберите файл</label>
                            </label>
                        </div>
                        <asp:Button ID="Button_csv" CssClass="csv" runat="server" Text="Загрузить csv" style="margin-right: 10px;" OnClick="Button_csv_Click" />
                    </asp:Panel>
                </div>
            </div>
            <div>
                <section id="Section_kred" runat="server" class="finder" style="display: flex; align-items: flex-end; margin-top: 10px" visible="false">
                    <h3 class="withrotate" style="display: flex; justify-content: space-between; align-items: center; padding: 0px !important;">
                        Кредиторская задолженность
                        <label class="switch">
                            <asp:CheckBox ID="CheckBox_kred" runat="server" OnCheckedChanged="CheckBox_kred_CheckedChanged" AutoPostBack="true" />
                            <span class="slider round"></span>
                        </label>
                    </h3>
                </section>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="HiddenField_kfo" runat="server" style="display: none;"></asp:Label>
                <asp:Label ID="HiddenField_datee" runat="server" style="display: none;"></asp:Label>
                <asp:Label ID="HiddenField_dir" runat="server" style="display: none;"></asp:Label>
                <asp:Label ID="HiddenField_kfo2" runat="server" style="display: none;"></asp:Label>
                <asp:Label ID="HiddenField_datee2" runat="server" style="display: none;"></asp:Label>
                <asp:Label ID="HiddenField_kosgu" runat="server" style="display: none;"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chk_kfo" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_datee" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_dir" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_kfo2" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_datee2" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_kosgu" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_kosgu_smisl2" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chk_kosgu_smisl1" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="CheckBox2" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="CheckBox3" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="CheckBox4" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="CheckBox_kred" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <%-- ФИО --%>
        <asp:Panel ID="Panel_isp" runat="server">
            <div class="budget_blockmain">
                <div class="budget_substrate">
                    <div style="display: flex">
                        <asp:Label ID="Label_budget_ispol" runat="server" style="width: 100%; text-align: left; font-family: mossport; text-transform: uppercase; font-size: 36px; color: #f8913b; margin: 0px 20px;" Text="Исполнение бюджета"></asp:Label>
                    </div>
                    <div style="display:flex; flex-direction:row;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="width: 750px;">
                                    <canvas id="myChart"></canvas>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chk_kfo" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_datee" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_dir" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="CheckBox_kred" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div style="display: flex; flex-direction: row; width: max-content;">
                            <div style="display: flex; flex-direction: column;">
                                <div class="budget_blockmain_block block_ddl_kfo">
                                    <asp:Label ID="Label5" runat="server" style="margin: 15px 0px 15px 0;" Text="КФО"></asp:Label>
                                    <asp:CheckBoxList ID="chk_kfo" CssClass="check_style" SelectionMode="Multiple" runat="server" AppendDataBoundItems="true" DataTextField="kfo" DataValueField="kfo" AutoPostBack="true" OnSelectedIndexChanged="chk_dir_SelectedIndexChanged"></asp:CheckBoxList>
                                </div>
                                <div class="budget_blockmain_block block_ddl_datee">
                                    <asp:Label ID="Label4" runat="server" style="margin: 15px 0px 15px 0;" Text="Дата"></asp:Label>
                                    <div class="chk_scroll_menu chk_scroll_kosgu">
                                        <asp:CheckBoxList ID="chk_datee" style="min-width: 95px;" CssClass="check_style" AppendDataBoundItems="true" DataTextField="datee" DataValueField="datee" AutoPostBack="true" OnSelectedIndexChanged="chk_dir_SelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="budget_blockmain_block block_ddl_dir chk_menu" style="margin: 0 0 0 15px;">
                                <asp:Label ID="Label3" runat="server" style="margin: 15px 0px 15px 0;" Text="Направление"></asp:Label>
                                <div class="chk_scroll_menu chk_scroll_budget">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="check_style check__ind" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Сбросить все" AutoPostBack="true" Checked="true" />
                                            <asp:CheckBoxList ID="chk_dir" CssClass="check_style" style="min-width: 140px; width: 200px;" AppendDataBoundItems="true" DataTextField="direction" DataValueField="direction" AutoPostBack="true" OnSelectedIndexChanged="chk_dir_SelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="budget_substrate">
                                <div style="display: flex">
                                    <asp:Label ID="Label1" runat="server" style="width: 100%; text-align: left; font-family: mossport; text-transform: uppercase; font-size: 36px; color: #f8913b; margin: 0px 20px;" Text="Таблица с данными"></asp:Label>
                                </div>
                                <asp:GridView ID="GridView_FIO" SortedAscendingHeaderStyle-ForeColor="#f8913b" SortedDescendingHeaderStyle-ForeColor="#f8913b" CssClass="Tab_obj" GridLines="None" ShowFooter="True" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_FIO_RowDataBound" AllowSorting="true" OnSorting="GridView_FIO_Sorting">
                                    <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                    <RowStyle CssClass="row" Height="45px" />
                                    <FooterStyle CssClass="bold" Height="45px" />
                                    <Columns>
                                        <asp:BoundField DataField="Направление" HeaderText="Направление" SortExpression="Направление" />
                                        <asp:BoundField DataField="ПроцентЗаключенных" HeaderText="% заключенных договоров" SortExpression="ПроцентЗаключенных" />
                                        <asp:BoundField DataField="ПроцентОплаченных" HeaderText="% оплаченных договоров" SortExpression="ПроцентОплаченных" />
                                        <asp:BoundField DataField="Лимиты" HeaderText="ЛИМИТЫ" SortExpression="Лимиты" />
                                        <asp:BoundField DataField="ЗаключеныДоговоры" HeaderText="Заключены договоры" SortExpression="ЗаключеныДоговоры" />
                                        <asp:BoundField DataField="ДоговорыВПроцессе" HeaderText="Договоры в процессе заключения" SortExpression="ДоговорыВПроцессе" />
                                        <asp:BoundField DataField="НеОсвоеныЛимиты" HeaderText="Не освоены лимиты" SortExpression="НеОсвоеныЛимиты" />
                                        <asp:BoundField DataField="ОплаченыДоговоры" HeaderText="Оплачены договоры" SortExpression="ОплаченыДоговоры" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chk_kfo" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chk_datee" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chk_dir" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox1" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox_kred" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>

        <%-- КОСГУ --%>
        <asp:Panel ID="Panel_kosgu" runat="server" Visible="false">
            <div class="budget_blockmain">
                <div class="budget_substrate">
                    <div style="display:flex;">
                        <asp:Label ID="Label_budget_kosgy" runat="server" style="width: 100%; text-align: left; font-family: mossport; text-transform: uppercase; font-size: 36px; color: #f8913b; margin: 0px 20px;" Text="КОСГУ"></asp:Label>
                    </div>
                    <div style="display:flex; flex-direction:row;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                                    <div style="width: 750px;">
                                        <canvas id="myChart2"></canvas>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chk_kfo2" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_datee2" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_kosgu" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_kosgu_smisl2" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_kosgu_smisl1" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="CheckBox2" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="CheckBox3" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="CheckBox4" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="CheckBox_kred" EventName="CheckedChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <div style="display: flex; flex-direction: row; width: max-content;">
                            <div>
                                <div class="budget_blockmain_block block_ddl_kfo2">
                                    <asp:Label ID="Label6" runat="server" style="margin: 15px 0px 15px 0;" Text="КФО"></asp:Label>
                                    <asp:CheckBoxList ID="chk_kfo2" CssClass="check_style" SelectionMode="Multiple" runat="server" AppendDataBoundItems="true" DataTextField="kfo" DataValueField="kfo" AutoPostBack="true" OnSelectedIndexChanged="chk_dir_SelectedIndexChanged"></asp:CheckBoxList>
                                </div>
                                <div class="budget_blockmain_block block_ddl_datee2">
                                    <asp:Label ID="Label7" runat="server" style="margin: 15px 0px 15px 0;" Text="Дата"></asp:Label>
                                    <div class="chk_scroll_menu chk_scroll_kosgu">
                                        <asp:CheckBoxList ID="chk_datee2" style="min-width: 95px;" CssClass="check_style" SelectionMode="Multiple" runat="server" AppendDataBoundItems="true" DataTextField="datee" DataValueField="datee" AutoPostBack="true" OnSelectedIndexChanged="chk_dir_SelectedIndexChanged"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>

                            <div class="budget_blockmain_block block_ddl_kosgu chk_menu" style="margin: 0 0 0 15px;">
                                <asp:Label ID="Label8" runat="server" style="margin: 15px 0px 15px 0;" Text="КОСГУ"></asp:Label>
                                <div class="chk_scroll_menu chk_scroll_budget">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" CssClass="check_style check__ind" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Сбросить все" AutoPostBack="true" Checked="true" />
                                            <asp:CheckBoxList ID="chk_kosgu" CssClass="check_style" style="min-width: 140px;" AppendDataBoundItems="true" DataTextField="kosgu" DataValueField="kosgu" AutoPostBack="true" OnSelectedIndexChanged="chk_dir_SelectedIndexChanged" runat="server"></asp:CheckBoxList>
                                            <asp:CheckBox ID="CheckBox3" runat="server" CssClass="check_style check__ind" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Сбросить все" AutoPostBack="true" Checked="true" Visible="false" />
                                            <asp:CheckBoxList ID="chk_kosgu_smisl1" CssClass="check_style" style="min-width: 140px;" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="chk_kosgu_smisl1_SelectedIndexChanged" runat="server" Visible="false">
                                                <asp:ListItem Value="224 Аренда" Text="Аренда" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="343 ГСМ" Text="ГСМ" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="223 Коммуналка" Text="Коммуналка" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="341 Медматериалы','344 Строительные материалы','345 Мягкий инвентарь','346 Прочие материалы" Text="Материалы и инвентарь" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="310 ОС" Text="ОС" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="228 Проектирование" Text="Проектирование" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="221 Услуги связи','226 Прочие услуги','227 Страхование','291 Пошлины" Text="Прочие" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="225 Содержание имущества" Text="Содержание имущества" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="222 Транспортные услуги" Text="Транспортные услуги" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="211 ФОТ','212 Командировки','213 Взносы','264 Пособия','266 Пособия" Text="Фот+взносы + командировки+пособия" Selected="True"></asp:ListItem>
                                            </asp:CheckBoxList>
                                            <asp:CheckBox ID="CheckBox4" runat="server" CssClass="check_style check__ind" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Сбросить все" AutoPostBack="true" Checked="true" Visible="false" />
                                            <asp:CheckBoxList ID="chk_kosgu_smisl2" CssClass="check_style" style="min-width: 140px;" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="chk_kosgu_smisl1_SelectedIndexChanged" runat="server" Visible="false">
                                                <asp:ListItem Value="224 Аренда" Text="Аренда" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="343 ГСМ" Text="ГСМ" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="223 Коммуналка" Text="Коммуналка" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="341 Медматериалы','344 Строительные материалы','345 Мягкий инвентарь','346 Прочие материалы" Text="Материалы и инвентарь" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="310 ОС" Text="ОС" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="221 Услуги связи','226 Прочие услуги','227 Страхование','229 Аренда за пользование ЗУ','320 Права на интелектуальную собственность','349 Сувенирная продукция','" Text="Прочие" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="225 Содержание имущества" Text="Содержание имущества" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="222 Транспортные услуги" Text="Транспортные услуги" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="211 ФОТ','213 Взносы','264 Материальная помощь','265 Пособие на погребение','266 Пособия" Text="Фот+взносы + командировки+пособия" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="291 Налог на имущесто','292 Штрафы, пени (налоги)','293 Штрафы, пени (контракты)','295 Административные штрафы','296 Возмещение ущерба','297 Возмещение юрлицам" Text="Штрафы, возмещение ущерба, налоги" Selected="True"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="CheckBox2" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="CheckBox3" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="CheckBox4" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chk_kfo2" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="budget_substrate">
                                <div style="display: flex">
                                    <asp:Label ID="Label2" runat="server" style="width: 100%; text-align: left; font-family: mossport; text-transform: uppercase; font-size: 36px; color: #f8913b; margin: 0px 20px;" Text="Таблица с данными"></asp:Label>
                                </div>
                                <asp:GridView ID="GridView_KOSGU" CssClass="Tab_obj" SortedAscendingHeaderStyle-ForeColor="#f8913b" SortedDescendingHeaderStyle-ForeColor="#f8913b" GridLines="None" ShowFooter="True" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_FIO_RowDataBound" AllowSorting="True" OnSorting="GridView_FIO_Sorting">
                                    <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                                    <RowStyle CssClass="row" Height="45px" />
                                    <FooterStyle CssClass="bold" Height="45px" />
                                    <Columns>
                                        <asp:BoundField DataField="КОСГУ" HeaderText="КОСГУ" SortExpression="КОСГУ" />
                                        <asp:BoundField DataField="ПроцентЗаключенных" HeaderText="% заключенных договоров" SortExpression="ПроцентЗаключенных" />
                                        <asp:BoundField DataField="ПроцентОплаченных" HeaderText="% оплаченных договоров" SortExpression="ПроцентОплаченных" />
                                        <asp:BoundField DataField="Лимиты" HeaderText="ЛИМИТЫ" SortExpression="Лимиты" />
                                        <asp:BoundField DataField="ЗаключеныДоговоры" HeaderText="Заключены договоры" SortExpression="ЗаключеныДоговоры" />
                                        <asp:BoundField DataField="ДоговорыВПроцессе" HeaderText="Договоры в процессе заключения" SortExpression="ДоговорыВПроцессе" />
                                        <asp:BoundField DataField="НеОсвоеныЛимиты" HeaderText="Не освоены лимиты" SortExpression="НеОсвоеныЛимиты" />
                                        <asp:BoundField DataField="ОплаченыДоговоры" HeaderText="Оплачены договоры" SortExpression="ОплаченыДоговоры" />
                                        <asp:BoundField DataField="КредиторскаяЗадолженность" Visible="false" HeaderText="Кредиторская задолженность" SortExpression="КредиторскаяЗадолженность" />
                                        <asp:BoundField DataField="ОплаченнаяКредиторская" Visible="false" HeaderText="Оплаченная кредиторская задолженность" SortExpression="ОплаченнаяКредиторская" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chk_kfo2" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chk_datee2" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chk_kosgu" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chk_kosgu_smisl2" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="chk_kosgu_smisl1" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox2" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox3" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox4" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="CheckBox_kred" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>

        <script>
            $(document).ready(function () {
                $('#ContentPlaceHolder1_oFile').change(function () {
                    if (this.files[0])
                        $('#for1').text(this.files[0].name);
                });
            });
        </script>

    </div>
</div>

</asp:Content>