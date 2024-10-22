public class ActiveCubesTextView : BaseTextView<Cube>
{
    protected override void Start()
    {
        spawner.ChangedActiveCounter += ChangeView;
    }
}
