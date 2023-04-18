namespace Decorator
{

    abstract class Shape //Component sınıf
    {
        public abstract void Draw();
    }

    class Box : Shape //Component1 sınıfı
    {
        public override void Draw()
        {
            Console.WriteLine("Kutu çizildi.");
        }
    }


    abstract class Decorator : Shape
    {   /*Sınıf hem Shape tipinden türer, hem de Shape tipinde bir üye içerir.*/

        protected Shape Shape { get; set; }

        protected Decorator(Shape shape)
        {
            Shape = shape;
        }
    }


    class Filled : Decorator
    {
        public Filled(Shape shape) : base(shape)
        {
        }

        public override void Draw()
        {
            Shape.Draw();
            Console.WriteLine("Şeklin içi boyandı.");
        }
    }

    class Sketched : Decorator
    {
        public Sketched(Shape shape) : base(shape)
        {
        }

        public override void Draw()
        {
            Shape.Draw();
            Console.WriteLine("Şekle çizikler eklendi.");
        }
    }

    class Rounded : Decorator
    {
        public Rounded(Shape shape) : base(shape)
        {
        }

        public override void Draw()
        {
            Shape.Draw();
            Console.WriteLine("Şeklin köşeleri yuvarlatıldı.");
        }
    }

    class Circle : Shape
    {
        /*
           Çözümümüz gelişime açık değişime kapalı bir çözümdür.
           (Bkz. Open-Closed Principle) bu yüzden yeni bir sınıf tanımlayıp 
           ilgili sınıfınızı Shape türünden türettikten sonra derhal 
           onu dekore edebilirsiniz.
         */
        public override void Draw()
        {
            Console.WriteLine("Yuvarlak çizildi.");
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            Decorator decorator = new Sketched(
                new Filled(
                   new Circle()
                ));

            decorator.Draw();
        }
        /* ÇIKTI:
           Yuvarlak çizildi.
          Şeklin içi boyandı.
          Şekle çizikler eklendi.
         */
    }
}