using UnityEngine;
using System.Collections;

public class gameover : MonoBehaviour {
    private float waittime = 0.0f;
    [SerializeField] private float delay = 20f;
    private float alphaval = 0;
    public bool GO = false;
    [SerializeField]private SpriteRenderer GOblackout;

    [SerializeField] private outcome outcme;


    // Use this for initialization
    void Start () {
        
       // GOblackout.material.color = Color.clear;
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameOver();
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        StartCoroutine("DeathAnimation");
    }

    void GameOver()
    {
        if(GO)
        {
            waittime += Time.deltaTime;
            GOblackout.color = new Color(0, 0, 0, waittime/delay);
            if (waittime >= delay)
            {
                Application.LoadLevel(2);
                if (alphaval < 1)
                {
                    alphaval += 0.01f;
                }

                else if (alphaval >= 1)
                {
                    
                }
            }
        }
        
    }
    IEnumerator DeathAnimation()
    {
        outcme.getEnemyKillCount();
        ParticleEffect _par = ParticleManager.instance.GetParticle(0);
        _par = ParticleManager.instance.GetParticle(0);
        CameraShake2.instance.ShakeCamera(3.0f, 0.1f);
        _par.transform.position = new Vector3(-7.0f, -4.5f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(-6.0f, -4.3f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(-5.0f, -4.4f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(-4.0f, -4.5f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(-3.0f, -4.4f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(-2.0f, -4.7f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(-1.0f, -4.6f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(0.0f, -4.5f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(1.0f, -4.3f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(2.0f, -4.5f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(3.0f, -4.4f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(4.0f, -4.7f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(5.0f, -4.5f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(6.0f, -4.4f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        _par.transform.position = new Vector3(7.0f, -4.3f, 0.0f);
        _par.Trigger();
        GlobalSoundManager.instance.PlaySoundEffect(Random.Range(0, 5), Vector3.zero, 0.25f);
        yield return new WaitForSeconds(0.1f);
        GO = true;
    }
 }
