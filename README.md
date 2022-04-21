# SQLite2DTO
SQLiteのテーブルから.NET用のDTOを作る

## はじめに
SQLiteのテーブル情報を取得し、C#のDTOクラスを作成するプログラムです。

## 実行環境
* .NET 5  

## 実行方法
* ローカル実行  
    dotnet runで実行する。  
    ```sh
    dotnet run --project ./src/Presentation/ConsoleApp/ConsoleApp.csproj [NameSpace] [ファイル出力先] [SQliteファイルパス]
    ```  
    例：```dotnet run --project ./src/Presentation/ConsoleApp/ConsoleApp.csproj DB.Dto CSOutputs SQLiteExample/Test.db```

* Dockerコンテナでの実行  
    Dockerコンテナ上で開発環境を構築する。  
   * 前提  
     * Docker EngineやDocker Desktopがインストール済みであること。

   * 実行手順  
     SQLiteコンテナとdotnetコンテナを起動する。
      1. docker_devに移動  
          ```sh
          cd docker_dev
          ```

      1. (**初回のみ**)ビルド  
          ```sh
          docker-compose build
          ```

      1. コンテナ起動  
          ```sh
          docker-compose up -d
          ```

      1. コンテナに入る  
          ```sh
          docker exec -it docker_dev_dotnet_1 /bin/bash
          ```

      1. コンテナ内で実行 
          1. dotnet runで実行する。
              ```sh
              dotnet run --project ./src/Presentation/ConsoleApp/ConsoleApp.csproj DB.Dto CSOutputs SQLiteExample/Test.db 
              ```

          1. コンテナから離脱する。
              ```sh
              exit
              ```

      1. コンテナ停止・削除  
          ```sh
          docker-compose down
          ```
