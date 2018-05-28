using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : SecureMasterPage
{

    private Spiel currentGame;
    private Hire currentHire;
    private bool isVerlaengern;

    protected override void setupPageWithSession(Session session)
    {
        base.setupPageWithSession(session);
        setupInformationBasedOnUrlParameters();

    }

    private void setupInformationBasedOnUrlParameters()
    {
        handleIdParameter();
        handleActionParameter();
        handleHireParameter();
    }

    private void handleIdParameter()
    {
        int gameId = getGameId();
        if (gameId > 0)
        {
            ServerResponse gameResponse = ((ClientInterface)GetViewletProvider().GetPersonViewlet()).getSingleGame(gameId);
            if (gameResponse.getResponseStatus())
            {
                Spiel spiel = (Spiel)gameResponse.getResponseObject();
                currentGame = spiel;
                gameNameLabel.InnerText = currentGame.name + " ausleihen";
                gameDescriptionLabel.InnerText = spiel.description;
            }
            else
            {
                gameNameLabel.InnerText = "Dieses Spiel wurde leider nicht gefunden";
                Response.Redirect("../secure/MainMenu.aspx?page=0");
            }
        }
    }

    private void handleActionParameter()
    {
        int actionId = getActionCode();
        int verlaengernCode = 1;
        isVerlaengern = actionId == verlaengernCode;
    }

    private void handleHireParameter()
    {
            if (isVerlaengern)
            {
                int hireId = getIntFromParameter("hire");
                if (hireId > 0)
                {
                ServerResponse hireResponse = ((ClientInterface)GetViewletProvider().GetPersonViewlet()).getSingleHire(hireId, getCurrentSession());

                if(hireResponse.getResponseStatus())
                {
                    currentHire = (Hire)hireResponse.getResponseObject();

                    gameNameLabel.InnerText = "Ausleihe von " + currentGame.name + " verlängern";
                    tryButton.Value = "Verlängerung beantragen";
                    ToDateLabel.Visible = true;
                    currentHire.BisDatum = currentHire.BisDatum.AddDays(AppConst.HIRE_AMOUNT_DAYS);
                    ToDateLabel.InnerHtml = currentHire.BisDatum.ToString("dd.MM.yyyy");
                    VonDateLabel.InnerText = "Ausleihe verlängern bis:";
                    VonDateField.Visible = false;
                    VonDateField.Text = currentHire.VonDatum.ToString();
                } else
                {
                    Response.Redirect("../secure/MainMenu.aspx?page=1");
                }
                    
            }
        }
        else
        {
            VonDateField.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        setDetailInformation();

    }

    private void setDetailInformation()
    {
        if (currentGame != null)
        {
            ServerResponse serverResp = ServerViewletProvider.getInstance().GetSessionInterface().getPersonFromSession(getCurrentSession());
            if(serverResp.getResponseStatus())
            {
                Person currentPerson = (Person)serverResp.getResponseObject();
                String hireCosts = CalcViewlet.wrapInCurrency(CalcViewlet.create().calculatePrice(getCurrentSession(), currentGame, currentPerson));
                String kategorieName = CalcViewlet.create().getKategorieName(currentGame.kategorie);
                String kategorieInfo = kategorieName == null ? "Unbekannt" : kategorieName;
                detailDescription.InnerHtml = "Ausleihekosten: " + hireCosts + " <br>Kategorie: " + kategorieInfo;
                return;
            }
            
        }
            detailDescription.InnerText = "Keine Informationen verfügbar";
    }

    private int getGameId()
    {
        return getIntFromParameter("id");
    }

    private int getActionCode()
    {
        return getIntFromParameter("action");
    }

    protected override void handlePostback()
    {
        setupInformationBasedOnUrlParameters();
        handleSave();
    }

    private void handleSave()
    {
        bool hasError = false;
        User user = getCurrentSession().user;

        if (isVerlaengern)
        {

            ServerResponse response = GetViewletProvider().GetPersonViewlet().updateHire(currentHire);
            if(response.getResponseStatus())
            {
                Response.Redirect("../secure/MainMenu.aspx?page=1");
            } else
            {
                servererror.InnerText = response.getResponseMessage();
                hasError = true;
            }
                 
        }
        else
        {
            if (VonDateField.Text == null) {
                servererror.InnerText = "Bitte füllen Sie das Datumsfeld aus";
                hasError = true;
            }
            DateTime fromDate = Convert.ToDateTime(getValueFromForm("VonDateField"));
            DateTime toDate = fromDate.AddDays(AppConst.HIRE_AMOUNT_DAYS);

            if(fromDate.Date < DateTime.Now.Date)
            {
                servererror.InnerText = "Die Ausleihe darf nicht in der Vergangenheit liegen.";
                hasError = true;
            }

            Hire newHire = new Hire(-1, user.userId, currentGame.ID_Spiel, fromDate, toDate, false);

            if(!hasError) {
                ServerResponse response = GetViewletProvider().GetPersonViewlet().createHire(getCurrentSession(), newHire);
                if(!response.getResponseStatus())
                {
                    servererror.InnerText = response.getResponseMessage();
                    hasError = true;
                }
                
            }
        }
        if (!hasError)
        {
            Response.Redirect("../secure/MainMenu.aspx?page=1");
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

}