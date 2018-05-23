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

    
}