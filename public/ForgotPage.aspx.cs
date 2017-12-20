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

        User tmpUser = new User(-1, forgotEmail, "");
        User foundUser = DataHandler.getInstance().doesUserExist(tmpUser);
        if(foundUser != null)
        {
            servererror.InnerText = "";
            foundUser.password = newPasswort;
            DataHandler.getInstance().updateUser(foundUser);
            Response.Redirect("../public/LoginPage.aspx");
        }
        else
        {
            servererror.InnerText = "Es wurde kein Nutzer mit der angegeben E-Mail gefunden!";
        }
        

    }

    protected override bool isSecurePage()
    {
        return false;
    }
}