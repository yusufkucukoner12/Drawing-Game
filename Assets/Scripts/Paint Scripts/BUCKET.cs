using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BUCKET2 : MonoBehaviour
{
    public Texture2D canvasTexture; 
    public Color brushColor = Color.black;
    public float brushSize = 5f;
    public Slider slider;

    private Vector2? previousPoint = null;

    public AudioSource bucketAudioSource; 
    public AudioClip a;
    void Update()
    {
        slider.gameObject.SetActive(false);

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 canvasPoint = GetCanvasPoint();
            if (canvasPoint.x >= 0 && canvasPoint.x < canvasTexture.width &&
                canvasPoint.y >= 0 && canvasPoint.y < canvasTexture.height)
            {
                bucketAudioSource.PlayOneShot(a);

                FloodFill((int)canvasPoint.x, (int)canvasPoint.y, canvasTexture.GetPixel((int)canvasPoint.x, (int)canvasPoint.y), brushColor);
                canvasTexture.Apply();
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

    void FloodFill(int x, int y, Color targetColor, Color fillColor)
    {
        if (targetColor == fillColor)
            return;

        Stack<Vector2Int> pixels = new Stack<Vector2Int>();
        pixels.Push(new Vector2Int(x, y));

        while (pixels.Count > 0)
        {
            Vector2Int pixel = pixels.Pop();
            int px = pixel.x;
            int py = pixel.y;

            if (px < 0 || px >= canvasTexture.width || py < 0 || py >= canvasTexture.height)
                continue;

            if (canvasTexture.GetPixel(px, py) != targetColor)
                continue;

            canvasTexture.SetPixel(px, py, fillColor);

            pixels.Push(new Vector2Int(px + 1, py)); 
            pixels.Push(new Vector2Int(px - 1, py)); 
            pixels.Push(new Vector2Int(px, py + 1)); 
            pixels.Push(new Vector2Int(px, py - 1)); 
        }
    }
}
