using System;
using UnityEngine;

namespace Env
{
    public class EdgeController : MonoBehaviour
    {
        public void CollapseEdgeDestroy()
        {
            Destroy(gameObject);
        }

        public void CollapseEdgeAnimated()
        {
            throw new NotImplementedException();
        }
    }
}
