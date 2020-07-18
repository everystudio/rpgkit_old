using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace rpgkit{
    public class TeleportTo : MonoBehaviour
    {
        public string teleport_name;
        public string target_scene;
        public string target_teleport;
        public TeleportEntry entry;

        void Start()
        {
            entry.teleport_name = teleport_name;

            if( PlayerControl.Instance.m_strTargetTeleporterName == teleport_name)
            {
                PlayerControl.Instance.gameObject.transform.position = new Vector3(
                    entry.transform.position.x,
                    entry.transform.position.y,
                    PlayerControl.Instance.gameObject.transform.position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                // 移動先の保存
                PlayerControl.Instance.m_strTargetTeleporterName = target_teleport;

                // フェードアウトさせる処理
                StartCoroutine(ScreenEffect.Instance.Fadeout(1.0f, () =>
               {
                   SceneManager.LoadScene(target_scene);
               }));

                // 完了後にシーンをロードする
            }
        }


    }
}