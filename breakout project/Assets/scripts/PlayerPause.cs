using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    private PauseMenu pawnMenu;

    private void Start()
    {
        pawnMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        Debug.Log("Pause");
    }
    public void OnPause()
    {
        pawnMenu.TogglePause();
    }
}
