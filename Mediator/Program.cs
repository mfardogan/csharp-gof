//namespace Mediator
//{


abstract class Participant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    /* Katılımcının bir Mediator'ü olmalıdır. Katılımcılar bu sayede diğer katılımcıları
       doğrudan tanımadan mesaj iletebilir. 
    */
    public IChatMediator Mediator { get; set; }

    public virtual void Send(string message)
    {
        /* Mesaj gönderme işlemi şahısların kendisine değil, Mediator'e yapılır. */
        Mediator.Send(this, message);
    }

    public virtual void Take(Participant from, string message)
    {
        /* Başka birinden gelen mesajı yakalamak:  */
        Console.WriteLine($"[Gönderen: {from.Name}, Alıcı: {Name}] {message}");
    }
}


interface IChatMediator
{
    void Send(Participant from, string message);
    void AddParticipant(Participant participant);
}


class Whatsapp : IChatMediator
{
    /* Mediator, katılımcıları bir koleksiyon ile tutar. */
    private readonly IList<Participant> participants = new List<Participant>();

    public void AddParticipant(Participant participant)
    {
        /* Kullanıcıyı Mediator'e eklediğimizde kullanıcı içerideki koleksiyona
           eklenmeden önce, kullanıcının Mediator'ü setlenmelidir. En nihayetinde 
           kullanıcılar bu Mediator ile mesaj gönderip alırlar.
         */

        participant.Mediator = this;
        participants.Add(participant);
    }

    public void Send(Participant from, string message)
    {
        /* Mesaj gönderildiğinde gönderen kişinin kendisi hariç diğer tüm kişilere 
           mesaj iletilir.
         */
        foreach (var participant in participants)
        {
            if (participant.Id == from.Id)
            {
                continue;
            }

            participant.Take(from, message);
        }
    }
}


class John : Participant
{
    public John()
    {
        Id = 1;
        Name = "John";
    }
}

class Jack : Participant
{
    public Jack()
    {
        Id = 3;
        Name = "Jack";
    }
}

class Mary : Participant
{
    public Mary()
    {
        Id = 2;
        Name = "Mary";
    }
}



internal class Program
    {
    static void Main(string[] args)
    {
        Participant john = new John();
        Participant jack = new Jack();
        Participant mary = new Mary();

        IChatMediator mediator = new Whatsapp();
        mediator.AddParticipant(john);
        mediator.AddParticipant(jack);
        mediator.AddParticipant(mary);

        john.Send("Hello guys!");
        jack.Send("Welcome bro =)");
        mary.Send("Hi John. How are you today?");
    }
    /* ÇIKTI:
      [Gönderen: John, Alıcı: Jack] Hello guys!
      [Gönderen: John, Alıcı: Mary] Hello guys!
      [Gönderen: Jack, Alıcı: John] Welcome bro =)
      [Gönderen: Jack, Alıcı: Mary] Welcome bro =)
      [Gönderen: Mary, Alıcı: John] Hi John. How are you today?
      [Gönderen: Mary, Alıcı: Jack] Hi John. How are you today? 
    */


}
//}