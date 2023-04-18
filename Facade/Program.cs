namespace Facade
{

    abstract class Shape
    {
        public string Color { get; set; }
        public abstract void Draw();
    }

    class Moon : Shape //Subsystem-1
    {
        public override void Draw()
        {
            Console.WriteLine(Color + " ay çizildi.");
        }
    }

    class Star : Shape //Subsystem-2
    {
        public override void Draw()
        {
            Console.WriteLine(Color + " yıldız çizildi.");
        }
    }

    class Rectangle : Shape //Subsystem-3
    {
        public override void Draw()
        {
            Console.WriteLine(Color + " dikdörtgen çizildi.");
        }
    }

    class Toolbox //Facade
    {
        private Shape star = new Star();
        private Shape moon = new Moon();
        private Shape rectangle = new Rectangle();

        public string Color { get; set; }

        public void DrawStar()
        {
            star.Color = Color;
            star.Draw();
        }

        public void DrawMoon()
        {
            moon.Color = Color;
            moon.Draw();
        }

        public void DrawRectangle()
        {
            rectangle.Color = Color;
            rectangle.Draw();
        }

        public void DrawFlag()
        {
            rectangle.Color = "Kırmızı";
            rectangle.Draw();

            star.Color = moon.Color = "Beyaz";
            moon.Draw();
            star.Draw();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Toolbox facade = new Toolbox();
            facade.DrawFlag();
        }
        /* ÇIKTI:
          Kırmızı dikdörtgen çizildi.
          Beyaz ay çizildi.
          Beyaz yıldız çizildi.
         */
    }
}