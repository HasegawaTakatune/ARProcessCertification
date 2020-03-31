using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ARTraking : MonoBehaviour
{

    /// <summary>
    /// 表示するオブジェクト
    /// </summary>
    [SerializeField] private GameObject showObject = default;

    /// <summary>
    /// 1度のみ実行
    /// </summary>
    private bool doOnce = false;

    /// <summary>
    /// 検出されたマーカーイメージを格納する
    /// </summary>
    private List<AugmentedImage> augmentedImages = new List<AugmentedImage>();

    /// <summary>
    /// ループ
    /// </summary>
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking) return;

        Session.GetTrackables<AugmentedImage>(augmentedImages, TrackableQueryFilter.Updated);

        foreach (AugmentedImage image in augmentedImages)
        {
            if (image.TrackingState != TrackingState.Tracking) return;

            if (!doOnce)
            {
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                showObject.transform.parent = anchor.transform;
            }

            showObject.transform.position = image.CenterPose.position;
            showObject.transform.rotation = image.CenterPose.rotation;
        }
    }
}
