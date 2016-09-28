using UnityEngine;
using System.Collections.Generic;

public class ControllerManager : MonoBehaviour
{
    private static ControllerManager controllerManager = null;
    public static ControllerManager instance { get { return controllerManager; } }

    private ControllerProfile[] profiles = null;
    private List<Controller> controllers = new List<Controller>();
    private int controllersSetup = 0;

    private void Awake()
    {
        if (controllerManager)
        {
            DestroyImmediate(this);
        }
        else
        {
            controllerManager = this;
            profiles = new ControllerProfile[]
            {
                new XBOX360Profile(0),
                new DS4Profile(0),
                new GenericControllerProfile(0)
            };
        }
    }

    private void Update()
    {
        //Prepare slots for all possible controllers
        while (controllers.Count != Input.GetJoystickNames().Length)
        {
            //If a controller has been plugged in but is not plugged in during setup
            if (Input.GetJoystickNames()[controllers.Count] == string.Empty)
            {
                controllers.Add(new Controller(controllers.Count + 1));
            }
            //Controller is plugged in so find appropriate profile
            else
            {
                controllers.Add(new Controller(controllers.Count + 1));
                AssignProfileToController(controllers.Count - 1);
            }
        }
        //If controllers have been created but not setup
        if(controllersSetup != controllers.Count)
        {
            //Check all controllers to find which have not had a profile provided
            for(int i = 0; i < controllers.Count; ++i)
            {
                if(!controllers[i].hasProfileBeenProvided)
                {
                    //There is a profile available
                    if (Input.GetJoystickNames()[i] != string.Empty)
                    {
                        AssignProfileToController(i);
                    }
                }
            }
        }
    }

    private void AssignProfileToController(int _index)
    {
        bool _profileNotFound = true;
        foreach (ControllerProfile _profile in profiles)
        {
            if (_profile.profileName == Input.GetJoystickNames()[_index])
            {
                controllers[_index].ProvideProfile(_profile);
                _profileNotFound = false;
                break;
            }
        }
        if (_profileNotFound)
        {
            controllers[_index].ProvideProfile(profiles[profiles.Length - 1]);
        }
        ++controllersSetup;
    }

    public Controller GetController(int _index)
    {
        if (_index < controllers.Count && _index > -1)
        {
            if (controllers[_index].hasProfileBeenProvided)
            {
                return controllers[_index];
            }
        }
        return null;
    }
}
