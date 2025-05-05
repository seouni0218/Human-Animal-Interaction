using UnityEngine;

public class Touching : MonoBehaviour
{
    public Animator animator; // Animator를 Inspector에서 설정해 주세요
    public string touchingParameter = "isTouching"; // isTouching 파라미터 이름 (Trigger 파라미터로 사용)
    private string[] targetParameters = { "isWalking", "isEating", "isSitting", "isEnd", "isPlaying" }; // 검사할 특정 파라미터 이름들

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator가 설정되지 않았습니다.");
            return;
        }

        if (AreAllTargetParametersFalse())
        {
            Debug.Log("모든 지정된 파라미터가 false입니다. isTouching 트리거를 설정합니다.");
            SetIsTouching();
            animator.SetBool(touchingParameter, false);
        }
        else
        {
            Debug.Log("지정된 파라미터 중 하나 이상이 true입니다.");
        }
    }

    public bool AreAllTargetParametersFalse()
    {
        foreach (string parameter in targetParameters)
        {
            if (animator.GetBool(parameter))
            {
                return false;
            }
        }
        return true;
    }

    public void SetIsTouching()
    {
        animator.SetTrigger(touchingParameter);
        //animator.SetBool(touchingParameter, true);
        //animator.CrossFade("isTouching", 0.1f);
    }
}
