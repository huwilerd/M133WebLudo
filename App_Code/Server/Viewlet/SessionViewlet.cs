using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für SessionViewlet
/// </summary>
public class SessionViewlet : MasterViewlet, SessionInterface
{

    public ServerResponse loginUser(int fkPerson)
    {
        List<Dictionary<String, Object>> sessionResult = CommandUtil.create(getOpenConnection()).executeReader("SELECT * FROM Session WHERE FK_Person=@fkPerson",
            new string[] { "@fkPerson" }, new object[] { fkPerson });
        if(sessionResult.Count == 0)
        {
           return createResponse(1, "Session erfolgreich erstellt", createNewSession(fkPerson), true);
        } else
        {
            Session currentSession = ConvertUtil.getSession(sessionResult[0]);
            return createResponse(1, "Session erfolgreich verlängert",  updateSessionActivity(currentSession), true);
        }
    }

    public ServerResponse getSessionByToken(string sessionId)
    {
        if (sessionId != null)
        {
            Session sessionFromId = ServerUtil.getSessionFromId(sessionId, getOpenConnection());
            if (sessionFromId != null)
            {
                return createResponse(1, "Session erfolgreich gefunden", sessionFromId, true);
                
            }
        }
        return createResponse(1, "Keine Session für diese Id gefunden", null, false);
    }

    public ServerResponse hasToFillInInformation(Session session)
    {
        Person person = ServerUtil.getPersonFromId(session.FK_Person, getOpenConnection());
        if(person==null)
        {
            return createResponse(1, "Keine Person gefunden", /*Muss Informationen ausfüllen?*/false, false);
        }
        bool fillInFromUserRequired = String.IsNullOrEmpty(person.Name);
        return createResponse(1, "Ob User Informationen ausfüllen muss: "+fillInFromUserRequired, fillInFromUserRequired, true);
    }

    private Session createNewSession(int fkPerson)
    {
        Session newSession = new Session(generateSessionId(), fkPerson, true, DateTime.Now);

        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery(ServerConst.INSERT_SESSION_QUERY, 
            new string[] { "@sessionID", "@FkPerson", "@activeSession", "@lastActivity" },
            new object[] { newSession.sessionID, newSession.FK_Person, newSession.activeSession, newSession.lastActivity});

        if(executionState)
        {
            return newSession;
        }

        throw new Exception("Neue Session konnte nicht angelegt werden.");
    }

    private Session updateSessionActivity(Session session)
    {
        session.lastActivity = DateTime.Now;
        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Session SET lastActivity=@lastActivity, activeSession=@activeSession WHERE sessionID=@SessionID",
            new string[] {"@SessionID", "@lastActivity", "@activeSession" },
            new object[] { session.sessionID,session.lastActivity, true/*falls false wieder auf true!*/});
        if(!executionState)
        {
            throw new Exception("Session-LastAktivität konnte nicht gesetzt werden.");
        }
        return session;
    }

    private String generateSessionId()
    {
        return Guid.NewGuid().ToString();
    }

    public ServerResponse getPersonFromSession(Session session)
    {
        Person person = ServerUtil.getPersonFromId(session.FK_Person, getOpenConnection());
        return person != null ? createResponse(1, "Person gefunden", person, true) : createResponse(1, "Person nicht gefunden", null, false);
    }

    public ServerResponse destroySession(Session session)
    {
        bool destroyState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Session SET activeSession=@activeSession WHERE sessionID=@sessionID",
            new string[] { "@activeSession", "@sessionID" }, new object[] { false, session.sessionID });
        
        return destroyState ? createResponse(1, "Session erfolgreich zerstört", null, true) : createResponse(1, "Session konnte nicht zerstört werden", null, false);
    }
}