using UnityEngine;
using System.Collections;

public class DogCtrl : MonoBehaviour
{
    public bool isJump = false;
    public bool isSit=false;
    private bool isSitting=false;
    public bool isTurn=false;
    private bool isTurning = false;
    public bool isGetUp=false;
    public bool isHappy=false;

    private Animator animator;

    public GameObject heartEffect;
    public float heartDuration=2.0f;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found");
        }

        if (heartEffect != null)
        {
            heartEffect.SetActive(false);
        }

        isJump = false;
    }

    void Update()
    {
        if (isJump)
        {
            TriggerJump();
            isJump = false;
        }
        else if (isSit)
        {
            TriggerSit();
            isSitting=true;
            isSit=false;
        }
        else if (isTurn)
        {
            TriggerTurn();
            isTurn=false;
        }
        else if (isGetUp)
        {
            TriggerGetUp();
            isGetUp=false;
        }
        else if (isHappy)
        {
            TriggerHappy();
            isHappy=false;
        }
    }

    public void TriggerJump()
    {
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }

    public void TriggerSit()
    {
        if (animator != null)
        {
            animator.SetTrigger("Sit");
        }
    }

    public void TriggerTurn()
    {
        if (animator != null && !isTurning)
        {
            animator.SetTrigger("Turn");

            // turn around
            StartCoroutine(TurnWithDelay(0.25f, 6.0f));

        }
    }

    public void TriggerGetUp()
    {
        if (animator != null && isSitting)
        {
            animator.SetTrigger("GetUp");
            isSitting=false;
        }
    }

    public void TriggerHappy()
    {
        if (animator != null)
        {
            animator.SetTrigger("Happy");
            StartCoroutine(ShowHeartEffect());
            isHappy=false;
        }
    }

    IEnumerator ShowHeartEffect()
    {
        if (heartEffect == null) yield break;

        heartEffect.SetActive(true);
        yield return new WaitForSeconds(heartDuration);
        heartEffect.SetActive(false);
    }    


    IEnumerator TurnWithDelay(float delay, float rotateDuration)
    {
        isTurning = true;

        // 1️⃣ 애니메이션 출력되자마자 잠깐 가만히
        yield return new WaitForSeconds(delay);

        // 2️⃣ 실제 회전
        yield return StartCoroutine(Rotate360(rotateDuration));

        isTurning = false;
        animator.SetTrigger("TurnEnd");
    }

    IEnumerator Rotate360(float duration)
    {
        float elapsed = 0f;
        float totalRotation = 360f;

        while (elapsed < duration)
        {
            float deltaRotation = (totalRotation / duration) * Time.deltaTime;
            transform.Rotate(0f, deltaRotation, 0f, Space.Self);

            elapsed += Time.deltaTime;
            yield return null;
        }
        
    }

}
