using UnityEngine;

namespace Assets.Scripts
{
    public class JoystickManager : MonoBehaviour
    {
        [SerializeField]
        private InputLayout[] Layouts;

        [SerializeField]
        private InputLayout Default;

        public static InputLayout PLAYER1;
        public static InputLayout PLAYER2;

        private void Awake()
        {
            int player = 1;
            for (int i = 0; i < Mathf.Min(Input.GetJoystickNames().Length, 4); i++)
            {
                foreach (var item in Layouts)
                {
                    if (Input.GetJoystickNames()[i].IndexOf(item.TryParseName, System.StringComparison.OrdinalIgnoreCase) >= 0) //Input.GetJoystickNames()[i].Contains(item.TryParseName))
                    {
                        if (player == 1)
                        {
                            player++;
                            PLAYER1 = Init(item, i+1);
                        }
                        else if (player == 2)
                        {
                            player++;
                            PLAYER2 = Init(item, i+1);
                        }
                        break;
                    }
                }
            }

            // Fallback
            if (player < 3)
            {
                if (player <= 1)
                {
                    PLAYER1 = Init(Default, 1);
                }

                if (player <= 2)
                {
                    PLAYER2 = Init(Default, 2);
                }
            }

        }

        private InputLayout Init(InputLayout layout, int id)
        {
            InputLayout clone = Instantiate(layout) as InputLayout;
            clone.Init(id);
            return clone;
        }

    }
}
