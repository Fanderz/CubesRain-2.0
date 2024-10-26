using UnityEngine;
using TMPro;

public class BaseTextView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI SpawnedView;
    [SerializeField] protected TextMeshProUGUI CreatedView;
    [SerializeField] protected TextMeshProUGUI ActiveView;
    [SerializeField] protected BaseSpawner<T> Spawner;

    protected void OnEnable()
    {
        Spawner.ChangedSpawnedCounter += ChangeSpawnedView;
        Spawner.ChangedCreatedCounter += ChangeCreatedView;
        Spawner.ChangedActiveCounter += ChangeActiveView;
    }

    protected void OnDisable()
    {
        Spawner.ChangedSpawnedCounter -= ChangeSpawnedView;
        Spawner.ChangedCreatedCounter -= ChangeCreatedView;
        Spawner.ChangedActiveCounter -= ChangeActiveView;
    }

    protected void Start()
    {
        SpawnedView.text = "0";
        CreatedView.text = "0";
        ActiveView.text = "0";
    }

    protected void ChangeSpawnedView(int value)
    {
        SpawnedView.text = value.ToString();
    }

    protected void ChangeCreatedView(int value)
    {
        CreatedView.text = value.ToString();
    }

    protected void ChangeActiveView(int value)
    {
        ActiveView.text = value.ToString();
    }
}
