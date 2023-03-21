namespace Banks.Client;

public interface IClientInfo
{
    Guid Id { get; }
    string Name { get; }
    string SecondName { get; }
    string? Address { get; }
    string? PassportNumber { get; }
    void UpdateName(string name);
    void UpdateSecondName(string secondName);
    void UpdateAddress(string address);
    void UpdatePassportNumber(string passportNumber);
}