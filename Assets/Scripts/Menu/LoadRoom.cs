using TMPro;
using UnityEngine;

public class LoadRoom : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private LobbyManager lobbyManager;
    
    private void OnEnable()
    {
        text.text = lobbyManager.roomName;
    }
}
