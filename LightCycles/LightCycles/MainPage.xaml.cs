using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

using LightCycles.Entities;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightCycles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Map map;
        Player[] players;

        public MainPage()
        {
            this.InitializeComponent();

            int width = 20;
            int height = 20;
            this.map = new Map(width, height);
            this.players = new Player[2];

            for (int index = 0; index < this.players.Length; index++)
            {
                this.players[index] = new Player(map);
            }

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canvas.RemoveFromVisualTree();
            this.canvas = null;
        }

        private void canvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
        }

        private void canvas_Draw_1(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            double canvas_width = this.canvas.Size.Width;
            double canvas_height = this.canvas.Size.Height;

            // https://microsoft.github.io/Win2D/html/P_Microsoft_Graphics_Canvas_CanvasDrawingSession_Transform.htm
            args.DrawingSession.Transform = Matrix3x2.CreateTranslation(new Vector2((float)(canvas_width/2 - map.map_width/2), (float)(canvas_height/2 - map.map_height/2)));
            map.Draw(sender, args);

            for (int index = 0; index < this.players.Length; index++)
            {
                this.players[index].Draw(sender, args, index);
            }

            //args.DrawingSession.DrawText("Hello, World!", 100, 100, Colors.Black);
            //args.DrawingSession.DrawCircle(125, 125, 100, Colors.Green);
            //args.DrawingSession.DrawLine(0, 0, 50, 200, Colors.Red);
        }
    }
}
