using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class TooltipSprite : MonoBehaviour
    {
        [SerializeField]
        private float Duration = 2;

        private void Start()
        {
            StartCoroutine(RunTooltip());
        }

        private IEnumerator RunTooltip()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            sprite.color = Color.clear;

            while(sprite.color.a < 0.95f)
            {
                sprite.color = Color.Lerp(sprite.color, Color.black, Time.deltaTime * 7);
                yield return null;
            }

            yield return new WaitForSeconds(Duration);

            while (sprite.color.a > 0.05f)
            {
                sprite.color = Color.Lerp(sprite.color, Color.clear, Time.deltaTime * 5);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
