using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rpgkit
{
    public class DialogShopKeeper : DialogHandle
    {
        public int[] item_list = new int[10];

        public override IEnumerator MessageStart(Action _onFinished)
        {
            m_bIsPlaying = true;
            PlayerControl.Instance.m_bCanMove = false;
            yield return StartCoroutine(DialogManager.Instance.ShowDialog(m_MessageLineArray, () =>
            {
            }));

            // ここにショップの処理

            FinishedAction();
            _onFinished.Invoke();

        }


    }
}


