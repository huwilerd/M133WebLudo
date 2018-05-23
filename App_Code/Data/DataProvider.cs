using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Holt Daten von Files
/// </summary>
public class DataProvider
{

    private static DataProvider instance;

    private DataProvider()
    {

    }

    public static DataProvider getInstance()
    {
        if (instance == null)
        {
            instance = new DataProvider();
        }
        return instance;
    }

    public List<Session> getAllSession()
    {
        DataHelper.createFileAndDirectory(AppConst.SESSION_FILE);
        String sessionJson = DataHelper.getContentOfFile(AppConst.SESSION_FILE);
        List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(sessionJson);
        if(sessions == null)
        {
            sessions = new List<Session>();
        }
        return sessions;
    }

    public List<User> getAllUser()
    {
        DataHelper.createFileAndDirectory(AppConst.USER_FILE);
        String userJson = DataHelper.getContentOfFile(AppConst.USER_FILE);
        List<User> users = JsonConvert.DeserializeObject<List<User>>(userJson);
        if(users == null)
        {
            users = new List<User>();
        }
        return users;
    }

    public User getUserFromId(int userId)
    {
        List<User> users = getAllUser();
        foreach(User user in users)
        {
            if(user.userId == userId)
            {
                return user;
            }
        }
        return null;
    }

    public Session getSessionFromToken(String token)
    {
        if (token != null)
        {
            List<Session> sessions = getAllSession();
            foreach (Session session in sessions)
            {
               /* if (session.token == token)
                {
                    return session;
                }*/
            }
        }
        return null;
    }

    public User getUserFromToken(String token)
    {
        Session session = getSessionFromToken(token);
        if(session != null)
        {
            /*User user = getUserFromId(session.userId);
            return user;*/
        }
        return null;
    }

    public List<UserDetail> getAllUserDetail()
    {
        DataHelper.createFileAndDirectory(AppConst.DETAIL_FILE);
        String detailJson = DataHelper.getContentOfFile(AppConst.DETAIL_FILE);
        List<UserDetail> details = JsonConvert.DeserializeObject<List<UserDetail>>(detailJson);
        if(details == null)
        {
            details = new List<UserDetail>();
        }
        return details;
    }

    public UserDetail getUserDetail(int userId)
    {
        List<UserDetail> details = getAllUserDetail();
        foreach(UserDetail detail in details)
        {
            if(detail.userId == userId)
            {
                return detail;
            }
        }
        return null;
    }

    public List<Hire> getAllClosedHiresOfClient(int userId)
    {
        List<Hire> hires = getAllHiresOfClient(userId);
        List<Hire> closedList = new List<Hire>();
        foreach(Hire hire in hires)
        {
            if (hire.closed)
            {
                closedList.Add(hire);
            }
        }
        return closedList;
    }

    public List<Hire> getAllOpenHiresOfClient(int userId)
    {
        List<Hire> hires = getAllHiresOfClient(userId);
        List<Hire> openList = new List<Hire>();
        foreach (Hire hire in hires)
        {
            if (!hire.closed)
            {
                openList.Add(hire);
            }
        }
        return openList;
    }

    public List<Hire> getAllHiresOfClient(int userId)
    {
        List<Hire> hires = getAllHires();
        List<Hire> clientList = new List<Hire>();
        foreach(Hire hire in hires)
        {
            if(hire.userId == userId)
            {
                clientList.Add(hire);
            }
        }
        return clientList;
    }

    public List<Hire> getAllHires()
    {
        DataHelper.createFileAndDirectory(AppConst.HIRE_FILE);
        String hireJson = DataHelper.getContentOfFile(AppConst.HIRE_FILE);
        List<Hire> hires = JsonConvert.DeserializeObject<List<Hire>>(hireJson);
        if(hires == null)
        {
            hires = new List<Hire>();
        }
        return hires;
    }

    public Hire getHireOfId(int hireId)
    {
        List<Hire> hires = getAllHires();
        foreach(Hire hire in hires)
        {
            if(hire.hireId == hireId)
            {
                return hire;
            }
        }
        return null;
    }

    public Game getGameFromId(int gameId)
    {
        List<Game> games = getAllGame();
        foreach(Game game in games)
        {
            if(game.gameId == gameId)
            {
                return game;
            }
        }
        return null;
    }
   
    public List<Game> getAllGame()
    {
        DataHelper.createFileAndDirectory(AppConst.GAME_FILE);
        String gameJson = DataHelper.getContentOfFile(AppConst.GAME_FILE);
        List<Game> games = JsonConvert.DeserializeObject<List<Game>>(gameJson);
        if(games == null)
        {
            games = new List<Game>();

            //default data
            Game tmpOne = new Game(1, "Call of Duty", "Call of Duty ist eine Computerspielreihe des amerikanischen Publishers Activision aus dem Genre der Ego-Shooter. Der Spieler übernimmt darin üblicherweise die Rolle eines Soldaten in einem Kriegsszenario.", "http://1images.cgames.de/images/gamestar/207/call-of-duty-4-modern-warfare_1821710.jpg", 1);
            Game tmpTwo = new Game(2, "Monopoly", "Ein Spiel für die Ganze Familie!", "data:image/jpeg;base64,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", 1);
            Game tmpThree = new Game(3, "Fifa 18", "FIFA 18 bietet euch unterschiedliche Passarten, mit denen ihr euch erfolgreich nach vorne spielt. Neben einem normalen Pass könnt ihr auch einen hohen oder flachen Pass in den Lauf spielen, um schnell anzugreifen. ", "https://www.instant-gaming.com/images/products/2064/271x377/2064.jpg", 1);
            Game tmpFour = new Game(4, "Grand Theft Auto 5", "Grand Theft Auto V ist ein Computerspiel, das vom schottischen Studio Rockstar North entwickelt wurde. Die weltweite Veröffentlichung durch Rockstar Games für PlayStation 3 und Xbox 360 fand am 17. September 2013 statt. Es ist der insgesamt 11.", "https://www.rockstargames.com/V/img/global/order/mobile-cover.jpg", 1);
            Game tmpFive = new Game(5, "Mensch ärgere dich nicht", "Mensch ärgere Dich nicht ist ein Gesellschaftsspiel für zwei bis sechs Personen. Es zählt zu den Klassikern unter den deutschen Brettspielen und ist ein Abkömmling des indischen Spiels Pachisi. ", "http://www.toysrus.ch/graphics/product_images/pTRUDE1-7432139enh-z6.jpg", 1);
            Game tmpSix = new Game(6, "Schach", "Schach ist ein strategisches Brettspiel, bei dem zwei Spieler abwechselnd Spielsteine auf einem Spielbrett bewegen. Ziel des Spiels ist, den Gegner schachmatt zu setzen, das heißt seine als König bezeichnete Spielfigur unabwendbar anzugreifen.", "https://www.gadgetsforfun.de/image/cache/data/schach-500x500.jpg", 1);
            games.Add(tmpOne);
            games.Add(tmpTwo);
            games.Add(tmpThree);
            games.Add(tmpFour);
            games.Add(tmpFive);
            games.Add(tmpSix);
            String json = DataHelper.createJsonFromObject(games);
            DataHelper.saveDataToFile(AppConst.GAME_FILE, json, true);
        }
        return games;
    }

    






}