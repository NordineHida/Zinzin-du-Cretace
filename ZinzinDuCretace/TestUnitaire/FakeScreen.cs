using IUTGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnitaire
{
    /// <summary>
    /// Fausse fenetre pour les tests unitaires
    /// </summary>
    /// <author>Nordine</author>
    public class FakeScreen : IScreen
    {
        public KeyEvent KeyDown { get => null; set { } }
        public KeyEvent KeyUp { get => null; set { } }
        public MouseWheelEvent MouseWheel { get => null; set { } }
        public MouseButtonEvent MouseDown { get => null; set { } }
        public MouseButtonEvent MouseUp { get => null; set { } }
        public MouseMoveEvent MouseMove { get => null; set { } }


        public double Width =>100;

        public double Height => 100;

        public void DrawSprite(int spriteID, double x, double y, double angle = 0, double scaleX = 1, double scaleY = 1, int zindex = 0)
        {
            
        }

        public void Focus()
        {
            
        }

        public double GetSpriteHeight(int spriteID)
        {
            return (10);
        }

        public double GetSpriteWidth(int spriteID)
        {
            return (10);
        }

        public void InitSpritesStore(string spritesFolderName)
        {

        }

        public int LoadSprite(string spriteName)
        {
            return (10);
        }

        public void RemoveSprite(int spriteID)
        {

        }
    }
}
