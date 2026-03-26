using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Y Dead Zone")]
    [SerializeField] private float yDeadZone = 1.5f;

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 3f;
    [SerializeField] private float lookAheadSpeed = 5f;

    [Header("Screenshake")]
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.3f;

    [Header("Level Boundaries")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 360f;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 15f;

    private float targetY;
    private float currentLookAhead;
    private float shakeTimer;
    private Vector3 shakeOffset;
    private Vector3 velocity;

    void Start()
    {
        targetY = transform.position.y;
        if(player == null)
        {
            Debug.LogError("the player position is missing in camera script");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        HandleScreenshake();
        CalculateLookAhead();
        CalculateTargetY();
        CalculateEndZoneOffset();

        Vector3 desiredPosition = new Vector3(
            player.position.x + offset.x + currentLookAhead + shakeOffset.x,
            targetY + offset.y + shakeOffset.y,
            offset.z
        );

        desiredPosition = ClampPosition(desiredPosition);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            1f / smoothSpeed
        );
    }

    void CalculateLookAhead()
    {
        float input = Input.GetAxisRaw("Horizontal");
        float targetLookAhead = input > 0.1f ? lookAheadDistance : 0f;
        currentLookAhead = Mathf.Lerp(currentLookAhead, targetLookAhead, lookAheadSpeed * Time.deltaTime);
    }

    void CalculateTargetY()
    {
        float yDiff = player.position.y - targetY;
        if (Mathf.Abs(yDiff) > yDeadZone)
        {
            targetY = player.position.y - Mathf.Sign(yDiff) * yDeadZone;
        }
    }

    void CalculateEndZoneOffset()
    {
        if (player.position.x >= 345)
        {
            offset = Vector3.Lerp(offset, new Vector3(0, 1, -10.66f), Time.deltaTime * 3f);
        }
    }

    Vector3 ClampPosition(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        return pos;
    }

    void HandleScreenshake()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            shakeOffset = new Vector3(x, y, 0);
        }
        else
        {
            shakeOffset = Vector3.zero;
        }
    }

    public void TriggerShake()
    {
        shakeTimer = shakeDuration;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeTimer = duration;
        shakeMagnitude = magnitude;
    }
}