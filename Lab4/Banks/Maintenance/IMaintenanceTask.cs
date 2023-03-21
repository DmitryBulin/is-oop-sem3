using Banks.Account;

namespace Banks.Maintenance;

public interface IMaintenanceTask
{
    void Maintain(IAccount account, int daysPassed);
}