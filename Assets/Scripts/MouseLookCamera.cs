using UnityEngine;

/// <summary>/ A simple mouse look camera controller that can be used to explore the beach scene.
/// Click to lock the cursor and look around, or hold right-click to temporarily look without locking.</summary>
public class MouseLookCamera : MonoBehaviour
{
    // Inspector settings for mouse look limits and the helper UI.
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private bool lockCursorOnClick = true;
    [SerializeField] private bool showInstructions = true;

    // Runtime rotation state and cached IMGUI resources.
    private float yaw;
    private float pitch;
    private GUIStyle instructionStyle;
    private Texture2D sandPanelTexture;

    private void Start()
    {
        // Start from the camera's current scene rotation.
        Vector3 currentRotation = transform.eulerAngles;
        yaw = currentRotation.y;
        pitch = NormalizeAngle(currentRotation.x);
    }

    private void Update()
    {
        // Left click locks the cursor; Escape releases it.
        if (lockCursorOnClick && Input.GetMouseButtonDown(0))
        {
            SetCursorLock(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(false);
        }

        // Only apply mouse look while the camera is in look mode.
        if (!CanLook())
        {
            return;
        }

        // Accumulate mouse input and apply the clamped rotation.
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void OnGUI()
    {
        // Draw the bottom-left instruction panel when enabled.
        if (!showInstructions)
        {
            return;
        }

        EnsureStyles();

        string message = CanLook()
            ? "Move mouse to look around  |  Esc to release"
            : "Click the beach scene to look around";

        Vector2 size = instructionStyle.CalcSize(new GUIContent(message));
        float paddingX = 16f;
        float paddingY = 10f;
        float width = Mathf.Min(size.x + paddingX * 2f, Screen.width - 24f);
        float height = size.y + paddingY * 2f;
        Rect panelRect = new Rect(16f, Screen.height - height - 16f, width, height);

        GUI.DrawTexture(panelRect, sandPanelTexture);
        GUI.Label(panelRect, message, instructionStyle);
    }

    private void EnsureStyles()
    {
        // Create the panel texture and label style once, then reuse them.
        if (instructionStyle != null)
        {
            return;
        }

        sandPanelTexture = MakeRoundedTexture(16, 10, new Color(0.93f, 0.79f, 0.55f, 0.88f), 4f);

        instructionStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleLeft,
            fontSize = 12,
            fontStyle = FontStyle.Normal,
            padding = new RectOffset(16, 16, 10, 10),
            normal = { textColor = new Color(0.12f, 0.27f, 0.32f, 1f) }
        };
    }

    private static void SetCursorLock(bool locked)
    {
        // Keep cursor visibility in sync with the chosen lock state.
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    private static bool CanLook()
    {
        return Cursor.lockState == CursorLockMode.Locked || Input.GetMouseButton(1);
    }

    private static Texture2D MakeRoundedTexture(int width, int height, Color color, float radius)
    {
        // Build a small rounded fill texture by clearing pixels outside the corner radius.
        Texture2D texture = new Texture2D(width, height);
        Color clear = new Color(color.r, color.g, color.b, 0f);
        float clampedRadius = Mathf.Min(radius, Mathf.Min(width, height) * 0.5f);
        float maxX = width - clampedRadius - 1f;
        float maxY = height - clampedRadius - 1f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float nearestX = Mathf.Clamp(x, clampedRadius - 1f, maxX);
                float nearestY = Mathf.Clamp(y, clampedRadius - 1f, maxY);
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(nearestX, nearestY));
                texture.SetPixel(x, y, distance <= clampedRadius ? color : clear);
            }
        }

        texture.Apply();
        return texture;
    }

    private static float NormalizeAngle(float angle)
    {
        // Convert Unity's 0-360 rotation into a signed pitch angle.
        return angle > 180f ? angle - 360f : angle;
    }
}
