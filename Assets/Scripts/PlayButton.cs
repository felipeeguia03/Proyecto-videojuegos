using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void PlayButtonManager(){
        SceneManager.LoadScene("SampleScene");
    }

}
