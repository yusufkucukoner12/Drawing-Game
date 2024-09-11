using UnityEngine;
using UnityEngine.UI;

public class TextureManager : MonoBehaviour
{
    [SerializeField]private RawImage canvasRawImage; 
    [SerializeField]private Texture2D textureToDisplay; 

    void Start()
    {
        if (canvasRawImage != null && textureToDisplay != null)
        {
            canvasRawImage.texture = textureToDisplay;
        }
        else
        {
            Debug.LogError("Canvas RawImage or Texture2D not assigned!");
        }
    }
}
