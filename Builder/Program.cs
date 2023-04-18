namespace Builder
{

    class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        public void ShowOff()
        {
            Console.WriteLine($"{Color} {Brand} {Model} çalıştı...");
        }
    }


    interface ICarBuilder
    {
        Car Get(); //Hazır hale gelmiş nesneyi döndürür.
        void BuildBrand(); //Araç markasını ayarlar
        void BuildModel(); //Araç modelini ayarlar
        void BuildColor(); //Araç rengini ayarlar
    }


    class BmwBuilder : ICarBuilder
    {
        private Car car = new Car();

        public Car Get()
        {
            return car;
        }

        public void BuildBrand()
        {
            car.Brand = "Bmw";
        }

        public void BuildModel()
        {
            car.Model = "E38";
        }

        public void BuildColor()
        {
            car.Color = "Kırmızı";
        }
    }



    class MercedesBuilder : ICarBuilder
    {
        private Car car = new Car();

        public Car Get()
        {
            return car;
        }

        public void BuildBrand()
        {
            car.Brand = "Mercedes";
        }

        public void BuildModel()
        {
            car.Model = "C180";
        }

        public void BuildColor()
        {
            car.Color = "Siyah";
        }
    }


    class BuilderDirector
    {
        public ICarBuilder Builder { get; set; }
        public BuilderDirector(ICarBuilder builder)
        {
            Builder = builder;
        }

        public Car Build()
        {
            /*
               Director sınıfı, nesnenin kullanıma hazır hale gelmesi için gereken 
               adımların ne olduğunu ve hangi sırayla uygulanması gerektiğini bilir.
               Bu detayları istemci tarafından gizlemek için kullanılır.
             */

            Builder.BuildBrand();
            Builder.BuildModel();
            Builder.BuildColor();
            return Builder.Get();
        }
    }






    internal class Program
    {
        static void Main(string[] args)
        {
            BuilderDirector director = new BuilderDirector(new BmwBuilder());
            Car bmw = director.Build();
            bmw.ShowOff();

            director.Builder = new MercedesBuilder();
            Car mercedes = director.Build();
            mercedes.ShowOff();
        }
        /* ÇIKTI:
          Kırmızı Bmw E38 çalıştı...
          Siyah Mercedes C180 çalıştı...
         */

    }
}