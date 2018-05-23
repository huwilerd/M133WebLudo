using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ServerUtil
/// </summary>
public class ServerUtil
{
    public ServerUtil()
    {
        //
        //TODO: Hier Konstruktorlogik hinzufügen
        //
    }

    public static String hashPassword(String password)
    {
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }


}
