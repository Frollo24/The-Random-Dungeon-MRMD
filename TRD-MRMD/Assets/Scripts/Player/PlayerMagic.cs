using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public int maxMagic = 100;
    public int currentMagic;

    public Spell fireSpell;

    public MagicBarBehaviour magicBar;

    //Debug stuff
    [Header("Debugging options")]
    [SerializeField] private KeyCode debugKeySpellCost = KeyCode.M;
    [SerializeField] private int debugSpellCostAmt = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gameManager.playerHealth == 0)
        {
            currentMagic = maxMagic;
            magicBar.SetMaxMagic(maxMagic);
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

    }

    public void CastSpell(int spellCost)
    {
        if(currentMagic >= spellCost)
        {
            currentMagic -= spellCost;
            magicBar.SetMagic(currentMagic);

            //Generates the spell ball.
            Spell spell = Instantiate(fireSpell, transform.position + transform.forward * 1.2f, transform.rotation);
            spell.direction = transform.forward;
        }
    }

    public void RestoreMagic(int magicAmt)
    {
        currentMagic += magicAmt;
        if(currentMagic > maxMagic)
        {
            currentMagic = maxMagic;
        }
        magicBar.SetMagic(currentMagic);
    }

    public void SetMagic(int magic)
    {
        currentMagic = magic;
        magicBar.SetMagic(currentMagic);
    }

    public int GetMagic()
    {
        return currentMagic;
    }
}
