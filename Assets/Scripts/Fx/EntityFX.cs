using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EntityFX : MonoBehaviour
{
    protected Player player;
    protected SpriteRenderer sr;

    public GameObject yarabandi;

    [Header("Critical Attack Fx")]
    [SerializeField] private GameObject powFx;

    [Header("Emotes FX")]
    public GameObject[] emotes;

    [Header("Gun FX")]
    [SerializeField] Transform gunTransform;
    [SerializeField] private GameObject gunShotFx;
    [SerializeField] private GameObject bulletFx;


    [Header("Pop Up Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;

    [Header("Hit FX")]
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject criticalHitFx;

    [SerializeField] private ParticleSystem dustFx;


    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;
        originalMat = sr.material;
    }
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }

    #region  Create FX 

    public void CreatePopUpText(string _text)
    {
        float randomX = Random.Range(-.5f, .5f);
        float randomY = Random.Range(.5f, 1f);

        Vector3 positionOffset = new Vector3(randomX, randomY, 0);

        GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);
        newText.GetComponentInChildren<TextMeshPro>().text = _text;
    }

    public void CreateEmotesOfChance(int _emoteNumber, Transform _target)
    {
        float chance = Random.Range(0, 100);
        float randomX = Random.Range(-.5f, .5f);
        float randomY = Random.Range(.5f, 1f);

        Vector3 positionOffset = new Vector3(randomX, randomY, 0);

        if (chance > Random.Range(60, 100))
        {
            GameObject newEmote = Instantiate(emotes[_emoteNumber], transform.position + positionOffset, Quaternion.identity);
            newEmote.transform.SetParent(_target.transform);
        }
    }

    public void CreateEmotes(int _emoteNumber, Transform _target)
    {
        float randomX = Random.Range(-.5f, .5f);
        float randomY = Random.Range(.5f, 1f);

        Vector3 positionOffset = new Vector3(randomX, randomY, 0);
        GameObject newEmote = Instantiate(emotes[_emoteNumber], transform.position + positionOffset, Quaternion.identity);
        newEmote.transform.SetParent(_target.transform);

    }

    public void CreateHitFX(Transform _target, bool _critical)
    {
        float zRot = Random.Range(-90, 90);
        float xPos = Random.Range(-.5f, .5f);
        float yPos = Random.Range(-1f, .5f);

        Vector3 hitFxRot = new Vector3(0, 0, zRot);

        GameObject hitPrefab = hitFx;

        if (_critical)
        {
            hitPrefab = criticalHitFx;

            float yRotation = 90;

            if (GetComponent<Entity>().facingDir == -1)
                yRotation = -90;

            hitFxRot = new Vector3(0, yRotation, zRot);

        }

        GameObject newHitFx = Instantiate(hitPrefab, _target.position + new Vector3(xPos, yPos), Quaternion.identity);

        newHitFx.transform.Rotate(hitFxRot);
        Destroy(newHitFx, .5f);
    }

    public void CreateBullet()
    {
        float yRotation = 0;
        Vector3 bulletRot = new Vector3(0, yRotation, 0);


        GameObject muzzleFx = Instantiate(gunShotFx, gunTransform.position, Quaternion.identity);
        muzzleFx.transform.parent = gunTransform;
        Destroy(muzzleFx, .2f);

        if (GetComponent<Entity>().facingDir == -1)
            yRotation = 180;

        bulletRot = new Vector3(0, yRotation, 0);
        GameObject bullet = Instantiate(bulletFx, gunTransform.position, Quaternion.identity);
        bullet.transform.Rotate(bulletRot);
    }

    public void CreateCriticalAttack()
    {
        GameObject newPowFx = Instantiate(powFx, transform.position, Quaternion.identity);
        Destroy(newPowFx, 1.2f);
    }

    #endregion

    public void PlayDustFX()
    {
        if (dustFx != null)
            dustFx.Play();
    }
    public void StopDustFX()
    {
        if (dustFx != null)
            dustFx.Stop();
    }
}
