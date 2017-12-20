using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Speichert Daten in Files ab
/// </summary>
public class DataHandler
{

    private static DataHandler instance;

    public static DataHandler getInstance()
    {
        if(instance==null)
        {
            instance = new DataHandler();
        }
        return instance;
    }
    
    private DataHandler()
    {
    }

    public User doesUserExist(User user)
    {
        List<User> users = DataProvider.getInstance().getAllUser();
        foreach (User tmpUser in users)
        {
            if (tmpUser.email == user.email)
            {
                return tmpUser;
            }
        }
        return null;
    }

    public bool checkUserLogin(User user)
    {
        List<User> users = DataProvider.getInstance().getAllUser();
        foreach(User tmpuser in users)
        {
            if(tmpuser.userId == user.userId)
            {
                Console.WriteLine("Username: " + tmpuser.email + "==" + user.email + " PASSWORT: " + tmpuser.password + "==" + user.email);
                return tmpuser.email == user.email && tmpuser.password == user.password;
            }
        }
        return false;
    }

    public User registerNewUser(User newUser)
    {
        List<User> users = DataProvider.getInstance().getAllUser();
        if (users == null)
        {
            users = new List<User>();
        }

        int userId = users.Count + 1;
        newUser.userId = userId;

        users.Add(newUser);

        String newJson = DataHelper.createJsonFromObject(users);
        DataHelper.saveDataToFile(AppConst.USER_FILE, newJson, true);
        return newUser;
    }

    public User updateUser(User updatedUser)
    {
        List<User> users = DataProvider.getInstance().getAllUser();
        foreach(User user in users)
        {
            if(user.userId == updatedUser.userId)
            {
                users.Remove(user);
                break;
            }
        }
        users.Add(updatedUser);

        String newJson = DataHelper.createJsonFromObject(users);
        DataHelper.saveDataToFile(AppConst.USER_FILE, newJson, true);
        return updatedUser;
    }

    public UserDetail getDetailOfUser(User user)
    {
        if (user != null)
        {
            List<UserDetail> details = DataProvider.getInstance().getAllUserDetail();
            foreach (UserDetail detail in details)
            {
                if (detail.userId == user.userId)
                {
                    return detail;
                }
            }
        }
        return null;
    }

    public Hire saveExpandedHire(Hire expandedHire)
    {
        List<Hire> hires = DataProvider.getInstance().getAllHires();
        foreach(Hire hire in hires)
        {
            if(hire.hireId == expandedHire.hireId)
            {
                hires.Remove(hire);
                break;
            }
        }
        expandedHire.expandCount += 1;
        hires.Add(expandedHire);
        Console.WriteLine(expandedHire.toDate);

        String json = DataHelper.createJsonFromObject(hires);
        DataHelper.saveDataToFile(AppConst.HIRE_FILE, json, true);
        return expandedHire;
    }

    public bool canExpandHire(Hire hire)
    {
        return hire.expandCount + 1 < AppConst.HIRE_MAX_AMOUNT_OF_REHIRES;
    }

    public Hire saveHireOfUser(Hire hire)
    {
        List<Hire> hires = DataProvider.getInstance().getAllHires();
        int hireId = hires.Count + 1;
        hire.hireId = hireId;
        hires.Add(hire);
        hires.Insert(0, hire);

        String newJson = DataHelper.createJsonFromObject(hires);
        DataHelper.saveDataToFile(AppConst.HIRE_FILE, newJson, true);

        //Decrease game count
        List<Game> games = DataProvider.getInstance().getAllGame();

        foreach(Game tmpGame in games)
        {
            if(tmpGame.gameId == hire.gameId)
            {
                tmpGame.anzahl -= 1;
                break;
            }
        }
        String gameJson = DataHelper.createJsonFromObject(games);
        DataHelper.saveDataToFile(AppConst.GAME_FILE, gameJson, true);

        return hire;
    }

    public void saveDetailOfUser(UserDetail detail)
    {
        List<UserDetail> details = DataProvider.getInstance().getAllUserDetail();
        foreach(UserDetail tmpDetail in details)
        {
            if(tmpDetail.userId == detail.userId)
            {
                details.Remove(tmpDetail);
                break;
            }
        }
        details.Add(detail);
        String newJson = DataHelper.createJsonFromObject(details);
        DataHelper.saveDataToFile(AppConst.DETAIL_FILE, newJson, true);
    }

    public bool hasTokenActiveSession(String token)
    {
        List<Session> sessions = DataProvider.getInstance().getAllSession();
        foreach (Session session in sessions)
        {
            if (session.token == token)
            {
                return session.status;
            }
        }
        return false;
    }

    /**
     * status: Ob Nutzer eingeloggt ist 
     **/
    public Session handleSessionForUser(User user, bool status)
    {
        List<Session> sessions = DataProvider.getInstance().getAllSession();
        Session usersSession = null;
        foreach(Session session in sessions)
        {
            if(session.userId == user.userId)
            {
                session.status = status;
                usersSession = session;
                break;
            }
        }

        if(usersSession == null)
        {
            String token = DateTime.Now.ToString().GetHashCode().ToString("x");
            Session newSession = new Session(token, user.userId, status);
            sessions.Add(newSession);
            usersSession = newSession;
        }

        String newJson = DataHelper.createJsonFromObject(sessions);
        DataHelper.saveDataToFile(AppConst.SESSION_FILE, newJson, true);
        return usersSession;
    }

}