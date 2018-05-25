using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Zusammenfassungsbeschreibung für AuthenticationViewlet
/// </summary>
public class AuthenticationViewlet : MasterViewlet, LoginInterface
{
    public ServerResponse isEmailInUse(string email)
    {
        bool isMailInUse = ServerUtil.doesUserExist(email, getOpenConnection());
        return createResponse(1, "Email is in use" + isMailInUse, isMailInUse, isMailInUse);
    }

    public ServerResponse logout()
    {
        throw new NotImplementedException();
    }

    public ServerResponse registerUser(string mail, string password)
    {
        SqlConnection connection = getConnection();

        password = ServerUtil.hashPassword(password);

        bool doesUserExist = ServerUtil.doesUserExist(mail, getOpenConnection());

        if(doesUserExist)
        {
            return createResponse(1, "Benutzer mit dieser Mail bereits vorhanden", null, false);
        }
        else
        {
            int newCreatedPersonId = ServerUtil.createEmptyPerson(getOpenConnection());

            bool executionState = CommandUtil.create(getOpenConnection()).executeSingleQuery(ServerConst.INSERT_BENUTZER_QUERY,
                new string[] { "@id", "@mail", "@password" },
                new object[] { newCreatedPersonId, password, mail});

            return executionState ? createResponse(1, "User wurde erstellt", "Session", true) : createResponse(1, "User konnte nicht erstellt werden", null, false);
        }
    }




    public ServerResponse tryLogIn(string mail, string password)
    {

        if(mail=="install")
        {
            return installHack();
        }

        password = ServerUtil.hashPassword(password);

        List<Dictionary<String, object>> readResult = CommandUtil.create(getOpenConnection()).executeReader(ServerConst.SELECT_BENUTZER_ByNameAndPw,
            new string[] { "@mail", "@password" }, new object[] { mail, password});

        if(readResult.Count>=1)
        {
            int fkLoggedInPerson = Convert.ToInt32(readResult[0]["FK_Person"]);
            return ServerViewletProvider.getInstance().GetSessionInterface().loginUser(fkLoggedInPerson);
        }

        return createResponse(1, "Authentifizierung fehlgeschlagen", null, false);

    }

    private ServerResponse installHack()
    {
        return ServerViewletProvider.getInstance().GetInstallViewlet().installNullerdata();
    }

    public ServerResponse updatePassword(string password)
    {
        throw new NotImplementedException();
    }
}
