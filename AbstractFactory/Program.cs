namespace AbstractFactory
{

    class Computer
    {
        public string Type { get; set; }

        public Computer(string type)
        {
            Type = type;
        }
    }


    class Mouse
    {
        public string Type { get; set; }

        public Mouse(string type)
        {
            Type = type;
        }
    }


    /* Masaüstü bilgisayar sınıfı */

    class Desktop : Computer
    {
        public Desktop()
            : base("Masaüstü Bilgisayar")
        {
        }
    }

    /* Laptop bilgisayar sınıfı */

    class Laptop : Computer
    {
        public Laptop()
            : base("Dizüstü Bilgisayar")
        {
        }
    }


    /* PS2 mouse sınıfı */

    class PS2Mouse : Mouse
    {
        public PS2Mouse()
            : base("PS2 Mouse")
        {
        }
    }



    /* USB mouse sınıfı */

    class UsbMouse : Mouse
    {
        public UsbMouse()
            : base("USB Mouse")
        {
        }
    }


    interface IComputerFactory
    {
        Mouse NewMouse();        /* Mouse üretir. */
        Computer NewComputer();  /* Bilgisayar üretir. */
    }


    class DesktopFactory : IComputerFactory
    {
        /* Bu fabrika, masaüstü bilgisayar
           ve ona uygun PS2 mouse üretir.*/

        public Mouse NewMouse()
        {
            return new PS2Mouse();
        }

        public Computer NewComputer()
        {
            return new Desktop();
        }
    }


    class LaptopFactory : IComputerFactory
    {
        /* Bu fabrika, laptop bilgisayar
          ve ona uygun USB mouse üretir.*/

        public Mouse NewMouse()
        {
            return new UsbMouse();
        }

        public Computer NewComputer()
        {
            return new Laptop();
        }
    }


    class ComputerCentre
    {
        private IComputerFactory factory;

        /* Dependency Inversion & Strategy pattern. */
        public ComputerCentre(IComputerFactory factory)
        {
            this.factory = factory;
        }

        public (Computer, Mouse) Create()
        {
            /* Abstraction üzerinden 1.ürünü ve 2. ürünü üretip, tuple olarak 
              döndürelim.*/

            return (factory.NewComputer(), factory.NewMouse());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /* Nesne üretecek olan sınıf, fabrika sınıfını ve üretilen nesnelerin ne
              olduklarını bilmez. Bu sebepten dolayı bu örüntü, Abstract Factory olarak
              adlandırılmıştır.*/

            IComputerFactory factory = new DesktopFactory();
            ComputerCentre centre = new ComputerCentre(factory);

            (Computer computer, Mouse mouse) = centre.Create();
            Console.WriteLine($"{mouse.Type} ve {computer.Type}");


            factory = new LaptopFactory();
            centre = new ComputerCentre(factory);

            (computer, mouse) = centre.Create();
            Console.WriteLine($"{mouse.Type} ve {computer.Type}");
        }
        /* ÇIKTI:
          PS2 Mouse ve Masaüstü Bilgisayar
          USB Mouse ve Dizüstü Bilgisayar
        */

    }
}