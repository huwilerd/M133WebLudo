<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMenu.aspx.cs" Inherits="MainMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link rel="stylesheet" href="design/design.css" />

</head>
<body>
    <form id="form1" runat="server">
    <header class="clearfix">
        <div id="mainContainerHeader">
            <nav>
                
                <div class="navigationElement" id="user">
                    
                        Willkommen <span id="username" runat="server">XXXXX</span>, <a href="EditProfil.aspx" />PROFIL</a> <a href="Logout.aspx" runat="server" id="logoutLink" >Abmelden</a>
                    

                    
                </div>
                
            </nav>
        </div>
    </header>
    
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
        </main>
    </div>
    </form>
    <footer>

    </footer>










    
</body>
</html>
