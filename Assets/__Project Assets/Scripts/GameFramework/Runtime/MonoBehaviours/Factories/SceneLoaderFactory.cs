namespace GameFramework
{
    using d4160.SceneManagement;

    public class SceneLoaderFactory : DefaultSceneLoaderFactory
    {
        public override IAsyncSceneLoader Fabricate(int option = 0)
        {
            switch((SceneLoaderType)option)
            {
                case SceneLoaderType.UnityBuiltIn:
                    return new DefaultEmptySceneLoader();
                case SceneLoaderType.UniTask:
#if UNI_TASK
                    return new UniTaskEmptySceneLoader();
#endif
                case SceneLoaderType.Addressables:
                    return default;
                case SceneLoaderType.PUN:
                    return new PunSceneLoader();
                default: return default;
            }
        }
    }
}