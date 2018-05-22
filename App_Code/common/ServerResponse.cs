using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für ServerResponse
/// </summary>
public class ServerResponse
{
    private int responseId;
    private String responseMessage;
    private Object responseObject;
    private bool responseStatus;

    public ServerResponse(int responseId, string responseMessage, object responseObject, bool responseStatus)
    {
        this.responseId = responseId;
        this.responseMessage = responseMessage;
        this.responseObject = responseObject;
        this.responseStatus = responseStatus;
    }

    public int getResponseId()
    {
        return responseId;
    }

    public String getResponseMessage()
    {
        return responseMessage;
    }

    public Object getResponseObject()
    {
        return responseObject;
    }

    public bool getResponseStatus()
    {
        return responseStatus;
    }
}