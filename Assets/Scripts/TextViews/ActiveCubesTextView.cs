public class ActiveCubesTextView : BaseTextView<Cube>
{
    protected override void Start()
    {
        Spawner.ChangedActiveCounter += ChangeView;
    }
}
