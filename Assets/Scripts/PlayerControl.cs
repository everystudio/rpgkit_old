using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class PlayerControl : StaticInstance<PlayerControl>
    {
        public string m_strTargetTeleporterName;
        public bool m_bCanMove = true;
        public Rigidbody2D m_rigidBody;
        public float m_fMoveSpeed = 1.0f;

        // カメラの調整
        private Vector3 m_boundary1;
        private Vector3 m_boundary2;

        public DialogHandle m_dialogHandle;


        void Start()
        {
            m_rigidBody = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (m_bCanMove)
            {
                m_rigidBody.velocity = new Vector2(Mathf.RoundToInt(Input.GetAxis("Horizontal")), Mathf.RoundToInt(Input.GetAxis("Vertical"))) * m_fMoveSpeed;
            }
            else
            {
                m_rigidBody.velocity = Vector2.zero;
            }

            if(Input.GetButtonDown("Jump"))
            {
                if( m_dialogHandle != null && m_dialogHandle.m_bIsPlaying == false )
                {
                    Debug.Log("message start");
                    StartCoroutine( m_dialogHandle.MessageStart(() =>
                    {
                        // なにかするなら
                        Debug.Log("message end");
                    }));
                }
            }

        }

        public void SetBounds(Vector3 bound1, Vector3 bound2)
        {
            m_boundary1 = bound1 + new Vector3(.5f, 1f, 0f);
            m_boundary2 = bound2 + new Vector3(-.5f, -1f, 0f);
        }


    }
}
