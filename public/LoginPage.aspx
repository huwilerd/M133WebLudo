<%@ Page Language="C#" CodeFile="LoginPage.aspx.cs" Inherits="LoginPageController" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="design/style.css" rel="stylesheet" />
    <title>Login</title>
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
			<div class="wrap-login100">
				<div class="login100-pic js-tilt" data-tilt>
					<img src="design/login/images/img-01.png" alt="IMG">
				</div>

				<form class="login100-form validate-form" id="loginForm" runat="server">
					<span class="login100-form-title">
						Member Login
					</span>

					<div class="wrap-input100 validate-input" data-validate = "Valid email is required: ex@abc.xyz">
						
                        <asp:TextBox ID="EmailField" runat="server" CssClass="input100" type="text" name="email" placeholder="Email"/>
                        <!--<input class="input100" type="text" name="email" placeholder="Email">-->
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
					
					<div class="container-login100-form-btn">
						
                        <input type="submit" value="Login" id="loginButton" runat="server" class="login100-form-btn"/>
                        
					</div>

					<div class="text-center p-t-12">
						<span class="txt1">
							Forgot
						</span>
						<a class="txt2" href="ForgotPage.aspx">
							Username / Password?
						</a>
					</div>
                    <div id="validation">
                        <div id="servererror" runat="server">

                    </div>
                    <asp:RequiredFieldValidator class="error" ID="requiredInputPwFieldValidator" ControlToValidate="PasswordField" ErrorMessage="Bitte eine korrekte E-mail angeben" runat="server" /><br />
                    <asp:RequiredFieldValidator class="error" ID="requiredInputFieldValidator" ControlToValidate="EmailField" ErrorMessage="Bitte ein richtiges Passwort eingeben" runat="server" />
                    </div>
					<div class="text-center p-t-136">
						<a class="txt2" href="RegisterPage.aspx">
							Create your Account
							<i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>
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

