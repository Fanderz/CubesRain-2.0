using UnityEngine;
using TMPro;

public class BaseTextView<T> : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI View;
    [SerializeField] protected BaseSpawner<T> Spawner;

    protected void Awake()
    {
        View.text = "0";
    }

    protected void ChangeView(int value)
    {
        View.text = value.ToString();
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnDisable()
    {
        Spawner.ChangedSpawnedCounter -= ChangeView;
        Spawner.ChangedActiveCounter -= ChangeView;
        Spawner.ChangedCreatedCounter -= ChangeView;
    }
}
