using UnityEngine;
using UnityEngine.UI;

public class StatusScreen : MonoBehaviour, IStatusScreen
{
    /// <summary>
    /// 進捗テキストUI名
    /// </summary>
    private const string PROGRESS_TEXT = "Progress_Text";

    /// <summary>
    /// メッセージテキストUI名
    /// </summary>
    private const string COMMENT_TEXT = "Comment_Text";

    /// <summary>
    /// 進捗
    /// </summary>
    [SerializeField] private Text progressText = default;

    /// <summary>
    /// コメント
    /// </summary>
    [SerializeField] private Text commentText = default;

    /// <summary>
    /// キャンバス
    /// </summary>
    [SerializeField] private Canvas canvas = default;

    /// <summary>
    /// オブジェクトインスペクタリセット時の処理
    /// </summary>
    private void Reset()
    {
        progressText = GameObject.Find(PROGRESS_TEXT).GetComponent<Text>();
        commentText = GameObject.Find(COMMENT_TEXT).GetComponent<Text>();
        canvas = GetComponentInChildren<Canvas>();
    }

    /// <summary>
    /// ステータスデータの設定
    /// </summary>
    /// <param name="data"></param>
    public void SetStatus(StatusData data)
    {
        progressText.text = data.Progress;
        commentText.text = data.Comment;
        canvas.enabled = true;
    }

    /// <summary>
    /// ステータスデータの取得
    /// </summary>
    /// <returns></returns>
    public StatusData GetStatus()
    {
        return null;
    }
}
