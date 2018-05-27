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
    private EmployeeViewlet employeeViewlet;
    private HtmlViewlet htmlViewlet;

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

    public PersonFunctionInterface GetPersonViewlet()
    {
        if(personViewlet == null)
        {
            personViewlet = new PersonViewlet();
        }
        return personViewlet;
    }

    public EmployeeInterface GetEmployeeInterface(Session session)
    {
        if(employeeViewlet == null)
        {
            employeeViewlet = new EmployeeViewlet();
        }
        if(session.sessionRole.Equals(SessionRole.Client))
        {
            throw new Exception("Es ist Kunden nicht erlaubt auf Mitarbeiteraktivitäten zuzugreifen!");
        }
        return employeeViewlet;
    }

    public HtmlInterface GetHtmlViewlet()
    {
        if(htmlViewlet == null)
        {
            htmlViewlet = new HtmlViewlet();
        }
        return htmlViewlet;
    }

    public InstallViewlet GetInstallViewlet()
    {
        return new InstallViewlet();
    }


}