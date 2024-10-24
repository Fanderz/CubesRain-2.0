public class CreatedCubesTextView : BaseTextView<Cube>
{
    protected override void Start()
    {
        Spawner.ChangedCreatedCounter += ChangeView;
    }
}
