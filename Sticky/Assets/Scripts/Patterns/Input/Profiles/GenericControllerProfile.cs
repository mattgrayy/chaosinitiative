public class GenericControllerProfile : ControllerProfile
{
    public GenericControllerProfile(int _controllerNum) : base("Generic", _controllerNum)
    {
        controllerInputData = new ControlMaster[(int)InputMapping.COUNT];
        controllerInputData[(int)InputMapping.LEFT_STICK_X] = new ControlAxis("XAxis", _controllerNum);
        controllerInputData[(int)InputMapping.LEFT_STICK_Y] = new ControlAxis("YAxis", _controllerNum);
        controllerInputData[(int)InputMapping.DPAD_X] = new ControlAxis("5Axis", _controllerNum);
        controllerInputData[(int)InputMapping.DPAD_Y] = new ControlAxis("6Axis", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION1] = new ControlButton("1Button", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION2] = new ControlButton("2Button", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION3] = new ControlButton("0Button", _controllerNum);
        controllerInputData[(int)InputMapping.ACTION4] = new ControlButton("3Button", _controllerNum);
        controllerInputData[(int)InputMapping.LEFT_BUMPER] = new ControlButton("4Button", _controllerNum);
        controllerInputData[(int)InputMapping.RIGHT_BUMPER] = new ControlButton("5Button", _controllerNum);
        controllerInputData[(int)InputMapping.ADDITIONAL1] = new ControlButton("8Button", _controllerNum);
        controllerInputData[(int)InputMapping.ADDITIONAL2] = new ControlButton("9Button", _controllerNum);
    }

    public override ControllerProfile CreateControllerProfileInstance(int _controllerNum)
    {
        return new GenericControllerProfile(_controllerNum);
    }
}
