<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Site.Contact" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

        function myFunction(parameter) {
            __doPostBack('<%= TextSearch.ClientID %>', '');
        }
    </script>

    <style>
        @media (max-width: 1040px) {
            .selectric-wrapper { width: 100%; }
        }
    </style>

    <div class="a-table">
        <section class="finder">
            <h3>СПИСОК</h3>
        </section>

        <div class="find" style="padding: unset;">
            <div class="selectsandim">
                <asp:DropDownList CssClass="select" ID="DropDownList1" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="District" DataValueField="District" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" class="default">
                    <asp:ListItem Value="%" Text="Все округа"></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList CssClass="select" ID="DropDownList2" AppendDataBoundItems="true" runat="server" AutoPostBack="True" DataTextField="Raion" DataValueField="Raion" BackColor="White" ForeColor="Black" class="default" style="cursor: pointer; box-shadow: 0 0 5px red" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem Value="%" Text="Все районы"></asp:ListItem>
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

                <asp:Button ID="Vse_zone" runat="server" CssClass="tablebtn3" Text="Вcе спортивные зоны МСО" visible="false" PostBackUrl="~/Vse_spots_zone.aspx" EnableTheming="True" UseSubmitBehavior="False" />

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button1" runat="server" Text="Скачать" CssClass="tablebtn3" OnClick="Button1_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TextSearch" />
                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="Button1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="rightfind">
                <asp:TextBox ID="TextSearch" runat="server" style="max-width: 200px;" placeholder="Поиск" CssClass="input2" autocomplete="off" MaxLength="250" AutoPostBack="true" OnTextChanged="TextSearch_TextChanged"></asp:TextBox>

                <button id="Btn_poisk" runat="server" visible="false" style="border: none; background: none; cursor: pointer;">
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

        <div class="test">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" GridLines="None" DataKeyNames="NameObject" table-layout="fixed" width="100%" CssClass="Tab_obj" OnRowCreated="GridView1_RowCreated" style="margin-top:20px" OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                        <RowStyle CssClass="row" Height="45px" />
                        <emptydatatemplate>
                            <asp:Label ID="Label1" runat="server" Text="По вашему запросу ничего не найдено." Font-Bold="True"></asp:Label>
                        </emptydatatemplate>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="DropDownList2" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="TextSearch" />
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="progress">
                        <asp:Image ID="Image1" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

        <asp:GridView ID="GridView2" runat="server" DataKeyNames="NameObject" table-layout="fixed" width="100%" OnRowCreated="GridView1_RowCreated" Visible="False">
            <HeaderStyle BorderStyle="None" CssClass="zagolovok" Width="100px" />
        </asp:GridView>
    </div>
</div>

</asp:Content>