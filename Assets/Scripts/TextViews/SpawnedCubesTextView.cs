public class SpawnedCubesTextView : BaseTextView<Cube>
{
    protected override void Start()
    {
        spawner.ChangedSpawnedCounter += ChangeView;
    }
}
