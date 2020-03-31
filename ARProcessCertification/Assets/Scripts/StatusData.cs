
public class StatusData
{
    /// <summary>
    /// 進捗
    /// </summary>
    public string Progress { get; private set; } = string.Empty;

    /// <summary>
    /// メッセージ
    /// </summary>
    public string Comment { get; private set; } = string.Empty;

    /// <summary>
    /// オブジェクトID
    /// </summary>
    public string ObjectId { get; private set; } = string.Empty;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public StatusData()
    {
        Set(string.Empty, string.Empty, string.Empty);
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="objectId"></param>
    /// <param name="progress"></param>
    /// <param name="comment"></param>
    public StatusData(string objectId, string progress, string comment)
    {
        Set(objectId, progress, comment);
    }

    /// <summary>
    /// データ設定
    /// </summary>
    /// <param name="objectId"></param>
    /// <param name="progress"></param>
    /// <param name="comment"></param>
    /// <returns></returns>
    public bool Set(string objectId, string progress, string comment)
    {
        if (string.IsNullOrEmpty(objectId)) return false;
        if (string.IsNullOrEmpty(progress)) return false;
        if (string.IsNullOrEmpty(comment)) return false;

        ObjectId = objectId;
        Progress = progress;
        Comment = comment;
        return true;
    }
}
