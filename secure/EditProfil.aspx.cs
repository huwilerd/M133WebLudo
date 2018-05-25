using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : SecureMasterPage
{

    protected override void setupPageWithSession(Session session)
    {
        base.setupPageWithSession(session);
    }

    protected override void setupPage(Session session)
    {
        base.setupPage(session);
        if (session != null)
        {
            Person currentPerson = getCurrentPerson();

            if(currentPerson!=null)
            {
                AnredeField.Value = AnredeField.Items.FindByText(currentPerson.Geschlecht == "Männlich" ? "Herr" : "Frau").Value;
                NameField.Text = currentPerson.Name;
                GeburtsdatumField.Text = currentPerson.Geburtsdatum.ToString("yyyy-MM-dd");
                StrasseField.Text = currentPerson.strasse;
                PLZField.Text = Convert.ToString(currentPerson.postleitzahl);
                OrtField.Text = currentPerson.ort;
                LandField.Text = currentPerson.land;
                ServerResponse response = GetViewletProvider().GetSessionInterface().hasToFillInInformation(getCurrentSession());
                bool canShowAccountFields = !(response.getResponseStatus() && ((bool)response.getResponseObject()));
                showAccountFields(canShowAccountFields);
            }

            Session currentSession = getCurrentSession();

            User currentUser = currentSession.user;
            if(currentUser!=null)
            {
                EmailField.Text = currentUser.email;
            }

        } else {
            Response.Redirect("../public/LoginPage.aspx");
        }

        handleParameter();
    }

    private void handleParameter()
    {
        String actionParam = getStringFromParameter("action");
        if(actionParam!=null && actionParam == "deleteacc")
        {
            handleAccountDelete();
        }
    }

    private void showAccountFields(bool show)
    {
        EmailFieldLabel.Visible = show;
        PasswortFieldLabel.Visible = show;
        EmailField.Visible = show;
        PasswortField.Visible = show;
        deleteLink.Visible = show;
        accountLabel.Visible = show;

        bool showStellvertretungFields = getCurrentSession().sessionRole.Equals(SessionRole.Administrator);
        stellvertretung.Visible = showStellvertretungFields;
        stellvertretungLabel.Visible = showStellvertretungFields;
        if (showStellvertretungFields)
        {
            ServerResponse response = GetViewletProvider().GetAdminInterface(getCurrentSession()).getAllEmployees();
            if (response.getResponseStatus())
            {
                Person filialleiter = getCurrentPerson();
                Person stellvertreterPerson = null;
                ServerResponse stellvertretungResp = GetViewletProvider().GetAdminInterface(getCurrentSession()).getStellvertretung(filialleiter.ID_Person);
                if (!stellvertretungResp.getResponseStatus())
                {
                    servererror.InnerText = stellvertretungResp.getResponseMessage();
                    return;
                } else
                {
                    stellvertreterPerson = (Person)stellvertretungResp.getResponseObject();
                }
                if(stellvertreterPerson!=null)
                {
                    List<Person> mitarbeiterList = (List<Person>)response.getResponseObject();
                    mitarbeiterList.ForEach(delegate (Person ma)
                    {
                        stellvertretung.Items.Add(new ListItem(ma.Name, Convert.ToString(ma.ID_Person), true));
                    });

                    stellvertretung.Value = stellvertretung.Items.FindByValue(Convert.ToString(stellvertreterPerson.ID_Person)).Value;
                }
            }
            else
            {
                servererror.InnerText = response.getResponseMessage();
            }
        }
    }

    protected override void handlePostback()
    {
        handleSave();
    }

    private void handleSave()
    {
        bool hasError = false;

        Session session = getCurrentSession();
        Person person = getCurrentPerson();

        String anrede = Request.Form["AnredeField"];
        String name = NameField.Text;
        String geburtsdatum = GeburtsdatumField.Text;
        String strasse = StrasseField.Text;
        String plz = PLZField.Text;
        String ort = OrtField.Text;
        String land = LandField.Text;

        person.Geschlecht = anrede;
        person.Name = name;
        person.Geburtsdatum = Convert.ToDateTime(geburtsdatum);
        person.strasse = strasse;

        ValidateResult plzResult = ValidateUtil.getInstance().validatePostleitzahl(plz);
        if (!plzResult.validateStatus)
        {
            servererror.InnerText = plzResult.validateMessage;
            hasError = true;
        }
        else
        {
            person.postleitzahl = Convert.ToInt32(plz);
        }
        person.ort = ort;
        person.land = land;
       
        if (PasswortField.Visible && EmailField.Visible)
        {
            User currentLoggedInUser = session.user;

            String originalEmail = currentLoggedInUser.email;
            String inputMail = EmailField.Text;

            bool userHasChanges = false;
            if (!originalEmail.Equals(inputMail))
            {

                ServerResponse emailUseResponse = GetViewletProvider().getAuthenticationViewlet().isEmailInUse(inputMail);

                if(!emailUseResponse.getResponseStatus())
                {
                    currentLoggedInUser.email = inputMail;
                    userHasChanges = true;
                }
                else
                {
                    servererror.InnerText = "Nutzer mit Mail besteht bereits!";
                    hasError = true;
                }
            }

            if (PasswortField.Text != "")
            {
                currentLoggedInUser.password = ServerUtil.hashPassword(PasswortField.Text);
                userHasChanges = true;
            }

            if (userHasChanges)
            {
                if (!hasError)
                {
                    ServerResponse updateUserResp = GetViewletProvider().GetPersonViewlet().updateUser(currentLoggedInUser);
                    if(!updateUserResp.getResponseStatus())
                    {
                        servererror.InnerText = updateUserResp.getResponseMessage();
                        hasError = true;
                    }
                }
            }
        }

        if(stellvertretung.Visible)
        {
            Person currentFilialleiter = getCurrentPerson();
            int newFilialleiterId = Convert.ToInt32(Request.Form["stellvertretung"]);
            ServerResponse updateResp = GetViewletProvider().GetAdminInterface(getCurrentSession()).updateStellvertretung(currentFilialleiter.ID_Person, newFilialleiterId);
            if(!updateResp.getResponseStatus())
            {
                servererror.InnerText = updateResp.getResponseMessage();
                hasError = true;
            }
        }

        if (!hasError)
        {
            ServerResponse serverResponse = GetViewletProvider().GetPersonViewlet().updatePerson(person);
            if (serverResponse.getResponseStatus())
            {
                Response.Redirect("../secure/MainMenu.aspx?page=1");
            } else
            {
                servererror.InnerText = serverResponse.getResponseMessage();
            }
        }
    }

    private void handleAccountDelete()
    {
        ServerResponse response = ((ClientInterface)GetViewletProvider().GetPersonViewlet()).deleteAccount(getCurrentSession());
        if (response.getResponseStatus())
        {
            refresh();
        } else
        {
            servererror.InnerText = response.getResponseMessage();
        }
    }

    protected override string getPageName()
    {
        return AppConst.DETAIL_FORM_PAGE_NAME;
    }

}