using UnityEngine;
using TMPro;

public class BaseTextView<T> : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected BaseSpawner<T> spawner;

    protected void Awake()
    {
        text.text = "0";
    }

    protected void ChangeView(int value)
    {
        text.text = value.ToString();
    }

    protected virtual void Start()
    {
    }
}
