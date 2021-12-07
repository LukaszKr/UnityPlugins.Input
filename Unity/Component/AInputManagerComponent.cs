using ProceduralLevel.Common.Ext;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralLevel.UnityPlugins.Input.Unity
{
    public abstract class AInputManagerComponent<TInputManager> : MonoBehaviour
        where TInputManager : InputManager, new()
    {
        public abstract TInputManager Manager { get; }

        private void OnDestroy()
        {
            Manager.OnActiveDeviceChanged.RemoveAllListeners();
        }

        protected virtual void Update()
        {
            Manager.Update();
        }

        protected void DrawDebugGUI()
        {
            TouchDevice touchDevice = TouchDevice.Instance;
            TouchData[] touches = touchDevice.Touches;
            int touchCount = touchDevice.Count;
            for (int x = 0; x < touchCount; ++x)
            {
                TouchData touch = touches[x];
                GUILayout.Label(touch.ToString());
            }

            List<AInputProvider> providers = new List<AInputProvider>();
            Manager.RecordProviders(providers);
            if (providers.Count > 0)
            {
                string str = StringExt.JoinToString(providers);
                GUILayout.Label(str);
            }
        }
    }
}
