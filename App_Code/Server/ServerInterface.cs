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

    Session updateSessionActivity(Session session);
}

public interface ClientInterface
{
    ServerResponse getOwnOpenHires(Session session);

    ServerResponse getOwnClosedHires(Session session);

    ServerResponse getOwnHires(Session session);

    ServerResponse getAllGames();

    ServerResponse getSingleGame(int gameId);

    ServerResponse getSingleHire(int hireId, Session session);

    ServerResponse deleteAccount(Session session);
}

public interface PersonFunctionInterface
{
    ServerResponse updatePerson(Person person);

    ServerResponse updateUser(User user);

    ServerResponse updateUserPassword(User user);

    ServerResponse updateHire(Hire hire);

    ServerResponse createHire(Session session, Hire hire);
}

public interface EmployeeInterface
{
    ServerResponse getAllClients();

    ServerResponse getAllGames();

    ServerResponse getAllHires();

    ServerResponse getAllUsers();

    ServerResponse addNewGame(Spiel newSpiel);

    ServerResponse updateGame(Spiel updatedSpiel);

    ServerResponse removeGame(int idSpiel);

    ServerResponse closeHire(int idHire);

    ServerResponse reopenHire(int idHire);

    ServerResponse removeHire(int idHire);

    ServerResponse logoutUser(int userId);

    ServerResponse deleteUser(int userId);

    ServerResponse createMitgliedsschaftForPerson(int personId);

}

public interface AdminInterface
{
    ServerResponse getAllEmployees();

    ServerResponse getAllLudotheken();

    ServerResponse makeEmployee(int fkPerson);

    ServerResponse removeEmployee(Session session, int fkPerson);

    ServerResponse updateLudothek(Session session, Ludothek ludothek);

    ServerResponse getSingleLudothek(int fkLudothek);
}

public interface HtmlInterface
{
    String getAllHires(Session session);

    String getAllClients(Session session);

    String getAllEmployees(Session session);

    String getOwnHires(Session session);

    String getOwnOpenHires(Session session);

    String getDashboard(Session session);

    String getAllGames(Session session, bool manageView);

    String getAllUsers(Session session);

    String getAllLudotheken(Session session);
}