public class CreatedBombsTextView : BaseTextView<Bomb>
{
    protected override void Start()
    {
        Spawner.ChangedCreatedCounter += ChangeView;
    }
}
