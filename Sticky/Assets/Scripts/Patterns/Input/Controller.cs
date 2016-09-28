using UnityEngine;

public class Controller : ControlMethod
{
    private int controllerNumber;
    private ControllerProfile controllerProfile;

    public Controller(int _controllerNumber)
    {
        controllerNumber = _controllerNumber;
        controllerProfile = null;
    }

    public bool hasProfileBeenProvided { get { return controllerProfile != null; } }

    public void ProvideProfile(ControllerProfile _profile)
    {
        controllerProfile = _profile.CreateControllerProfileInstance(controllerNumber);
    }

    public ControlMaster InputData(InputMapping _input)
    {
        return controllerProfile.GetData(_input,controllerNumber);
    }

    public override void UpdateProfile()
    {
        controllerProfile.UpdateControllerInputData();
    }
}
