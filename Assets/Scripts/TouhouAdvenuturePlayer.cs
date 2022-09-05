using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class TouhouAdvenuturePlayer : NetworkBehaviour
{

    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    public Rigidbody2D rb;
    private Vector2 pos;
    private void Start()
    {
        if (IsLocalPlayer)
        {
            if (dataStatic.Instance != null)
            {
                this.transform.position = dataStatic.Instance.position;
                Position.Value = dataStatic.Instance.position;
            }
            rb = this.gameObject.GetComponent<Rigidbody2D>();
        }
    }

   
    public void Move(Vector2 pos)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Position.Value = pos;
            move();
           
        }
        else
        {
           
            SubmitPositionRequestServerRpc(pos);
           
        }
    }

    [ServerRpc]
    void SubmitPositionRequestServerRpc(Vector2 pos, ServerRpcParams rpcParams = default)
    {
        Position.Value = pos;
        move();
    }
     void move()
    {
        transform.position = Position.Value;
    }


   

}
