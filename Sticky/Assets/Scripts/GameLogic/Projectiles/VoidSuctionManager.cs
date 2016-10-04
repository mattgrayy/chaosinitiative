using UnityEngine;
using System.Collections;

public class VoidSuctionManager : MonoBehaviour
{
    public static VoidSuctionManager instance { get; private set; }

    [SerializeField] private Transform parentFolder = null; //Sorting folder for pooled enemies
    [SerializeField] private VoidSuction voidSuctionPrefab = null; //Array of different enemy types as prefabs
    private IterativeBehaviourPool<VoidSuction> voidSuctionPool = null; //Array of enemy pools for different enemy types

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            voidSuctionPool = new IterativeBehaviourPool<VoidSuction>(voidSuctionPrefab, 5, parentFolder);
        }
    }

    public VoidSuction GetPooledVoidSuction()
    {
        return voidSuctionPool.GetPooledObject();
    }
}
