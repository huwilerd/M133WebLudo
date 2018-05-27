using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : SecureMasterPage
{

    private bool isEditMode = false;

    protected override void setupPageWithSession(Session session)
    {
        base.setupPageWithSession(session);
    }

    protected override void setupPage(Session session)
    {
        base.setupPage(session);
        handleParameter();
    }

    private void showAccountFields(bool show)
    {
        /*EmailFieldLabel.Visible = show;
        PasswortFieldLabel.Visible = show;
        EmailField.Visible = show;
        PasswortField.Visible = show;*/
    }

    protected override void handlePostback()
    {
        handleParameter();
        handleSave();
    }

    private void handleSave()
    {
        int gameId = getIntFromParameter("game");
        String newGameName = getValueFromForm("SpielNameField");
        String newVerlagName = getValueFromForm("VerlagField");
        int newLagerbestand = Convert.ToInt32(getValueFromForm("LagerbestandField"));
        int kategorie = Convert.ToInt32(Request.Form["KategorieField"]);
        int tarifKategorie = Convert.ToInt32(Request.Form["TarifKategorieField"]);
        String newGameDescription = getValueFromForm("SpielDescriptionField");
        String newGameLink = getValueFromForm("SpielImageLinkField");

        Spiel updatedGame = new Spiel(gameId, newGameName, newVerlagName, newLagerbestand, tarifKategorie, kategorie, newGameLink, newGameDescription);

        //todo validate

        if (isEditMode)
        {
            if (gameId != -1)
            {
                ServerResponse gameResponse = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).updateGame(updatedGame);
                if (gameResponse.getResponseStatus())
                {
                    leavePage();
                }
                else
                {
                    servererror.InnerText = "Es ist ein Problem aufgetreten: " + gameResponse.getResponseMessage();
                }
            }
        } else
        {
            ServerResponse addedGame = GetViewletProvider().GetEmployeeInterface(getCurrentSession()).addNewGame(updatedGame);
            if(addedGame.getResponseStatus())
            {
                leavePage();
            } else
            {
                servererror.InnerText = "Es ist ein Problem aufgetreten: " + addedGame.getResponseMessage();
            }
        }
    }

    private String getValueFromForm(String key)
    {
        return Request.Form[key]; 
    }

    protected override string getPageName()
    {
        return AppConst.DETAIL_FORM_PAGE_NAME;
    }

    private void handleParameter()
    {

        int gameParam = getIntFromParameter("game");

        String actionParam = getStringFromParameter("action");
        if (actionParam != null)
        {
            switch (actionParam)
            {
                case "edit":
                    isEditMode = true;
                    if (gameParam != -1)
                    {
                        ServerResponse response = ((ClientInterface)GetViewletProvider().GetPersonViewlet()).getSingleGame(gameParam);
                        if (response.getResponseStatus())
                        {
                            Spiel game = (Spiel)response.getResponseObject();
                            handleEditGame(game);
                        }
                    }
                    break;
                case "new":
                    isEditMode = false;
                    handleNewGame();
                    break;
                default:
                    leavePage();
                    break;
            }
        } else
        {
            leavePage();
        }
    }

    private void leavePage()
    {
        Response.Redirect("../secure/MainMenu.aspx?page=5");
    }

    private void handleEditGame(Spiel spiel)
    {
        gameFormTitle.InnerText = "Spiel bearbeiten";
        SpielNameField.Text = spiel.name;
        VerlagField.Text = spiel.verlag;
        LagerbestandField.Text = Convert.ToString(spiel.lagerbestand);
        KategorieField.Value = KategorieField.Items.FindByValue(Convert.ToString(spiel.kategorie)).Value;
        TarifKategorieField.Value = TarifKategorieField.Items.FindByValue(Convert.ToString(spiel.tarifkategorie)).Value;
        SpielDescriptionField.Text = spiel.description;
        SpielImageLinkField.Text = spiel.imageLink;
    }

    private void handleNewGame()
    {
        gameFormTitle.InnerText = "Neues Spiel erfassen";
    }

}