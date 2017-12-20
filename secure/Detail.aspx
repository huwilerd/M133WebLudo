<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Personalien</title>
    <link href="design/detailStyle.css" rel="stylesheet" />
</head>
<body>
	
    <form id="loginForm" runat="server">
		<div id="mainContainer">
            <table>
                <tr>
                    <td colspan="2"><h1><span id="gameNameLabel" runat="server">XXXXX</span></h1></td>
                </tr>
                <tr>
                <td>
				    <label for="AnredeField">Beschreibung:</label>	 
                </td>
                <td>     
                   <p id="gameDescriptionLabel" runat="server"></p>
                </td>
		        </tr>
                <tr>
                <td>
				    <label for="VonDateField" id="VonDateLabel" runat="server">Ausleihungsbeginn-Datum:</label>	 
                </td>
                <td>
                    <span id="ToDateLabel" runat="server"></span>  
				    <asp:TextBox ID="VonDateField" TextMode="Date" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator3" ControlToValidate="VonDateField" ErrorMessage="Bitte mit Datum ausfüllen" runat="server" />
                </td>
		        </tr>

		    <tr>
			    <td><p class="error" id="servererror" runat="server"></p></td>
			    <td>
                    <a href="MainPage.aspx"><   Zurück</a>
				    <input type="submit" value="Jetzt Ausleihe beantragen" id="tryButton" runat="server"/>
			    </td>
		    </tr>
		    </table>
         </div>
    </form>
</body>
</html>
