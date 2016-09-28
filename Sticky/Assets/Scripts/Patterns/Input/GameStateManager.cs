using UnityEngine;
using UnityEngine.Events;

public enum GameStates
{
    STATE_GAMEPLAY = 0,
    STATE_PAUSE,
    GAMESTATES_COUNT
}

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager gameStateManager = null;
    public static GameStateManager instance { get { return gameStateManager; } }

    private GameState[] states = null;
    private GameStates currentState = GameStates.STATE_GAMEPLAY;
    public GameStates state { get { return currentState; } }

    private UnityEvent[] stateEvents = null;

    private void Awake()
    {
        if (gameStateManager)
        {
            DestroyImmediate(this);
        }
        else
        {
            gameStateManager = this;
            //Create gamestate classes
            states = new GameState[(int)GameStates.GAMESTATES_COUNT];
            states[(int)GameStates.STATE_GAMEPLAY] = new GameplayState();
            states[(int)GameStates.STATE_PAUSE] = new PauseState();
            //Create events for the activating and deactivating of states
            stateEvents = new UnityEvent[(int)GameStates.GAMESTATES_COUNT * 2];
            for(int i = 0; i < stateEvents.Length; ++i)
            {
                stateEvents[i] = new UnityEvent();
            }
        }
    }

    private void Update()
    {
        states[(int)currentState].Update();
    }

	public void ChangeState(GameStates _state)
    {
        if (currentState != _state)
        {
            //Deactivate the state and invoke functionality on listeners for state deactivating
            states[(int)_state].OnStateDeactivate();
            stateEvents[((int)_state * 2) + 1].Invoke();
            //Change to new state
            currentState = _state;
            //Activate the state and invoke functionality on listeners for state activating
            states[(int)_state].OnStateActivate();
            stateEvents[((int)_state * 2)].Invoke();
        }
    }

    public bool CompareState(GameStates _state)
    {
        return currentState == _state ? true : false;
    }

    public void StartListeningState(GameStates _state, UnityAction _activateListener, UnityAction _deactivateListener)
    {
        if(_activateListener != null) stateEvents[(int)_state * 2].AddListener(_activateListener);
        if(_deactivateListener != null) stateEvents[((int)_state * 2) + 1].AddListener(_deactivateListener);
    }

    public void StopListeningState(GameStates _state, UnityAction _activateListener, UnityAction _deactivateListener)
    {
        if(_activateListener != null) stateEvents[(int)_state * 2].RemoveListener(_activateListener);
        if(_deactivateListener != null) stateEvents[((int)_state * 2) + 1].RemoveListener(_deactivateListener);
    }
}
