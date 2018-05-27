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
    }

    private void showAccountFields(bool show)
    {
        EmailFieldLabel.Visible = show;
        PasswortFieldLabel.Visible = show;
        EmailField.Visible = show;
        PasswortField.Visible = show;
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
        person.postleitzahl = Convert.ToInt32(plz);
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
                    GetViewletProvider().GetPersonViewlet().updateUser(currentLoggedInUser);
                }
            }
        }

        if (!hasError)
        {
            GetViewletProvider().GetPersonViewlet().updatePerson(person);
            Response.Redirect("../secure/MainMenu.aspx");
        }
    }

    protected override string getPageName()
    {
        return AppConst.DETAIL_FORM_PAGE_NAME;
    }

}