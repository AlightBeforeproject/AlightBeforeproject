using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HealthBarPlayer : MonoBehaviour {

    public Image currentHealthbar;
    public Text ratioText;

    GameObject Player;
    PlayerComand deusExiste;

    public float hitpoint = 150f;
    private float maxHitpoint = 150f;

    public void Start()
    {
        if (SaveLoadGame.instance.deuLoad == true)
        {
            hitpoint = SaveLoadGame.instance.recebeVida;
        }
        Player = GameObject.FindGameObjectWithTag("Player");       
        deusExiste = Player.gameObject.GetComponent<PlayerComand>();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float ratio = hitpoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString() + "%";
    }

    public void TakeDamage(float damage)
    {
        hitpoint -= damage;
        UpdateHealthBar();        
    }

    public void HealDamage(float heal)
    { 
        hitpoint += heal;
        UpdateHealthBar();
    }

    public float getMaxPoints()
    {
        return maxHitpoint;
    }

    public float GetHitpoint()
    {
        return hitpoint;
    }

}
