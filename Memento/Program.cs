namespace Memento
{
    class PointMemento //Memento sınıfı
    {
        public PointMemento(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }


    class Point //Originator sınıfı
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PointMemento Backup()
        {
            /* 
               Backup fonksiyonu nesnenin mevcut durumlarını bir Memento olarak dışarıya 
               iletir. 
             */

            return new PointMemento(X, Y);
        }

        public void Setup(PointMemento memento)
        {
            /* 
               Setup fonksiyonu nesnenin iç durumlarını dışarıdan gelen ile değiştirir.        
             */

            X = memento.X;
            Y = memento.Y;
        }
    }

    class Caretaker //Caretaker sınıfı
    {
        /* Caretaker sınıfın Memento içerir. Bazen oldukça karmaşık memento 
           algoritmaları ile çalışmanız gerekebilir. Bu durumda geri yükleme 
           işlemleriyle alakalı algoritmaların yazılabileceği bir yer olacak 
           Caretaker sınıfı bulunur...
         */

        public PointMemento Memento { get; set; }
    }



    class Memento
    {
        public Memento(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }



    class CaretakerTwoWay
    {
        private readonly Stack<Memento> forwards = new Stack<Memento>();
        private readonly Stack<Memento> backwards = new Stack<Memento>();

        public void Step(Memento memento)
        {
            /* Yapılan her işlem forwards yığınına alınır. */
            forwards.Push(memento);
        }

        /* Geri alınacak adım var mı? */
        public bool CanUndo() => forwards.Count > 0;

        public Memento Undo()
        {
            if (!CanUndo()) { return null; }
            /* Geri almak için 1. yığından attığını 2. yığına koy.*/

            Memento memento = forwards.Pop();
            backwards.Push(memento);
            return memento;
        }

        /* İleri alınabilecek adım var mı? */
        public bool CanRedo() => backwards.Count > 0;

        public Memento Redo()
        {
            if (!CanRedo()) { return null; }
            /* İleri almak için 2. yığından attığını 1. yığına koy.*/

            Memento memento = backwards.Pop();
            forwards.Push(memento);
            return memento;
        }
    }



    class Mouse
    {
        public Mouse()
        {  /* Nesne ilk oluştuğunda koordinatlar 0,0 olacaktır. */
            caretaker.Step(new Memento(0, 0));
        }

        private int x = 0;
        private int y = 0;

        public int X
        {
            get => x;
            set
            {  /* X veya Y her değiştiğinde bunu Caretaker’e söylememiz gerekir. */
                x = value;
                caretaker.Step(new Memento(x, y));
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                caretaker.Step(new Memento(x, y));
            }
        }

        private readonly CaretakerTwoWay caretaker = new CaretakerTwoWay();

        public void Undo() /* Geri al */
        {
            if (!caretaker.CanUndo()) { return; }

            Memento memento = caretaker.Undo();
            x = memento.X;
            y = memento.Y;
        }

        public void Redo() /* İleri al */
        {
            if (!caretaker.CanRedo()) { return; }

            Memento memento = caretaker.Redo();
            x = memento.X;
            y = memento.Y;
        }

        public void WhereAmI()
        {
            Console.WriteLine($"X: {x}, Y:{y}");
        }
    }




    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    Point point = new Point();
        //    point.X = 10;
        //    point.Y = 30;

        //    Caretaker caretaker = new Caretaker();
        //    caretaker.Memento = point.Backup();
        //    /* 
        //       Bu noktada Point nesnesi 10 ve 30 değerlerini tuttuğu için 
        //       geri yükleme sonrasında nesneye 10 ve 30 değerleri yüklenecektir.
        //    */

        //    point.X = 50;
        //    point.Y = 90;
        //    point.Setup(caretaker.Memento);

        //    Console.WriteLine(point.X);
        //    Console.WriteLine(point.Y);
        //}
        ///* ÇIKTI
        //   10
        //   30
        // */

        static void Main(string[] args)
        {
            Mouse mouse = new Mouse();
            mouse.X = 100;
            mouse.Y = 101;

            mouse.X = 200;
            mouse.Y = 202;

            mouse.X = 300;
            mouse.Y = 303;

            /*
              Yalnız ‘u’ ve ‘r’ karakterlerine basabilirsiniz. Geri almak için ‘u’, 
              ileri almak için ‘r’ kullanın. 
            */

            while (true)
            {
                Console.Write("U:Undo, R:Redo :");
                ConsoleKey info = Console.ReadKey().Key;
                Console.Clear();

                if (info == ConsoleKey.U)
                {
                    mouse.Undo();
                }
                else if (info == ConsoleKey.R)
                {
                    mouse.Redo();
                }

                mouse.WhereAmI();
            }
        }
    }
}