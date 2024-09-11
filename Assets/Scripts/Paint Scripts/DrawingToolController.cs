using UnityEngine;

public class DrawingToolController : MonoBehaviour
{
    public BRUSH drawingManagerScript;
    public ERASER eraserToolScript;
    public BUCKET2 bucketToolScript;
    public STAMP stampToolScript;

    public AudioSource audioSource;
    void Start()
    {
        ActivateDrawingManager();
    }
    public void ActivateDrawingManager()
    {
        audioSource.Stop();
        drawingManagerScript.enabled = true;
        eraserToolScript.enabled = false;
        stampToolScript.enabled = false;
        bucketToolScript.enabled = false;
        
    }

    public void ActivateEraserTool()
    {

        audioSource.Stop();
        drawingManagerScript.enabled = false;
        eraserToolScript.enabled = true;
        stampToolScript.enabled = false; 
        bucketToolScript.enabled = false;

        
    }

    public void ActivatBucketTool()
    {
        drawingManagerScript.enabled = false;
        eraserToolScript.enabled = false;
        stampToolScript.enabled = false;
        bucketToolScript.enabled = true;
        audioSource.Stop();


    }
    public void ActivatStampTool()
    {
        drawingManagerScript.enabled = false;
        eraserToolScript.enabled = false;
        stampToolScript.enabled = true;
        bucketToolScript.enabled = false;
        audioSource.Stop();


    }
}

