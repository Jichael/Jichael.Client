using UnityEngine;

namespace Silicom.Client
{
    public abstract class SilicomServerHandler : MonoBehaviour
    {
        public abstract void SendData(SilicomData data);
    }
}