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
}

public interface ClientInterface
{
    ServerResponse getOwnOpenHires();

    ServerResponse getOwnHires();

    ServerResponse getAllGames();
}

public interface PersonFunctionInterface
{
    ServerResponse updatePerson();

    ServerResponse updateHire();

    ServerResponse createHire();
}

public interface EmployeeInterface
{
    ServerResponse getAllClients();

    ServerResponse getAllGames();

    ServerResponse getAllHires();

    ServerResponse addNewGame();

    ServerResponse removeGame();

    ServerResponse closeHire();

}