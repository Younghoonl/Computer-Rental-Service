namespace Assignment1;

//Computer 클래스
public class Computer
{
    //computer 아이디를 인자로 받아 생성자에서 초기화 해준다
    public Computer(int computerId)
    {
        ComId = computerId;
    }
    public string Type { get; set; }           //타입
    public int ComId { get; set; }            //컴퓨터 번호
    public int TypeId { get; set; }        //컴퓨터 타입에 따른 번호
    public string[] UsedFor { get; set; }      //사용 목적  
    public int Price { get; set; }             //사용 요금 
    public string Avail { get; set; } = "Y";   //대여 가능 여부
    public int UserId { get; set; }            //사용자 아이디
    public int DaysRequested { get; set; } = 0;  //대여 요청 일 수
    public int DaysLeft { get; set; } = 0;     //남은 대여일 수
    public int DayUsed { get; set; } = 0;      //사용 일 수
}