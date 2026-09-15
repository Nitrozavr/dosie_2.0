<%@ Page Title="" Language="C#" MasterPageFile="~/Mosedo.Master" AutoEventWireup="true" CodeBehind="FlaseError.aspx.cs" Inherits="Site.FlaseError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Main.css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="imagetoolbar" content="no" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" /> <!--Строчка, которая делает магию-->
    <title>Ошибка</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .locked {
             -webkit-user-select: none;
             -moz-user-select: none;
             user-select: none;
             pointer-events: none;
        }

        .submitbtn {
        margin: 10px 0;
        padding: 10px;
        font-family: GOTHAPROBOL;
        font-size: 13px;
        display: block;
        float: left;
        background: white;
        color: #f8913b;
        border: 3px solid #f8913b;
        border-radius:5px;
        height: 50px;
        cursor: pointer;

        }
        .submitbtn:hover {
        background: #f8913b;
        color:#fff;
}
        .cont{
            display: flex;
            flex-direction: column;
            max-width: 1470px;
            margin: auto;
        }
        .fon {
            padding-top: 80px;
            display: flex; 
            flex-direction: row; 
            align-items: center; 
            text-align: center;
        }
        .zag1 {
            font-family: GOTHAPROREG;
            font-size: 72px;
            color: #f8913b;
            font-weight: 900;
            width: 100%;
            padding:10px
        }
        .zag2 {
            font-family: GOTHAPROREG;
            font-size: 40px;
            min-width: 400px;
            width: 100%;
            padding:10px;
            padding-bottom:40px;
        }
        @media (max-width: 1000px) {
            .fon {
                flex-direction:column !important;
            }
            .img1 {
                max-width:75% !important;
            }
            .zag1 {
            font-size: 60px;
        }
        .zag2 {
            font-size: 32px;
        }
        }
    </style>
        <script>
            (function () {
                "use strict";

                var el = document.createElement('div');
                el.style.cssText = 'pointer-events:auto';

                if (el.style.pointerEvents !== 'auto') {
                    el = null;

                    var _lock = function (evt) {
                        evt = evt || window.event;
                        var el = evt.target || evt.srcElement;
                        if (el && /\slocked\s/.test(' ' + el.className + ' ')) {
                            if (evt.stopPropagation) {
                                evt.preventDefault();
                                evt.stopPropagation();
                            } else {
                                evt.returnValue = true;
                                evt.cancelBubble = true;
                            }
                        }
                    };

                    if (document.addEventListener) {
                        document.addEventListener('mousedown', _lock, false);
                        document.addEventListener('contextmenu', _lock, false);
                    } else {
                        document.attachEvent('onmousedown', _lock);
                        document.attachEvent('oncontextmenu', _lock);
                    }
                }
            })();
        </script>
            <div class="cont">
            <div class="fon">
                <div style=" display: flex;flex-direction: column; margin-bottom: 40px;" >
                    <p class="zag1">Упс..</p>
                    <p class="zag2">Ваш бизнес аналитик еще не успел заполнить подробную информацию об это документе</p>
                    <div style="margin: auto; padding-top: 40px;">
                            <asp:Button CssClass="submitbtn"  ID="Button_homes" runat="server"  Width="350" OnClick="Button_homes_Click"  Text="Вернуться назад" />
                    </div>
                </div>
                <img class="img1 locked" style="max-width: 55%;" src="Logo/Error_orange.svg" />
            </div>
            
        </div>
</asp:Content>
