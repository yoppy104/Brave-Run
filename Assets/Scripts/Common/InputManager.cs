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

    //ビームの発射操作
    public static bool BeamInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    //ビームの発射位置の取得
    public static Vector3 BeamPoint()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
