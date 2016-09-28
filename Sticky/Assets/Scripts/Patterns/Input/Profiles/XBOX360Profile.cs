public class XBOX360Profile : ControllerProfile
{
    public XBOX360Profile(int _controllerNum) : base("Controller (XBOX 360 For Windows)", _controllerNum)
    {
        controllerInputData = new ControlMaster[(int)InputMapping.COUNT];
        controllerInputData[(int)InputMapping.LEFT_STICK_X] = new ControlAxis("XAxis", _controllerNum);
        controllerInputData[(int)InputMapping.LEFT_STICK_Y] = new ControlAxis("YAxis", _controllerNum);
        controllerInputData[(int)InputMapping.LEFT_STICK_BUTTON] = new ControlButton("8Button", _controllerNum);
        controllerInputData[(int)InputMapping.RIGHT_STICK_X] = new ControlAxis("4Axis", _controllerNum);
        controllerInputData[(int)InputMapping.RIGHT_STICK_Y] = new ControlAxis("5Axis", _controllerNum);
        controllerInputData[(int)InputMapping.RIGHT_STICK_BUTTON] = new ControlButton("9Button", _controllerNum);
        controllerInputData[(int)InputMapping.DPAD_X] = new ControlAxis("6Axis", _controllerNum);
        controllerInputData[(int)InputMapping.DPAD_LEFT] = new ControlButtonFromAxis("6Axis", _controllerNum, false);
        controllerInputData[(int)InputMapping.DPAD_RIGHT] = new ControlButtonFromAxis("6Axis", _controllerNum, true);
        controllerInputData[(int)InputMapping.DPAD_Y] = new ControlAxis("7Axis", _controllerNum);
        controllerInputData[(int)InputMapping.DPAD_UP] = new ControlButtonFromAxis("7Axis", _controllerNum, true);
        controllerInputData[(int)InputMapping.DPAD_DOWN] = new ControlButtonFromAxis("7Axis", _controllerNum, false);
        controllerInputData[(int)InputMapping.ACTION1] = new ControlButton("0Button", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION2] = new ControlButton("1Button", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION3] = new ControlButton("2Button", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION4] = new ControlButton("3Button", _controllerNum);
        controllerInputData[(int)InputMapping.LEFT_TRIGGER] = new ControlAxis("9Axis", _controllerNum);
        controllerInputData[(int)InputMapping.LEFT_TRIGGER_BUTTON] = new ControlButtonFromAxis("9Axis", _controllerNum, true);
        controllerInputData[(int)InputMapping.RIGHT_TRIGGER] = new ControlAxis("10Axis", _controllerNum);
        controllerInputData[(int)InputMapping.RIGHT_TRIGGER_BUTTON] = new ControlButtonFromAxis("10Axis", _controllerNum, true);
        controllerInputData[(int)InputMapping.LEFT_BUMPER] = new ControlButton("4Button", _controllerNum);
        controllerInputData[(int)InputMapping.RIGHT_BUMPER] = new ControlButton("5Button", _controllerNum);
        controllerInputData[(int)InputMapping.ADDITIONAL1] = new ControlButton("6Button", _controllerNum);
        controllerInputData[(int)InputMapping.ADDITIONAL2] = new ControlButton("7Button", _controllerNum);
    }

    public override ControllerProfile CreateControllerProfileInstance(int _controllerNum)
    {
        return new XBOX360Profile(_controllerNum);
    }

    public override void UpdateControllerInputData()
    {
        controllerInputData[(int)InputMapping.DPAD_LEFT].UpdateControlInput();
        controllerInputData[(int)InputMapping.DPAD_RIGHT].UpdateControlInput();
        controllerInputData[(int)InputMapping.DPAD_UP].UpdateControlInput();
        controllerInputData[(int)InputMapping.DPAD_DOWN].UpdateControlInput();
        controllerInputData[(int)InputMapping.LEFT_TRIGGER_BUTTON].UpdateControlInput();
        controllerInputData[(int)InputMapping.RIGHT_TRIGGER_BUTTON].UpdateControlInput();
    }
}
