using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BRUSH : MonoBehaviour
{
    public Texture2D canvasTexture; 
    public Color brushColor = Color.black;
    public float brushSize = 0.5f;
    public Texture2D brushCursor; 
    public AudioSource brushAudioSource; 
    public AudioClip a;
    public Slider slider;

    
    private Vector2? previousPoint = null;
    private bool isDrawing = false;
    
    private void Start()
    {
        slider.value = brushSize;
        slider.onValueChanged.AddListener(UpdateBrushSize);
    }
    void UpdateBrushSize(float newSize)
    {
        brushSize = newSize;
    }
    void LateUpdate()
    {
        Cursor.SetCursor(brushCursor, Vector2.zero, CursorMode.Auto);
    }
    void Update() // works all the time.
    {
        slider.gameObject.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            previousPoint = GetCanvasPoint();
            StartDrawing();
        }
        // Check for left mouse button hold (drag)
        else if (Input.GetMouseButton(0) && previousPoint != null)
        {
            Vector2 currentPoint = GetCanvasPoint();
            DrawLine(previousPoint.Value, currentPoint);
            previousPoint = currentPoint;
        }
        // Check for left mouse button release
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
            previousPoint = null;
        }
    }

    void StartDrawing()
    {
        if (!isDrawing)
        {
            isDrawing = true;
            if (brushAudioSource != null && a != null)
            {
                brushAudioSource.clip = a;
                brushAudioSource.loop = true;
                brushAudioSource.Play();
            }
        }
    }

    void StopDrawing()
    {
        if (isDrawing)
        {
            isDrawing = false;
            if (brushAudioSource != null)
            {
                brushAudioSource.Stop();
                brushAudioSource.loop = false;
            }
        }
    }

    Vector2 GetCanvasPoint()
    {
        Vector2 mousePosition = Input.mousePosition;
        float canvasWidthRatio = canvasTexture.width / (float)Screen.width;
        float canvasHeightRatio = canvasTexture.height / (float)Screen.height;
        return new Vector2(mousePosition.x * canvasWidthRatio, mousePosition.y * canvasHeightRatio);
    }

    void DrawLine(Vector2 startPoint, Vector2 endPoint)
    {
        Vector2 canvasStartPoint = startPoint;
        Vector2 canvasEndPoint = endPoint;

        Vector2 delta = canvasEndPoint - canvasStartPoint;
        float step = 1.0f / Mathf.Max(Mathf.Abs(delta.x), Mathf.Abs(delta.y));

        for (float t = 0; t <= 1.0f; t += step)
        {
            Vector2 interpolatedPoint = Vector2.Lerp(canvasStartPoint, canvasEndPoint, t);
            DrawPoint((int)interpolatedPoint.x, (int)interpolatedPoint.y);
        }
    }


    void DrawPoint(int x, int y)
    {
        int radius = Mathf.RoundToInt(brushSize);
        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                if (i * i + j * j <= radius * radius)
                {
                    int pixelX = x + i;
                    int pixelY = y + j;

                    if (pixelX >= 0 && pixelX < canvasTexture.width && pixelY >= 0 && pixelY < canvasTexture.height)
                    {
                        canvasTexture.SetPixel(pixelX, pixelY, brushColor);
                    }
                }
            }
        }

        canvasTexture.Apply();
    }
}
