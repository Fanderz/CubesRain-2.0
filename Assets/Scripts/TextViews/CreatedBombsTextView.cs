public class CreatedBombsTextView : BaseTextView<Sphere>
{
    protected override void Start()
    {
        spawner.ChangedCreatedCounter += ChangeView;
    }
}
