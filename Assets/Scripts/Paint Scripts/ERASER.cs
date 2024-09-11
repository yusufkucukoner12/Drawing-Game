using UnityEngine;
using UnityEngine.UI;

public class ERASER : MonoBehaviour
{
    public Texture2D canvasTexture;
    public Color brushColor = Color.white;
    public float brushSize = 0;
    public AudioSource eraserAudioSource; 
    public AudioClip eraserSound; 

    private Vector2? previousPoint = null;
    private bool isErasing = false;

    public Slider slider;
    private void Start()
    {
        slider.value = brushSize; 
        slider.onValueChanged.AddListener(UpdateBrushSize);
    }
    void UpdateBrushSize(float newSize)
    {
        brushSize = newSize;
    }

    void Update()
    {
        slider.gameObject.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            previousPoint = GetCanvasPoint();
            StartErasing();
        }
        else if (Input.GetMouseButton(0) && previousPoint != null)
        {
            Vector2 currentPoint = GetCanvasPoint();
            DrawLine(previousPoint.Value, currentPoint);
            previousPoint = currentPoint;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopErasing();
            previousPoint = null;
        }
    }

    void StartErasing()
    {
        if (!isErasing)
        {
            isErasing = true;
            if (eraserAudioSource != null && eraserSound != null)
            {
                eraserAudioSource.clip = eraserSound;
                eraserAudioSource.loop = true;
                eraserAudioSource.Play();
            }
        }
    }

    void StopErasing()
    {
        slider.gameObject.SetActive(false);
        if (isErasing)
        {
            isErasing = false;
            if (eraserAudioSource != null)
            {
                eraserAudioSource.Stop();
                eraserAudioSource.loop = false;
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
        for (int i = -Mathf.RoundToInt(brushSize ); i <= Mathf.RoundToInt(brushSize ); i++)
        {
            for (int j = -Mathf.RoundToInt(brushSize ); j <= Mathf.RoundToInt(brushSize ); j++)
            {
                int pixelX = x + i;
                int pixelY = y + j;

                if (pixelX >= 0 && pixelX < canvasTexture.width && pixelY >= 0 && pixelY < canvasTexture.height)
                {
                    canvasTexture.SetPixel(pixelX, pixelY, brushColor);
                    
                }
            }
        }

        canvasTexture.Apply();
    }
}



