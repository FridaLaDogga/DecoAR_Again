using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShareScreenShot : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuCanvas;
    private ARPointCloudManager arPointCloudManager;
    // Start is called before the first frame update
    void Start()
    {
        arPointCloudManager = FindObjectOfType<ARPointCloudManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void screenshot()
    {
        TurnOnOffArUI();
        StartCoroutine(TakeScreenshotAndShare());
    }

    private void TurnOnOffArUI()//Desactivacion de la UI 
    {
        var points = arPointCloudManager.trackables;
        foreach (var point in points)
        {
            point.gameObject.SetActive(!point.gameObject.activeSelf);
        }
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
    }

    //Corutina de toma de Screenshot
    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("Mira mi futura renovación")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
        TurnOnOffArUI();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
