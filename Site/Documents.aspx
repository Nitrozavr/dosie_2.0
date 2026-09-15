<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="Site.Documents" enableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    .selectric-wrapper { width: 100%; }
    .selectric-select_2 .selectric { width: unset; }
    .selectric { border-radius: 10px; width: 380px; }
    .selectric .label { font-size: 14px; line-height: 40px; height: 40px; }
    .selectric .button { width: 40px; height: 40px; }

    .chchbx2 { padding: 3px 0px 0px 0px; }

    .Docum_row td:nth-child(1) { font-weight: 600; font-size: 14px; text-align: left; }
    .Docum_row td:nth-child(2) { font-weight: 300; font-size: 12px; text-align: left; }
    .Docum_row td:nth-child(3) { font-weight: 300; font-size: 12px; text-align: left; }
    .Docum_row td:nth-child(4) { font-weight: 300; font-size: 12px; text-align: left; }

    .Docum_inner td:nth-child(n+1):nth-child(-n+5) {
        font-weight: 200;
        font-size: 12px;
        text-align: left;
        width: fit-content;
        margin: 3px;
        padding: 7px;
        border-radius: 0.55556rem;
        background: #efefef;
        color: #9f9797;
    }

    .Docum_tab tbody { display: grid; grid-template-columns: 2fr 2fr 1fr; }
    .Docum_inner tbody { display: unset; }

    .empty__row { display: none; }

    .pagination {
        grid-column-start: 1;
        grid-column-end: 4;
        grid-row-start: 6;
        grid-row-end: 6;
    }

    .documents_textbox:focus {
        outline: 1px solid #f8913b;
        border: 1px solid transparent;
    }
</style>

