public class BombsSpawner : BaseSpawner<Bomb>
{
    public void SpawnBomb(Cube cube)
    {
        cube.SpawningBomb -= SpawnBomb;

        if (cube != null)
        {
            var bomb = GetObject();

            if (bomb != null)
            {
                bomb.SetPosition(cube.transform.position);

                Spawn(bomb);
            }
        }
    }

    protected override void Spawn(Bomb bomb)
    {
        bomb.Releasing += Release;

        base.Spawn(bomb);
    }

    protected override void Release(Bomb bomb)
    {
        bomb.Releasing -= Release;

        base.Release(bomb);
    }
}
