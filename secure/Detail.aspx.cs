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
        /*int userId = session.userId;
        User user = DataProvider.getInstance().getUserFromId(userId);*/
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
            DateTime fromDate = Convert.ToDateTime(VonDateField.Text);
            DateTime toDate = fromDate.AddDays(AppConst.HIRE_AMOUNT_DAYS);

            if(fromDate.Date <= DateTime.Now)
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

    protected override string getPageName()
    {
        return AppConst.DETAIL_FORM_PAGE_NAME;
    }

}