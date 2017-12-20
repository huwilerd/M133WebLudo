using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für User
/// </summary>
public class User
{
    public int userId;
    public String email;
    public String password;

    public User(int userId, String email, String password)
    {
        this.userId = userId;
        this.email = email;
        this.password = password;
    }

}