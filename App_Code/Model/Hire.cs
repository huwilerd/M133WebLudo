using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Zusammenfassungsbeschreibung für Hire
/// </summary>
public class Hire
{
    public int hireId;
    public int userId;
    public int gameId;
    public DateTime fromDate;
    public DateTime toDate;
    public int expandCount;
    public bool closed;

    public Hire(int hireId, int userId, int gameId, DateTime fromDate, DateTime toDate, int expandCount, bool closed)
    {
        this.hireId = hireId;
        this.userId = userId;
        this.gameId = gameId;
        this.fromDate = fromDate;
        this.toDate = toDate;
        this.expandCount = expandCount;
        this.closed = closed;
    }
}