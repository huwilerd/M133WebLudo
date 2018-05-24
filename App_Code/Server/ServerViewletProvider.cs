using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ServerFactory
/// </summary>
public class ServerViewletProvider
{

    private static ServerViewletProvider instance;

    public static ServerViewletProvider getInstance()
    {
        if (instance == null)
        {
            instance = new ServerViewletProvider();
        }
        return instance;
    }

    private AuthenticationViewlet authenticationViewlet;
    private SessionViewlet sessionViewlet;
    private PersonViewlet personViewlet;

    private ServerViewletProvider()
    {
        
    }

    public AuthenticationViewlet getAuthenticationViewlet()
    {
        if(authenticationViewlet == null)
        {
            authenticationViewlet = new AuthenticationViewlet();
        }
        return authenticationViewlet;
    }

    public SessionInterface GetSessionInterface()
    {
        if(sessionViewlet == null)
        {
            sessionViewlet = new SessionViewlet();
        }
        return sessionViewlet;
    }

    public PersonViewlet GetPersonViewlet()
    {
        if(personViewlet == null)
        {
            personViewlet = new PersonViewlet();
        }
        return personViewlet;
    }


}