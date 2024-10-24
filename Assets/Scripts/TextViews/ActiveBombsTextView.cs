public class ActiveBombsTextView : BaseTextView<Bomb>
{
    protected override void Start()
    {
        Spawner.ChangedActiveCounter += ChangeView;
    }
}
