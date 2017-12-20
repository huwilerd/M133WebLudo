<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPage.aspx.cs" Inherits="public_ForgotPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>Bitte geben Sie ihre E-Mail an:</p>
        <table>
		<tr><td>
				<label for="EmailField">E-Mail:</label>
		    </td>
			<td>
				<asp:TextBox ID="EmailField" runat="server" />
				<asp:RequiredFieldValidator ID="requiredInputFieldValidator" ControlToValidate="EmailField" ErrorMessage="Bitte füllen Sie das Feld aus" runat="server" />
			</td>
		</tr>
           <tr><td>
				<label for="PasswortField">Neues Passwort:</label>
		    </td>
			<td>
				<asp:TextBox ID="PasswortField" TextMode="Password" runat="server" />
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="PasswortField" ErrorMessage="Bitte füllen Sie das Feld aus" runat="server" />
			</td>
		</tr>
        <tr>
			<td><p class="error" id="servererror" runat="server"></p></td>
			<td>
				<input type="submit" value="Passwort zurücksetzen" id="forgotButton" runat="server"/>
			</td>
		</tr>
    </form>
</body>
</html>
