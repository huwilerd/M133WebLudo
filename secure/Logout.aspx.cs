using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class secure_Logout : SecureMasterPage
{

    protected override void setupPage(Session session)
    {
        GetViewletProvider().GetSessionInterface().destroySession(session);
        refresh();
    }
}