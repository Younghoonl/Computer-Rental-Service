namespace Assignment1;
using System;

//Main클래스 -> TextFileData객체 생성, input파일 읽는 메소드 호츨 
class MainApp
{
    static void Main(string[] args)
    {
        TextFileData textfileData = new TextFileData();
        textfileData.SetTextFile();
    }
}



