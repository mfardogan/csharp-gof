namespace Prototype
{
    using System.Runtime.Serialization.Formatters.Binary;

    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    Cellphone samsung = new Samsung();
        //    samsung.Property.Add("Gps");
        //    samsung.Property.Add("Camera");
        //    samsung.Property.Add("Wireless");

        //    Samsung copy = (Samsung)samsung.Clone();
        //    copy.Print();

        //    /* Klonlama fonksiyonu temel sınıfta olduğundan
        //       türeyen alt sınıfların hepsi bu fonksiyonu kullanmaya hak sahibidir.

        //       Cellphone iPhone = new IPhone();
        //       iPhone.Property.Add("Gps");
        //       iPhone.Property.Add("Camera");

        //       IPhone copyI = (IPhone)iPhone.Clone();
        //       copyI.Print();
        //     */
        //}
        ///* ÇIKTI:
        //   Samsung:
        //   Gps
        //   Camera
        //   Wireless
        //*/

        static void Main(string[] args)
        {
            Cellphone samsung = new Samsung();
            samsung.Property.Add("Gps");
            samsung.Property.Add("Camera");
            samsung.Property.Add("Wireless");

            Samsung copy = (Samsung)samsung.Clone();
            copy.Property.Add("Stereo Voice");
            samsung.Print();
        }
        /* ÇIKTI:
           Samsung:
           Gps
           Camera
           Wireless
           Stereo Voice (Bu da ne şimdi :) )
         */



    }


    [Serializable]
    class Property
    {
        private IList<string> properties = new List<string>();

        public void Add(string property)
        {
            properties.Add(property);
        }

        public void Print()
        {
            foreach (string prop in properties)
            { Console.WriteLine(prop); }
        }
    }


    interface IPrototype<T>
    {
        T Clone();
    }

    //class Cellphone : IPrototype<Cellphone>
    //{
    //    public string Title { get; set; }
    //    public Property Property { get; set; } = new Property();

    //    public void Print()
    //    {
    //        Console.WriteLine($"{Title}:");
    //        Property.Print();
    //    }

    //    public Cellphone Clone()
    //    {
    //        /* MemberwiseClone() fonksiyonu Shallow-copy işlemi yapar. */

    //        return (Cellphone)MemberwiseClone();
    //    }
    //}


    /*
         Serializable özniteliğinin Property, IPhone ve Samsung 
         sınıflarına da uygulanması gerekir. Aksi halde hata alınır!
        */

    [Serializable]
    class Cellphone : IPrototype<Cellphone>
    {
        public string Title { get; set; }
        public Property Property { get; set; } = new Property();


        public void Print()
        {
            Console.WriteLine($"{Title}:");
            Property.Print();
        }

        public Cellphone Clone()
        {
            //Deep-copy işlemi:

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);

                stream.Position = 0;
                return (Cellphone)formatter.Deserialize(stream);
            }
        }
    }


    [Serializable]
    class IPhone : Cellphone
    {
        public IPhone()
        {
            Title = "IPhone";
        }
    }

    [Serializable]

    class Samsung : Cellphone
    {
        public Samsung()
        {
            Title = "Samsung";
        }
    }

}