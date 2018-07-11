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

        public static InputLayout PLAYER1;
        public static InputLayout PLAYER2;

        public JoystickManager()
        {

        }

        private void Awake()
        {
            int player = 1;
            for (int i = 0; i < Mathf.Min(Input.GetJoystickNames().Length, 4); i++)
            {
                foreach (var item in Layouts)
                {
                    if (Input.GetJoystickNames()[i].Contains(item.TryParseName))
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
                PLAYER1 = Layouts[0];
                PLAYER1.Init(1);

                PLAYER2 = Layouts[1];
                PLAYER2.Init(2);
            }

            //int i = 0;
            //while (i < 4)
            //{
            //    Debug.Log(Input.GetJoystickNames()[i]);

            //    i++;
            //}
        }

    }
}
