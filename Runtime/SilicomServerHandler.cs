using UnityEngine;

namespace Silicom.Name
{
    public abstract class SilicomServerHandler : MonoBehaviour
    {
        public abstract void SendData(SilicomData data);
    }
}