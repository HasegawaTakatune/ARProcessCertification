using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class SyncStatusData : MonoBehaviour
{

    /// <summary>
    /// ステータス表示UI名
    /// </summary>
    private const string STATUS_SCREEN = "StatusScreen";
    private const string STATUS_SCREEN2 = "StatusScreen2";

    /// <summary>
    /// マネージャオブジェクト
    /// </summary>
    private const string MANAGER = "Manager";

    /// <summary>
    /// ローディングUI名
    /// </summary>
    public const string NOW_LOADING = "NowLoading";

    /// <summary>
    /// ステータス表示UI
    /// </summary>
    [SerializeField] private IStatusScreen screen = default;

    /// <summary>
    /// QRリーダーオブジェクト
    /// </summary>
    [SerializeField] private QRReader reader = default;

    /// <summary>
    /// ローディングUIオブジェクト
    /// </summary>
    [SerializeField] private GameObject nowLoading = default;

    /// <summary>
    /// オブジェクトインスペクタリセット時の処理
    /// </summary>
    private void Reset()
    {
        screen = GameObject.Find(STATUS_SCREEN)?.GetComponent<StatusScreen>();
        if (screen == null)
            screen = GameObject.Find(STATUS_SCREEN2)?.GetComponent<StatusScreen>();

        reader = GameObject.Find(MANAGER).GetComponent<QRReader>();

        nowLoading = GameObject.Find(NOW_LOADING);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        reader.AddQRCodeScanedCallback(GetStatusData);
    }

    /// <summary>
    /// DB上のステータスを取得
    /// </summary>
    /// <param name="objectId"></param>
    public void GetStatusData(string objectId)
    {
        StatusData data = new StatusData();
        string inputObjectId = string.Empty;
        string inputProgress = string.Empty;
        string inputMessage = string.Empty;

        nowLoading.SetActive(true);

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(Common.TABLE_PROCESS);

        query.WhereEqualTo(Common.COLUMN_OBJECT_ID, objectId);
        query.FindAsync((List<NCMBObject> objectList, NCMBException e) =>
        {
            if (e != null)
            {
                return;
            }
            else
            {
                foreach (NCMBObject obj in objectList)
                {
                    inputObjectId = obj.ObjectId;
                    inputProgress = obj[Common.COLUMN_PROGRESS].ToString();
                    inputMessage = obj[Common.COLUMN_COMMENT].ToString();
                }

                data.Set(inputObjectId, inputProgress, inputProgress);
                screen.SetStatus(data);
            }
            nowLoading.SetActive(false);
        });
    }

    /// <summary>
    /// DBにステータスを保存
    /// </summary>
    public void SetStatusData()
    {
        StatusData data = screen.GetStatus();

        nowLoading.SetActive(true);

        NCMBObject nCMBObject = new NCMBObject(Common.TABLE_PROCESS);
        nCMBObject.ObjectId = data.ObjectId;
        nCMBObject[Common.COLUMN_PROGRESS] = data.Progress;
        nCMBObject[Common.COLUMN_COMMENT] = data.Comment;

        nCMBObject.SaveAsync((NCMBException e) =>
        {
            nowLoading.SetActive(false);

            if (Application.platform != RuntimePlatform.Android) return;
            if (e != null)
            {
                Toast.makeText("データの保存に失敗しました。");
            }
            else
            {
                Toast.makeText("データの保存に成功しました。");
            }
        });
    }
}
