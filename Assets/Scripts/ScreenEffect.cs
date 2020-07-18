using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace rpgkit
{
    public class ScreenEffect : StaticInstance<ScreenEffect>
    {
        public Image m_imgScreen;
        public float m_fFadeSpeed;
        public IEnumerator Fadeout(float _fSpeed , Action _onFinish)
        {
            if(_fSpeed < 0.0f)
            {
                _fSpeed = 1.0f;
            }
            while(m_imgScreen.color.a<1.0f)
            {
                m_imgScreen.color = new Color(
                    m_imgScreen.color.r, 
                    m_imgScreen.color.g, 
                    m_imgScreen.color.b,
                    Mathf.MoveTowards(m_imgScreen.color.a, 1f, _fSpeed * m_fFadeSpeed * Time.deltaTime));
                yield return null;
            }
            m_imgScreen.color = new Color(
                m_imgScreen.color.r,
                m_imgScreen.color.g,
                m_imgScreen.color.b,
                1.0f);
            _onFinish.Invoke();
        }


    }
}