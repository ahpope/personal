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
    class Tile
    {
        private int pos_x;
        private int pos_y;

        private int tile_width;
        private int tile_height;

        private Random random;

        public Tile(int pos_x, int pos_y, int tile_width, int tile_height)
        {
            this.pos_x = pos_x;
            this.pos_y = pos_y;

            this.tile_width = tile_width;
            this.tile_height = tile_height;

            random = new Random();
        }
        
        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.FillRectangle(pos_x, pos_y, tile_width, tile_height, Color.FromArgb(255, 0, 0, 0));
            args.DrawingSession.DrawRectangle(pos_x, pos_y, tile_width, tile_height, Color.FromArgb(255, 0, 253, 255));
        }
        
    }

    class Map
    {
        public int width, height;
        public Tile[,] tiles;

        private int tile_width;
        private int tile_height;

        public int map_width;
        public int map_height;

        public Map (int width, int height)
        {
            this.width  = width;
            this.height = height;

            this.tile_width = 32;
            this.tile_height = 32;

            this.map_width = this.width * this.tile_width;
            this.map_height = this.height * this.tile_height;

            tiles = new Tile[width, height];

            for (int ix = 0; ix < width; ix++)
            {
                for (int iy = 0; iy < height; iy++)
                {
                    int pos_x = ix * tile_width;
                    int pos_y = iy * tile_height;

                    tiles[ix, iy] = new Tile(pos_x, pos_y,
                                             tile_width, tile_height);
                }
            }
        }

        // returns the position of the center of the tile
        public void TileToPos(int tile_x, int tile_y, out int pos_x, out int pos_y)
        {
            pos_x = (tile_x * tile_width ) + tile_width  / 2;
            pos_y = (tile_y * tile_height) + tile_height / 2;
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            for (int ix = 0; ix < width; ix++)
            {
                for (int iy = 0; iy < height; iy++)
                {
                    tiles[ix, iy].Draw(sender, args);
                }
            }
        }
    }
}
