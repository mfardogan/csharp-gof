namespace Strategy
{
    interface ISortStrategy
    {
        /*
           Her şeyden evvel, streteji sınıflarının uyması gereken strandartları
           anlatan bir abstraction gerekli. Bir sıralama algoritması parametre olarak 
           bir dizi alıp geriye bir dizi teslim eder.
         */
        int[] Execute(int[] numbers);
    }


    class BubbleSort : ISortStrategy
    {
        public int[] Execute(int[] numbers)
        {
            Console.WriteLine("Dizi, Bubble Sort yöntemiyle sıralandı...");
            /* Konudan uzaklamaşmamak için sıralama algoritmasının detaylarına 
              girmiyorum. */
            return numbers.OrderBy(e => e).ToArray();
        }
    }

    class MergeSort : ISortStrategy
    {
        public int[] Execute(int[] numbers)
        {
            Console.WriteLine("Dizi, Merge Sort yöntemiyle sıralandı...");
            /* Konudan uzaklamaşmamak için sıralama algoritmasının detaylarına
               girmiyorum. */
            return numbers.OrderBy(e => e).ToArray();
        }
    }


    class SortingStrategy
    {
        public SortingStrategy(ISortStrategy strategy)
        {
            Strategy = strategy;
        }

        /*
           Burada bir composition yapılır. ISortStrategy ara birimini uygulamış tüm 
           sınıf nesneleri bu alana verilebilir. Üst seviyedeki sınıf, doğrudan alt 
           seviyedeki sınıfların kendilerine değil, onların uyguladığı ISortStrategy ara 
           birimine bağımlı olur. Buna Export Coupling adı verilir ve bunun uygulaması 
           Dependency Inversion sağlar.
         */

        public ISortStrategy Strategy { get; set; }

        public int[] Execute(int[] numbers)
        {
            /*
              Context sınıfı kendisine parametre olarak verilmiş
              Strategy nesnesinin Execute fonksiyonunu çağırır.
              Algoritmanın detaylarına dair hiç bir şey bilmez!
             */
            return Strategy.Execute(numbers);
        }
    }

    internal class Program
    {

        public static void Main(string[] args)
        {
            /*
              Sıralama yöntemini değiştirmek istediğinizde yalnızca kurucu fonksiyon 
             parametresini aşağıdan değiştirmeniz yeterli olacaktır.
             */
            SortingStrategy strategy =
                 new SortingStrategy(new BubbleSort());

            int[] unsorted = { 1, 5, -3, 2, 90, 12, 9 };
            int[] sorted1 = strategy.Execute(unsorted);

            /* Dilediğiniz yerde stratejiyi değiştirmeniz mümkün.  */

            strategy.Strategy = new MergeSort();
            int[] sorted2 = strategy.Execute(unsorted);
        }
        /* ÇIKTI:
         Dizi, Bubble Sort yöntemiyle sıralandı...
         Dizi, Merge Sort yöntemiyle sıralandı...
         */

    }
}