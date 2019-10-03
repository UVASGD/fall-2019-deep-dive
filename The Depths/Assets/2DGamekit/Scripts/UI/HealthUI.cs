using System.Collections;
using UnityEngine;

/*
namespace Gamekit2D
{
    public class HealthUI : MonoBehaviour
    {
        public Damageable representedDamageable;

        IEnumerator Start ()
        {
            if(representedDamageable == null)
                yield break;
            yield return null;

            // On Start, draw health bar
            // Rect 1 for bar bg
            float StartHP= representedDamageable.startingHealth;
            Texture2D healthGUI = new Texture2D(300, 75);
            // Make wrendah?
            GetComponent<Renderer>().material.mainTexture = healthGUI;
            for (int y = 0; y < healthGUI.height; y++)
            {
                for (int x = 0; x < healthGUI.width; x++)
                {
                    Color bgColor = Color.gray;
                    healthGUI.SetPixel(x, y, bgColor);
                }
            }
            healthGUI.Apply();
            // Rect 2 for health bar
        }

        public void ChangeHitPointUI (Damageable damageable)
        {
            // Change bar size on hit
        }
    }
}
*/

// Healthbar using 2D Gamekit Template (buggy)
namespace Gamekit2D
{
    public class HealthUI : MonoBehaviour
    {
        public Damageable representedDamageable;
        public GameObject healthIconPrefab;

        protected Animator[] m_HealthIconAnimators;

        protected readonly int m_HashActivePara = Animator.StringToHash("Active");
        protected readonly int m_HashInactiveState = Animator.StringToHash("Inactive");
        protected const float k_HeartIconAnchorWidth = 0.041f;
        // hp per heart
        protected int hpPerHeart = 20;

        IEnumerator Start()
        {
            if (representedDamageable == null)
                yield break;

            yield return null;

            // Get starting HeartCount (as of now, 5 hearts (20hp /ea))
            int numHearts = (int)representedDamageable.startingHealth / hpPerHeart;
            m_HealthIconAnimators = new Animator[numHearts];

            for (int i = 0; i < numHearts; i++)
            {
                GameObject healthIcon = Instantiate(healthIconPrefab);
                healthIcon.transform.SetParent(transform);
                RectTransform healthIconRect = healthIcon.transform as RectTransform;
                healthIconRect.anchoredPosition = Vector2.zero;
                healthIconRect.sizeDelta = Vector2.zero;
                healthIconRect.anchorMin += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
                healthIconRect.anchorMax += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
                m_HealthIconAnimators[i] = healthIcon.GetComponent<Animator>();

                //I just kinda added a thing here
                if (representedDamageable.CurrentHealth / hpPerHeart < i + 1)
                {
                    m_HealthIconAnimators[i].Play(m_HashInactiveState);
                    m_HealthIconAnimators[i].SetBool(m_HashActivePara, false);
                }
            }
        }

        public void ChangeHitPointUI(Damageable damageable)
        {
            if (m_HealthIconAnimators == null)
                return;

            for (int i = 0; i < m_HealthIconAnimators.Length; i++)
            {
                //I also just kinda added a thing here
                m_HealthIconAnimators[i].SetBool(m_HashActivePara, damageable.CurrentHealth / hpPerHeart >= i + 1);
            }
        }
    }
}