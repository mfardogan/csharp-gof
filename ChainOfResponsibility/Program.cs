namespace ChainOfResponsibility
{

    abstract class Compiler
    {
        /* Her bir sorumluluk kendindinden bir sonrakine işaret eden bir referansa 
           sahip olmalıdır. Buna Next diyelim. */

        protected Compiler Next { get; set; }

        public virtual Compiler Bind(Compiler next)
        {
            /* Bir fonksiyon yardımıyla ilgili hakanın devamındaki halkanın 
               bağlanması gerekir. */
            Next = next;
            return this;
        }
        /* Her bir alt sınıfın ayrıca uygulacağı bir abstract fonksiyon olmalıdır. 
           Çünkü sorumluluk zincirindeki her halkanın görevi bir diğerininkinden 
           farklı olacaktır. Bu fonksiyon alt sınıflarca uygulanır ve programlanır. 
           Zincirdeki halkaların çalışması için  dış kaynaktan alınması gerekecek bir 
           parametre varsa ilgili parametre bu fonksiyona verilecektir. */

        public abstract void Handle();
    }

    class LexicalAnalyzer : Compiler
    {
        public override void Handle()
        {
            /* Zincirin her halkası öncelikle kendi sorumluluğunu yerine getirir ve 
              ardından kendisinin işaret ettiği bir sonraki sorumluluk halkasının 
              Handle() fonsiyonunu çağırır. */

            Console.WriteLine("Lexical analiz yapılıyor");
            Next?.Handle(); /* veya   if(Next != null) { Next.Handle(); } */
        }
    }

    class SyntaxAnalyzer : Compiler
    {
        public override void Handle()
        {
            Console.WriteLine("Syntax analizi yapılıyor");
            Next?.Handle();
        }
    }

    class SemanticAnalyzer : Compiler
    {
        public override void Handle()
        {
            Console.WriteLine("Semantik analizi yapılıyor");
            Next?.Handle();
        }
    }

    class IntermediateCodeGenerator : Compiler
    {
        public override void Handle()
        {
            Console.WriteLine("Ara kod üretiliyor");
            Next?.Handle();
        }
    }

    class CodeOptimizer : Compiler
    {
        public override void Handle()
        {
            Console.WriteLine("Kod optimizasyonu yapılıyor");
            Next?.Handle();
        }
    }

    class TargetCodeGenerator : Compiler
    {
        public override void Handle()
        {
            Console.WriteLine("Hedef kod üretiliyor");
            Next?.Handle();
        }
    }


    #region Example2
    abstract class Component
    {
        protected Component Next { get; set; } //Bir sonraki halka
        protected Component Previous { get; set; } //Bir önceki halka

        public virtual Component Bind(Component next)
        {
            /* Zincirin bir sonraki halkasını belirlerken sonraki halkanın öncesi de 
               belirlenir. */
            next.Previous = this;
            Next = next;
            return this;
        }

        public abstract void Handle();
        public abstract void Rollback();
    }

    class Alhpa : Component
    {
        public override void Handle()
        {
            Console.WriteLine("Alpha işlemi yapıldı.");
            Next?.Handle();
        }

        public override void Rollback()
        {
            Console.WriteLine("Alpha işlemi geri alındı.");
            Previous?.Rollback();
        }
    }
    class Beta : Component
    {
        public override void Handle()
        {
            Console.WriteLine("Betha işlemi yapıldı.");
            Next?.Handle();
        }

        public override void Rollback()
        {
            Console.WriteLine("Betha işlemi geri alındı.");
            Previous?.Rollback();
        }
    }

    class Teta : Component
    {
        public override void Handle()
        {
            /* Beklenmeyen bir durum oluştuğunu düşünelim. */

            Console.WriteLine("Teta işlemi yapılırken hata alındı!");
            Rollback();
        }

        public override void Rollback()
        {
            Console.WriteLine("Teta işlemi geri alındı!");
            Previous?.Rollback();
        }
    }

    class Responsibilities
    {
        /* Sorumluluk zincirini oluşturma görevini üstlenen
           bir Factory sınıf. İstemci tarafların zinciri 
           inşa etme yükünü alır. */

        public Component GetChain()
        {
            /* Alpha -> Beta -> Teta sıralaması değişecek olursa 
               bu değişiklik istemcileri rahatsız etmeden yalnız bu 
               kısımda değişiklik yaparak çözümlenebilir. */

            return new Alhpa().Bind(new Beta().Bind(new Teta()));
        }
    }

    #endregion

    internal class Program
    {
        /*
        static void Main(string[] args)
        {
            Compiler compiler = new LexicalAnalyzer().Bind(
                    new SyntaxAnalyzer().Bind(
                        new SemanticAnalyzer().Bind(
                            new IntermediateCodeGenerator().Bind(
                                new CodeOptimizer().Bind(
                                    new TargetCodeGenerator()
                           )))));

            compiler.Handle();
        }*/
        /* ÇIKTI:
          Lexical analiz yapılıyor
          Syntax analizi yapılıyor
          Semantik analizi yapılıyor
          Ara kod üretiliyor
          Kod optimizasyonu yapılıyor
          Hedef kod üretiliyor
        */


        static void Main(string[] args)
        {
            /* İstemci taraflar Alpha, Beta gibi tüm sınıflara bağımlı değildir.
               Onun yerine yalnız Factory sınıfına talepte bulunurlar. */

            Responsibilities responsibilities = new Responsibilities();
            Component component = responsibilities.GetChain();
            component.Handle();
        }
        /* ÇIKTI:
          Alpha işlemi yapıldı.
          Betha işlemi yapıldı.
          Teta işlemi yapılırken hata alındı!
          Teta işlemi geri alındı!
          Betha işlemi geri alındı.
          Alpha işlemi geri alındı.
        */
    }
}