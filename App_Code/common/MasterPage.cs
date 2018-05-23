using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Zusammenfassungsbeschreibung für MasterPage
/// </summary>
public abstract class MasterPage : Page
{
    public MasterPage()
    {

    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Console.WriteLine("hey");
        if (IsPostBack)
        {
            handlePostback();
        }
        checkSession();
    }

    protected virtual void handlePostback()
    {

    }

    protected virtual void setupPage(Session session)
    {
        if (session != null)
        {
            setupPageWithSession(session);
        }
    }

    protected virtual void setupPageWithSession(Session session)
    {

    }

    private void checkSession()
    {
        String sessionKey = getSessionKey();
        if (sessionKey != null)
        {
            System.Diagnostics.Debug.WriteLine("Key " + sessionKey + " vorhanden");
            if (DataHandler.getInstance().hasTokenActiveSession(sessionKey))
            {
                User user = DataProvider.getInstance().getUserFromToken(sessionKey);
                UserDetail detail = DataHandler.getInstance().getDetailOfUser(user);
                if (detail != null)
                {
                    redirectUser(true);
                }
                else
                {
                    if (getPageName() != AppConst.DETAIL_FORM_PAGE_NAME)
                    {
                        Response.Redirect("../secure/EditProfil.aspx");
                    } else
                    {
                        setupPage(DataProvider.getInstance().getSessionFromToken(getSessionKey()));
                    }
                }
            }
            else
            {
                redirectUser(false);
            }

        }
        else
        {
            redirectUser(false);
            System.Diagnostics.Debug.WriteLine("No Sessionkey vorhanden");
        }
    }

    protected virtual String getPageName()
    {
        return "default";
    }

    private void redirectUser(bool toSecure)
    {
        if (isSecurePage() && !toSecure)
        {
            Response.Redirect("../public/LoginPage.aspx");
        }
        else if (!isSecurePage() && toSecure)
        {
            Response.Redirect("../secure/MainMenu.aspx");
        }
        else
        {
            setupPage(DataProvider.getInstance().getSessionFromToken(getSessionKey()));
        }
    }

    protected bool isEmployee()
    {
        return false;
    }

    protected String getSessionKey()
    {
        Object sessionValue = Session[AppConst.SESSION_KEY];
        if (sessionValue != null)
        {
            return (String)sessionValue;
        }
        return null;
    }

    protected void refresh()
    {
        Response.Redirect(Page.Request.Url.ToString(), true);
    }

    protected abstract bool isSecurePage();

}