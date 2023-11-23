static class EmailParser 
{
    public static (string recipient, string domain, string topLevelDomain) ParseEmail(string emailAddress)
    {
        string[] AddressSplit = emailAddress.Trim().Split("@");
        if (AddressSplit.Length != 2) return (null, null, null)!;

        string recipient = AddressSplit[0];
        
        string[] domains = AddressSplit[1].Split(".");
        if (domains.Length != 2) return (null, null, null)!;

        string domain = AddressSplit[1];

        string topLevelDomain = domains[1];

        return (recipient, domain, topLevelDomain);
    }

    public static bool IsEmailValid(string emailAddress)
    {
        if (ParseEmail(emailAddress).recipient is null) return false;
        else return true;
    }
}