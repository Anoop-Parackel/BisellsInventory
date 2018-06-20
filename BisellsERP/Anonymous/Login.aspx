<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BisellsERP.Anonymous.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Bisells | Login</title>
    <style>
        @import url(https://fonts.googleapis.com/css?family=Nunito);

        body {
            padding: 0;
            margin: 0;
            height: 100%;
            width: 100%;
            font-family: "Nunito", sans-serif;
            background-color: #f5f5f5;
        }

        .bg-head {
            background: url(/Theme/images/login-dash/head-bg.jpg) no-repeat top left;
            background-color: #50554e;
            background-size: cover;
            position: relative;
            height: 60vh;
        }

            .bg-head h1 {
                color: #f2f2f2;
                line-height: 1.2em;
                text-transform: capitalize;
                margin-bottom: 0;
            }

            .bg-head h4 {
                color: #90A4AE;
                margin: 0;
            }

        .product-desc {
            position: absolute;
            top: 50%;
            transform: translateY(-50%);
            margin-left: 4em;
        }

            .product-desc > img {
                width: 300px;
                background-color: rgba(255, 255, 255, 0.8);
                padding: 0px 2em;
                border-radius: 5px;
            }

        .bg-foot {
            position: fixed;
            bottom: 0;
            height: 40vh;
            width: 100%;
            /*background: #f5f5f5;*/
            background: url(/Theme/images/login-dash/cream_pixels.png);
        }

        .features {
            height: 100%;
            width: 100%;
            padding: 0 40px;
        }

        .login-page {
            width: 350px;
            position: absolute;
            top: 50%;
            right: 0%;
            left: auto;
            transform: translate(-40%,-50%);
        }

        .form {
            position: relative;
            z-index: 1;
            background: #f2f2f2;
            /*background: url('../Theme/images/login-dash/dust_scratches.png');*/
            max-width: 350px;
            padding: 20px 35px;
            border-radius: 3px;
            box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
        }
            .form h3 {
                color: #455A64;
                margin-bottom: 2em;
                text-align: center;
            }

            .form > img {
                width: 80%;
            }

            .form input[type="text"], .form input[type="password"] {
                font-family: "Nunito", sans-serif;
                outline: 0;
                background: #fff;
                border-radius: 3px;
                box-shadow: 0 2px 1px 0 rgba(0,0,0,.1), inset 0 2px 2px rgba(0, 0, 0, 0.14);
                width: 100%;
                border: 0;
                margin: 0 0 15px;
                padding: 13px;
                box-sizing: border-box;
                font-size: 16px;
                color: #455A64;
            }

        .login-btn {
            font-family: "Nunito", sans-serif;
            text-transform: uppercase;
            outline: 0;
            background-color: #3e95cd !important;
            width: 100%;
            border: 0;
            padding: 15px;
            color: #FFF;
            font-size: 14px;
            border-radius: 2px;
            -webkit-transition: all .1s ease-in-out;
            transition: all .1s ease-in-out;
            box-shadow: 0px 2px 2px 0 #47a3de;
            cursor: pointer;
            opacity: .9;
            margin-top: 30px;
        }

            .login-btn:hover, .login-btn:active, .login-btn:focus {
                opacity: 1;
            }

        .hidden-xs {
            display: block;
        }

        .erp-vec {
            display: inline-block;
            width: 25%;
            position: absolute;
            right: 36%;
            bottom: 5%;
        }

        .contact-details {
            /*width: 25%;
            display: inline-block;
            float: left;*/
            width: 25%;
            height: 250px;
            position: absolute;
            top: 50%;
            transform: translateY(-50%);
        }
        .contact-details:nth-of-type(1) {
            left: 25%;
        }
        .contact-details .contact {
            font-size: 12px;
            list-style: circle;
            color: #607D8B;
        }
        .contact-details h5 {
            margin-bottom: 15px;
            color: #607D8B;
        }
        .contact-details p {
            margin-bottom: 0;
            margin-top: 10px;
        }
        .contact-details a {
            color: #509fd1;
        }

        /* MediaQueries*/
        @media only screen and (max-width : 768px) {
            .product-desc > img {
                width: 250px;
            }

            .login-page {
                width: 400px;
                right: auto;
                left: 50%;
                transform: translate(-50%,-50%);
            }

            .product-desc {
                top: 5%;
                left: 50%;
                margin-left: 0;
                transform: translateX(-50%);
            }

                .product-desc h1, .product-desc h4 {
                    display: none;
                }

            .hidden-xs {
                display: none;
            }
        }

        @media only screen and (max-width : 425px) {
            .login-page {
                width: 360px;
                top: 60%;
            }

            .hidden-xs {
                display: none;
            }
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        .fadeIn {
            animation: fadeIn .75s linear;
        }
    </style>
</head>
<body>
    <div class="bg-head">

        <div class="product-desc">
            <img src="/Theme/images/login-dash/logo.png" alt="Bisells ERP Logo" />
            <h1>accounting & inventory
                <br />
                software</h1>
            <h4>A complete ERP solution for your business</h4>
        </div>
    </div>
    <div class="bg-foot">
        <div class="features hidden-xs">
            <div class="contact-details">
                    <h5>Middle East- UAE-Dubai</h5>
                    <ul class="contact">
                        <li>
                            <p>
                                <i class="fa fa-map-marker"></i><strong>Address: </strong> Maclink Infosolutions<br />
                                Dubai National Insurance Building <br />
                                Office 405,Port Saeed Road, Dubai, UAE. <br />
                                P.O Box 378484
                            </p>
                        </li>
                        <li>
                            <p><i class="fa fa-phone"></i><strong>Phone: </strong> +971-42516656</p>
                        </li>
                        <li>
                            <p>
                                <i class="fa fa-envelope"></i><strong>Email: </strong> <a href="mailto:info@maclinkinfo.com">info@maclinkinfo.com</a>
                            </p>
                        </li>
                        <li>
                            <p><i class="fa fa-envelope"></i><strong>Location: </strong> <a href="https://www.google.ae/maps/place/Maclink/@25.2533032,55.3266066,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f5cd88e15ca0b:0xa4c59b90d6d627b4!8m2!3d25.2533032!4d55.3287953">Dubai</a></p>
                        </li>
                    </ul>
                </div>
            <div class="contact-details">
                    <h5>Asia Pacific- INDIA-Trivandrum</h5>
                    <ul class="contact">
                        <li>
                            <p>
                                <i class="fa fa-map-marker"></i><strong>Address: </strong> Maclink Info Pvt.Ltd.<br />
                                Chellam Building 2nd Floor <br />
                                Sreekrishna Lane, Behind Winsor Palace <br />
                                Kowdiar, Trivandrum<br />
                                INDIA<br />
                                P.O Box 695003
                            </p>
                        </li>
                        <li>
                            <p><i class="fa fa-phone"></i><strong>Phone: </strong> +91-9656-044222</p>
                        </li>

                        <li>
                            <p><i class="fa fa-envelope"></i><strong>Email: </strong> <a href="mailto:info@maclinkinfo.com">info@maclinkinfo.com</a></p>
                        </li>
                        <li>
                            <p><i class="fa fa-envelope"></i><strong>Location: </strong> <a href="https://www.google.ae/maps/place/Maclink/@25.2533032,55.3266066,17z/data=!3m1!4b1!4m5!3m4!1s0x3e5f5cd88e15ca0b:0xa4c59b90d6d627b4!8m2!3d25.2533032!4d55.3287953">Trivandrum</a></p>
                        </li>
                    </ul>
                </div>


            <%--            <div class="text-rotator">
              <div class="slider">
                <div class="mask">
                  <ul>
                    <li class="anim1">
                         <div class="quote">
                            <h2>This is a heading 1</h2>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus qui distinctio at, ullam id impedit.
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus qui distinctio at, ullam id impedit.
                          </div>
                    </li>
                    <li class="anim2">
                          <div class="quote">
                            <h2>This is a heading 2</h2>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Modi facere ipsa asperiores dignissimos autem id.
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus qui distinctio at, ullam id impedit.
                          </div>
                    </li>
                    <li class="anim3">
                         <div class="quote">
                            <h2>This is a heading 3</h2>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sunt illum blanditiis quidem ipsam nam nostrum?
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus qui distinctio at, ullam id impedit.
                          </div>
                    </li>
                    <li class="anim4">
                         <div class="quote">
                            <h2>This is a heading 4</h2>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Optio exercitationem ipsam hic atque officiis fugiat!
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus qui distinctio at, ullam id impedit.
                          </div>
                    </li>
                    <li class="anim5">
                         <div class="quote">
                            <h2>This is a heading 5</h2>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nostrum soluta, perspiciatis mollitia fugit ipsum, illum!
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Delectus qui distinctio at, ullam id impedit.
                          </div>
                    </li>
                  </ul>
                </div>
              </div>
            </div>--%>

            <div class="erp-vec">
                <%--<img src="../Theme/images/login-dash/1.svg" alt=""/>--%>
            </div>
        </div>
    </div>
    <div class="login-page fadeIn">
        <form id="form1" runat="server" class="form">
            <h3>Login to Bisells ERP</h3>
            <div class="login-form">
                <asp:TextBox ID="txtUserName" runat="server" ClientIDMode="Static" type="text" placeholder="username"></asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" type="password" ClientIDMode="Static" placeholder="password"></asp:TextBox>
                <asp:CheckBox ID="chkRememberMe" runat="server" /><span>Remember Me</span>
                <%--<input type="checkbox" />--%>
                <asp:Button ID="btnLogin" runat="server" Text="login" CssClass="login-btn" OnClick="btnLogin_Click" />
                <span id="error" style="color: red" visible="false" runat="server">Username or Password Incorrect</span>
            </div>
        </form>
        <!-- <p class="message">© Copyright 2017 | <a href="#">Maclink Info Solution</a></p> -->
    </div>
</body>
</html>
