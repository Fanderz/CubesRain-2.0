using UnityEngine;
using TMPro;

public class BaseView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _cubesText;
    [SerializeField] protected TextMeshProUGUI _bombsText;
    [SerializeField] protected Spawner _spawner;

    private void Awake()
    {
        _cubesText.text = "0";
        _bombsText.text = "0";
    }

    protected void ChangeCubesView(int value)
    {
        _cubesText.text = value.ToString();
    }

    protected void ChangeBombsView(int value)
    {
        _bombsText.text = value.ToString();
    }
}
