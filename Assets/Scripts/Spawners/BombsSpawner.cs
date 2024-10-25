public class BombsSpawner : BaseSpawner<Bomb>
{
    public void SpawnBomb(Cube cube)
    {
        if (cube != null)
        {
            var bomb = GetObject();

            if (bomb != null)
            {
                bomb.SetPosition(cube.transform.position);

                bomb.Releasing -= Pool.Release;
                bomb.Releasing += Pool.Release;
            }
        }
    }
}
