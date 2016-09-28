public enum InputMapping
{
    LEFT_STICK_X = 0,
    LEFT_STICK_Y,
    LEFT_STICK_BUTTON,
    RIGHT_STICK_X,
    RIGHT_STICK_Y,
    RIGHT_STICK_BUTTON,
    DPAD_X,
    DPAD_LEFT,
    DPAD_RIGHT,
    DPAD_Y,
    DPAD_UP,
    DPAD_DOWN,
    ACTION1,
    ACTION2,
    ACTION3,
    ACTION4,
    LEFT_TRIGGER,
    LEFT_TRIGGER_BUTTON,
    RIGHT_TRIGGER,
    RIGHT_TRIGGER_BUTTON,
    LEFT_BUMPER,
    RIGHT_BUMPER,
    ADDITIONAL1,
    ADDITIONAL2,
    COUNT
}

public abstract class ControllerProfile : InputProfile
{
    protected ControlMaster[] controllerInputData;

    public ControllerProfile(string _profileName, int _controllerNum) : base(_profileName){}

    public ControlMaster GetData(InputMapping _input, int _controllerNum)
    {
        return controllerInputData[(int)_input];
    }

    public abstract ControllerProfile CreateControllerProfileInstance(int _controllerNum);
    public virtual void UpdateControllerInputData() { }
}
