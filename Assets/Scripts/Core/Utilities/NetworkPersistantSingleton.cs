namespace Core.Utilities
{
    public class NetworkPersistantSingleton<T> : NetworkSingleton<T> where T : NetworkSingleton<T>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}