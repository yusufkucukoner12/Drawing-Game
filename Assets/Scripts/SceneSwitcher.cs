using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Globalization;


public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]private string mainMenu;
    [SerializeField]private string drawingScene;

    public Texture2D canvasTexture1;
    


    private string fileName = "asdadas";
    private string firstFiel = "firstfile";
    private void Awake()
    {
        saveImage(firstFiel, canvasTexture1);
    }

    public void SwitchToScene1()
    {
        
        saveImage(fileName, canvasTexture1);
        SceneManager.LoadScene(mainMenu); 
    }

    public void SwitchToDrawingScene() {
        
        loadImage(canvasTexture1, fileName);   
        SceneManager.LoadScene(drawingScene);
    
    }

    public void saveImage(string file, Texture2D canvasTexture)
    {
        string filePath = Path.Combine(Application.persistentDataPath, file);
        byte[] bytes = canvasTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    public void loadImage(Texture2D canvasTexture, string file)
    {
        
            string filePath = Path.Combine(Application.persistentDataPath, file);
            Debug.Log("Loading canvas texture from: " + filePath);
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D loadedTexture = new Texture2D(2, 2);
            loadedTexture.LoadImage(bytes);
            canvasTexture.SetPixels(loadedTexture.GetPixels());
            canvasTexture.Apply();
    }


    public void resetIt(Texture2D canvasTexture, string firstFile)
    {
        string filePath = Path.Combine(Application.persistentDataPath, firstFile);
        byte[] bytes = File.ReadAllBytes(filePath);
        Texture2D loadedTexture = new Texture2D(2, 2);
        loadedTexture.LoadImage(bytes);
        canvasTexture.SetPixels(loadedTexture.GetPixels());
        canvasTexture.Apply();
    }

    public void resetTheTexture()
    {
        resetIt(canvasTexture1, firstFiel); 
    }
}

