using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class FinishTrigger : MonoBehaviour
{
    public UnityAction FinishTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<BallsTrail>(out BallsTrail ballsTrail))
        {
            FinishTriggered?.Invoke();
            StartCoroutine(StartFinish(ballsTrail));
        }
    }

    private IEnumerator StartFinish(BallsTrail ballsTrail)
    {
        yield return new WaitForSeconds(0f);

        ballsTrail.BreakUp();
    }
}
