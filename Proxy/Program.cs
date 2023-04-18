namespace Proxy
{

    interface IEquipment //Subject ara birimi
    {
        void CountUp();
    }

    class Equipment : IEquipment //RealSubject sınıfı
    {
        public void CountUp()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i + 1);
                Thread.Sleep(1000);
            }
        }
    }


    //Proxy sınıfı (RealSubject sınıfına vekil olur.)
    class EquipmentProxy : IEquipment
    {
        /*
           Proxy sınıfı, gerçek sınıftan(RealSubject) bir alan içerir.
           Aynı zamanda gerçek sınıfın uyguladığı ara birimi uygular ve
           ara biriminden gelen fonksiyonların gövdesinde gerçek nesnenin
           ilişkili fonksiyonu çağrılır.
         */
        private Equipment equipment = new Equipment();

        public void CountUp()
        {
            /*
              Equipment.CountUp() fonksiyonunu asenkron olarak yürütelim.
             */
            void proc() => equipment.CountUp();
            var process = new ThreadStart(proc);
            new Thread(process).Start();
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            IEquipment proxy = new EquipmentProxy();
            proxy.CountUp();

            Console.WriteLine("İşlem devam ediyor...");
        }
        /* ÇIKTI:
          İşlem devam ediyor...
          1
          2 
          3
          4
          5
         */
    }
}