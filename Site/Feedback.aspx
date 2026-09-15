<%@ Page Title="Обратная связь" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="Site.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" src="CSS/selectric2.css" />

    <style>
        /* ============================================================
           Обратная связь — выравнивание иллюстрации и формы
           ============================================================ */
        .feedback {
            display: flex;
            flex-direction: row;
            align-items: flex-start;
            justify-content: space-around;
            gap: 40px;
            padding: 30px 0;
        }

        .feedback .frame {
            max-width: 500px;
            width: 100%;
            height: auto;
        }

        .feedback .info2 {
            max-width: 500px;
            width: 100%;
            background: white;
            border-radius: 20px;
            padding: 30px;
            box-shadow: 0px 0px 20px 0px rgb(35 30 60 / 20%);
        }

        @media (max-width: 1040px) {
            .feedback {
                flex-direction: column;
                align-items: center;
            }
        }

        .selectric { width: 100%; margin: 10px 0px !important; }
        .selectric .label, .selectric-items li { font-family: GOTHAPROREG; font-size: 12px; }
        .selectric-wrapper { position: relative; cursor: pointer; padding: 0; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="a-table">

            <section class="finder">
                <h3 class="feed">ОБРАТНАЯ СВЯЗЬ</h3>
            </section>

            <div class="feedback">

                <%-- Иллюстрация --%>
                <img class="frame" src="Logo/feedback-illustration.svg" alt="" />

                <%-- Форма создания заявки --%>
                <asp:Panel ID="Panel1" runat="server" CssClass="info2">

                    <p style="font-family: Mossport; font-size: 36px; color: #1a364b; margin-bottom: 30px; text-transform: uppercase;">
                        Создать заявку
                    </p>

                    <asp:TextBox ID="FIOText" placeholder="ФИО сотрудника" CssClass="input_feedback" runat="server"></asp:TextBox>
                    <asp:TextBox ID="NumberText" placeholder="Телефон для связи" CssClass="input_feedback" runat="server"></asp:TextBox>

                    <asp:DropDownList CssClass="select" ID="OtdelList" runat="server">
                        <asp:ListItem Value="" Text="Отдел"></asp:ListItem>
                        <asp:ListItem>Юридический отдел</asp:ListItem>
                        <asp:ListItem>Просто отдел</asp:ListItem>
                        <asp:ListItem>Руководство</asp:ListItem>
                    </asp:DropDownList>

                    <asp:TextBox ID="DoljnostText" placeholder="Должность" CssClass="input_feedback" runat="server"></asp:TextBox>

                    <asp:DropDownList CssClass="select" ID="TemaList" runat="server">
                        <asp:ListItem Value="" Text="Тема обращения"></asp:ListItem>
                        <asp:ListItem>Карта</asp:ListItem>
                        <asp:ListItem>Список</asp:ListItem>
                        <asp:ListItem>Отчёты</asp:ListItem>
                        <asp:ListItem>Обратная связь</asp:ListItem>
                        <asp:ListItem>Документы</asp:ListItem>
                        <asp:ListItem>Статистика</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList CssClass="select" ID="Deistvie_list" runat="server">
                        <asp:ListItem Value="" Text="Действие"></asp:ListItem>
                        <asp:ListItem>Обновление данных</asp:ListItem>
                        <asp:ListItem>Внесение данных</asp:ListItem>
                        <asp:ListItem>Удаление данных</asp:ListItem>
                        <asp:ListItem>Комплексные вопросы</asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList CssClass="select" ID="SpochnosList" runat="server">
                        <asp:ListItem Value="" Text="Срочность рассмотрения"></asp:ListItem>
                        <asp:ListItem>Незамедлительно</asp:ListItem>
                        <asp:ListItem>Срочно</asp:ListItem>
                        <asp:ListItem>Обычная</asp:ListItem>
                    </asp:DropDownList>

                    <asp:TextBox ID="TextMSG" TextMode="MultiLine" placeholder="Текст обращения"
                        Rows="10" MaxLength="2000" CssClass="input_message" runat="server"></asp:TextBox>

                    <%-- Загрузка файла --%>
                    <div class="input__wrapper">
                        <input id="oFile" type="file" runat="server" NAME="oFile" class="inputt inputt__file">
                        <label for="ContentPlaceHolder1_oFile" class="inputt__file-button">
                            <span class="inputt__file-icon-wrapper">
                                <svg display="none">
                                    <symbol id="download" viewBox="0 0 24 24">
                                        <g>
                                            <path d="M14.7928932,11.5 L11.6464466,8.35355339 C11.4511845,8.15829124 11.4511845,7.84170876 11.6464466,7.64644661 C11.8417088,7.45118446 12.1582912,7.45118446 12.3535534,7.64644661 L16.3535534,11.6464466 C16.5488155,11.8417088 16.5488155,12.1582912 16.3535534,12.3535534 L12.3535534,16.3535534 C12.1582912,16.5488155 11.8417088,16.5488155 11.6464466,16.3535534 C11.4511845,16.1582912 11.4511845,15.8417088 11.6464466,15.6464466 L14.7928932,12.5 L4,12.5 C3.72385763,12.5 3.5,12.2761424 3.5,12 C3.5,11.7238576 3.72385763,11.5 4,11.5 L14.7928932,11.5 Z M16,4.5 C15.7238576,4.5 15.5,4.27614237 15.5,4 C15.5,3.72385763 15.7238576,3.5 16,3.5 L19,3.5 C20.3807119,3.5 21.5,4.61928813 21.5,6 L21.5,18 C21.5,19.3807119 20.3807119,20.5 19,20.5 L16,20.5 C15.7238576,20.5 15.5,20.2761424 15.5,20 C15.5,19.7238576 15.7238576,19.5 16,19.5 L19,19.5 C19.8284271,19.5 20.5,18.8284271 20.5,18 L20.5,6 C20.5,5.17157288 19.8284271,4.5 19,4.5 L16,4.5 Z" transform="rotate(90 12.5 12)"/>
                                        </g>
                                    </symbol>
                                </svg>
                                <svg class="topbtn2">
                                    <use href="#download"></use>
                                </svg>
                            </span>
                            <label id="for1" for="ContentPlaceHolder1_oFile" class="inputt__file-button-text">Выберите файл</label>
                        </label>
                    </div>
                    <br />

                    <asp:Button ID="Otpravit" CssClass="submitbtn" runat="server"
                        style="width: 200px; margin: 0" Text="Отправить" OnClick="Otpravit_Click" />
                </asp:Panel>

            </div>
        </div>
    </div>

    <%-- Маска для телефона --%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.10/jquery.mask.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= NumberText.ClientID %>").mask("+7(999)999-99-99");

            // Отображение имени выбранного файла
            $('#ContentPlaceHolder1_oFile').change(function () {
                if (this.files[0])
                    $('#for1').text(this.files[0].name);
            });
        });
    </script>

</asp:Content>