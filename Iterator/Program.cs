namespace Iterator
{
    using System.Collections;
    using System.Collections.ObjectModel;

    interface IIterator<T>
    {
        /*
           Iterasyon aracı sıradaki elemanı döndüren GetNext() ve listenin sonuna 
           gelip gelinmediğini söyleyen HasMore() işlevlerini içerir.
         */
        T GetNext();
        bool HasMore();
    }

    interface IIterable<T>
    {
        /*
           Bu ara birim, bizzat koleksiyon sınıflarına uygulanır.
           Ara birim sadece IIterator<T> döndüren bir fonksiyon içerir.
         */
        IIterator<T> GetIterator();
    }


    class ClusterIterator<T> : IIterator<T>
    {
        /*
           Iterator sınıfı itere edilecek verileri parametre olarak alır ve bir 
           algoritma ile verileri dış dünyaya döndürür. Örneğimizde tüm elemanları 
           eklenme sırasına göre döndüreceğiz. 
         */
        private int i = 0;
        private readonly IList<T> items;

        public ClusterIterator(IList<T> items)
        {
            this.items = items;
        }

        public T GetNext()
        {
            /* Sıradaki elemanı dış dünyaya ver, sayacı bir artır. */
            return items[i++];
        }

        public bool HasMore()
        {
            /* Koleksiyon içinde dış dünuyaya iletilmemiş başka veri var mı? */
            return i < items.Count;
        }
    }


    class Cluster<T> : IIterable<T>
    {
        private readonly IList<T> items = new List<T>();

        public void Add(T item)
        {
            /*
               Veri yapısına dışarıdan eleman eklemek çok kolaydır. Fakat eklenen her 
               eleman muhakkak dış kaynaklar tarafından geri istenecektir. Peki, 
               eklenen elemanları dış dünyaya nasıl verebiliriz?
            */
            items.Add(item);
        }

        public IIterator<T> GetIterator()
        {
            /*
               Verilerin nasıl bir sırayla hangi stratejilerle dış dünyaya 
               aktarılacağı ayrı bir bileşenin sorumluluğundadır. Bu bileşen 
               IIterator<T> arabirimini uygular ve verilerin dış dünyaya aktarılmasına 
               dair stratejileri uygular. Bu stratejilerin uygulayan sınıfla 
               koleksiyon sınıfının ayrılması sorumlulukların bölünmesi açısından 
               yararlıdır.
            */
            return new ClusterIterator<T>(items);
        }
    }


    class StackIterator : IIterator<int>
    {
        private int i;
        private readonly IList<int> items;

        public StackIterator(IList<int> items)
        {
            this.items = items;
            /* 
              Son giren eleman ilk önce çıkacağı için akışı listenin sonundan 
              başlatmalıyız.
             */
            i = items.Count;
        }

        public int GetNext()
        {
            return items[i];
        }

        public bool HasMore()
        {
            return i-- > 0; //her adımda başa doğru bir adım yaklaşmalıyız.
        }
    }


    class Stack : IIterable<int>
    {
        private readonly IList<int> numbers = new Collection<int>();

        public void Push(int i)
        { numbers.Add(i); }

        public int Pop()
        {
            /* Pop, en tepedeki elemanı listeden çıkrır. */
            int last = numbers.Count - 1;
            int item = numbers[last];
            numbers.RemoveAt(last);
            return item;
        }

        public IIterator<int> GetIterator()
        {
            return new StackIterator(numbers);
        }
    }




    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    Cluster<string> strs = new Cluster<string>();
        //    strs.Add("Furkan");
        //    strs.Add("Yusuf");
        //    strs.Add("Arzu");

        //    IIterator<string> iter = strs.GetIterator();//İterasyon aracını getir.

        //    while (iter.HasMore()) //Listenin sonuna gelinmediği sürece
        //    {
        //        //sıradaki elemanı yazdır.
        //        Console.WriteLine(iter.GetNext());
        //    }
        //}
        ///* ÇIKTI:
        //  Furkan
        //  Yusuf
        //  Arzu
        //*/

        //static void Main(string[] args)
        //{
        //    var stack = new Stack();
        //    for (var i = 1; i <= 5; i++)
        //    {
        //        stack.Push(i);
        //    }

        //    stack.Pop(); //5'i at
        //    stack.Pop(); //4'ü at

        //    IIterator<int> iter = stack.GetIterator();
        //    while (iter.HasMore())
        //    {
        //        Console.WriteLine(iter.GetNext());
        //    }
        //}
        ///* ÇIKTI:
        //  3
        //  2
        //  1
        //*/

        static void Main(string[] args)
        {
            MyQueue vehicles = new MyQueue();
            vehicles.Enqueue("Sputnik");
            vehicles.Enqueue("Falcon 9 ");
            vehicles.Enqueue("Apollon 2 ");



            foreach (string vehicle in vehicles)
            {
                Console.WriteLine(vehicle);
            }

            //LINQ denemesi yapalım:

            List<string> endsWithANumber =
                (from vehc in vehicles
                 where vehc.Length >= 8
                 select vehc.ToUpper())
                 .ToList(); //"FALCON 9" ve "APOLLON 2" içerir.


            List<string> endsWithANumber2 =
                vehicles.Where(e => e.Length >= 8)
                .Select(e => e.ToUpper())
                .ToList(); //"FALCON 9" ve "APOLLON 2" içerir.
        }
        /* ÇIKTI:
           Sputnik
           Falcon 9
           Apollon 2
        */
    }



    class MyQueue : IEnumerable<string>
    {
        private List<string> items;
        public MyQueue()
        {
            items = new List<string>();
        }

        public void Enqueue(string item)
        {
            items.Add(item);
        }

        public string Dequeue()
        {
            string item = items[0];
            items.RemoveAt(0);
            return item;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}