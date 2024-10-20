public class ActiveObjectsTextView : BaseView
{
    private void Start()
    {
        _spawner.ChangedActiveCubesCounter += ChangeCubesView;
        _spawner.ChangedActiveBombsCounter += ChangeBombsView;
    }
}
