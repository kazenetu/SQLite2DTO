using Application;
using Application.Model;
using System;

namespace Presentation.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length < 3)
      {
        Console.WriteLine();
        Console.WriteLine("Input parameters! \"NameSpace OutputPath SQLiteFilePath ['useSnakeCase']\"");
        Console.WriteLine();
        return;
      }

      //DI設定
      SettingDIContainer.SetDI();

      // パラメータ取得
      var useSnakeCase = false;
      var nameSpace = args[0];
      var outputPath = args[1];
      var sqliteFilePath = args[2];
      if(args.Length >= 4 && args[3] == "useSnakeCase")
      {
        useSnakeCase = true;
      }

      try
      {
        // Appication呼び出し
        var inputParamModel = new InputParamModel(nameSpace, outputPath, sqliteFilePath, useSnakeCase);
        var messages = new GenerateCSApplicationService().GenerateCSFileFromDB(inputParamModel).Messages;

        // ファイル生成結果を取得
        foreach (var message in messages)
        {
          Console.WriteLine(message);
        }
      }
      catch (ExceptionModel exceptionModel)
      {
        Console.WriteLine("---Exception!!---");
        Console.WriteLine(exceptionModel);
      }
      catch (Exception ex)
      {
        Console.WriteLine("---Exception!!---");
        Console.WriteLine(new ExceptionModel(ex));
      }
    }
  }
}
