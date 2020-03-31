using UnityEngine;
using UnityEngine.UI;

public class StatusScreen2 : MonoBehaviour, IStatusScreen
{
    /// <summary>
    /// 進捗テキストUI名
    /// </summary>
    private const string PROGRESS_FIELD = "Progress_Field";

    /// <summary>
    /// メッセージテキストUI名
    /// </summary>
    private const string COMMENT_FIELD = "Comment_Field";

    /// <summary>
    /// マネージャーオブジェクト
    /// </summary>
    private const string MANAGER = "Manager";

    /// <summary>
    /// 進捗
    /// </summary>
    [SerializeField] private InputField progressField = default;

    /// <summary>
    /// コメント
    /// </summary>
    [SerializeField] private InputField commentField = default;

    /// <summary>
    /// キャンバス
    /// </summary>
    [SerializeField] private Canvas canvas = default;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private SyncStatusData syncData = default;

    /// <summary>
    /// オブジェクトID
    /// </summary>
    private string objectId = string.Empty;

    /// <summary>
    /// オブジェクトインスペクタリセット時の処理
    /// </summary>
    private void Reset()
    {
        progressField = GameObject.Find(PROGRESS_FIELD).GetComponent<InputField>();
        commentField = GameObject.Find(COMMENT_FIELD).GetComponent<InputField>();
        canvas = GetComponentInChildren<Canvas>();
        syncData = GameObject.Find(MANAGER).GetComponent<SyncStatusData>();
    }

    /// <summary>
    /// ステータスデータの設定
    /// </summary>
    /// <param name="data"></param>
    public void SetStatus(StatusData data)
    {
        objectId = data.ObjectId;
        progressField.text = data.Progress;
        commentField.text = data.Comment;
        canvas.enabled = true;
    }

    /// <summary>
    /// ステータスデータの取得
    /// </summary>
    /// <returns></returns>
    public StatusData GetStatus()
    {
        return new StatusData(objectId, progressField.text, commentField.text);
    }

    /// <summary>
    /// 保存ボタンクリックイベント
    /// </summary>
    public void OnClickSaveButton()
    {
        syncData.SetStatusData();
    }
}
