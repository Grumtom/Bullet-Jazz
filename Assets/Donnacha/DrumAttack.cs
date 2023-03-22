using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumAttack : MonoBehaviour
{

    [SerializeField] int beatPoint = 0;
    [SerializeField] GameObject collider;
    [SerializeField] float hitBoxTimer;

    public void Attack()//drumbeat
    {

        switch (beatPoint)
        {
            case (0):

                GetComponent<EnemyMovement>().myMode = EnemyMovement.EnemyMode.MeleeRange;

                beatPoint++;
                break;
            case (1):

                GetComponent<EnemyMovement>().myMode = EnemyMovement.EnemyMode.Fire;

                beatPoint++;
                break;
            case (2):

                collider.SetActive(true);
                Invoke(nameof(HideCollider), hitBoxTimer);

                beatPoint = 0;
                break;
        }

    }

    private void HideCollider()
    {
        collider.SetActive(false);
        GetComponent<EnemyMovement>().myMode = EnemyMovement.EnemyMode.Chase;
    }

}
