namespace RunnerTT
{
    public class GameState
    {
        public State State;
        public float CurrentSpeed;

        public GameState(State state, float currentSpeed)
        {
            State = state;
            CurrentSpeed = currentSpeed;
        }
    }
    public enum State
    {
        Start,
        End,
        Game
    }
}