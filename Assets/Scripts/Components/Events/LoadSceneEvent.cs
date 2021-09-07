using Types;

namespace Components.Events
{
    public struct LoadSceneEvent
    {
        public SceneType SceneToLoad;
        public bool UseDarkScreen;
    }
}