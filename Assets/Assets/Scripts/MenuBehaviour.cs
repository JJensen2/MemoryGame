using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {
    public void triggerMenuBehavior(int i)
    {
        if(i == 1)
        {
            Application.Quit(); 
        }
        else
        {
            SceneManager.LoadScene("Level");
        }
    }

}
