public interface IObstacleCounter
{
    int Count(GridManager gridManager);
}

public class BoxCounter : IObstacleCounter
{
    public int Count(GridManager gridManager)
    {
        int count = 0;
        for (int x = 0; x < gridManager.levelData.grid_width; x++)
        {
            for (int y = 0; y < gridManager.levelData.grid_height; y++)
            {
                if (gridManager.grid[x, y] != null)
                {
                    if (gridManager.grid[x, y].name == "box(Clone)")
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
}

public class StoneCounter : IObstacleCounter
{
    public int Count(GridManager gridManager)
    {
        int count = 0;
        for (int x = 0; x < gridManager.levelData.grid_width; x++)
        {
            for (int y = 0; y < gridManager.levelData.grid_height; y++)
            {
                if (gridManager.grid[x, y] != null)
                {
                    if (gridManager.grid[x, y].name == "stone(Clone)")
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
}

public class VaseCounter : IObstacleCounter
{
    public int Count(GridManager gridManager)
    {
        int count = 0;
        for (int x = 0; x < gridManager.levelData.grid_width; x++)
        {
            for (int y = 0; y < gridManager.levelData.grid_height; y++)
            {
                if (gridManager.grid[x, y] != null)
                {
                    if (gridManager.grid[x, y].name == "vase_01(Clone)")
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
}
