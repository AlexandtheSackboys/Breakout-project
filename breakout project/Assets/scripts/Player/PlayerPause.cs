using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    private PauseMenu pawnMenu; // reference to the pause menu script

    private void Start()
    {
        pawnMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        //Debug.Log("Pause");
    }
    public void OnPause()
    {
        pawnMenu.TogglePause();
    }
}
