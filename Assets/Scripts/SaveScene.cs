using UnityEngine;

public class SaveScene : MonoBehaviour
{
    public Texture2D canvasTexture; 

    public void SaveDrawing()
    {
        byte[] bytes = canvasTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("SavedDrawing.png", bytes);
    }

    public void LoadDrawing()
    {
        byte[] bytes = System.IO.File.ReadAllBytes("SavedDrawing.png");

        canvasTexture.LoadImage(bytes);
    }
}

