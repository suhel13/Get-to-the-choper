using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    [SerializeField] float pelletsdAngle;
    [SerializeField] float velocityVariation;
    [SerializeField] float angleVariation;
    protected override void SpawnBullets(float damage, float speed)
    {
        for (int i = 0; i < pelets; i++)
        {

            tempBulletGO = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            tempBulletGO.GetComponent<Rigidbody2D>().velocity = (bulletSpawnPoint.position - this.transform.position).RotateVectorByAxisZ(pelletsdAngle * Random.Range(1 - angleVariation, 1 + angleVariation) * i / (pelets - 1) - pelletsdAngle / 2).normalized * speed * Random.Range(1 - velocityVariation, 1 + velocityVariation);
            tempBulletGO.GetComponent<Bullet>().setParameters(this.gameObject, damage, pierce, bulletLifeTime);
            foreach (var status in statuses)
            {
                tempBulletGO.GetComponent<Bullet>().addStatus(status.copy());
            }
        }
    }
    public override void Attack(Vector2 targetPos)
    {
        if (isReloding && mag > 0)
            CancelRelode();
        base.Attack(targetPos);
    }

    protected override void EndRelode()
    {
        Debug.Log("ShootGun end relode" + mag + "/" + magSize);
        if (mag < magSize)
        {
            mag += 1;
            relodeTimer = relodeTime;
            animator.SetTrigger("Relode");
            animator.SetTrigger("EndRelode");
        }
        else
        {
            relodeSlider.gameObject.SetActive(false);
            isReloding = false;
            mag = magSize;
            fireRateTimer = 0;
            animator.SetTrigger("EndRelode");
        }
        if (iconControler != null)
        {
            if(ammoCounterText != null)
                ammoCounterText.text = mag.ToString();
            iconControler.updateWepponIconAmmo(mag + " / " + magSize);
        }
    }
}