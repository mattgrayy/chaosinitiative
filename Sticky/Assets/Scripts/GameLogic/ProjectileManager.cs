using UnityEngine;
using System.Collections;
//Replace all enemies with projectiles
public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance { get; private set; }

    [SerializeField] private Transform parentFolder = null;

    [SerializeField] private Enemy /*ProjPrefab1*/ basicProjPrefab = null;
    [SerializeField] private Enemy /*ProjPrefab1*/ bounceShockProjPrefab = null;
    [SerializeField] private Enemy /*ProjPrefab1*/ bounceDamageProjPrefab = null;
    [SerializeField] private Enemy /*ProjPrefab1*/ voidProjPrefab = null;

    private IterativeBehaviourPool<Enemy/*...ProjType1...*/> basicProjsPool;
    private IterativeBehaviourPool<Enemy/*...ProjType1...*/> bounceShockProjsPool;
    private IterativeBehaviourPool<Enemy/*...ProjType1...*/> bounceDamageProjsPool;
    private IterativeBehaviourPool<Enemy/*...ProjType1...*/> voidProjsPool;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            basicProjsPool = new IterativeBehaviourPool<Enemy>(basicProjPrefab, 5, parentFolder);
            bounceShockProjsPool = new IterativeBehaviourPool<Enemy>(bounceShockProjPrefab, 5, parentFolder);
            bounceDamageProjsPool = new IterativeBehaviourPool<Enemy>(bounceDamageProjPrefab, 5, parentFolder);
            voidProjsPool = new IterativeBehaviourPool<Enemy>(voidProjPrefab, 5, parentFolder);
        }
    }

    public Enemy GetPooledProjectile(ProjType _type)
    {
        switch(_type)
        {
            case ProjType.BASIC:
                return basicProjsPool.GetPooledObject();
            case ProjType.BOUNCE_SHOCK:
                return bounceShockProjsPool.GetPooledObject();
            case ProjType.BOUNCE_DAMAGE:
                return bounceDamageProjsPool.GetPooledObject();
            case ProjType.VOID:
                return voidProjsPool.GetPooledObject();
            default:
                return null;
        }
    }
}
