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
        Session sessionFromId = ServerUtil.getSessionFromId(sessionId, getOpenConnection());
        if(sessionFromId==null)
        {
            return createResponse(1, "Keine Session für diese Id gefunden", null, false);
        }
        return createResponse(1, "Session erfolgreich gefunden", sessionFromId, true);
        
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
        bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery("UPDATE Session SET lastActivity=@lastActivity WHERE sessionID=@SessionID",
            new string[] {"@SessionID", "@lastActivity" },
            new object[] { session.sessionID,session.lastActivity});
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

    
}