/***********************************************************************************
 * CameraShake.cs
 * Generic camera shaker from Generic Framework Project developed by Shaun Landy 
***********************************************************************************/

using UnityEngine;

/// <summary>
/// Shakes the camera given a strength and duration
/// </summary>
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

    /// <summary>
    /// Manages the shaking of the camera
    /// </summary>
    private void Update()
    {
        //Only do if not paused
        if (!isShakingPaused)
        {
            if (isShaking)
            {
                shakeTime += Time.deltaTime;
                //Shakes the camera
                if (shakeTime < shakeDuration)
                {
                    camTransform.localPosition = originalLocalPosition + ((Random.insideUnitSphere * shakeStrength) * shakeDecayRate.Evaluate(shakeTime / shakeDuration));
                }
                //Set back to original position at the beginning of the shake
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
        isShakingPaused = true;
    }

    public void ResumeShake()
    {
        isShakingPaused = false;
    }

    /// <summary>
    /// Shakes the camera and can be called from anywhere
    /// </summary>
    /// <param name="_duration">How long the shake should last</param>
    /// <param name="_strength">How strong the shake should be</param>
    public void ShakeCamera(float _duration, float _strength)
    {
        originalLocalPosition = camTransform.localPosition;
        shakeTime = 0.0f;
        shakeDuration = _duration;
        shakeStrength = _strength;
        isShaking = true;
    }
}

/// <summary>
/// Editor functionality to test within the inspector
/// </summary>
#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : UnityEditor.Editor
{
    private float duration = 0.2f;
    private float strength = 0.2f;

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

