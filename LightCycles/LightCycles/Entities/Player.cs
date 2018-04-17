using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

using Microsoft.Graphics.Canvas.Brushes;

namespace LightCycles.Entities
{
    class Player
    {
        private int tile_x;
        private int tile_y;

        private int pos_x;
        private int pos_y;

        private int width;
        private int height;

        private Map map;

        private int counter;

        private Random random;

        bool death0 = false;
        bool death1 = false;
        bool death2 = false;
        bool death3 = false;
        bool leftKeyDown = false;
        bool rightKeyDown = false;
        bool upKeyDown = false;
        bool downKeyDown = false;
        bool aKeyDown = false;
        bool dKeyDown = false;
        bool wKeyDown = false;
        bool sKeyDown = false;
        bool iKeyDown = false;
        bool jKeyDown = false;
        bool kKeyDown = false;
        bool lKeyDown = false;
        bool tKeyDown = false;
        bool fKeyDown = false;
        bool gKeyDown = false;
        bool hKeyDown = false;

        public Player(Map map)
        {
            // https://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values
            random = new Random(Guid.NewGuid().GetHashCode());

            this.map = map;

            tile_x = random.Next(5, 15);
            tile_y = random.Next(5, 15);

            width = 20;
            height = 20;

            counter = 0;

            Window.Current.CoreWindow.KeyDown += KeyDown_Handler;
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args, int player)
        {
            // because we cant test with xbox controllers right now and only have keyboards to work with only two players will be implemented
            // this code will have to be changed once we are ready to test with an xbox, but for now this will allow us to set up the functionality
            // for the rest of the game. Players also dont start moving until input is given. In the real game they should start moving in a random
            // direction immediately, but for testing purposes I'm leaving that out so there is no random element to deal with when testing
            counter++;
            if (counter >= 15)
            {
                counter = 0;
                if (death0 == false)
                {

                    if (player == 0)
                    {
                        if (dKeyDown) { tile_x++; }
                        if (aKeyDown) { tile_x--; }
                        if (sKeyDown) { tile_y++; }
                        if (wKeyDown) { tile_y--; }
                        if (tile_x == 20 || tile_x == -1 || tile_y == -1 || tile_y == 20) { death0 = true; }
                    }
                }
                if (death1 == false)
                {
                    if (player == 1)
                    {
                        if (rightKeyDown) { tile_x++; }
                        if (leftKeyDown) { tile_x--; }
                        if (downKeyDown) { tile_y++; }
                        if (upKeyDown) { tile_y--; }
                        if (tile_x == 20 || tile_x == -1 || tile_y == -1 || tile_y == 20) { death1 = true; }
                    }
                }
                ///* 
                // up = I
                // left = j
                // down = k
                // right = L
                // */
                //if (death2 == false)
                //{
                //    if (player == 2)
                //    {
                //        if (lKeyDown) { tile_x++; }
                //        if (jKeyDown) { tile_x--; }
                //        if (kKeyDown) { tile_y++; }
                //        if (iKeyDown) { tile_y--; }
                //        if (tile_x == 20 || tile_x == -1 || tile_y == -1 || tile_y == 20) { death2 = true; }
                //    }
                //}
                ///* 
                // up = T
                // left = F
                // down = G
                // right = H
                // */
                //if (death3 == false)
                //{
                //    if (player == 3)
                //    {
                //        if (hKeyDown) { tile_x++; }
                //        if (fKeyDown) { tile_x--; }
                //        if (gKeyDown) { tile_y++; }
                //        if (tKeyDown) { tile_y--; }
                //        if (tile_x == 20 || tile_x == -1 || tile_y == -1 || tile_y == 20) { death3 = true; }
                //    }
                //}
            }

            map.TileToPos(tile_x, tile_y, out pos_x, out pos_y);
            
            if (player == 0)
            {
                args.DrawingSession.FillRectangle(pos_x - width / 2, pos_y - height / 2, width, height, Color.FromArgb(255, 0, 253, 255));
                args.DrawingSession.DrawRectangle(pos_x - width / 2, pos_y - height / 2, width, width, Color.FromArgb(255, 0, 253, 255));
            }
            if (player == 1)
            {
                args.DrawingSession.FillRectangle(pos_x - width / 2, pos_y - height / 2, width, height, Color.FromArgb(255, 255, 0, 0));
                args.DrawingSession.DrawRectangle(pos_x - width / 2, pos_y - height / 2, width, width, Color.FromArgb(255, 255, 0, 0));
            }
            if (player == 2)
            {
                args.DrawingSession.FillRectangle(pos_x - width / 2, pos_y - height / 2, width, height, Color.FromArgb(255, 255, 0, 208));
                args.DrawingSession.DrawRectangle(pos_x - width / 2, pos_y - height / 2, width, width, Color.FromArgb(255, 255, 0, 208));
            }
            if (player == 3)
            {
                args.DrawingSession.FillRectangle(pos_x - width / 2, pos_y - height / 2, width, height, Color.FromArgb(255, 246, 255, 0));
                args.DrawingSession.DrawRectangle(pos_x - width / 2, pos_y - height / 2, width, width, Color.FromArgb(255, 246, 255, 0));
            }
        }

        private void KeyDown_Handler(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Left)
            {
                leftKeyDown = true;
                rightKeyDown = false;
                upKeyDown = false;
                downKeyDown = false;
            }
            else if (e.VirtualKey == Windows.System.VirtualKey.Right)
            {
                leftKeyDown = false;
                rightKeyDown = true;
                upKeyDown = false;
                downKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.Up)
            {
                leftKeyDown = false;
                rightKeyDown = false;
                upKeyDown = true;
                downKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.Down)
            {
                leftKeyDown = false;
                rightKeyDown = false;
                upKeyDown = false;
                downKeyDown = true;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.A)
            {
                aKeyDown = true;
                dKeyDown = false;
                wKeyDown = false;
                sKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.D)
            {
                aKeyDown = false;
                dKeyDown = true;
                wKeyDown = false;
                sKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.W)
            {
                aKeyDown = false;
                dKeyDown = false;
                wKeyDown = true;
                sKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.S)
            {
                aKeyDown = false;
                dKeyDown = false;
                wKeyDown = false;
                sKeyDown = true;
            }
            
            else if(e.VirtualKey == Windows.System.VirtualKey.I)
            {
                iKeyDown = true;
                jKeyDown = false;
                kKeyDown = false;
                lKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.J)
            {
                iKeyDown = false;
                jKeyDown = true;
                kKeyDown = false;
                lKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.K)
            {
                iKeyDown = false;
                jKeyDown = false;
                kKeyDown = true;
                lKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.L)
            {
                iKeyDown = false;
                jKeyDown = false;
                kKeyDown = false;
                lKeyDown = true;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.H)
            {
                tKeyDown = false;
                fKeyDown = false;
                gKeyDown = false;
                hKeyDown = true;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.G)
            {
                tKeyDown = false;
                fKeyDown = false;
                gKeyDown = true;
                hKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.F)
            {
                tKeyDown = false;
                fKeyDown = true;
                gKeyDown = false;
                hKeyDown = false;
            }

            else if (e.VirtualKey == Windows.System.VirtualKey.T)
            {
                tKeyDown = true;
                fKeyDown = false;
                gKeyDown = false;
                hKeyDown = false;
            }
        }

    }
}
