using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QRReader : MonoBehaviour
{
    /// <summary>
    /// QRコードの読み込み結果格納
    /// </summary>
    private string result = string.Empty;

    /// <summary>
    /// Webカメラ
    /// </summary>
    private WebCamTexture webCam = default;

    /// <summary>
    /// デリゲート
    /// </summary>
    /// <param name="result"></param>
    public delegate void DELEGATE(string result);
    /// <summary>
    /// QRコード読み込み時のコールバック
    /// </summary>
    private DELEGATE QRCodeScanedCallback;

    [SerializeField] private Text text;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
        {
            Debug.LogFormat("no camera.");
            yield break;
        }
        Debug.LogFormat("camera ok.");
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices == null || devices.Length == 0) yield break;
        webCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 12);
        webCam.Play();
    }

    /// <summary>
    /// ループ
    /// </summary>
    void Update()
    {
        if (webCam != null)
        {
            result = QRCodeHelper.Read(webCam);
            Debug.LogFormat("result : " + result);
            if (!result.Equals("error"))
                QRCodeScanedCallback?.Invoke(result);
            Toast.makeText(result);
            text.text = "camera ok." + result;
        }
    }

    /// <summary>
    /// コールバックイベントの追加
    /// </summary>
    /// <param name="callback"></param>
    public void AddQRCodeScanedCallback(DELEGATE callback)
    {
        QRCodeScanedCallback = callback;
    }
}
