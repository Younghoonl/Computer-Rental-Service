namespace Assignment1;

//Computer클래스를 상속받은 Netbook 클래스
public class Netbook : Computer
{
    //생성자에서 컴퓨터 타입, 사용목적, 가격 프로퍼티 초기화
    public Netbook(int computerId) : base(computerId)
    {
        Type = "Netbook";
        UsedFor = new string[] { "internet" };
        Price = 7000;
    }
}