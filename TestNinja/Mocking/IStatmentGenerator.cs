using System;

namespace TestNinja.Mocking
{
    public interface IStatmentGenerator
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }
}