public class ActiveBombsTextView : BaseTextView<Sphere>
{
    protected override void Start()
    {
        spawner.ChangedActiveCounter += ChangeView;
    }
}
