namespace Singleton
{

    //class MySingleton
    //{
    //    /* Kurucu fonksiyon(lar), private veya protected yapılarak 
    //       dış dünyanın bu sınıftan bir nesne oluşturması yasaklanır.
    //     */
    //    protected MySingleton() { }

    //    /* Sınıf, kendi tipinde statik bir alana sahiptir ve
    //       statik bir fonksiyon aracılığıyla bu alan dış dünyaya iletilir.
    //     */
    //    private static MySingleton instance = new MySingleton(); /* Eager Initialization */

    //    public static MySingleton GetObject()
    //    {
    //        return instance;
    //    }

    //    public void SayHi()
    //    {
    //        Console.WriteLine("Hello from " + GetHashCode());
    //    }
    //}


    //class MySingleton
    //{
    //    protected MySingleton() { }

    //    /* Alan tanımlandığı yerde başlatılmaz!*/
    //    private static MySingleton instance; 

    //    public static MySingleton GetObject()
    //    {
    //        //Lazy initialization:

    //        if (instance == null)
    //        {
    //            instance = new MySingleton();
    //        }

    //        return instance;
    //    }

    //    public void SayHi()
    //    {
    //        Console.WriteLine("Hello from " + GetHashCode());
    //    }
    //}

    //class MySingleton
    //{
    //    protected MySingleton() { }

    //    private static MySingleton instance;

    //    /* Sekronizasyonu sağlamak için statik bir alan tanımlanır. */

    //    private static object sync = new object();

    //    public static MySingleton GetObject()
    //    {
    //        lock (sync)
    //        {
    //            if (instance == null)
    //            {
    //                instance = new MySingleton();
    //            }
    //        }

    //        return instance;
    //    }

    //    public void SayHi()
    //    {
    //        Console.WriteLine("Hello from " + GetHashCode());
    //    }
    //}

    class MySingleton
    {
        protected MySingleton() { }

        private static MySingleton instance;
        private static object sync = new object();

        public static MySingleton GetObject()
        {
            if (instance == null)
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new MySingleton();
                    }
                }
            }

            return instance;
        }

        public void SayHi()
        {
            Console.WriteLine("Hello from " + GetHashCode());
        }
    }


    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    /* var obj = new MySingleton(); 
        //       Sınıfın kurucu fonksiyonu protected yapıldığından
        //       dış dünyada yukarıdaki gibi bu sınıf adına bir nesne üretimi yapılamaz!
        //     */

        //    MySingleton.GetObject().SayHi();
        //    MySingleton.GetObject().SayHi();
        //}
        ///* ÇIKTI:
        //   Hello from 58225482 (Sayılar sizde farklı gelecektir!)
        //   Hello from 58225482
        // */

        static void Main(string[] args)
        {
            //Client-1:
            new Thread(new ThreadStart(() =>
                MySingleton.GetObject().SayHi()
            )).Start();

            //Client-2:
            new Thread(new ThreadStart(() =>
                MySingleton.GetObject().SayHi()
            )).Start();

            //Client-3:
            new Thread(new ThreadStart(() =>
                MySingleton.GetObject().SayHi()
            )).Start();
        }
        /* ÇIKTI:
           Hello from 6044116
           Hello from 4032828
           Hello from 4032828
         */
    }
}