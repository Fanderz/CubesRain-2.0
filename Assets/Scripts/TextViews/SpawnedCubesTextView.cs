public class SpawnedCubesTextView : BaseTextView<Cube>
{
    protected override void Start()
    {
        Spawner.ChangedSpawnedCounter += ChangeView;
    }
}
