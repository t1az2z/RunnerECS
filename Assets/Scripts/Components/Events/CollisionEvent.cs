namespace RunnerTT
{
    public struct CollisionEvent
    {
        public CollisionType Type;
    }

    public enum CollisionType
    {
        Coin,
        Obstacle
    }
}