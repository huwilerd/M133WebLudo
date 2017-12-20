using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für UserHandlerInterface
/// </summary>
public interface UserProviderInterface
{
    bool doesUserExist(User user);

}