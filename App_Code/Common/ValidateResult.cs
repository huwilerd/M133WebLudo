using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ValidateResult
/// </summary>
public class ValidateResult
{
    public String validateMessage;
    public bool validateStatus;

    public ValidateResult(string validateMessage, bool validateStatus)
    {
        this.validateMessage = validateMessage;
        this.validateStatus = validateStatus;
    }
}