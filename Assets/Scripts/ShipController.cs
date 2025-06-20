using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    public bool isDead = false;
    public float moveSpeed = 5f;
    public float rotationSpeed = 1f;
    public Transform leftBoundary;
    public Transform rightBoundary;
    public UnityEvent OnHitByTorpedo; // Событие при попадании торпеды
    


    private Animator animator;
    private bool isMoving = true;
    private bool movingRight = true;

    const string TRIGGER_NAME = "Sink";


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving)
            return;

        // Движение корабля из стороны в сторону
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightBoundary.position, moveSpeed * Time.deltaTime);

            if (transform.position == rightBoundary.position)
            {
                StartCoroutine(TurnAround());
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftBoundary.position, moveSpeed * Time.deltaTime);

            if (transform.position == leftBoundary.position)
            {
                StartCoroutine(TurnAround());
                movingRight = true;
            }
        }
    }

    void Death() 
    {
        isMoving = false;
        animator.SetTrigger(TRIGGER_NAME);
        StartCoroutine(checkDeath());
    }

    IEnumerator checkDeath() 
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        isDead = true;
    }
    private IEnumerator TurnAround()
    {
        float rotationAmount = 0f;
        while (rotationAmount < 180f)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, step, 0f);
            rotationAmount += step;
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TorpedoController controller))
        {
            OnHitByTorpedo.Invoke(); // Выполняем событие попадания торпеды

            controller.Explosion();

            Death();
        }
    }
}
