using UnityEngine;

namespace CommandTest
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private Transform player = null;

        [SerializeField] private Transform ghost = null;

        public bool IsPressed
        {
            get
            {
                return DistanceUtil.IsNear(transform, player) || DistanceUtil.IsNear(transform, ghost);
            }
        }
    }
}
