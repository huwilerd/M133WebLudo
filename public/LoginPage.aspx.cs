using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPageController : MasterPage
{

    protected override void handlePostback()
    {
        handleLogin();
    }

    private void handleLogin()
	{
		String username = EmailField.Text;
		String password = PasswordField.Text;
        Session session = SessionHandler.getInstance().tryLoginUser(username, password);
        if (session.status)
		{
            Session[AppConst.SESSION_KEY] = session.token;
            servererror.InnerText = "";
		} else
        {
            servererror.InnerText = "Der Nutzername oder das entsprechende Passwort ist inkorrekt.";
        }

	}

    protected override bool isSecurePage()
    {
        return false;
    }


}