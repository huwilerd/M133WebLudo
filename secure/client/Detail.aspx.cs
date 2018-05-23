using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : SecureMasterPage
{

    private Game currentGame;
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
        int gameId = getGameId();
        if(gameId > 0)
        {
            currentGame = DataProvider.getInstance().getGameFromId(gameId);
            gameNameLabel.InnerText = currentGame.name + " ausleihen";
            gameDescriptionLabel.InnerText = currentGame.description;

            int actionId = getActionCode();
            if(actionId >= 0)
            {
                isVerlaengern = actionId == 1;
                if(isVerlaengern)
                {
                    int hireId = getIntFromParameter("hire");
                    if(hireId > 0)
                    {
                        currentHire = DataProvider.getInstance().getHireOfId(hireId);
                        currentHire.toDate.AddDays(AppConst.HIRE_AMOUNT_DAYS);
                        gameNameLabel.InnerText = "Ausleihe von " + currentGame.name + " verlängern";
                        tryButton.Value = "Verlängerung beantragen";
                        ToDateLabel.Visible = true;
                        ToDateLabel.InnerHtml = currentHire.toDate.AddDays(AppConst.HIRE_AMOUNT_DAYS).ToString("dd.MM.yyyy");
                        VonDateLabel.InnerText = "Ausleihe verlängern bis:";
                        VonDateField.Visible = false;
                        VonDateField.Text = currentHire.fromDate.ToString();
                    }
                }
                return;
            }
        }
           Response.Redirect("../public/LoginPage.aspx");
    }

    private int getGameId()
    {
        return getIntFromParameter("id");
    }

    private int getActionCode()
    {
        return getIntFromParameter("action");
    }

    private int getIntFromParameter(String param)
    {
        String gameIdString = Request.QueryString[param];
        int gameId;
        bool isNumeric = int.TryParse(gameIdString, out gameId);
        if (isNumeric)
        {
            return gameId;
        }
        return -1;
    }

    protected override void handlePostback()
    {
        setupInformationBasedOnUrlParameters();
        handleSave();
    }

    private void handleSave()
    {
        bool hasError = false;
        User user = DataProvider.getInstance().getUserFromToken(getSessionKey());

        if (isVerlaengern)
        {
            if (DataHandler.getInstance().canExpandHire(currentHire))
            {
                currentHire.toDate = currentHire.toDate.AddDays(AppConst.HIRE_AMOUNT_DAYS);
                DataHandler.getInstance().saveExpandedHire(currentHire);
            } else
            {
                hasError = true;
                servererror.InnerText = "Die Ausleihe kann nicht mehr als drei Wochen verlängert werden.";
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

            DateTime currentDate = DateTime.Now;
            if(fromDate.Date < currentDate.Date)
            {
                servererror.InnerText = "Die Ausleihe darf nicht in der Vergangenheit liegen.";
                hasError = true;
            }

            Hire newHire = new Hire(-1, user.userId, currentGame.gameId, fromDate, toDate, 0, false);

            if(!hasError) { 
                Hire savedHire = DataHandler.getInstance().saveHireOfUser(newHire);
            }
        }
        if (!hasError)
        {
            Response.Redirect("../secure/MainMenu.aspx");
        }
    }

    protected override string getPageName()
    {
        return AppConst.DETAIL_FORM_PAGE_NAME;
    }

}