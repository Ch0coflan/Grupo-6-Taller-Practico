using TMPro;
using UnityEngine;

public class LoadRoom : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_InputField inputField;
    
    private void OnEnable()
    {
        text.text = inputField.text;
    }
}
