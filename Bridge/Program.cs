namespace Bridge
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    /* Erikli marka damacana ve Bosch markalı bir sebil kombinasyonu yapalım. */
        //    ICarboy carboy = new Erikli();
        //    IDispenser dispenser = new Bosch();
        //    var bridge = new Bridge(carboy, dispenser);

        //    Water warm = bridge.Warm();
        //    Water cold = bridge.Cold();
        //    Water hot = bridge.Hot();

        //    Console.WriteLine(warm.Degree);
        //    Console.WriteLine(cold.Degree);
        //    Console.WriteLine(hot.Degree);
        //}
        ///* ÇIKTI:
        //   20
        //   5
        //   90
        // */

        static void Main(string[] args)
        {
            Bridge bridge = new Combination();

            Water warm = bridge.Warm();
            Water cold = bridge.Cold();
            Water hot = bridge.Hot();

            Console.WriteLine(warm.Degree);
            Console.WriteLine(cold.Degree);
            Console.WriteLine(hot.Degree);
        }
        /* ÇIKTI:
          14
          7
          93
         */


    }


    class Combination : Bridge
    {
        public Combination() : base(new Saka(), new Siemens())
        {
        }
    }


    class Water
    {
        /* Su sınıfı... 
           Suyun o anki sıcaklığını tutan bir Degree özelliğimiz olsun.       
         */
        public int Degree { get; set; }
    }


    interface ICarboy
    {
        /* Damacana sınıfları için interface. Dış dünyaya su vermek için Flow 
           fonksiyonu içerir.
         */
        Water Flow();
    }

    interface IDispenser
    {
        /* Sebil sınıfları için interface. Dış dünyaya sıcak, soğuk veya ılık su 
           vermek için fonksiyonlar içerir. Bu fonksiyonlar sebile bağlı olan 
           damacanadan alınan suyun derecesini değiştirip tekrar dış dünyaya iletir.
         */
        Water Hot(Water w);
        Water Cold(Water w);
        Water Warm(Water w);
    }


    class Bridge
    {
        public Bridge() { }

        public Bridge(ICarboy carboy, IDispenser dispenser)
        {
            Carboy = carboy;
            Dispenser = dispenser;
        }

        public ICarboy Carboy { get; set; } //Damacana
        public IDispenser Dispenser { get; set; } //Sebil

        public Water Hot()
        {
            /* Damacanadan suyu al, suyu ısıtmak için sebile ver, ısınmış suyu geri
               döndür.
             */

            Water w = Carboy.Flow();
            return Dispenser.Hot(w);
        }

        public Water Cold()
        {
            Water w = Carboy.Flow();
            return Dispenser.Cold(w);
        }

        public Water Warm()
        {
            Water w = Carboy.Flow();
            return Dispenser.Warm(w);
        }
    }




    class Erikli : ICarboy
    {
        public Water Flow()
        {
            /* Damacanadan dışarıya bir
               miktar su akıtalım. 
               Damacanının kimyasal yapısı 
               gereği suyun 20 derece 
               olduğunu ve bunun 
               damacanadan damacanaya göre 
               değişebileceğini 
               unutmayalım.
             */
            Water w = new Water();
            w.Degree = 20;
            return w;
        }
    }


    class Saka : ICarboy
    {
        public Water Flow()
        {
            Water w = new Water();
            w.Degree = 14;
            return w;
        }
    }


    class Bosch : IDispenser
    {
        public Water Hot(Water w)
        {
            w.Degree = 90;
            return w;
        }

        public Water Cold(Water w)
        {
            w.Degree = 5;
            return w;
        }

        public Water Warm(Water w)
        {
            return w;
        }
    }

    class Siemens : IDispenser
    {
        public Water Hot(Water w)
        {
            w.Degree = 93;
            return w;
        }

        public Water Cold(Water w)
        {
            w.Degree = 7;
            return w;
        }

        public Water Warm(Water w)
        {
            return w;
        }
    }
}