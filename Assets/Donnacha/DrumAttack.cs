using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrumAttack : MonoBehaviour
{

    [SerializeField] int beatPoint = 0;
    [SerializeField] GameObject hitBox;
    [SerializeField] SpriteRenderer aoe;
    private Color heldColor;
    private EnemyMovement myMover;

    private int mode = 0;
    public List<float> speeds;

    private void Start()
    {
        heldColor = aoe.color;
    }

    public void Attack()//drumbeat
    {
        if(myMover == null)
            myMover = GetComponent<EnemyMovement>();

        switch (beatPoint)
        {
            case (0):

                myMover.myMode = EnemyMovement.EnemyMode.MeleeRange;
                GetComponent<NavMeshAgent>().speed *= 1.5f;

                beatPoint++;
                break;
            case (2):
                myMover.enemyArt.GetComponent<Animator>().SetBool("Attacking", true);
                aoe.gameObject.SetActive(true);
                aoe.color = heldColor;

                beatPoint++;
                break;
            case (3):

                myMover.myMode = EnemyMovement.EnemyMode.Fire;
                hitBox.SetActive(true);
                aoe.color = Color.red;

                Invoke(nameof(HideCollider), BeatSender.GiveInstance().secondsPerBeat / 2);

                beatPoint = 0;
                break;
            default:
                beatPoint++;
                break;
        }

    }

    private void HideCollider()
    {
        hitBox.SetActive(false);
        aoe.color = heldColor;
        aoe.gameObject.SetActive(false);
        myMover.myMode = EnemyMovement.EnemyMode.Chase;
        myMover.enemyArt.GetComponent<Animator>().SetBool("Attacking", false);
        GetComponent<EnemyFire>().readyToFire = false;
        GetComponent<NavMeshAgent>().speed = speeds[mode];

    }

    private void Update()
    {

        if (!GetComponent<EnemyFire>().readyToFire)
        {

            mode = Mathf.RoundToInt( FindObjectOfType<Gun_Script>().comboLevels[0]);
            GetComponent<NavMeshAgent>().speed = speeds[mode];

        }

    }

}
