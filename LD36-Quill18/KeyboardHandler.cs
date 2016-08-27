﻿using System;
namespace LD36Quill18
{
    public class KeyboardHandler
    {

        public static void Update_Keyboard()
        {
            if (Console.KeyAvailable)
            {
                // Read one key
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (((cki.Modifiers & ConsoleModifiers.Control) != 0) && cki.Key == ConsoleKey.Q)
                {
                    Game.Instance.ExitGame();
                }
                else
                {
                    switch (Game.Instance.InputMode)
                    {
                        case InputMode.Normal:
                            Update_Keyboard_Normal(cki);
                            break;
                        case InputMode.Aiming:
                            Update_Keyboard_Aiming(cki);
                            break;
                        case InputMode.Inventory:
                            Update_Keyboard_Inventory(cki);
                            break;
                        case InputMode.Looking:
                            Update_Keyboard_Inventory(cki);
                            break;
                    }
                }
            }

            // If there are any more keys, drain them out of the buffer
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }




        public static void Update_Keyboard_Normal(ConsoleKeyInfo cki)
        {
                if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.NumPad6)
                {
                    PlayerCharacter.Instance.QueueMoveBy(1, 0);
                    Game.Instance.DoTick();
                }
                else if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.NumPad4)
                {
                    PlayerCharacter.Instance.QueueMoveBy(-1, 0);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.NumPad8)
                {
                    PlayerCharacter.Instance.QueueMoveBy(0, -1);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.NumPad2)
                {
                    PlayerCharacter.Instance.QueueMoveBy(0, 1);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.NumPad7 || cki.Key == ConsoleKey.Home)
                {
                    PlayerCharacter.Instance.QueueMoveBy(-1, -1);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.NumPad9 || cki.Key == ConsoleKey.PageUp)
                {
                    PlayerCharacter.Instance.QueueMoveBy(1, -1);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.NumPad3 || cki.Key == ConsoleKey.PageDown)
                {
                    PlayerCharacter.Instance.QueueMoveBy(1, 1);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.NumPad1 || cki.Key == ConsoleKey.End)
                {
                    PlayerCharacter.Instance.QueueMoveBy(-1, 1);
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.NumPad5)
                {
                // Do nothing
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.OemPeriod)
                {
                    PlayerCharacter.Instance.GoDown();
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.OemComma)
                {
                    PlayerCharacter.Instance.GoUp();
                Game.Instance.DoTick();
            }
                else if (cki.Key == ConsoleKey.F)
                {
                    Game.Instance.InputMode = InputMode.Aiming;
                    Game.Instance.aimingOverlay = new AimingOverlay();
                    Game.Instance.aimingOverlay.Draw();
                }
                else if (cki.Key == ConsoleKey.I)
                {
                    Game.Instance.InputMode = InputMode.Inventory;
                    FrameBuffer.Instance.Clear();
                }
            }

        static void Update_Keyboard_Inventory(ConsoleKeyInfo cki)
        {
                FrameBuffer.Instance.Clear();
                switch (cki.Key)
                {
                    case ConsoleKey.Escape:
                        Game.Instance.InputMode = InputMode.Normal;
                        RedrawRequests.FullRedraw();
                        break;
                    default:
                        // Try to map to an inventory item
                        int i = (int)cki.KeyChar - 'a';
                        if (i >= 0 && i < PlayerCharacter.Instance.Items.Length)
                        {
                            PlayerCharacter.Instance.UseItem(i);
                        }
                        i = (int)cki.KeyChar - 'A';
                        if (i >= 0 && i < PlayerCharacter.Instance.Items.Length)
                        {
                            PlayerCharacter.Instance.UseItem(i);
                        }
                        break;
                }
            }

        static void Update_Keyboard_Aiming(ConsoleKeyInfo cki)
        {
            AimingOverlay aimingOverlay = Game.Instance.aimingOverlay;

                // Direction keys move the targeting reticle instead of the player
                if (cki.Key == ConsoleKey.Escape)
                {
                    Game.Instance.InputMode = InputMode.Normal;
                    Game.Instance.aimingOverlay = null;
                    return;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    // Fire!
                    PlayerCharacter.Instance.FireTowards(aimingOverlay.X, aimingOverlay.Y);
                    Game.Instance.InputMode = InputMode.Normal;
                    Game.Instance.aimingOverlay = null;
                    Game.Instance.DoTick();
                    return;
                }
                else if (cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.NumPad6)
                {
                    aimingOverlay.X += 1;
                    aimingOverlay.Y += 0;
                }
                else if (cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.NumPad4)
                {
                    aimingOverlay.X -= 1;
                    aimingOverlay.Y += 0;
                }
                else if (cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.NumPad8)
                {
                    aimingOverlay.X += 0;
                    aimingOverlay.Y -= 1;
                }
                else if (cki.Key == ConsoleKey.DownArrow || cki.Key == ConsoleKey.NumPad2)
                {
                    aimingOverlay.X += 0;
                    aimingOverlay.Y += 1;
                }
                else if (cki.Key == ConsoleKey.NumPad7 || cki.Key == ConsoleKey.Home)
                {
                    aimingOverlay.X -= 1;
                    aimingOverlay.Y -= 1;
                }
                else if (cki.Key == ConsoleKey.NumPad9 || cki.Key == ConsoleKey.PageUp)
                {
                    aimingOverlay.X += 1;
                    aimingOverlay.Y -= 1;
                }
                else if (cki.Key == ConsoleKey.NumPad3 || cki.Key == ConsoleKey.PageDown)
                {
                    aimingOverlay.X += 1;
                    aimingOverlay.Y += 1;
                }
                else if (cki.Key == ConsoleKey.NumPad1 || cki.Key == ConsoleKey.End)
                {
                    aimingOverlay.X -= 1;
                    aimingOverlay.Y += 0;
                }

            }     
    }
}
