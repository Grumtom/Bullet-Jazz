using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrumAttack : MonoBehaviour
{

    [SerializeField] int beatPoint = 0;
    [SerializeField] GameObject hitBox;
    private EnemyMovement myMover;

    public void Attack()//drumbeat
    {
        if(myMover == null)
            myMover = GetComponent<EnemyMovement>();

        switch (beatPoint)
        {
            case (0):

                myMover.myMode = EnemyMovement.EnemyMode.MeleeRange;

                beatPoint++;
                break;
            case (2):

                myMover.myMode = EnemyMovement.EnemyMode.Fire;
                myMover.enemyArt.GetComponent<Animator>().SetTrigger("StartUp");

                beatPoint++;
                break;
            case (3):

                hitBox.SetActive(true);

                Invoke(nameof(HideCollider), BeatSender.GiveInstance().secondsPerBeat);

                beatPoint = 0;
                break;
            default:
                beatPoint++;
                break;
        }

    }

    private void HideCollider()
    {
        print("Triggered end of animation");
        hitBox.SetActive(false);
        myMover.myMode = EnemyMovement.EnemyMode.Chase;
        myMover.enemyArt.GetComponent<Animator>().SetTrigger("Return");
        GetComponent<EnemyFire>().readyToFire = false;

    }

}
