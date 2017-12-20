<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditProfil.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Personalien</title>
    <link href="design/style.css" rel="stylesheet" />
</head>
<body>
	
    <form id="loginForm" runat="server">
		<div id="mainContainer">
            <table>
                <tr>
                    <td><h1>Personalien:</h1></td>
                </tr>
                <tr>
                <td>
				    <label for="AnredeField">Anrede:</label>	 
                </td>
                <td>     
                    <select id="AnredeField" runat="server" required>
                        <option value="Herr" selected="selected">Herr</option>
                        <option value="Frau">Frau</option>
                    </select>
                </td>
		        </tr>
		        <tr>
                <td>
				    <label for="VornameField">Vorname:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="VornameField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="requiredInputVornameFieldValidator" ControlToValidate="VornameField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>
                <tr>
                <td>
				    <label for="NachnameField">Nachname:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="NachnameField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="requiredInputNachnameFieldValidator" ControlToValidate="NachnameField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>
                <tr>
                <td>
				    <label for="GeburtsdatumField">Geburtsdatum:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="GeburtsdatumField" TextMode="Date" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator3" ControlToValidate="GeburtsdatumField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="TelefonField">Telefon:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="TelefonField" TextMode="Phone" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator4" ControlToValidate="TelefonField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="StrasseField">Strasse:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="StrasseField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator5" ControlToValidate="StrasseField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="PLZField">Postleitzahl:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="PLZField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator6" ControlToValidate="PLZField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="OrtField">Ort:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="OrtField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator7" ControlToValidate="OrtField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="LandField">Land:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="LandField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator8" ControlToValidate="LandField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                 <tr>
                <td>
				    <label for="EmailField">E-Mail:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="EmailField" runat="server" />
				    <asp:RequiredFieldValidator class="error" ID="RequiredFieldValidator1" ControlToValidate="EmailField" ErrorMessage="Bitte ausfüllen" runat="server" />
                </td>
		        </tr>

                <tr>
                <td>
				    <label for="PasswortField">Passwort:</label>	 
                </td>
                <td>     
				    <asp:TextBox ID="PasswortField" TextMode="Password" runat="server" />
                </td>
		        </tr>

		    <tr>
			    <td><p class="error" id="servererror" runat="server"></p></td>
			    <td>
				    <input type="submit" value="Speichern" id="saveButton" runat="server"/>
			    </td>
		    </tr>
		    </table>
         </div>
    </form>
</body>
</html>
