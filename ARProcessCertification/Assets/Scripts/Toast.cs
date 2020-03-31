using UnityEngine;

public static class Toast
{

    public static void makeText(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
         {
             AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
             AndroidJavaObject toast = toastClass.CallStatic<AndroidJavaObject>("makeText", context, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
         }));
    }

}
