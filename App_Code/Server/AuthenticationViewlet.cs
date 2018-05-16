using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für AuthenticationViewlet
/// </summary>
public class AuthenticationViewlet : MasterViewlet, LoginInterface
{
    public AuthenticationViewlet()
    {
        //
        //TODO: Hier Konstruktorlogik hinzufügen
        //
    }

    public void logout()
    {
        throw new NotImplementedException();
    }

    public void registerUser(string username, string password)
    {
        throw new NotImplementedException();
    }

    public bool tryLogIn(string username, string password)
    {
        throw new NotImplementedException();
    }

    public void updatePassword(string password)
    {
        throw new NotImplementedException();
    }
}