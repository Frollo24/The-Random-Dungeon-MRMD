using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public int MaxMagic = 100;
    public int currentMagic;

    public Spell fireSpell;

    public MagicBarBehaviour magicBar;

    [SerializeField] private float timeWithoutCast;
    [SerializeField] private float maxTimeWithoutCast = 2.0f;

    //Debug stuff
    [Header("Debugging options")]
    [SerializeField] private KeyCode debugKeySpellCost = KeyCode.M;
    [SerializeField] private int debugSpellCostAmt = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gameManager.playerHealth == 0)
        {
            currentMagic = MaxMagic;
            magicBar.SetMaxMagic(MaxMagic);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(debugKeySpellCost))
        {
            CastSpell(debugSpellCostAmt);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            RestoreMagic(10);
        }
#endif

        if (Input.GetMouseButtonDown(1))
        {
            CastSpell(fireSpell.spellCost);
        }

        timeWithoutCast += Time.deltaTime;
        if (timeWithoutCast >= maxTimeWithoutCast)
        {
            RestoreMagic(1);
        }
    }

    public void CastSpell(int spellCost)
    {
        if(currentMagic >= spellCost)
        {
            currentMagic -= spellCost;
            magicBar.SetMagic(currentMagic);

            timeWithoutCast = 0;

            //Generates the spell ball.
            Spell spell = Instantiate(fireSpell, transform.position + transform.forward * 1.2f, transform.rotation);
            spell.Direction = transform.forward;
        }
    }

    public void RestoreMagic(int magicAmt)
    {
        currentMagic += magicAmt;
        if(currentMagic > MaxMagic)
        {
            currentMagic = MaxMagic;
        }
        magicBar.SetMagic(currentMagic);
    }

    public void SetMagic(int magic)
    {
        currentMagic = magic;
        if (currentMagic > MaxMagic)
        {
            currentMagic = MaxMagic;
        }
        if (currentMagic < 0)
        {
            currentMagic = 0;
        }
        magicBar.SetMagic(currentMagic);
    }

    public int GetMagic()
    {
        return currentMagic;
    }
}
