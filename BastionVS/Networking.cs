using UnityEngine;
using UnityEngine.Networking;
using R2API.Networking.Interfaces;

namespace Bastian
{
    public class Networking
    {
        public class SyncFillCharge : INetMessage
        {
            private float charge;
            private GameObject bodyObject;

            public SyncFillCharge()
            {
            }
            public SyncFillCharge(float charge_, GameObject bodyObject_)
            {
                charge = charge_;
                bodyObject = bodyObject_;
            }

            public void OnReceived()
            {
                bodyObject.GetComponent<BlastDamageBuildupController>().SyncCheckBuffs(charge);
            }

            public void Deserialize(NetworkReader reader)
            {
                charge = reader.ReadSingle();
                bodyObject = reader.ReadGameObject();
            }

            public void Serialize(NetworkWriter writer)
            {
                writer.Write(charge);
                writer.Write(bodyObject);
            }
        }
        public class SyncResetCharge : INetMessage
        {
            private GameObject bodyObject;

            public SyncResetCharge()
            {
            }
            public SyncResetCharge(GameObject bodyObject_)
            {
                bodyObject = bodyObject_;
            }

            public void OnReceived()
            {
                bodyObject.GetComponent<BlastDamageBuildupController>().SyncResetCharge();
            }

            public void Deserialize(NetworkReader reader)
            {
                bodyObject = reader.ReadGameObject();
            }

            public void Serialize(NetworkWriter writer)
            {
                writer.Write(bodyObject);
            }
        }
    }
}
