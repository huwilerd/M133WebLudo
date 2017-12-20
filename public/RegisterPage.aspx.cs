using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : MasterPage
{

    protected override void handlePostback()
    {
        handleRegister();
    }

    private void handleRegister()
    {
        String email = EmailField.Text;
        String password = PasswordField.Text;
        User newUser = new User(-1, email, password);
        if(DataHandler.getInstance().doesUserExist(newUser) == null)
        {
            servererror.InnerHtml = "";
            User createdUser = DataHandler.getInstance().registerNewUser(newUser);
            Session session = SessionHandler.getInstance().tryLoginUser(createdUser.email, createdUser.password);
            if(session!= null)
            {
                Session[AppConst.SESSION_KEY] = session.token;
            }
                Response.Redirect("../public/LoginPage.aspx");

        }
        else
        {
            servererror.InnerHtml = "Nutzer existiert bereits!";
        }
        
    }

    protected override bool isSecurePage()
    {
        return false;
    }
}