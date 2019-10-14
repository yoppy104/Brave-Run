using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene(ConstNumbers.SCENE_NAME_TITLE);
    }

    public void OnClickGameButton()
    {
        SceneManager.LoadScene(ConstNumbers.SCENE_NAME_GAME);
    }

}
