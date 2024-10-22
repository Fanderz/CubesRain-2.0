public class SpawnedBombsTextView : BaseTextView<Sphere>
{
    protected override void Start()
    {
        spawner.ChangedSpawnedCounter += ChangeView;
    }
}
