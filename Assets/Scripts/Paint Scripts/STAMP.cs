using UnityEngine;

public class STAMP: MonoBehaviour
{
    public Texture2D canvasTexture;
    public Texture2D stampTexture;

    public float stampSize = 0.5f;
    private Vector2? previousPoint = null;
    private bool isStamping = false;
    

    void Update()
    {   

        if (Input.GetMouseButtonDown(0))
        {
            previousPoint = GetCanvasPoint();
           
        }
        else if (Input.GetMouseButton(0) && previousPoint != null)
        {
            Vector2 currentPoint = GetCanvasPoint();
            DrawStamp(previousPoint.Value, currentPoint);
            previousPoint = currentPoint;
        }
        else if (Input.GetMouseButtonUp(0))
        {

            previousPoint = null;
        }
    }   
   
    Vector2 GetCanvasPoint()
    {
        Vector2 mousePosition = Input.mousePosition;
        float canvasWidthRatio = canvasTexture.width / (float)Screen.width;
        float canvasHeightRatio = canvasTexture.height / (float)Screen.height;
        return new Vector2(mousePosition.x * canvasWidthRatio, mousePosition.y * canvasHeightRatio);
    }

    void DrawStamp(Vector2 startPoint, Vector2 endPoint)
    {
        Vector2Int start = new Vector2Int((int)startPoint.x, (int)startPoint.y);
        Vector2Int end = new Vector2Int((int)endPoint.x, (int)endPoint.y);
        Vector2Int size = new Vector2Int(stampTexture.width, stampTexture.height);
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int pixel = new Vector2Int(start.x + x, start.y + y);
                if (pixel.x < 0 || pixel.x >= canvasTexture.width || pixel.y < 0 || pixel.y >= canvasTexture.height)
                    continue;
                Color stampColor = stampTexture.GetPixel(x, y);
                if (stampColor.a == 0)
                    continue;
                canvasTexture.SetPixel(pixel.x, pixel.y, stampColor);
            }
        }
        canvasTexture.Apply();
    }
}

