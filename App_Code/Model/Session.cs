using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Session
/// </summary>
public class Session
{

    public String token;
    public int userId;
    public bool status; //is user logged in

    public Session(String token, int userId, bool status)
    {
        this.token = token;
        this.userId = userId;
        this.status = status;
    }

}