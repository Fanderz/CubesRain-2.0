public class SpawnedObjectTextView : BaseView
{
    private void Start()
    {
        _spawner.ChangedSpawnedCubesCounter += ChangeCubesView;
        _spawner.ChangedSpawnedBombsCounter += ChangeBombsView;
    }
}
