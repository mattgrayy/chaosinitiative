using UnityEngine;

public abstract class ControlMaster
{
    public ControlMaster(string _name, int _controllerNum, FloatModifier _axisMod = null)
    {
        mappingName = _name;
        controllerNumber = _controllerNum;
        if(_axisMod == null)
        {
            axisModifier = x => x;
        }
        else
        {
            axisModifier = _axisMod;
        }
    }
   
    public delegate float FloatModifier(float i);
    private FloatModifier axisModifier = null;

    protected string mappingName;
    protected int controllerNumber;

    public virtual float value { get { return 0.0f; } }
    public virtual float rawValue { get { return 0.0f; } }
    public virtual bool isHeld { get { return false; } }
    public virtual bool isPressed { get { return false; } }
    public virtual bool isReleased { get { return false; } }

    public virtual void UpdateControlInput() { }

    protected float GetModifiedAxis(string _name)
    {
        return axisModifier(Input.GetAxis(mappingName + controllerNumber));
    }

    protected float GetModifiedAxisRaw(string _name)
    {
        return axisModifier(Input.GetAxisRaw(mappingName + controllerNumber));
    }

}

public class ControlAxis : ControlMaster
{
    public ControlAxis(string _name, int _controllerNum, FloatModifier _axisMod = null) : base(_name, _controllerNum, _axisMod) { }

    public override float value { get { return GetModifiedAxis(mappingName + controllerNumber); } }
    public override float rawValue { get { return GetModifiedAxisRaw(mappingName + controllerNumber); } }
}

public class ControlButton : ControlMaster
{
    public ControlButton(string _name, int _controllerNum) : base(_name, _controllerNum) { }

    public override bool isHeld { get { return Input.GetButton(mappingName + controllerNumber); } }
    public override bool isPressed { get { return Input.GetButtonDown(mappingName + controllerNumber); } }
    public override bool isReleased { get { return Input.GetButtonUp(mappingName + controllerNumber); } }
}

public class ControlButtonFromAxis : ControlMaster
{
    public ControlButtonFromAxis(string _name, int _controllerNum, bool _isPositive = true, FloatModifier _axisMod = null) : base(_name, _controllerNum, _axisMod) { isPositive = _isPositive; }

    private bool isPositive;
    private bool isAxisHeld;
    private bool isAxisPressed;
    private bool isAxisReleased;

    public override bool isHeld { get { return isAxisHeld; } }
    public override bool isPressed { get { return isAxisPressed; } }
    public override bool isReleased { get { return isAxisReleased; } }

    public override void UpdateControlInput()
    {
        isAxisPressed = false;
        isAxisReleased = false;
        float _axis = GetModifiedAxis(mappingName + controllerNumber);
        if (!isAxisHeld)
        {
            if (_axis != 0.0f)
            {
                if (_axis > 0.0f)
                {
                    if (isPositive)
                    {
                        isAxisPressed = true;
                        isAxisHeld = true;
                    }
                }
                else
                {
                    if (!isPositive)
                    {
                        isAxisPressed = true;
                        isAxisHeld = true;
                    }
                }
            }
        }
        else
        { 
            if (_axis == 0.0f)
            {
                isAxisHeld = false;
                isAxisReleased = true;
            }
            else if (_axis > 0.0f)
            {
                if (!isPositive)
                {
                    isAxisHeld = false;
                    isAxisReleased = true;
                }
            }
            else
            {
                if (isPositive)
                {
                    isAxisHeld = false;
                    isAxisReleased = true;
                }
            }
        }
    }
}

