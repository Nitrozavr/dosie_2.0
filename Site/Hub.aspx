<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hub.aspx.cs" Inherits="Site.Hub" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/Main.css" rel="stylesheet" />
        <title>
        ВАША КОМПАНИЯ
         </title>
    <link href="Logo/favicon-test.ico" rel="shortcut icon" type="image/x-icon" />
    <style>
        * {
                 margin: 0;
                padding: 0;
                font-family:montserrat;
        }
    
        .button_vis {
            padding: 12px 20px;
            border:1px solid transparent;
            border-radius: 5px;
            font-family: GOTHAPROBOL;
            color: #fff;
            line-height: 1.2;
            background: #F8913B;
            font-size: 20px;
            min-width: 79px;
            min-height: 30px;
            text-align: center;
        }

        .button_vis:hover {
            background: #1A364B;
            color: #ffffff;
            border: 1px solid #1A364B;
            cursor: pointer;
            border-radius: 5px;
            padding: 12px 20px;
        }

        .checkbox_2 input[type="checkbox"] + label {
            margin-right: 10px;
            margin-bottom: 10px;
            padding: 10px;
            border: 1px solid #dcdfe6;
            border-radius: 5px;
            color: #606266;
            background: white;
            font-size: 11px;
            -webkit-box-shadow: 0px 2px 6px rgb(0 0 0 / 9%);
            box-shadow: 0px 2px 6px rgb(0 0 0 / 9%);
            border-radius: 5px;
            min-width: 79px;
            min-height: 30px;
            text-align: center;
        }

        .checkbox_2 input[type="checkbox"]:checked + label {
            background: #E22B36;
            color: #ffffff;
            border: 1px solid #E22B36;
            cursor: pointer;
            border-radius: 5px;
            padding: 10px;
        }

        .checkbox_2 input[type="checkbox"]:hover + label {
            background: #E22B36;
            color: #ffffff;
            border: 1px solid #E22B36;
            cursor: pointer;
            border-radius: 5px;
            padding: 10px;
        }

        #hub_1 {
            width:400px;
            padding: 20px;
        }

        #hub_2 {
            width:400px;
            padding: 20px;
        }

        #hub_3 {
            width:400px;
            padding: 20px;
        }

        .Label_main {
            font-size:44px;
        }

        .Labelblock {
            font-size:20px;
        }

        .hub {
            width: 100%;
            height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .hub__main {
                display: flex;
                justify-content: center;
                flex-direction: column;
                margin:auto;
        }
        .hub__main__row {
                display: flex;
                justify-content: center;
                flex-direction: row;
                margin:auto;
        }

        .hub__main_blocks {
                display: flex;
                align-items: center;
                flex-direction: column;
                padding:20px;
        }


        /*_____*/

        .test {
              
        }

        .test__main {
              display: flex;
              flex-direction: row;
              width: 100vw;
              height: 100vh;
              overflow: hidden;
              
        }

        .section__main {
                 overflow:hidden;
                 width: 100vw;
                height: 100vh;
                display: flex;
                justify-content: flex-start;
                align-items: center;
                font-size: 2em;
                color: white;
                transition: transform 0.5s ease;
                position: relative;
                flex-direction: row;
                 transition: transform 0.2s ease-in-out;
        }

        .hubmain_blocks {
            margin:auto;
        }

        section {
            min-height: 100vh;
            width:-webkit-fill-available;
        }

        section:nth-child(1) {
            background: url("Photo_hub/hub_1.png");
            color: white;
            background-size:cover;
        }

        section:nth-child(2) {
        background: url("Photo_hub/hub_2.png");
          color: white;
          background-size:cover;
        }
        section:nth-child(3) {
          background: url("Photo_hub/hub_3.png");
          color: white;
          background-size:cover;
        }

        .description-block {
          position: fixed;
          bottom: 0;
          background-color: black;
          width:fit-content;
          color:white;
          opacity:0.7;
          padding: 20px 40px 70px 40px;
          box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
          z-index: 1;
          transform: translateY(100%);
        }

         .section__main:hover {
           transform: scale(1.1); 
         }

         .section__main {
           transform: scale(1); 
         }

        .section__main:hover .description-block {
          display: block;
          animation: slide-up 0.3s ease-in-out forwards;
        }

        .section__main:not(:hover) .description-block {
            transition:300ms;
            animation: slide-down 0.3s ease-in-out forwards;
        }

        @keyframes slide-up {
          from {
            transform: translateY(100%);
          }
          to {
            transform: translateY(0%);
          }
        }
        @keyframes slide-down {
          from {
            transform: translateY(0%);
          }
          to {
            transform: translateY(100%);
          }
        }

        @media (min-width: 1100px) and (max-width: 1600px) {  
            .Labelblock {
                font-size: 12px;
                line-height: 2;
                display: block;
            }
            .description-block {
                padding: 10px 30px 45px 30px;
            }
            section:nth-child(1){
                background-position: top center;
            }
            section:nth-child(2){
                background-position: top center;
            }
            section:nth-child(3){
                background-position: top center;
            }
            
        }
        @media (min-width: 500px) and (max-width: 1100px) {
            .Labelblock {
                font-size: 8px;
                line-height: 1.2;
                display: block;
            }
            .description-block {
                padding: 15px 10px 45px 20px;
            }
        }



    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hub">
             
                  <div class="test">
                        <div class="test__main">
                            
                            <section class="section__main">
                                <asp:Panel ID="Panel_Dosie" runat="server" CssClass="hubmain_blocks">
                                    <asp:Button ID="Button_Dosie" runat="server" CssClass="button_vis but1" Style="margin-top: 100px;" Text="Система Досье" OnClick="Button_Dosie_Click"/>
                                </asp:Panel>
                                    <div class="description-block">
                                        <asp:Label ID="Label1" runat="server" CssClass="Labelblock" Text="Простая в использовании автоматизированная информационная система для сбора и работы с широким спектром данных об объектах (движимое и недвижимое имущество)
"></asp:Label>
                                    </div>
                                
                            </section>
                            <section class="section__main" style="border-right:1px solid #121212; border-left:1px solid #121212; z-index:99">
                                <asp:Panel ID="Panel_Doc" runat="server" CssClass="hubmain_blocks">
                                    <asp:Button ID="Button_Doc" runat="server" CssClass="button_vis but2" Style="margin-top: 100px;" Text="Система Документы" OnClick="Button_Doc_Click"/>
                                </asp:Panel>
                                    <div class="description-block">
                                        <asp:Label ID="Label2" runat="server" CssClass="Labelblock" Text="Автоматизированная, интуитивно понятная система хранения и работы с документами организации (электронные образы документов в разных форматах, адаптированная к структуре и должностям навигация, простое обновление и архивирование)"></asp:Label>
                                    </div>
                                
                            </section>
                            <section class="section__main">
                                <asp:Panel ID="Panel_Stat" runat="server" CssClass="hubmain_blocks">
                                    <asp:Button ID="Button_Stat" runat="server" CssClass="button_vis but3" Style="margin-top: 100px;" Text="Статистика" OnClick="Button_Stat_Click"/>
                                </asp:Panel>
                                    <div class="description-block">
                                        <asp:Label ID="Label3" runat="server" CssClass="Labelblock" Text="Все самые нужные и актуальные данные в любой момент времени структурировано на одном экране онлайн (свод информации из различных источников и систем)
"></asp:Label>
                                    </div>
                            </section>
                        </div>
                </div>                
         </div>
       
    </form>
</body>
</html>