<script>
    function docum_grid() {
        $('.select').selectric({ nativeOnMobile: true });
        $(".selectric-input").attr('readonly', 'readonly');
    }

    function myFunctionSMain(parameter) {
        __doPostBack('<%= Documents_search.ClientID %>', '');
    }

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

    <div class="a-table">
        <div class="find">
            <div class="selectsandim">
                <h3>ДОКУМЕНТЫ НА ОБЪЕКТАХ</h3>
            </div>
        </div>

        <div>
            <div class="documents_text">
                <asp:Label ID="Label5" runat="server" style="margin-bottom: 15px;" Visible="true" Text="В соответсвии с приказом 1979 от 27.12.2022"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="Об утверждении Сводного перечня документов и мероприятий, обязательных к наличию и ведению на объектах" Visible="true"></asp:Label>
                <div style="margin-top: 15px;">
                    <asp:Label ID="Label7" runat="server" Text="Данный перечень доступен для скачивания ">
                        <asp:HyperLink ID="HyperLink1" Text="(нажмите чтобы скачать)" Target="_blank" NavigateUrl="/DocumentsObjects/pdf№1.pdf" runat="server"></asp:HyperLink>
                    </asp:Label>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="Documents_maincontent_UpdatePanel_results" runat="server">
            <ContentTemplate>
                <div class="documents_maincontent_block">
                    <div class="documents_maincontent_block_chk">
                        <asp:CheckBox ID="Documents_maincontent_block_chk_pers" runat="server" Text="Вы сотрудник объекта" Width="100%" Height="50px" CssClass="documents_check_style" Checked="false" AutoPostBack="true" OnCheckedChanged="Documents_maincontent_block_chk_pers_CheckedChanged" />
                    </div>
                    <div class="documents_maincontent_block_chk">
                        <asp:CheckBox ID="Documents_maincontent_block_chk_rucv" runat="server" Text="Вы сотрудник головного офиса" Width="100%" Height="50px" Style="margin: 0 10px;" CssClass="documents_check_style" Checked="false" AutoPostBack="true" OnCheckedChanged="Documents_maincontent_block_chk_rucv_CheckedChanged" />
                    </div>
                    <div class="documents_maincontent_block_chk">
                        <asp:CheckBox ID="Documents_all_elements" runat="server" Text="Показать все" Width="100%" Height="50px" CssClass="documents_check_style" Checked="true" AutoPostBack="true" OnCheckedChanged="Documents_all_elements_CheckedChanged" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Documents_maincontent_block_chk_pers" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Documents_maincontent_block_chk_rucv" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Documents_all_elements" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <div id="Div1" runat="server" style="display: flex; font-size:20px; flex-direction: column; justify-content: space-between; box-shadow: 0px 0px 10px 0px rgb(35 30 60 / 20%); border-radius: 20px; padding: 10px; margin-bottom:15px;">
            <div class="documents_maincontent_block_chk">
                <asp:TextBox ID="Documents_search" runat="server" OnTextChanged="Documents_search_TextChanged" onkeyup="myFunctionSMain()" placeholder="Поиск..." CssClass="documents_textbox"></asp:TextBox>
            </div>
        </div>

        <asp:UpdatePanel ID="Documents_maincontent_gridview" runat="server">
            <ContentTemplate>
                <div id="Documents_maincontent_block_filters" runat="server" style="display: flex; font-size:20px; flex-direction: column; justify-content: space-between; box-shadow: 0px 0px 10px 0px rgb(35 30 60 / 20%); border-radius: 20px; padding: 10px;">
                    <div style="display:flex">
                        <asp:DropDownList ID="DropDownList_doctype" CssClass="select" AppendDataBoundItems="true" AutoPostBack="True" DataTextField="DocType" DataValueField="DocType" runat="server" OnSelectedIndexChanged="DropDownList_doctype_SelectedIndexChanged">
                            <asp:ListItem value="%" selected="True">Тип документа</asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList ID="DropDownList_storage_location" CssClass="select" AppendDataBoundItems="true" AutoPostBack="True" DataTextField="StorageLocation" DataValueField="StorageLocation" runat="server" OnSelectedIndexChanged="DropDownList_doctype_SelectedIndexChanged">
                            <asp:ListItem value="%" selected="True">Место хранения/размещения</asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList ID="DropDownList_department" CssClass="select" AppendDataBoundItems="true" AutoPostBack="True" DataTextField="Department" DataValueField="Department" runat="server" OnSelectedIndexChanged="DropDownList_doctype_SelectedIndexChanged">
                            <asp:ListItem value="%" selected="True">Раздел</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div style="display:flex; flex-direction: row; align-items: center;">
                        <div class="documents_maincontent_block_chk" style="align-items: center;">
                            <asp:DropDownList ID="DropDownList_responsible_person" CssClass="select" AppendDataBoundItems="true" AutoPostBack="True" DataTextField="ResponsiblePerson" DataValueField="ResponsiblePerson" runat="server" OnSelectedIndexChanged="DropDownList_doctype_SelectedIndexChanged">
                                <asp:ListItem value="%" selected="True">Отв. лицо на объекте спорта</asp:ListItem>
                            </asp:DropDownList>

                            <asp:DropDownList ID="DropDownList_responsible_department" CssClass="select" AppendDataBoundItems="true" AutoPostBack="True" DataTextField="ResponsibleDepartment" DataValueField="ResponsibleDepartment" runat="server" OnSelectedIndexChanged="DropDownList_doctype_SelectedIndexChanged">
                                <asp:ListItem value="%" selected="True">Отв. подр. МСО</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="documents_maincontent_block_chk" style="align-items: center; width: 566px;">
                            <asp:Button ID="Documents_Button_reset" runat="server" CssClass="documents_button" OnClick="Documents_Button_reset_Click" Text="Сбросить фильтры" />
                        </div>
                    </div>
                </div>

                <div>
                    <asp:GridView ID="GridView_documents" runat="server" ShowHeader="false" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="Documents_empty" EmptyDataText="По вашему запросу ничего не найдено" GridLines="None" CssClass="Docum_tab" AllowPaging="true" PageSize="15" OnRowDataBound="GridView_documents_RowDataBound">
                        <HeaderStyle CssClass="th_table" Width="100px" Height="40px" />
                        <RowStyle CssClass="Docum_row" />
                        <PagerStyle CssClass="pagination" />
                        <Columns>
                            <asp:BoundField DataField="Doc" HeaderText="Документ" />
                            <asp:BoundField DataField="Department" HeaderText="Раздел" />
                            <asp:BoundField DataField="RegulatoryDocumentIn" HeaderText="Внешний нормативный документ" />
                            <asp:BoundField DataField="RegulatoryDocumentEx" HeaderText="Внутренний нормативный документ" />

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:GridView ID="GridView_inner_documents" runat="server" ShowHeader="false" AutoGenerateColumns="false" GridLines="None" CssClass="Docum_inner" OnRowDataBound="GridView_inner_documents_RowDataBound">
                                        <RowStyle CssClass="" />
                                        <Columns>
                                            <asp:BoundField DataField="DocType" HeaderText="Тип документа" />
                                            <asp:BoundField DataField="StorageLocation" HeaderText="Место хранения/размещения" />
                                            <asp:BoundField DataField="ResponsiblePerson" HeaderText="Отв. лицо на объекте спорта" />
                                            <asp:BoundField DataField="ResponsibleDepartment" HeaderText="Отв. подр. МСО" />
                                            <asp:BoundField DataField="Frequency" HeaderText="Периодичность актуализации/комментарии" />
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList_doctype" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DropDownList_storage_location" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DropDownList_responsible_person" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DropDownList_responsible_department" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DropDownList_department" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Documents_maincontent_block_chk_pers" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Documents_maincontent_block_chk_rucv" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Documents_all_elements" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Documents_search" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProgress4" runat="server">
            <ProgressTemplate>
                <div class="progress">
                    <asp:Image ID="Image1" src="Logo/785.gif" Width="50px" Height="50px" runat="server" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</div>

</asp:Content>