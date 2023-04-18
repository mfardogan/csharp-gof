namespace Composite
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            //Giriş birimi tanımlayalım:

            Composite input = new Input()
                              .AddChild(new Mouse());

            //Çıkış birimini tanımlayalım:

            Composite output = new Output()
                              .AddChild(new Speaker())
                              .AddChild(new Monitor());

            //Donanım birimini tanımlayalım:

            Composite hardware = new Hardware()
                              .AddChild(input)
                              .AddChild(output);

            //Bilgisayar birimini tanımlayalım:

            Composite computer = new Computer()
                              .AddChild(new Software())
                              .AddChild(hardware);

            computer.Traverse();



            /*

############################
# Computer         #
#             *            #
#           *   *          #
#         *       *        #
# Harware   Software    # 
#        *                 #
#      *   *               #
#   *        *             #
# Input     Output         #
#   *          *           #
#   *        *   *         #
#   *       *      *       #
# Mouse  Speaker  Monitor  #  
############################
            */
        }
        /* ÇIKTI:
          Bilgisayar birimi çalıştı.
          Yazılım birimi çalıştı.
          Donanım birimi çalıştı.
          Giriş birimi çalıştı.
          Mouse birimi çalıştı.
          Çıkış birimi çalıştı.
          Ekran birimi çalıştı.
          Hoparlör birimi çalıştı. */

    }

    interface IComponent
    {
        /*
           Leaf veya Composite tipindeki tüm nesnelerin bir işi yapması için 
           Traverse fonksiyonu olmalı.
         */
        void Traverse();
    }


    abstract class Leaf : IComponent
    {
        /*
         Kendisinden herhangi bir çocuk meydana gelmemiş nesneler için Leaf sınıfı.
         */
        public string Name { get; }
        protected Leaf(string name)
        {
            Name = name;
        }

        public void Traverse()
        {
            Console.WriteLine($"{Name} birimi çalıştı.");
        }
    }


    abstract class Composite : IComponent
    {
        /*
          Kendisinden çocuk(lar)  meydana gelmemiş nesneler için Composite sınıfı.
          Bu sınıf kendisinden meydana gelen nesneleri tutmak için bir liste içerir.
          Traverse fonksiyonu ile liste içindeki tüm nesnelerin Traverse fonksiyonunu 
          işletir.
        */
        protected Composite(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<IComponent> Components { get; } = new List<IComponent>();

        public Composite AddChild(IComponent component)
        {
            Components.Add(component);
            return this;
        }

        public void Traverse()
        {
            Console.WriteLine($"{Name} birimi çalıştı.");
            foreach (IComponent component in Components)
            {
                component.Traverse();
            }
        }
    }


    class Computer : Composite
    {
        public Computer()
           : base("Bilgisayar")
        {
        }
    }

    class Hardware : Composite
    {
        public Hardware()
           : base("Donanım")
        {
        }
    }

    class Output : Composite
    {
        public Output()
           : base("Çıkış")
        {
        }
    }

    class Monitor : Leaf
    {
        public Monitor()
           : base("Ekran")
        {
        }
    }
    class Software : Composite
    {
        public Software()
           : base("Yazılım")
        {
        }
    }

    class Input : Composite
    {
        public Input()
           : base("Giriş")
        {
        }
    }

    class Mouse : Leaf
    {
        public Mouse()
           : base("Mouse")
        {
        }
    }

    class Speaker : Leaf
    {
        public Speaker()
           : base("Hoparlör")
        {
        }
    }



}