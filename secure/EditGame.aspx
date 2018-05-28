<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditGame.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Spiel bearbeiten</title>
    <link href="design/formStyle.css" rel="stylesheet" />
</head>
<body>
	
    <form id="gameForm" runat="server">
		<div id="mainContainer">
            <table>
                <tr>
                    <td><h1 id="gameFormTitle" runat="server">Spiel bearbeiten:</h1></td>
                </tr>

		        <tr>
                <td>
				    <label for="SpielNameField">Spielname:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="SpielNameField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="requiredInputNameFieldValidator" ControlToValidate="SpielNameField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="SpielDescriptionField">Beschreibung:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="SpielDescriptionField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="SpielDescriptionFieldValidator" ControlToValidate="SpielDescriptionField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                 <tr>
                <td>
				    <label for="SpielImageLinkField">Bild-Url:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="SpielImageLinkField" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="VerlagField">Verlag:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="VerlagField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="VerlagFieldValidator" ControlToValidate="VerlagField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="LagerbestandField">Lagerbestand:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="LagerbestandField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator6" ControlToValidate="LagerbestandField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="KategorieField">Kategorie:</label>	 
                </td>
                <td>     
                    <select id="KategorieField" runat="server" required>
                    </select>
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="TarifKategorieField">Tarif-Kategorie:</label>	 
                </td>
                <td>     
                    <select id="TarifKategorieField" runat="server">
                    </select>
                </td>
		        </tr>

		        <tr>
			    <td>
                    <p class="error" id="servererror" runat="server"></p>
                    
			    </td>
			    <td>
                    <a href="MainMenu.aspx?page=5"><   Zurück</a>
				    <input type="submit" value="Speichern" id="saveButton" runat="server"/>
                    
			    </td>
		    </tr>
		    </table>
         </div>
    </form>
</body>
</html>
