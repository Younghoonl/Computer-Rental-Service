namespace Assignment1;

//User클래스를 상속받은 Gamer클래스
public class Gamer : User
{
    //타입과 사용목적 프로퍼티 초기화
    public Gamer(int userId) : base(userId)
    {
        Type = "Gamers";
        UsedFor = new string[] { "internet", "game" };
    }
    public int GamerId { get; set; }   //게이머 아이디 프로퍼티
}