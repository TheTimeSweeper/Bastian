using UnityEngine;

namespace Bastian
{
    public class ClassicMenuSoundBehaviour: MonoBehaviour
    {
        void Awake()
        {
            if (Configs.Personality.Value)
            {
                AkSoundEngine.PostEvent(Sounds.Play_Bastian_Display, base.gameObject);
            }
        }
    }
}
