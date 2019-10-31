using UnityEngine;
/// <summary>
/// キー入力を管理するクラス。
/// 多くのクラスに分散させると齟齬が起きる場合があるため、集約管理し、これのメソッドを呼び出す。
/// </summary>
public class InputManager
{
    //ジャンプのキー操作
    public static bool JampInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    //通常弾の発射操作
    public static bool BeamInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    //特殊弾の発射操作
    public static bool SpecialBeamInput()
    {
        return Input.GetMouseButtonDown(1);
    }

    //通常弾の発射位置の取得
    public static Vector3 BeamPoint()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }


    public static void KillGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
            #endif
        }
    }
}
