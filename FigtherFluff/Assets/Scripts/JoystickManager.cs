using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    if (Input.GetJoystickNames()[i].IndexOf(item.TryParseName, StringComparison.OrdinalIgnoreCase) >= 0) //Input.GetJoystickNames()[i].Contains(item.TryParseName))
                    {
                        if (player == 1)
                        {
                            player++;
                            PLAYER1 = item;
                            item.Init(i+1);
                        }
                        else if (player == 2)
                        {
                            player++;
                            PLAYER2 = item;
                            item.Init(i+1);
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
                    InputLayout clone = UnityEngine.Object.Instantiate(Default) as InputLayout;
                    PLAYER1 = clone;
                    PLAYER1.Init(1);
                }

                if (player <= 2)
                {
                    InputLayout clone2 = UnityEngine.Object.Instantiate(Default) as InputLayout;
                    PLAYER2 = clone2;
                    PLAYER2.Init(2);
                }
            }

        }

    }
}
