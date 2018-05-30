using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class public_ForgotPage : MasterPage
{

    protected override void handlePostback()
    {
        handleForgotPassword();
    }

    private void handleForgotPassword()
    {
        String forgotEmail = EmailField.Text;
        String newPasswort = PasswortField.Text;

        User tmpUser = new User(-1, forgotEmail, newPasswort);
        ServerResponse isEmailResp = GetViewletProvider().getAuthenticationViewlet().isEmailInUse(forgotEmail);

        if(isEmailResp.getResponseStatus())
        {
            ServerResponse userIsUpdated = GetViewletProvider().GetPersonViewlet().updateUserPassword(tmpUser);
            if(userIsUpdated.getResponseStatus())
            {
                Response.Redirect("LoginPage.aspx");
            } else
            {
                servererror.InnerText = userIsUpdated.getResponseMessage();
            }
        } else
        {
            servererror.InnerText = "E-Mail gehört zu keinem Benutzer.";
        }

    }

    protected override bool isSecurePage()
    {
        return false;
    }
}