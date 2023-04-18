//namespace Flyweight
//{

interface IShape
{
    void Draw();
    string Color { get; set; }
}

class Square : IShape
{
    public string Color { get; set; }

    public Square(string color)
    {
        Color = color;
    }

    public void Draw()
    {
        Console.WriteLine($"{Color} renkli kare çizildi!");
    }
}


class Container
{
    /*
       Nesneleri renklerine göre tutacağımız bir sözlük(map) bulunur.
       İstemci tarafından talep edilen nesne, renk bilgisine bakılarak bu sözlükten 
       istenir.
     */
    private Dictionary<string, IShape> dictionary
           = new Dictionary<string, IShape>();

    public IShape Get(string key)
    {
        /*
           Önce sözlükte nesne varsa doğrudan istemciye iletilir. Yoksa, önce ilgili 
           nesne oluşturulup sözlüğe eklenir, sonrasında istemci tarafa iletilir.
         */

        dictionary.TryGetValue(key, out IShape obj);
        if (obj == null)
        {
            obj = new Square(key);
            dictionary[key] = obj;
        }

        return obj;
    }
}




internal class Program
{
    static void Main(string[] args)
    {
        List<string> colors = new List<string>() { "Kırmızı", "Siyah", "Beyaz" };

        var flyweight = new Container();
        for (var i = 0; i < 10000; i++)
        {
            string key = colors[i % 3];
            /* Nesneye ihtiyaç duyulduğunda yeni bir nesne üretilmez!
               Bunun yerine Flyweight sınıfından nesne talep edilir.  */

            IShape shape = flyweight.Get(key);
            shape.Draw();
        }
    }
    /* ÇIKTI:
       Kırmızı renkli kare çizildi!
       Siyah renkli kare çizildi!
       Beyaz renkli kare çizildi! 
       ...
       ...
       ...
       Beyaz renkli kare çizildi!
       Kırmızı renkli kare çizildi!
     */


}
//}