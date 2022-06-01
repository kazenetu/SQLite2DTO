using Application;
using Application.Model;
using Domain.Exceptions;
using PostgreSQL2DTOTest.Shared;
using System;
using Xunit;

namespace PostgreSQL2DTOTest.ApplicationTest
{
  /// <summary>
  /// CSファイル作成アプリケーションサービスのテスト
  /// </summary>
  public class TestGenerateCSApplicationService : IDisposable
  {
    /// <summary>
    /// テスト対象
    /// </summary>
    private GenerateCSApplicationService applicationService;

    /// <summary>
    /// Setup
    /// </summary>
    public TestGenerateCSApplicationService()
    {
      SetTestDI.SetDI();
      applicationService = new GenerateCSApplicationService();
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact]
    public void ExceptionParamNull()
    {
      var ex = Assert.ThrowsAny<DomainException>(() => applicationService.GenerateCSFileFromDB(null));
      Assert.Single(ex.MessageIds);
      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("inputParamModel[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void Success()
    {
      var useSnakeCase = false;
      var inputParamModel = new InputParamModel("nameSpace", "outputPath", "sqliteFilePath", useSnakeCase);
      var generetedFileResultsModel = applicationService.GenerateCSFileFromDB(inputParamModel);

      Assert.Equal(2, generetedFileResultsModel.Messages.Count);
      Assert.Equal($"  >>m_test... finish{Environment.NewLine}", generetedFileResultsModel.Messages[0]);
      Assert.Equal($"  >>t_test... finish{Environment.NewLine}", generetedFileResultsModel.Messages[1]);
    }
  }
}
