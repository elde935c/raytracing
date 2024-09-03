namespace raytracer.Domain;

public class Scene
{
    private readonly World world;
    private readonly ViewPort viewport;

    public Scene(World world, Vector[] corners, Vector viewPoint,
        int screenWidth, int screenHeight) 
    {
        this.world = world;
        this.viewport = new ViewPort(corners, viewPoint,
            screenWidth, screenHeight, this);
    }

    public World GetWorld() { return world; }

    public ViewPort GetViewport() { return viewport; }

}
