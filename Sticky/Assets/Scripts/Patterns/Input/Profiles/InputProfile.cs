public abstract class InputProfile
{
    protected string inputProfileName;
    public string profileName { get { return inputProfileName; } }

    public InputProfile(string _profileName) { inputProfileName = _profileName; }
}
