using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    [Header("跟隨目標（拖入角色）")]
    [SerializeField] private Transform target;

    [Header("相對目標的偏移位置（如頭頂高度）")]
    [SerializeField] private Vector3 offset = new Vector3(0, 1.4f, 0);

    // 使用 LateUpdate 確保在角色移動、動畫更新完成後，才更新血條位置，避免畫面抖動
    void LateUpdate()
    {
        if (target != null)
        {
            // 將自身的世界座標直接對齊到：目標當前座標 + 偏移量
            transform.position = target.position + offset;
        }
    }

    /// <summary>
    /// 若角色是動態生成（Prefab），可透過程式碼動態指定目標
    /// </summary>
    public void SetTarget(Transform newTarget, Vector3 newOffset)
    {
        target = newTarget;
        offset = newOffset;
    }
}