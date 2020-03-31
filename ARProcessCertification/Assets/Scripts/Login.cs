using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NCMB;

public class Login : MonoBehaviour
{
    /// <summary>
    /// ログインID入力UI名
    /// </summary>
    private const string LOGINID_FIELD = "LoginId_Field";

    /// <summary>
    /// ログインパスワード入力UI名
    /// </summary>
    private const string LOGINPASSWORD_FIELD = "LoginPassword_Field";

    /// <summary>
    /// エラーメッセージUI名
    /// </summary>
    private const string ERROR_MESSAGE = "ErrorMessage";

    /// <summary>
    /// ローディングUI名
    /// </summary>
    private const string NOW_LOADING = "NowLoading";

    /// <summary>
    /// シーン名　従業員画面
    /// </summary>
    private const string SCENE_EMPLOYEE = "EmployeeScreen";

    /// <summary>
    /// シーン名　管理者画面
    /// </summary>
    private const string SCENE_ADMIN = "AdminScreen";

    /// <summary>
    /// ログインID
    /// </summary>
    [SerializeField] private InputField loginId = default;

    /// <summary>
    /// ログインパスワード
    /// </summary>
    [SerializeField] private InputField loginPassWord = default;

    /// <summary>
    /// エラーメッセージ
    /// </summary>
    [SerializeField] private Text errorMessage = default;

    /// <summary>
    /// ローディング表示
    /// </summary>
    [SerializeField] private GameObject NowLoading = default;

    /// <summary>
    /// なし
    /// </summary>
    private const int NONE = 0;

    /// <summary>
    /// OK
    /// </summary>
    private const int OK = 1;

    /// <summary>
    /// ロード中
    /// </summary>
    private const int LOADING = 2;

    /// <summary>
    /// NG
    /// </summary>
    private const int NG = 3;

    /// <summary>
    /// 従業員
    /// </summary>
    private const int EMPLOYEE = 1;

    /// <summary>
    /// 管理者
    /// </summary>
    private const int ADMIN = 2;

    /// <summary>
    /// 権限
    /// </summary>
    private int authority = NONE;

    /// <summary>
    /// オブジェクトインスペクタリセット時の処理
    /// </summary>
    private void Reset()
    {
        loginId = GameObject.Find(LOGINID_FIELD).GetComponent<InputField>();
        loginPassWord = GameObject.Find(LOGINPASSWORD_FIELD).GetComponent<InputField>();
        errorMessage = GameObject.Find(ERROR_MESSAGE).GetComponent<Text>();
        NowLoading = GameObject.Find(NOW_LOADING);
    }

    /// <summary>
    /// ログインボタン処理
    /// </summary>
    public void OnClickLoginButton()
    {
        loginId.readOnly = true;
        loginPassWord.readOnly = true;

        TryLogin();

        loginId.readOnly = false;
        loginPassWord.readOnly = false;
    }

    /// <summary>
    /// ログイン実行
    /// </summary>
    private void TryLogin()
    {
        string inputPass = string.Empty;
        string inputAuthoruty = string.Empty;

        NowLoading.SetActive(true);

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(Common.TABLE_USER);

        query.WhereEqualTo(Common.COLUMN_USER_ID, loginId.text);
        query.FindAsync((List<NCMBObject> objectList, NCMBException e) =>
        {
            NowLoading.SetActive(false);
            if (e != null)
            {
                StartCoroutine(ShowError());
                return;
            }
            else
            {
                foreach (NCMBObject obj in objectList)
                {
                    inputPass = obj[Common.COLUMN_PASSWORD].ToString();
                    inputAuthoruty = obj[Common.COLUMN_AUTHORITY].ToString();
                }

                // パスワード判定
                if (!inputPass.Equals(loginPassWord.text)) { StartCoroutine(ShowError()); return; }

                // 権限判定
                if (!int.TryParse(inputAuthoruty, out authority)) { StartCoroutine(ShowError()); return; }

                LoadScene(authority);
            }
        });

    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="type"></param>
    private void LoadScene(int type)
    {
        switch (type)
        {
            case EMPLOYEE: SceneManager.LoadScene(SCENE_EMPLOYEE); break;
            case ADMIN: SceneManager.LoadScene(SCENE_ADMIN); break;
            default: break;
        }
    }

    /// <summary>
    /// エラー表示
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowError()
    {
        errorMessage.text = "ログイン失敗";
        yield return new WaitForSeconds(10 * 1000);
        errorMessage.text = "";
    }
}
