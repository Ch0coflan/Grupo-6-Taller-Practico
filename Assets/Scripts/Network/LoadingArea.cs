using UnityEngine;

public class LoadingArea : MonoBehaviour
{
    public static LoadingArea Instance;
    public GameObject chargePanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowLoading()
    {
        chargePanel.SetActive(true);
    }

    public void StopLoading()
    {
        chargePanel.SetActive(false);
    }
    
}
