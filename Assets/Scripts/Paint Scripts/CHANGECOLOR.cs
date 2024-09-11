using UnityEngine;
using UnityEngine.UI;

public class CHANGECOLOR : MonoBehaviour
{
    public BRUSH drawingManagerScript;
    public BUCKET2 bucketManagerScript;
    public Image brushButtonImage;
    public Image bucketButtonImage;

    public Sprite redButtonImage;
    public Sprite yellowButtonImage;
    public Sprite blueButtonImage;
    public Sprite pinkButtonImage;
    public Sprite purpleButtonImage;
    public Sprite orangeButtonImage;
    public Sprite greenButtonImage;
    public Sprite blackButtonImage;

    public Sprite redButtonImage1;
    public Sprite yellowButtonImage1;
    public Sprite blueButtonImage1;
    public Sprite pinkButtonImage1;
    public Sprite purpleButtonImage1;
    public Sprite orangeButtonImage1;
    public Sprite greenButtonImage1;
    public Sprite blackButtonImage1;

    public void red()
    {
        ChangeColor("#ff0000", redButtonImage, redButtonImage1);
    }

    public void yellow()
    {
        ChangeColor("#ffff00", yellowButtonImage, yellowButtonImage1);
    }

    public void blue()
    {
        ChangeColor("#0000ff", blueButtonImage, blueButtonImage1);
    }

    public void pink()
    {
        ChangeColor("#ffc0cb", pinkButtonImage, pinkButtonImage1);
    }

    public void purple()
    {
        ChangeColor("#800080", purpleButtonImage, purpleButtonImage1);
    }

    public void orange()
    {
        ChangeColor("#ffa500", orangeButtonImage, orangeButtonImage1);
    }

    public void green()
    {
        ChangeColor("#008000", greenButtonImage, greenButtonImage1);
    }

    public void black()
    {
        ChangeColor("#000000", blackButtonImage, blackButtonImage1);

    }
    public void white()
    {
        ChangeColor("#FFFFFF", blackButtonImage, blackButtonImage1);
    }

    private void ChangeColor(string colorHex, Sprite buttonImage, Sprite buttonImage1)
    {
        Color newColor;
        ColorUtility.TryParseHtmlString(colorHex, out newColor);
        drawingManagerScript.brushColor = newColor;
        bucketManagerScript.brushColor = newColor;

        if (brushButtonImage != null)
            brushButtonImage.sprite = buttonImage;

        if (bucketButtonImage != null)
            bucketButtonImage.sprite = buttonImage1;
    }
}
