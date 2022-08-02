using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using Assets.SimpleFileBrowserForWindows;
public class DownloadLogFile : MonoBehaviour
{

    string logFile;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Browser plugin should be called in OnPointerDown.
    public void FileDownload() {

        Debug.Log("Clicked download log button");
        var path = Application.persistentDataPath + "/" + "MineBuddiesLog.csv";
        var bytes = System.IO.File.ReadAllBytes(path);

        DownloadFile(gameObject.name, "OnFileDownload", "MineBuddiesLog.csv", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
        Debug.Log("Log file successfully downloaded");
    }

#else
    //
    // Standalone platforms & editor
    //

    public void OnPointerDown(PointerEventData eventData) { }

    // Listen OnClick event in standlone builds
    void Awake()
    {
        logFile = Application.persistentDataPath + "/" + "MineBuddiesLog.csv";
        if (!File.Exists(logFile))
        {
            Destroy(gameObject);
        }
        //button.onClick.AddListener(OnClick);
    }

    public void FileDownload()
    {
        StartCoroutine(WindowsFileBrowser.SaveFile("Save", null, "MineBuddiesLog", "CSV", ".csv", File.ReadAllBytes(logFile), (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        }));

    }

#endif

/*************
    [DllImport("__Internal")]
    private static extern void OpenLogFileInTab(string str);


    // Start is called before the first frame update

    public void OpenLogFileTab()

    {
        
        var path = Application.persistentDataPath + "/" + "MineBuddiesLog.csv";
        var bytes = System.IO.File.ReadAllBytes(path);
        OpenLogFileInTab(System.Convert.ToBase64String(bytes));
    }
**************/
}

