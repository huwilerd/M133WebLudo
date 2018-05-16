using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public interface LoginInterface
{
    bool tryLogIn(String username, String password);

    void registerUser(String username, String password);

    void updatePassword(String password);

    void logout();
}

public interface ClientInterface
{
    void getOwnOpenHires();

    void getOwnHires();

    void getAllGames();
}

public interface PersonFunctionInterface
{
    void updatePerson();

    void updateHire();

    void createHire();
}

public interface EmployeeInterface
{
    void getAllClients();

    void getAllGames();

    void getAllHires();

    void addNewGame();

    void removeGame();

    void closeHire();

}