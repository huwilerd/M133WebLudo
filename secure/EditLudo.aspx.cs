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
        handleParameter();
    }

    protected override void handlePostback()
    {
        handleSave();
    }

    private void handleSave()
    {
        int ludoId = getIntFromParameter("id");
        String newLudoName = getValueFromForm("LudoNameField");
        String newLudoStrasse = getValueFromForm("LudoStrasseField");
        int newLudoPlz = Convert.ToInt32(getValueFromForm("LudoPostleitzahlField"));
        String newLudoOrt = getValueFromForm("LudoOrtField");

        Ludothek updatedLudothek = new Ludothek(ludoId, newLudoName, newLudoStrasse, newLudoPlz, newLudoOrt, InstallViewlet.verband.ID_Verband);
        //todo validate

         if (ludoId != -1)
            {
                ServerResponse ludothekResponse = GetViewletProvider().GetAdminInterface(getCurrentSession()).updateLudothek(getCurrentSession(), updatedLudothek);
                if (ludothekResponse.getResponseStatus())
                {
                    leavePage();
                }
                else
                {
                    servererror.InnerText = "Es ist ein Problem aufgetreten: " + ludothekResponse.getResponseMessage();
                }
         }
    }

    private String getValueFromForm(String key)
    {
        return Request.Form[key]; 
    }

    private void handleParameter()
    {

        if (getCurrentSession().sessionRole.Equals(SessionRole.Administrator))
        {

            int ludothekID = getIntFromParameter("id");

            if (ludothekID > 0)
            {
                ServerResponse ludoResponse = GetViewletProvider().GetAdminInterface(getCurrentSession()).getSingleLudothek(ludothekID);
                if (ludoResponse.getResponseStatus())
                {
                    Ludothek ludothek = (Ludothek)ludoResponse.getResponseObject();
                    LudoNameField.Text = ludothek.name;
                    LudoStrasseField.Text = ludothek.strasse;
                    LudoPostleitzahlField.Text = Convert.ToString(ludothek.postleitzahl);
                    LudoOrtField.Text = ludothek.ort;
                    return;
                }
            }
        }

        leavePage();
    }

    private void leavePage()
    {
        Response.Redirect("../secure/MainMenu.aspx?page=8");
    }

}