using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    protected ControlMethod controlMethod = null;

    public void AssignControlMethod(ControlMethod _method)
    {
        controlMethod = _method;
    }

    public void UpdateControlMethod()
    {
        controlMethod.UpdateProfile();
    }

    //public float moveX
    //{
    //    get
    //    {
    //        if(controlMethod is Controller)
    //        {
    //            return ((Controller)controlMethod).Value(InputMapping.DPAD_Y);
    //        }
    //        return 0.0f;
    //    }
    //}

    //public bool fire
    //{
    //    get
    //    {
    //        if (controlMethod is Controller)
    //        {
    //            return ((Controller)controlMethod).IsPressed(InputMapping.RIGHT_BUMPER);
    //        }
    //        return false;
    //    }
    //}
}
