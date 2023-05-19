namespace SpaceGame.Weapons
{
	public interface IDestroyedAfterDelay
    {
        public float TimeBeforeDestroyed { get; }
        public float TimeSinceSpawn { get; }

        void Destroy();
    }
}
