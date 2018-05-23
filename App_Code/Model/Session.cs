using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Session
/// </summary>
public class Session
{

    public readonly String sessionID;
    public readonly int FK_Person;
    public bool activeSession; //is user logged in
    public DateTime lastActivity;

    public Session(string sessionID, int fK_Person, bool activeSession, DateTime lastActivity)
    {
        this.sessionID = sessionID;
        FK_Person = fK_Person;
        this.activeSession = activeSession;
        this.lastActivity = lastActivity;
    }
}