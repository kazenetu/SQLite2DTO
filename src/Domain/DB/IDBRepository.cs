using System.Collections.Generic;
using Domain.Classes;

namespace Domain.DB
{
  /// <summary>
  /// DBリポジトリ インターフェイス
  /// </summary>
  public interface IDBRepository
  {
    /// <summary>
    /// DBからClassEntityリストを取得する
    /// </summary>
    /// <returns>ClassEntityリスト</returns>
    List<ClassEntity> GetClasses(DBParameterEntity parameterEntity);
  }
}