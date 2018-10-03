using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    protected NavMeshAgent agent;

    [SerializeField]
    protected Color baseColor = Color.blue;
    [SerializeField]
    protected Color taggedColor = Color.red;

    protected MeshRenderer renderer;

    public delegate void OnActorTagged(bool val);
    public OnActorTagged onActorTagged;

    public bool IsTagged
    {
        get; protected set;
    }

    public int touchCount
    {
        get; protected set;
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    protected virtual void Awake()
    {
        FindObjectOfType<GameController>().onGameEnd += Stop;
    }

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        renderer = GetComponent<MeshRenderer>();

        SetTouched(false);

        onActorTagged += SetTouched;
    }

    protected virtual void OnDestroy()
    {
        agent = null;
        renderer = null;
        onActorTagged -= SetTouched;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            print("collided!");

            otherActor.onActorTagged(true);

            onActorTagged(false);
        }
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        agent.SetDestination(GetTargetLocation());
    }

    private void SetTouched(bool val)
    {
        IsTagged = val;

        if (IsTagged)
        {
            touchCount++;

            FindObjectOfType<GameController>().SetCurrentPlayerTouched(this);
        }

        if (renderer)
        {
            print(string.Format("Changing color to {0}", gameObject.name));

            renderer.material.color = val ? taggedColor : baseColor;
        }
    }

    private void Stop()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (agent != null)
            agent.enabled = false;

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.isKinematic = true;
    }
}