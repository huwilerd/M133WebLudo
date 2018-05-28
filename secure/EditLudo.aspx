<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditLudo.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ludothek bearbeiten</title>
    <link href="design/formStyle.css" rel="stylesheet" />
</head>
<body>
	
    <form id="ludoForm" runat="server">
		<div id="mainContainer">
            <table>
                <tr>
                    <td><h1 id="ludoFormTitle" runat="server">Ludothek bearbeiten:</h1></td>
                </tr>

		        <tr>
                <td>
				    <label for="LudoNameField">Ludothekname:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="LudoNameField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="requiredInputNameFieldValidator" ControlToValidate="LudoNameField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="LudoStrasseField">Strasse:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="LudoStrasseField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="SpielDescriptionFieldValidator" ControlToValidate="LudoStrasseField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="LudoPostleitzahlField">Postleitzahl:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="LudoPostleitzahlField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator6" ControlToValidate="LudoPostleitzahlField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                 <tr>
                <td>
				    <label for="LudoOrtField">Ort:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="LudoOrtField" runat="server" />
                    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator1" ControlToValidate="LudoOrtField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

		        <tr>
			    <td>
                    <p class="error" id="servererror" runat="server"></p>
                    
			    </td>
			    <td>
                    <a href="MainMenu.aspx?page=8"><   Zurück</a>
				    <input type="submit" value="Speichern" id="saveButton" runat="server"/>
                    
			    </td>
		    </tr>
		    </table>
         </div>
    </form>
</body>
</html>
