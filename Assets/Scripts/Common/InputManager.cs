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
}
