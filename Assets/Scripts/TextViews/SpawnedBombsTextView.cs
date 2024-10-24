public class SpawnedBombsTextView : BaseTextView<Bomb>
{
    protected override void Start()
    {
        Spawner.ChangedSpawnedCounter += ChangeView;
    }
}
