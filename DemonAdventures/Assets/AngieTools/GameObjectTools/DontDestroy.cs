using UnityEngine;

namespace AngieTools.GameObjectTools
{
    public class DontDestroy : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
