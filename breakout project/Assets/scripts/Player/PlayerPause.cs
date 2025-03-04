using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    private PauseMenu _pawnMenu; // reference to the pause menu script

    private void Start()
    {
        _pawnMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();

    }
    public void OnPause()
    {
        _pawnMenu.TogglePause();
    }
}
