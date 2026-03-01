using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class StinkyTowerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    // HP cost consumed when placement is confirmed.
    [SerializeField, Min(0)] private int _healthCost = 2;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite stillSprite;
    [SerializeField] private Sprite stinkSprite;
    private int level = 1;
    [SerializeField] private int baseUpgradeCost = 2;

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
        CloseUpgradeUI();
    }

    //start of turret gameplay
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 2f; //attacks per second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    private StateManager SanityValue;
    private float targetingRangeBase;

    private float timeUntilFire;


     private void Start()
    {
        targetingRangeBase = targetingRange;
        GameObject stateManager = GameObject.FindWithTag("HealthBar");
        SanityValue = stateManager.GetComponent<StateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update()
    {

        timeUntilFire += Time.deltaTime;

        if(timeUntilFire >= 1f / aps)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0)
        {
            spriteRenderer.sprite = stinkSprite;
            for(int i=0; i<hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
        else
        {
           spriteRenderer.sprite = stillSprite; 
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }
    
    private void OnDrawGizmosSelected()
    {
        
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }
// start upgrade section
    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }

    public void Upgrade()
    {
        SanityValue.SpendSanity(CalculateCost());
        level++;
        //bps = CalculateBps();
        targetingRange = CalculateRange(); 

        CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateRange()
    {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }






    void OnMouseDown()
    {
        if (this.CompareTag("IsUpgradable"))
        {
            this.OpenUpgradeUI();
        }
    }

    

}
