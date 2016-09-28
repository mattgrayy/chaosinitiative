using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake cameraShake = null;
    public static CameraShake instance { get { return cameraShake; } }

    private Transform camTransform = null;
    private Vector3 originalLocalPosition = Vector3.zero;

    private bool isShaking = false;
    private bool isShakingPaused = false;
    private float shakeDuration = 0.0f;
    private float shakeTime = 0.0f;
    private float shakeStrength = 0.7f;
    [SerializeField] private AnimationCurve shakeDecayRate = null;

    private void Awake()
    {
        if (cameraShake)
        {
            DestroyImmediate(this);
        }
        else
        {
            cameraShake = this;
            camTransform = Camera.main.transform;
        }
    }

    private void Start()
    {
        GameStateManager.instance.StartListeningState(GameStates.STATE_PAUSE, PauseShake, null);
    }

    private void Update()
    {
        if (!isShakingPaused)
        {
            if (isShaking)
            {
                shakeTime += Time.deltaTime;
                if (shakeTime < shakeDuration)
                {
                    camTransform.localPosition = originalLocalPosition + ((Random.insideUnitSphere * shakeStrength) * shakeDecayRate.Evaluate(shakeTime / shakeDuration));
                }
                else
                {
                    isShaking = false;
                    camTransform.localPosition = originalLocalPosition;
                }
            }
        }
    }

    public void PauseShake()
    {
        GameStateManager.instance.StopListeningState(GameStates.STATE_PAUSE, PauseShake, null);
        isShakingPaused = true;
        GameStateManager.instance.StartListeningState(GameStates.STATE_GAMEPLAY, ResumeShake, null);
    }

    public void ResumeShake()
    {
        GameStateManager.instance.StopListeningState(GameStates.STATE_PAUSE, ResumeShake, null);
        isShakingPaused = false;
        GameStateManager.instance.StartListeningState(GameStates.STATE_GAMEPLAY, PauseShake, null);
    }

    public void ShakeCamera(float _duration, float _strength)
    {
        originalLocalPosition = camTransform.localPosition;
        shakeTime = 0.0f;
        shakeDuration = _duration;
        shakeStrength = _strength;
        isShaking = true;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : UnityEditor.Editor
{
    private float duration = 2.0f;
    private float strength = 0.7f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UnityEditor.EditorGUILayout.Space();
        UnityEditor.EditorGUILayout.LabelField("Camera Shake Test", UnityEditor.EditorStyles.boldLabel);

        CameraShake myScript = (CameraShake)target;

        duration = UnityEditor.EditorGUILayout.Slider("Duration", duration, 0.1f, 10.0f);
        strength = UnityEditor.EditorGUILayout.Slider("Strength", strength, 0.1f, 5.0f);

        if (GUILayout.Button("Shake"))
        {
            myScript.ShakeCamera(duration, strength);
        }
    }
}
#endif

