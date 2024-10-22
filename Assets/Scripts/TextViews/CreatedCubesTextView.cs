public class CreatedCubesTextView : BaseTextView<Cube>
{
    protected override void Start()
    {
        spawner.ChangedCreatedCounter += ChangeView;
    }
}
