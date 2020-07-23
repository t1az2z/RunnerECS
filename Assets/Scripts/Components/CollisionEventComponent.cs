namespace RunnerTT
{
    public struct CollisionEventComponent
    {
        public CollisionType Type;
    }

    public enum CollisionType
    {
        Coin,
        Obstacle
    }
}