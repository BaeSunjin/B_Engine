using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  Need Admin right
 */ 

namespace Runtime_CodeBuild
{
    class Program
    {
        static void Main(string[] args)
        {

            System.IO.StreamReader myFile = 
                new System.IO.StreamReader("D:\\B_Engine\\Runtime_CodeBuild\\Runtime_CodeBuild\\TestBuildClass.cs");

            string codeToString = myFile.ReadToEnd();
            myFile.Close();


            Console.WriteLine(codeToString);


            //닷넷 언어들은 다 쓸수 있다.
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("c#");
            CompilerParameters compilerParameters = new CompilerParameters();

           
            compilerParameters.GenerateExecutable = false;  //생성 파일 결정여부 exe, dll
            compilerParameters.GenerateInMemory = false;    //메모리, 디스크 생성여부
            compilerParameters.OutputAssembly = @"D:\B_Engine\Runtime_CodeBuild\Runtime_CodeBuild\CompileResult\TestBuildClass.dll";


            CompilerResults compilerResults =
                codeDomProvider.CompileAssemblyFromSource(compilerParameters, codeToString);

           
            if (compilerResults.Errors.Count > 0)
            {

                
                foreach (CompilerError ce in compilerResults.Errors)
                {
                    String msg;
                    if (ce.IsWarning)
                        msg = String.Format("[WARN] {0} in {1} at line {2}:{3}", ce.ErrorText, ce.FileName, ce.Line, ce.Column);
                    else
                    {
                        msg = String.Format("[ERROR] {0} in {1} at line {2}:{3}", ce.ErrorText, ce.FileName, ce.Line, ce.Column);
                    }
                    Console.WriteLine(msg);
                }

                
                Console.WriteLine("Compile Faile");
            }else
            {
                Console.WriteLine("Compile file Path : " + compilerResults.PathToAssembly);
                Console.WriteLine("Compile Success");
            }


        }
    }
}


/* TO DO 
 * 
 * C#   코드 컴파일 테스트 성공
 * C++  코드 컴파일 테스트  //C++ dll 생성에 관하여 알아봐야한다 ex) cpp파일 h 파일 이분리.
 * 
 * C#   코드 함수 호출
 * C++  코드 함수 호출
 * https://msdn.microsoft.com/ko-kr/library/saf5ce06(v=vs.110).aspx
 * 
 */
