using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rpgkit
{
    public class DamageNumber : MonoBehaviour
    {
        [Header("Initialization")]
        public Text damageText;

        [Header("Effect Settings")]
        public float lifetime = 1f;
        public float moveSpeed = 1f;

        public float placementJitter = .5f;

        void Update()
        {
            Destroy(gameObject, lifetime);
            //Move the damage number in the y-axis
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        }

        public void SetDamage(int damageAmount)
        {
            damageText.text = damageAmount.ToString();
            //Show the damage number everytime in a slighty altered position
            transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
            transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
        }
    }
}
