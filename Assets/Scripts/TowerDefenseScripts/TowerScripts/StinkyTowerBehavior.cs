using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Search;
#endif
using UnityEngine;
using UnityEditor;

public class StinkyTowerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    // HP cost consumed when placement is confirmed.
    [SerializeField, Min(0)] private int _healthCost = 2;

    // Read by DragController during confirm.
    public int HealthCost => _healthCost;

    void OnMouseEnter()
    {
        // Hover feedback only; does not affect drag state.
        if (_highlight != null)
            _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        // Hide hover feedback when pointer leaves.
        if (_highlight != null)
            _highlight.SetActive(false);
    }

    //start of turret gameplay
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 2f; //attacks per second
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;

    private void Update()
    {

        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }
#endif

}
