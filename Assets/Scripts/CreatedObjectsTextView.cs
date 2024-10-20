public class CreatedObjectsTextView : BaseView
{
    private void Start()
    {
        _spawner.ChangedCreatedCubesCounter += ChangeCubesView;
        _spawner.ChangedCreatedBombsCounter += ChangeBombsView;
    }
}
