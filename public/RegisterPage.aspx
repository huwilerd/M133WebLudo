<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterPage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrieren</title>
    <link href="design/style.css" rel="stylesheet" />
    <!--===============================================================================================-->	
	<link rel="icon" type="image/png" href="design/login/images/icons/favicon.ico"/>
    <!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="design/login/vendor/bootstrap/css/bootstrap.min.css">
    <!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="design/login/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="design/login/vendor/animate/animate.css">
    <!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="design/login/vendor/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="design/login/vendor/select2/select2.min.css">
    <!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="design/login/css/util.css">
	<link rel="stylesheet" type="text/css" href="design/login/css/main.css">
    <!--===============================================================================================-->

</head>
<body>
    <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100" style="padding-top:100px;">
				<div class="login100-pic js-tilt" data-tilt>
					<img src="design/login/images/img-01.png" alt="IMG">
				</div>

				<form class="login100-form validate-form" id="loginForm" runat="server">
					<span class="login100-form-title">
						Member Register
					</span>

					<div class="wrap-input100 validate-input" data-validate = "Valid email is required: ex@abc.xyz">
						
                        <asp:TextBox ID="EmailField" runat="server" CssClass="input100" type="text" name="email" placeholder="Email"/>
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<i class="fa fa-envelope" aria-hidden="true"></i>
						</span>
					</div>

                    <div class="wrap-input100 validate-input" data-validate = "Valid email is required: ex@abc.xyz">
						
                        <asp:TextBox ID="EmailRepeatField" runat="server" CssClass="input100" type="text" name="email" placeholder="Repeat Email"/>
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<i class="fa fa-envelope" aria-hidden="true"></i>
						</span>
					</div>

					<div class="wrap-input100 validate-input" data-validate = "Password is required">
						
                        <asp:TextBox ID="PasswordField" TextMode="Password" runat="server" CssClass="input100" type="password" name="pass" placeholder="Password"/>
                        
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<i class="fa fa-lock" aria-hidden="true"></i>
						</span>
					</div>
					

                    <div class="wrap-input100 validate-input" data-validate = "Password is required">
						
                        <asp:TextBox ID="PasswordRepeatField" TextMode="Password" runat="server" CssClass="input100" type="password" name="pass" placeholder="Repeat Password"/>
                        
						<span class="focus-input100"></span>
						<span class="symbol-input100">
							<i class="fa fa-lock" aria-hidden="true"></i>
						</span>
					</div>


					<div class="container-login100-form-btn">
						
                        <input type="submit" value="Registrieren" id="registerButton" runat="server" class="login100-form-btn"/>
                        
                        
					</div>

					<div class="text-center p-t-12">
						<span class="txt1">
							Forgot
						</span>
						<a class="txt2" href="#">
							Username / Password?
						</a>
					</div>
                    <div id="validation">
                        <div id="servererror" runat="server">

                        </div>
                        <asp:RequiredFieldValidator class="error" ID="requiredInputPwFieldValidator" ControlToValidate="PasswordField" ErrorMessage="Bitte eine korrekte E-mail angeben <br />" runat="server" />
                        <asp:RequiredFieldValidator class="error" ID="requiredInputFieldValidator" ControlToValidate="EmailField" ErrorMessage="Bitte ein richtiges Passwort eingeben <br />" runat="server" />

                        
                        <asp:CompareValidator class="error" ID="CompareMailValidator" ControlToValidate="EmailRepeatField" ControlToCompare="EmailField" ErrorMessage="E-Mail Adressen stimmen nicht überein. <br />" type="String" runat="server" />
                        <asp:CompareValidator class="error" ID="CompareValidator1" ControlToValidate="PasswordField" ControlToCompare="PasswordRepeatField" ErrorMessage="Passwörter stimmen nicht überein." type="String" runat="server" />
                    </div>
					<div class="text-center p-t-136" style="padding-top:10px;">
						<a class="txt2" href="LoginPage.aspx">
							Zurück zum Login
							<i class="fa fa-long-arrow-right m-l-5" aria-hidden="true" style="transform: rotate(180deg);"></i>
						</a>
					</div>

                    
                    
				</form>

                
				

            </div>
		</div>
	</div>
	
    
    <!--===============================================================================================-->	
	<script src="design/login/vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
	<script src="design/login/vendor/bootstrap/js/popper.js"></script>
	<script src="design/login/vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
	<script src="design/login/vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
	<script src="design/login/vendor/tilt/tilt.jquery.min.js"></script>
	<script >
		$('.js-tilt').tilt({
			scale: 1.1
		})

	    

	</script>
    <!--===============================================================================================-->
	<script src="js/main.js"></script>

    
</body>
</html>
