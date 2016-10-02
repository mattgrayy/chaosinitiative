using UnityEngine;
using System.Collections;
//Replace all enemies with projectiles
public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance { get; private set; }

    [SerializeField] private Transform parentFolder = null; //Sorting folder for pooled projectiles

    //Prefabs for each projectile type
    [SerializeField] private BasicProjectile basicProjPrefab = null;
    [SerializeField] private BasicProjectile bounceKnockProjPrefab = null;
    [SerializeField] private BasicProjectile bounceDamageProjPrefab = null;
    [SerializeField] private VoidProjectile voidProjPrefab = null;

    //Object pools for each projectile type
    private IterativeBehaviourPool<BasicProjectile> basicProjsPool;
    private IterativeBehaviourPool<BasicProjectile> bounceKnockProjsPool;
    private IterativeBehaviourPool<BasicProjectile> bounceDamageProjsPool;
    private IterativeBehaviourPool<VoidProjectile> voidProjsPool;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            basicProjsPool = new IterativeBehaviourPool<BasicProjectile>(basicProjPrefab, 5, parentFolder);
            bounceKnockProjsPool = new IterativeBehaviourPool<BasicProjectile>(bounceKnockProjPrefab, 5, parentFolder);
            bounceDamageProjsPool = new IterativeBehaviourPool<BasicProjectile>(bounceDamageProjPrefab, 5, parentFolder);
            voidProjsPool = new IterativeBehaviourPool<VoidProjectile>(voidProjPrefab, 5, parentFolder);
        }
    }

    //Given a projectile type will get a pooled projectile of that type
    public BasicProjectile GetPooledProjectile(ProjType _type)
    {
        switch(_type)
        {
            case ProjType.BASIC:
                return basicProjsPool.GetPooledObject();
            case ProjType.BOUNCE_KNOCK:
                return bounceKnockProjsPool.GetPooledObject();
            case ProjType.BOUNCE_DAMAGE:
                return bounceDamageProjsPool.GetPooledObject();
            case ProjType.VOID:
                return voidProjsPool.GetPooledObject();
            default:
                return null;
        }
    }
}
