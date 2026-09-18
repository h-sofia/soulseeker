using UnityEngine;

public class jumpBehaviour : StateMachineBehaviour
{
    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerFight");

        if (player != null)
        {
            playerPos = player.transform;
        }

        timer = Random.Range(minTime, maxTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (playerPos != null)
        {
            Vector2 target = playerPos.position;

            animator.transform.position = Vector2.MoveTowards(
                animator.transform.position,
                target,
                speed * Time.deltaTime
            );
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}