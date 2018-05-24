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
        
        ServerResponse response = ServerViewletProvider.getInstance().getAuthenticationViewlet().tryLogIn(username, password);

        if(response.getResponseStatus())
        {
            Session currentSession = (Session) response.getResponseObject();
            Session[AppConst.SESSION_KEY] = currentSession.sessionID;
            servererror.InnerText = "";
        }
        servererror.InnerText = response.getResponseMessage();


	}

    protected override bool isSecurePage()
    {
        return false;
    }


}