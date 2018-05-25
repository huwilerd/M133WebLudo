<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link rel="stylesheet" href="design/design.css" />

</head>
<body>
    <header class="clearfix">
        <div id="mainContainerHeader">
            <nav>
                
                <div class="navigationElement" id="user">
                    
                        Willkommen <span id="username" runat="server">XXXXX</span>, <a href="EditProfil.aspx" />PROFIL</a> <a href="Logout.aspx" runat="server" id="logoutLink" >Abmelden</a>
                    

                    
                </div>
                
            </nav>
        </div>
    </header>
    
    <form id="mainMenuForm" runat="server">
    <div id="wrapper">
        <aside>
            <div id="containterSideBar">
                <div class="sideElement">
                    <asp:LinkButton ID="newestGamesLink" Text="Neuste Spiele" OnClick="showNewestGames" runat="server" />
                </div>
                <div class="sideElement">
                    <asp:LinkButton ID="currentHiresLink" Text="Aktuelle Ausleihen" OnClick="showCurrentHires" runat="server" />
                </div>
                <div class="sideElement">
                    <asp:LinkButton ID="closedHiresLink" Text="Abgeschlossene Ausleihen" OnClick="showClosedHires" runat="server" />
                </div>
                <div class="sideElement">
                    <asp:LinkButton ID="allEmployees" Text="Mitarbeiter Verwalten" OnClick="showAllEmployees"  runat="server" />
                </div>
                <div class="sideElement">
                    <asp:LinkButton ID="allGames" Text="Spiele Verwalten" OnClick="showAllGames" runat="server" />
                </div>
                <div class="sideElement">
                    <asp:LinkButton ID="allClients" Text="Kunden Verwalten" OnClick="showAllClients" runat="server" />
                </div>
                <div class="sideElement">
                    <asp:LinkButton ID="dashboard" Text="Gesamtübersicht" OnClick="showDashboard" runat="server" />
                </div>
            </div>

        </aside>
        <main>
            <div id="containerMain">
                
                <div id="flexContainer" runat="server">

                    <!--Items-->
                    <div class="flexItem">
                        <div class="mainItemImage">
                            <img src="http://1images.cgames.de/images/gamestar/207/call-of-duty-4-modern-warfare_1821710.jpg" />
                        </div>
                        <div class="mainItemTitle">
                            <h1>Call of Duty</h1>
                            <p>Call of Duty ist eine Computerspielreihe des amerikanischen Publishers Activision aus dem Genre der Ego-Shooter. Der Spieler übernimmt darin üblicherweise die Rolle eines Soldaten in einem Kriegsszenario.</p>
                        </div>
                        <button class="button" style="vertical-align:middle"><span>Jetzt auslehnen </span></button>
                    </div>
                    <div class="flexItem">
                        <div class="mainItemImage">
                            <img src="http://1images.cgames.de/images/gamestar/207/call-of-duty-4-modern-warfare_1821710.jpg" />
                        </div>
                        <div class="mainItemTitle">
                            <h1>Call of Duty</h1>
                            <p>Call of Duty ist eine Computerspielreihe des amerikanischen Publishers Activision aus dem Genre der Ego-Shooter. Der Spieler übernimmt darin üblicherweise die Rolle eines Soldaten in einem Kriegsszenario.</p>
                        </div>
                        <button class="button" style="vertical-align:middle"><span>Ausleihe verlängern</span></button>
                    </div>
                    

		            <a href="#!" onclick="handleLogoutClick()" runat="server">Ausloggen</a>

                </div>  
            </div>

            <div id="addGameForm" runat="server">
                        
                <div class="row">
                    <h1>Neues Spiel hinzufügen</h1>
                </div>

                <div class="row">
                    <label for="gameNameField">Spielname:</label>
                    <asp:TextBox ID="gameNameField" runat="server" CssClass="input100" type="text" name="gameNameField" placeholder=""/>
                </div>

                <div class="row">
                    <label for="gameNameField">Verlag:</label>
                    <asp:TextBox ID="verlagField" runat="server" CssClass="input100" type="text" name="verlagField" placeholder=""/>
                </div>

                 <div class="row">
                    <label for="lagerbestandField">Lagerbestand:</label>
                    <asp:TextBox ID="lagerbestandField" runat="server" CssClass="input100" type="text" name="lagerbestandField" placeholder=""/>
                </div>

                <div class="row">
                    <label for="tarifkategorieField">Tarifkategorie:</label>
                     <select id="tarifkategorieField" runat="server" required>
                        <option value="Männlich" selected="selected">Kinder</option>
                        <option value="Weiblich">Frau</option>
                    </select>
                </div>

                <div class="row">
                    <label for="kategorieField">Kategorie:</label>
                     <select id="Select1" runat="server" required>
                        <option value="Männlich" selected="selected">Kinder</option>
                        <option value="Weiblich">Frau</option>
                    </select>
                </div>

                <div class="row">
                    <asp:LinkButton ID="AddGameButton" Text="Spiel hinzufügen" OnClick="addGame" runat="server" />
                </div>"

            </div>
            </form>
        </main>
    </div>
    <footer>

    </footer>
    
</body>
</html>
