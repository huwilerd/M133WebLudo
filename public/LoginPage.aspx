<%@ Page Language="C#" CodeFile="LoginPage.aspx.cs" Inherits="LoginPageController" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="design/style.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="loginForm" runat="server">
		<div id="mainContainerLogin">

            

            <table>
                <tr>
                    <td>
                        <h1>Login:</h1>
                    </td>
                </tr>
		        <tr>
                    <td>
				        <label for="EmailField">E-Mail:</label>
		            </td>
			        <td>
				        <asp:TextBox ID="EmailField" runat="server" />
				        <asp:RequiredFieldValidator class="error" ID="requiredInputFieldValidator" ControlToValidate="EmailField" ErrorMessage="Bitte ausfüllen" runat="server" />
			        </td>
		        </tr>
		        <tr>
			        <td>
				        <label for="PasswordField" runat="server" />Password:</label>
			        </td>
			        <td>
				        <asp:TextBox ID="PasswordField" TextMode="Password" runat="server" />
				        <asp:RequiredFieldValidator class="error" ID="requiredInputPwFieldValidator" ControlToValidate="PasswordField" ErrorMessage="Bitte ausfüllen" runat="server" />
			        </td>
		        </tr>
		        <tr>
			        
			        <td>
				        <input type="submit" value="Login" id="loginButton" runat="server"/>
			        </td>
                    <td>
                        <a href="RegisterPage.aspx">Noch kein Login?</a><br />
                        <a href="ForgotPage.aspx">Passwort zurücksetzen.</a>
                    </td>
		        </tr>
                <tr>
                    <td colspan="2"><p id="servererror" runat="server"></p></td>
                </tr>
		    </table>
        </div>
    </form>
</body>
</html>
