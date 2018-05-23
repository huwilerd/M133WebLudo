using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für SessionHandler
/// </summary>
public class SessionHandler
{

    private static SessionHandler instance;

    public static SessionHandler getInstance()
    {
        if (instance == null)
        {
            instance = new SessionHandler();
        }
        return instance;
    }

    private SessionHandler()
    {

    }

    public Session tryLoginUser(String email, String password)
    {
        User newUser = new User(-1, email, password);
        User existingUser = DataHandler.getInstance().doesUserExist(newUser);
        if (existingUser != null)
        {
            existingUser.email = email;
            existingUser.password = password;
            if (DataHandler.getInstance().checkUserLogin(existingUser))
            {
                return DataHandler.getInstance().handleSessionForUser(existingUser, true);
            }
        }
        //return new Session("-1", -1, false);
        return null;
    }

    public Session logoutUser(String token)
    {
        Session session = DataProvider.getInstance().getSessionFromToken(token);
        if(session != null)
        {
            /*User user = DataProvider.getInstance().getUserFromId(session.userId);
            return DataHandler.getInstance().handleSessionForUser(user, false);*/
        }
        //return new Session("-1", -1, false);
        return null;
    }
}