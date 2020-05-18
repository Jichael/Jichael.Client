using System.Collections.Generic;
using UnityEngine;

namespace Silicom.Client
{
    public class SilicomClient : MonoBehaviour
    {

        public static SilicomClient Instance { get; private set; }

        private readonly Dictionary<string, SilicomEquipment> _equipments = new Dictionary<string, SilicomEquipment>();

        private readonly Queue<SilicomData> _dataQueue = new Queue<SilicomData>();

        [SerializeField] private SilicomServerHandler serverHandler;

        private void Awake()
        {

            if (Instance == null)
            {
                Instance = this;
                GetEquipments();
            }
            else
            {
                // TODO : what do we do in this case ?
            }


        }


        private void FixedUpdate()
        {
            // TODO : Ask for server every X, or does the server automatically send every X after a successful connexion ?
            // Do this in another thread ?
            // If done in main thread, spread the load across multiple frame ? -> careful about accumulation

            while (_dataQueue.Count > 0)
            {
                SilicomData data = _dataQueue.Dequeue();

                if (_equipments.ContainsKey(data.EquipmentName))
                {
                    _equipments[data.EquipmentName].ReceiveData(data);
                }
            }
        }


        // TODO : Find another way to do this more efficiently and less error prone
        private void GetEquipments()
        {
            SilicomEquipment[] equipments = FindObjectsOfType<SilicomEquipment>();

            for (int i = 0; i < equipments.Length; i++)
            {
                if (_equipments.ContainsKey(equipments[i].name))
                {
                    Debug.LogError("This key is already in use. This is not supported.");
                    continue;
                }

                _equipments.Add(equipments[i].name, equipments[i]);
            }
        }


        #region SilicomEquipments

        public void Initialize(SilicomData[] data)
        {
            // TODO : handle this
        }
        
        public void QueueData(SilicomData data)
        {
            _dataQueue.Enqueue(data);
        }

        public void SendData(SilicomData data)
        {
            serverHandler.SendData(data);
        }

        #endregion


    }
}