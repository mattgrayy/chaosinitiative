using UnityEngine;
using System.Collections;

public class DebugInputHandler : InputHandler
{
    private static DebugInputHandler debugInputHandler = null;
    public static DebugInputHandler instance { get { return debugInputHandler; } }

    [SerializeField] private int controllerTest = 0;

    private void Awake()
    {
        debugInputHandler = this;
    }

    private void Update()
    {
        if (controlMethod != null)
        {
            UpdateControlMethod();
        }
    }

    public void AssignController()
    {
        controlMethod = ControllerManager.instance.GetController(controllerTest);
    }

    private void OnGUI()
    {
        if (controlMethod != null)
        {
            GUI.Label(new Rect(5, 5, 250, 20), "LeftStick: " + leftStick.ToString());
            GUI.Label(new Rect(5, 25, 250, 20), "LeftStickButton: " + leftStickPress.isHeld.ToString());
            GUI.Label(new Rect(5, 45, 250, 20), "RightStick: " + rightStick.ToString());
            GUI.Label(new Rect(5, 65, 250, 20), "RightStickButton: " + rightStickPress.isHeld.ToString());

            GUI.Label(new Rect(5, 85, 250, 20), "DPad: " + dPad.ToString());
            GUI.Label(new Rect(5, 105, 250, 20), "DPadUp: " + dPadUp.isHeld.ToString());
            GUI.Label(new Rect(5, 125, 250, 20), "DPadDown: " + dPadDown.isHeld.ToString());
            GUI.Label(new Rect(5, 145, 250, 20), "DPadLeft: " + dPadLeft.isHeld.ToString());
            GUI.Label(new Rect(5, 165, 250, 20), "DPadRight: " + dPadRight.isHeld.ToString());

            GUI.Label(new Rect(5, 185, 250, 20), "ActionDown: " + actionDown.isHeld.ToString());
            GUI.Label(new Rect(5, 205, 250, 20), "ActionRight: " + actionRight.isHeld.ToString());
            GUI.Label(new Rect(5, 225, 250, 20), "ActionLeft: " + actionLeft.isHeld.ToString());
            GUI.Label(new Rect(5, 245, 250, 20), "ActionUp: " + actionUp.isHeld.ToString());

            GUI.Label(new Rect(5, 265, 250, 20), "LeftTrigger: " + leftTrigger.value.ToString());
            GUI.Label(new Rect(5, 285, 250, 20), "LeftTriggerButton: " + leftTriggerButton.isHeld.ToString());
            GUI.Label(new Rect(5, 305, 250, 20), "LeftBumper: " + leftBumper.isHeld.ToString());

            GUI.Label(new Rect(5, 325, 250, 20), "RightTrigger: " + rightTrigger.value.ToString());
            GUI.Label(new Rect(5, 345, 250, 20), "RightTriggerButton: " + rightTriggerButton.isHeld.ToString());
            GUI.Label(new Rect(5, 365, 250, 20), "RightBumper: " + rightBumper.isHeld.ToString());

            GUI.Label(new Rect(5, 385, 250, 20), "LeftAdditional: " + leftAdditional.isHeld.ToString());
            GUI.Label(new Rect(5, 405, 250, 20), "RightAdditional: " + rightAdditional.isHeld.ToString());
        }
    }

    public Vector2 leftStick
    {
        get
        {
            if (controlMethod is Controller)
            {
                return new Vector2(((Controller)controlMethod).InputData(InputMapping.LEFT_STICK_X).value, -((Controller)controlMethod).InputData(InputMapping.LEFT_STICK_Y).value);
            }
            Debug.Log("LeftStick Axis Error");
            return Vector2.zero;
        }
    }

    public Vector2 rightStick
    {
        get
        {
            if (controlMethod is Controller)
            {
                return new Vector2(((Controller)controlMethod).InputData(InputMapping.RIGHT_STICK_X).value, -((Controller)controlMethod).InputData(InputMapping.RIGHT_STICK_Y).value);
            }
            Debug.Log("RightStick Axis Error");
            return Vector2.zero;
        }
    }

    public Vector2 dPad
    {
        get
        {
            if (controlMethod is Controller)
            {
                return new Vector2(((Controller)controlMethod).InputData(InputMapping.DPAD_X).value, ((Controller)controlMethod).InputData(InputMapping.DPAD_Y).value);
            }
            Debug.Log("DPad Axis Error");
            return Vector2.zero;
        }
    }

    public ControlMaster leftTrigger
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.LEFT_TRIGGER);
            }
            Debug.Log("LeftTrigger Axis Error");
            return null;
        }
    }

    public ControlMaster rightTrigger
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.RIGHT_TRIGGER);
            }
            Debug.Log("RightTrigger Axis Error");
            return null;
        }
    }

    public ControlMaster leftStickPress
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.LEFT_STICK_BUTTON);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster rightStickPress
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.RIGHT_STICK_BUTTON);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster actionDown
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.ACTION1);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster actionRight
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.ACTION2);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster actionLeft
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.ACTION3);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster actionUp

    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.ACTION4);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster dPadDown
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.DPAD_DOWN);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster dPadUp
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.DPAD_UP);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster dPadLeft
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.DPAD_LEFT);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster dPadRight
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.DPAD_RIGHT);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster leftTriggerButton
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.LEFT_TRIGGER_BUTTON);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster rightTriggerButton
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.RIGHT_TRIGGER_BUTTON);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster leftBumper
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.LEFT_BUMPER);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster rightBumper
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.RIGHT_BUMPER);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster leftAdditional
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.ADDITIONAL1);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }

    public ControlMaster rightAdditional
    {
        get
        {
            if (controlMethod is Controller)
            {
                return ((Controller)controlMethod).InputData(InputMapping.ADDITIONAL2);
            }
            Debug.Log("Button Axis Error");
            return null;
        }
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(DebugInputHandler))]
public class DebugInputHandlerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DebugInputHandler myScript = (DebugInputHandler)target;
        if (GUILayout.Button("Assign Controller"))
        {
            myScript.AssignController();
        }
    }
}
#endif
