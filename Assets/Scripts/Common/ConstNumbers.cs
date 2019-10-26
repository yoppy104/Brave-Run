/// <summary>
/// Globalの定数値を管理する
/// Privateにするべき数値はここには入れない
/// </summary>
public class ConstNumbers
{
    /*
     template : 
     public const (型名) (変数名) = (数値);
     */

    //定数値 
    public const int GRAVITY_POWER = 20;

    //Tagの名前
    public const string TAG_NAME_STAGE = "Stage";
    public const string TAG_NAME_PLAYER = "Player";
    public const string TAG_NAME_MAGIC = "Magic";
    public const string TAG_NAME_ENEMY = "Enemy";
    public const string TAG_NAME_GAME_AREA = "GameArea";
    public const string TAG_NAME_GOAL_AREA = "GoalArea";
    public const string TAG_NAME_ITEM = "Item";

    //Sceneの名前
    public const string SCENE_NAME_TITLE = "Title";
    public const string SCENE_NAME_GAME = "Game";
    public const string SCENE_NAME_CLEAR = "Clear";
    public const string SCENE_NAME_GAMEOVER = "GameOver";

    //Animationトリガーの名前
    public const string ANIMATION_FLAG_NAME_JUMP = "isJump";
    public const string ANIMATION_FLAG_NAME_ATTACK = "isAttack";
    public const string ANIMATION_FLAG_NAME_DAMAGE = "isDamage";

}
