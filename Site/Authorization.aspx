<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Authorization.aspx.cs" Inherits="Site.Authorization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" href="CSS/Authorization.css" />
    <link href="Logo/favicon-test.ico" rel="shortcut icon" type="image/x-icon" />
    <script src="JS/jquery-3.5.1.min.js"></script>
    <script src="JS/sweetalert2.js"></script>
    <link href="JS/sweetalert2.css" rel="stylesheet" />

    <script>
        $(document).ready(function () {
            // Показать пароль
            $('.password-control').click(function () {
                $("#<%=Password.ClientID %>").attr('type', 'text');
                $('.password-control').css('display', 'none');
                $('.view').css('display', 'unset');
            });
            // Скрыть пароль
            $('.view').click(function () {
                $("#<%=Password.ClientID %>").attr('type', 'password');
                $('.password-control').css('display', 'unset');
                $('.view').css('display', 'none');
            });
        });

        // Неверный логин/пароль
        function sw() {
            swal({
                title: 'Проверьте правильность логина и пароля!',
                type: 'warning',
                confirmButtonText: 'Ок',
            }).then(function (isConfirm) {
                if (isConfirm.value) {
                    window.location.href = "Authorization.aspx";
                }
            });
        }

        // Пустые поля
        function sw2() {
            swal({
                title: 'Все поля обязательны к заполнению.',
                position: "bottom",
                allowOutsideClick: false,
                allowEscapeKey: false,
                allowEnterKey: false,
                showConfirmButton: false,
                showCancelButton: false,
                background: null,
                backdrop: null,
                timer: 1500
            });
        }
    </script>

    <title>Авторизация</title>
</head>
<body>
    <div class="page">
        <div class="content">
            <div class="akvatoria">
                <div class="info">
                    <div class="limiter">
                        <div class="container-login100">
                            <div class="wrap-login100 p-t-50 p-b-90">
                                <form class="login100-form validate-form flex-sb flex-w" runat="server">
                                    <asp:ScriptManager ID="ScriptManager2" runat="server" />

                                    <span class="login100-form-title p-b-51">Вход</span>

                                    <div style="display: flex; justify-content: center; flex-direction: row">
                                        <span style="opacity: 0.7; text-align: center">
                                            Вам предоставлен тестовый
                                            <br />
                                            доступ к 3 системам
                                        </span>
                                    </div>

                                    <div class="wrap-input100 validate-input m-b-16" data-validate="Username is required">
                                        <asp:TextBox runat="server" CssClass="input100" ID="UserName" TextMode="SingleLine"
                                            placeholder="Имя пользователя" MaxLength="45" autocomplete="off" />
                                        <span class="focus-input100"></span>
                                    </div>

                                    <div class="wrap-input100 validate-input m-b-16" data-validate="Password is required">
                                        <asp:TextBox runat="server" CssClass="input100" ID="Password" TextMode="Password"
                                            placeholder="Пароль" MaxLength="45" autocomplete="off" />
                                        <span class="focus-input100"></span>

                                        <svg version="1.1" class="password-control" xmlns="http://www.w3.org/2000/svg" width="20px" viewBox="0 0 533.333 533.334">
                                            <path d="M437.147,171.05c40.439,28.662,73.63,67.235,96.187,112.283C483.791,382.27,382.976,450,266.667,450 c-32.587,0-63.954-5.319-93.322-15.148l40.602-40.602c17.218,3.802,34.881,5.75,52.72,5.75c46.646,0,92.111-13.274,131.482-38.387 c31.334-19.988,57.888-46.761,77.832-78.281c-19.298-30.503-44.801-56.536-74.817-76.299L437.147,171.05z M266.667,380.208 c-11.835,0-23.308-1.55-34.233-4.445l163.116-163.116c2.898,10.923,4.45,22.393,4.45,34.228 C400,320.512,340.304,380.208,266.667,380.208z M500,16.667h-27.988L357.63,131.048c-28.686-9.335-59.247-14.381-90.964-14.381 c-116.312,0-217.126,67.73-266.667,166.667c22.218,44.371,54.754,82.453,94.372,110.974L0,488.678v27.989h27.989L500,44.655V16.667z M216.667,180.208c25.023,0,45.753,18.382,49.423,42.38l-57.043,57.044c-23.997-3.672-42.379-24.401-42.379-49.424 C166.667,202.594,189.052,180.208,216.667,180.208z M57.352,283.333c19.944-31.522,46.497-58.293,77.83-78.279 c2.041-1.302,4.102-2.563,6.176-3.802c-5.187,14.233-8.025,29.595-8.025,45.623c0,30.48,10.235,58.567,27.447,81.022 l-30.495,30.495C101.081,338.786,76.247,313.198,57.352,283.333z" />
                                        </svg>

                                        <svg version="1.1" class="view" xmlns="http://www.w3.org/2000/svg" width="20px" viewBox="0 0 512 512">
                                            <path d="M256,96C144.341,96,47.559,161.021,0,256c47.559,94.979,144.341,160,256,160c111.656,0,208.439-65.021,256-160 C464.441,161.021,367.656,96,256,96z M382.225,180.852c30.082,19.187,55.572,44.887,74.719,75.148 c-19.146,30.261-44.639,55.961-74.719,75.148C344.428,355.257,300.779,368,256,368c-44.78,0-88.428-12.743-126.225-36.852 c-30.08-19.188-55.57-44.888-74.717-75.148c19.146-30.262,44.637-55.962,74.717-75.148c1.959-1.25,3.938-2.461,5.929-3.65 C130.725,190.866,128,205.613,128,221c0,70.691,57.308,128,128,128c70.691,0,128-57.309,128-128 c0-15.387-2.725-30.134-7.703-43.799C378.285,178.39,380.266,179.602,382.225,180.852z M256,205c0,26.51-21.49,48-48,48 s-48-21.49-48-48s21.49-48,48-48S256,178.49,256,205z" />
                                        </svg>
                                    </div>

                                    <div class="container-login100-form-btn m-t-17">
                                        <asp:Button ID="ButtonVhod" runat="server" Text="Войти"
                                            CssClass="login100-form-btn" OnClick="LoginButton_Click" />
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>