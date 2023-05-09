namespace Assignment1;

//User 클래스
public class User
{
   //사용자 아이디를 생성자에서 초기화
   public User(int userId)
   {
      UserId = userId;
   }

   public string Type { get; set; }         //유저 타입
   public string Name { get; set; }         //유저 이름
   public int UserId { get; set; }          //유저 아이디
   public int TypeId { get; set; }          //유저 타입에 따른 아이다
   public string[] UsedFor { get; set; }    //사용 목적
   public string Rent { get; set; } = "N";    //컴퓨터 렌트 유무
   public int RentedComputerId { get; set; } = 0;  //렌트한 컴퓨터 아이디(랜트 안했을시 0으로 초기화)
}

