<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterPage.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrieren</title>
    <link href="design/style.css" rel="stylesheet" />
</head>
<body>
	
    <form id="loginForm" runat="server">
		<div id="mainContainer">
            <table>
                <tr>
                    <td><h1>Registrieren:</h1></td>
                </tr>
		        <tr>
                <td>
				    <label for="EmailField">E-Mail:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="EmailField" TextMode="Email" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="requiredInputFieldValidator" ControlToValidate="EmailField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		    </tr>
               <tr><td>
				    <label for="EmailRepeatField">E-Mail bestätigen:</label>
		        </td>
			    <td>
                    
				    <asp:TextBox ID="EmailRepeatField" TextMode="Email" runat="server" />
                    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator1" ControlToValidate="EmailRepeatField" ErrorMessage="Bitte ausfüllen" runat="server" />
                    <asp:CompareValidator class="error" ID="CompareMailValidator" ControlToValidate="EmailRepeatField" ControlToCompare="EmailField" ErrorMessage="Felder stimmen nicht überein." type="String" runat="server" />
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
				    <label for="PasswordRepeatField" runat="server" />Password:</label>
			    </td>
			    <td>
                    
				    <asp:TextBox ID="PasswordRepeatField" TextMode="Password" runat="server" />
                    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator2" ControlToValidate="PasswordRepeatField" ErrorMessage="Bitte ausfüllen" runat="server" />
                    <asp:CompareValidator class="error" ID="CompareValidator1" ControlToValidate="PasswordField" ControlToCompare="PasswordRepeatField" ErrorMessage="Felder stimmen nicht überein." type="String" runat="server" />
                </td>
		    </tr>
		    <tr>
			    <td><p class="error" id="servererror" runat="server"></p></td>
			    <td>
				    <input type="submit" value="Registrieren" id="registerButton" runat="server"/>
			    </td>
		    </tr>
		    </table>
         </div>
    </form>
</body>
</html>
