using Domain.Classes;
using Domain.CSFiles;
using Domain.DB;
using Domain.Exceptions;
using Infrastructure.CSFiles;
using PostgreSQL2DTOTest.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace PostgreSQL2DTOTest.InfrastructureTest
{
  /// <summary>
  /// CSファイル出力リポジトリのテスト
  /// </summary>
  public class TestCSFileRepository : IDisposable
  {
    /// <summary>
    /// テスト対象
    /// </summary>
    private CSFileRepository repository;

    /// <summary>
    /// Setup
    /// </summary>
    public TestCSFileRepository()
    {
      repository = new CSFileRepository();
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact]
    public void ExceptionAllNG()
    {
      var classEntities = new List<ClassEntity>();
      var useSnakeCase = false;
      FileDataEntity fileDataEntity = null;

      var ex = Assert.ThrowsAny<DomainException>(() => repository.Generate(classEntities, fileDataEntity, useSnakeCase));
      Assert.Equal(2, ex.Messages.Count);
    }

    [Fact]
    public void ExceptionClassEntityZero()
    {
      var classEntities = new List<ClassEntity>();
      var fileDataEntity = FileDataEntity.Create("DB.Dto", "CSOutputs");
      var useSnakeCase = false;

      var ex = Assert.ThrowsAny<DomainException>(() => repository.Generate(classEntities, fileDataEntity, useSnakeCase));
      Assert.Single(ex.Messages);
    }

    [Fact]
    public void ExceptionFileDataEntityNull()
    {
      var mockDBRepository = new MockDBRepository();
      var classEntities = mockDBRepository.GetClasses(DBParameterEntity.Create("SQLitePath"));
      var useSnakeCase = false;
      FileDataEntity fileDataEntity = null;

      var ex = Assert.ThrowsAny<DomainException>(() => repository.Generate(classEntities, fileDataEntity, useSnakeCase));
      Assert.Single(ex.Messages);
    }

    [Fact]
    public void CreateFiles()
    {
      var csOutputName = "CSOutputs";
      var mockDBRepository = new MockDBRepository();
      var classEntities = mockDBRepository.GetClasses(DBParameterEntity.Create("SQLitePath"));
      var fileDataEntity = FileDataEntity.Create(csOutputName, "DB.Dto");
      var useSnakeCase = false;

      var messages = repository.Generate(classEntities, fileDataEntity, useSnakeCase);
      Assert.Equal(2, messages.Count);
      Assert.Equal($"  >>m_test... finish{Environment.NewLine}", messages[0]);
      Assert.Equal($"  >>t_test... finish{Environment.NewLine}", messages[1]);

      var fileNames = Directory.GetFiles(fileDataEntity.OutputPath).OrderBy(filename => filename).ToList();
      Assert.Equal(2, fileNames.Count);
      Assert.Equal($"{Path.Combine(fileDataEntity.OutputPath,"MTest.cs")}", fileNames[0]);
      Assert.Equal($"{Path.Combine(fileDataEntity.OutputPath,"TTest.cs")}", fileNames[1]);
      
      // テスト終了後にフォルダ削除
      Directory.Delete(csOutputName, true);
    }

    [Fact]
    public void CheckFileClassNotUseSnake()
    {
      var csOutputName = "CSOutputs2";

      var mockDBRepository = new MockDBRepository();
      var classEntities = mockDBRepository.GetClasses(DBParameterEntity.Create("SQLitePath"));
      var fileDataEntity = FileDataEntity.Create(csOutputName, "DB.Dto");
      var useSnakeCase = false;

      repository.Generate(classEntities, fileDataEntity, useSnakeCase);

      var fileNames = Directory.GetFiles(fileDataEntity.OutputPath).Where(filename => filename == $"{Path.Combine(fileDataEntity.OutputPath, "MTest.cs")}").ToList();
      Assert.Single(fileNames);

      var actual = string.Empty;
      using (StreamReader sr = new StreamReader(fileNames[0]))
      {
        actual = sr.ReadToEnd();
      }

      var expected = @"using System.ComponentModel.DataAnnotations.Schema;
namespace DB.Dto
{
  /// <summary>
  /// マスタテーブル
  /// </summary>
  public class MTest
  {
    public MTest()
    {
    }
    
    /// <summary>
    /// intになる
    /// </summary>
    [Column(""int_1"")]
    public int Int1{set; get;}
    
    /// <summary>
    /// Dateになる
    /// </summary>
    [Column(""Date_1_1"")]
    public DateTime Date11{set; get;}
  }
}
";

      Assert.Equal(expected, actual);

      // テスト終了後にフォルダ削除
      Directory.Delete(csOutputName, true);
    }

    [Fact]
    public void CheckFileClassUseSnake()
    {
      var csOutputName = "CSOutputs3";

      var mockDBRepository = new MockDBRepository();
      var classEntities = mockDBRepository.GetClasses(DBParameterEntity.Create("SQLitePath"));
      var fileDataEntity = FileDataEntity.Create(csOutputName, "DB.Dto");
      var useSnakeCase = true;

      repository.Generate(classEntities, fileDataEntity, useSnakeCase);

      var fileNames = Directory.GetFiles(fileDataEntity.OutputPath).Where(filename => filename == $"{Path.Combine(fileDataEntity.OutputPath, "MTest.cs")}").ToList();
      Assert.Single(fileNames);

      var actual = string.Empty;
      using (StreamReader sr = new StreamReader(fileNames[0]))
      {
        actual = sr.ReadToEnd();
      }

      var expected = @"using System.ComponentModel.DataAnnotations.Schema;
namespace DB.Dto
{
  /// <summary>
  /// マスタテーブル
  /// </summary>
  public class MTest
  {
    public MTest()
    {
    }
    
    /// <summary>
    /// intになる
    /// </summary>
    [Column(""int_1"")]
    public int Int1{set; get;}
    
    /// <summary>
    /// Dateになる
    /// </summary>
    [Column(""Date_1_1"")]
    public DateTime Date1_1{set; get;}
  }
}
";

      Assert.Equal(expected, actual);

      // テスト終了後にフォルダ削除
      Directory.Delete(csOutputName, true);
    }
  }
}
