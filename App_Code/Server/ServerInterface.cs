using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public interface LoginInterface
{
    ServerResponse tryLogIn(String username, String password);

    ServerResponse registerUser(String username, String password);

    ServerResponse updatePassword(String password);

    ServerResponse logout();

    ServerResponse isEmailInUse(String email);
}

public interface SessionInterface
{
    ServerResponse loginUser(int fkPerson);

    ServerResponse getSessionByToken(String sessionId);

    ServerResponse hasToFillInInformation(Session session);

    ServerResponse getPersonFromSession(Session session);

    ServerResponse destroySession(Session session);

    ServerResponse isSessionValid(Session session);
}

public interface ClientInterface
{
    ServerResponse getOwnOpenHires(Session session);

    ServerResponse getOwnHires(Session session);

    ServerResponse getAllGames();
}

public interface PersonFunctionInterface
{
    ServerResponse updatePerson(Person person);

    ServerResponse updateUser(User user);

    ServerResponse updateHire(Hire hire);

    ServerResponse createHire(Session session, Hire hire);
}

public interface EmployeeInterface
{
    ServerResponse getAllClients();

    ServerResponse getAllGames();

    ServerResponse getAllHires();

    ServerResponse addNewGame(Spiel newSpiel);

    ServerResponse removeGame(Spiel spiel);

    ServerResponse closeHire(Hire hire);

}

public interface AdminInterface
{
    ServerResponse getAllEmployees();
}

public interface HtmlInterface
{
    String getAllHires(Session session);

    String getAllClients(Session session);

    String getOwnHires(Session session);

    String getOwnOpenHires(Session session);

    String getDashboard(Session session);

    String getAllGames(Session session, bool manageView);
}