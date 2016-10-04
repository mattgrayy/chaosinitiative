using UnityEngine;

public class CameraShake2 : MonoBehaviour
{
    private static CameraShake2 cameraShake = null;
    public static CameraShake2 instance { get { return cameraShake; } }

    private Transform camTransform = null;
    private Vector3 originalLocalPosition = Vector3.zero;

    private bool isShaking = false;
    private bool isShakingPaused = false;
    private float shakeDuration = 0.0f;
    private float shakeTime = 0.0f;
    private float shakeStrength = 0.7f;
    [SerializeField]
    private AnimationCurve shakeDecayRate = null;

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
        isShakingPaused = true;
    }

    public void ResumeShake()
    {
        isShakingPaused = false;
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


