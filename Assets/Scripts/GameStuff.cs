using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameStuff : MonoBehaviour
{
    [SerializeField] Texture2D canvasTexture; 
    [SerializeField] private string file;

    void Awake()
    {
        LoadCanvasTexture();
    }

    void OnApplicationQuit()
    {
        SaveCanvasTexture();
    }

    public void LoadCanvasTexture()
    {
        string filePath = Path.Combine(Application.persistentDataPath, file);
        if (File.Exists(filePath))
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D loadedTexture = new Texture2D(2, 2);
            loadedTexture.LoadImage(bytes);
            canvasTexture.SetPixels(loadedTexture.GetPixels());
            canvasTexture.Apply();
        }
        else
        {
            Debug.LogWarning("Canvas texture file not found at path: " + filePath);
        }
    }

    public void SaveCanvasTexture()
    {
        string filePath = Path.Combine(Application.persistentDataPath, file);
        byte[] bytes = canvasTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }
}
